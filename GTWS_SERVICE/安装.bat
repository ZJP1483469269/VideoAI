@echo.服务启动......  
@echo off  
@sc create GTWS_SERVICE binPath= "D:\VideoAI\GTWS\GTWS_SERVICE.exe"  
@net start GTWS_SERVICE  
@sc config GTWS_SERVICE start= AUTO  
@echo off  
@echo.启动完毕！  
@pause