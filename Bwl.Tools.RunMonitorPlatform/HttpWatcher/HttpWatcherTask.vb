Public Class HttpWatcherTask
    Inherits CommonTask

    Sub New(address As String, goodWords As String, badWords As String, mustChange As Boolean)
        MyBase.New("HttpWatcherTask" + address)
        Checks.Add(New HttpRequestCheck(address, goodWords, badWords, mustChange))
        FaultActions.Add(New RestartComputerAction("30", 10))
    End Sub

End Class
