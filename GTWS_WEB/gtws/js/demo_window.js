/**
 * 设置视频播放时，视频画面是否拉伸用以填满播放窗格
 */
function setScale()
{
	if (ocx)
	{
		var scale = $("#scale").val();
		
		var result = ocx.IVS_OCX_SetDisplayScale(scale);
		
		$("#resultcode").val("IVS_OCX_SetDisplayScale:" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_SetDisplayScale(scale);");
		showCode("} ");
	}
}

/**
 * 设置OCX播放窗格中显示的工具栏按钮
 */
function setToolbar()
{
	if (ocx)
	{
		// 设置OCX控件的播放窗格显示工具条
		ocx.IVS_OCX_ShowToolbar(1);
		
		var toolSnapshot = ($("#tool_SNAPSHOT").attr("checked") == "checked"  ? $("#tool_SNAPSHOT").val() : 0x00000000);
		var toolRecord = ($("#tool_LOCAL_RECORD").attr("checked") == "checked"  ? $("#tool_LOCAL_RECORD").val() : 0x00000000);
		var toolBookmark = ($("#tool_BOOKMARK").attr("checked") == "checked"  ? $("#tool_BOOKMARK").val() : 0x00000000);
		var toolZoom = ($("#tool_ZOOM").attr("checked") == "checked"  ? $("#tool_ZOOM").val() : 0x00000000);
		var toolPlayRecord = ($("#tool_PLAY_RECORD").attr("checked") == "checked"  ? $("#tool_PLAY_RECORD").val() : 0x00000000);
		var toolPlaySound = ($("#tool_PLAY_SOUND").attr("checked") == "checked"  ? $("#tool_PLAY_SOUND").val() : 0x00000000);
		var toolTalkback = ($("#tool_TALKBACK").attr("checked") == "checked"  ? $("#tool_TALKBACK").val() : 0x00000000);
		var toolVoiceTvw = ($("#tool_VIDEO_TVW").attr("checked") == "checked"  ? $("#tool_VIDEO_TVW").val() : 0x00000000);
		var toolAlarmWin = ($("#tool_ALARM_WIN").attr("checked") == "checked"  ? $("#tool_ALARM_WIN").val() : 0x00000000);
		var toolPtz = ($("#tool_PTZ").attr("checked") == "checked"  ? $("#tool_PTZ").val() : 0x00000000);
		var toolIa = ($("#tool_IA").attr("checked") == "checked"  ? $("#tool_IA").val() : 0x00000000);
		var toolOpenMap = ($("#tool_OPEN_MAP").attr("checked") == "checked"  ? $("#tool_OPEN_MAP").val() : 0x00000000);
		var toolWindowMain = ($("#tool_WINDOW_MAIN").attr("checked") == "checked"  ? $("#tool_WINDOW_MAIN").val() : 0x00000000);
		var toolPlayQuality = ($("#tool_PLAY_QUALITY").attr("checked") == "checked"  ? $("#tool_PLAY_QUALITY").val() : 0x00000000);

		var ulToolbarFlag = toolSnapshot ^ toolRecord ^ toolBookmark ^ toolZoom ^ toolPlayRecord ^ toolPlaySound 
						^ toolTalkback ^ toolVoiceTvw ^ toolAlarmWin ^ toolPtz ^ toolIa ^ toolOpenMap ^ toolWindowMain ^ toolPlayQuality;
		
		// 设置工具条上显示哪些工具按钮
		var result = ocx.IVS_OCX_SetToolbar(ulToolbarFlag);
		
		ocx.IVS_OCX_RefreshWnd(2);
		
		$("#resultcode").val("IVS_OCX_SetToolbar:" +result);
		 
		//result = ocx.IVS_OCX_RefreshWnd(0);
		
		//result = ocx.IVS_OCX_SetToolbarButtonStatus(1, 8, 1);
		
		//result = ocx.IVS_OCX_ShowInstantReplayBar(1, 1);
		
		result = ocx.IVS_OCX_ShowTitlebar(1);
		
		result = ocx.IVS_OCX_SetDeviceName(1,"xxx摄像机");
		
		result = ocx.IVS_OCX_SetTitleBarStatus(1, 2, 1);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var result = ocx.IVS_OCX_SetToolbar(ulToolbarFlag);");
		showCode("} ");
	}
}

/**
 * 设置OCX控件全屏显示
 */
function setFullScreen()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_FullScreenDisplay();
		
		$("#resultcode").val("IVS_OCX_FullScreenDisplay:" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_FullScreenDisplay();");
		showCode("} ");
	}
}

/**
 * 设置OCX控件退出全屏幕显示，一般情况下使用"Esc"键退出全屏幕显示
 */
function setNormalScreen()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_NormalScreenDisplay();
		
		$("#resultcode").val("IVS_OCX_NormalScreenDisplay:" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_NormalScreenDisplay();");
		showCode("} ");
	}
}

/**
 * 设置OCX控件用于播放显示视频的窗格布局
 * @param wndNum 窗格数量
 */
