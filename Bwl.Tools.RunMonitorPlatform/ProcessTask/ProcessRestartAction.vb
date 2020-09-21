Imports Bwl.Network.ClientServer
Imports Bwl.Framework

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
        'Console.WriteLine("Public Sub StartProcess()")

        Dim prcs = _task.GetProcesses
        'Console.WriteLine("prcs " + prcs.Length.ToString)

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
                Dim enc As System.Text.Encoding
                Try
                    enc = System.Text.Encoding.GetEncoding(866)
                Catch ex As Exception
                    enc = System.Text.Encoding.UTF8
                End Try
                If System.Environment.OSVersion.VersionString.Contains("Unix") Then enc = System.Text.Encoding.UTF8
                _remcmd = New CmdlineServer(_task.Transport, _task.ID, filename, _task.Parameters.Arguments, workdir) With {.Encoding = enc}

                Try
                    'Console.WriteLine("Process starting")
                    _remcmd.Start()
                    If _task.Parameters.ProcessName Is Nothing OrElse _task.Parameters.ProcessName = "" Then
                        _task.SetProcessName(_remcmd.Process.ProcessName)
                    End If
                    'Console.WriteLine("ProcessName " + _remcmd.Process.ProcessName)
                Catch ex As Exception
                    Throw New FaultActionException(_task, Me, "Process (CmdlineServer) Start error: " + ex.Message)
                End Try
            Else
                If System.Environment.OSVersion.VersionString.Contains("Unix") Then
                    Dim prChmod As New Process
                    prChmod.StartInfo.FileName = "chmod"
                    prChmod.StartInfo.WorkingDirectory = workdir
                    prChmod.StartInfo.Arguments = "744 " & filename
                    Try
                        prChmod.Start()
                        Threading.Thread.Sleep(1000)
                    Catch ex As Exception
                        Throw New FaultActionException(_task, Me, "Process Start chmod error: " + ex.Message)
                    End Try
                End If

                Dim prc As New Process
                prc.StartInfo.FileName = filename
                prc.StartInfo.WorkingDirectory = workdir
                prc.StartInfo.Arguments = _task.Parameters.Arguments
                'Console.WriteLine("Process starting")
                Try
                    prc.Start()
                    If _task.Parameters.ProcessName Is Nothing OrElse _task.Parameters.ProcessName = "" Then
                        _task.SetProcessName(prc.ProcessName)
                    End If
                    'Console.WriteLine("ProcessName " + prc.ProcessName)
                Catch ex As Exception
                    Throw New FaultActionException(_task, Me, "Process Start error: " + ex.Message)
                End Try
            End If

            If System.Environment.OSVersion.Platform = PlatformID.Unix Then
                _lastCall.Message += "Process started, not checked (unix)" + vbCrLf
                Return
            Else
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
        End If
    End Sub

    Public Overrides Sub Run()
        _lastCall.Message = "ProcessRestartAction started" + vbCrLf
        KillAllProcesses()
        StartProcess()
    End Sub
End Class
