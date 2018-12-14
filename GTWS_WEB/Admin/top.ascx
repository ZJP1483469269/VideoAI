<%@ Control Language="C#" AutoEventWireup="true" CodeFile="top.ascx.cs" Inherits="Admin_top" %>
<%@ Import Namespace="TLKJ.Utils" %>
<script type="text/javascript">
    $(function () {
        $(".imgPlayer2").imgPlayer({
            iconOpacity: '0.7', 	 //图标显示的透明度
            activeIconOpacity: '0.7', //激活时图标显示的透明度
            iconColor: '#fff',  //图标的颜色
            iconFontColor: '#fff', //图标字体的颜色
            activeIconColor: '#e70012'//激活时图标的颜色
        });
    })
</script>
<div id="main" align="center" style="height: 170px; overflow: hidden;">
    <div id="topBg" style="height: 119px; background: url(/static/images/top/topBg.png) no-repeat;">
        <div style="position: absolute; top: 30px; left: 20px; z-index: 9999;">
            <img id="imgLogo" src="/static/images/top/864114_header.png" style="border-width: 0px;" />
        </div>
        <div style="position: absolute; top: 0px; right: 0px; color: #fff; padding: 0px 0px;
            color: #000" id="divChange">
            <div class="imgPlayer2">
                <img src="../images/top/tu001.png" />
                <img src="../images/top/tu002.png" />
                <img src="../images/top/tu004.png" />
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <%
        String cDefID = "";
        String cDefUrl = "";
    %>
    <div style="width: 100%; background-color: #6C6B69; text-align: left;">
        <table style="width: 100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #6C6B69;
                        height: 50px;" id="nav">
                        <tr style="height: 50px;">
                            <% for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
                               {
                                   String cID = StringEx.getString(dtRows, i, "id");
                                   String cUrl = StringEx.getString(dtRows, i, "url");
                                   if (cUrl.IndexOf("?") == -1)
                                   {
                                       cUrl = cUrl + "?TYPE_ID=" + cID;
                                   }
                                   else
                                   {
                                       cUrl = cUrl + "&TYPE_ID=" + cID;
                                   }
                                   if (cDefUrl.Length == 0)
                                   {
                                       cDefUrl = cUrl;
                                   }
                                   String cText = StringEx.getString(dtRows, i, "text");
                                   String cIMG = StringEx.getString(dtRows, i, "img");
                                   String cTarget = StringEx.getString(dtRows, i, "target");
                                   if (cDefID.Length == 0)
                                   {
                                       cDefID = cTarget;
                                   }

                                   String cWidth = "150px";
                                   if (cID.Equals("10"))
                                   {
                                       cWidth = "80px";
                                   }
                            %>
                            <td id='key_<%=cID %>' onclick="goNavPage('<%=cUrl %>','<%=cTarget %>')" style='width: <%=cWidth%>;
                                text-align: center; height: 50px; background: #237EA9; color: #fff; cursor: pointer;
                                font-size: 25px;'>
                                <div style='width: <%=cWidth%>; border-right: 2px solid #AFAFAF; padding: 0px 10px 0px 10px;'>
                                    <%=cText%></div>
                            </td>
                            <%} %>
                        </tr>
                    </table>
                </td>
                <td nowrap="nowrap" style="color: Black; padding-left: 5px; height: 35px; padding-top: 1px;"
                    align="right">
                    <img id="tool" style="border: 0px; cursor: pointer; width: 35px; height: 35px;" src="/static/images/top/1.gif"
                        alt="隐藏顶部背景" onclick="oa_tool()" />
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</div>
<div id="userInfo" style="position: absolute; top: 5px; right: 5px; font-size: 12px;
    color: #fff; padding: 3px 3px; color: #000">
    <table>
        <tr>
            <td>
                <span style="font-weight: bold">单位:
                    <%= this.cOrg_Full_Name %>&nbsp;&nbsp; 用户:<%= this.cUser_Name %></span>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <a href="logoff.aspx" onclick="return confirm('确定退出系统吗？')" style="color: #505050;
                    font-size: 14px; cursor: pointer; color: Red; text-decoration: none">[注销]</a>
                <a href="help.aspx" target="_blank" style="color: #505050; font-size: 14px; cursor: pointer;
                    color: Red; text-decoration: none">[帮助]</a>
            </td>
        </tr>
    </table>
    <input type="hidden" id="DEF_URL" value="<%=cDefUrl %>" />
    <input type="hidden" id="DEF_ID" value="<%=cDefID %>" />
    <script type="text/javascript">
        $(document).ready(function () {
            var cDefUrl = $("#DEF_URL").val();
            var cDefID = $("#DEF_ID").val();
            goNavPage(cDefUrl, cDefID);
        });
    </script>
</div>
