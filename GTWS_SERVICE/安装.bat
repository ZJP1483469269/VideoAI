@echo.��������......  
@echo off  
@sc create GTWS_SERVICE binPath= "D:\VideoAI\GTWS\GTWS_SERVICE.exe"  
@net start GTWS_SERVICE  
@sc config GTWS_SERVICE start= AUTO  
@echo off  
@echo.������ϣ�  
@pause