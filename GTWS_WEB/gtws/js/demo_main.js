var langs = new Array();
langs["zh-cn"] = new Array();
langs["en-us"] = new Array(); 
var lang = "";
if (navigator.language) 
{
	lang = navigator.language;
}
else 
{
	lang = navigator.browserLanguage;	
}
lang = lang.toLowerCase();
if (lang != "zh-cn")
{
	lang = "en-us";
}

/**
 * 页面初始化加载国际化内容
 */
function init()
{
	/**
	 * 菜单条
	 */
	$("#loginbtn").attr("value",langs[lang]["loginbtn"]);
	$("#windowbtn").attr("value",langs[lang]["windowbtn"]);
	$("#livebtn").attr("value",langs[lang]["livebtn"]);
	$("#videobtn").attr("value",langs[lang]["videobtn"]);
	$("#playbtn").attr("value",langs[lang]["playbtn"]);
	$("#voicebtn").attr("value",langs[lang]["voicebtn"]);
	$("#downloadbtn").attr("value",langs[lang]["downloadbtn"]);
	$("#result").text(langs[lang]["result"]);
	$("#resultcode").attr("title",langs[lang]["resultcode"]);
	$("#demoCode").text(langs[lang]["demoCode"]);

	/**
	 * 登录注销页面
	 */
	$("#initBtn").attr("value",langs[lang]["initBtn"]);
	$("#versionBtn").attr("value",langs[lang]["versionBtn"]);
	$("#releaseBtn").attr("value",langs[lang]["releaseBtn"]);
	$("#loginBtn").attr("value",langs[lang]["loginBtn"]);
	$("#logoutBtn").attr("value",langs[lang]["logoutBtn"]);
	$("#userNameLbl").text(langs[lang]["userNameLbl"]);
	$("#userPwdLbl").text(langs[lang]["userPwdLbl"]);
	$("#serverLbl").text(langs[lang]["serverLbl"]);
	$("#portLbl").text(langs[lang]["portLbl"]);
	$("#changepwdBtn").attr("value",langs[lang]["changepwdBtn"]);
	$("#newPwdLbl").text(langs[lang]["newPwdLbl"]);
	$("#oldPwdLbl").text(langs[lang]["oldPwdLbl"]);

	/**
	 * 设置窗格页面
	 */
	$("#setScaleBtn").attr("value",langs[lang]["setScaleBtn"]);
	$("#fullScreenBtn").attr("value",langs[lang]["fullScreenBtn"]);
	$("#exitFullScreenBtn").attr("value",langs[lang]["exitFullScreenBtn"]);
	$("#displayScale").text(langs[lang]["displayScale"]);
	$("#stretchScale").text(langs[lang]["stretchScale"]);
	$("#originalScale").text(langs[lang]["originalScale"]);
	$("#setToolbar").text(langs[lang]["setToolbar"]);
	$("#setToolbarBtn").attr("value",langs[lang]["setToolbarBtn"]);
	$("#setLayoutOneBtn").attr("value",langs[lang]["setLayoutOneBtn"]);
	$("#setLayoutSixBtn").attr("value",langs[lang]["setLayoutSixBtn"]);
	$("#setLayoutNineBtn").attr("value",langs[lang]["setLayoutNineBtn"]);
	$("#setLayout").text(langs[lang]["setLayout"]);
	$("#tool01").text(langs[lang]["tool01"]);
	$("#tool02").text(langs[lang]["tool02"]);
	$("#tool03").text(langs[lang]["tool03"]);
	$("#tool04").text(langs[lang]["tool04"]);
	$("#tool05").text(langs[lang]["tool05"]);
	$("#tool06").text(langs[lang]["tool06"]);
	$("#tool07").text(langs[lang]["tool07"]);
	$("#tool08").text(langs[lang]["tool08"]);
	$("#tool09").text(langs[lang]["tool09"]);
	$("#tool10").text(langs[lang]["tool10"]);
	$("#tool11").text(langs[lang]["tool11"]);
	$("#tool12").text(langs[lang]["tool12"]);
	$("#tool13").text(langs[lang]["tool13"]);
	$("#tool14").text(langs[lang]["tool14"]); 
	$("#activeWndCode").text(langs[lang]["activeWndCode"]);
	$("#activeWndCodeBtn").attr("value",langs[lang]["activeWndCodeBtn"]);
	$("#freeWnd").text(langs[lang]["freeWnd"]);
	$("#freeWndBtn").attr("value",langs[lang]["freeWndBtn"]);
	$("#selectedWnd").text(langs[lang]["selectedWnd"]);
	$("#selectedWndBtn").attr("value",langs[lang]["selectedWndBtn"]);

	/**
	* 实况与云台页面
	*/
	$("#cameraListLbl").text(langs[lang]["cameraListLbl"]);
	$("#getCameraListBtn").attr("value",langs[lang]["getCameraListBtn"]);
	$("#liveParam").text(langs[lang]["liveParam"]);
	$("#protocolType").attr("title",langs[lang]["protocolType"]);
	$("#streamType").attr("title",langs[lang]["streamType"]);
	$("#streamType0").text(langs[lang]["streamType0"]);
	$("#streamType1").text(langs[lang]["streamType1"]);
	$("#streamType2").text(langs[lang]["streamType2"]);
	$("#streamType3").text(langs[lang]["streamType3"]);
	$("#direstFirst").attr("title",langs[lang]["direstFirst"]);
	$("#direstFirst0").text(langs[lang]["direstFirst0"]);
	$("#direstFirst1").text(langs[lang]["direstFirst1"]);
	$("#multi").attr("title",langs[lang]["multi"]);
	$("#multi0").text(langs[lang]["multi0"]);
	$("#multi1").text(langs[lang]["multi1"]);
	$("#startLiveBtn").attr("value",langs[lang]["startLiveBtn"]);
	$("#liveWndLbl").text(langs[lang]["liveWndLbl"]);
	$("#stopLiveBtn").attr("value",langs[lang]["stopLiveBtn"]);
	$("#lock").attr("value",langs[lang]["lock"]);
	$("#unlock").attr("value",langs[lang]["unlock"]);
	$("#zoomIn").attr("value",langs[lang]["zoomIn"]);
	$("#zoomOut").attr("value",langs[lang]["zoomOut"]);
	$("#presetNameLbl").text(langs[lang]["presetNameLbl"]);
	$("#presetIndexLbl").text(langs[lang]["presetIndexLbl"]);
	$("#addPresetBtn").attr("value",langs[lang]["addPresetBtn"]);
	$("#getPresetListBtn").attr("value",langs[lang]["getPresetListBtn"]);
	$("#delPresetBtn").attr("value",langs[lang]["delPresetBtn"]);
	$("#loadPresetBtn").attr("value",langs[lang]["loadPresetBtn"]);


	/**
	* page video
	*/
	$("#platVideoCode").text(langs[lang]["platVideoCode"]);
	$("#platVideoTime").text(langs[lang]["platVideoTime"]);
	$("#platrecordtime").attr("title",langs[lang]["platrecordtime"]);
	$("#puVideoCode").text(langs[lang]["puVideoCode"]);
	$("#puVideoTime").text(langs[lang]["puVideoTime"]);
	$("#purecordtime").attr("title",langs[lang]["purecordtime"]);
	$("#startPlatRecordBtn").attr("value",langs[lang]["startPlatRecordBtn"]);
	$("#stoptPlatRecordBtn").attr("value",langs[lang]["stoptPlatRecordBtn"]);
	$("#startPuRecordBtn").attr("value",langs[lang]["startPuRecordBtn"]);
	$("#stoptPuRecordBtn").attr("value",langs[lang]["stoptPuRecordBtn"]);
	$("#videoParamLbl").text(langs[lang]["videoParamLbl"]);
	$("#videoFileName").text(langs[lang]["videoFileName"]);
	$("#localRecordWndLbl").text(langs[lang]["localRecordWndLbl"]);
	$("#startLocalRecordBtn").attr("value",langs[lang]["startLocalRecordBtn"]);
	$("#stopLocalRecordBtn").attr("value",langs[lang]["stopLocalRecordBtn"]);


	/**
	* 录像回放页面
	*/
	/**
	 * 常用设置方法
	 * 修改对象value
	 * $("#").attr("value",
	 * 
	 * 修改对象title
	 * $("#").attr("title",
	 * 
	 * 修改对象text
	 * $("#").text(
	 */
	$("#codeTh").text(langs[lang]["codeTh"]);
	$("#startTimeTh").text(langs[lang]["startTimeTh"]);
	$("#stopTimeTh").text(langs[lang]["stopTimeTh"]);
	$("#typeTh").text(langs[lang]["typeTh"]);
	$("#wndTh").text(langs[lang]["wndTh"]);
	$("#optTh").text(langs[lang]["optTh"]);
	$("#fromtime").attr("title",langs[lang]["fromtime"]);
	$("#fromTimeLbl").text(langs[lang]["fromTimeLbl"]);
	$("#totime").attr("title",langs[lang]["totime"]);
	$("#toTimeLbl").text(langs[lang]["toTimeLbl"]);
	$("#queryBtn").attr("value",langs[lang]["queryBtn"]);
	$("#playBackCode").text(langs[lang]["playBackCode"]);
	$("#recordMethodLbl").text(langs[lang]["recordMethodLbl"]);
	$("#recordMethod0").text(langs[lang]["recordMethod0"]);
	$("#recordMethod1").text(langs[lang]["recordMethod1"]);
	$("#playBackFile").text(langs[lang]["playBackFile"]);
	$("#playBackWnd").text(langs[lang]["playBackWnd"]);
	$("#startLocalPlayBack").attr("value",langs[lang]["startLocalPlayBack"]);
	$("#stopLocalPlayBack").attr("value",langs[lang]["stopLocalPlayBack"]);
	$("#playBackPause").attr("value",langs[lang]["playBackPause"]);
	$("#playBackResume").attr("value",langs[lang]["playBackResume"]);
	$("#frameStepForward").attr("value",langs[lang]["frameStepForward"]);
	$("#frameStepBackward").attr("value",langs[lang]["frameStepBackward"]);
	$("#setPlayBackSpeed").attr("value",langs[lang]["setPlayBackSpeed"]);
	$("#time").attr("title",langs[lang]["time"]);
	$("#playBackTime").attr("value",langs[lang]["playBackTime"]);
	$("#speed00").text(langs[lang]["speed00"]);
	$("#speed01").text(langs[lang]["speed01"]);
	$("#speed02").text(langs[lang]["speed02"]);
	$("#speed03").text(langs[lang]["speed03"]);
	$("#speed04").text(langs[lang]["speed04"]);
	$("#speed05").text(langs[lang]["speed05"]);
	$("#speed06").text(langs[lang]["speed06"]);
	$("#speed07").text(langs[lang]["speed07"]);
	$("#speed08").text(langs[lang]["speed08"]);
	$("#speed09").text(langs[lang]["speed09"]);
	$("#speed10").text(langs[lang]["speed10"]);
	$("#speed11").text(langs[lang]["speed11"]);
	$("#speed12").text(langs[lang]["speed12"]);
	$("#speed13").text(langs[lang]["speed13"]);
	$("#speed14").text(langs[lang]["speed14"]);
	$("#speed15").text(langs[lang]["speed15"]);
	$("#speed16").text(langs[lang]["speed16"]); 

	/**
	* 语音页面
	*/
	$("#talkParamLbl").text(langs[lang]["talkParamLbl"]);
	$("#talkCode").text(langs[lang]["talkCode"]);
	$("#talkHandle").text(langs[lang]["talkHandle"]);
	$("#startTalkBtn").attr("value",langs[lang]["startTalkBtn"]);
	$("#stopTalkBtn").attr("value",langs[lang]["stopTalkBtn"]);
	$("#broadcastCode").text(langs[lang]["broadcastCode"]);
	$("#addBroadcastDeviceBtn").attr("value",langs[lang]["addBroadcastDeviceBtn"]);
	$("#deleteBroadcastDeviceBtn").attr("value",langs[lang]["deleteBroadcastDeviceBtn"]);
	$("#getBroadcastDeviceBtn").attr("value",langs[lang]["getBroadcastDeviceBtn"]);
	$("#startRealBroadcastBtn").attr("value",langs[lang]["startRealBroadcastBtn"]);
	$("#stopRealBroadcastBtn").attr("value",langs[lang]["stopRealBroadcastBtn"]);
	$("#startFileBroadcastBtn").attr("value",langs[lang]["startFileBroadcastBtn"]);
	$("#stopFileBroadcastBtn").attr("value",langs[lang]["stopFileBroadcastBtn"]);
	$("#voiceFileLbl").text(langs[lang]["voiceFileLbl"]);
	$("#cycleTypeLbl").text(langs[lang]["cycleTypeLbl"]);
	$("#cycletype0").text(langs[lang]["cycletype0"]);
	$("#cycletype1").text(langs[lang]["cycletype1"]);

	/**
	 * 下载和截图页面
	 */
	$("#picBtn").attr("value",langs[lang]["picBtn"]);
	$("#picFormat").text(langs[lang]["picFormat"]);
	$("#picWnd").text(langs[lang]["picWnd"]);
	$("#picPath").text(langs[lang]["picPath"]);
	$("#getDonwloadinfoBtn").attr("value",langs[lang]["getDonwloadinfoBtn"]);
	$("#downloadhandleLbl").text(langs[lang]["downloadhandleLbl"]);
	$("#downloadinfoLbl").text(langs[lang]["downloadinfoLbl"]);
	$("#pauseBtn").attr("value",langs[lang]["pauseBtn"]);
	$("#resumeBtn").attr("value",langs[lang]["resumeBtn"]);
	$("#stopPlatDownBtn").attr("value",langs[lang]["stopPlatDownBtn"]);
	$("#stopPUDownBtn").attr("value",langs[lang]["stopPUDownBtn"]);
	$("#startPlatDownBtn").attr("value",langs[lang]["startPlatDownBtn"]);
	$("#startPUDownBtn").attr("value",langs[lang]["startPUDownBtn"]);
	$("#endtime").attr("title",langs[lang]["endtime"]);
	$("#endtimeLbl").text(langs[lang]["endtimeLbl"]);
	$("#starttime").attr("title",langs[lang]["starttime"]);
	$("#starttimeLbl").text(langs[lang]["starttimeLbl"]);
	$("#downloadSavePath").text(langs[lang]["downloadSavePath"]);
	$("#downloadCode").text(langs[lang]["downloadCode"]);
	$("#downloadParamLbl").text(langs[lang]["downloadParamLbl"]);
}

