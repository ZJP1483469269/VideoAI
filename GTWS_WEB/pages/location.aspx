<%@ Page Language="C#" AutoEventWireup="true" CodeFile="location.aspx.cs" Inherits="pages_location" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../static/leaflet/leaflet.css" rel="stylesheet" type="text/css" />
    <script src="../static/js/jquery.js" type="text/javascript"></script>
    <script src="../static/leaflet/leaflet.js" type="text/javascript"></script>
    <script src="../static/leaflet/map.js" type="text/javascript"></script>
    <script type="text/javascript">

        //JS 获取地址参数
        function getQueryString(cKeyName) {
            var reg = new RegExp('(^|&)' + cKeyName + '=([^&]*)(&|$)', 'i');
            var r = window.location.search.substr(1).match(reg);
            if (r != null) {
                return unescape(r[2]);
            }
            return null;
        }

    
        var Lat;
        var Lon;
        $(document).ready(function () {
            Lat = getQueryString("X");
            Lon = getQueryString("Y");
            InitMap();
        });

        function InitMap() {
            map = L.map('map', {
                measureControl: true,
                layersControl: true,
                attributionControl: false,
                center: [Lat, Lon],
                zoom: 14,
                minZoom: 1,
                maxZoom: 18

            });
            LoadBasicLayer();

            var greenIcon = L.icon({
                iconUrl: "../static/leaflet/images/marker-icon.png",
                iconSize: [25, 30]// size of the icon
            });
            var wgs84togcj02 = coordtransform.wgs84togcj02(Lat, Lon);
            var vMarker = new L.Marker((wgs84togcj02),
                        { draggable: false,        // 使图标可拖拽
                            opacity: 1,   // 设置透明度
                            icon: greenIcon
                        }
                );
            vMarker.addTo(map);
            function LoadBasicLayer() {
                var baseLayers = {
                    "街道地图": L.tileLayer('http://webrd0{s}.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=8&x={x}&y={y}&z={z}', { subdomains: "1234" }).addTo(map),
                    "影像地图": L.layerGroup([
	    L.tileLayer('http://mt1.google.cn/vt/lyrs=s&hl=zh-CN&gl=CN&x={x}&y={y}&z={z}&s=Gali'),
	    L.tileLayer('http://mt1.google.cn/vt/imgtp=png32&lyrs=h@207000000&hl=zh-CN&gl=cn&x={x}&y={y}&z={z}&s=Galil')
	])
                };
                L.control.layers(baseLayers, {}, { position: "topright" }).addTo(map);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="map" style="position: absolute; left: 1px; top: 1px; right: 1px; bottom: 1px;
        z-index: 999999; margin: 2px; padding: 0px">
    </div>
    </form>
</body>
</html>
