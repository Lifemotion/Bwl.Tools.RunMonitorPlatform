﻿
Public Class MemWatcherTask
    Inherits CommonTask

    Sub New(limit As Integer)
        MyBase.New("MemWatcherTask" + limit.ToString + "mb")
        Checks.Add(New FreeMemoryCheck(limit))
        FaultActions.Add(New RestartComputerAction("5", 10))
    End Sub
End Class
