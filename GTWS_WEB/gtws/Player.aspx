<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Player.aspx.cs" Inherits="Player" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>国土卫士</title>
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="/static/css/bootstrap.min.css" />
    <link href="../static/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="js/demo_main.js"></script>
    <script type="text/javascript" src="js/demo_login.js"></script>
    <script type="text/javascript" src="js/demo_window.js"></script>
    <script type="text/javascript" src="js/demo_live.js"></script>
    <script type="text/javascript" src="js/demo_video.js"></script>
    <script type="text/javascript" src="js/demo_play.js"></script>
    <script type="text/javascript" src="js/demo_voice.js"></script>
    <script type="text/javascript" src="js/demo_download.js"></script>
    <script type="text/javascript" src="js/i18n/demo_en_US.js"></script>
    <script type="text/javascript" src="js/i18n/demo_zh_CN.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
    <script type="text/javascript" src="/static/js/bootstrap-collapse.js"></script> 
    <style type="text/css">
        body, html {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }

        .content {
            width: 100%;
            height: 100%;
        }

        .contentframe {
            width: 100%;
            height: 100%;
        }
    </style>
    <script type="text/javascript">
        function changeTextColor() {
            var i = document.getElementById("resultcode").value;
            var str = i.toString().split(":");
            if (str[1].toString() != "0") {
                document.getElementById("resultcode").style.color = "red";
            }
            else {
                document.getElementById("resultcode").style.color = "black";
            }
        }
        $(document).ready(function () {
            $("#PLAY_CONFIG").load('Play_Config.aspx');
            $("#PLAY_PARM_CONFIG").load('Play_Parm_Config.aspx');
            $("#RECORD_CONFIG").load('Record_Config.aspx', function () {
                setInit();
                setLogin();
                if (ocx) {
                    ocx.IVS_OCX_SetWndLayout(11);
                }
            });
        });


        function VIDEO_OPEN() {
            stopLivePlay();
            var cCameraCode = "93700995803001000101#249f8714c5114ec2b19c21fd8e5ca2b7";//摄像机编码 
            var iWnd = ocx.IVS_OCX_GetSelectWnd();
            if (iWnd == 0) {
                iWnd = ocx.IVS_OCX_GetFreeWnd();//获取空闲窗格
                var xml = ocx.IVS_OCX_SetActiveWnd(iWnd); //设置激活窗格
                alert(xml);
            }

            alert(iWnd);
            alert(iWnd);
            alert(iWnd);
            var streamType = "1";//码流类型
            var protocolType = "1";//协议类型
            var direstFirst = "1";//是否直连
            var multi = "0";//是否组播

            var pMediaParaxml = '<?xml version=\"1.0\" encoding=\"UTF-8\"?>'; //播放参数
            pMediaParaxml += '<Content>';
            pMediaParaxml += '<RealplayParam>';
            pMediaParaxml += '<StreamType>';
            pMediaParaxml += streamType;
            pMediaParaxml += '</StreamType>';
            pMediaParaxml += '<ProtocolType>';
            pMediaParaxml += protocolType;
            pMediaParaxml += '</ProtocolType>';
            pMediaParaxml += '<DirectFirst>';
            pMediaParaxml += direstFirst;
            pMediaParaxml += '</DirectFirst>';
            pMediaParaxml += '<Multicast>';
            pMediaParaxml += multi;
            pMediaParaxml += '</Multicast>';
            pMediaParaxml += '</RealplayParam>';
            pMediaParaxml += '</Content>';
            var iResult = ocx.IVS_OCX_StartRealPlay(pMediaParaxml, cCameraCode, iWnd);//开始实况 
            alert("StartRealPlay:" + iResult);
            return iResult;
        }

        function stopLivePlay() {
            if (ocx) {
                var iWnd = ocx.IVS_OCX_GetSelectWnd();//获取窗格 
                var iResult = ocx.IVS_OCX_StopRealPlay(iWnd);//停止实况
                alert("LivePlay:" + iResult);
                return iResult;
            }
        }


        function setMapCenter(vX, vY) {

        }
    </script>
</head>
<body onload="init();" onbeforeunload="closeSession();" style="position: absolute; margin: 0px; padding: 0px; left: 1px; right: 1px; top: 1px; bottom: 1px; z-index: 99">
    <div class="container-fluid">
        <div class="accordion" id="accordion" style="width: 280px">
            <div class="accordion-group" id="PLAY_CONFIG">
            </div>
            <div class="accordion-group" id="PLAY_PARM_CONFIG">
            </div>
            <div class="accordion-group" id="RECORD_CONFIG">
            </div>
        </div>
        <input type="button" onclick='VIDEO_OPEN();' value="1111111" />
    </div>
    <div id="right" style="position: absolute; right: 3px; bottom: 3px; top: 3pt; left: 281px; padding: 0px; margin: 0px; border: solid 1px grey; background-color: Red"
        title="">
        <object id="ocx" style="width: 100%; height: 100%;" classid="CLSID:3556A474-8B23-496F-9E5D-38F7B74654F4"
            codebase="ocx/IVS_OCX.cab#version=2,2,0,22">
        </object>
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

    $("#eventType").val("IVS_OCX_Event:" + MSG_TYPE);

</script>
<script type="text/javascript" for="ocx" event="IVS_OCX_Windows_Event(MSG_TYPE,data)">
    var xmlDoc = $.parseXML(data);
    xmlDoc = $(xmlDoc);
    $("#eventType").val("IVS_OCX_Windows_Event:" + MSG_TYPE);
</script>
