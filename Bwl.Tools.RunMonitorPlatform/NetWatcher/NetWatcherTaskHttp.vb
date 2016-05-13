Public Class NetWatcherTaskHttp
    Inherits CommonTask

    Sub New()
        Me.New("Internet", "https://ya.ru", True)
    End Sub

    Sub New(shortname As String, address As String, getOnlyHeaders As Boolean)
        MyBase.New("NetWatcherTask_" + address, shortname)
        Checks.Add(New HttpNetCheck(address, getOnlyHeaders))
        FaultActions.Add(New RestartComputerAction("30", 10))
    End Sub

End Class
