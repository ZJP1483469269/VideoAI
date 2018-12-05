/**
* 获取摄像机设备列表
*/
function getCameraList() {
    if (ocx) {
        // 第一个参数为设备类型:"2"为摄像机设备，"3"为语音设备，"4"为告警设备，"9"为外部告警设备  
        // 第二和第三个参数为查询设备数量分页范围，传入 "1"和"1000"表示查询设备列表中前1000个设备
        var cameraListXml = ocx.IVS_OCX_GetDeviceList(2, 1, 100000);

        addCameraToList(cameraListXml);

        cleanUpCode();
        showCode("if (ocx)");
        showCode("{ ");
        showCode("&nbsp;&nbsp;&nbsp;&nbsp;//2-CameraDevice ;  3-VoiceDevice ; 4-AlarmDevice ; 9-external AlarmDevice  ");
        showCode("&nbsp;&nbsp;&nbsp;&nbsp;//1-FromIndex ; 1000-ToIndex");
        showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraList = ocx.IVS_OCX_GetDeviceList(2, 1, 100000);");
        showCode("} ");
    }
}

/**
* 解析摄像机列表查询结果cameraListXml
* 
* @param xml 查询设备列表返回结果Xml
*/
function addCameraToList(xml) {
    var xmlDoc = $.parseXML(xml);
    xmlDoc = $(xmlDoc);
    var result = xmlDoc.find("ResultCode").text();

    $("#resultcode").val("IVS_OCX_GetDeviceList:" + result);

    if (result == 0) {
        var videoInChanInfoNode = xmlDoc.find("VideoInChanInfo");

        $("#cameraList").empty();
        for (i = 0; i < videoInChanInfoNode.length; i++) {
            var cameraCode = xmlDoc.find("VideoInChanInfo:eq(" + i + ")").find("CameraCode").text();
            var parentCode = xmlDoc.find("VideoInChanInfo:eq(" + i + ")").find("NVRCode").text();
            var cameraStatus = xmlDoc.find("VideoInChanInfo:eq(" + i + ")").find("IsOnline").text();
            var cameraType = xmlDoc.find("VideoInChanInfo:eq(" + i + ")").find("CameraType").text();
            var cameraLocation = xmlDoc.find("VideoInChanInfo:eq(" + i + ")").find("CameraLocation").text();
            var cameraName = xmlDoc.find("VideoInChanInfo:eq(" + i + ")").find("CameraName").text();

            $("#cameraList").append("<input type='radio' name='camera' value=" + cameraCode + " camera_name=" + cameraName + "> (" + cameraStatus + ") " + cameraName + " (" + cameraCode + ") <br>");
        }
    }
}


function Video_OpenCamera(cCameraCode) {
    var wnd = ocx.IVS_OCX_GetFreeWnd();

    var xml = ocx.IVS_OCX_SetActiveWnd(wnd);
    if (xml != 0) {
        wnd = ocx.IVS_OCX_GetSelectWnd();
    }

    $("#livewnd").val(wnd);

    var streamType = $("#streamType").val();
    var protocolType = $("#protocolType").val();
    var direstFirst = $("#direstFirst").val();
    var multi = $("#multi").val();

    var pMediaParaxml =
			"<?xml version='1.0' encoding='UTF-8'?>" +
		    "<Content>" +
            "    <RealplayParam>" +
	        "        <StreamType>" + streamType + "</StreamType>" +
		    "        <ProtocolType>" + protocolType + "</ProtocolType>" +
			"        <DirectFirst>" + direstFirst + "</DirectFirst>" +
	        "        <Multicast>" + multi + "</Multicast>" +
	        "    </RealplayParam>" +
	        "</Content>";

    var result = ocx.IVS_OCX_StartRealPlay(pMediaParaxml, cCameraCode, wnd);
    alert("IVS_OCX_StartRealPlay:" + result);
    $("#resultcode").val("IVS_OCX_StartRealPlay:" + result);
    cleanUpCode();
}


