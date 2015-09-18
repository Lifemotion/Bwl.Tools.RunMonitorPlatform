Public Enum TaskState
    disabled
    ok
    warning
    fault
End Enum

Public Interface ITask
    ReadOnly Property ID As String
    Property Description As String
    Property State As TaskState
    ReadOnly Property Info As String
    Property Checks As List(Of ITaskCheck)
    Property FaultActions As List(Of IFaultAction)
    Property CheckStats As CheckStats
End Interface