/**
 * 页面切换控制
 * @param pageNo
 */
function changePage(pageNo)
{
	if (pageNo == 1)
	{
		document.getElementById("login").style.display = "";
		document.getElementById("window").style.display = "none";
		document.getElementById("live").style.display = "none";
		document.getElementById("video").style.display = "none";
		document.getElementById("play").style.display = "none";
		document.getElementById("voice").style.display = "none";
		document.getElementById("download").style.display = "none";
	}
	else if (pageNo == 2)
	{
		document.getElementById("login").style.display = "none";
		document.getElementById("window").style.display = "";
		document.getElementById("live").style.display = "none";
		document.getElementById("video").style.display = "none";
		document.getElementById("play").style.display = "none";
		document.getElementById("voice").style.display = "none";
		document.getElementById("download").style.display = "none";
	}
	else if (pageNo == 3)
	{
		document.getElementById("login").style.display = "none";
		document.getElementById("window").style.display = "none";
		document.getElementById("live").style.display = "";
		document.getElementById("video").style.display = "none";
		document.getElementById("play").style.display = "none";
		document.getElementById("voice").style.display = "none";
		document.getElementById("download").style.display = "none";
	}
	else if (pageNo == 4)
	{
		document.getElementById("login").style.display = "none";
		document.getElementById("window").style.display = "none";
		document.getElementById("live").style.display = "none";
		document.getElementById("video").style.display = "";
		document.getElementById("play").style.display = "none";
		document.getElementById("voice").style.display = "none";
		document.getElementById("download").style.display = "none";
	}
	else if (pageNo == 5)
	{
		document.getElementById("login").style.display = "none";
		document.getElementById("window").style.display = "none";
		document.getElementById("live").style.display = "none";
		document.getElementById("video").style.display = "none";
		document.getElementById("play").style.display = "";
		document.getElementById("voice").style.display = "none";
		document.getElementById("download").style.display = "none";
	}
	else if (pageNo == 6)
	{
		document.getElementById("login").style.display = "none";
		document.getElementById("window").style.display = "none";
		document.getElementById("live").style.display = "none";
		document.getElementById("video").style.display = "none";
		document.getElementById("play").style.display = "none";
		document.getElementById("voice").style.display = "";
		document.getElementById("download").style.display = "none";
	}
	else if (pageNo == 7)
	{
		document.getElementById("login").style.display = "none";
		document.getElementById("window").style.display = "none";
		document.getElementById("live").style.display = "none";
		document.getElementById("video").style.display = "none";
		document.getElementById("play").style.display = "none";
		document.getElementById("voice").style.display = "none";
		document.getElementById("download").style.display = "";
	}
}

