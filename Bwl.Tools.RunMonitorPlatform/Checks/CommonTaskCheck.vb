
Public MustInherit Class CommonTaskCheck
    Implements ITaskCheck

    Protected _info As String = ""
    Protected _lastCheck As New LastCall

    Public ReadOnly Property CheckIntervalSeconds As Integer = 3 Implements ITaskCheck.CheckIntervalSeconds

    Public ReadOnly Property Info As String Implements ITaskCheck.Info
        Get
            Return _info
        End Get
    End Property

    Public ReadOnly Property LastCheck As LastCall Implements ITaskCheck.LastCheck
        Get
            Return _lastCheck
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ITaskCheck.Name

    Public Property Task As ITask Implements ITaskCheck.Task

    Public MustOverride Sub Check() Implements ITaskCheck.Check

    Public Sub New(name As String)
        _Name = name
    End Sub
End Class
