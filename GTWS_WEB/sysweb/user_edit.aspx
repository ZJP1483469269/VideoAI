<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_edit.aspx.cs" Inherits="sysweb_user_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/static/js/jquery.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadInitValue();
        });

        function LoadInitValue() {
            var vKeyID = getQueryString("USER_ID");
            $.ajax({
                url: "/api/rest.ashx?action_type=UserInf&action_method=find&USER_ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#org_id").val(vInfo.org_id);
                    $("#user_id").val(vInfo.user_id);
                    $("#user_code").val(vInfo.user_code);
                    $("#user_name").val(vInfo.user_name);
                    $("#org_full_name").val(vInfo.org_full_name);
                    $("#orderby").val(vInfo.orderby);
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
                    <td>用户编码
                    </td>
                    <td>
                        <input type="text" id="user_id" name="user_id" />
                    </td>
                </tr>
                <tr>
                    <td>登录账号
                    </td>
                    <td>
                        <input type="text" id="user_code" name="user_code" />
                    </td>
                </tr>
                <tr>
                    <td>用户名称
                    </td>
                    <td>
                        <input type="text" id="user_name" name="user_name" />
                    </td>
                </tr>
                <tr>
                    <td>用户角色
                    </td>
                    <td>
                        <select id="role_id" name="role_id"></select>
                    </td>
                </tr>
                <tr>
                    <td>手机号码
                    </td>
                    <td>
                        <input type="text" id="phone_telno" name="phone_telno" />
                    </td>
                </tr>
                <tr>
                    <td>手机号码
                    </td>
                    <td>
                        <input type="text" id="remark" name="remark" />
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
    </form>
</body>
</html>