/**
 * 代码窗口折叠
 */
function setCodeDivHeight() 
{
	if ( $("#code").css("height") == "150px")
	{
		$("#code").css( {
			"height":"27px"
		});
		$("#pages").css({
			"bottom":"60px"
		});
	}
	else 
	{
		$("#code").css( {
			"height":"150px"
		});
		$("#pages").css({
			"bottom":"303px"
		});
	}
	
}

/**
 * 调用ocx加载文件路径选择框
 * @param objId 页面input对象的id
 */
function getFilePath(objId)
{
	if (ocx)
	{
		var path = ocx.IVS_OCX_PopupFloderDialog();

		$("#" + objId).val(path);
	}
}

/**
 * 将用户输入的本地时间转换成UTC时间提交给平台
 * Convert LocalTime to UTC 
 * @param loacltime 时间格式为：yyyyMMddHHmmss
 * @returns
 */
/*function toUTCString(loacltime)
{
	var localDate = new Date(loacltime.substr(0, 4), loacltime.substr(4, 2), loacltime.substr(6, 2), loacltime.substr(8, 2), loacltime.substr(10, 2), loacltime
			.substr(12, 2));
	var utcString = localDate.getUTCFullYear() +""+ 
					(localDate.getUTCMonth()< 10?  "0"+ localDate.getUTCMonth() : "" + localDate.getUTCMonth()) +
					(localDate.getUTCDate()< 10?   "0"+ localDate.getUTCDate()  : "" + localDate.getUTCDate())  +
					(localDate.getUTCHours()< 10?  "0"+ localDate.getUTCHours() : "" + localDate.getUTCHours()) +
					(localDate.getUTCMinutes()< 10?"0"+ localDate.getUTCMinutes() : ""+localDate.getUTCMinutes())+
					(localDate.getUTCSeconds()< 10?"0"+ localDate.getUTCSeconds() : ""+localDate.getUTCSeconds());
	return utcString;
}*/

