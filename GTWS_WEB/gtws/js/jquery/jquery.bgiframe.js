/*! Copyright (c) 2010 Brandon Aaron (http://brandonaaron.net)
 * Licensed under the MIT License (LICENSE.txt).
 *
 * Version 2.1.2
 */
/**
 * 2012-4-14 为了解决jquery对话框被OCX遮住的问题。对代码做了如下修改。
 * 原程序只在IE6浏览器下执行添加iframe的操作。这里删除了对浏览器的判断，在所有浏览器下都执行此操作。
 * 2012-5-22 为了兼容IE9，创建iframe的DOM元素，改为标准创建方法
 * */
(function($){

$.fn.bgiframe = (function(s) {
    s = $.extend({
        top     : 'auto', // auto == .currentStyle.borderTopWidth
        left    : 'auto', // auto == .currentStyle.borderLeftWidth
        width   : 'auto', // auto == offsetWidth
        height  : 'auto', // auto == offsetHeight
        opacity : true,
        src     : 'javascript:false;'
    }, s);

    var html = '<iframe class="bgiframe"frameborder="0"tabindex="-1"src="'+s.src+'"'+
    'style="display:block;width:100%;height:100%;position:absolute;z-index:-1;'+
        (s.opacity !== false?'filter:Alpha(Opacity=\'0\');':'')+
        'top:'+(s.top=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderTopWidth)||0)*-1)+\'px\')':prop(s.top))+';'+
        'left:'+(s.left=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderLeftWidth)||0)*-1)+\'px\')':prop(s.left))+';'+
        'width:'+(s.width=='auto'?'expression(this.parentNode.offsetWidth+\'px\')':prop(s.width))+';'+
        'height:'+(s.height=='auto'?'expression(this.parentNode.offsetHeight+\'px\')':prop(s.height))+';'+
        '"/>';
    
    return this.each(function() {
        if ( $(this).children('iframe.bgiframe').length === 0 ){
        	var iframe = null;
		    if (/msie 9\.0/i.test(navigator.userAgent)) {
		    	iframe = createIframe(s);
		    }else{
		    	iframe = document.createElement(html);
		    }
		    this.insertBefore( iframe, this.firstChild );
        }
    });
});

// old alias
$.fn.bgIframe = $.fn.bgiframe;

function createIframe(s) {
	var iframe = document.createElement("iframe");
	iframe.setAttribute("class", "bgiframe");
	iframe.setAttribute("frameborder", "0");
	iframe.setAttribute("tabindex", "-1");
	iframe.setAttribute("src", s.src);
	iframe.setAttribute("style", 'display:block;width:100%;height:100%;position:absolute;z-index:-1;'+
    (s.opacity !== false?'filter:Alpha(Opacity=\'0\');':'')+
    'top:'+(s.top=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderTopWidth)||0)*-1)+\'px\')':prop(s.top))+';'+
    'left:'+(s.left=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderLeftWidth)||0)*-1)+\'px\')':prop(s.left))+';'+
    'width:'+(s.width=='auto'?'expression(this.parentNode.offsetWidth+\'px\')':prop(s.width))+';'+
    'height:'+(s.height=='auto'?'expression(this.parentNode.offsetHeight+\'px\')':prop(s.height))+';');
	return iframe;
}

function prop(n) {
    return n && n.constructor === Number ? n + 'px' : n;
}

})(jQuery);