Imports Bwl.Framework
Imports Bwl.Network.ClientServerMessaging
Imports Bwl.Tools.RunMonitorPlatform

Public Module App
    Dim _app As New AppBase
    Dim _core As New RunMonitorCore(_app.RootLogger)
    Dim _form As New RunMonitorStatus(_app.RootLogger)
    Sub Main()
        Application.EnableVisualStyles()
        _core.Tasks.Add(New ProcessTask(New ProcessTaskParameters() With {.ProcessName = "Calculator", .ExecutableFileName = "calc.exe"}))
        _core.Tasks.Add(New ProcessTask(New ProcessTaskParameters() With {.ProcessName = "Bwl.Tools.RunMonitor.TestApp", .ExecutableFileName = "..\..\TestApp\bin\Bwl.Tools.RunMonitor.TestApp.exe"}))
        _core.Tasks(1).Checks.Add(New NetClientCheck("localhost", 5654))
        _core.Tasks.Add(New MemWatcherTask(15000))
        _form.Show()
        _form.Tasks = _core.Tasks.ToArray
        _core.RunInThread()
        Application.Run()
    End Sub
End Module
