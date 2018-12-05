$(function () {

    if (self.location.href.indexOf("WFTB.aspx") != -1) {
        searchInfoJs("city");
        chooselx("zylx");
        chooselx("lxxh");
        chooselx("wfyy");
        sendToWftb('tbinfo', "", "")   
    } else if (self.location.href.indexOf("JZTB.aspx") != -1) {
        searchInfoJs("city");
        jztbselect("jzlx");
        sendToWftb('jztb_tbinfo', "", "")  
    }
    else if (self.location.href.indexOf("fxzaspx.aspx") != -1) {
        searchInfoJs("city");
        sendToWftb('fxztb_info', "", "")
    } else if (self.location.href.indexOf("zgaspx.aspx") != -1) {
        searchInfoJs("city");
        sendToWftb('zgtb_info', "", "");
        $("#zglx").hide();


    }

});

function searchInfoJs(type) {
    if (type == "city") {
        jQuery.post("/ashx/searchInfoAshx.ashx", { flag: "serachInfoCity" }, function (backDate) {
            var strTemnp = eval("(" + backDate + ")");
            $("#City").empty();
            $("#City").append("<option value='-1'>" + "选择市" + "</option>");
            for (var i = 0; i < strTemnp.length; i++) {
                $("#City").append("<option value=" + strTemnp[i].Name + ">" + strTemnp[i].Value + "</option>");
            }
            if (strTemnp.length == 1) {
                $("#City").val(strTemnp[0].Name);
                searchInfoJs("area");
            }
        });

    } else if (type == "area") {
        var cityXzqdm = $(City).find("option:selected").val(); 
        jQuery.post("/ashx/searchInfoAshx.ashx", { flag: "serachInfoArea", cityOrg: cityXzqdm }, function (backDate) {
            $("#Area").empty();
            $("#Area").append("<option value='-1'>" + "选择区" + "</option>");
            var strTemnp = eval("(" + backDate + ")");
            for (var i = 0; i < strTemnp.length; i++) {
                $("#Area").append("<option value=" + strTemnp[i].Name + ">" + strTemnp[i].Value + "</option>");
            }
            if (strTemnp.length == 1) {
                //$(City).find("option:selected").val(cityXzqdm);
                $("#Area").val(strTemnp[0].Name);
            } else {
                if (cityXzqdm == -1) {
                    $(City).find("option:selected").val(-1);
                }
            }
        });
    }
}
function chooselx(tag) {
    if (tag == "zylx") {
        jQuery.post("/ashx/searchInfoAshx.ashx", { flag: "zylx" }, function (backDate) {
            var strTemnp = eval("(" + backDate + ")");
            for (var i = 0; i < strTemnp.length; i++) {
                $("#zylx").append("<option value=" + strTemnp[i].Name + ">" + strTemnp[i].Value + "</option>");
            }
        });
    } else if (tag == "lxxh") {
        jQuery.post("/ashx/searchInfoAshx.ashx", { flag: "lxxh" }, function (backDate) {
            var strTemnp = eval("(" + backDate + ")");
            for (var i = 0; i < strTemnp.length; i++) {
                $("#lxxh").append("<option value=" + strTemnp[i].Name + ">" + strTemnp[i].Value + "</option>");
            }
        });
    }
    else if (tag == "wfyy") {
         jQuery.post("/ashx/searchInfoAshx.ashx", { flag: "wfyy" }, function (backDate) {
            var strTemnp = eval("(" + backDate + ")");
            for (var i = 0; i < strTemnp.length; i++) {
                $("#wfyy").append("<option value=" + strTemnp[i].Name + ">" + strTemnp[i].Value + "</option>");
            }
        });
    } else if (tag == "fxz") {
        jQuery.post("/ashx/searchInfoAshx.ashx", { flag: "fxz" }, function (backDate) {
            var strTemnp = eval("(" + backDate + ")");
            for (var i = 0; i < strTemnp.length; i++) {
                $("#wfyy").append("<option value=" + strTemnp[i].Name + ">" + strTemnp[i].Value + "</option>");
            }
        });

    }
    else if (tag == "all") {
        var xzjg = $("#xzjg").val();
        var xzjg2 = $("#HiddenFiel2").val();
        var xzjg3 = $("#HiddenFiel3").val();

        var city = $("#City").val();
        var area = $("#Area").val();
        
        var sdvalue = $("#sdlx").val();

        var msgc = document.getElementById("msgc");
        var yhyz = document.getElementById("yhyz");
        var zgdw = document.getElementById("zgdw");
        var ms = msgc.checked;
        var yh = yhyz.checked;
        var zg = zgdw.checked;
        $("#left").load("tbsearch_leftList.aspx?xzjginfo=" + xzjg + "" + "&xzjg2info=" + xzjg2 + "&xzjg3info=" + xzjg3 + "&msgc=" + ms + "&yhyz=" + yh + "&zgdw=" + zg + "&city=" + city + "&area=" + area + "&sdvalue=" + sdvalue);
    
    }


}
function cchooseInfo(flag) {
    var zylx = "";
    var xmlxText = "";
    var xmlxValue = "";
    var lxxh = "";
    if (flag == "clear") {
       //$("#zylx").val(-1);
       $("#xmlx").val(-1);
       $("#xzjg").val("");
       $("#Hidexzjgyc").val("");
    } else if (flag == "clear2") {
        $("#lxxh").val(-1);
        $("#xzjg2").val("");
        $("#HiddenFiel2").val("");
    }else if(flag=="clear3"){
        $("#wfyy").val(-1);
        $("#xzjg3").val("");
        $("#HiddenFiel3").val("");
    }
        else if (flag == "xmlx") {
            var strTempText;
            var strTempValue;

        var xzjgValue = $("#xzjg").val();   
        xmlxValue = $("#xmlx").val();
        xmlx = $("#xmlx").find("option:selected").text();
   
        if (xmlxValue != -1) {
            strTempText = xmlx;
            strTempValue = xmlxValue;

        } else {
            xmlx = "";
            strTempText = xmlx;
            xmlxValue = "";
            strTempValue = xmlxValue;
        }
        var HiddenFieVal = $("#Hidexzjgyc").val();
        var xzjgCruValue = $("#xzjg").val();
        if (HiddenFieVal.indexOf(strTempValue) < 0) {
            strTempText = strTempText + ";" + xzjgValue;
            strTempValue = strTempValue + ";" + HiddenFieVal
        } else {
            alert("当前类型已经选中");
            strTempText = xzjgCruValue;
            strTempValue = HiddenFieVal;
        }
        $("#xzjg").val(strTempText);
        $("#Hidexzjgyc").val(strTempValue);
       } else if (flag == "lxxh") {
        var strTempText;
        var strTempVal;

        var selectLxxhText = $("#lxxh").find("option:selected").text();
        var selectLxxhVal = $("#lxxh").val();

        if (selectLxxhVal != -1) {
            strTempVal = selectLxxhVal;
            strTempText = selectLxxhText;
        } else {
            selectLxxhText = "";
            selectLxxhVal = "";
            strTempVal = selectLxxhVal;
            strTempText = selectLxxhText;
        }
        var HiddenFiel2Val = $("#HiddenFiel2").val();
        var xzjg2Temp = $("#xzjg2").val();

        if (HiddenFiel2Val.indexOf(strTempVal) < 0) {
            strTempVal = strTempVal + ";" + HiddenFiel2Val;
            strTempText = strTempText + ";" + xzjg2Temp
        } else {
            alert("当前类型已经选中");
            strTempVal = HiddenFiel2Val;
            strTempText = xzjg2Temp;
        }
        $("#xzjg2").val(strTempText);
        $("#HiddenFiel2").val(strTempVal);
        //var a = $("#HiddenFiel2").val();
        //alert(a);
    } else if (flag == "wfyy") {
        var strTempText;
        var strTempVal;

        var selectWfyyText = $("#wfyy").find("option:selected").text();
        var selectWfyyVal = $("#wfyy").val();

        if (selectWfyyVal != -1) {
            strTempVal = selectWfyyVal;
            strTempText = selectWfyyText;
        } else {
            selectWfyyText = "";
            selectWfyyVal = "";
            strTempVal = selectWfyyVal;
            strTempText = selectWfyyText;
        }
        var HiddenFiel3Val = $("#HiddenFiel3").val();
        var xzjg3Temp = $("#xzjg3").val();

        if (HiddenFiel3Val.indexOf(strTempVal) < 0) {
            strTempVal = strTempVal + ";" + HiddenFiel3Val;
            strTempText = strTempText + ";" + xzjg3Temp
        } else {
            alert("当前类型已经选中");
            strTempVal = HiddenFiel3Val;
            strTempText = xzjg3Temp;
        }
        $("#xzjg3").val(strTempText);
        $("#HiddenFiel3").val(strTempVal);

        //var a = $("#HiddenFiel3").val();
        //alert(a);
    }
   //var a = $("#Hidexzjgyc").val();
    // alert(a);XT_INVALID_REASON
}
function sendToWftb(flag,jctb,xzqdm,tbid)
{
    //add by dnn  定位到地图时使用
    $("#tb_id", parent.document).val(tbid);


    if (flag == "tbinfo"){
        $("#buttonTbInfo").load("tb_searchShowInfo.aspx?jctb=" + jctb + "&xzqdm=" + xzqdm);
    } else if (flag == "jztb_tbinfo") {
        $("#baseinfo").load("jztbbaseinfo.aspx?jctb=" + jctb + "&xzqdm=" + xzqdm);
    } else if (flag == "fxztb_info") {
        $("#baseinfo").load("baseinfo.aspx?jctb=" + jctb + "&xzqdm=" + xzqdm);
    } else if (flag == "zgtb_info") {
        $("#baseinfo").load("zgbaseinfo.aspx?jctb=" + jctb + "&xzqdm=" + xzqdm);   
    }

}