/**
* 开始浏览实况
* 说明：开始实况播放操作前，必须先调用设置了播放窗格接口。
*/
function getLivePlay() {
    var cameraCode = $("input[name='camera']:checked").val();
    if (cameraCode == undefined || cameraCode == "") {
        alert(langs[lang]["selectCameraFirst"]);
        return;
    }

    var wnd = $("#livewnd").val();
    if (wnd == "") {
        var wnd = ocx.IVS_OCX_GetFreeWnd();

        var xml = ocx.IVS_OCX_SetActiveWnd(wnd);
        if (xml != 0) {
            wnd = ocx.IVS_OCX_GetSelectWnd();
        }

        $("#livewnd").val(wnd);
    }

    var streamType = $("#streamType").val();
    var protocolType = $("#protocolType").val();
    var direstFirst = $("#direstFirst").val();
    var multi = $("#multi").val();

    var pMediaParaxml =
			"<?xml version='1.0' encoding='UTF-8'?>" +
		    "<Content>" +
            "    <RealplayParam>" +
	        "        <StreamType>" + streamType + "</StreamType>" +
		    "        <ProtocolType>" + protocolType + "</ProtocolType>" +
			"        <DirectFirst>" + direstFirst + "</DirectFirst>" +
	        "        <Multicast>" + multi + "</Multicast>" +
	        "    </RealplayParam>" +
	        "</Content>";

    var result = ocx.IVS_OCX_StartRealPlay(pMediaParaxml, cameraCode, wnd);

    $("#resultcode").val("IVS_OCX_StartRealPlay:" + result);

    cleanUpCode();
}

/**
* 停止浏览实况
*/
function stopLivePlay() {
    if (ocx) {
        var wnd = $("#livewnd").val();
        if (wnd == "") {
            alert(langs[lang]["inputWndFirst"]);
            return;
        }

        var result = ocx.IVS_OCX_StopRealPlay(wnd);

        $("#resultcode").val("IVS_OCX_StopRealPlay:" + result);
    }
}

/**
* 云台控制方法
* @param optCode 云台操作类型编码
*/
function ptzControlMethod(optCode) {
    // 云台转动模式："1"为点动模式，"2"为连动模式
    var paramModel = 2;

    // 云台转动速度：1~10
    var paramSpeed = 5;

    // 操作类型为"1"时，是停止云台操作
    if (optCode == 1) {
        paramModel = "";
        paramSpeed = "";
    }

    var selectedWnd = ocx.IVS_OCX_GetSelectWnd();
    var cameraCode = ocx.IVS_OCX_GetCameraByWnd(selectedWnd);

    var resultXml = ocx.IVS_OCX_PtzControl(cameraCode, optCode, paramModel, paramSpeed);

    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);
    var result = xmlDoc.find("ResultCode").text();

    $("#resultcode").val("IVS_OCX_PtzControl:" + result);

    cleanUpCode();
}

/**
* 添加预置位
*/
function addPreset() {
    if (ocx) {
        var cameraCode = $("input[name='camera']:checked").val();
        if (cameraCode == undefined || cameraCode == "") {
            alert(langs[lang]["selectCameraFirst"]);
            return;
        }

        var presetName = $("#presetname").val();
        if (presetName == "") {
            alert(langs[lang]["inputPresetNameFirst"]);
            return;
        }

        var resultXml = ocx.IVS_OCX_AddPTZPreset(cameraCode, presetName);

        var xmlDoc = $.parseXML(resultXml);
        xmlDoc = $(xmlDoc);
        var result = xmlDoc.find("ResultCode").text();

        $("#resultcode").val("IVS_OCX_AddPTZPreset:" + result);

        cleanUpCode();
        var presetIndex = xmlDoc.find("Index").text();
        $("#presetindex").val(presetIndex);
    }

}

