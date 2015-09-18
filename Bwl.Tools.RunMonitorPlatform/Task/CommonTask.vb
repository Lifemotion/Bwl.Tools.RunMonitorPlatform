
Public Class CommonTask
    Implements ITask

    Private _id As String
    Private _info As String

    Public Property Checks As New List(Of ITaskCheck) Implements ITask.Checks

    Public Property Description As String Implements ITask.Description

    Public ReadOnly Property ID As String Implements ITask.ID
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property Info As String Implements ITask.Info
        Get
            Return _info
        End Get
    End Property

    Public Sub New(id As String)
        _id = id
    End Sub

    Public Property State As TaskState = TaskState.warning Implements ITask.State

    Public Property FaultActions As New List(Of IFaultAction) Implements ITask.FaultActions

    Public Property CheckStats As New CheckStats Implements ITask.CheckStats

End Class
