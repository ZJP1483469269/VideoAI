
/**
 * 获取摄像列表
 */
function getRecordList()
{
	if (ocx)
	{
		var xmlDoc = $.parseXML(ocx.IVS_OCX_GetUserID());
		xmlDoc = $(xmlDoc);
		var operatorID = xmlDoc.find("UserID").text();
		
		var fromTime = $("#fromtime").val();
		var toTime = $("#totime").val();
		var recordMethod = $("#recordmethod").val();
		var cameraCode = $("#cameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		
		var queryType = 0;       // 按时间查询
		var recordType = 1100;   // 查询手动录像和计划录像
		
		var queryXmlStr = "";    // 查询条件Xml
		queryXmlStr += "<Content>";
		queryXmlStr += "    <PageInfo>";
		queryXmlStr += "        <FromIndex>1</FromIndex>";
		queryXmlStr += "        <ToIndex>1000</ToIndex>";
		queryXmlStr += "        <QueryCond>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>CameraCode</Field>";
		queryXmlStr += "		        <Value> "+ cameraCode+ "</Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>QueryType</Field>";
		queryXmlStr += "		        <Value>" + queryType + "</Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>RecordMethod</Field>";
		queryXmlStr += "		        <Value> " + recordMethod + " </Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>RecordType</Field>";
		queryXmlStr += "		        <Value> " + recordType + "</Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>FromTime</Field>";
		queryXmlStr += "		        <Value> " + fromTime + " </Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>ToTime</Field>";
		queryXmlStr += "		        <Value> " + toTime + " </Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>OperatorID</Field>";
		queryXmlStr += "		        <Value> " + operatorID + " </Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "        </QueryCond>";
		queryXmlStr += "    </PageInfo>";
		queryXmlStr += "</Content>";
		
		var recordListXml = ocx.IVS_OCX_QueryRecordList(queryXmlStr);
		 
		xmlDoc = $.parseXML(recordListXml);
		xmlDoc = $(xmlDoc);
		var result = xmlDoc.find("ResultCode").text();
		$("#resultcode").val("IVS_OCX_QueryRecordList:" + result);
		
		if (result == 0)
		{
			var recordDataInfoNode = xmlDoc.find("RecordDataInfo");
			cameraCode = xmlDoc.find("CameraCode").text();
		    $("#recordlist").empty();
			for (i = 0; i < recordDataInfoNode.length; i++)
			{
				recordMethod = xmlDoc.find("RecordDataInfo:eq(" + i + ")").find("RecordMethod").text();
				var startTime = xmlDoc.find("RecordDataInfo:eq(" + i + ")").find("StartTime").text();
				var endTime = xmlDoc.find("RecordDataInfo:eq(" + i + ")").find("EndTime").text();
				 
				var htmlStr = "<div>" + 
						"<input type='text' id='cameraCode" + i + "' value='"+ cameraCode+ "' style='width: 120px;' readonly='readonly'>" +
						"<span>|</span>" +  
						"<input type='text' id='startTime" + i + "' value='"+ startTime+ "' style='width: 100px;' readonly='readonly'>" +
						"<span>|</span>" + 
						"<input type='text' id='endTime" + i + "' value='"+ endTime+ "' style='width: 100px;' readonly='readonly'>" +
						"<span>|</span>" + 
						"<input type='text' id='recordMethod" + i + "' value='"+ (recordMethod == 0? langs[lang]["recordMethod0"] : langs[lang]["recordMethod1"])+ "' style='width: 70px;' readonly='readonly'>" +
						"<span>|</span>" + 
						"<input type='text' id='wnd" + i +"' style='width: 70px;'>" +
						"<span>|</span>" +
						"<input type='button' value=" + langs[lang]["startPlayBack"] +" onclick='startPlayBack("+ i +")'>" + 
						"<input type='button' value=" + langs[lang]["stopPlayBack"] +" onclick='stopPlayBack("+ i +")'>" + 
						"</div>";
				
				$("#recordlist").append(htmlStr);
			}
		}

		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var xmlDoc = $.parseXML(ocx.IVS_OCX_GetUserID());");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;xmlDoc = $(xmlDoc);");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var operatorID = xmlDoc.find('UserID').text();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var fromTime = $('#fromtime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var toTime = $('#totime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var recordMethod = $('#recordmethod').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#cameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var queryType = 0;");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var recordType = 110;");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var queryXmlStr = '&lt;Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '&lt;PageInfo&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '&lt;FromIndex&gt;1&lt;/FromIndex&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '&lt;ToIndex&gt;1000&lt;/ToIndex&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '&lt;QueryCond&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Field&gt;CameraCode&lt;/Field&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Value&gt; '+ cameraCode+ '&lt;/Value&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;QueryType&gt;EXACT&lt;/QueryType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;/QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Field&gt;QueryType&lt;/Field&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Value&gt;' + queryType + '&lt;/Value&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;QueryType&gt;EXACT&lt;/QueryType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;/QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Field&gt;RecordMethod&lt;/Field&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Value&gt; ' + recordMethod + ' &lt;/Value&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;QueryType&gt;EXACT&lt;/QueryType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;/QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Field&gt;RecordType&lt;/Field&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Value&gt; ' + recordType + '&lt;/Value&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;QueryType&gt;EXACT&lt;/QueryType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;/QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Field&gt;FromTime&lt;/Field&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Value&gt; ' + fromTime + ' &lt;/Value&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;QueryType&gt;EXACT&lt;/QueryType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;/QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Field&gt;ToTime&lt;/Field&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Value&gt; ' + toTime + ' &lt;/Value&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;QueryType&gt;EXACT&lt;/QueryType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;/QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Field&gt;OperatorID&lt;/Field&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;Value&gt; ' + operatorID + ' &lt;/Value&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '		&lt;QueryType&gt;EXACT&lt;/QueryType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '	&lt;/QueryField&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '&lt;/QueryCond&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '&lt;/PageInfo&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;queryXmlStr = queryXmlStr + '&lt;/Content&gt;';");
		
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var recordListXml = ocx.IVS_OCX_QueryRecordList(queryXmlStr);");
		showCode("} ");
	}
}

/**
 * 开始平台和前端录像回放
 * @param id 窗格编号
 */
function startPlayBack(id)
{
	if (ocx) 
	{
		//ocx.IVS_OCX_StopAllRealPlay();
		
		var wndid = "wnd"+id;
		var wnd = $('#'+ wndid).val();
		if (wnd == "")
		{
			alert(langs[lang]["inputWndFirst"]);
			return;
		}
		var startTimeid = "startTime"+id;
		var startTime = $('#'+ startTimeid).val();
		
		var endTimeid = "endTime"+id;
		var endTime = $('#'+ endTimeid).val();
		
		//startTime = toUTCString(startTime);
		//endTime = toUTCString(endTime);
		
		var cameraCode = $("#cameracode").val();
		var recordMethod = $("#recordmethod").val();
		
		var playbackParam = "";
		playbackParam += "<Content>";
		playbackParam += "    <PlaybackParam>";
		playbackParam += "        <ProtocolType>2</ProtocolType>";             // 使用TCP协议执行录像回放
		playbackParam += "        <StartTime>" + startTime + "</StartTime>";   // 设置起始时间，从录像中任意时间点开始播放，为空是从录像片段的开始时间点播放
		playbackParam += "        <EndTime> " + endTime + " </EndTime>";       // 设置结束时间，播放到该时间点时，停止录像回放
		playbackParam += "        <Speed>1</Speed>";                           // 正常速度播放文件
		playbackParam += "    </PlaybackParam>";
		playbackParam += "</Content>";
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = $('#wnd').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var startTime = $('#startTime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var endTime = $('#endTime').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#cameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var playbackParam = '&lt;Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;PlaybackParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;ProtocolType&gt;2&lt;/ProtocolType&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;StartTime&gt;' + startTime + '&lt;/StartTime&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;EndTime&gt; ' + endTime + ' &lt;/EndTime&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;Speed>1&lt;/Speed&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;/PlaybackParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;/Content&gt;';");
		
		var result = 0;
		if (recordMethod  == 0) 
		{
			result = ocx.IVS_OCX_StartPlatformPlayBack(cameraCode, playbackParam, wnd);
			$("#resultcode").val("IVS_OCX_StartPlatformPlayBack:" + result);
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartPlatformPlayBack(cameraCode, playbackParam, wnd);");
		}
		else if (recordMethod  == 1) 
		{
			result = ocx.IVS_OCX_StartPUPlayBack(cameraCode, playbackParam, wnd);
			$("#resultcode").val("IVS_OCX_StartPUPlayBack:" + result);
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartPUPlayBack(cameraCode, playbackParam, wnd);");
		
		}
		showCode("} ");
	}
}

/**
 * 停止平台和前端录像回放
 * @param id 窗格编号
 */
function stopPlayBack(id)
{
	if (ocx) 
	{
		var wndid = "wnd"+id;
		var wnd = $('#'+ wndid).val();
		if (wnd == "")
		{
			alert(langs[lang]["inputWndFirst"]);
			return;
		} 
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = $('#wnd').val();");
		var result;
		var recordMethod = $("#recordmethod").val();
		if (recordMethod  == 0) 
		{
			// 停止录像回放
			result = ocx.IVS_OCX_StopPlatformPlayBack(wnd);
			
			$("#resultcode").val("IVS_OCX_StopPlatformPlayBack:" + result);
			
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopPlatformPlayBack(wnd);");
		}
		else if (recordMethod  == 1) 
		{
			// 停止前端设备录像回放
			result = ocx.IVS_OCX_StopPUPlayBack(wnd);
			
			$("#resultcode").val("IVS_OCX_StopPUPlayBack:" + result);
			
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopPUPlayBack(wnd);");
		}
		
		showCode("} ");
	}
}

/**
 * 开启本地录像文件回放
 */
function startLocalPlayBack()
{
	if (ocx) 
	{
		ocx.IVS_OCX_StopAllRealPlay();
		
		var wnd = $("#wndplayback").val();
		if (wnd == "")
		{
			alert(langs[lang]["inputWndFirst"]);
			return;
		}
		
		var videofile = $("#videofile").val();
		if (videofile == "")
		{
			alert(langs[lang]["selectVideoFileFirst"]);
			return;
		}
		else
		{
			var fileNameList = videofile.split("\\");
			var i = 0;
			videofile ="";
			for ( i = 0; i < fileNameList.length - 1 ; i++)
			{
				videofile = videofile + fileNameList[i] + "\\\\";
			}
			 
			videofile = videofile + fileNameList[i];
		}
		 
		var playbackParam = "";
		playbackParam += "<Content>";
		playbackParam += "    <PlaybackParam>";
		playbackParam += "        <StartTime></StartTime>";   // 本地录像回放时，不需要设置开始时间
		playbackParam += "        <EndTime></EndTime>";       // 本地录像回放时，不需要设置结束时间
		playbackParam += "        <Speed>1.0</Speed>";        // 正常速度播放文件
		playbackParam += "        <pPWD></pPWD>";             // 如果录像已加密，设置录像密码
		playbackParam += "    </PlaybackParam>";
		playbackParam += "</Content>";
		
		var result = ocx.IVS_OCX_StartLocalPlayBack(wnd, videofile, playbackParam);
		$("#resultcode").val("IVS_OCX_StartLocalPlayBack:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = $('#wndplayback').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var videofile = $('#videofile').val();");
		
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var playbackParam = '&lt;Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;PlaybackParam&gt';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;StartTime&gt;&lt;/StartTime&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;EndTime&gt;&lt;/EndTime&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;Speed&gt;1&lt;/Speed&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;pPWD&gt;&lt;/pPWD&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;/PlaybackParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;playbackParam = playbackParam +  '&lt;/Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartLocalPlayBack(wnd, videofile, playbackParam);");
		showCode("} ");
	}
}

/**
 * 停止本地录像回放
 */
function stopLocalPlayBack()
{
	if (ocx) 
	{
		var wnd = $("#wndplayback").val();
		
		var result = ocx.IVS_OCX_StopLocalPlayBack(wnd);
		
		$("#resultcode").val("IVS_OCX_StopLocalPlayBack:" + result);

		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = $('#wnd').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopLocalPlayBack(wnd);");
		showCode("} ");
	}
}

/**
 * 暂停录像播放
 */
function pause()
{
	if (ocx) 
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		
		var result = ocx.IVS_OCX_PlayBackPause(wnd);
		
		$("#resultcode").val("IVS_OCX_PlayBackPause:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_PlayBackPause(wnd);");
		showCode("} ");
	}
}

/**
 * 恢复录像播放
 */
function resume()
{
	if (ocx) 
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		
		var result = ocx.IVS_OCX_PlayBackResume(wnd);

		$("#resultcode").val("IVS_OCX_PlayBackResume:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_PlayBackResume(wnd);");
		showCode("} ");
	}
}

