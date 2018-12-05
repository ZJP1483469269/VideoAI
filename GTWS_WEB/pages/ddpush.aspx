<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ddpush.aspx.cs" Inherits="pages_ddpush" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="SERVER_HOST" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="SERVER_PORT" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="UID" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="MSG" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnSend" runat="server" Text="发送" OnClick="btnSend_Click" />
        </div>
    </form>
</body>
</html>
