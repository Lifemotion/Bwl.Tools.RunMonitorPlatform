Public Class NetWatcherTask
    Inherits CommonTask

    Sub New(address As String)
        MyBase.New("NetWatcherTask_" + address)
        Checks.Add(New HttpNetCheck(address))
        FaultActions.Add(New RestartComputerAction("30", 10))
    End Sub

End Class
