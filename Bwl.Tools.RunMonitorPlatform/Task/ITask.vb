Public Enum TaskState
    Disabled
    Ok
    Warning
    Fault
End Enum

Public Interface ITask
    ReadOnly Property ID As String
    Property Description As String
    Property State As TaskState
    ReadOnly Property Info As String
    Property ExternalInfo As String
    Property Checks As ChecksList
    Property FaultActions As FaultActionsList
    Property CheckStats As CheckStats
    Property ShortName As String
    Property ShortState As String
    Property AutoStart As Boolean
End Interface
