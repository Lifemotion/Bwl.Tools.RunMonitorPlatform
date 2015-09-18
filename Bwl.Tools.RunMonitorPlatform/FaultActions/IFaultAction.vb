
Public Interface IFaultAction
    ReadOnly Property Name As String
    ReadOnly Property Info As String
    ReadOnly Property LastAttempt As LastCall
    Property DelayBeforeActionSeconds As Single
    Property FaultsToRun As Integer
    Sub Run()
End Interface