/**
 * 开始平台录像
 */
function startPlatRecord()
{
	if (ocx)
	{
		var cameraCode = $("#platrecordcameracode").val();
		var recordTime = $("#platrecordtime").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		if (recordTime=="" || recordTime == 0) 
		{
			alert(langs[lang]["inputRecordTime"]);
			return;
		}
		
		var resultXml = ocx.IVS_OCX_GetRecordStatus(cameraCode, 0);
		 
		xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		var result = xmlDoc.find("ResultCode").text();
		if (result == 0)
		{
			result = xmlDoc.find("RecordState").text();
			if (result.substr(2) == "1")
			{
				alert(langs[lang]["hasRecordTask"]);
				return;
			}
		}
		
		result = ocx.IVS_OCX_StartPlatformRecord(cameraCode, recordTime);
		
		$("#resultcode").val("IVS_OCX_StartPlatformRecord:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#platrecordcameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var recordTime = $('#platrecordtime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartPlatformRecord(cameraCode, recordTime);");
		showCode("} ");
	}
}

/**
 * 停止平台录像
 */
function stopPlatRecord()
{
	if (ocx)
	{
		var cameraCode = $("#platrecordcameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		
		var resultXml = ocx.IVS_OCX_GetRecordStatus(cameraCode, 0);
		 
		xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		var result = xmlDoc.find("ResultCode").text();
		if (result == 0)
		{
			result = xmlDoc.find("RecordState").text();
			if (result.substr(2) == "0")
			{
				alert(langs[lang]["noRecordTask"]);
				return;
			}
		}
		
		result = ocx.IVS_OCX_StopPlatformRecord(cameraCode);
		
		$("#resultcode").val("IVS_OCX_StopPlatformRecord：" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#platrecordcameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopPlatformRecord(cameraCode);");
		showCode("} ");
	}
}

/**
 * 开始摄像机前端录像
 */
function startPuRecord()
{
	if (ocx)
	{
		var cameraCode = $("#purecordcameracode").val();
		var recordTime = $("#purecordtime").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		if (recordTime=="" || recordTime == 0) 
		{
			alert(langs[lang]["inputRecordTime"]);
			return;
		}
		var result = ocx.IVS_OCX_StartPURecord(cameraCode, recordTime);
		
		$("#resultcode").val("IVS_OCX_StartPURecord:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#purecordcameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var recordTime = $('#purecordtime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartPURecord(cameraCode, recordTime);");
		showCode("} ");
	}
}

/**
 * 停止摄像机前端录像
 */
function stopPuRecord()
{
	if (ocx)
	{
		var cameraCode = $("#purecordcameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		var result = ocx.IVS_OCX_StopPURecord(cameraCode);
		
		$("#resultcode").val("IVS_OCX_StopPURecord:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#purecordcameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopPURecord(cameraCode);");
		showCode("} ");
	}
}
	
/**
 * 开始本地录像
 */
function startLocalRecord()
{
	if (ocx)
	{
		var localConfig = ocx.IVS_OCX_GetLocalRecordConfig();
		var xmlDoc = $.parseXML(localConfig);
		xmlDoc = $(xmlDoc);
		
		var filename = xmlDoc.find("RecordPath").text();
		if ($("#filename").val() == "")
		{
			$("#filename").val(filename);
		}
		else
		{
			filename = $("#filename").val();
		}
		var videoparam = $("#videoparam").val();
		if (videoparam == "") 
		{
			videoparam += "<Content>";
			videoparam += "    <RecordParam>";
			videoparam += "        <RecordFormat>1</RecordFormat>";           // 录像文件格式：1-MP4
			videoparam += "        <SplitterType>1</SplitterType>";           // 录像文件分割方式：1-按时间，2-按容量
			/**
			 * 录像文件分割值：
			 * 按时间分割，5-720分钟，默认30分钟，同时满足文件大小不超过3072MB的限制；
			 * 按容量分割，200-3072MB，默认2048MB。
			 */
			videoparam += "        <SplitterValue>30</SplitterValue>";        
			videoparam += "        <DiskWarningValue>2048</DiskWarningValue>";// 磁盘空间小于此值告警，单位M 
			videoparam += "        <StopRecordValue>512</StopRecordValue>";   // 磁盘空间小于此值停止录像，单位M
			videoparam += "        <RecordTime>300</RecordTime>";             // 录像时长：整数，300 ~ 43200（12小时）
			videoparam += "        <EncryptRecord>0</EncryptRecord>";         // 是否加密：0-不加密，1-加密
			//videoparam += "        <RecordPWD></RecordPWD>";                // 加密密码：最大32个字节
			/**
			 * 录像文件命名规则：
			 * 1-摄像机名_序号_时间（开始时间-结束时间，YYYY-MM-DD-hh-mm-ss）
			 * 2-摄像机名_时间（开始时间-结束时间，YYYY-MM-DD-hh-mm-ss）_序号
			 * 3-时间（开始时间-结束时间，YYYY-MM-DD-hh-mm-ss）_摄像机名_序号
			 */
			videoparam += "        <NameRule>1</NameRule>"; 
			videoparam += "        <SavePath>" + filename + "</SavePath>";  
			videoparam += "    </RecordParam>";
			videoparam += "</Content>";
			
			$("#videoparam").val(videoparam);
		}
		else
		{
			var index = videoparam.indexOf("<SavePath>");
			videoparam = videoparam.substring(0, index);
			videoparam += "        <SavePath>" + filename + "</SavePath>"; 
			videoparam += "    </RecordParam>";
			videoparam += "</Content>";
			
			$("#videoparam").val(videoparam);
		}
		 
		var wnd = $("#localrecordwnd").val();
		if (wnd == "")
		{
			alert(langs[lang]["inputWndFirst"]);
			return;
		}
		
		var result = ocx.IVS_OCX_StartLocalRecord(videoparam, wnd);
		
		$("#resultcode").val("IVS_OCX_StartLocalRecord:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var filename = $('#filename').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var videoparam = '&lt;Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;RecordParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;RecordFormat&gt;1&lt;/RecordFormat&gt;'; //1-MP4");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;SplitterType&gt;1&lt;/SplitterType&gt;';  //1-By Time，2-By Capacity");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;SplitterValue&gt;30&lt;/SplitterValue&gt;';//By Time Value 5-60minutes，By Capacity 1-2048MB");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;DiskWarningValue&gt;2048&lt;/DiskWarningValue&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;StopRecordValue&gt;512&lt;/StopRecordValue&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;RecordTime&gt;300&lt;/RecordTime&gt;'; //300-43200seconds");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;EncryptRecord&gt;0&lt;/EncryptRecord&gt;'; // 0-no encrypt，1-encrypt");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;RecordTime&gt;&lt;/RecordTime&gt;'; ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;NameRule&gt;1&lt;/NameRule&gt;'; ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;SavePath&gt;' + filename + '&lt;/SavePath&gt;';  ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;/RecordParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;videoparam = videoparam + '&lt;/Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = $('#localrecordwnd').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartLocalRecord(videoparam, wnd);");
		showCode("} ");
		
	}
}

/**
 * 停止本地录像
 */
function stopLocalRecord()
{
	if (ocx)
	{
		var wnd = $("#localrecordwnd").val();
		
		var result = ocx.IVS_OCX_StopLocalRecord(wnd);
		
		$("#resultcode").val("IVS_OCX_StopLocalRecord：" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = $('#localrecordwnd').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopLocalRecord(wnd);");
		showCode("} ");
	}
}


/**
 * 添加录像计划
 */
function addRecordPlan()
{
	if (ocx)
	{
		var pRecordPlan = "<Content>";
		pRecordPlan += "	<CameraList>";
		pRecordPlan += "		<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
		pRecordPlan += "	</CameraList>";
		pRecordPlan += "	<Plan>";
		pRecordPlan += "		<RecordMethod>0</RecordMethod>";
		pRecordPlan += "		<IsEnable>1</IsEnable>";
		pRecordPlan += "		<PlanType>0</PlanType>";
		pRecordPlan += "		<PlanList>";
		pRecordPlan += "			<PlanInfo>";
		pRecordPlan += "				<DayType>1</DayType>";
		pRecordPlan += "				<TimeInfoNum>1</TimeInfoNum>";
		pRecordPlan += "				<TimeList>";
		pRecordPlan += "					<TimeInfo>";
		pRecordPlan += "						<StartTime>120000</StartTime>";
		pRecordPlan += "						<EndTime>180000</EndTime>";
		pRecordPlan += "					</TimeInfo>";
		pRecordPlan += "				</TimeList>";
		pRecordPlan += "			</PlanInfo>";
		pRecordPlan += "		</PlanList>";
		pRecordPlan += "	</Plan>";
		pRecordPlan += "</Content>";
		
		var resultXml = ocx.IVS_OCX_AddRecordPlan(pRecordPlan);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			alert(resultXml);
		}
	}
}

/**
 * 修改录像计划
 */
function modifyRecordPlan()
{
	if (ocx)
	{
		var pRecordPlan = "<Content>";
		pRecordPlan += "	<CameraList>";
		pRecordPlan += "		<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
		pRecordPlan += "	</CameraList>";
		pRecordPlan += "	<Plan>";
		pRecordPlan += "		<RecordMethod>0</RecordMethod>";
		pRecordPlan += "		<IsEnable>1</IsEnable>";
		pRecordPlan += "		<PlanType>0</PlanType>";
		pRecordPlan += "		<PlanList>";
		pRecordPlan += "			<PlanInfo>";
		pRecordPlan += "				<DayType>1</DayType>";
		pRecordPlan += "				<TimeInfoNum>1</TimeInfoNum>";
		pRecordPlan += "				<TimeList>";
		pRecordPlan += "					<TimeInfo>";
		pRecordPlan += "						<StartTime>180000</StartTime>";
		pRecordPlan += "						<EndTime>220000</EndTime>";
		pRecordPlan += "					</TimeInfo>";
		pRecordPlan += "				</TimeList>";
		pRecordPlan += "			</PlanInfo>";
		pRecordPlan += "		</PlanList>";
		pRecordPlan += "	</Plan>";
		pRecordPlan += "</Content>";
		
		var resultXml = ocx.IVS_OCX_ModifyRecordPlan(pRecordPlan);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			alert(resultXml);
		}
	}
}

/**
 * 删除录像计划
 */
function deleteRecordPlan()
{
	if (ocx)
	{
		var pRecordPlan = "<Content>";
		pRecordPlan += "	<CameraList>";
		pRecordPlan += "		<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
		pRecordPlan += "	</CameraList>";
		pRecordPlan += "	<Plan>";
		pRecordPlan += "		<RecordMethod>0</RecordMethod>";
		pRecordPlan += "	</Plan>";
		pRecordPlan += "</Content>";
		
		var resultXml = ocx.IVS_OCX_DeleteRecordPlan(pRecordPlan);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			alert(resultXml);
		}
	}
}


/**
 * 查询录像计划
 */
function getRecordPlan()
{
	if (ocx)
	{
		var pRecordPlanListReq = "";
		pRecordPlanListReq += "<Content>";
		pRecordPlanListReq += "    <CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
		pRecordPlanListReq += "    <RecordMethod>0</RecordMethod>";
		pRecordPlanListReq += "</Content>";
		var resultXml = ocx.IVS_OCX_GetRecordPlan(pRecordPlanListReq);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		var result = xmlDoc.find("ResultCode").text();
		if (result == 0)
		{
			alert(resultXml);
		}
	}
}

/**
 * 设置录像策略
 */
function setRecordPolicyByTime()
{
	var pRecordPolicyXml = "<Content>";
	 
	pRecordPolicyXml += "	<CameraCode>01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519</CameraCode>";
	pRecordPolicyXml += "	<RecordMode>0</RecordMode>";
	pRecordPolicyXml += "	<Time>30</Time>";
	pRecordPolicyXml += "	<FrameExtractRecordTime>0</FrameExtractRecordTime>";
	pRecordPolicyXml += "	<AlarmTime></AlarmTime>";
	pRecordPolicyXml += "	<KeyframeTime>0</KeyframeTime>";
	pRecordPolicyXml += "	<BeforeAlarm></BeforeAlarm>";
	pRecordPolicyXml += "	<AfterAlarm></AfterAlarm>";
	pRecordPolicyXml += "	<PlanStreamID>1</PlanStreamID>";
	pRecordPolicyXml += "	<AlarmStreamID>1</AlarmStreamID>";
	pRecordPolicyXml += "	<AlarmRecordTTL>0</AlarmRecordTTL>";
	pRecordPolicyXml += "	<ManualRecordTTL>0</ManualRecordTTL>";
	pRecordPolicyXml += "	<PreRecord>0</PreRecord>";
	pRecordPolicyXml += "	<PreRecordTime>30</PreRecordTime>";
	pRecordPolicyXml += "	<AssociatedAudio>0</AssociatedAudio>";
	pRecordPolicyXml += "</Content>";
	
	var result = ocx.IVS_OCX_SetRecordPolicyByTime(pRecordPolicyXml);
	alert(result);
}

/**
 * 获取录像策略
 */
function getRecordPolicyByTime()
{
	var resultXml = ocx.IVS_OCX_GetRecordPolicyByTime("01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519");
	var xmlDoc = $.parseXML(resultXml);
	xmlDoc = $(xmlDoc);
	var result = xmlDoc.find("ResultCode").text();
	if (result == 0)
	{
		alert(resultXml);
	}
}