/**
* 删除预置位
*/
function delPreset() {
    if (ocx) {
        var cameraCode = $("input[name='camera']:checked").val();
        if (cameraCode == undefined || cameraCode == "") {
            alert(langs[lang]["selectCameraFirst"]);
            return;
        }

        var presetIndex = $("input[name='preset']:checked").val();
        if (presetIndex == "") {
            alert(langs[lang]["selectPresetFirst"]);
            return;
        }

        var result = ocx.IVS_OCX_DelPTZPreset(cameraCode, presetIndex);

        $("#resultcode").val("IVS_OCX_DelPTZPreset:" + result);
    }
}

/**
* 获取预置位列表
*/
function getPresetList() {
    if (ocx) {
        var cameraCode = $("input[name='camera']:checked").val();
        if (cameraCode == undefined || cameraCode == "") {
            alert(langs[lang]["selectCameraFirst"]);
            return;
        }

        var presetList = ocx.IVS_OCX_GetPTZPresetList(cameraCode);

        var xmlDoc = $.parseXML(presetList);
        xmlDoc = $(xmlDoc);
        var result = xmlDoc.find("ResultCode").text();
        if (result == 0) {
            var presetInfoNode = xmlDoc.find("PresetPTZ");

            $("#presetlist").empty();
            for (i = 0; i < presetInfoNode.length; i++) {
                var presetIndex = xmlDoc.find("PresetPTZ:eq(" + i + ")").find("Index").text();
                var presetName = xmlDoc.find("PresetPTZ:eq(" + i + ")").find("Name").text();

                $("#presetlist").append("<input type='radio' name='preset' value=" + presetIndex + " > " + presetName + "<br>");
            }
        }
        $("#resultcode").val("IVS_OCX_GetPTZPresetList:" + result);

        cleanUpCode();
    }
}

/**
* 调用预置位
* 说明：调用预置位使用的是云台控制接口
*/
function loadPreset() {
    if (ocx) {
        var cameraCode = $("input[name='camera']:checked").val();
        if (cameraCode == undefined || cameraCode == "") {
            alert(langs[lang]["selectCameraFirst"]);
            return;
        }

        var presetIndex = $("input[name='preset']:checked").val();
        if (presetIndex == undefined || presetIndex == "") {
            alert(langs[lang]["selectPresetFirst"]);
            return;
        }

        // 调用预置位需要使用的云台操作类型编码为 "11"
        var optCode = 11;

        // 调用预置位时，云台操作模式使用预置位编码替代
        var paramModel = presetIndex;

        // 调用预置位时，云台转动模式使用 1~10 中的任意值
        var paramSpeed = 5;

        var resultXml = ocx.IVS_OCX_PtzControl(cameraCode, optCode, paramModel, paramSpeed);
        var xmlDoc = $.parseXML(resultXml);
        xmlDoc = $(xmlDoc);
        var result = xmlDoc.find("ResultCode").text();
        $("#resultcode").val("IVS_OCX_PtzControl:" + result);

        cleanUpCode();
      
    }
}


/**
* 设置看首位
*/
function setGuardPos() {
    if (ocx) {
        var cameraCode = $("input[name='camera']:checked").val();

        var guardPosInfoXml = "<Content>";
        guardPosInfoXml += "    <GuardPositionPara>";
        guardPosInfoXml += "        <CameraCode>" + cameraCode + "</CameraCode>";  // 0为查询全部类型
        guardPosInfoXml += "        <Enabled>1</Enabled>";
        guardPosInfoXml += "        <WaitTime>300</WaitTime>";
        guardPosInfoXml += "        <PresetIndex>6</PresetIndex>";
        guardPosInfoXml += "    </GuardPositionPara>";
        guardPosInfoXml += "</Content>";
        var result = ocx.IVS_OCX_SetGuardPos(guardPosInfoXml);

        alert(result);
    }
}

