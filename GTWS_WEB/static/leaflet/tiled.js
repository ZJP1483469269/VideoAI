L.TileLayer.ArcGIS = L.TileLayer.extend({
    options: {
        continuousWorld: !0,
        tileSize: 256,
        tileOrigin: null,
        resolutions: [],
        maxExtent: null
    },
    _adjustTilePoint: function () { },
    _tileShouldBeLoaded: function (k) {
        return k.x + ":" + k.y in this._tiles ? !1 : this._IsInMapExtent(this.getTileBound(k))
    },
    _IsInMapExtent: function (k) {
        var f = this.options.maxExtent;
        return null == f || f.contains(k) || f.intersects(k) ? !0 : !1
    },
    getTileBound: function (k) {
        var f = this._map,
        i = this._level.zoom,
        d = this.options.tileSize,
        b = f.options.crs,
        k = k.multiplyBy(d),
        d = k.add(new L.Point(d, d)),
        k = b.project(f.unproject(k, i)),
        f = b.project(f.unproject(d, i));
        return new L.Bounds(k, f)
    },
    getTileUrl: function (k, f) {
        var f = this._level.zoom,
        i = this.getTileBound(k).getCenter(!1),
        d = Math.floor((i.x - this.options.tileOrigin.x) / (this.options.tileSize * this.options.resolutions[f]));
        return this._url + (10 > f ? "L0" + f + "/" : "L" + f + "/") + "R" + this.zeroPad(Math.floor((this.options.tileOrigin.y - i.y) / (this.options.tileSize * this.options.resolutions[f])), 8, 16) + "/C" + this.zeroPad(d, 8, 16) + "." + this.options.format
    },
    zeroPad: function (k, f, i) {
        for (k = k.toString(i || 10); k.length < f; ) k = "0" + k;
        return k
    }
});