/**
 * 开始语音对讲
 */
function startVoiceTalkback()
{
	if (ocx)
	{
		var cameraCode = $("#talkcameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		var talkParam = $("#talkparam").val();
		
		if ( talkParam == "")
		{
			talkParam += "<Content>";
			talkParam += "    <TalkbackParam>";
			talkParam += "        <ProtocolType>1</ProtocolType>"; // 协议类型: "1"为UDP
			talkParam += "        <DirectFirst>1</DirectFirst>";   // 是否直连: "0"为不直连  "1"为直连
			talkParam += "    </TalkbackParam>";
			talkParam += "</Content>";
			
			 $("#talkparam").val(talkParam);
		}
		var resultXml = ocx.IVS_OCX_StartVoiceTalkback(talkParam , cameraCode);
		
		xmlDoc = $.parseXML(resultXml);
		xmlDoc = $(xmlDoc);
		var result = xmlDoc.find("ResultCode").text();
		
		$("#resultcode").val("IVS_OCX_StartVoiceTalkback:" + result);
		
		var handle = xmlDoc.find("TalkbackHandle").text();
		$("#handle").val(handle);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#talkcameracode').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var talkParam = $('#talkparam').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;if (talkParam == '')");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;{");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;talkParam += '&lt;Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;talkParam += '&lt;TalkbackParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;talkParam += '&lt;ProtocolType&gt;1&lt;/ProtocolType&gt;'; //1-UDP");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;talkParam += '&lt;DirectFirst&gt;1&lt;/DirectFirst&gt;';  //0-No 1-Yes");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;talkParam += '&lt;/TalkbackParam&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;talkParam += '&lt;/Content&gt;';");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$('#talkparam').val(talkParam);");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;}");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var resultXml = ocx.IVS_OCX_StartVoiceTalkback(talkParam , cameraCode);");
		showCode("} ");
	}
}


/**
 * 停止语音对讲
 */
function stopVoiceTalkback()
{
	if (ocx)
	{
		var handle = $("#handle").val();
		if (handle == "")
		{
			alert(langs[lang]["noTalkBackTask"]);
			return;
		}
		var result = ocx.IVS_OCX_StopVoiceTalkback(handle);
		
		$("#resultcode").val("IVS_OCX_StopVoiceTalkback:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var handle = $('#handle').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopVoiceTalkback(handle);");
		showCode("} ");
	}
}

/**
 * 添加需要广播的设备
 */
function addBroadcastDevice()
{
	if (ocx)
	{
		var cameraCode = $("#broadcameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		var result = ocx.IVS_OCX_AddBroadcastDevice(cameraCode);
		
		$("#resultcode").val("IVS_OCX_AddBroadcastDevice:" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#cameraCode');");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_DeleteBroadcastDevice(cameraCode);");
		showCode("} ");
	}
}

/**
 * 删除需要广播的设备
 */
function deleteBroadcastDevice()
{
	if (ocx)
	{
		var cameraCode = $("#broadcameracode").val();
		if (cameraCode == undefined || cameraCode == "")
		{
			alert(langs[lang]["inputCameraFirst"]);
			return;
		}
		var result = ocx.IVS_OCX_DeleteBroadcastDevice(cameraCode);
		$("#resultcode").val("IVS_OCX_DeleteBroadcastDevice:" +result);
		
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraCode = $('#cameraCode');");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_DeleteBroadcastDevice(cameraCode);");
		showCode("} ");
	}
}

/**
 * 显示全部需要广播的设备列表
 */
function getBroadcastDevice()
{
	if (ocx)
	{
		var cameraListXml = ocx.IVS_OCX_GetBroadcastCameraList();
		xmlDoc = $.parseXML(cameraListXml);
		xmlDoc = $(xmlDoc);
		
		var result = xmlDoc.find("ResultCode").text();
		if (result == 0)
		{
			var cameraCodeNode = xmlDoc.find("CameraCode");
			
		    $("#cameracodelist").empty();
			for (i = 0; i < cameraCodeNode.length; i++)
			{
				var cameraCode = xmlDoc.find("CameraCode:eq(" + i + ")").text();
				var htmlStr = "<span>" + cameraCode + "</span><br>";
				$("#cameracodelist").append(htmlStr);
			}
		}
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cameraListXml = ocx.IVS_OCX_GetBroadcastCameraList();");
		showCode("} ");
	}
}

/**
 * 开启语音广播
 */
function startRealBroadcast()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_StartRealBroadcast();
		
		$("#resultcode").val("IVS_OCX_StartRealBroadcast:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartRealBroadcast();");
		showCode("} ");
	}
}

/**
 * 停止语音广播
 */
function stopRealBroadcast()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_StopRealBroadcast();
		$("#resultcode").val("IVS_OCX_StopRealBroadcast:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopRealBroadcast();");
		showCode("} ");
	}
}

/**
 * 开启音频文件广播
 */
function startFileBroadcast()
{
	if (ocx)
	{
		var voiceFile = $("#voicefile").val();
		if (voiceFile == "")
		{
			alert(langs[lang]["selectVoiceFileFirst"]);
			return;
		}
		var cycleType = $("#cycletype").val();
		
		var result = ocx.IVS_OCX_StartFileBroadcast(voiceFile, cycleType);
		
		$("#resultcode").val("IVS_OCX_StartFileBroadcast:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var voiceFile = $('#voicefile').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var cycleType = $('#cycletype').val();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StartFileBroadcast(voiceFile, cycleType);");
		showCode("} ");
	}
}

/**
 * 停止音频文件广播
 */
function stopFileBroadcast()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_StopFileBroadcast();
		
		$("#resultcode").val("IVS_OCX_StopFileBroadcast:" + result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_StopFileBroadcast();");
		showCode("} ");
	}
}
