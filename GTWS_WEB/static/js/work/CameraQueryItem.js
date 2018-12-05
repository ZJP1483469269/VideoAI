function LoadCounty() {
    $("#COUNTY").empty();
    $("#VILLAGE").empty();
    $("#VILLAGE").append("<option value=''>--全部--</option>");
    var cORG_ID = $("#ORG_ID").val();
    var vUrl = getAjaxUrl() + "/api/rest.ashx?action_type=Camera&action_method=county_list";
    var cValue = "ORG_ID=" + cORG_ID;
    $.ajax({
        type: "POST",
        url: vUrl,
        dataType: "json",
        cache: false,
        data: cValue,
        success: function (vret) {
            var rs = vret.rows;
            for (var i = 0; i < rs.length; i++) {
                var vo = rs[i];
                $("#COUNTY").append("<option value='" + vo.org_id + "'>" + vo.org_name + "</option>");
            }
        }
    });
}

function LoadVillage() {
    $("#VILLAGE").empty();
    $("#VILLAGE").append("<option value=''>--全部--</option>");
    var cORG_ID = $("#COUNTY").val();
    var vUrl = getAjaxUrl() + "/api/rest.ashx?action_type=Camera&action_method=village_list";
    var cValue = "ORG_ID=" + cORG_ID;
    $.ajax({
        type: "POST",
        url: vUrl,
        dataType: "json",
        cache: false,
        data: cValue + "&no-cache=" + Math.round(Math.random() * 10000),
        success: function (vret) {
            var rs = vret.rows;
            for (var i = 0; i < rs.length; i++) {
                var vo = rs[i];
                $("#VILLAGE").append("<option value='" + vo.org_id + "'>" + vo.org_name + "</option>");
            }
        }
    });
}

function doQuery() {
    vMaxID = "";
    ClearMaker();
    var cCOUNTY = $("#COUNTY").val();
    var cVILLAGE = $("#VILLAGE").val();
    var cADDR = $("#ADDR").val();
    $("#TABLE_TBODY").empty();
    vActiveCounty = cCOUNTY;
    vActiveVillage = cVILLAGE;
    vActiveAddr = cADDR;
    LoadVectorPointLayer(cCOUNTY, cVILLAGE, cADDR);
}


function ClearMaker() {
    for (var i = Marker_List.length - 1; i >= 0; i--) {
        var vo = Marker_List[i];
        map.removeLayer(vo);
    }
};

function LoadVectorPointLayer() {
    //var markers = new L.MarkerClusterGroup();
    var cORG_ID = $("#ORG_ID").val();
    var cValue = "ORG_ID=" + cORG_ID + "&PAGE_SIZE=" + vPAGE_SIZE + "&COUNTY=" + vActiveCounty + "&VILLAGE=" + vActiveVillage + "&ADDR=" + vActiveAddr;
    $.ajax({
        url: "/api/rest.ashx?action_type=Camera&action_method=query&MAXID=" + vMaxID,
        dataType: "json",
        data: cValue,
        success: function (ret) {
            var rs = ret.rows;
            for (var i = 0; i < rs.length; i++) {
                var vo = rs[i];
                if (vMaxID < vo.id) {
                    vMaxID = vo.id;
                }
                var vLon = vo.x;
                var vLat = vo.y;
                var vORG_ID = vo.org_id;
                var pt = new L.LatLng(vLat, vLon);
                var cStr = "<tr >";
                cStr = cStr + "<td>" + vo.county + "</td>";
                cStr = cStr + "<td>" + vo.village + "</td>";
                cStr = cStr + "<td>" + vo.addr + "</td>";
                cStr = cStr + "<td><a href='#' onclick='setMapView(" + vo.y + "," + vo.x + ");'>定位</a></td>";
                cStr = cStr + "</tr>";
                $("#TABLE_TBODY").append(cStr);
                var greenIcon = L.icon({
                    iconUrl: "../static/leaflet/images/marker-camera.png",
                    iconSize: [18, 25]// size of the icon
                });
                //vo.is_complete
               
                var wgs84togcj02 = coordtransform.wgs84togcj02(vLon, vLat);
                var vMarker = new L.Marker((wgs84togcj02),
                        {
                            draggable: false,        // 使图标可拖拽
                            opacity: 1,   // 设置透明度
                            icon: greenIcon
                        }
                        );

                var cHtml = "<table>";
                cHtml = cHtml + "<tr><td>所属县区</td><td>" + vo.county + "</td></tr>";
                cHtml = cHtml + "<tr><td>所属乡镇</td><td>" + vo.village + "</td></tr>";
                cHtml = cHtml + "<tr><td>详细地址</td><td>" + vo.addr + "</td></tr>";
                cHtml = cHtml + "<tr><td>完成情况</td><td>" + vo.village + "</td></tr>";
                //cHtml = cHtml + "<a href='#' onclick='drawCircle(" + vo.y + "," + vo.x + ");'>查看监控范围</a>";
                cHtml = cHtml + "<tr>";
                cHtml = cHtml + "   <td colspan=2 style='text-align:center'>";
                cHtml = cHtml + "       <a href='#' onclick=\"OpenWinForm('../gtws/Live.aspx?TYPEID=3&CAMERACODE=" + vo.device_id + "');\">打开视频</a>&nbsp;&nbsp;<a href='#' onclick=\"OpenWinForm('../gtws/RecordQuery.aspx?TYPEID=4&CAMERACODE=" + vo.device_id + "');\">查询录像</a>";
                cHtml = cHtml + "   </td>";
                cHtml = cHtml + "</tr>";
                vMarker.bindPopup(cHtml);
                vMarker.data = vo;
                Marker_List.push(vMarker);
                //map.addLayer(markers);
                map.addLayer(vMarker);
            }
            if (rs.length == vPAGE_SIZE) {
                LoadVectorPointLayer();
            }
        }
    })
};


