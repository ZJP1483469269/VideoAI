
$(window).resize(function () {
    reFresh();
});
$(function () {
    selectArea();
    reFresh();
    uploadFile();
    $('#RESID').val('');

    var cUrlVal = getQueryString("sendfrom");

    $(".Gtd").hide();
    if (cUrlVal != null) {

        if (cUrlVal.indexOf('shenhe') >= 0) {
            $("#btnBack").show();
            //$(".btn").val("审定");
            $(".Gtd").show();
            var sendType = "WLJB_LIST";
            var cVal = getQueryString("vals");
            var cVal1 = getQueryString("types");
            $(".selfui-zg-bansinfo").hide();
            $("#uploadfileSoft").remove();
            $("#saveButton").replaceWith('<input type="button" class="saveBtn btn" id="saveButton" value="确定" onclick="shenp()" />');
            $.ajax({
                type: "POST",
                url: "/api/Handler.ashx",
                dataType: "json",
                cache: false,
                data: cVal + "&action_method=" + sendType + "&vals=" + cVal,
                success: function (ret) {
                    if (ret != "" && ret != null) {
                        var backedata = eval(ret);
                        $(".area").val(backedata.rows[0]["a_xianqu"]);
                        $(".baseAdress").val(backedata.rows[0]["a_adress"]);
                        $(".xmmc").val(backedata.rows[0]["a_xmdisc"]);
                        $(".suspectPerson").val(backedata.rows[0]["a_danwei"]);
                        $(".linkName").val(backedata.rows[0]["a_people"]);
                        $(".linkPhone").val(backedata.rows[0]["a_phone"]);
                        $(".eamil").val(backedata.rows[0]["a_email"]);
                        $(".jbText").val(backedata.rows[0]["a_neirong"]);
                        $(".shenheEffect").val(backedata.rows[0]["a_sd_reslut"]);
                        $("#jbbaseinfo input").attr("readOnly", "true");
                        $("#jbbaseinfo textarea").attr("readOnly", "true");

                        if (backedata.rows[0]["a_sd_reslut"] != 0 && backedata.rows[0]["a_sd_reslut"] != null) {
                            $("#shenhe").val(backedata.rows[0]["a_sd_reslut"]);
                            $("#saveButton").hide();
                            $("#jbbaseinfo select").attr("disabled", "true");
                        } else {
                            $("#shenhe").val(1);
                        }
                        var files = backedata.rows[0]["a_files_id"];
                        var files_id = new Array();
                        files_id = files.split(',');
                        var htmls;
                        for (var i = 0; i < files_id.length - 1; i++) {
                            htmls = '<div class="imgDiv"><img src="' + backedata.rows[i]["url"] + '" class="files"  id="files' + i + '" onclick=opentTpInfo(' + i + ',"files"); data-tag="' + backedata.rows[i]["url"] + '" /> </div>  ';
                            $(".imgsdiv").append(htmls);

                        }

                    }

                }
            });
        } else if (cUrlVal.indexOf('gtws') >= 0) {//str.replace(/(^\s*)|(\s*$)/g, "") 字符串去掉空格

            $(".selfui-zg-bansinfo").hide();
            $("#uploadfileSoft").remove();
            var obj = document.getElementById("jbbaseinfo");
            obj.style.top = "15px";
            var areaXzqdm = parent.document.getElementById("xzqdm").value;
            $("#select_area").val(areaXzqdm);
            var adress = parent.document.getElementById("adress").value;
            $("#xmmc").val("国土卫士举报");
            var areaXzqdm = parent.document.getElementById("xzqdm").value;
            $("#baseAdress").val(adress);
            $("#suspectPerson").val(adress);
            var photoinfo = parent.document.getElementById("photoinfo").src;

            $(".imgsdiv").replaceWith('<div class="imgsdiv" style="display: inline-flex;width:100%"><input type="text" style ="width:100%;" id="photoUrls" readonly="readonly" value="' + photoinfo.substr(21, photoinfo.length) + '"/>     </div>');
            $("#saveButton").replaceWith('<input type="button" class="saveBtn btn" id="saveButton" value="保存" onclick="savrInfo()" />');


        }

    }
});


