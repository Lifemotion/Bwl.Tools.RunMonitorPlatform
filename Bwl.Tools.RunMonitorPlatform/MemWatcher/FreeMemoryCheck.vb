Public Class FreeMemoryCheck
    Inherits CommonTaskCheck
    Private _limit As Integer

    Public Sub New(memoryLimitMb As Integer)
        MyBase.New("FreeMemoryCheck" + memoryLimitMb.ToString + "mb")
        _limit = memoryLimitMb
    End Sub

    Public Overrides Sub Check()
        Dim mem = 0UL
        mem = Hardware.GetFreeMemoryInfo
        _lastCheck.Message = ("ОЗУ свободно: " + mem.ToString + " MB.")
        If mem < _limit Then Throw New Exception("Слишком маленький объем свободной ОЗУ, " + mem.ToString + " < " + _limit.ToString)
    End Sub
End Class
