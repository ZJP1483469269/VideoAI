<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="alarm_Default" %>

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
    <script type="text/javascript">
        $(document).ready(function () {
            InitGrid();
        });
        function InitGrid() {
            $('#DBGrid').bootstrapTable({
                url: '/api/rest.ashx?action_type=XT_IMG_REC&action_method=query_alarm',         //请求后台的URL（*）
                method: 'get',             //请求方式（*）
                striped: true,              //是否显示行间隔色
                cache: false,               //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: true,           //是否显示分页（*）
                sortable: false,            //是否启用排序
                sortOrder: "asc",           //排序方式
                sidePagination: "server",   //分页方式：client客户端分页，server服务端分页（*）
                queryParams: getParams, //传递参数（*）
                pageNumber: 1,               //初始化加载第一页，默认第一页
                pageSize: 5,               //每页的记录行数（*）
                pageList: [15, 30, 50, 100], //可供选择的每页的行数（*）
                strictSearch: true,
                clickToSelect: true,         //是否启用点击选中行
                uniqueId: "rec_id",      //每一行的唯一标识，一般为主键列
                cardView: false,            //是否显示详细视图
                detailView: false,          //是否显示父子表
                columns: [
                    {
                        field: 'rec_id'
                        , title: '序号'
                        , align: 'center'
                        , formatter: function (value, row, index) {
                            return index + 1;
                        }
                    },
                    {
                        field: 'addr',
                        align: 'center'
                        , title: '地址'
                    },

                    {
                        field: 'preset_id'
                        , title: '预制位'
                        , align: 'center'
                    },
                    {
                        field: 'create_time'
                        , title: '报警时间'
                        , align: 'center'
                        , formatter: function (value, row, index) {
                            return getDayTime(value);
                        }
                    },
                    {
                        field: 'p'
                        , title: 'P'
                        , align: 'center'
                    },
                    {
                        field: 't'
                        , title: 'T'
                        , align: 'center'
                    },
                    {
                        field: 'x'
                        , title: 'X'
                        , align: 'center'
                    },
                    {
                        field: 'alarm_checked'
                        , title: '处理状态'
                        , align: 'center'
                        , formatter: function (value, row, index) {
                            if (value == '1') {
                                return "已处理";
                            } else {
                                return "未处理";
                            }
                        }
                    },
                    {
                        field: ''
                        , title: '操作'
                        , align: 'center'
                        , formatter: function (value, row, index) {
                            var cStr = "<a href='#' onclick='LoadList(\"" + row.rec_id + "\");'>查看</a>";
                            cStr = cStr + "  <a href='#' onclick='Modify_Event(" + row.device_id + ");'>定位</a>";
                            return cStr;
                        }
                    }
                ]
            });
        }

        function Modify_Event(cOrgID) {
            AjaxOpenDialog('修改字典信息', "org_edit.aspx?ORG_ID=" + cOrgID, "640px", "480px");
        }

        var getParams = function (params) {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                limit: params.limit, //页面大小
                offset: params.offset, //页码
                maxrows: params.limit,
                pageindex: params.pageNumber,
                ORG_ID: "<%=getLoginUserInfo().ORG_ID %>",
                ADDR: $("#ADDR").val(),
                ALARM_CHECKED: $("#ALARM_CHECKED").val(),
            };
            return temp;
        };

        function LoadList(cREC_ID) {
            $("#IMAGE_LIST").empty();
            var vUrl = "/api/alarm.ashx?action_type=XT_IMG_REC&action_method=query_list";
            var cValue = "REC_ID=" + cREC_ID;
            $.ajax({
                type: "POST",
                url: vUrl,
                dataType: "json",
                cache: false,
                data: cValue + "&no-cache=" + Math.round(Math.random() * 10000),
                success: function (vret) {
                    if (vret.result == 1) {
                        var rs = vret.rows;
                        for (var i = 0; rs.length; i++) {
                            var rowKey = rs[i];
                            var vImageUrl = "<img src='" + rowKey.file_url + "' width= '300px' height='200px'>";
                            $("#IMAGE_LIST").append("<td>" + vImageUrl + "</td>");
                        }
                    }
                }
            });
        }
        function doQuery() {

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">系统管理-报警管理</h3>
            </div>
            <div>
                <table>
                    <tr>
                        <td>摄像机
                        </td>
                        <td>
                            <input type="text" id="ADDR" name="ADDR" class="form-control" style="width: 300px; height: 28px" />
                        </td>
                        <td>状态
                        </td>
                        <td>
                            <select class="form-control" id="ALARM_CHECKED" name="ALARM_CHECKED" style="width: 200px">
                                <option value="1">已处理</option>
                                <option value="">--全部--</option>
                                <option value="0">未处理</option>
                            </select>
                        </td>
                        <td>
                            <input type="button" value="查询" class="btn-sm btn-info" onclick="doQuery();" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="panel-body">
                <table id="DBGrid">
                </table>
            </div>
        </div>
        <table class="panel panel-primary">
            <tr id="IMAGE_LIST">
            </tr>
        </table>
        <input type="hidden" id="action_type" name="action_type" value="XT_IMG_REC" />
    </form>
</body>
</html>
