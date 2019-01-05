<%@ Page Language="C#" AutoEventWireup="true" CodeFile="camera_edit.aspx.cs" Inherits="camera_edit" %>

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
            var vKeyID = $("#ID").val();
            var vUrl = "/api/rest.ashx?action_type=XT_NEWS&action_method=save";
            if (vKeyID != null) {
                vUrl = vUrl + "&ID=" + vKeyID;
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
            alert(vKeyID);
            $.ajax({
                url: "/api/rest.ashx?action_type=Camera&action_method=find&ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#org_id").val(vInfo.org_id);
                    $("#camera_name").val(vInfo.camera_name);
                    $("#addr").val(vInfo.addr);
                    $("#village").val(vInfo.village);
                    $("#village_id").val(vInfo.village_id);
                    $("#unit").val(vInfo.unit);//所属乡村
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
                    <td>摄像机名称
                    </td>
                    <td>                       
                        <input type="text" id="camera_name" name="camera_name" style="width: 600px" />
                    </td>
                </tr>
                <tr>
                    <td>摄像机地址
                    </td>
                    <td>
                        <input type="text" id="addr" name="addr" style="width: 600px" />
                    </td>
                </tr>
                 <tr>
                    <td>所属乡镇
                    </td>
                    <td>
                        <input type="text" id="village" name="village" style="width: 600px" />
                    </td>
                </tr>
                <tr>
                    <td>乡镇编码
                    </td>
                    <td>
                        <input type="text" id="village_id" name="village_id" style="width: 600px" />
                    </td>
                </tr>
                <tr>
                    <td>所属乡村
                    </td>
                    <td>
                        <input type="text" id="unit" name="unit" style="width: 600px" />
                    </td>
                </tr>
                 <tr>
                    <td>坐标
                    </td>
                    <td>
                        x:<input type="text" id="x" name="x" style="width: 600px" />
                        x:<input type="text" id="y" name="y" style="width: 600px" />
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
        <asp:HiddenField ID="org_id" runat="server" />
        
    </form>
</body>
</html>
