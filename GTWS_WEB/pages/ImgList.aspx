<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImgList.aspx.cs" Inherits="pages_ImgList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/static/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/static/bootstrap/bootstrap-table.min.css" rel="stylesheet" />
    <script src="/static/js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/static/bootstrap/bootstrap-table.min.js"></script>
    <script type="text/javascript" src="/static/bootstrap/bootstrap-table-zh-CN.js"></script>
    <script type="text/javascript" src="/static/js/layer/layer.js"></script>
    <script type="text/javascript" src="/static/js/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            InitGrid();
        });
s
        function InitGrid() {
            $('#DBGrid').bootstrapTable({
                url: '/api/rest.ashx?action_type=ImgList&action_method=query',         //请求后台的URL（*）
                method: 'get',             //请求方式（*）
                striped: true,              //是否显示行间隔色
                cache: false,               //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                pagination: true,           //是否显示分页（*）
                sortable: false,            //是否启用排序
                sortOrder: "asc",           //排序方式
                sidePagination: "server",   //分页方式：client客户端分页，server服务端分页（*）
                queryParams: getParams, //传递参数（*）
                pageNumber: 1,               //初始化加载第一页，默认第一页
                pageSize: 20,               //每页的记录行数（*）
                pageList: [20, 40, 60, 100], //可供选择的每页的行数（*）
                strictSearch: true,
                clickToSelect: true,         //是否启用点击选中行
                uniqueId: "id",      //每一行的唯一标识，一般为主键列
                cardView: false,            //是否显示详细视图
                detailView: false,          //是否显示父子表
                columns: [
                    {
                        field: 'id'
                        , title: '序号'
                        , align: 'center'
                        , width: '5%'
                        , formatter: function (value, row, index) {
                            return index + 1;
                        }
                    },
                    {
                        field: 'camera_addr'
                        , title: '摄像机地址'
                        , align: 'center'
                        , width: '30%'
                    },
                   {
                       field: 'createtime'
                        , title: '拍照时间'
                        , align: 'center'
                        , width: '30%'
                        , formatter: function (value, row, index) {
                            return getDayTimeValue(value, "yyyy-MM-dd HH:mm:ss");
                        }
                   },
                   {
                       field: 'url'
                        , title: '拍摄图片'
                        , align: 'center'
                        , width: '20%'
                         , formatter: function (value, row, index) {
                             var ab = "";
                             ab = "<a  href='" + value + "'><img  src='" + value + "' width ='32px' height = '24px'></a>";
                             return ab;
                         }
                   },
                   {
                       field: ''
                        , title: '操作'
                        , align: 'center'
                         , width: '15%'
                        , formatter: function (value, row, index) {
                            var cStr = "<a href='#' onclick='Modify_Event(\"" + row.id + "\");'>查看大图</a>";
                            return cStr;
                        }
                   }
                ]
            });
        }

        function Modify_Event(cKeyID) {
            AjaxOpenDialog('', "BlueSky_Jb.aspx?ID=" + cKeyID, "1000px", "840px");
        }
       
        var getParams = function (params) {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                limit: params.limit, //页面大小
                offset: params.offset, //页码
                maxrows: params.limit,
                pageindex: params.pageNumber
            };
            return temp;
        };

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="panel-body">
        <table id="DBGrid">
        </table>
    </div>
    </form>
</body>
</html>
