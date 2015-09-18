Public Class NetWatcherTask
    Inherits CommonTask

    Sub New()
        Me.New("https://ya.ru", True)
    End Sub

    Sub New(address As String, getOnlyHeaders As Boolean)
        MyBase.New("NetWatcherTask_" + address)
        Checks.Add(New HttpNetCheck(address, getOnlyHeaders))
        FaultActions.Add(New RestartComputerAction("30", 10))
    End Sub

End Class
