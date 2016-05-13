Public Class HttpWatcherTask
    Inherits CommonTask

    Sub New(shortname As String, address As String, goodWords As String, badWords As String, mustChange As Boolean)
        MyBase.New("HttpWatcherTask" + address, shortname)
        Checks.Add(New HttpRequestCheck(address, goodWords, badWords, mustChange))
        FaultActions.Add(New RestartComputerAction("30", 10))
    End Sub

End Class