/**
 * 将UTC时间转换成本地时间转换成显示给用户
 * Convert UTC to LocalTime
 * @param utctime 时间格式为：yyyyMMddHHmmss
 * @returns
 */
/*function toLocalDateString(utctime)
{
	var utcDate = new Date(utctime.substr(0, 4), utctime.substr(4, 2), utctime.substr(6, 2), utctime.substr(8, 2), utctime.substr(10, 2), utctime.substr(12, 2));

	//var localOffset = utcDate.getTimezoneOffset()*60000; 
	var localOffset = utcDate.getTimezoneOffset()/60; 
	
	var loaclDate = new Date(utcDate.getTime() - localOffset);
	var loaclString = loaclDate.getFullYear()  +""+  
	                  (loaclDate.getMonth()< 10? "0" +  loaclDate.getMonth()     : ""+ loaclDate.getMonth())+  
	                  (loaclDate.getDate()< 10? "0" +   loaclDate.getDate()      : ""+ loaclDate.getDate())+ 
	                  (loaclDate.getHours()< 10? "0" +  loaclDate.getHours()     : ""+ loaclDate.getHours())+
	                  (loaclDate.getMinutes()< 10? "0" + loaclDate.getMinutes()  : ""+ loaclDate.getMinutes())+
	                  (loaclDate.getSeconds()< 10? "0" + loaclDate.getSeconds()  : ""+ loaclDate.getSeconds());
	return loaclString;
}*/

//===============================================显示代码  start====================================================================
/**
 * 在代码窗口加载代码示例
 * @param code
 */
function showCode(code)
{
	$("#codeline").append("<span class='codeline'>" + code + "</span><br>");
}

/**
 * 清空代码示例
 * Clear eSDK Demo Code
 */
function cleanUpCode()
{
	$("#codeline").empty();
}
//===============================================显示代码  end====================================================================

// ==============================================页面关闭清理关闭ocx控件================================================================
/**
 * 退出时使用中断，防止OCX卸载异常，造成浏览器崩溃
 * 
 */
function closeSession()
{
	if (ocx)
	{
		try
		{
			event.returnValue = alert(langs[lang]["exitDemo"]);
			logout();
		}
		catch (e)
		{
		}
	}
}

/**
 * 退出界面关闭所有资源
 * Release Ocx Object
 */
function logout()
{
	try
	{
		ocx.IVS_OCX_StopAllRealPlay(); // Stop All Playing Live 
		ocx.IVS_OCX_Logout(); // User Logout
		ocx.IVS_OCX_CleanUp(); // Release OCX
		ocx = null;
		$("#ocx").remove(); // Remove OCX From Html Document
	} 
	catch (e)
	{
	}
}