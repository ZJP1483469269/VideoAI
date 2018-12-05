function submit_Click() {
    var user_danwei = form1.danwei.value;
    var user_adress = form1.adress.value;
    var user_neirong = form1.neirong.value;
    var user_time = form1.time.value;
    var user_cailiao = form1.UPLOAD.value;

    if ((user_danwei == "") || (user_danwei == null)) {
        alert("请输入被举报单位或个人！");
        form1.danwei.focus();
        return false;
    }
    else if ((user_adress == "") || (user_adress == null)) {
        alert("请输入问题发生地！");
        form1.adress.focus();
        return false;
    }
    else if ((user_time == "") || (user_time == null)) {
        alert("请输入问题发生时间！");
        form1.time.focus();
        return false;
    }
    else if ((user_neirong == "") || (user_neirong == null)) {
        alert("请输入举报内容！");
        form1.neirong.focus();
        return false;
    }
    else {
        return true;
    }
}
   