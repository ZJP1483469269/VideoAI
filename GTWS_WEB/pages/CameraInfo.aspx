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
            LoadItem();
        });

        function LoadItem() {
            var vKeyID = getQueryString("ID");
            $.ajax({
                url: "/api/rest.ashx?action_type=Camera&action_method=find&ID=" + vKeyID,
                dataType: "json",
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#county").text(vInfo.county);
                    $("#village").text(vInfo.village);
                    $("#addr").text(vInfo.addr);
                }
            });
        }
    </script>
</head>
<body>
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    所属县区
                </td>
                <td>
                    <span id="county"></span>
                </td>
            </tr>
            <tr>
                <td>
                    所属乡镇
                </td>
                <td>
                    <span id="village"></span>
                </td>
            </tr>
            <tr>
                <td>
                    详细地址
                </td>
                <td>
                    <span id="addr"></span>
                </td>
            </tr>
            <tr>
                <td>
                    完成情况
                </td>
                <td>
                    <span id="is_complete"></span>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
