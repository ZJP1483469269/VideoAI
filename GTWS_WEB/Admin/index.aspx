<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Admin_index" %>

<%@ Register Src="top.ascx" TagName="top" TagPrefix="uc_top" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="/static/js/jquery.js" type="text/javascript"></script>
    <script src="/static/js/work/index.js" type="text/javascript"></script>
    <script type="text/javascript">
        function oa_tool() {
            var vtop = 0;
            if (parent.mainFrame.rows == "180,*") {
                parent.mainFrame.rows = "60,*";
                $("#topBg,#userInfo").hide();
                $("#tool").attr("src", "../images/top/2.gif");
                $("#tool").attr("alt", "显示顶部背景");
            } else {
                $("#tool").attr("src", "../images/top/1.gif");
                $("#tool").attr("alt", "隐藏顶部背景");
                $("#topBg,#userInfo").show();
                parent.mainFrame.rows = "180,*";
            }
            try {
                //parent.workFrame.cmdresize();
                // alert(0);
                parent.workFrame.changeDiv();
            }
            catch (ex) {

            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc_top:top ID="top" runat="server" />
    </div>
    <div style="position: absolute; left: 1px; right: 1px; top: 180px; bottom: 1px; z-index: 99">
        <div id="div_left" style="position: absolute; left: 0px; top: 0px; bottom: 2px">
            <iframe id="rf_left" name="rf_left" style='width: 180px; height: 100%' frameborder="0">
            </iframe>
        </div>
        <div id="div_right" style="position: absolute; left: 190px; top: 0px; bottom: 2px;
            right: 0px">
            <iframe id="rf_right" name="rf_right" style='width: 100%; height: 100%' frameborder="0">
            </iframe>
        </div>
        <div id="div_main" style="position: absolute; left: 0px; top: 0px; bottom: 2px; right: 0px">
            <iframe id="rf_main" name="rf_main" style='width: 100%; height: 100%' frameborder="0">
            </iframe>
        </div>
    </div>
    <asp:HiddenField ID="token" runat="server" />
    <asp:HiddenField ID="usercode" runat="server" />
    <asp:HiddenField ID="orgid" runat="server" />
    </form>
</body>
</html>
