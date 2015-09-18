Public Interface ITaskCheck
    Property Task As ITask
    ReadOnly Property Name As String
    ReadOnly Property LastCheck As LastCall
    ReadOnly Property CheckIntervalSeconds As Integer
    ReadOnly Property Info As String
    Sub Check()
End Interface

