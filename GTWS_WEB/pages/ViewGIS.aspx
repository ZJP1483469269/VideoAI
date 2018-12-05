<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewGIS.aspx.cs" Inherits="ViewGIS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
   
    <link href="/static/leaflet/leaflet.css" rel="stylesheet" type="text/css" />
     <link href="../static/leaflet/markercluster/MarkerCluster.css" rel="stylesheet" />
    <link href="../static/leaflet/markercluster/MarkerCluster.Default.css" rel="stylesheet" />
    <script type="text/javascript" src="/static/js/jquery.js"></script>
    <script type="text/javascript" src="/static/leaflet/leaflet.js"></script>
    <script type="text/javascript" src="/static/leaflet/proj4.js"></script>
    <script type="text/javascript" src="/static/leaflet/proj4leaflet.js"></script>
    <script type="text/javascript" src="/static/leaflet/tiled.js"></script>
    <script type="text/javascript" src="../static/js/XZQ_GXQ.js"></script>
    <script src="../static/leaflet/markercluster/leaflet.markercluster-src.js" type="text/javascript"></script>
    <script src="../static/js/work/Camera.js" type="text/javascript"></script>
    <style>
        body
        {
            margin: 0;
            padding: 0;
        }
        #map
        {
            position: absolute;
            top: 0;
            bottom: 0;
            right: 0;
            left: 0;
            z-index: 1000;
        }
    </style>
    <script type="text/javascript">
        var XZQDM_LIST = [];
        var XZQDM = "";
        var vPAGE_SIZE = 100;
        var vActiveCounty = "";
        var vActiveVillage = "";
        var vActiveAddr = "";
        var vMaxID = "";
        var Marker_List = [];
        $(document).ready(function () {
            LoadV2();
        });
        function LoadV2() {
            var vORG_ID = 411402;
//            if (!isEasyXZQ(vORG_ID)) {
//                return;
//            }
            var vXZQDM= vORG_ID;
            $("#map").replaceWith('<div id="map"></div>');
            var e_x1, e_y1, e_x2, e_y2;
            var xzqName = vORG_ID;
            $.ajax({
                url: "/map/" + vXZQDM + "/Layers/conf.cdi",
                async: false,
                success: function (xml) {
                    e_x1 = $(xml).find("XMin").text();
                    e_y1 = $(xml).find("YMin").text();
                    e_x2 = $(xml).find("XMax").text();
                    e_y2 = $(xml).find("YMax").text();
                },
                error: function (a, b) {
                    alert("没有找到地图配置文件，请核对！");
                    return;
                }
            });
            if (e_x1 == null) {
                return;
            }
            //原点
            var origin = new L.Point(-400, 400);
            //范围
            var maxExtent = new L.Bounds(new L.Point(e_x1, e_y1), new L.Point(e_x2, e_y2));
            //转换为国家2000坐标
            var crs = new L.Proj.CRS('EPSG:4490', '+proj=longlat +ellps=GRS80 +no_defs',
    {
        origin: [origin.x, origin.y],
        resolutions: [0.0073097042099106212, 0.0036548521049553106, 0.0018274260524776553, 0.00091371302623882765, 0.00045685651311941383, 0.00022842825655970691, 0.00011421412827985346, 5.7107064139926728e-005, 2.8553532069963364e-005, 1.4276766034981682e-005, 7.138383017490841e-006],

        //resolutions: [0.0027496601665244619, 0.0013748300832622309, 0.00068741502973381039, 0.00034370750296960017, 0.00017185373958749506, 8.5926857896442501e-005, 4.2963428948221251e-005, 2.1481559809145244e-005, 1.0740839391097767e-005, 5.3704196955488835e-006, 2.6851979504694131e-006],
        bounds: maxExtent
    });
            //中心点
            var center = maxExtent.getCenter(false);
            center = crs.unproject(center);
            center = [center.lat, center.lng];
            map = new L.Map
            ('map',
            { crs: crs,
                maxZoom: crs.options.resolutions.length - 1,
                center: center,
                zoom: 3,
                minZoom: 1,
                attributionControl: false,
                zoomControl: true,
                touchZoom: true
            });
            map.on('measurefinish', function (evt) {
                writeResults(evt);
            });
            var dituLayer = null;
            var dituLayer = new L.TileLayer.ArcGIS("/map/" + vXZQDM + "/Layers/_alllayers/", {
                minZoom: 0,
                maxZoom: crs.options.resolutions.length - 1,
                format: 'png',
                transparent: true,
                opacity: 1.0,
                tileSize: 256,
                tileOrigin: origin,
                resolutions: crs.options.resolutions,
                maxExtent: maxExtent
            });
            map.addLayer(dituLayer);
           // LoadVectorPointLayer();
           LoadVectorPolygonLayer();
        }
   
    </script>
</head>
<body>
    <div id="map">
    </div>
    <div style="position: absolute; right: 20px; top: 10px">
    </div>
    <script type="text/javascript"> 
        
    </script>
       <input type="hidden" id="ORG_ID" name="ORG_ID" value="411402" />
</body>
</html>
