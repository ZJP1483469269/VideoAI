<%@ Page Language="C#" AutoEventWireup="true" CodeFile="webgis.aspx.cs" Inherits="webgis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>商丘</title>
    <link href="../static/leaflet/leaflet.css" rel="stylesheet" type="text/css" />
    <link href="../static/leaflet/leaflet-measure.css" rel="stylesheet" type="text/css" />
    <script src="../static/js/jquery.js" type="text/javascript"></script>
    <script src="../static/js/layer/layer.js" type="text/javascript"></script>
    <script src="../static/leaflet/leaflet.js" type="text/javascript"></script>
    <script src="../static/leaflet/L.Control.MousePosition.js" type="text/javascript"></script>
    <script src="../static/leaflet/leaflet-measure.js" type="text/javascript"></script>
    <script src="../static/leaflet/map.js" type="text/javascript"></script>
    <script src="../static/area_range/<%=ORG_ID %>.js" type="text/javascript"></script>
    <script src="../static/js/default.js" type="text/javascript"></script>
    <script src="../static/js/work/CameraQueryItem.js" type="text/javascript"></script>
</head>
<body>
    <div id="map" style="position: absolute; left: 1px; top: 1px; right: 1px; bottom: 1px;
        z-index: 999999; margin: 2px; padding: 0px">
    </div>
    <script type="text/javascript">
        var vMaxID = "";
        var IS_LOAD = false;
        var vPAGE_SIZE = 100;
        var map = null;
        var vActivePopup = null;
        var vType_ID = "<%= TYPE_ID %>";
        var ID = "<%=ID%>";
        var CENTER_X = "";
        var CENTER_Y = "";
        if (CENTER_X == "") {
            CENTER_X = "34.39";
        }
        if (CENTER_Y == "") {
            CENTER_Y = "115.65";
        }
        $(document).ready(function () {
            InitMap();
        });

        function InitMap() {
            map = L.map('map', {
                measureControl: true,
                layersControl: true,
                attributionControl: false,
                center: [CENTER_X, CENTER_Y],
                zoom: 14,
                minZoom: 1,
                maxZoom: 18
            });
            map.on('measurefinish', function (evt) {
                writeResults(evt);
            });

            LoadBasicLayer();
            // LoadVectorPointLayer();
            LoadVectorPolygonLayer();
            CENTER_X = <%= X %>;
            CENTER_Y = <%= Y %>;
            if((CENTER_X>0)&&(CENTER_Y>0))
            {
                setMapView(CENTER_X, CENTER_Y);
            }
            var greenIcon = L.icon({
                iconUrl: "../static/leaflet/images/marker-camera.png",
                iconSize: [18, 25]// size of the icon
            });
            var wgs84togcj02 = coordtransform.wgs84togcj02(CENTER_Y, CENTER_X);
            var vMarker = new L.Marker((wgs84togcj02),
                    {
                        draggable: false,        // 使图标可拖拽
                        opacity: 1,   // 设置透明度
                        icon: greenIcon
                    }
            );
            vMarker.addTo(map);

//            geojson = L.geoJson(statesData, {
//                style: style,
//                onEachFeature: onEachFeature,
//                attribution: ''
//            });
//            geojson.addTo(map);
        }
        function LoadBasicLayer() {
            var overlayer = {
                "影像地图": L.layerGroup([
	    L.tileLayer('http://mt1.google.cn/vt/lyrs=s&hl=zh-CN&gl=CN&x={x}&y={y}&z={z}&s=Gali'),
	    L.tileLayer('http://mt1.google.cn/vt/imgtp=png32&lyrs=h@207000000&hl=zh-CN&gl=cn&x={x}&y={y}&z={z}&s=Galil')
                ]).addTo(map),
                "街道地图": L.tileLayer('http://webrd0{s}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=8&x={x}&y={y}&z={z}', { subdomains: "1234" })

                //                "天地图": L.layerGroup([
                //                     L.tileLayer('http://t{s}.tianditu.cn/DataServer?T=vec_w&X={x}&Y={y}&L={z}', { subdomains: ['0', '1', '2', '3', '4', '5', '6', '7'] }),
                //                     L.tileLayer('http://t{s}.tianditu.cn/DataServer?T=cva_w&X={x}&Y={y}&L={z}', { subdomains: ['0', '1', '2', '3', '4', '5', '6', '7'] })
                //                ]),
                //                "天地图影像": L.layerGroup([
                //                    L.tileLayer('http://t{s}.tianditu.cn/DataServer?T=img_w&X={x}&Y={y}&L={z}', { subdomains: ['0', '1', '2', '3', '4', '5', '6', '7'] }),
                //                    L.tileLayer('http://t{s}.tianditu.cn/DataServer?T=cia_w&X={x}&Y={y}&L={z}', { subdomains: ['0', '1', '2', '3', '4', '5', '6', '7'] })
                //                ]),
                //                "天地图地形": L.layerGroup([
                //                   L.tileLayer('http://t{s}.tianditu.cn/DataServer?T=ter_w&X={x}&Y={y}&L={z}', { subdomains: ['0', '1', '2', '3', '4', '5', '6', '7'] }),
                //                   L.tileLayer('http://t{s}.tianditu.cn/DataServer?T=cta_w&X={x}&Y={y}&L={z}', { subdomains: ['0', '1', '2', '3', '4', '5', '6', '7'] })
                //                ])
            };
            L.control.layers(overlayer, {}, { position: "topright" }).addTo(map);
            L.control.scale().addTo(map);
            L.control.mousePosition().addTo(map);
        }

        var Marker_List = [];
        var vActiveCounty = "";
        var vActiveVillage = "";
        var vActiveAddr = "";
        function setQueryItem(vCounty, vVillage, vAddr) {
            vMaxID = "";
            vActiveCounty = vCounty;
            vActiveVillage = vVillage;
            vActiveAddr = vAddr;
            KeyInfoList = [];
            ClearMaker();
            LoadVectorPointLayer();
        }

        function addVectorPointLayer() {
            LoadVectorPointLayer();
        }

        function setMapView(vX, vY) {
            var hide = document.getElementById("DBGRID");
            hide.style.display = "none";
            var wgs84togcj02 = coordtransform.wgs84togcj02(vY, vX);
            map.setView(new L.LatLng(wgs84togcj02[0], wgs84togcj02[1]), 15);
        };

        function onViewVideo(vKeyID) {
            AjaxOpenDialog('查看视频', 'VideoAgent.aspx?ID=' + vKeyID, "640px", "480px");
        }

        function onViewHistory(vKeyID) {
            AjaxOpenDialog('录像查询', '../gtws/VideoQuery.aspx?ID=' + vKeyID, "640px", "480px");
        }

        function QueryItem() {
            $("#DBGRID").toggle();
            if (!IS_LOAD) {
                var vORG_ID=$("#ORG_ID").val();
                $("#DBGRID").load("CameraQueryItem.aspx?ORG_ID="+vORG_ID,function(){
                    LoadCounty();
                    IS_LOAD = true;
                });
            }
        }
        //测量距离和面积
        function writeResults(results) {
            document.getElementById('eventoutput').innerHTML = JSON.stringify({
                area: results.area,
                areaDisplay: results.areaDisplay,
                lastCoord: results.lastCoord,
                length: results.length,
                lengthDisplay: results.lengthDisplay,
                pointCount: results.pointCount,
                points: results.points
            }, null, 2);
        };
        //geojson县区边界
