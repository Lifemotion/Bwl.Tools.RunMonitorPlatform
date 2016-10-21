
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
        _parametersInfo = _task.Parameters.ProcessName + ", CheckRespond: " + checkProcessResponding.ToString + ", CheckMulCopies: " + checkMultiplyCopies.ToString + ", MemLimit: " + processMemoryLimit.ToString
    End Sub

    Public Overrides Sub Check()
        _lastCheck.Message = "Process check: " + _task.Parameters.ProcessName + vbCrLf
        Dim prcs = _task.GetProcesses
        _lastCheck.Message += "Processes found: " + prcs.Length.ToString + vbCrLf
        If prcs.Length = 0 Then _task.ProcessState = "Stopped, not found" : Throw New TaskCheckException(_task, Me, "Running processes not found")
        _statusInfo = "Processes: " + prcs.Count.ToString + ", "
        For Each prc In prcs
            Dim prcMemoryMb As Integer = (prc.PrivateMemorySize64 / 1024L / 1024L)
            _lastCheck.Message += "Process [" + prc.Id.ToString + "] - " + prcMemoryMb.ToString + " Mb" + vbCrLf
            _statusInfo += "[" + prc.Id.ToString + "] - " + prcMemoryMb.ToString + " Mb" + ", "

            If _checkProcessResponding Then
                If prc.Responding = False Then
                    If System.Environment.OSVersion.VersionString.Contains("Windows") Then
                        _task.ProcessState = "Stopped, not responding"
                        Throw New TaskCheckException(_task, Me, "Process not responding")
                    End If
                End If
                If prc.HasExited = True Then _task.ProcessState = "Stopped, exited" : Throw New TaskCheckException(_task, Me, "Process has exited")
            End If
            If _processMemoryLimit > 0 Then
                If prcMemoryMb > _processMemoryLimit Then _task.ProcessState = "Running, memory over limit" : Throw New TaskCheckException(_task, Me, "Memory limit " + _processMemoryLimit.ToString + " > process memory " + prcMemoryMb.ToString)
            End If
        Next

        If prcs.Length > 1 And _checkMultiplyCopies Then _task.ProcessState = "Running, >1 processes" : Throw New TaskCheckException(_task, Me, ">1 processes found")

        _task.ProcessState = "Running"
    End Sub


End Class
