<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="Admin_left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="/static/js/jquery.js" type="text/javascript"></script>
    <script src="/static/js/work/index.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var cDefUrl = $("#DEF_URL").val();
            var cDefID = $("#DEF_ID").val();
            self.parent.goNavPage(cDefUrl, cDefID);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%
                String cDefID = "";
                String cDefUrl = "";
            %>
            <ul class="nav nav-pills nav-stacked">
                <% 
                    for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
                    {
                        String cID = StringEx.getString(dtRows, i, "id");
                        String cUrl = StringEx.getString(dtRows, i, "url");
                        if (cUrl.IndexOf("?") == -1)
                        {
                            cUrl = cUrl + "?ID=" + cID;
                        }
                        else
                        {
                            cUrl = cUrl + "&ID=" + cID;
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
                %>
                <li><a href="<%=cUrl %>" target="<%=cTarget %>">
                    <%= cText%></a></li>
                <%} %>
            </ul>
        </div>
        <input type="hidden" id="DEF_URL" value="<%=cDefUrl %>" />
        <input type="hidden" id="DEF_ID" value="<%=cDefID %>" />
    </form>
    <script type="text/javascript">
        
    </script>
</body>
</html>