/**
 * 录像回放正向单帧播放
 */
function frameStepForward()
{
	if (ocx) 
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		
		var result = ocx.IVS_OCX_PlaybackFrameStepForward(wnd);
		
		$("#resultcode").val("IVS_OCX_PlaybackFrameStepForward:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_PlaybackFrameStepForward(wnd);");
		showCode("} ");
	}
}

/**
 * 录像回放反向单帧播放
 */
function frameStepBackward()
{
	if (ocx) 
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		
		var result = ocx.IVS_OCX_PlaybackFrameStepBackward(wnd);
		
		$("#resultcode").val("IVS_OCX_PlaybackFrameStepBackward:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_PlaybackFrameStepBackward(wnd);");
		showCode("} ");
	}
}

/**
 * 设置录像回放速率
 */
function setSpeed()
{
	if (ocx) 
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		var speed = $("#speed").val();
		
		var result = ocx.IVS_OCX_SetPlayBackSpeed(wnd, speed);
		
		$("#resultcode").val("IVS_OCX_SetPlayBackSpeed:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var speed = $('#speed').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_SetPlayBackSpeed(wnd, speed);");
		showCode("} ");
	}
}

/**
 * 设置录像回放时，跳转到当前录像的指定时间点
 * 说明：假设录像全长100s，如果输入40，则从跳转到录像的第40秒播放录像。如果输入110，则停止录像播放
 */
