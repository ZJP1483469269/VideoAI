<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>IVS SDK DEMO</title>
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <link rel="stylesheet" type="text/css" href="/static/css/bootstrap.min.css" />
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
    </script>
</head>
<body onload="init();" onbeforeunload="closeSession();" style="background-color: #ffffff">
    <div class="accordion" id="accordion2" style="wdith: 160px">
        <div class="accordion-group">
            <div class="accordion-heading">
                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne">
                    窗格管理 </a>
            </div>
            <div class="accordion-body collapse" style="height: 0px;">
                <div class="accordion-inner">
                    <div id="window" class="div_page">
                        <div>
                            <span id="displayScale" style="display: inline-block; width: 140px;">显示比例</span>
                            <select id="scale">
                                <option value="1" id="originalScale">原始比例</option>
                                <option value="2" id="stretchScale">铺满窗口</option>
                            </select>
                            <input id="setScaleBtn" type="button" value="设置显示比例" onclick="setScale();" align="right">
                            <br>
                            <input id="fullScreenBtn" type="button" value="全屏显示" onclick="setFullScreen();" align="right">
                            <input id="exitFullScreenBtn" type="button" value="退出全屏" onclick="setNormalScreen();"
                                align="right">
                        </div>
                        <div>
                            <span id="setToolbar" style="display: inline-block; width: 140px;">设置工具栏</span>
                            <input id="setToolbarBtn" type="button" value="设置工具栏" onclick="setToolbar();"><br>
                            <input type="checkbox" id="tool_SNAPSHOT" value="0x00000001">
                            <span id="tool01" style="display: inline-block; width: 140px;">抓拍</span>
                            <input type="checkbox" id="tool_LOCAL_RECORD" value="0x00000002">
                            <span id="tool02" style="display: inline-block; width: 140px;">本地录像</span>
                            <input type="checkbox" id="tool_BOOKMARK" value="0x00000004">
                            <span id="tool03" style="display: inline-block; width: 140px;">书签</span>
                            <br>
                            <input type="checkbox" id="tool_ZOOM" value="0x00000008">
                            <span id="tool04" style="display: inline-block; width: 140px;">局部放大</span>
                            <input type="checkbox" id="tool_PLAY_RECORD" value="0x00000010">
                            <span id="tool05" style="display: inline-block; width: 140px;">即时回放</span>
                            <input type="checkbox" id="tool_PLAY_SOUND" value="0x00000020">
                            <span id="tool06" style="display: inline-block; width: 140px;">声音</span>
                            <br>
                            <input type="checkbox" id="tool_TALKBACK" value="0x00000040">
                            <span id="tool07" style="display: inline-block; width: 140px;">对讲</span>
                            <input type="checkbox" id="tool_VIDEO_TVW" value="0x00000080">
                            <span id="tool08" style="display: inline-block; width: 140px;">视频上墙</span>
                            <input type="checkbox" id="tool_ALARM_WIN" value="0x00000100">
                            <span id="tool09" style="display: inline-block; width: 140px;">设置告警窗口</span>
                            <br>
                            <input type="checkbox" id="tool_PTZ" value="0x00000200">
                            <span id="tool10" style="display: inline-block; width: 140px;">云镜控制</span>
                            <input type="checkbox" id="tool_IA" value="0x00000400">
                            <span id="tool11" style="display: inline-block; width: 140px;">叠加智能分析</span>
                            <input type="checkbox" id="tool_OPEN_MAP" value="0x00000800">
                            <span id="tool12" style="display: inline-block; width: 140px;">打开电子地图</span>
                            <br>
                            <input type="checkbox" id="tool_WINDOW_MAIN" value="0x00001000">
                            <span id="tool13" style="display: inline-block; width: 140px;">一主多辅</span>
                            <input type="checkbox" id="tool_PLAY_QUALITY" value="0x00002000">
                            <span id="tool14" style="display: inline-block; width: 140px;">网速优先/画质优先</span>
                            <br>
                            <span id="setLayout" style="display: inline-block; width: 140px;">设置窗格数量</span>
                            <input id="setLayoutOneBtn" type="button" value="一格" onclick="setLayout(1);">
                            <input id="setLayoutSixBtn" type="button" value="六格" onclick="setLayout(6);">
                            <input id="setLayoutNineBtn" type="button" value="九格" onclick="setLayout(9);">
                            <br>
                            <span id="selectedWnd" style="display: inline-block; width: 140px;">选中窗格ID</span>
                            <input type="text" id="selectwnd">
                            <input id="selectedWndBtn" type="button" value="获取选中窗格ID" onclick="getSelectedWnd();">
                            <br>
                            <span id="freeWnd" style="display: inline-block; width: 140px;">空闲窗格ID</span>
                            <input type="text" id="freewnd">
                            <input id="freeWndBtn" type="button" value="获取空闲窗格ID" onclick="getFreeWnd();">
                            <br>
                            <span id="activeWndCode" style="display: inline-block; width: 140px;">活动窗格设备编码</span>
                            <input type="text" id="codeinwnd">
                            <input id="activeWndCodeBtn" type="button" value="获取活动窗格中设备编码" onclick="getCameraByWnd();">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="accordion-group">
            <div class="accordion-heading">
                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne">
                    窗格管理 </a>
            </div>
            <div class="accordion-body collapse" style="height: 0px;">
                <div class="accordion-inner">
                    <div id="page">
                        <!-- 登录注销页面 -->
                        <div id="login" class="div_page">
                            <div style="border: none;">
                                <input id="initBtn" type="button" value="初始化" onclick="setInit();">
                                <input id="versionBtn" type="button" value="获取版本号" onclick="getVersion();">
                                <input id="version" type="text">
                                <input id="releaseBtn" type="button" value="释放" onclick="setCleanUp();">
                            </div>
                            <div style="display: none">
                                <span id="userNameLbl" style="display: inline-block; width: 110px;">用户名 :</span>
                                <input id="user" type="text" value="<%= cUSER %>">
                                <span id="userPwdLbl" style="display: inline-block; width: 110px; padding-left: 70px">
                                    密码:</span>
                                <input id="pwd" type="text" value="<%= cPASS %>">
                                <input id="loginBtn" type="button" value="登录" onclick="setLogin();">
                                <br>
                                <span id="serverLbl" style="display: inline-block; width: 110px;">服务器:</span>
                                <input id="ip" type="text" value="<%= cHOST %>">
                                <span id="portLbl" style="display: inline-block; width: 110px; padding-left: 70px">端口:</span>
                                <input id="port" type="text" value="<%= cPORT %>">
                                <input id="logoutBtn" type="button" value="注销" onclick="setLogout();">
                            </div>
                        </div>
                        <!-- 窗格管理 -->
                        <!-- 实况与云台控制 -->
                        <div id="live" class="div_page" style="top: 100px">
                            <div>
                                <span id="cameraListLbl" style="display: inline-block; width: 120px;">摄像机列表</span>
                                <input id="getCameraListBtn" type="button" value="获取列表" onclick="getCameraList();" />
                                <input id="Button1" type="button" value="读取数据" onclick="getCameraInfoList();" />
                                <br />
                                <input id="Button2" type="button" value="打开监控" onclick="Video_OpenCamera('93700975875001000101#249f8714c5114ec2b19c21fd8e5ca2b7');" />
                                <div id="cameraList" style="overflow: auto; height: 155px;">
                                </div>
                            </div>
                            <div>
                                <span id="liveParam" style="display: inline-block; width: 120px;">播放参数</span>
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
                            <br>
                            <div>
                                <input class="ptzbtn" type="button" value="↖" name="ptz_button" onmousedown="ptzControlMethod(5)"
                                    onmouseup="ptzControlMethod(1)">
                                <input class="ptzbtn" type="button" value="↑" name="ptz_button" onmousedown="ptzControlMethod(2)"
                                    onmouseup="ptzControlMethod(1)">
                                <input class="ptzbtn" type="button" value="↗" name="ptz_button" onmousedown="ptzControlMethod(8)"
                                    onmouseup="ptzControlMethod(1)">
                                <br>
                                <input class="ptzbtn" type="button" value="←" name="ptz_button" onmousedown="ptzControlMethod(4)"
                                    onmouseup="ptzControlMethod(1)">
                                <input class="ptzbtn" type="button" value="□" name="ptz_button" onclick="ptzControlMethod(1)"
                                    title="停止">
                                <input class="ptzbtn" type="button" value="→" name="ptz_button" onmousedown="ptzControlMethod(7)"
                                    onmouseup="ptzControlMethod(1)">
                                <br>
                                <input class="ptzbtn" type="button" value="↙" name="ptz_button" onmousedown="ptzControlMethod(6)"
                                    onmouseup="ptzControlMethod(1)">
                                <input class="ptzbtn" type="button" value="↓" name="ptz_button" onmousedown="ptzControlMethod(3)"
                                    onmouseup="ptzControlMethod(1)">
                                <input class="ptzbtn" type="button" value="↘" name="ptz_button" onmousedown="ptzControlMethod(9)"
                                    onmouseup="ptzControlMethod(1)">
                                <br>
                                <input id="lock" class="ptzbtn" type="button" value="加锁" name="ptz_button" onclick="ptzControlMethod(34)">
                                <input id="unlock" class="ptzbtn" type="button" value="解锁" name="ptz_button" onclick="ptzControlMethod(35)">
                                <br>
                                <input id="zoomIn" class="ptzbtn" type="button" value="放大" name="ptz_button" onmousedown="ptzControlMethod(23)"
                                    onmouseup="ptzControlMethod(1)">
                                <input id="zoomOut" class="ptzbtn" type="button" value="缩小" name="ptz_button" onmousedown="ptzControlMethod(24)"
                                    onmouseup="ptzControlMethod(1)">
                            </div>
                            <br>
                            <div>
                                <span id="presetNameLbl" style="display: inline-block; width: 120px;">预置位名称</span>
                                <input id="presetname" type="text">
                                <input id="addPresetBtn" type="button" value="增加预置位" onclick="addPreset()">
                                <br>
                                <span id="presetIndexLbl" style="display: inline-block; width: 120px;">预置位索引</span>
                                <input id="presetindex" type="text" readonly="readonly">
                            </div>
                            <br>
                            <div id="presetlist">
                            </div>
                            <div>
                                <input id="getPresetListBtn" type="button" value="查询预置位" onclick="getPresetList()">
                                <br>
                                <input id="delPresetBtn" type="button" value="删除预置位" onclick="delPreset()">
                                <br>
                                <input id="loadPresetBtn" type="button" value="调用预置位" onclick="loadPreset()">
                            </div>
                        </div>
                        <!-- 录像 -->
                        <div id="video" class="div_page" style="display: none;">
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
                                <span id="videoParamLbl" style="display: inline-block; width: 100px;">录像参数</span>
                                <textarea id="videoparam" style="width: 500px; height: 100px;"></textarea>
                                <br>
                                <span id="videoFileName" style="display: inline-block; width: 100px;">本地文件路径</span>
                                <input type="text" id="filename" style="width: 500px;" onclick="getFilePath('filename')"
                                    readonly="readonly">
                                <br>
                                <span id="localRecordWndLbl" style="display: inline-block; width: 100px;">窗格ID</span>
                                <input type="text" id="localrecordwnd">
                                <input id="startLocalRecordBtn" type="button" value="开始本地录像" onclick="startLocalRecord()">
                                <input id="stopLocalRecordBtn" type="button" value="停止本地录像" onclick="stopLocalRecord()">
                            </div>
                        </div>
                        <!-- 回放 -->
                        <div id="play" class="div_page" style="display: none;">
                            <div>
                                <span id="fromTimeLbl" style="display: inline-block; width: 120px;">开始时间</span>
                                <input id="fromtime" type="text" value="20130427000000" title="时间格式为yyyyMMddHHmmss">
                                <span id="toTimeLbl" style="display: inline-block; width: 120px;">结束时间</span>
                                <input id="totime" type="text" value="20130427230000" title="时间格式为yyyyMMddHHmmss">
                                <input id="queryBtn" type="button" value="查询" onclick="getRecordList1()">
                                <br>
                                <span id="playBackCode" style="display: inline-block; width: 120px;">摄像机编码</span>
                                <input id="cameracode" type="text">
                                <span id="recordMethodLbl" style="display: inline-block; width: 120px;">录像方式</span>
                                <select id="recordmethod">
                                    <option id="recordMethod0" value="0" selected="selected">平台</option>
                                    <option id="recordMethod1" value="1">前端</option>
                                </select>
                                <br>
                                <div id="recordtable">
                                    <div>
                                        <span id="codeTh" style="display: inline-block; width: 120px; text-align: center;">摄像头编码</span>
                                        <span>|</span> <span id="startTimeTh" style="display: inline-block; width: 100px;
                                            text-align: center;">开始时间</span> <span>|</span> <span id="stopTimeTh" style="display: inline-block;
                                                width: 100px; text-align: center;">结束时间</span> <span>|</span> <span id="typeTh" style="display: inline-block;
                                                    width: 69px; text-align: center;">录像方式</span> <span>|</span> <span id="wndTh" style="display: inline-block;
                                                        width: 69px; text-align: center;">窗格ID</span> <span>|</span> <span id="optTh" style="display: inline-block;
                                                            width: 120px; text-align: center;">操作</span>
                                    </div>
                                    <div id="recordlist" style="height: 200px; overflow: scroll;">
                                    </div>
                                </div>
                            </div>
                            <div>
                                <span id="playBackFile" style="display: inline-block; width: 120px;">本地文件名</span>
                                <input type="file" id="videofile" style="width: 500px;">
                                <br>
                                <span id="playBackWnd" style="display: inline-block; width: 120px;">窗格ID</span>
                                <input type="text" id="wndplayback">
                                <input id="startLocalPlayBack" type="button" value="开始本地回放" onclick="startLocalPlayBack()">
                                <input id="stopLocalPlayBack" type="button" value="结束本地回放" onclick="stopLocalPlayBack()">
                            </div>
                            <div>
                                <input type="button" id="playBackPause" value="回放暂停" onclick="pause()">
                                <input type="button" id="playBackResume" value="回放恢复" onclick="resume()">
                                <br>
                                <input type="button" id="frameStepForward" value="单帧快进" onclick="frameStepForward()">
                                <input type="button" id="frameStepBackward" value="单帧快退" onclick="frameStepBackward()">
                                <br>
                                <input type="button" id="setPlayBackSpeed" value="快速播放" onclick="setSpeed()">
                                <select id="speed">
                                    <option id="speed00" value="1.0">正常速率</option>
                                    <option id="speed01" value="2.0">2倍速顺序播放</option>
                                    <option id="speed02" value="4.0" selected="selected">4倍速顺序播放</option>
                                    <option id="speed03" value="8.0">8倍速顺序播放</option>
                                    <option id="speed04" value="16.0">16倍速顺序播放</option>
                                    <option id="speed05" value="-0.25">4分之1速后退播放</option>
                                    <option id="speed06" value="-0.5">2分之1速后退播放</option>
                                    <option id="speed07" value="-1.0">1倍速后退播放</option>
                                    <option id="speed08" value="-2.0">2倍速后退播放</option>
                                    <option id="speed09" value="-4.0">4倍速后退播放</option>
                                    <option id="speed10" value="-8.0">8倍速后退播放</option>
                                    <option id="speed11" value="-16.0">16倍速后退播放</option>
                                    <option id="speed12" value="0.5">2分之1速顺序播放</option>
                                    <option id="speed13" value="0.25">4分之1速顺序播放</option>
                                    <option id="speed14" value="0.125">8分之1速顺序播放</option>
                                    <option id="speed15" value="0.0625">16分之1速顺序播放</option>
                                    <option id="speed16" value="0.03125">32分之1速顺序播放</option>
                                </select>
                                <br>
                                <input type="button" id="playBackTime" value="回放拖动" onclick="setTime()">
                                <input type="text" id="time" title="输入为数字，单位：秒。例如：输入30，则录像跳转至第30秒开始播放，如果超出录像时长则录像跳转至尾部">
                            </div>
                        </div>
                        <!-- 语音对讲和广播 -->
                        <div id="voice" class="div_page" style="display: none;">
                            <div>
                                <span id="talkParamLbl" style="display: inline-block; width: 120px;">媒体参数</span>
                                <input type="text" id="talkparam" style="width: 500px;"><br>
                                <span id="talkCode" style="display: inline-block; width: 120px;">音频设备</span>
                                <input type="text" id="talkcameracode" style="width: 500px;"><br>
                                <span id="talkHandle" style="display: inline-block; width: 120px;">对讲句柄</span>
                                <input type="text" id="handle" readonly="readonly">
                                <br>
                                <input id="startTalkBtn" type="button" value="开始语音对讲" onclick="startVoiceTalkback()">
                                <input id="stopTalkBtn" type="button" value="停止语音对讲" onclick="stopVoiceTalkback()">
                            </div>
                            <div>
                                <span id="broadcastCode" style="display: inline-block; width: 120px;">广播摄像机编码</span>
                                <input type="text" id="broadcameracode" style="width: 500px;">
                                <br>
                                <input id="addBroadcastDeviceBtn" type="button" value="添加广播摄像机" onclick="addBroadcastDevice()">
                                <input id="deleteBroadcastDeviceBtn" type="button" value="删除广播摄像机" onclick="deleteBroadcastDevice()">
                                <input id="getBroadcastDeviceBtn" type="button" value="获取广播摄像机列表" onclick="getBroadcastDevice()">
                                <br>
                                <span style="display: inline-block; width: 120px;"></span>
                                <div id="cameracodelist" style="display: inline-block; width: 500px;">
                                </div>
                                <br>
                                <input id="startRealBroadcastBtn" type="button" value="开始语音广播" onclick="startRealBroadcast()">
                                <input id="stopRealBroadcastBtn" type="button" value="停止语音广播" onclick="stopRealBroadcast()">
                                <br>
                                <span id="voiceFileLbl" style="display: inline-block; width: 120px;">文件名</span>
                                <input type="file" id="voicefile" style="width: 500px;"><br>
                                <span id="cycleTypeLbl" style="display: inline-block; width: 120px;">循环类型</span>
                                <select id="cycletype">
                                    <option value="0" id="cycletype0">不循环</option>
                                    <option value="1" id="cycletype1">循环</option>
                                </select>
                                <br>
                                <input id="startFileBroadcastBtn" type="button" value="开始文件广播" onclick="startFileBroadcast()">
                                <input id="stopFileBroadcastBtn" type="button" value="停止文件广播" onclick="stopFileBroadcast()">
                            </div>
                        </div>
                        <!-- 抓拍与录像下载 -->
                        <div id="download" class="div_page" style="display: none;">
                            <div>
                                <span id="downloadParamLbl" style="display: inline-block; width: 120px;">下载参数</span>
                                <input type="text" id="downloadparam" style="width: 500px;">
                                <br>
                                <span id="downloadCode" style="display: inline-block; width: 120px;">摄像机编码</span>
                                <input type="text" id="downloadcameracode" style="width: 500px;" value="05235480000000000101#aefe620eea574926827148a50a837dd4">
                                <br>
                                <span id="downloadSavePath" style="display: inline-block; width: 120px;">保存文件路径</span>
                                <input type="text" id="downloadfilePath" style="width: 500px;" onclick="getFilePath('downloadfilePath')">
                                <br>
                                <span id="starttimeLbl" style="display: inline-block; width: 120px;">开始时间</span>
                                <input type="text" id="starttime" value="20130426000644" title="时间格式为yyyyMMddHHmmss">
                                <span id="endtimeLbl" style="display: inline-block; width: 120px;">结束时间</span>
                                <input type="text" id="endtime" value="20130426230644" title="时间格式为yyyyMMddHHmmss">
                                <br>
                                <span id="downloadhandleLbl" style="display: inline-block; width: 120px;">下载句柄</span>
                                <input type="text" id="downloadhandle" readonly="readonly">
                                <br>
                                <input id="startPlatDownBtn" type="button" value="开始平台下载" onclick="startDownload(0)">
                                <input id="startPUDownBtn" type="button" value="开始前端下载" onclick="startDownload(1)">
                                <input id="stopPlatDownBtn" type="button" value="停止平台下载" onclick="stopDownload(0)">
                                <input id="stopPUDownBtn" type="button" value="停止前端下载" onclick="stopDownload(1)">
                                <input id="pauseBtn" type="button" value="暂停下载" onclick="downloadPause()">
                                <input id="resumeBtn" type="button" value="继续下载" onclick="downloadResume()">
                                <br>
                                <span id="downloadinfoLbl" style="display: inline-block; width: 120px;">下载信息</span>
                                <textarea id="downloadinfo" style="width: 500px; height: 100px;"></textarea>
                                <input id="getDonwloadinfoBtn" type="button" value="获取下载信息" onclick="getDownloadInfo()">
                            </div>
                            <div>
                                <span id="picWnd" style="display: inline-block; width: 120px;">窗格ID</span>
                                <input type="text" id="snapshotwnd">
                                <input id="picBtn" type="button" value="本地单张抓拍" onclick="localSnapshot()">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="position: absolute; right: 3px; bottom: 3px; top: 3pt; left: 600px;
        border: solid 1px grey" title="">
        <object id="ocx" style="width: 100%; height: 100%;" classid="CLSID:3556A474-8B23-496F-9E5D-38F7B74654F4"
            codebase="ocx/11IVS_OCX.cab#version=2,2,0,22">
        </object>
    </div>
</body>
</html>
<script language="javascript" for="ocx" event="IVS_OCX_Event(MSG_TYPE,data)">
	 var xmlDoc = $.parseXML(data);
	xmlDoc = $(xmlDoc);
	 
	$("#eventType").val("IVS_OCX_Event:" +MSG_TYPE);
		  
</script>
<script language="javascript" for="ocx" event="IVS_OCX_Windows_Event(MSG_TYPE,data)">
	 var xmlDoc = $.parseXML(data);
	xmlDoc = $(xmlDoc);
	 
	$("#eventType").val("IVS_OCX_Windows_Event:" +MSG_TYPE);	  
</script>
