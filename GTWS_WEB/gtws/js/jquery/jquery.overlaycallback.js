/*! Copyright (c) huawei
 * Licensed under the MIT License (LICENSE.txt).
 *
 * 解决遮罩层不能覆盖ocx控件的问题
 * Version 1.0.0.0
 */
(function($){

	$.fn.overlaycallback = {
		/**
		 * 创建遮罩层后执行此方法
		 */
	    createdOverlay:function(){
	    },
	    /**
		 * 销毁遮罩层后执行此方法
		 */
	    destroyOverlay:function(){
	    }
	};

})(jQuery);