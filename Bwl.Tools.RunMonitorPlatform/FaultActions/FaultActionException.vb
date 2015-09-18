Public Class FaultActionException
    Inherits Exception
    Public Property MainMessage As String = ""

    Sub New(task As ITask, action As IFaultAction, msg As String)
        MyBase.New("FaultAction Failed Exception, Task: " + If(task IsNot Nothing, task.ID, "-") + ", FaultAction: " + action.Name + ", " + msg)
        MainMessage = msg
    End Sub
End Class
