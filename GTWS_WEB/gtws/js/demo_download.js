/**
 * 本地视频抓拍
 */
function localSnapshot()
{
	if (ocx)
	{

		var wnd = $("#snapshotwnd").val();
		if (wnd == "")
		{
			alert(langs[lang]["inputWndFirst"]);
			return;
		}
		
		result = ocx.IVS_OCX_LocalSnapshot(wnd);
		
		$("#resultcode").val("IVS_OCX_LocalSnapshot:" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var snapshotFormat = $('#snapshotformat').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var snapshotPath = $('#snapshotpath').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var localCaptureConfigxml = '&lt;CaptureConfig&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;CapturePath&gt;' + snapshotPath + '&lt;/CapturePath&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;CaptureDownloadPath&gt;' + snapshotPath + '&lt;/CaptureDownloadPath&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;SnapshotMode&gt;0&lt;/SnapshotMode&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;SnapshotCount&gt;1&lt;/SnapshotCount&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;SnapshotInterval&gt;1&lt;/SnapshotInterval&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;SnapshotFormat&gt;' + snapshotFormat + '&lt;/SnapshotFormat&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;NameRule&gt;1&lt;/NameRule&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;localCaptureConfigxml += '&lt;/CaptureConfig&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_SetLocalCaptureConfig(localCaptureConfigxml);");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = $('#snapshotwnd').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_LocalSnapshot(wnd);");
		showCode("} ");
	}
}

/**
 * 获取录像下载信息
 */
function getDownloadInfo()
{
	if (ocx)
	{
		var downloadHandle = $("#downloadhandle").val();
		if (downloadHandle == "")
		{
			alert(langs[lang]["noDownloadTask"]);
			return;
		}
		var resultXml = ocx.IVS_OCX_GetDownloadInfo(downloadHandle);
		 
		xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			var status = (xmlDoc.find("Status").text()=="0"? langs[lang]["pauseLbl"] : langs[lang]["downLbl"]);
			var speed = xmlDoc.find("Speed").text() +  "KB/S";
			var size = xmlDoc.find("Size").text() + "KB";
			var timeLeft = xmlDoc.find("TimeLeft").text() + "S";
			var progress = xmlDoc.find("Progress").text() + "%";
			
			var downloadInfo = langs[lang]["downStatus"] +
					            ":" + status + 
								"\n" +
								langs[lang]["downSpeed"] +
								":" + speed +  
								"\n" +
								langs[lang]["downSize"] +
								":" +   size +   
								"\n" +
								langs[lang]["downTimeLeft"]  +
								":" + timeLeft +   
								"\n" +
								langs[lang]["downProgress"]  +
								":"  + progress; 
 			$("#downloadinfo").val(downloadInfo);
		}
	}
}

/**
 * 开始录像下载
 */