function setTime()
{
	if (ocx) 
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		var time = $("#time").val();
		if (time == "")
		{
			alert(langs[lang]["inputTimeFirst"]);
			return;
		} 
		
		var result = ocx.IVS_OCX_SetPlayBackTime(wnd, time);
		
		$("#resultcode").val("IVS_OCX_SetPlayBackTime:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ "); 
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var wnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var time = $('#time').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_SetPlayBackTime(wnd, time);");
		showCode("} ");
	}
}


/**
 * 获取回放时间
 */
function getPlayBackTime()
{
	if (ocx) 
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		
		var resultXml = ocx.IVS_OCX_GetPlayBackTime(wnd);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			$("#resultcode").val("IVS_OCX_GetPlayBackTime:" + result);
			var playBackTime = xmlDoc.find("PlayBackTime").text();
			var startTime = xmlDoc.find("StartTime").text();
			var endTime = xmlDoc.find("EndTime").text();
			$("#playBackTime1").val("PlayBackTime :" + playBackTime + " StartTime :" + startTime + " EndTime :" + endTime); 
		}
	}
}

/**
 * 获取回放状态
 */
function getPlayBackStatus()
{
	if (ocx)
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		
		var resultXml = ocx.IVS_OCX_GetPlayBackStatus(wnd);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			$("#resultcode").val("IVS_OCX_GetPlayBackStatus:" + result);
			var playBackStatus = xmlDoc.find("Status").text();
			$("#playBackStatus").val(playBackStatus); 
		}
	}
}

