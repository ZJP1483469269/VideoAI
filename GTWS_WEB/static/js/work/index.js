function goNavPage(vUrl, vTypeID) {
    var vPageUrl = null;
    if (vUrl.indexOf("?") == -1) {
        vPageUrl = vUrl + "?TYPE_ID=" + vTypeID;
    }
    else {
        vPageUrl = vUrl + "&TYPE_ID=" + vTypeID;
    }
    if (vTypeID == "rf_left") {
        $("#div_main").hide();
        $("#div_left").show();
        $("#div_right").show();


        $("#rf_left").attr("src", vUrl);
        $("#rf_left").show();

        $("#rf_right").show();
    }

    if (vTypeID == "rf_right") {
        $("#div_main").hide();
        $("#div_left").show();
        $("#div_right").show();

        $("#rf_right").attr("src", vUrl);
        $("#rf_right").show();

        $("#rf_left").show();
    }

    if (vTypeID == "rf_main") {

        $("#div_main").show();
        $("#div_left").hide();
        $("#div_right").hide();

        $("#rf_left").hide();
        $("#rf_right").hide();

        $("#rf_main").attr("src", vUrl);
        $("#rf_main").show();
    }
}