copy debug\RunMonitorHostControl\bin\*.* ..\host-control-service\bin /Y
cd ..\host-control-service
git add *
git commit -m "Bin update"
git push
pause