/**
 * 获取回放状态
 */
function getPlayBackSpeed()
{
	if (ocx)
	{
		var wnd = ocx.IVS_OCX_GetSelectWnd();
		
		var resultXml = ocx.IVS_OCX_GetPlayBackSpeed(wnd);
		
		var xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			$("#resultcode").val("IVS_OCX_GetPlayBackSpeed:" + result);
			var playBackSpeed = xmlDoc.find("Speed").text();
			$("#playBackSpeed").val(playBackSpeed); 
		}
	}
}

/**
 * 添加书签
 */
function addBookmark()
{
	if (ocx)
	{
		var pNVRCode = "ec7bac2727c548ce8c0d0caf25a43519";
		var pCameraCode = "01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519";
		var pBookmarkName = "testBookmarkName";
		var pBookmarkTime = "20130912023719";
		 
		var result = ocx.IVS_OCX_AddBookmark(pNVRCode, pCameraCode, pBookmarkName, pBookmarkTime);
		
		alert(result);
	}
}

/**
 * 删除书签
 */
function deleteBookmark()
{
	if (ocx)
	{
		var nvrCode = "ec7bac2727c548ce8c0d0caf25a43519";
		var cameraCode = "01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519";
		var bookmarkID = "6";
		 
		var result = ocx.IVS_OCX_DeleteBookmark(nvrCode, cameraCode, bookmarkID);
		
		alert(result);
	}
}

/**
 * 删除书签
 */
function modifyBookmark()
{
	if (ocx)
	{
		var nvrCode = "ec7bac2727c548ce8c0d0caf25a43519";
		var cameraCode = "01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519";
		var bookmarkID = "6";
		var bookmarkName = "testBookmarkNameChange";
		
		var result = ocx.IVS_OCX_ModifyBookmark(nvrCode, cameraCode, bookmarkID, bookmarkName);
		
		alert(result);
	}
}


