<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageOnline.aspx.cs" Inherits="alarm_ImageOnline" %>

<%@ Import Namespace="TLKJ.Utils" %>
<%@ Import Namespace="TLKJ.DB" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container" id="left_div">
                <% 
                  
                %>
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
                <script language="javascript">
                    setTimeout("ReloadImageList", 1000 * 60);
                </script>
            </div>
        </div>
    </form>
</body>
</html>