function startDownload(downloadType)
{
	if (ocx)
	{
		var cameraCode = $("#downloadcameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		var startTime = $("#starttime").val();
		var endTime = $("#endtime").val();
		
		// 界面输入的时间为本地时间，需要转换成标准时间
		//startTime = toUTCString(startTime);
		//endTime = toUTCString(endTime);
		
		var downloadFilePath = $("#downloadfilePath").val();
		
		var downloadParam = $("#downloadparam").val();
		
		if (downloadParam == "")
		{
			downloadParam += "<?xml version='1.0' encoding='UTF-8'?>";
		    downloadParam += "<Content>";
			downloadParam += "    <DownloadParam>";
			downloadParam += "        <RecordFormat>1</RecordFormat>";               // 录像格式：1-MP4
			downloadParam += "        <SplitterType>1</SplitterType>";               // 录像分割方式：1-按时间 2-按容量
			
			/**
			 * 录像分割值：
			 * 按时间分割，5-720分钟，默认30分钟，同时满足文件大小不超过3072MB的限制；
			 * 按容量分割，200-3072MB，默认2048MB。
			 */
			downloadParam += "        <SplitterValue>100</SplitterValue>";           
			downloadParam += "        <DiskWarningValue>200</DiskWarningValue>";     // 磁盘空间小于此值告警，单位M
			downloadParam += "        <StopRecordValue>100</StopRecordValue>";       // 磁盘空间小于此值停止录像，单位M
			downloadParam += "        <NameRule>1</NameRule>";                       // 录像文件命名规则 	 1-规则1 2-规则2
			downloadParam += "        <EncryptRecord>0</EncryptRecord>";             // 下载录像文件是否加密 0-不加密，1-加密
		    //downloadParam += "        <RecordPWD></RecordPWD>";                    // 加密密码：最大32个字节（如果加密则必填）
			/**
			 * 设置下载速度。
			 * 平台录像下载时："1"为正常速率下载(速度等同于播放视频速度)，"255"为全速下载
	         * 设备前端录像下载时：	"1"为正常速率下载; "2"为两倍速率下载
	         * 比如：视频码流1Mbps，选择"1"下载时，下载速率为1Mbps上下，选择"255"下载时，下载速率是本机与服务器间的网络带宽的上限，可能达到10Mbps以上。
	         */            
			downloadParam += "        <DownloadSpeed>1</DownloadSpeed>"; 
			downloadParam += "        <SavePath>" + downloadFilePath + "</SavePath>";// 本地录像存放路径 
			downloadParam +=  "   </DownloadParam>";
			downloadParam +=  "</Content>";
			
			 $("#downloadparam").val(downloadParam);
		}
		else 
		{
			var index = downloadParam.indexOf("<SavePath>");
			downloadParam = downloadParam.substring(0, index);
			downloadParam += "<SavePath>" + downloadFilePath + "</SavePath>";  
			downloadParam += "</DownloadParam>";
			downloadParam += "</Content>";
			$("#downloadparam").val(downloadParam);
		}
		 
		var resultXml = "";
		var result = "";
		var xmlDoc;
		if (downloadType == 0 ) 
		{
			resultXml= ocx.IVS_OCX_StartPlatformDownload(cameraCode, startTime, endTime, downloadParam);
			
			xmlDoc = $.parseXML(resultXml);
			xmlDoc = $(xmlDoc);
			result = xmlDoc.find("ResultCode").text();
			$("#resultcode").val("IVS_OCX_StartPlatformDownload:" + result);
		}
		else 
		{
			resultXml= ocx.IVS_OCX_StartPUDownload(cameraCode, startTime, endTime, downloadParam);
			
			xmlDoc = $.parseXML(resultXml);
			xmlDoc = $(xmlDoc);
			result = xmlDoc.find("ResultCode").text();
			$("#resultcode").val("IVS_OCX_StartPUDownload:" + result);
		}
		 
		if (result == 0 )
		{
			var handle = xmlDoc.find("DownloadHandle").text();
			$("#downloadhandle").val(handle);
		}
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#downloadcameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var startTime = $('#starttime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var endTime = $('#endtime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var downloadFilePath = $('#downloadfilePath').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var downloadParam = '&lt;?xml version='1.0' encoding='UTF-8'?&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;DownloadParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;RecordFormat&gt;1&lt;/RecordFormat&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;SplitterType&gt;1&lt;/SplitterType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;SplitterValue&gt;100&lt;/SplitterValue&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;DiskWarningValue&gt;100&lt;/DiskWarningValue&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;StopRecordValue&gt;100&lt;/StopRecordValue&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;NameRule&gt;1&lt;/NameRule&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;EncryptRecord&gt;0&lt;/EncryptRecord&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;DownloadSpeed&gt;1&lt;/DownloadSpeed&gt;'; //DownloadSpeed：Platform：1-1x; 255-Full Speed  PU：	1-1x; 2-2x");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam += '&lt;SavePath&gt;' + downloadFilePath + '&lt;/SavePath&gt;'; ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam +=  '&lt;/DownloadParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;downloadParam +=  '&lt;/Content&gt;';");
		if (downloadType == 0 ) 
		{
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;var resultXml= ocx.IVS_OCX_StartPlatformDownload(cameraCode, startTime, endTime, downloadParam);");
		}
		else 
		{
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;var resultXml= ocx.IVS_OCX_StartPUDownload(cameraCode, startTime, endTime, downloadParam);");
		}
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;xmlDoc = $.parseXML(resultXml);");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;xmlDoc = $(xmlDoc);");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;result = xmlDoc.find('ResultCode').text();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;if (result == 0 )");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;{");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var handle = xmlDoc.find('DownloadHandle').text();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$('#downloadhandle').val(handle);");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;}");
		showCode("} ");
	}
}

