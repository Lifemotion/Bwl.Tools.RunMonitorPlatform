Imports Bwl.Tools.RunMonitorPlatform
Imports Bwl.Framework
Imports Bwl.Network.ClientServer

Public Module App
    Dim _logger As New Logger
    Dim _core As New RunMonitorCore(_logger)
    Dim _autoui As New RunMonitorAutoUI(_logger)
    Dim _basedir = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..")

    Sub Main()
        _autoui.Tasks = _core.Tasks.ToArray
        _core.RunInThread()
    End Sub

End Module
