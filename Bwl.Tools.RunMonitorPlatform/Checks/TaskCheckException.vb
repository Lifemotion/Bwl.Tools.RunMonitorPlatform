Public Class TaskCheckException
    Inherits Exception
    Public Property MainMessage As String = ""
    Sub New(task As ITask, check As ITaskCheck, msg As String)
        MyBase.New("TaskCheck Failed Exception, Task: " + If(task IsNot Nothing, task.ID, "-") + ", TaskCheck: " + check.Name + ", " + msg)
        MainMessage = msg
    End Sub
End Class
