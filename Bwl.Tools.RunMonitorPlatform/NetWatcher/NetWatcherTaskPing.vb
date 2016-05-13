Public Class NetWatcherTaskPing
    Inherits CommonTask

    Sub New(shortname As String, addresses As IEnumerable(Of String), timeout As Integer)
        MyBase.New("NetWatcherTask_" + addresses.Count.ToString, shortname)
        Checks.Add(New PingCheck(addresses, timeout))
        FaultActions.Add(New RestartComputerAction("30", 10))
    End Sub

End Class
