<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="widget_header" %>
<div id="navN">
    <ul>
        <li><a href="/index.aspx" target="_self">首 页</a></li>
        <li><a href="/QT/ZZJG.aspx" target="_self">组织机构</a></li>
        <li><a href="#" target="_self" onclick="c_tab('WZ_GTYW','WZ_GZDT','WZ_ZFGS')">国土要闻</a></li>
        <li><a href="#" target="_self" onclick="gzdt('WZ_GZDT')">工作动态</a></li>
        <li><a href="#" target="_self" onclick="c_tab('WZ_ZFGS','WZ_ZCFG','WZ_TZGG')">执法公示</a></li>
        <li><a href="#" target="_self" onclick="c_tab('WZ_ZCFG','WZ_TZGG','WZ_GTYW')">政策法规</a></li>
        <li><a href="#" target="_self" onclick="zfjg('WZ_ZFJG')">执法监管</a></li>
        <li><a href="#" target="_self" onclick="c_tab('WZ_TZGG','WZ_GTYW','WZ_GZDT')">通知公告</a></li>
    </ul>
</div>
