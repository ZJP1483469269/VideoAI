<%@ Page Language="C#" AutoEventWireup="true" CodeFile="page_edit.aspx.cs" Inherits="sysweb_page_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../static/js/jquery.js"></script>
    <script type="text/javascript" src="../static/js/layer/layer.js"></script>
    <script type="text/javascript" src="../static/js/jquery.form.js"></script>
    <script type="text/javascript" src="../static/kindeditor/kindeditor.js"></script>
    <script type="text/javascript" src="../static/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../static/js/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadInitValue();
        });

        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[id="editor"]', {
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
            var vUrl = "/api/rest.ashx?action_type=Page&action_method=save";
            if (vKeyID != null) {
                vUrl = vUrl + "&PAGE_ID=" + vKeyID;
            }
            $("#page_html").val(editor.html());
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
            var vKeyID = getQueryString("PAGE_ID");
            $.ajax({
                url: "/api/rest.ashx?action_type=Page&action_method=find&PAGE_ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#org_id").val(vInfo.org_id);
                    $("#page_id").val(vInfo.page_id);
                    $("#page_name").val(vInfo.page_name);
                    editor.html(vInfo.page_html);
                    $("#isactive").val(vInfo.isactive);
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
                    <td>标题
                    </td>
                    <td>
                        <input type="text" id="page_id" name="page_id" style="width: 600px" />
                    </td>
                </tr>
                <tr>
                    <td>发布单位
                    </td>
                    <td>
                        <input type="text" id="page_name" name="page_name" style="width: 600px" />
                    </td>
                </tr>
                <tr>
                    <td>内容
                    </td>
                    <td>
                        <textarea id="editor" name="editor" style="width: 600px; height: 360px"></textarea>
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
        <asp:HiddenField ID="page_html" runat="server" />
    </form>
</body>
</html>