//生成矢量图斑
function LoadVectorPolygonLayer() {
    var LayerObject = new Object();
    var vORG_ID = $("#ORG_ID").val();
    //LayerObject.XZQDM = vXZQDM;
    var polygonlist = [];
    $.ajax({
        url: "/api/rest.ashx?action_type=JCTB&action_method=query&ORG_ID=" + vORG_ID,
        dataType: "json",
        success: function (ret) {
            var rs = ret.rows;
            for (var i = 0; i < rs.length; i++) {
                var vo = rs[i];
                var vPointVal = vo.ogr_geometry;
                var vKeyList = vPointVal.split("), (");
                var vLength = vKeyList.length;
                for (var k = 0; k < vLength; k++) {
                    vPointVal = vKeyList[k];
                    vPointVal = vPointVal.replace("POLYGON ((", "");
                    vPointVal = vPointVal.replace("))", "");
                    var PointList = vPointVal.split(",");
                    var vPTList = [];
                    for (var j = 0; j < PointList.length; j++) {
                        vPointVal = $.trim(PointList[j]);
                        if (vPointVal != "") {
                            var ptx = vPointVal.split(" ");
                            var vptx = parseFloat(ptx[1]) + 0.00005;      //34
                            var vpty = parseFloat(ptx[0]) + 0.0013;   //115
                            var wgs84togcj02 = coordtransform.wgs84togcj02(vpty, vptx);
                            var pt = new L.LatLng(wgs84togcj02[0], wgs84togcj02[1]);
                            vPTList.push(pt);
                        }
                    }
                    var vColor = "red";
                    var vFillColor = "red";
                    if (k == 1) {
                        vColor = "yellow";
                        vFillColor = 'yellow';
                    }
                    if (k == 2) {
                        vColor = "yellow";
                        vFillColor = "yellow";
                    }
                    if (k == 3) {
                        vColor = "yellow";
                        vFillColor = 'yellow';
                    }
                    var vPolygon = new L.Polygon(vPTList, {
                        color: vColor,
                        fillColor: vFillColor,
                        fillOpacity: 0.3
                    });
                    //vPolygon.bindTooltip('图斑代码' + vo.jcbh, { permanent: true, interactive: true });
                    vPolygon.bindPopup("图斑编号：" + vo.jcbh + "<br>监测面积：" + vo.jcmj);
                    map.addLayer(vPolygon);
                    polygonlist.push(vPolygon);
                }
            }
            LayerObject.POLYGON_LIST = polygonlist;
        },
        error: function (a, b) {
            return;
        }
    });
}

