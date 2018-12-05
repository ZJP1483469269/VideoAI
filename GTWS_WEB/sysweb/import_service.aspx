<%@ Page Language="C#" AutoEventWireup="true" CodeFile="import_service.aspx.cs" Inherits="import_service" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/static/bootstrap/bootstrap-table.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/static/js/jquery.js"></script>
    <script type="text/javascript" src="/static/bootstrap/bootstrap-table.min.js"></script>
    <script type="text/javascript" src="/static/bootstrap/bootstrap-table-zh-CN.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">
                数据服务-数据导入</h3>
        </div>
        <div class="panel-body">
            <table style="width: 98%" class="table">
                <tr>
                    <td>
                        选择文件
                    </td>
                    <td>
                        <asp:FileUpload ID="Upload" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnImport" runat="server" Text="导入举报结果" OnClick="btnImport_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <input type="hidden" id="TABLE_ID" name="TABLE_ID" value="101" />
    </form>
</body>
</html>
