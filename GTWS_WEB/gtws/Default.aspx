<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="gtws_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>国土卫士</title>
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="/static/css/bootstrap.min.css" />
    <link href="../static/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
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
    <script src="../static/zTree/js/jquery.ztree.all.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/static/js/work/TV.js"></script>
    <style>
        body, html
        {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }
        .content
        {
            width: 100%;
            height: 100%;
        }
        
        .contentframe
        {
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
            var vNodeList = <%= ROOT_LIST%>;
            $.fn.zTree.init($("#TV_ID"), setting, vNodeList); 

            setInit();
            setLogin();
            if (ocx) {
                ocx.IVS_OCX_SetWndLayout(63);
            }
        });

        var CameraValList = [];
        var CameraTextList = [];
        function getCameraInfoList() {
            getCameraList();

            $("input:radio").each(function () {
                var vCameraID = $(this).attr("value");
                var vCameraName = $(this).attr("camera_name");
                CameraValList.push(vCameraID);
                CameraTextList.push(vCameraName);
            });

            UpdateLayerItem();
        }

        function setMapCenter(vX,vY) {
            var vUrl=$("#rf_map").attr("src");
            if(vUrl==null)
            {
                $("#rf_map").attr("src","../pages/webgis.aspx");                  
            }
            
            var rf_map=$("#rf_map"); 
            rf_map.contentWindow.setMapView(vX,vY);                        
        }

        function UpdateLayerItem() {
            var vORG_ID = "<%= ORG_ID %>";
            if (CameraValList.length > 0) {
                var vCameraID = CameraValList.pop();
                var vCameraName = CameraTextList.pop();
                var cValue = "ORG_ID=" + vORG_ID + "&IDS=" + vCameraID + "&NAMES=" + escape(vCameraName);
                var vUrl = getAjaxUrl() + "/api/rest.ashx?action_type=Camera&action_method=updatefromvideo";
                $.ajax({
                    url: vUrl,
                    dataType: "json",
                    method: 'GET',
                    data: cValue,
                    success: function (ret) {
                        UpdateLayerItem();
                    },
                    error: function (req, textStatus, errorThrown) {
                        alert("ERROR:" + errorThrown);
                    }
                });
            }
        }

        function doChangeView(vTypeID)
        {
            if(vTypeID==1)
            {
                $("#rf_map").show();
                $("#video").hide();
                var vUrl=$("map").attr("src");
                if(vUrl==null)
                {
                    $("#rf_map").attr("src","../pages/webgis.aspx");                  
                }
            }
            else
            {
                $("#rf_map").hide();
                $("#video").show();
               
            }
        }        
    </script>