/**
* 删除看首位
*/
function delGuardPos() {
    if (ocx) {
        var cameraCode = $("input[name='camera']:checked").val();

        var guardPosInfoXml = "<Content>";
        guardPosInfoXml += "    <GuardPositionPara>";
        guardPosInfoXml += "        <CameraCode>" + cameraCode + "</CameraCode>";  // 0为查询全部类型
        guardPosInfoXml += "        <Enabled>1</Enabled>";
        guardPosInfoXml += "        <WaitTime>300</WaitTime>";
        guardPosInfoXml += "        <PresetIndex>6</PresetIndex>";
        guardPosInfoXml += "    </GuardPositionPara>";
        guardPosInfoXml += "</Content>";
        var result = ocx.IVS_OCX_DelGuardPos(guardPosInfoXml);

        alert(result);
    }
}


/**
* 查询看守位
*/
function getGuardPos() {
    if (ocx) {
        var cameraCode = $("input[name='camera']:checked").val();

        var resultXml = ocx.IVS_OCX_GetGuardPos(cameraCode);
        alert(resultXml);
        var xmlDoc = $.parseXML(resultXml);
        xmlDoc = $(xmlDoc);

        var result = xmlDoc.find("ResultCode").text();

        if (result == 0) {

        }
    }
}

/**
* 增加巡航轨迹 
*/
function addCruiseTrack() {
    var pCuriseTrackInfo = "<Content>";
    pCuriseTrackInfo += "	<CruiseTrack>";
    pCuriseTrackInfo += "		<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
    pCuriseTrackInfo += "		<Name>testCuriseTrack</Name>";
    pCuriseTrackInfo += "		<CruiseType>1</CruiseType>";
    pCuriseTrackInfo += "		<CruisePointList>";
    pCuriseTrackInfo += "			<CruisePoint>";
    pCuriseTrackInfo += "				<PresetIndex>6</PresetIndex>";
    pCuriseTrackInfo += "				<DwellTime>10</DwellTime>";
    pCuriseTrackInfo += "				<Speed>5</Speed>";
    pCuriseTrackInfo += "			</CruisePoint>";
    pCuriseTrackInfo += "			<CruisePoint>";
    pCuriseTrackInfo += "				<PresetIndex>4</PresetIndex>";
    pCuriseTrackInfo += "				<DwellTime>10</DwellTime>";
    pCuriseTrackInfo += "				<Speed>5</Speed>";
    pCuriseTrackInfo += "			</CruisePoint>";
    pCuriseTrackInfo += "		</CruisePointList>";
    pCuriseTrackInfo += "	</CruiseTrack>";
    pCuriseTrackInfo += "</Content>";

    var resultXml = ocx.IVS_OCX_AddCruiseTrack(pCuriseTrackInfo);
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 删除巡航轨迹
*/
function delCruiseTrack() {
    var result = ocx.IVS_OCX_DelCruiseTrack("01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519", 1, 1);
    alert(result);
}

/**
* 修改巡航轨迹
*/
function modCruiseTrack() {
    var pCuriseTrackInfo = "<Content>";
    pCuriseTrackInfo += "	<CruiseTrack>";
    pCuriseTrackInfo += "		<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
    pCuriseTrackInfo += "		<CruiseNO>4</CruiseNO>";
    pCuriseTrackInfo += "		<Name>testCuriseTrack</Name>";
    pCuriseTrackInfo += "		<CruiseType>1</CruiseType>";
    pCuriseTrackInfo += "		<CruisePointList>";
    pCuriseTrackInfo += "			<CruisePoint>";
    pCuriseTrackInfo += "				<PresetIndex>6</PresetIndex>";
    pCuriseTrackInfo += "				<DwellTime>10</DwellTime>";
    pCuriseTrackInfo += "				<Speed>5</Speed>";
    pCuriseTrackInfo += "			</CruisePoint>";
    pCuriseTrackInfo += "			<CruisePoint>";
    pCuriseTrackInfo += "				<PresetIndex>5</PresetIndex>";
    pCuriseTrackInfo += "				<DwellTime>10</DwellTime>";
    pCuriseTrackInfo += "				<Speed>5</Speed>";
    pCuriseTrackInfo += "			</CruisePoint>";
    pCuriseTrackInfo += "		</CruisePointList>";
    pCuriseTrackInfo += "	</CruiseTrack>";
    pCuriseTrackInfo += "</Content>";

    var result = ocx.IVS_OCX_ModCruiseTrack(pCuriseTrackInfo);
    alert(result);

}

/**
* 获取巡航轨迹列表
*/
function getCruiseTrackList() {
    var resultXml = ocx.IVS_OCX_GetCruiseTrackList("01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519");
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 获取巡航轨迹信息
*/
function getCruiseTrack() {
    var resultXml = ocx.IVS_OCX_GetCruiseTrack("01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519", 4);
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 设置巡航计划
*/
function setCruisePlan() {
    var pCruisePlan = "<Content>";
    pCruisePlan += "	<CruisePlanInfo>";
    pCruisePlan += "		<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
    pCruisePlan += "		<IsEnable>1</IsEnable>";
    pCruisePlan += "		<PlanType>1</PlanType>";
    pCruisePlan += "		<PlanList>";
    pCruisePlan += "			<PlanInfo>";
    pCruisePlan += "				<DayType>0</DayType>";
    pCruisePlan += "				<TimeInfoNum>1</TimeInfoNum>";
    pCruisePlan += "				<TimeList>";
    pCruisePlan += "					<TimeInfo>";
    pCruisePlan += "						<StartTime>120000</StartTime>";
    pCruisePlan += "						<EndTime>180000</EndTime>";
    pCruisePlan += "						<TrackInfo>";
    pCruisePlan += "							<TrackType>1</TrackType>";
    pCruisePlan += "							<TrackIndex>2</TrackIndex>";
    pCruisePlan += "						</TrackInfo>";
    pCruisePlan += "					</TimeInfo>";
    pCruisePlan += "				</TimeList>";
    pCruisePlan += "			</PlanInfo>";
    pCruisePlan += "		</PlanList>";
    pCruisePlan += "	</CruisePlanInfo>";
    pCruisePlan += "</Content>";

    var result = ocx.IVS_OCX_SetCruisePlan(pCruisePlan);

    alert(result);
}

/**
* 获取巡航计划
*/
function getCruisePlan() {
    var resultXml = ocx.IVS_OCX_GetCruisePlan("01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519");
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}


/**
* 启动开关量输出告警
*/
function startAlarmOut() {
    var result = ocx.IVS_OCX_StartAlarmOut("01398830000000000301#ec7bac2727c548ce8c0d0caf25a43519");
    alert(result);
}

/**
* 停止开关量输出告警
*/
function stopAlarmOut() {
    var result = ocx.IVS_OCX_StopAlarmOut("01398830000000000301#ec7bac2727c548ce8c0d0caf25a43519");
    alert(result);
}

/**
* 告警订阅查询
*/
function subscribeAlarmQuery() {
    var xmlDoc = $.parseXML(ocx.IVS_OCX_GetUserID());
    xmlDoc = $(xmlDoc);
    var subscriberID = xmlDoc.find("UserID").text();

    var pReqXml = "<Content>";
    pReqXml += "	<DomainCode>ec7bac2727c548ce8c0d0caf25a43519</DomainCode>";
    pReqXml += "	<PageInfo>";
    pReqXml += "		<FromIndex>1</FromIndex>";
    pReqXml += "		<ToIndex>10</ToIndex>";
    pReqXml += "	</PageInfo>";
    pReqXml += "	<Subscribe>";
    pReqXml += "		<SubscriberInfo>";
    pReqXml += "			<Subscriber>1</Subscriber>";
    pReqXml += "			<SubscriberID>" + subscriberID + "</SubscriberID>";
    pReqXml += "		</SubscriberInfo>";
    pReqXml += "	</Subscribe>";
    pReqXml += "</Content>";

    var resultXml = ocx.IVS_OCX_SubscribeAlarmQuery(pReqXml);
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }

}


/**
* 设置摄像机OSD参数
*/
function setDeviceConfig() {
    var pReqXml = "<Content>";
    pReqXml += "	<OSDParaInfo>";
    pReqXml += "		<OSDEnable>1</OSDEnable>";
    pReqXml += "		<OSDTimeInfo>";
    pReqXml += "			<OSDTimeEnable>ON</OSDTimeEnable>";
    pReqXml += "			<TimeFormatType>2</TimeFormatType>";
    pReqXml += "			<TimeX>1</TimeX>";
    pReqXml += "			<TimeY>50</TimeY>";
    pReqXml += "		</OSDTimeInfo>";
    pReqXml += "		<OSDNameInfo>";
    pReqXml += "			<OSDNameEnable>ON</OSDNameEnable>";
    pReqXml += "			<OSDNameText>OSDName</OSDNameText>";
    pReqXml += "			<TextX>1</TextX>";
    pReqXml += "			<TextY>1</TextY>";
    pReqXml += "			<SwitchEnable>1</SwitchEnable>";
    pReqXml += "			<SwitchInterval>2</SwitchInterval>";
    pReqXml += "			<TextBlinkEnable>1</TextBlinkEnable>";
    pReqXml += "			<TextTranslucentEnable>0</TextTranslucentEnable>";
    pReqXml += "			<TextTranslucentPercent></TextTranslucentPercent>";
    pReqXml += "		</OSDNameInfo>";
    pReqXml += "		<OSDPlatform>";
    pReqXml += "			<PresetEnable>0</PresetEnable>";
    pReqXml += "			<PTZEnable>0</PTZEnable>";
    pReqXml += "			<PictureEnable>0</PictureEnable>";
    pReqXml += "		</OSDPlatform>";
    pReqXml += "	</OSDParaInfo>";
    pReqXml += "</Content>";
    var result = ocx.IVS_OCX_SetDeviceConfig(8, "01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519", pReqXml);

    alert(result);
}
/**
* 查询摄像机OSD参数
*/
function getDeviceConfig() {
    var resultXml = ocx.IVS_OCX_GetDeviceConfig(8, "01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519");
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 新增告警联动策略
*/
function addAlarmLinkage() {
    var xmlDoc = $.parseXML(ocx.IVS_OCX_GetUserID());
    xmlDoc = $(xmlDoc);
    var userID = xmlDoc.find("UserID").text();

    var pReqXml = "<Content>";
    pReqXml += "	<DomainCode>ec7bac2727c548ce8c0d0caf25a43519</DomainCode>";
    pReqXml += "	<Linkage>";
    pReqXml += "		<AlarmInCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</AlarmInCode>";
    pReqXml += "		<AlarmInType>01</AlarmInType>";
    pReqXml += "		<AlarmType>ALARM_TYPE_MOVE_DECTION</AlarmType>";
    pReqXml += "		<ActionList>";
    pReqXml += "			<Action>";
    pReqXml += "				<ActionType>5</ActionType>";
    pReqXml += "				<GlobalParam>0</GlobalParam>";
    pReqXml += "				<ActionBranch>0</ActionBranch>";
    pReqXml += "				<DevList>";
    pReqXml += "					<DevInfo>";
    pReqXml += "						<DevCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</DevCode>";
    pReqXml += "						<DevDomainCode>ec7bac2727c548ce8c0d0caf25a43519</DevDomainCode>";
    pReqXml += "						<Param>1</Param>";
    pReqXml += "					</DevInfo>";
    pReqXml += "				</DevList>";
    pReqXml += "				<UserList>";
    pReqXml += "				<UserInfo>";
    pReqXml += "					<UserID>" + userID + "</UserID>";
    pReqXml += "					<UserDomainCode>ec7bac2727c548ce8c0d0caf25a43519</UserDomainCode>";
    pReqXml += "				</UserInfo>";
    pReqXml += "				</UserList>";
    pReqXml += "			</Action>";
    pReqXml += "		</ActionList>";
    pReqXml += "	</Linkage>";
    pReqXml += "</Content>";
    var resultXml = ocx.IVS_OCX_AddAlarmLinkage(pReqXml);

    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 修改告警联动策略
*/
function modifyAlarmLinkage() {
    var xmlDoc = $.parseXML(ocx.IVS_OCX_GetUserID());
    xmlDoc = $(xmlDoc);
    var userID = xmlDoc.find("UserID").text();

    var pReqXml = "<Content>";
    pReqXml += "	<Linkage>";
    pReqXml += "		<LinkageID>1</LinkageID>";
    pReqXml += "		<AlarmInCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</AlarmInCode>";
    pReqXml += "		<AlarmInType>01</AlarmInType>";
    pReqXml += "		<AlarmType>ALARM_TYPE_MOVE_DECTION</AlarmType>";
    pReqXml += "		<ActionList>";
    pReqXml += "			<Action>";
    pReqXml += "				<ActionType>5</ActionType>";
    pReqXml += "				<GlobalParam>0</GlobalParam>";
    pReqXml += "				<ActionBranch>0</ActionBranch>";
    pReqXml += "				<DevList>";
    pReqXml += "					<DevInfo>";
    pReqXml += "						<DevCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</DevCode>";
    pReqXml += "						<DevDomainCode>ec7bac2727c548ce8c0d0caf25a43519</DevDomainCode>";
    pReqXml += "						<Param>6</Param>";
    pReqXml += "					</DevInfo>";
    pReqXml += "				</DevList>";
    pReqXml += "				<UserList>";
    pReqXml += "				<UserInfo>";
    pReqXml += "					<UserID>" + userID + "</UserID>";
    pReqXml += "					<UserDomainCode>ec7bac2727c548ce8c0d0caf25a43519</UserDomainCode>";
    pReqXml += "				</UserInfo>";
    pReqXml += "				</UserList>";
    pReqXml += "			</Action>";
    pReqXml += "		</ActionList>";
    pReqXml += "	</Linkage>";
    pReqXml += "</Content>";
    var result = ocx.IVS_OCX_ModifyAlarmLinkage(pReqXml);

    alert(result);
}

/**
* 查询告警联动策略详情
*/
function getAlarmLinkage() {
    var pReqXml = "<Content>";
    pReqXml += "	<DomainCode>ec7bac2727c548ce8c0d0caf25a43519</DomainCode>";
    pReqXml += "	<Linkage>";
    pReqXml += "		<LinkageID>3</LinkageID>";
    pReqXml += "		<AlarmInCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</AlarmInCode>";
    pReqXml += "		<AlarmInType>01</AlarmInType>";
    pReqXml += "		<DevDomainCode></DevDomainCode>";
    pReqXml += "		<AlarmType>ALARM_TYPE_MOVE_DECTION</AlarmType>";
    pReqXml += "	</Linkage>";
    pReqXml += "</Content>";

    var resultXml = ocx.IVS_OCX_GetAlarmLinkage(pReqXml);
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 删除告警联动策略
*/
function deleteAlarmLinkage() {
    var alarmInCode = "01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519";
    var pReqXml = "";
    pReqXml += "<Content>";
    pReqXml += "<Linkage>";
    pReqXml += "<LinkageID>3</LinkageID>"; //联动策略ID
    pReqXml += "<AlarmInCode>" + alarmInCode + "</AlarmInCode>"; //告警源编码
    pReqXml += "<AlarmInType>01</AlarmInType>"; //告警源类型 01-摄像机
    pReqXml += "<AlarmType>ALARM_TYPE_MOVE_DECTION</AlarmType>"; //告警类型:移动侦测告警
    pReqXml += "</Linkage>";
    pReqXml += "</Content>";

    var result = ocx.IVS_OCX_DeleteAlarmLinkage(pReqXml);
    alert(result);
}


/**
* 查询告警联动策略列表
*/
function getAlarmLinkageList() {
    var pReqXml = "<Content>";
    pReqXml += "		<AlarmInCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</AlarmInCode>";
    pReqXml += "		<AlarmInType>01</AlarmInType>";
    pReqXml += "</Content>";

    var resultXml = ocx.IVS_OCX_GetAlarmLinkageList(pReqXml);
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 查询告警联动动作详情
*/
function getAlarmLinkageAction() {
    var pReqXml = "<Content>";
    pReqXml += "	<DomainCode>ec7bac2727c548ce8c0d0caf25a43519</DomainCode>";
    pReqXml += "	<Linkage>";
    pReqXml += "		<AlarmInCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</AlarmInCode>";
    pReqXml += "		<AlarmInType>01</AlarmInType>";
    pReqXml += "		<AlarmType>ALARM_TYPE_MOVE_DECTION</AlarmType>";
    pReqXml += "		<ActionType>5</ActionType>";
    pReqXml += "	</Linkage>";
    pReqXml += "</Content>";

    var resultXml = ocx.IVS_OCX_GetAlarmLinkageAction(pReqXml);
    alert(resultXml);
    var xmlDoc = $.parseXML(resultXml);
    xmlDoc = $(xmlDoc);

    if (result == 0) {

    }
}

/**
* 停止平台录像同步回放
*/
function stopSyncPlay() {
    var result = ocx.IVS_OCX_StopSyncPlay();
}

/**
* 开启平台录像同步回放
*/
function startSyncPlay() {
    var pSyncPlayInfo = "<Content>";
    pSyncPlayInfo += "	<SyncPlayInfo>";
    pSyncPlayInfo += "		<ProtocolType>2</ProtocolType>";
    pSyncPlayInfo += "		<PlayList>";
    pSyncPlayInfo += "			<PlayInfo>";
    pSyncPlayInfo += "				<WndID>1</WndID>";
    pSyncPlayInfo += "				<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
    pSyncPlayInfo += "				<TimeSpanList>";
    pSyncPlayInfo += "					<TimeInfo>";
    pSyncPlayInfo += "						<StartTime>20131026060820</StartTime>";
    pSyncPlayInfo += "						<EndTime>20131026065820</EndTime>";
    pSyncPlayInfo += "					</TimeInfo>";
    pSyncPlayInfo += "				</TimeSpanList>";
    pSyncPlayInfo += "			</PlayInfo>";
    pSyncPlayInfo += "			<PlayInfo>";
    pSyncPlayInfo += "				<WndID>2</WndID>";
    pSyncPlayInfo += "				<CameraCode>20000880000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
    pSyncPlayInfo += "				<TimeSpanList>";
    pSyncPlayInfo += "					<TimeInfo>";
    pSyncPlayInfo += "						<StartTime>20131026060820</StartTime>";
    pSyncPlayInfo += "						<EndTime>20131026065820</EndTime>";
    pSyncPlayInfo += "					</TimeInfo>";
    pSyncPlayInfo += "				</TimeSpanList>";
    pSyncPlayInfo += "			</PlayInfo>";
    pSyncPlayInfo += "		</PlayList>";
    pSyncPlayInfo += "	</SyncPlayInfo>";
    pSyncPlayInfo += "</Content>";

    var result = ocx.IVS_OCX_StartSyncPlay(pSyncPlayInfo);
    alert(result);
}

