<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mobile_user_edit.aspx.cs"
    Inherits="mobile_user_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/static/js/jquery.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/jquery.form.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadInitValue();
        });

        function LoadInitValue() {
            var vKeyID = getQueryString("USER_COUNT");
            var vUrl = "/api/rest.ashx?action_type=MOBILE_USER&action_method=find";
            if (vKeyID != null) {
                $("#ID").val(vKeyID);
                vUrl = vUrl + "&ID=" + vKeyID;
            }
            $.ajax({
                url: vUrl,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info; 
                    $("#phone_num").val(vInfo.phone_num);
                    $("#org_id").val(vInfo.org_id);
                    $("#user_name").val(vInfo.user_name);
                    $("#is_active").val(vInfo.is_active);
                }
            });
        }

        function AjaxPost() {
            var vKeyID = $("#ID").val();
            var vUrl = "/api/rest.ashx?action_type=MOBILE_USER&action_method=save";
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="DBGrid" class="table">           
            <tr>
                <td style="text-align: right">
                    手机号码
                </td>
                <td>
                    <input type="text" id="phone_num" name="phone_num" />
                </td>
            </tr>
                <tr>
                <td style="text-align: right">
                    用户姓名
                </td>
                <td>
                    <input type="text" id="user_name" name="user_name" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    用户密码
                </td>
                <td>
                    <input type="password" id="pass_word" name="pass_word" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    所属乡镇
                </td>
                <td>
                    <asp:DropDownList ID="org_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    状态
                </td>
                <td>
                    <select id="is_active" name="is_active">
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
    <input type="hidden" id="ID" name="ID" />
    <asp:HiddenField ID="AREA_ID" runat="server" />
    </form>
</body>
</html>
