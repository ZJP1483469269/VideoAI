<%@ Page Language="C#" AutoEventWireup="true" CodeFile="page_dict_edit.aspx.cs" Inherits="sysweb_page_dict_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../static/js/jquery.js"></script>
    <script type="text/javascript" src="../static/js/layer/layer.js"></script>
    <script type="text/javascript" src="../static/js/jquery.form.js"></script>
    <script type="text/javascript" src="../static/js/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadInitValue();
        });

        function AjaxPost() {
            var vKeyID = $("#ID").val();
            var vUrl = "/api/rest.ashx?action_type=PageDict&action_method=save";
            if (vKeyID != null) {
                vUrl = vUrl + "&DICT_ID=" + vKeyID;
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
            var vKeyID = getQueryString("DICT_ID");
            $.ajax({
                url: "/api/rest.ashx?action_type=PageDict&action_method=find&DICT_ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#dict_id").val(vInfo.dict_id);
                    $("#dict_name").val(vInfo.dict_name);
                    $("#sql_text").val(vInfo.sql_text);  
                    $("#org_id").val(vInfo.org_id);  
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
                        <input type="text" id="dict_id" name="dict_id" style="width: 600px" />
                    </td>
                </tr>
                <tr>
                    <td>发布单位
                    </td>
                    <td>
                        <input type="text" id="dict_name" name="dict_name" style="width: 600px" />
                    </td>
                </tr>
                <tr>
                    <td>SQL语句
                    </td>
                    <td>
                        <input type="text" id="sql_text" name="sql_text" style="width: 600px" />
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
