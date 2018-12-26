<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultPage.aspx.cs" Inherits="DefaultPage" %>

<%@ Register Src="widget/header.ascx" TagName="header" TagPrefix="uc1" %>

<%@ Register Src="widget/footer.ascx" TagName="footer" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title> 
    <link href="widget/div.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="banner"></div>
            <div>
                <uc1:header ID="header1" runat="server" />
            </div>
            <div>
                <uc2:footer ID="footer1" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
