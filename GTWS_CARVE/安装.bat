@echo.��������......  
@echo off  
@sc create GTWS_CARVE binPath= "D:\VideoAI\GTWS_CARVE\bin\Debug\GTWS_CARVE.exe"  
@net start GTWS_CARVE  
@sc config GTWS_CARVE start= AUTO  
@echo off  
@echo.������ϣ�  
@pause