//        function getColor() {
//        }
//        function style(feature) {
//            return {
//                weight: 2,
//                opacity: 1,
//                color: 'blue',
//                dashArray: '3',
//                fillOpacity: 0.2,
//                fillColor: getColor(feature.properties.density)
//            };
//        }
//        function highlightFeature(e) {
//            var layer = e.target;
//            layer.setStyle({
//                weight: 5,
//                color: '#666',
//                dashArray: '',
//                fillOpacity: 0.7
//            });
//        }
//        var geojson;
//        function resetHighlight(e) {
//            geojson.resetStyle(e.target);
//            //info.update();
//        }
//        function zoomToFeature(e) {
//            map.fitBounds(e.target.getBounds());
//        }
//        function onEachFeature(feature, layer) {
//            layer.on({
//                mouseover: highlightFeature,
//                mouseout: resetHighlight,
//                click: zoomToFeature
//            });
//        }

    </script>
    <div title="在地图上显示摄像机位置" style="position: absolute; right: 12px; top: 125px; z-index: 999999;">
        <input type="button" value="显示" onclick="addVectorPointLayer();;" style="height: 45px;
            width: 45px; border-radius: 3px; background-color: White;" />
    </div>
    <div title="查看摄像机视频" style="position: absolute; right: 12px; top: 180px; z-index: 999999;">
        <input type="button" value="视频" onclick="OpenWinForm('../gtws/Default.aspx?TYPEID=2');"
            style="height: 45px; width: 45px; border-radius: 3px; background-color: White;" />
    </div>
    <div title="查询定位到某个摄像机" style="position: absolute; right: 12px; top: 235px; z-index: 999999;">
        <input type="button" value="查询" onclick="QueryItem();" style="height: 45px; width: 45px;
            border-radius: 3px; background-color: White" />
    </div>
    <div id="eventoutput" style="position: absolute; right: 1px; bottom: 0px; z-index: 999999;
        display: none">
    </div>
    <div id="DBGRID" style="position: absolute; right: 1px; top: 280px; width: 640px;
        height: 400px; background-color: #EEEEEE; z-index: 999999; overflow-y: auto;
        overflow-x: auto; display: none;">
    </div>
    <input type="hidden" id="ORG_ID" name="ORG_ID" value="<%=UserInfo.ORG_ID %>" />
</body>
</html>
