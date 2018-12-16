<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageList.aspx.cs" Inherits="alarm_ImageList" %>

<%@ Import Namespace="TLKJ.Utils" %>
<%@ Import Namespace="TLKJ.DB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../static/css/bootstrap.css" />
    <script type="text/javascript" src="../static/js/jquery-3.2.0.min.js"></script>
    <script type="text/javascript" src="../static/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../static/js/layer/layer.js"></script>
    <script type="text/javascript" src="../static/js/default.js"></script>
    <title></title>
    <style>
        .container {
            width: 100%;
            height: calc(100% - 130px);
        }
    </style>
    <script type="text/javascript">
        function ReloadImageList() {
            self.window.location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container" id="left_div">
                <div>
                    <% for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
                        {
                            String cFileUrl = StringEx.getString(dtRows, i, "FILE_URL");
                            String cAddr = StringEx.getString(dtRows, i, "ADDR");
                    %>
                    <a href="<%= cFileUrl%>" target="_blank">
                        <img src='<%= cFileUrl%>' style="width: 180px; height: 180px" />
                    </a>
                    <%} %>
                </div>
                <a href="ImageOnline.aspx">切换自动刷新</a>
            </div>
        </div>
    </form>
</body>
</html>