/**
 * 停止录像下载
 */
function stopDownload(downloadType)
{
	if (ocx)
	{
		var downloadHandle = $("#downloadhandle").val();
		if (downloadHandle == "") 
		{
			alert(langs[lang]["noDownloadTask"]);
			return;
		}
		var result = "";
		if (downloadType == 0 ) 
		{
			result = ocx.IVS_OCX_StopPlatformDownload(downloadHandle);
			$("#resultcode").val("IVS_OCX_StopPlatformDownload:" + result);
		}
		else 
		{
			result = ocx.IVS_OCX_StopPUDownload(downloadHandle);
			$("#resultcode").val("IVS_OCX_StopPUDownload:" + result);
		}
		
		if (result == 0)
		{
			$("#downloadhandle").val("");
			$("#downloadinfo").val("");
		}
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var downloadHandle = $('#downloadhandle').val();");
		if (downloadType == 0 ) 
		{
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopPlatformDownload(downloadHandle);");
		}
		else
		{
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopPUDownload(downloadHandle);");
		}
		showCode("} "); 
	}
}

/**
 * 暂停录像下载
 */
function downloadPause()
{
	if (ocx)
	{
		var downloadHandle = $("#downloadhandle").val();
		
		if (downloadHandle == "") 
		{
			alert(langs[lang]["noDownloadTask"]);
			return;
		}
		var result = ocx.IVS_OCX_DownloadPause(downloadHandle);
		
		$("#resultcode").val("IVS_OCX_DownloadPause:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var downloadHandle = $('#downloadhandle').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_DownloadPause(downloadHandle);");
		showCode("} "); 
	}
}

/**
 * 恢复录像下载
 */
function downloadResume()
{
	if (ocx)
	{
		var downloadHandle = $("#downloadhandle").val();
		
		if (downloadHandle == "") 
		{
			alert(langs[lang]["noDownloadTask"]);
			return;
		}
		var result = ocx.IVS_OCX_DownloadResume(downloadHandle);
		
		$("#resultcode").val("IVS_OCX_DownloadResume:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var downloadHandle = $('#downloadhandle').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_DownloadResume(downloadHandle);");
		showCode("} "); 
	}
}

/**
 * 平台抓拍
 */
function platformSnapshot()
{
	alert("平台抓拍");
	if (ocx)
	{
		var cameraCode = $("input[name='camera']:checked").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["selectCameraFirst"]);
			return;
		}
		var result = ocx.IVS_OCX_PlatformSnapshot(cameraCode);
		
		$("#resultcode").val("IVS_OCX_PlatformSnapshot:" + result);
	}
}

/**
 * 获取平台抓拍图片列表
 */
function getSnapshotList()
{
	alert("图片列表");
	if (ocx)
	{
		
		var cameraCode = $("input[name='camera']:checked").val();
		
		var pQueryParamXml = "";
		pQueryParamXml += "<Content>";
		pQueryParamXml += "    <CameraCode>" + cameraCode + "</CameraCode>";     
		pQueryParamXml += "    <PageInfo>";
		pQueryParamXml += "        <FromIndex>1</FromIndex>";
		pQueryParamXml += "        <ToIndex>10</ToIndex>";
		pQueryParamXml += "    </PageInfo>";
		pQueryParamXml += "    <TimeInfo>";
		pQueryParamXml += "        <FromTime>20131021125959</FromTime>";
		pQueryParamXml += "        <ToTime>20131025125959</ToTime>";
		pQueryParamXml += "    </TimeInfo>";
		pQueryParamXml += "    <SnapType>4</SnapType>";                                
		pQueryParamXml += "</Content>";
		
		var resultXml = ocx.IVS_OCX_GetSnapshotList(pQueryParamXml);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		var result = xmlDoc.find("ResultCode").text();
		if (result)
		{
			alert(resultXml);
		}
	}
}

/**
 * 获取平台抓拍图片列表
 */
function deleteSnapshot()
{
	if (ocx)
	{
		var cameraCode = $("input[name='camera']:checked").val();
		var pictureID = "1292";
		var result = ocx.IVS_OCX_DeleteSnapshot(cameraCode, pictureID);
		
		$("#resultcode").val("IVS_OCX_DeleteSnapshot:" + result);
	}
}