</head>
<body onload="init();" onbeforeunload="closeSession();" style="position: absolute;
    margin: 0px; padding: 0px; left: 1px; right: 1px; top: 1px; bottom: 1px; z-index: 99">
    <div class="container-fluid">
        <div class="accordion" id="accordion" style="width: 280px">
            <div class="accordion-group" id="CAMERA_INFO">
                <div class="accordion-heading" style="text-align: center">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne">
                        摄像机 </a>
                </div>
                <div id="collapseOne" class="accordion-body collapse" style="height: 0px;">
                    <div class="accordion-inner">
                        <div id="TVS" style="width: 100%; height: 600px; overflow-y: scroll; overflow-x: scroll;">
                            <div class="zTreeDemoBackground left">
                                <ul id="TV_ID" class="ztree">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="accordion-group" id="PLAY_CONFIG">
                <div class="accordion-heading" style="text-align: center">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseTwo">
                        播放控制 </a>
                </div>
                <div id="collapseTwo" class="accordion-body collapse" style="text-align: center">
                    <div class="accordion-inner">
                        <div>
                            <input class="ptzbtn" type="button" value="↖" name="ptz_button" onmousedown="ptzControlMethod(5)"
                                onmouseup="ptzControlMethod(1)" />
                            <input class="ptzbtn" type="button" value="↑" name="ptz_button" onmousedown="ptzControlMethod(2)"
                                onmouseup="ptzControlMethod(1)" />
                            <input class="ptzbtn" type="button" value="↗" name="ptz_button" onmousedown="ptzControlMethod(8)"
                                onmouseup="ptzControlMethod(1)" />
                            <br>
                            <input class="ptzbtn" type="button" value="←" name="ptz_button" onmousedown="ptzControlMethod(4)"
                                onmouseup="ptzControlMethod(1)" />
                            <input class="ptzbtn" type="button" value="□" name="ptz_button" onclick="ptzControlMethod(1)"
                                title="停止">
                            <input class="ptzbtn" type="button" value="→" name="ptz_button" onmousedown="ptzControlMethod(7)"
                                onmouseup="ptzControlMethod(1)" />
                            <br>
                            <input class="ptzbtn" type="button" value="↙" name="ptz_button" onmousedown="ptzControlMethod(6)"
                                onmouseup="ptzControlMethod(1)" />
                            <input class="ptzbtn" type="button" value="↓" name="ptz_button" onmousedown="ptzControlMethod(3)"
                                onmouseup="ptzControlMethod(1)" />
                            <input class="ptzbtn" type="button" value="↘" name="ptz_button" onmousedown="ptzControlMethod(9)"
                                onmouseup="ptzControlMethod(1)" />
                            <br>
                            <input id="lock" class="ptzbtn" type="button" value="加锁" name="ptz_button" onclick="ptzControlMethod(34)" />
                            <input id="unlock" class="ptzbtn" type="button" value="解锁" name="ptz_button" onclick="ptzControlMethod(35)" />
                            <input id="zoomIn" class="ptzbtn" type="button" value="放大" name="ptz_button" onmousedown="ptzControlMethod(23)"
                                onmouseup="ptzControlMethod(1)" />
                            <input id="zoomOut" class="ptzbtn" type="button" value="缩小" name="ptz_button" onmousedown="ptzControlMethod(24)"
                                onmouseup="ptzControlMethod(1)" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="accordion-group" id="PLAY_PARM_CONFIG">
                <div class="accordion-heading" style="text-align: center">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse1">
                        播放参数 </a>
                </div>
                <div id="collapse1" class="accordion-body collapse" style="height: 0px;">
                    <div class="accordion-inner">
                        <select id="protocolType" title="协议类型">
                            <option value="1">UDP</option>
                            <option value="2" selected="selected">TCP</option>
                        </select>
                        <select id="streamType" title="码流类型">
                            <option id="streamType0" value="0">不指定</option>
                            <option id="streamType1" value="1" selected="selected">主码流</option>
                            <option id="streamType2" value="2">副码流1</option>
                            <option id="streamType3" value="3">副码流2</option>
                        </select>
                        <select id="direstFirst" title="是否直连优先">
                            <option id="direstFirst0" value="0">否</option>
                            <option id="direstFirst1" value="1" selected="selected">是</option>
                        </select>
                        <select id="multi" title="是否支持组播">
                            <option id="multi0" value="0" selected="selected">否</option>
                            <option id="multi1" value="1">是</option>
                        </select>
                        <input id="startLiveBtn" type="button" value="播放实况" onclick="getLivePlay();">
                        <br>
                        <span id="liveWndLbl" style="display: inline-block; width: 120px;">播放窗格</span>
                        <input type="text" id="livewnd">
                        <input id="stopLiveBtn" type="button" value="停止实况" onclick="stopLivePlay();">
                    </div>
                </div>
            </div>
            <div class="accordion-group" id="RECORD_CONFIG">
                <div class="accordion-heading" style="text-align: center">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseThree">
                        录像 </a>
                </div>
                <div id="collapseThree" class="accordion-body collapse">
                    <div class="accordion-inner">
                        <div>
                            <span id="platVideoCode" style="display: inline-block; width: 100px;">摄像机编码</span>
                            <input type="text" id="platrecordcameracode">
                            <span id="platVideoTime" style="display: inline-block; width: 100px;">录像时长</span>
                            <input type="text" id="platrecordtime" title="最小值为300，单位：秒">
                            <input id="startPlatRecordBtn" type="button" value="开始平台录像" onclick="startPlatRecord()">
                            <input id="stoptPlatRecordBtn" type="button" value="停止平台录像" onclick="stopPlatRecord()">
                        </div>
                        <div>
                            <span id="puVideoCode" style="display: inline-block; width: 100px;">摄像机编码</span>
                            <input type="text" id="purecordcameracode">
                            <span id="puVideoTime" style="display: inline-block; width: 100px;">录像时长</span>
                            <input type="text" id="purecordtime" title="最小值为300，单位：秒">
                            <input id="startPuRecordBtn" type="button" value="开始前端录像" onclick="startPuRecord()">
                            <input id="stoptPuRecordBtn" type="button" value="停止前端录像" onclick="stopPuRecord()">
                        </div>
                        <div>
                            <span id="videoParamLbl" style="display: inline-block; width: 100%;">录像参数</span>
                            <textarea id="videoparam" style="width: 100%; height: 100px;"></textarea>
                            <br>
                            <span id="videoFileName" style="display: inline-block; width: 100%">本地文件路径</span>
                            <input type="text" id="filename" style="width: 100%" onclick="getFilePath('filename')"
                                readonly="readonly">
                            <br>
                            <span id="localRecordWndLbl" style="display: inline-block; width: 100px;">窗格ID</span>
                            <input type="text" id="localrecordwnd">
                            <input id="startLocalRecordBtn" type="button" value="开始本地录像" onclick="startLocalRecord()">
                            <input id="stopLocalRecordBtn" type="button" value="停止本地录像" onclick="stopLocalRecord()">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="position: absolute; right: 3px; bottom: 3px; top: 3pt; left: 281px;
        padding: 0px; margin: 0px; border: solid 1px grey; background-color: Red" title="">
        <div id="video" style="width: 100%; height: 100%">
            <object id="ocx" style="width: 100%; height: 100%;" classid="CLSID:3556A474-8B23-496F-9E5D-38F7B74654F4"
                codebase="ocx/IVS_OCX.cab#version=2,2,0,22">
            </object>
        </div>
        <iframe id="rf_map" style="position: absolute; right: 3px; bottom: 3px; top: 3pt;
            left: 0px; padding: 0px; margin: 0px; width: 100%; height: 100%; display: none">
        </iframe>
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
