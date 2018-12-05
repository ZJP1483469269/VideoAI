<%@ Page Language="C#" AutoEventWireup="true" CodeFile="org_wx_config.aspx.cs" Inherits="sysweb_org_wx_config" %>

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
            var vKeyID = $("#ORG_ID").val();

            $.ajax({
                url: getAjaxUrl() + "/api/rest.ashx?action_type=OrgWXConfig&action_method=find&ORG_ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                success: function (ret) {
                    var vInfo = ret.info;
                    $("#org_id").val(vInfo.org_id);
                    $("#wx_id").val(vInfo.wx_id);
                    $("#wx_appid").val(vInfo.wx_appid);
                    $("#wx_appsecret").val(vInfo.wx_appsecret);
                    $("#wx_token").val(vInfo.wx_token);
                    $("#wx_aeskey").val(vInfo.wx_aeskey);
                    $("#isactive").val(vInfo.isactive);
                }
            });
        }

        function AjaxPost() {
            var vKeyID = $("#ORG_ID").val();
            var cValue = $('#form1').formSerialize();
            $.ajax({
                url: getAjaxUrl() + "/api/rest.ashx?action_type=OrgWXConfig&action_method=save&ORG_ID=" + vKeyID,
                dataType: "json",
                method: 'POST',
                data: cValue,
                success: function (ret) {
                    if (ret.result == 1) {
                        $("#rf").attr("src", "refresh_account.aspx");
                        alert("参数配置成功！");
                    } else {
                        alert("参数配置失败！");
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">
                系统管理-微信账号配置</h3>
        </div>
        <div class="panel-body">
            <table class="table">
                <tr>
                    <td style="width: 160px; text-align: right">
                        微信ID
                    </td>
                    <td>
                        <input type="text" id="wx_id" name="wx_id" style="width: 500px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 160px; text-align: right">
                        开发者ID
                    </td>
                    <td>
                        <input type="text" id="wx_appid" name="wx_appid" style="width: 500px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        开发者密码
                    </td>
                    <td>
                        <input type="text" id="wx_appsecret" name="wx_appsecret" style="width: 500px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        令牌
                    </td>
                    <td>
                        <input type="text" id="wx_token" name="wx_token" style="width: 500px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        消息加解密密钥
                    </td>
                    <td>
                        <input type="text" id="wx_aeskey" name="wx_aeskey" style="width: 500px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <input type="hidden" id="action_type" name="action_type" value="OrgWXConfig" />
    <input type="hidden" id="ORG_ID" name="ORG_ID" value="<%=getLoginUserInfo().ORG_ID %>" />
    <iframe name="rf" id="rf" style="display: none; width: 0px; height: 0px" frameborder="0px">
    </iframe>
    </form>
</body>
</html>