function gb() {
    window.history.go(-1);
};
function reFresh() {
    var obj = document.getElementById("jbbaseinfo");
    obj.style.position = "relative";
    var sreenWidth = document.body.clientWidth;
    var leftStyle = (sreenWidth - 980) / 2;
    obj.style.left = leftStyle + "px";
    obj.style.top = "-35px";
}
function saves() {
    uploadfiles();
    var base_Adress = form1.baseAdress.value;
    var xm_mc = form1.xmmc.value;
    var suspect_Person = form1.suspectPerson.value;
    if ((base_Adress == "") || (base_Adress == null)) {
        alert("请输入详细地址！");
        form1.baseAdress.focus();
        return false;
    }
    else if ((xm_mc == "") || (xm_mc == null)) {
        alert("请输入项目描述！");
        form1.xmmc.focus();
        return false;
    }
    else if ((suspect_Person == "") || (suspect_Person == null)) {
        alert("请输入举报单位！");
        form1.suspectPerson.focus();
        return false;
    }
    else {
        return true;
    }
}
function shenp() {
    var cValue = $("#form1").formSerialize();
    var cVal = getQueryString("vals");
    $.ajax({
        type: "POST",
        url: "/api/Handler.ashx",
        dataType: "json",
        cache: false,
        data: cValue + "&action_method=WLJB_CHECK" + "&cVal=" + cVal,
        success: function (ret) {
            if (ret > 0) {
                alert("审核成功");
                window.history.go(-1);
            }
        }
    });
}
function savrInfo() {

    var getJbtype = getQueryString("action_method");
    var getFiles_Id = $("#RESID").val();
    getJbtype = "WLJB_SAVE";
    var cValue = $("#form1").formSerialize();
    var otherValue = $("#photoUrls").val();
    var strTemp = false;
    var updateImgState;
    if (parent.window.location.href.indexOf("BlueSky_jb.html") > -1) {
        var updateImgState= parent.document.getElementById("cameraAdress").value;
        otherValue = "&ltwsJb=ltwsJb&urls='" + otherValue + "'"+"&img_id="+updateImgState;
        strTemp = true;
    } else {
        otherValue = "";
    }
    if (getJbtype == "WLJB_SAVE") {
        $.ajax({
            type: "POST",
            url: "/api/Handler.ashx",
            dataType: "json",
            cache: false,
            data: cValue + "&action_method=" + getJbtype + "&getFiles_Id=" + getFiles_Id + otherValue,
            success: function (ret) {
                if (ret != "" && ret != null) {
                    var backedata = eval(ret);
                    if (backedata == "-1") {
                        alert("举报未成功，请重新填报，或者联系管理员QQ：1920352020");
                        return;
                    }
                    if (strTemp == true) {
                        alert("查询号：" + backedata);
                        AjaxClose();
                        parent.LoadInitValue();

                    } else {
                        location.href = "saveOver.html?basebit=" + backedata;
                    }


                }
            }
        });
    }
}
function AjaxClose() {
    var index = parent.layer.getFrameIndex(window.name); // 获取窗口索引
    parent.layer.close(index);
}
//JS 获取地址参数
function getQueryString(cKeyName) {
    var reg = new RegExp('(^|&)' + cKeyName + '=([^&]*)(&|$)', 'i');
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}
function uploadFile() {
    $('#file-pic').fileinput({//初始化上传文件框
        showUpload: false,
        showRemove: false,
        uploadAsync: true,
        uploadLabel: "上传", //设置上传按钮的汉字
        uploadClass: "btn btn-primary", //设置上传按钮样式
        showCaption: true, //是否显示标题
        language: "zh", //配置语言
        uploadUrl: "/api/upload.ashx?",
        maxFileSize: 0,
        maxFileCount: 3, /*允许最大上传数，可以多个，当前设置单个*/
        enctype: 'multipart/form-data',
        allowedPreviewTypes: ['image'], //allowedFileTypes: ['image', 'video', 'flash'],
        allowedFileExtensions: ["jpg", "png", "gif"], /*上传文件格式*/
        msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
        dropZoneTitle: "请通过拖拽图片文件放到这里",
        dropZoneClickTitle: "或者点击此区域添加图片",
        //uploadExtraData: { "id":id},//这个是外带数据
        showBrowse: false,
        browseOnZoneClick: true,
        slugCallback: function (filename) {
            var filename = filename.replace('(', '_').replace(']', '_');
            return filename;
        }
    });
    $('#file-pic').on("fileuploaded", function (event, data, previewId, index) {
        var result = data.response; //后台返回的json
        var strValue = $('#RESID').val();
        var fileCount = $('#file-pic').fileinput('getFilesCount');
        if (result.ID != null && result.ID != '') {
            strValue = strValue + result.ID + ",";
            $('#RESID').val(strValue);
            if (fileCount != 1) {
                if ((fileCount - 1) == 0) {
                    savrInfo();
                }
            } else {
                savrInfo();
            }
        } else {
            //strValue = result.ID;
            alert("图片保存失败");
            return;
        }
    });
}

function uploadfiles() {
    $('#file-pic').fileinput('upload');
}
//点击事件的另一种写法
//$('#savePic').on('click', function () {// 提交图片信息 //
//    //先上传文件，然后在回调函数提交表单
//    $('#file-pic').fileinput('upload');
//});


function opentTpInfo(id, flag) {
    var paths = $("#files" + id).attr("data-tag");
    dialog(id, paths, flag);
}

var vidx = 0;
function ChangeRotatefiles() {
    vidx = vidx + 90;
    $(".filesbig").rotate(vidx);

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
    oMask.style.height = sHeight + "px";
    oMask.style.width = sWidth + "px";
    document.body.appendChild(oMask);            //添加到body末尾
    //创建登录框
    var oLogin = document.createElement("div");
    oLogin.id = "login";
    if (flag == "files") {
        oLogin.innerHTML = "<div style='text-align:center' > <img id='files" + id + "' onclick='ChangeRotatefiles(" + id + ");' class='filesbig' src='" + paths + "'></div>";
    }

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
    oClose.onclick = oMask.onclick = function () {
        document.body.removeChild(oLogin);
        document.body.removeChild(oMask);
    };
}

function selectArea(types) {
    var cValue = $("#form1").formSerialize();

    $.ajax({
        type: "POST",
        url: "/api/selects.ashx",
        dataType: "json",
        cache: false,
        data: cValue,
        async: false,
        success: function (ret) {
            //alert(1);
            if (ret != "" && ret != null) {
                for (var i = 0; i < ret.length; i++) {
                    $("#select_area").append("<option value=" + ret[i]["ORG_ID"] + ">" + ret[i]["ORG_NAME"] + "</option>");
                }
            }
        }
    });
}