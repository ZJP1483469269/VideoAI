<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Live.aspx.cs" Inherits="gtws_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>IVS SDK DEMO</title>
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="/static/css/bootstrap.min.css" />
    <link href="../static/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery/jquery.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.ui.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.overlaycallback.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.bgiframe.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.layout.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.ztree.core.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.ztree.excheck.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.ztree.exedit.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.sha256.min.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.combobox.js"></script>
    <script type="text/javascript" src="js/jquery/jquery.ui.autocomplete.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
    <script type="text/javascript" src="/static/js/bootstrap-collapse.js"></script>
    <script src="../static/zTree/js/jquery.ztree.all.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/static/js/work/TV.js"></script>
    <script type="text/javascript">
        var vDeviceID = "<%=DEVICE_ID %>";
        $(document).ready(function () {
            Video_Init();
            Video_UserLogin();
            if (ocx) {
                ocx.IVS_OCX_SetWndLayout(11);
            }

            if (vDeviceID.length > 0) {
                Video_OpenCamera(vDeviceID);
            }
        });

        function Video_Init() {
            if (ocx) {
                var result = ocx.IVS_OCX_Init();
                var path = $("logPath").val();
                var path = "c:/ocxlog/";
                result = ocx.IVS_OCX_SetLogPath(path);

                // 设置OCX界面色调： "1"为黑色(暗色调) ，"2"为白色(亮色调)
                ocx.IVS_OCX_SetSkin(1);

                // 设置OCX显示语言："zh-CN"为中文，"en-US"为英文               
                ocx.IVS_OCX_SetLanguage("zh-CN");
            }
        }

        function Video_UserLogin() {
            if (ocx) {
                var user = $("#user").val();
                var pwd = $("#pwd").val();
                var ip = $("#ip").val();
                var port = $("#port").val();

                var result = ocx.IVS_OCX_Login(user, pwd, ip, port, 1);
                if (result == 0) {
                    // 如果登录成功，调用设置接收事件回调方法，用于接收服务端事件回调消息
                    ocx.IVS_OCX_SetEventReceiver();
                }
            }
        }

        function closeSession() {
            if (ocx) {
                try {
                    logout();
                }
                catch (e) {
                }
            }
        }

        function logout() {
            try {
                ocx.IVS_OCX_StopAllRealPlay(); // Stop All Playing Live 
                ocx.IVS_OCX_Logout(); // User Logout
                ocx.IVS_OCX_CleanUp(); // Release OCX
                ocx = null;
                $("#ocx").remove(); // Remove OCX From Html Document
            }
            catch (e) {
            }
        }

        var ActiveWnd = 0;
        function Video_OpenCamera(cCameraCode) {
            ActiveWnd = ocx.IVS_OCX_GetFreeWnd();

            var xml = ocx.IVS_OCX_SetActiveWnd(ActiveWnd);
            if (xml != 0) {
                ActiveWnd = ocx.IVS_OCX_GetSelectWnd();
            }

            var streamType = "1";
            var protocolType = "2";
            var direstFirst = "1";
            var multi = "0";

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

            var result = ocx.IVS_OCX_StartRealPlay(pMediaParaxml, cCameraCode, ActiveWnd);
        }

        function Video_TakeCamera() {
            var iWin = ocx.IVS_OCX_GetSelectWnd();
            if (iWin) {

            }
        }

        var vREC_FLAG = false;
        function Video_Record() {
            if (vREC_FLAG) {
                Video_StopLocalRecord();
            }
            else {
                Video_StartLocalRecord();
            }
        }
        Date.prototype.Format = function (fmt) { //author: meizz 
            var o = {
                "M+": this.getMonth() + 1, //月份 
                "d+": this.getDate(), //日 
                "h+": this.getHours(), //小时 
                "m+": this.getMinutes(), //分 
                "s+": this.getSeconds(), //秒 
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
                "S": this.getMilliseconds() //毫秒 
            };
            if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }

        function Video_StartLocalRecord() {
            if (ocx) {
                var vDayTime = new Date();
                var filename = vDayTime.Format("yyyyMMddHHMM") + ".mp4";
                var videoparam = "C:\\\\Video\\";
                if (videoparam == "") {
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
                    videoparam += "        <DiskWarningValue>2048</DiskWarningValue>"; // 磁盘空间小于此值告警，单位M 
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
                else {
                    var index = videoparam.indexOf("<SavePath>");
                    videoparam = videoparam.substring(0, index);
                    videoparam += "        <SavePath>" + filename + "</SavePath>";
                    videoparam += "    </RecordParam>";
                    videoparam += "</Content>";

                    $("#videoparam").val(videoparam);
                }

                var wnd = $("#localrecordwnd").val();
                if (wnd == "") {
                    alert(langs[lang]["inputWndFirst"]);
                    return;
                }

                var result = ocx.IVS_OCX_StartLocalRecord(videoparam, wnd);
                $("#btnRec").attr("value", "停止录像");
                vREC_FLAG = true;
            }
        }

        /**
        * 停止摄像机前端录像
        */
        function Video_StopLocalRecord() {
            if (ocx) {
                var iWnd = ocx.IVS_OCX_GetSelectWnd();
                var result = ocx.IVS_OCX_StopLocalRecord(iWnd);
                $("#btnRec").attr("value", "开始录像");
                vREC_FLAG = false;
            }
        }
    </script>
</head>
<body onload="init();" onbeforeunload="closeSession();" style="background-color: #ffffff">
    <div id="right" style="position: absolute; right: 3px; bottom: 43px; top: 3pt; left: 3px;
        border: solid 1px grey" title="">
        <object id="ocx" style="width: 100%; height: 100%;" classid="CLSID:3556A474-8B23-496F-9E5D-38F7B74654F4"
            codebase="ocx/IVS_OCX.cab#version=2,2,0,22">
        </object>
    </div>
    <div style="position: absolute; bottom: 3px; left: 0px; right: 0px; width: 100%;
        height: 40px; background-color: #000000; z-index: 9999">
        <input type="button" value="抓拍" />
        <input type="button" id="btnRec" value="开始录像" onclick="Video_Record();" />
    </div>
    <div style="display: none">
        <input id="user" type="text" value="<%= cUSER %>" />
        <input id="pwd" type="text" value="<%= cPASS %>" />
        <input id="ip" type="text" value="<%= cHOST %>" />
        <input id="port" type="text" value="<%= cPORT %>" />
        <input type="hidden" id="ORG_ID" name="ORG_ID" value="<%=ORG_ID  %>" />
    </div>
</body>
</html>
<script type="text/javascript" for="ocx" event="IVS_OCX_Event(MSG_TYPE,data)">
	 var xmlDoc = $.parseXML(data);
	xmlDoc = $(xmlDoc);
	 
	$("#eventType").val("IVS_OCX_Event:" +MSG_TYPE);
		  
</script>
<script type="text/javascript" for="ocx" event="IVS_OCX_Windows_Event(MSG_TYPE,data)">
    var xmlDoc = $.parseXML(data);
    xmlDoc = $(xmlDoc);
    $("#eventType").val("IVS_OCX_Windows_Event:" +MSG_TYPE);	  
</script>
