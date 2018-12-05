﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="org_list.aspx.cs" Inherits="sysweb_org_list" %>

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
                url: '/api/rest.ashx?action_type=OrgInf&action_method=query',         //请求后台的URL（*）
                method: 'get',             //请求方式（*）
                striped: true,              //是否显示行间隔色
                cache: false,               //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: true,           //是否显示分页（*）
                sortable: false,            //是否启用排序
                sortOrder: "asc",           //排序方式
                sidePagination: "server",   //分页方式：client客户端分页，server服务端分页（*）
                queryParams: getParams, //传递参数（*）
                pageNumber: 1,               //初始化加载第一页，默认第一页
                pageSize: 15,               //每页的记录行数（*）
                pageList: [15, 30, 50, 100], //可供选择的每页的行数（*）
                strictSearch: true,
                clickToSelect: true,         //是否启用点击选中行
                uniqueId: "org_id",      //每一行的唯一标识，一般为主键列
                cardView: false,            //是否显示详细视图
                detailView: false,          //是否显示父子表
                columns: [
                    {
                        field: 'org_id'
                        , title: '序号'
                        , align: 'center'
                        , formatter: function (value, row, index) {
                            return index + 1;
                        }
                    },
                    {
                        field: 'org_id',
                        align: 'center'
                        , title: '单位编码'
                    },

                    {
                        field: 'org_name'
                        , title: '单位简称'
                        , align: 'center'
                    },
                   {
                       field: 'org_full_name'
                        , title: '单位全称'
                        , align: 'center'
                   },
                  {
                      field: 'x'
                                , title: '中心坐标'
                                , align: 'center'
                                 , formatter: function (value, row, index) {
                                     return "(" + row.x + "," + row.y + ")";
                                 }
                  },

                         {
                             field: 'isactive'
                        , title: '状态'
                        , align: 'center'
                       , formatter: function (value, row, index) {
                           if (value == '1') {
                               return "有效";
                           } else {
                               return "无效";
                           }
                       }
                         },
                    {
                        field: ''
                        , title: '操作'
                        , align: 'center'
                        , formatter: function (value, row, index) {
                            var cStr = "<a href='#' onclick='Modify_Event(" + row.org_id + ");'>修改</a>";
                            cStr = cStr + "&nbsp;&nbsp;<a href='#' data-val='" + row.org_id + "' onclick='Event_DEL_Item(this);'>删除</a>";
                            return cStr;
                        }
                    }
                ]
            });
        }

        function Modify_Event(cOrgID) {
            AjaxOpenDialog('修改单位信息', "org_edit.aspx?ID=" + cOrgID, "800px", "640px");
        }

        var getParams = function (params) {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                limit: params.limit, //页面大小
                offset: params.offset, //页码
                maxrows: params.limit,
                pageindex: params.pageNumber,
                ORG_ID: "<%=ORG_ID %>"
            };
            return temp;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">
                系统管理-单位管理</h3>
        </div>
        <div>
            <input type="button" value="添加" onclick="AjaxOpenDialog('添加单位信息','org_edit.aspx','800px','650px');" />
        </div>
        <div class="panel-body">
            <table id="DBGrid">
            </table>
        </div>
    </div>
    <input type="hidden" id="action_type" name="action_type" value="OrgInf" />
    </form>
</body>
</html>
