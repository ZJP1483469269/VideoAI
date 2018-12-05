<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index2.aspx.cs" Inherits="Admin_index2" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>国土执法监察外网平台</title>
    <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,user-scalable=yes, minimum-scale=0.4, initial-scale=0.8,target-densitydpi=low-dpi" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />

    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="./css/font.css">
    <link rel="stylesheet" href="./css/xadmin.css">
    <script type="text/javascript" src="https://cdn.bootcss.com/jquery/3.2.1/jquery.min.js"></script>
    <script src="./lib/layui/layui.js" charset="utf-8"></script>
    <script type="text/javascript" src="./js/xadmin.js"></script>

</head>
<body>
    <form id="Form1" runat="server">
        <!-- 顶部开始 -->
        <div class="container">
            <div class="logo"><a href="./index.html">国土执法监察外网平台</a></div>
            <div class="left_open">
                <i title="展开左侧栏" class="iconfont">&#xe699;</i>
            </div>

            <ul class="layui-nav right" lay-filter="">
                <li class="layui-nav-item to-index"><%= this.cUser_Name %></li>
                <li class="layui-nav-item to-index"><a href="/">系统帮助</a></li>
                <li class="layui-nav-item to-index"><a href="logoff.aspx">退出系统</a></li>
            </ul>
        </div>
        <!-- 顶部结束 -->
        <!-- 中部开始 -->
        <!-- 左侧菜单开始 -->
        <div class="left-nav">
            <div id="side-nav">
                <ul id="nav">
                    <% for (int i = 0; (drHeader != null) && (i < drHeader.Length); i++)
                        {
                            String cID = StringEx.getString(drHeader[i]["ID"]);
                            String cText = StringEx.getString(drHeader[i]["TEXT"]);
                            System.Data.DataRow[] drChild = dao.QueryMenuItem(cID);
                    %>
                    <li>
                        <a href="javascript:;">
                            <i class="iconfont">&#xe6b8;</i>
                            <cite><%=cText %></cite>
                            <i class="iconfont nav_right">&#xe697;</i>
                        </a>
                        <ul class="sub-menu">
                            <%
                                for (int j = 0; (drChild != null) && (j < drChild.Length); j++)
                                {
                                    String cChildID = StringEx.getString(drChild[j]["ID"]);
                                    String cChildText = StringEx.getString(drChild[j]["TEXT"]);
                                    String cChildUrl = StringEx.getString(drChild[j]["URL"]);
                                    Response.Write("<li>");
                                    Response.Write("<a _href='" + cChildUrl + "'>");
                                    Response.Write("    <i class='iconfont'>&#xe6a7;</i>");
                                    Response.Write("        <cite>" + cChildText + "</cite>");
                                    Response.Write("</a>");
                                    Response.Write("</li>");
                                }
                            %>
                        </ul>
                    </li>
                    <%} %>
                </ul>
            </div>
        </div>
        <!-- <div class="x-slide_left"></div> -->
        <!-- 左侧菜单结束 -->
        <!-- 右侧主体开始 -->
        <div class="page-content">
            <div class="layui-tab tab" lay-filter="xbs_tab" lay-allowclose="false">
                <ul class="layui-tab-title">
                    <li class="home"><i class="layui-icon">&#xe68e;</i>我的桌面</li>
                </ul>
                <div class="layui-tab-content">
                    <div class="layui-tab-item layui-show">
                        <iframe src='DefView.aspx' frameborder="0" scrolling="yes" class="x-iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div class="page-content-bg"></div>
        <!-- 右侧主体结束 -->
        <!-- 中部结束 -->
        <!-- 底部开始 -->
        <div class="footer">
            <div class="copyright">Copyright ©2018深圳市天龙科技有限公司 All Rights Reserved</div>
        </div>
        <!-- 底部结束 -->
        <script>
            //百度统计可去掉
            var _hmt = _hmt || [];
            (function () {
                var hm = document.createElement("script");
                hm.src = "https://hm.baidu.com/hm.js?b393d153aeb26b46e9431fabaf0f6190";
                var s = document.getElementsByTagName("script")[0];
                s.parentNode.insertBefore(hm, s);
            })();
        </script>
        <asp:HiddenField ID="orgid" runat="server" />
        <asp:HiddenField ID="usercode" runat="server" />
        <asp:HiddenField ID="token" runat="server" />
    </form>
</body>
</html>
