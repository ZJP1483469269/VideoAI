@echo.服务关闭......  
@echo off  
@net stop GTWS_SERVICE  
@echo.服务删除......  
@sc delete GTWS_SERVICE
@echo off  
@echo.卸载完毕！  
@pause