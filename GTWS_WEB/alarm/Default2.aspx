<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="alarm_Default2" %>

<%@ Import Namespace="TLKJ.Utils" %>
<%@ Import Namespace="TLKJ.DB" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../static/css/bootstrap.css" />
    <link rel="stylesheet" href="../static/css/demo.css" type="text/css"/>
    <link rel="stylesheet" href="../static/zTree/css/zTreeStyle/zTreeStyle.css" type="text/css"/>
    <script type="text/javascript" src="../static/js/jquery-3.2.0.min.js"></script>
    <script type="text/javascript" src="../static/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../static/js/layer/layer.js"></script>
    <script type="text/javascript" src="../static/js/default.js"></script>
    <script type="text/javascript" src="../static/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../static/js/jquery.ztree.core.js"></script>
    <style>
        .container {
            width: 100%;
            height: 500px;
        }

        .left {
            float: left;
            width: 200px;
            height: 500px;
        }

        .right {
            float: right;
            height: 500px;
            width: calc(100% - 200px);
        }

        .abc {
            float: right;
            width: 200px;
        }
    </style>
    <script type="text/javascript">
        var zTreeObj;
        var zCounty=new Array();
        var zViliage = new Array();
        var vKeyID = getOrg_ID();
        var zNodes = [];
        var vStr = '';
        var coun = '';
        var z = [{name:"董村镇"},{name:"石固镇"},{name:""},{name:"后河镇"}];
        $.ajax({
            url: "/api/rest.ashx?action_type=XT_CAMERA&action_method=query&ORG_ID=" + vKeyID,
            dataType: "json",
            method: 'POST',
            success: function (ret) {
                if (ret.result == 1) {
                    var vInfo = ret.rows;
                    for (var i = 0; i < vInfo.length; i++) {
                        vStr = vStr + vInfo[i].village + ',';                        
                    }                   
                    vStr = (vStr.slice(vStr.length - 1) == ',') ? vStr.slice(0, -1) : vStr;
                    zNodes = vStr.split(",");
                    for (var a = 0; a < zNodes.length; a++) {
                        zCounty.push("{name:" + zNodes[a] + "}");
                    }
                    //zNodes.push(vStr);
                    console.log(zCounty);
                } else {
                    alert(error);
                }
            }
        });
        
        // zTree 的参数配置，深入使用请参考 API 文档（setting 配置详解）
        var setting = {
            view: {
                nameIsHTML: true
            },
            data: {
                simpleData: {
                    enable: true
                },
                keep: {
                    parent: true
                }
            },
            open: false,
            callback: {
                onClick: "",
                onExpand: function (event, treeId, treeNode) {
                    //addSubNode(treeNode);
                }
            } 
        };
        // zTree 的数据属性，深入使用请参考 API 文档（zTreeNode 节点数据详解）
        zNodes=[{}]

        $(document).ready(function () {
            zTreeObj = $.fn.zTree.init($("#treeDemo"), setting, zNodes);
        });   

    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="abc">
            &nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" onclick="POLICE()">报警</a>&nbsp;&nbsp;&nbsp;
    <a href="javascript:void(0);" onclick="ESC()">取消报警</a>
        </div>
       <ul id="treeDemo" class="ztree"></ul>
        <div class="right" id="right_div">
            <iframe id="rf_right" name="rf_right" style="width: 100%; height: 100%; margin-left: 0px; margin-right: 0px"
                frameborder="0px"></iframe>
        </div>
    </form>
    
</body>
</html>
