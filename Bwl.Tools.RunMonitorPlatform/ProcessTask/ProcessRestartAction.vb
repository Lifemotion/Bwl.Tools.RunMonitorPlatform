Public Class ProcessRestartAction

    Inherits CommonFaultAction
    Private _task As ProcessTask

    Public Sub New(task As ProcessTask, faultsToRun As Integer)
        MyBase.New("ProcessRestartAction_" + task.ID, faultsToRun)
        DelayBeforeActionSeconds = 20
        _task = task
    End Sub

    Public Overrides Sub Run()
        _lastCall.Message = "ProcessRestartAction started" + vbCrLf

        Dim prcs = _task.GetProcesses

        If prcs.Length > 0 Then
            _info = "Running processes found and will be killed" : _lastCall.Message += _info + vbCrLf
            For Each prc In prcs
                Dim success As Boolean = True
                For i = 1 To 3
                    _info = "Process " + prc.Id.ToString + " " + prc.ProcessName + " kill attempt #" + i.ToString + "..."
                    Try
                        prc.Kill()
                    Catch ex As Exception
                    End Try
                    success = prc.HasExited
                    If Not success Then
                        Threading.Thread.Sleep(500)
                        success = prc.HasExited
                    End If

                    If success Then _info += " success"
                    _lastCall.Message += _info + vbCrLf
                    If success Then Exit For
                Next

                If Not success Then Throw New FaultActionException(_task, Me, "Failed to kill process")
            Next
        End If

        prcs = _task.GetProcesses
        If prcs.Length > 0 Then
            Threading.Thread.Sleep(500)
            prcs = _task.GetProcesses
        End If

        If prcs.Length > 0 Then
            Throw New FaultActionException(_task, Me, "Running processes found after successful killing")
        Else
            _info = "No running processes found, starting new process"
            _lastCall.Message += _info + vbCrLf
            Dim prc As New Process
            prc.StartInfo.FileName = _task.Parameters.ExecutableFileName
            prc.StartInfo.WorkingDirectory = _task.Parameters.WorkingDirectory
            prc.StartInfo.Arguments = _task.Parameters.Arguments

            Try
                prc.Start()
            Catch ex As Exception
                Throw New FaultActionException(_task, Me, "Process Start error: " + ex.Message)
            End Try
            For i = 1 To 10
                prcs = _task.GetProcesses
                If prcs.Length = 1 Then
                    _info = "Process started sucessfuly" : _lastCall.Message += _info + vbCrLf
                    Return
                Else
                    _info = "Waiting Process To Start... #" + i.ToString
                End If
                Threading.Thread.Sleep(500)
            Next

            Throw New FaultActionException(_task, Me, "Process was started, but not found after start")


        End If
    End Sub
End Class
