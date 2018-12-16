<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="alarm_Default" %>

<%@ Import Namespace="TLKJ.Utils" %>
<%@ Import Namespace="TLKJ.DB" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../static/css/bootstrap.css" />
    <script type="text/javascript" src="../static/js/jquery-3.2.0.min.js"></script>
    <script type="text/javascript" src="../static/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../static/js/layer/layer.js"></script>
    <script type="text/javascript" src="../static/js/default.js"></script>
    <style>
        .container {
            width: 100%;
            height: 500px;
        }

        .left {
            float: left;
            width: 200px;
            height: 500px;
        }

        .right {
            float: right;
            height: 500px;
            width: calc(100% - 200px);
        }

        .abc {
            float: right;
            width: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="abc">
            &nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" onclick="POLICE()">报警</a>&nbsp;&nbsp;&nbsp;
    <a href="javascript:void(0);" onclick="ESC()">取消报警</a>
        </div>
        <div class="left" id="left_div">
            <%
                System.Data.DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_CAMERA ");
            %>
            <table class="table-bordered">
                <% for (int i = 0; i < dtRows.Rows.Count; i++)
                    {%>
                <tr>
                    <td>
                        <a href="ImageList.aspx?DEVICE_ID=<%= StringEx.getString(dtRows,i,"device_id") %>"
                            target="rf_right">
                            <%= StringEx.getString(dtRows,i,"addr") %>
                        </a>
                    </td>
                </tr>
                <%} %>
            </table>
        </div>
        <div class="right" id="right_div">
            <iframe id="rf_right" name="rf_right" style="width: 100%; height: 100%; margin-left: 0px; margin-right: 0px"
                frameborder="0px"></iframe>
        </div>
    </form>
    </form>
</body>
</html>