/**
 * 查询录像书签
 */
function getBookmarkList()
{
	if (ocx)
	{
		var cameraCode = "01398830000000000101#ec7bac2727c548ce8c0d0caf25a43519";
		var bookmarkName = "test";
		
		var fromTime = "20130912000719";
		var toTime = "20130912024719";
		var xmlDoc = $.parseXML(ocx.IVS_OCX_GetUserID());
		xmlDoc = $(xmlDoc);
		var operatorID = xmlDoc.find("UserID").text();
		 
		var queryXmlStr = "<Content>";
		queryXmlStr += "    <PageInfo>";
		queryXmlStr += "        <FromIndex>1</FromIndex>";
		queryXmlStr += "        <ToIndex>1000</ToIndex>";
		queryXmlStr += "        <QueryCond>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>CameraCode</Field>";
		queryXmlStr += "		        <Value> "+ cameraCode+ "</Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>BookmarkName</Field>";
		queryXmlStr += "		        <Value>" + bookmarkName + "</Value>";
		queryXmlStr += "		        <QueryType>INEXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>FromTime</Field>";
		queryXmlStr += "		        <Value> " + fromTime + " </Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>ToTime</Field>";
		queryXmlStr += "		        <Value> " + toTime + " </Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "	        <QueryField>";
		queryXmlStr += "		        <Field>OperatorID</Field>";
		queryXmlStr += "		        <Value> " + operatorID + " </Value>";
		queryXmlStr += "		        <QueryType>EXACT</QueryType>";
		queryXmlStr += "	        </QueryField>";
		queryXmlStr += "        </QueryCond>";
		queryXmlStr += "    </PageInfo>";
		queryXmlStr += "</Content>";
		
		var resultXml = ocx.IVS_OCX_GetBookmarkList(queryXmlStr);
		
		xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		
		if (result == 0)
		{
			alert(resultXml);
		}
	}
}

/**
 * 获取录像列表使用IVS_OCX_GetRecordList
 */
function getRecordList1()
{
	if (ocx)
	{
		var fromTime = $("#fromtime").val();
		var toTime = $("#totime").val();
		var recordMethod = $("#recordmethod").val();
		var cameraCode = $("#cameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		 
		var recordListXml = ocx.IVS_OCX_GetRecordList(cameraCode,recordMethod,fromTime,toTime,1,10);
		 
		xmlDoc = $.parseXML(recordListXml);
		xmlDoc = $(xmlDoc);
		var result = xmlDoc.find("ResultCode").text();
		$("#resultcode").val("IVS_OCX_GetRecordList:" + result);
		
		if (result == 0)
		{
			var recordDataInfoNode = xmlDoc.find("RecordDataInfo");
			cameraCode = xmlDoc.find("CameraCode").text();
		    $("#recordlist").empty();
			for (i = 0; i < recordDataInfoNode.length; i++)
			{
				recordMethod = xmlDoc.find("RecordDataInfo:eq(" + i + ")").find("RecordMethod").text();
				var startTime = xmlDoc.find("RecordDataInfo:eq(" + i + ")").find("StartTime").text();
				var endTime = xmlDoc.find("RecordDataInfo:eq(" + i + ")").find("EndTime").text();
				
				var htmlStr = "<div>" + 
						"<input type='text' id='cameraCode" + i + "' value='"+ cameraCode+ "' style='width: 120px;' readonly='readonly'>" +
						"<span>|</span>" +  
						"<input type='text' id='startTime" + i + "' value='"+ startTime+ "' style='width: 100px;' readonly='readonly'>" +
						"<span>|</span>" + 
						"<input type='text' id='endTime" + i + "' value='"+ endTime+ "' style='width: 100px;' readonly='readonly'>" +
						"<span>|</span>" + 
						"<input type='text' id='recordMethod" + i + "' value='"+ (recordMethod == 0? langs[lang]["recordMethod0"] : langs[lang]["recordMethod1"])+ "' style='width: 70px;' readonly='readonly'>" +
						"<span>|</span>" + 
						"<input type='text' id='wnd" + i +"' style='width: 70px;'>" +
						"<span>|</span>" +
						"<input type='button' value=" + langs[lang]["startPlayBack"] +" onclick='startPlayBack("+ i +")'>" + 
						"<input type='button' value=" + langs[lang]["stopPlayBack"] +" onclick='stopPlayBack("+ i +")'>" + 
						"</div>";
				
				$("#recordlist").append(htmlStr);
			}
		}
	}
}


