var setting = {
    view: {
        selectedMulti: false
    },
    async: {
        enable: true, //启用异步加载 
        url: "/api/rest.ashx?action_type=Camera&action_method=Child", //调用的后台的方法
        autoParam: ["id", "pid", "name"],          //向后台传递的参数
        otherParam: {},             //额外的参数
        treeNodeKey: "id",          // 节点的id
        treeNodeParentKey: "pid",   // 父节点的id
        dataFilter: filter
    }
    ,
    callback: {
        onClick: zTreeOnClick
    }
};

function zTreeOnClick(event, treeId, treeNode) {
    if (!treeNode.isParent) {
        Video_OpenCamera(treeNode.device_id);
        //alert(treeNode.tId + ", " + treeNode.id + ", " + treeNode.name + ", " + treeNode.value + ", " + treeNode.device_id);
    }
}

function filter(treeId, parentNode, childNodes) {
    var array = [];
    if (!childNodes) {
        return null;
    }
    for (var i = 0, l = childNodes.length; i < l; i++) {
        var vo = childNodes[i];
        repname = vo.name.replace(/\.n/g, '.');
        if (vo.isParent == "true") {
            array[i] = { id: vo.id, name: repname, value: vo.value, device_id: vo.device_id, isParent: true };
        }
        else {
            array[i] = { id: vo.id, name: repname, value: vo.value, device_id: vo.device_id, isParent: false };
        }
    }
    return array;
}
function beforeClick(treeId, treeNode) {
    if (!treeNode.isParent) {
        alert("Please select one parent node...");
        return false;
    } else {
        return true;
    }
}
var log, className = "dark";
function beforeAsync(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    return true;
}

//function onAsyncSuccess(event, treeId, treeNode, msg) {   
//    showLog("[ " + getTime() + " onAsyncSuccess ]&nbsp;&nbsp;&nbsp;&nbsp;" + ((!!treeNode && !!treeNode.name) ? treeNode.name : "root"));
//}


function getTime() {
    var now = new Date(),
    h = now.getHours(),
    m = now.getMinutes(),
    s = now.getSeconds(),
    ms = now.getMilliseconds();
    return (h + ":" + m + ":" + s + " " + ms);
}

function refreshNode(e) {
    var zTree = $.fn.zTree.getZTreeObj("TV_ID"),
    type = e.data.type,
    silent = e.data.silent,
    nodes = zTree.getSelectedNodes();
    if (nodes.length == 0) {
        alert("Please select one parent node at first...");
    }
    for (var i = 0, l = nodes.length; i < l; i++) {
        zTree.reAsyncChildNodes(nodes[i], type, silent);
        if (!silent) zTree.selectNode(nodes[i]);
    }
}