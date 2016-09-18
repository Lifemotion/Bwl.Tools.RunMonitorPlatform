Imports Bwl.Network.ClientServer

Public Class ProcessRestartAction

    Inherits CommonFaultAction
    Private _task As ProcessTask
    Private _remcmd As CmdlineServer

    Public Sub New(task As ProcessTask, faultsToRun As Integer)
        MyBase.New("ProcessRestartAction_" + task.ID, faultsToRun)
        DelayBeforeActionSeconds = 20
        _task = task
    End Sub

    Public Sub KillProcesses(prcs As Process(), method As Integer)
        If prcs.Length > 0 Then
            _lastCall.Message += "Running processes found and will be killed (method #" + method.ToString + ")" + vbCrLf
            For Each prc In prcs
                Dim success As Boolean = False
                For i = 1 To 3
                    _lastCall.Message += "Process " + prc.Id.ToString + " " + prc.ProcessName + " kill (method #" + method.ToString + ") attempt #" + i.ToString + "..."
                    ProcessTools.KillProcess(prc, method)
                    If ProcessTools.ProcessExited(prc) Then _lastCall.Message += " success" + vbCrLf : success = True : Exit For Else _lastCall.Message += " failed" + vbCrLf
                Next

            Next
        End If
    End Sub

    Public Sub KillAllProcesses()
        Try
            ProcessTools.KillWindowsErrorReporting()
        Catch ex As Exception
        End Try

        For method = 0 To 2
            Dim prcs = _task.GetProcesses
            KillProcesses(prcs, method)

            prcs = _task.GetProcesses
            If prcs.Length > 0 Then
                Threading.Thread.Sleep(500)
                prcs = _task.GetProcesses
            End If

            If prcs.Length = 0 Then
                Try
                    ProcessTools.KillWindowsErrorReporting()
                Catch ex As Exception
                End Try
                Return
            End If
        Next


        Throw New FaultActionException(_task, Me, "Fault to close processes with all methods")
    End Sub

    Public Sub StartProcess()
        Dim prcs = _task.GetProcesses

        If prcs.Length > 0 Then
            Throw New FaultActionException(_task, Me, "Running processes found before starting")
        Else
            _lastCall.Message += "No running processes found, starting new process after delay " + _task.Parameters.RestartDelaySecongs.ToString + " sec" + vbCrLf
            If _task.Parameters.RestartDelaySecongs > 0 Then
                Threading.Thread.Sleep(1000 * _task.Parameters.RestartDelaySecongs)
            End If

            Dim filename = _task.Parameters.ExecutableFileName
            Dim workdir = _task.Parameters.WorkingDirectory
            Dim fullPath = IO.Path.Combine(workdir, filename)
            If IO.File.Exists(fullPath) Then filename = fullPath

            If filename = "@shell" Then
                If System.Environment.OSVersion.VersionString.Contains("Windows") Then filename = "cmd"
                If System.Environment.OSVersion.VersionString.Contains("Unix") Then filename = "bash" : workdir = ""
            End If

            If _task.Parameters.RedirectInputOutput Then
                    If _remcmd IsNot Nothing Then _remcmd.Dispose()
                _remcmd = New CmdlineServer(_task.Transport, _task.ID, filename, _task.Parameters.Arguments, workdir)
                Try
                        _remcmd.Start()
                        _task.SetProcess(_remcmd.Process, _remcmd)
                        If _task.Parameters.ProcessName Is Nothing OrElse _task.Parameters.ProcessName = "" Then
                            _task.SetProcessName(_remcmd.Process.ProcessName)
                        End If
                    Catch ex As Exception
                        Throw New FaultActionException(_task, Me, "Process (CmdlineServer) Start error: " + ex.Message)
                    End Try
                Else
                    Dim prc As New Process
                    prc.StartInfo.FileName = filename
                prc.StartInfo.WorkingDirectory = workdir
                prc.StartInfo.Arguments = _task.Parameters.Arguments
                    Try
                        prc.Start()
                        _task.SetProcess(prc, Nothing)
                        If _task.Parameters.ProcessName Is Nothing OrElse _task.Parameters.ProcessName = "" Then
                            _task.SetProcessName(prc.ProcessName)
                        End If
                    Catch ex As Exception
                        Throw New FaultActionException(_task, Me, "Process Start error: " + ex.Message)
                    End Try
                End If

                For i = 1 To 10
                    prcs = _task.GetProcesses
                If prcs.Length > 0 Then
                    _lastCall.Message += "Process started sucessfuly" + vbCrLf
                    Return
                Else
                    _lastCall.Message += "Waiting Process To Start... #" + i.ToString + vbCrLf
                    End If
                    Threading.Thread.Sleep(500)
                Next

                Throw New FaultActionException(_task, Me, "Process was started, but not found after start")
            End If
    End Sub

    Public Overrides Sub Run()
        _lastCall.Message = "ProcessRestartAction started" + vbCrLf
        KillAllProcesses()
        StartProcess()
    End Sub
End Class
