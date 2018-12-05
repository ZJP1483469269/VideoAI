/**
 * 初始化OCX控件
 */
function setInit()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_Init();
		var path = $("logPath").val();
		var path = "c:/ocxlog/";
		result = ocx.IVS_OCX_SetLogPath(path);
		$("#resultcode").val("IVS_OCX_Init:" +result);
		
		// 设置OCX界面色调： "1"为黑色(暗色调) ，"2"为白色(亮色调)
		ocx.IVS_OCX_SetSkin(1);
		
		//// 判断是否支持显卡硬解码
		//result = ocx.IVS_OCX_GetGPUDecodeAbility();
		//if (result == 0)
		//{   
		//	// 关闭显卡硬解码功能
		//	result = ocx.IVS_OCX_EnableGPU(result);
		//}
		
		// 设置OCX显示语言："zh-CN"为中文，"en-US"为英文
		if (lang == "zh-cn")
		{
			ocx.IVS_OCX_SetLanguage("zh-CN");
		}
		else	
		{
			ocx.IVS_OCX_SetLanguage("en-US");
		}
		
		cleanUpCode();
		showCode("html Code:");
		showCode('&lt;object id="ocx" style="width:100%;height: 100%; " classid="CLSID:3556A474-8B23-496F-9E5D-38F7B74654F4" codebase="/gtws/ocx/IVS_OCX.cab#version=2.2.0.1"&gt; &lt;/object&gt;');
		showCode("  ");
		showCode("javaScript Code:");
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_Init();");
		showCode("} ");
	}
}

/**
 * 获取OCX控件版本号
 */
function getVersion()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_GetVersion();
		
		result = result.toString(16);
		if (result.length >= 7) 
		{
			result = result.substr(0,1) + "." + result.substr(1,2) + "." + result.substr(3,2) + "." + result.substr(5,2);
			$("#version").val(result);
		}
		$("#resultcode").val("");
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_GetVersion();");
		showCode("} ");
	}
}

/**
 * 清理OCX控件  
 */
function setCleanUp()
{
	if (ocx)
	{
		// 清理OCX前，可以先调用用户注销方法
		var result = ocx.IVS_OCX_Logout();
		
		result = ocx.IVS_OCX_CleanUp();
		
		$("#resultcode").val("IVS_OCX_CleanUp:" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_CleanUp();");
		showCode("} ");
	}
}

/**
 * 用户登录
 */
function setLogin()
{
	if (ocx)
	{
		var user = $("#user").val();
		var pwd = $("#pwd").val();
		var ip = $("#ip").val();
		var port = $("#port").val();  
		
		var result = ocx.IVS_OCX_Login(user, pwd, ip, port, 1);
		
		$("#resultcode").val("IVS_OCX_Login:" +result);
		
		if (result == 0)
		{
			// 如果登录成功，调用设置接收事件回调方法，用于接收服务端事件回调消息
			ocx.IVS_OCX_SetEventReceiver();
			
			// 如果有需要，在登录成功后，设置告警订阅，用于接收指定类型的告警信息
			var pReqXml = 
				    "<?xml version='1.0' encoding='UTF-8'?>" +
					"<Content>" +
					"    <DomainCode>2431dc807b304590a006ff7a36cc26a9</DomainCode>" +
					"    <Subscribe>" +
					"        <SubscriberInfo>" +
					"            <Subscriber>1</Subscriber>" +
					"            <SubscriberID>0</SubscriberID>" +
					"            <UserDomainCode>2431dc807b304590a006ff7a36cc26a9</UserDomainCode>" +
					"        </SubscriberInfo>" +
					"        <SubscribeList>" +
					"            <SubscribeInfo>" +
					"                 <AlarmInCode></AlarmInCode>" +
					"                 <SubscribeType>1</SubscribeType>" +
					"                 <AlarmLevelValueMin></AlarmLevelValueMin>" +
					"                 <AlarmLevelValueMax></AlarmLevelValueMax>" +
					"            </SubscribeInfo>" +
					"        </SubscribeList>" +
					"    </Subscribe>" +
					"</Content>";
			result = ocx.IVS_OCX_SubscribeAlarm(pReqXml);
			 	
			$("#windowbtn").attr("disabled",false);
			$("#livebtn").attr("disabled",false);
			$("#videobtn").attr("disabled",false);
			$("#playbtn").attr("disabled",false);
			$("#voicebtn").attr("disabled",false);
			$("#downloadbtn").attr("disabled",false);
		}
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode('&nbsp;&nbsp;&nbsp;&nbsp;var user =   $("#user").val();');
		showCode('&nbsp;&nbsp;&nbsp;&nbsp;var pwd =  $("#pwd").val();');
		showCode('&nbsp;&nbsp;&nbsp;&nbsp;var ip =  $("#ip").val();');
		showCode('&nbsp;&nbsp;&nbsp;&nbsp;var port = $("#port").val();');
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_Login(user, pwd, ip, port, 1);");
		showCode("} ");
	}
}

/**
 * 用户注销
 */
function setLogout()
{
	if (ocx)
	{
		var result = ocx.IVS_OCX_Logout();
		
		$("#resultcode").val("IVS_OCX_Logout:" +result);
		
		if (result == 0)
		{
			$("#windowbtn").attr("disabled",true);
			$("#livebtn").attr("disabled",true);
			$("#videobtn").attr("disabled",true);
			$("#playbtn").attr("disabled",true);
			$("#voicebtn").attr("disabled",true);
			$("#downloadbtn").attr("disabled",true);
		}
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_Logout();");
		showCode("} ");
	}
}
/**
 * 修改用户密码
 * 说明 1 修改用户密码，必须先调用登录方法。
 *      2 密码有强度限制，该demo中未给出调用密码强度校验接口示例，请按照eSpace IVS产品手册关于密码强度的说明填写新密码。
 */
function setChangePwd()
{
	if (ocx)
	{
		var oldpwd =$("#oldpwd").val();
		var newpwd = $("#newpwd").val();
		
		var result = ocx.IVS_OCX_ChangePWD(oldpwd, newpwd);  
		
		$("#resultcode").val("IVS_OCX_ChangePWD:" +result);
		
		cleanUpCode();
		showCode("if (ocx)");
		showCode("{ ");
		showCode('&nbsp;&nbsp;&nbsp;&nbsp;var oldpwd =$("#oldpwd").val();');
		showCode('&nbsp;&nbsp;&nbsp;&nbsp;var newpwd = $("#newpwd").val();');
		showCode("&nbsp;&nbsp;&nbsp;&nbsp;ocx.IVS_OCX_ChangePWD(oldpwd, newpwd);");
		showCode("} ");
	}
}
