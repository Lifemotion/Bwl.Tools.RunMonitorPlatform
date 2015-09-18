

Public Class ProcessCheck
    Inherits CommonTaskCheck
    Private _task As ProcessTask
    Private _checkProcessResponding As Boolean
    Private _checkMultiplyCopies As Boolean
    Private _processMemoryLimit As Integer

    Sub New(task As ProcessTask, checkProcessResponding As Boolean, checkMultiplyCopies As Boolean, processMemoryLimit As Integer)
        MyBase.New("ProcessCheck_" + task.ID + "_state-" + checkProcessResponding.ToString + "_" + processMemoryLimit.ToString + "mb")
        _task = task
        _checkProcessResponding = checkProcessResponding
        _checkMultiplyCopies = checkMultiplyCopies
        _processMemoryLimit = processMemoryLimit
    End Sub

    Public Overrides Sub Check()
        _lastCheck.Message = "Process check: " + _task.Parameters.ProcessName + vbCrLf
        Dim prcs = _task.GetProcesses
        _lastCheck.Message += "Processes found: " + prcs.Length.ToString + vbCrLf
        If prcs.Length = 0 Then Throw New TaskCheckException(_task, Me, "Running processes not found")
        If prcs.Length > 1 And _checkMultiplyCopies Then Throw New TaskCheckException(_task, Me, ">1 processes found")
        For Each prc In prcs
            Dim prcMemoryMb = (prc.PrivateMemorySize64 / 1024L / 1024L)

            If _checkProcessResponding Then
                If prc.Responding = False Or prc.HasExited = True Then Throw New TaskCheckException(_task, Me, "Process has exited or not responding")
            End If
            If _processMemoryLimit > 0 Then
                If prcMemoryMb > _processMemoryLimit Then Throw New TaskCheckException(_task, Me, "Memory limit " + _processMemoryLimit.ToString + " > process memory " + prcMemoryMb.ToString)
            End If
            _lastCheck.Message += "Process " + prc.Id.ToString + " " + prcMemoryMb.ToString + "mb" + vbCrLf
        Next
    End Sub

End Class
