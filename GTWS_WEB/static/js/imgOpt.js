
function opentTpInfo(id, paths, flag) {

    dialog(id, paths, flag);
}
function dialog(id, paths, flag) {
    //获取页面的高度和宽度
    var sWidth = document.body.scrollWidth || document.documentElement.scrollWidth;
    var sHeight = document.body.scrollHeight || document.documentElement.scrollHeight;
    //获取页面的可视区域高度和宽度
    var wHeight = document.documentElement.clientHeight || document.body.clientHeight;
    //创建遮罩层
    var oMask = document.createElement("div");
    oMask.id = "mask";
    oMask.style.height = sHeight / 3 +50+ "px";
    oMask.style.width = sWidth / 6 +50+ "px";
    oMask.style.position = "relative";
    oMask.style.left = sWidth / 2.4 -25+ "px";

    oMask.style.top = -(wHeight/(3/2)+10) + "px";
    oMask.style.textAlign = "center";
    document.body.appendChild(oMask);            //添加到body末尾
    //创建登录框
    var oLogin = document.createElement("div");
    oLogin.id = "login";
    oLogin.innerHTML = "<div style='text-align:center'><img id='" + id + "' onclick='ChangeRotate();' class='images' src='" + paths + "'></div>";
    document.body.appendChild(oLogin);

    //获取登陆框的宽和高
    var dHeight = oLogin.offsetHeight;
    var dWidth = oLogin.offsetWidth;
    //设置登陆框的left和top
    oLogin.style.left = sWidth / 2 - dWidth / 2 + "px";
    oLogin.style.top = wHeight / 2 - dHeight / 2 + "px";

    //获取关闭按钮
    var oClose = document.getElementById("close");

    //点击关闭按钮和点击登陆框以外的区域都可以关闭登陆框
    //oClose.onclick = oMask.onclick = oLogin.onclick = function () {
    oClose.onclick  = oLogin.onclick = function () {
        document.body.removeChild(oLogin);
        document.body.removeChild(oMask);
    };
}
