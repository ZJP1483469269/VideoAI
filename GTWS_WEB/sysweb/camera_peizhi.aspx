<%@ Page Language="C#" AutoEventWireup="true" CodeFile="camera_peizhi.aspx.cs" Inherits="camera_peizhi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../static/js/jquery.js"></script>
    <script type="text/javascript" src="../static/js/layer/layer.js"></script>
    <script type="text/javascript" src="../static/js/jquery.form.js"></script>
    <script type="text/javascript" src="../static/js/default.js"></script>
    <script type="text/javascript" src="../static/kindeditor/kindeditor.js"></script>
    <script type="text/javascript" src="../static/kindeditor/lang/zh_CN.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            LoadInitValue();
        });

        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[id="page_html"]', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: true,
                items: [
                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'image', 'link'
                ]
            });
        });

        function AjaxPost() {
            var vKeyID = $("#device_id").val();           
            var vUrl = "/api/rest.ashx?action_type=CameraConfig&action_method=save";
            if (vKeyID != null) {
                vUrl = vUrl + "&DEVICE_ID=" + vKeyID;
                 
            }
            var cValue = $('#form1').formSerialize();
            $.ajax({
                url: getAjaxUrl() + vUrl,
                dataType: "json",
                method: 'POST',
                data: cValue,
                success: function (ret) {
                    if (ret.result == 1) {
                        alert("数据保存成功！");
                        self.parent.location.reload();
                    } else {
                        alert("数据保存失败！");
                    }
                }
            });
        }
        function LoadInitValue() {
            var vKeyID = getQueryString("ID");          
            $.ajax({
                url: "/api/rest.ashx?action_type=CameraConfig&action_method=find&ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#org_id").val(vInfo.org_id);
                    $("#offset_p").val(vInfo.offset_p);
                    $("#offset_t").val(vInfo.offset_t);
                    $("#offset_x").val(vInfo.offset_x);
                    $("#offset_h").val(vInfo.offset_h);                    
                    $("#x").val(vInfo.x); 
                    $("#y").val(vInfo.y);
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
                    <td>X坐标
                    </td>
                    <td>                       
                        <input type="text" id="x" name="x" style="width: 400px" />                        
                    </td>
                </tr>
                <tr>
                    <td>Y坐标
                    </td>
                    <td>                       
                        <input type="text" id="y" name="y" style="width: 400px" />                       
                    </td>
                </tr>
                
                 <tr>
                    <td>偏移量P
                    </td>
                    <td>
                        <input type="text" id="offset_p" name="offset_p" style="width: 400px" />
                    </td>
                </tr>
                <tr>
                    <td>偏移量T
                    </td>
                    <td>
                        <input type="text" id="offset_t" name="offset_t" style="width: 400px" />
                    </td>
                </tr>
                <tr>
                    <td>偏移量X
                    </td>
                    <td>
                        <input type="text" id="offset_x" name="offset_x" style="width: 400px" />
                    </td>
                </tr>
                 <tr>
                    <td>偏移量H
                    </td>
                    <td>
                       <input type="text" id="offset_h" name="offset_h" style="width: 400px" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center">
                        <input type="button" value="保存" onclick="AjaxPost();" />
                        <input type="button" value="关闭" onclick="AjaxClose();" />
                    </td>
                </tr>
            </table>
        </div>       
        <input type="hidden" id="device_id" name="device_id" />
        <input type="hidden" id="org_id" name="org_id" />
    </form>
</body>
</html>
