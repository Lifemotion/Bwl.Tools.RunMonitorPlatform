Public Class FaultActionException
    Inherits Exception
    Sub New(task As ITask, action As IFaultAction, msg As String)
        MyBase.New("FaultAction Failed Exception, Task: " + task.ID + ", TaskCheck: " + action.Name + ", " + msg)
    End Sub
End Class
