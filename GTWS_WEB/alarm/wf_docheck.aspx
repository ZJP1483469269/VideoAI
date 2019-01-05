<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wf_docheck.aspx.cs" Inherits="wf_docheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/static/js/jquery.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/jquery.form.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
    <script>
        $(document).ready(function () {
            LoadInitValue();
        });
        function LoadInitValue() {
            var vKeyID = getQueryString("REC_ID");            
            var vUrl = "/api/rest.ashx?action_type=XT_IMG_REC&action_method=find";
            if (vKeyID != null) {
                vUrl = vUrl + "&REC_ID=" + vKeyID;
            }
            $.ajax({
                url: vUrl,
                dataType: "json",                                  
                method: 'POST',
                success: function (ret) {
                    console.log(ret);
                    var vInfo = ret.rows[0];
                    $("#camera_name").val(vInfo.camera_name);
                    $("#addr").val(vInfo.addr);
                    $("#x").val(vInfo.x);
                    $("#y").val(vInfo.y);
                }
            });
        }

        function AjaxPost() {
            var Ccamera_name = $("#camera_name").val();
            var Caddr = $("#addr").val();
            var Cx = $("#x").val();
            var Cy = $("#y").val();
            var vUrl = "/api/rest.ashx?action_type=XT_IMG_REC&action_method=save&CAMERA_NAME=" + Ccamera_name + "&ADDR=" + Caddr + "&X=" + Cx + "&Y=" + Cy;
            var cValue = $('#form1').formSerialize();
            $.ajax({
                url:vUrl,
                dataType: "json",
                method: 'POST',
                data: cValue,
                success: function (vret) {
                      if (vret.result == 1) {
                        alert("交办成功");
                      }       
                },
                error: function (vret) {
                    alert("error");
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="DBGrid" class="table">
                <tr>
                    <td style="text-align: right">
                        摄像机名称：
                    </td>
                    <td>
                        <input type="text" id="camera_name" name="camera_name" disabled="true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        摄像机地址：
                    </td>
                    <td>
                        <input type="text" id="addr" name="addr" disabled="true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        摄像机坐标：
                    </td>
                    <td>
                        X：<input type="text" id="x" name="x" disabled="true" />
                        y：<input type="text" id="y" name="y" disabled="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                      <input type="button" value="交办" onclick="AjaxPost();" />
                      <input type="button" value="关闭" onclick="AjaxClose();" />
                   </td>
               </tr>
            </table>
        </div>
    </form>
</body>
</html>
