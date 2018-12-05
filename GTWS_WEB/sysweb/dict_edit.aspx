<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dict_edit.aspx.cs" Inherits="sysweb_dict_edit" %>

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
            var vKeyID = getQueryString("ORG_ID");
            $.ajax({
                url: "/api/rest.ashx?action_type=OrgInf&action_method=find&ORG_ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#org_id").val(vInfo.org_id);
                    $("#org_name").val(vInfo.org_name);
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
                <td>
                    单位编码
                </td>
                <td>
                    <input type="text" id="org_id" name="org_id" />
                </td>
            </tr>
            <tr>
                <td>
                    单位名称
                </td>
                <td>
                    <input type="text" id="org_name" name="org_name" />
                </td>
            </tr>
            <tr>
                <td>
                    单位名称
                </td>
                <td>
                    <input type="text" id="org_full_name" name="org_full_name" />
                </td>
            </tr>
            <tr>
                <td>
                    单位排序
                </td>
                <td>
                    <input type="text" id="orderby" name="orderby" />
                </td>
            </tr>
            <tr>
                <td>
                    状态
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
