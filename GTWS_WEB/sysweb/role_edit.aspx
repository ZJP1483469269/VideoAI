<%@ Page Language="C#" AutoEventWireup="true" CodeFile="role_edit.aspx.cs" Inherits="sysweb_role_edit" %>

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
            var vKeyID = getQueryString("ROLE_ID");
            $.ajax({
                url: "/api/rest.ashx?action_type=RoleInf&action_method=find&ROLE_ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#role_id").val(vInfo.role_id); 
                    $("#role_name").val(vInfo.role_name);
                    $("#org_type").val(vInfo.org_type);
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
                    <td>角色编码
                    </td>
                    <td>
                        <input type="text" id="role_id" name="role_id" />
                    </td>
                </tr>

                <tr>
                    <td>角色名称
                    </td>
                    <td>
                        <input type="text" id="role_name" name="role_name" />
                    </td>
                </tr>

                <tr>
                    <td>角色排序
                    </td>
                    <td>
                        <input type="text" id="orderby" name="orderby" />
                    </td>
                </tr>
                <tr>
                    <td>角色类型
                    </td>
                    <td>
                        <input type="text" id="org_type" name="org_type" />
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
