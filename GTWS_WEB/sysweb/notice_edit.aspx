<%@ Page Language="C#" AutoEventWireup="true" CodeFile="notice_edit.aspx.cs" Inherits="notice_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../static/js/jquery.js"></script>
    <script type="text/javascript" src="../static/js/layer/layer.js"></script>
    <script type="text/javascript" src="../static/js/default.js"></script>
    <script type="text/javascript" src="../static/kindeditor/kindeditor.js"></script>
    <script type="text/javascript" src="../static/kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../static/js/jquery.form.js"></script>
    <script type="text/javascript" src="../static/My97DatePicker/WdatePicker.js"></script>
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
            var vUrl = "/api/rest.ashx?action_type=Notice&action_method=save";
            if (vKeyID != null) {
                vUrl = vUrl + "&ID=" + vKeyID;
            }
            $("#notice_content").val(editor.html());
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
                url: "/api/rest.ashx?action_type=Notice&action_method=find&ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#notice_id").val(vInfo.notice_id);
                    $("#notice_title").val(vInfo.notice_title);
                    $("#notice_content").val(vInfo.notice_content);
                    $("#notice_date").val(vInfo.notice_date);
                    $("#page_html").val(vInfo.notice_content);
                    $("#isactive").val(vInfo.isactive);
                    editor.html(vInfo.notice_content);
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
                        <asp:TextBox ID="notice_title" runat="server" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>发布时间
                    </td>
                    <td>
                        <asp:TextBox ID="notice_date" runat="server" Width="600px" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>内容
                    </td>
                    <td>
                        <asp:TextBox ID="page_html" runat="server" TextMode="MultiLine" Style="width: 600px; height: 360px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>状态
                    </td>
                    <td>
                        <select id="isactive" name="isactive">
                            <option value="1">有效</option>
                            <option value="0">无效</option>
                        </select>
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
        <asp:HiddenField ID="notice_id" runat="server" />
        <asp:HiddenField ID="notice_content" runat="server" />

        <asp:HiddenField ID="ORG_ID" runat="server" />
    </form>
</body>
</html>
