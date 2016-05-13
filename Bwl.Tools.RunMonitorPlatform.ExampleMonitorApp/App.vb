﻿Imports Bwl.Tools.RunMonitorPlatform

Public Module App
    Dim _logger As New Logger
    Dim _core As New RunMonitorCore(_logger)
    Dim _form As New RunMonitorStatus(_logger)
    Sub Main()
        Application.EnableVisualStyles()
        _core.Tasks.Add(New ProcessTask("Calculator", New ProcessTaskParameters("Calculator", "calc.exe") With {.RestartDelaySecongs = 15}))
        _core.Tasks.Add(New ProcessTask("TestApp", New ProcessTaskParameters("Bwl.Tools.RunMonitor.TestApp", "..\..\TestApp\bin\Bwl.Tools.RunMonitor.TestApp.exe"),
                {New NetClientCheck("localhost", 5654, AddressOf testAppNetCheck)}))
        _core.Tasks.Add(New MemWatcherTask(1500))
        _core.Tasks.Add(New NetWatcherTaskHttp("Internet", "http: //ya.ru", True))
        _form.Show()
        _core.GetShortStatus()
        _form.Tasks = _core.Tasks.ToArray
        _core.RunInThread()
        Application.Run(_form)
        _core.StopThread()
    End Sub

    Private Sub testAppNetCheck(check As NetClientCheck)
        Dim result = check.NetClient.SendMessageWaitAnswer(New NetMessage("S", "testrequest"), "testanswer", 5)
        If result Is Nothing Then Throw New TaskCheckException(Nothing, check, "No Net Answer!")
    End Sub
End Module
