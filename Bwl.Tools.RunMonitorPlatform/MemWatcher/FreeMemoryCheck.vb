Public Class FreeMemoryCheck
    Inherits CommonTaskCheck
    Private _limit As Integer

    Public Sub New(memoryLimitMb As Integer)
        MyBase.New("FreeMemoryCheck" + memoryLimitMb.ToString + "mb")
        _limit = memoryLimitMb
        _parametersInfo = "Limit: " + memoryLimitMb.ToString + " Mb"
    End Sub

    Public Overrides Sub Check()
        Dim mem = 0UL
        mem = Hardware.GetFreeMemoryInfo
        _statusInfo = "Free memory: " + mem.ToString + " Mb"

        _lastCheck.Message = "Free memory: " + mem.ToString + " Mb"
        If mem < _limit Then Throw New Exception("Free memory below limit, " + mem.ToString + " < " + _limit.ToString)
    End Sub
End Class
