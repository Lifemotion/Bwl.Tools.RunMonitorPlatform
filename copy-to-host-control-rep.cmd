copy debug\RunMonitorHostControl\bin\*.* ..\rmhc\bin /Y
cd ..\rmhc
git add *
git commit -m "Bin update"
git push
pause