function setLayout(wndNum)
{
	if (ocx)
	{
		var result = 0;
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		if (wndNum == 1)
		{
			result = ocx.IVS_OCX_SetWndLayout(11);  // "11" 代表仅一个窗格布局模式
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_SetWndLayout(11);");
		}
		else if (wndNum == 6)
		{
			result = ocx.IVS_OCX_SetWndLayout(63);  // "63" 代表1大5小布局模式
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_SetWndLayout(63);");
		}
		else if (wndNum == 9)
		{
			result = ocx.IVS_OCX_SetWndLayout(92);  // "92" 代表九宫格布局模式
			showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_SetWndLayout(92);");
		}
		showCode("} ");
		
		$("#resultcode").val("IVS_OCX_SetWndLayout:" + result);
	}
}

/**
 * 获取鼠标选中的播放窗格编号
 */
function getSelectedWnd()
{
	if (ocx)
	{
		var selectedWnd = ocx.IVS_OCX_GetSelectWnd();
		
		$("#selectwnd").val(selectedWnd);

		$("#resultcode").val("");
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var selectedWnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("} ");
	}
}

/**
 * 获取空闲的(没有播放视频的)播放窗格编号
 */
function getFreeWnd()
{
	if (ocx)
	{
		var freeWnd = ocx.IVS_OCX_GetFreeWnd();
		
		$("#freewnd").val(freeWnd);
		
		$("#resultcode").val("");
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var freeWnd = ocx.IVS_OCX_GetFreeWnd();");
		showCode("} "); 
	}
}

/**
 * 获取选中的播放窗格中，正在播放的视频对应的摄像机编码
 */
function getCameraByWnd()
{
	if (ocx)
	{
		var selectedWnd = ocx.IVS_OCX_GetSelectWnd();
		
		var camera = ocx.IVS_OCX_GetCameraByWnd(selectedWnd);
		
		$("#codeinwnd").val(camera);
		
		$("#resultcode").val("");
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var selectedWnd = ocx.IVS_OCX_GetSelectWnd();");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;var camera = ocx.IVS_OCX_GetCameraByWnd(selectedWnd);");
		showCode("} "); 
	}
}

function setWndType()
{
	if (ocx) 
	{
		var wnd = $("#setWndInput").val();
		var wndType = $("#wndTypeSelect").val();
		
		var result = ocx.IVS_OCX_SetWndType(wnd, wndType);
	}
}

function getWndType()
{
	if (ocx) 
	{
		var wnd = $("#getWndInput").val();
		var wndType = ocx.IVS_OCX_GetWndType(wnd);
		
		$("#wndTypeInput").val(wndType);
	}
}


function setWndDrag()
{
	if (ocx)
	{
		var wnd = $("#setWndDragInput").val();
		var enable = $("#setWndDragSelect").val();
		
		var result = ocx.IVS_OCX_SetWndDrag(wnd, enable);
	}
}

function enableExchangePane()
{
	if (ocx)
	{
		var enable = $("#exchangePaneSelect").val();
		
		var result = ocx.IVS_OCX_EnableExchangePane(enable);
	}
}


function getMouseWnd()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_GetMouseWnd();
		$("#getMouseWndInput").val(result);
	}
}

function getPaneWnd()
{
	if (ocx)
	{
		var wnd = $("#getMouseWndInput").val();
		var result = ocx.IVS_OCX_GetPaneWnd(wnd);
		$("#getPaneWndInput").val(result);
	}
}

function getDomainRoute()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_GetDomainRoute();
		$("#getDomainRouteInput").val(result);
	}
}

function getDeviceGroup()
{
	if (ocx)
	{
		var xmlDoc = $.parseXML(ocx.IVS_OCX_GetDomainRoute());
		xmlDoc = $(xmlDoc);
		var domainCode = xmlDoc.find("DomainCode").text();
		
		var reqXml = "<Content>";
		reqXml += "    <DomainCode>" + domainCode + "</DomainCode>";
		reqXml += "    <GroupID>0</GroupID>";  // 0为默认的根节点ID
		reqXml += "</Content>";
		var result = ocx.IVS_OCX_GetDeviceGroup(reqXml);
		$("#getDeviceGroupInput").val(result);
	}
}

 
function getNVRList()
{
	if (ocx)
	{
		var xmlDoc = $.parseXML(ocx.IVS_OCX_GetDomainRoute());
		xmlDoc = $(xmlDoc);
		var domainCode = xmlDoc.find("DomainCode").text();
		
		var reqXml = "<Content>";
		reqXml += "    <DomainCode>" + domainCode + "</DomainCode>";
		reqXml += "    <NVRType>0</NVRType>";  // 0为查询全部类型
		reqXml += "    <PageInfo>";
		reqXml += "        <FromIndex>1</FromIndex>";
		reqXml += "        <ToIndex>10</ToIndex>";
		reqXml += "    </PageInfo>";
		reqXml += "</Content>";
		var result = ocx.IVS_OCX_GetNVRList(reqXml);
		$("#getNVRListInput").val(result);
	}
}









