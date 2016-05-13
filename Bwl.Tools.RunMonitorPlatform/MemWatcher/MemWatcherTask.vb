
Public Class MemWatcherTask
    Inherits CommonTask

    Sub New(limit As Integer)
        Me.New("Memory", limit)
    End Sub

    Sub New(shortname As String, limit As Integer)
        MyBase.New("MemWatcherTask" + limit.ToString + "mb", shortname)
        Checks.Add(New FreeMemoryCheck(limit))
        FaultActions.Add(New RestartComputerAction(5, 30))
    End Sub
End Class
