<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../static/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../static/js/jquery.js" type="text/javascript"></script>
    <script src="../static/js/layer/layer.js" type="text/javascript"></script>
    <script src="../static/js/default.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var vKeyID = getQueryString("ID");
            $("#ViewInfo").load("CameraInfo.aspx?ID=" + vKeyID);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100px">
            <tr>
                <td>
                    <div id="ViewInfo">
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    时间段
                </td>
                <td>
                    <input type="text" id="START_DAY" name="START_DAY" />
                    <input type="text" id="FINISH_DAY" name="FINISH_DAY" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <div style="width: 795px; height: 470px; background-color: Black; margin: 1px">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="text-align: center">
                        <input type="button" value="播放" />
                        <input type="button" value="停止" />
                        <input type="button" value="选取开始位置" />
                        <input type="button" value="选取结束位置" />
                        <input type="button" value="截取" />
                        <input type="button" value="关闭" onclick="AjaxClose();" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