function jztbselect(flag) {
    if (flag == "jzlx"){
        jQuery.post("/ashx/searchInfoAshx.ashx", { flag: "jzlx" }, function (backDate) {
            $("#jzlx").empty();
            $("#jzlx").append("<option value='-1'>" + "全部" + "</option>");
            var strTemnp = eval("(" + backDate + ")");
            for (var i = 0; i < strTemnp.length; i++) {
                $("#jzlx").append("<option value=" + strTemnp[i].Name + ">" + strTemnp[i].Value + "</option>");
            }
        });
   }
}
function sendToLeftList() {
   var jzlx = $("#jzlx").val();
   var city = $("#City").val();
   var area = $("#Area").val();
    //if()
   //alert("jz=" + jzlx + "   city = " + city + "   area= " + area);
   $("#left").load("tbsearch_leftList.aspx?jzlx=" + jzlx + "&city=" + city + "&area=" + area);
}


var setTime =null;
var cXZQDM = null;



function exchangeMapinfo(xzqdm,key) {

    try {
        parent.parent.userCurrentCode = xzqdm;
        parent.parent.userCurrentName = key;
    } catch (ex) {

    }
}

function exchangeMapXzqdm() {

    var tbsearchValue = $("#Area").val();
    var tbsearvhText = $("#Area").find("option:selected").text();
    //var tbsearvhText = $("#Area").text();

    cXZQDM = $("#Area").val();
    var cCITY = "";
    if (cXZQDM != -1) { 
        cCITY = cXZQDM.substr(0, 4) + "00";
        try
        {
            //$("#select_shi", parent.parent.document).val(cCITY);
            //setTime = setInterval("myInterval()", 1000);//1000为1秒钟
            //xzqChange($("#select_shi", parent.parent.document));

            parent.parent.userCurrentCode = tbsearchValue;
            parent.parent.userCurrentName = tbsearvhText;
       
           
        }catch(ex){


        }
    } 
}



function myInterval() {
    $("#select_xian", parent.parent.document).val(cXZQDM);
    var strTemp =$("#select_xian", parent.parent.document).val();
    if(strTemp ==cXZQDM )
    {
        clearInterval(setTime);//清除定时器
        setTime = null;//清除定时器

    }

}


