<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="Admin_help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function doGetClick() {
            var obj = new Object();
            obj.id = 1;
            obj.url = "http://www.baidu.com/";
            var vMSG = eval(obj);
            alert(vMSG);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="Button1" type="button" value="button" onclick="doGetClick();" />
    </div>
    </form>
</body>
</html>
