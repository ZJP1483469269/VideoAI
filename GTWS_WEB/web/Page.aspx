<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page.aspx.cs" Inherits="DefaultPage" %>

<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>

<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="div.css" rel="stylesheet" />
    <link href="style.css" rel="stylesheet" />
    <script src="../static/js/jquery.js"></script>
    <script src="index.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">

            <div id="conment">
                <div class="neirong">
                    <div class="banner">
                    </div>
                    <!-- 导航菜单-->
                    <uc1:header runat="server" />
                    <!-- 热点新闻等-->
                    <div class="conment1">
                        <div class="con_left">
                            <div class="conL_kuang" id="idTransformView">
                                <a href="#">
                                    <ul class="slider" id="idSlider">

                                        <li><a href="#" onclick="c_id('222 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-12-10-11-37-52.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('221 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-12-03-09-59-46.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('220 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-11-02-11-35-57.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('219 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-10-17-11-30-58.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('218 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-10-12-05-34-05.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('217 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-10-09-08-51-55.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('216 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-10-08-05-53-23.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('213 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-09-13-09-27-35.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('212 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-09-13-09-24-07.jpg" width="518" height="308" /></a></li>

                                        <li><a href="#" onclick="c_id('211 ','WZ_GTYW')">
                                            <img src="/TP/detail_tp/2018-09-12-03-39-41.jpg" width="518" height="308" /></a></li>

                                    </ul>
                                    <ul class="num" id="idNum">
                                        <li>1</li>
                                        <li>2</li>
                                        <li>3</li>
                                        <li>4</li>
                                        <li>5</li>
                                    </ul>
                            </div>
                            <div class="conL_zi" style="display: none;" id="tu0">
                                <a href="#" onclick="c_id(222,'WZ_GTYW')">王锋：发出正声音传递正能量奋力开创执法信访工作新局面
                                </a>
                            </div>
                            <div class="conL_zi" style="display: none;" id="tu1">
                                <a href="#" onclick="c_id(221,'WZ_GTYW')">省自然资源厅召开全省自然资源系统扫黑除恶专项斗争工作推进...
                                </a>
                            </div>
                            <div class="conL_zi" style="display: none;" id="tu2">
                                <a href="#" onclick="c_id(220,'WZ_GTYW')">全省“大棚房”问题清理整治专项行动工作推进会在省国土资源...
                                </a>
                            </div>
                            <div class="conL_zi" style="display: none;" id="tu3">
                                <a href="#" onclick="c_id(219,'WZ_GTYW')">省国土资源执法监察局走访慰问南大吴村困难群众
                                </a>
                            </div>
                            <div class="conL_zi" style="display: none;" id="tu4">
                                <a href="#" onclick="c_id(218,'WZ_GTYW')">河南省2018年例行督察意见反馈视频会在郑州召开
                                </a>
                            </div>
                        </div>
                        <div class="con_cent">
                            <div class="tab">
                                <ul class="menu" id="menutitle">
                                    <li id="tab_1" class="aaa"><a href="javascript:void(0)" onmouseover="tabs('1');">新闻热点</a></li>
                                    <li id="tab_2"><a href="javascript:void(0)" onmouseover="tabs('2');">工作动态</a></li>
                                </ul>
                                <div class="tab_b" id="tab_a1" style="display: block;">
                                    ● <a href="#" onclick="c_id(222,'WZ_GTYW')">王锋：发出正声音传递正能量奋力开创执法信访工作新局面
                                    <span style="float: right">2018-12-10</span></a><br />

                                    ● <a href="#" onclick="c_id(221,'WZ_GTYW')">省自然资源厅召开全省自然资源系统扫黑除恶专项斗争工作...
                                    <span style="float: right">2018-12-03</span></a><br />

                                    ● <a href="#" onclick="c_id(220,'WZ_GTYW')">全省“大棚房”问题清理整治专项行动工作推进会在省国土...
                                    <span style="float: right">2018-11-02</span></a><br />

                                    ● <a href="#" onclick="c_id(219,'WZ_GTYW')">省国土资源执法监察局走访慰问南大吴村困难群众
                                    <span style="float: right">2018-10-17</span></a><br />

                                    ● <a href="#" onclick="c_id(218,'WZ_GTYW')">河南省2018年例行督察意见反馈视频会在郑州召开
                                    <span style="float: right">2018-10-11</span></a><br />

                                    ● <a href="#" onclick="c_id(217,'WZ_GTYW')">王锋：加油冲刺，高标准完成年度执法监管各项任务
                                    <span style="float: right">2018-10-08</span></a><br />

                                    ● <a href="#" onclick="c_id(216,'WZ_GTYW')">徐光副省长到省国土资源厅调研指导工作
                                    <span style="float: right">2018-09-29</span></a><br />

                                    ● <a href="#" onclick="c_id(215,'WZ_GTYW')">执法监察局党支部开展“双节”廉政安全教育活动
                                    <span style="float: right">2018-09-21</span></a><br />

                                    ● <a href="#" onclick="c_id(214,'WZ_GTYW')">自然资源部约谈土地违法违规问题严重11地市政府主要负...
                                    <span style="float: right">2018-09-17</span></a><br />

                                    ● <a href="#" onclick="c_id(213,'WZ_GTYW')">徐光副省长会见国家土地督察济南局局长田文彪
                                    <span style="float: right">2018-09-13</span></a><br />

                                </div>
                                <div class="tab_b" id="tab_a2" style="display: none;">
                                    ● <a href="#" onclick="c_id(253,'WZ_GZDT')">省自然资源厅第五调研组到滑县国土资源局调研扫黑除恶专...
                                    <span style="float: right">2018-12-24</span></a><br />

                                    ● <a href="#" onclick="c_id(252,'WZ_GZDT')">濮阳市南乐县国土资源局多举措规范“12336”违法线...
                                    <span style="float: right">2018-12-24</span></a><br />

                                    ● <a href="#" onclick="c_id(251,'WZ_GZDT')">省自然资源厅扫黑除恶专项斗争第七调研组到邓州市国土局...
                                    <span style="float: right">2018-12-21</span></a><br />

                                    ● <a href="#" onclick="c_id(250,'WZ_GZDT')">濮阳市南乐县国土资源局开展执法监察工作“回头看”活动
                                    <span style="float: right">2018-12-20</span></a><br />

                                    ● <a href="#" onclick="c_id(249,'WZ_GZDT')">驻马店市驿城区国土资源局“三块地” 改革取得显著成效
                                    <span style="float: right">2018-12-20</span></a><br />

                                    ● <a href="#" onclick="c_id(248,'WZ_GZDT')">邓州市国土资源城区分局拆除3宗违法违规建筑
                                    <span style="float: right">2018-12-19</span></a><br />

                                    ● <a href="#" onclick="c_id(247,'WZ_GZDT')">南乐县国土资源局组织召开扫黑除恶工作专题会议
                                    <span style="float: right">2018-12-18</span></a><br />

                                    ● <a href="#" onclick="c_id(246,'WZ_GZDT')">邓州市召开2018年全天候监测违法整改工作推进会
                                    <span style="float: right">2018-12-14</span></a><br />

                                    ● <a href="#" onclick="c_id(245,'WZ_GZDT')">巩义市国土资源局传达学习全省自然资源系统工作会议精神
                                    <span style="float: right">2018-12-14</span></a><br />

                                    ● <a href="#" onclick="c_id(244,'WZ_GZDT')">嵩县国土资源局加大冬季执法动态巡查力度
                                    <span style="float: right">2018-12-14</span></a><br />

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="conment2">
                        <div class="conment2_l">
                            <div class="conment2lB">
                                <img src="QT/images/bar.jpg" width="700" height="110" />
                            </div>
                            <div class="con2L_hang">
                                <div class="com2L_kuang">
                                    <div class="tit_lan">
                                        国土要闻<span style="float: right"><a href="#" onclick="c_tab('WZ_GTYW')" class="br">更多
                                        >></a></span>
                                    </div>
                                    <div class="con2L_kuai">

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(222,'WZ_GTYW')">王锋：发出正声音传递正能量奋力开创...
                                            <span style="float: right">2018-12-10</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(221,'WZ_GTYW')">省自然资源厅召开全省自然资源系统扫...
                                            <span style="float: right">2018-12-03</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(220,'WZ_GTYW')">全省“大棚房”问题清理整治专项行动...
                                            <span style="float: right">2018-11-02</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(219,'WZ_GTYW')">省国土资源执法监察局走访慰问南大吴...
                                            <span style="float: right">2018-10-17</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(218,'WZ_GTYW')">河南省2018年例行督察意见反馈视...
                                            <span style="float: right">2018-10-11</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(217,'WZ_GTYW')">王锋：加油冲刺，高标准完成年度执法...
                                            <span style="float: right">2018-10-08</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(216,'WZ_GTYW')">徐光副省长到省国土资源厅调研指导工...
                                            <span style="float: right">2018-09-29</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(215,'WZ_GTYW')">执法监察局党支部开展“双节”廉政安...
                                            <span style="float: right">2018-09-21</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(214,'WZ_GTYW')">自然资源部约谈土地违法违规问题严重...
                                            <span style="float: right">2018-09-17</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(213,'WZ_GTYW')">徐光副省长会见国家土地督察济南局局...
                                            <span style="float: right">2018-09-13</span></a>
                                        </div>

                                    </div>
                                </div>
                                <div class="com2L_kuang">
                                    <div class="tit_lan">
                                        工作动态<span style="float: right"><a href="#" onclick="c_tab('WZ_GZDT')" class="br">更多
                                        >></a></span>
                                    </div>
                                    <div class="con2L_kuai">

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(253,'WZ_GZDT')">省自然资源厅第五调研组到滑县国土资...
                                            <span style="float: right">2018-12-24</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(252,'WZ_GZDT')">濮阳市南乐县国土资源局多举措规范“...
                                            <span style="float: right">2018-12-24</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(251,'WZ_GZDT')">省自然资源厅扫黑除恶专项斗争第七调...
                                            <span style="float: right">2018-12-21</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(250,'WZ_GZDT')">濮阳市南乐县国土资源局开展执法监察...
                                            <span style="float: right">2018-12-20</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(249,'WZ_GZDT')">驻马店市驿城区国土资源局“三块地”...
                                            <span style="float: right">2018-12-20</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(248,'WZ_GZDT')">邓州市国土资源城区分局拆除3宗违法...
                                            <span style="float: right">2018-12-19</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(247,'WZ_GZDT')">南乐县国土资源局组织召开扫黑除恶工...
                                            <span style="float: right">2018-12-18</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(246,'WZ_GZDT')">邓州市召开2018年全天候监测违法...
                                            <span style="float: right">2018-12-14</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(245,'WZ_GZDT')">巩义市国土资源局传达学习全省自然资...
                                            <span style="float: right">2018-12-14</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(244,'WZ_GZDT')">嵩县国土资源局加大冬季执法动态巡查...
                                            <span style="float: right">2018-12-14</span></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="con2L_hang">
                                <div class="com2L_kuang">
                                    <div class="tit_lan">
                                        执法公示<span style="float: right"><a href="#" onclick="c_tab('WZ_ZFGS')" class="br">更多
                                        >></a></span>
                                    </div>
                                    <div class="con2L_kuai">

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(48,'WZ_ZFGS')">河南省国土资源厅公开挂牌督办10起...
                                            <span style="float: right">2018-06-26</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(47,'WZ_ZFGS')">河南省国土资源厅公示2017年11...
                                            <span style="float: right">2018-06-01</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(46,'WZ_ZFGS')">河南厅挂牌督办6起土地违法案
                                            <span style="float: right">2017-07-18</span></a>
                                        </div>

                                    </div>
                                </div>
                                <div class="com2L_kuang">
                                    <div class="tit_lan">
                                        政策法规<span style="float: right"><a href="#" onclick="c_tab('WZ_ZCFG')" class="br">更多
                                        >></a></span>
                                    </div>
                                    <div class="con2L_kuai">

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(70,'WZ_ZCFG')">国务院办公厅关于印发跨省域补充耕地...
                                            <span style="float: right">2018-03-29</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(69,'WZ_ZCFG')">坚守两道红线     严格执法监管
                                            <span style="float: right">2018-01-23</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(68,'WZ_ZCFG')">国土资源部关于进一步加强和改进执法...
                                            <span style="float: right">2017-12-25</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(67,'WZ_ZCFG')">国务院办公厅关于印发推行行政执法公...
                                            <span style="float: right">2017-12-25</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(64,'WZ_ZCFG')">国土资规〔2017〕3号
                                            <span style="float: right">2017-12-21</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(63,'WZ_ZCFG')">中华人民共和国土地管理法 第六章
                                            <span style="float: right">2017-05-26</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(62,'WZ_ZCFG')">中华人民共和国土地管理法 第五章
                                            <span style="float: right">2017-05-22</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(61,'WZ_ZCFG')">中华人民共和国土地管理法 第四章
                                            <span style="float: right">2017-05-23</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(60,'WZ_ZCFG')">中华人民共和国土地管理法 第三章
                                            <span style="float: right">2017-05-24</span></a>
                                        </div>

                                        <div class="con2L_zi">
                                            ● <a href="#" onclick="c_id(59,'WZ_ZCFG')">中华人民共和国土地管理法 第二章
                                            <span style="float: right">2017-05-25</span></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="conment2_r">
                            <div class="con_right">
                                <div class="conR1">
                                    <div class="conR1_hang">
                                        <a href="QT/ZZJG.aspx">河南省国土资源执法监察局介绍</a>
                                    </div>
                                    <div class="conR1_hang">
                                        <a href="QT/ZZJG.aspx">河南省国土资源执法监察局职责分工</a>
                                    </div>
                                </div>
                                <div class="conR2">
                                    <div class="conR2_kuang">
                                        <table width="250" border="0" cellspacing="0" cellpadding="1">
                                            <tr>
                                                <td>
                                                    <span class="td1"><a href="QT/ZZJG.aspx" class="br">综合处:0371-68108419</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="td1"><a href="QT/ZZJG.aspx" class="br">土地执法监察处:0371-68108185</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="td1"><a href="QT/ZZJG.aspx" class="br">矿产执法监察处:0371-68108869</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="td1"><a href="QT/ZZJG.aspx" class="br">执法检查指导处:0371-68108761</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="td1"><a href="QT/ZZJG.aspx" class="br">投稿邮箱:zfjcjtg@163.com</a></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="con2R_kuang">
                                <div class="tit_hui">
                                    通知公告
                                </div>
                                <div class="con2R_kuai">

                                    <div class="con2R_zi">
                                        <span class="fontR">〓</span> <a href="#" onclick="c_id(29,'WZ_TZGG')">河南省国土资源厅关于全面...
                                        <span style="float: right">2018-07-18</span></a>
                                    </div>

                                    <div class="con2R_zi">
                                        <span class="fontR">〓</span> <a href="#" onclick="c_id(26,'WZ_TZGG')">2018年7月1日正式启...
                                        <span style="float: right">2018-06-29</span></a>
                                    </div>

                                    <div class="con2R_zi">
                                        <span class="fontR">〓</span> <a href="#" onclick="c_id(24,'WZ_TZGG')">省国土资源系统 扫黑除恶...
                                        <span style="float: right">2018-03-30</span></a>
                                    </div>

                                    <div class="con2R_zi">
                                        <span class="fontR">〓</span> <a href="#" onclick="c_id(23,'WZ_TZGG')">河南省国土资源厅关于印发...
                                        <span style="float: right">2018-02-02</span></a>
                                    </div>

                                    <div class="con2R_zi">
                                        <span class="fontR">〓</span> <a href="#" onclick="c_id(22,'WZ_TZGG')">河南省国土资源执法监察局...
                                        <span style="float: right">2017-12-21</span></a>
                                    </div>

                                    <div class="con2R_zi">
                                        <span class="fontR">〓</span> <a href="#" onclick="c_id(20,'WZ_TZGG')">河南省国土资源厅办公室关...
                                        <span style="float: right">2017-12-21</span></a>
                                    </div>

                                    <div class="con2R_zi">
                                        <span class="fontR">〓</span> <a href="#" onclick="c_id(19,'WZ_TZGG')">河南省国土资源厅办公室关...
                                        <span style="float: right">2017-05-17</span></a>
                                    </div>

                                </div>
                            </div>
                            <a href="QT/WFJB.aspx">
                                <div class="con2R_tu">
                                    <p>举报电话：12336</p>
                                    <p>
                                        非工作时间举报电话： 13203812336
                                   
                                    </p>


                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="clearfloat">
                    </div>
                    <!-- 底部滚动图片-->
                    <div class="conment3">
                        <div class="title2">
                            图片
                        <div class="more">
                            <a href="#" class="gd" onclick="c_tab('WZ_GTYW','WZ_GZDT','WZ_ZFGS')">查看更多 >></a>
                        </div>
                        </div>
                        <div id="marquee1" class="marqueeleft">
                            <div style="width: 8000px;">
                                <ul id="marquee1_1">

                                    <li><a class="pic" href="#" onclick="c_id('222 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-12-10-11-37-52.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('222 ','WZ_GTYW')">王锋：发出正声音传递正能量...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('221 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-12-03-09-59-46.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('221 ','WZ_GTYW')">省自然资源厅召开全省自然资...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('220 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-11-02-11-35-57.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('220 ','WZ_GTYW')">全省“大棚房”问题清理整治...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('219 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-10-17-11-30-58.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('219 ','WZ_GTYW')">省国土资源执法监察局走访慰...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('218 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-10-12-05-34-05.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('218 ','WZ_GTYW')">河南省2018年例行督察意...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('217 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-10-09-08-51-55.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('217 ','WZ_GTYW')">王锋：加油冲刺，高标准完成...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('216 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-10-08-05-53-23.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('216 ','WZ_GTYW')">徐光副省长到省国土资源厅调...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('213 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-09-13-09-27-35.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('213 ','WZ_GTYW')">徐光副省长会见国家土地督察...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('212 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-09-13-09-24-07.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('212 ','WZ_GTYW')">国家土地督察济南局局长田文...</a>
                                        </div>
                                    </li>

                                    <li><a class="pic" href="#" onclick="c_id('211 ','WZ_GTYW')">
                                        <img src="../TP/detail_tp/2018-09-12-03-39-41.jpg" width="200" height="150" /></a>
                                        <div class="txt">
                                            <a href="#" onclick="c_id('211 ','WZ_GTYW')">王锋：牢记总书记教导 坚决...</a>
                                        </div>
                                    </li>

                                </ul>
                                <ul id="marquee1_2">
                                </ul>
                            </div>
                        </div>

                    </div>
                    <div id="link">
                        <table width="930" border="0">
                            <tr>
                                <td width="40">&nbsp;
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.bjgtj.gov.cn/col/col3664/index.html" target="_blank">北京</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.tjsqgt.gov.cn/Pages/index.aspx" target="_blank">天津</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.hebgt.gov.cn/index.do?templet=index" target="_blank">河北</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.shanxilr.gov.cn/" target="_blank">山西</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.nmggtt.gov.cn/" target="_blank">内蒙古</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.lgy.gov.cn/" target="_blank">辽宁</a>
                                </td>
                            </tr>
                            <tr>
                                <td width="40">&nbsp;
                                </td>
                                <td width="150" height="24">
                                    <a href="http://dlr.jl.gov.cn/" target="_blank">吉林</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.hljlr.gov.cn/" target="_blank">黑龙江</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.shgtj.gov.cn/" target="_blank">上海</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.jsmlr.gov.cn/" target="_blank">江苏</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.zjdlr.gov.cn/" target="_blank">浙江</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.fjgtzy.gov.cn/" target="_blank">福建</a>
                                </td>
                            </tr>
                            <tr>
                                <td width="40">&nbsp;
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.jxgtt.gov.cn/" target="_blank">江西</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.sddlr.gov.cn/" target="_blank">山东</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.hblr.gov.cn/" target="_blank">湖北</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.gdlr.gov.cn/" target="_blank">广东</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.gxdlr.gov.cn/" target="_blank">广西</a>
                                </td>
                                <td width="150" height="24">
                                    <a href="http://www.cqgtfw.gov.cn/" target="_blank">重庆</a>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </div>
            <div id="footer">
                <uc2:footer runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
