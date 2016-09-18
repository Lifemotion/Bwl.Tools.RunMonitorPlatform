Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Module App
    Private _appBase As New AppBase
    Private _transport As IMessageTransport
    Private _core As New RunMonitorCore(_appBase.RootLogger)
    Private _ui As New RunMonitorAutoUI(_appBase.RootLogger)
    Private _conWriter As New ConsoleLogWriter

    Sub Main(args() As String)
        _core.AutomaticEnableTasks = False
        _appBase.RootLogger.ConnectWriter(_conWriter)
        _appBase.RootLogger.AddMessage("Bwl RunMonitor Host Control")
        _appBase.RootLogger.AddMessage("OS: " + System.Environment.OSVersion.VersionString)

        For Each arg In args
            Dim argParts = arg.Split("=")
            If argParts(0) = "localserver" AndAlso argParts.Length = 3 Then
                _transport = New NetServer(argParts(1))
                Dim beacon As New NetBeacon(argParts(1), "HostControl" + argParts(2), False, True)
                AddHandler _transport.ReceivedMessage, Sub(message As NetMessage)
                                                           ProcessMessage(_transport, message)
                                                       End Sub
                AddHandler _transport.RegisterClientRequest, Sub(clientInfo As Dictionary(Of String, String), id As String, method As String, password As String, serviceName As String, options As String, ByRef allowRegister As Boolean, ByRef infoToClient As String)
                                                                 allowRegister = True
                                                             End Sub
                _transport.RegisterMe("LocalServer", "", "HostControl", "")
            End If
            If argParts(0) = "repeater" AndAlso argParts.Length = 3 Then
                _transport = New MessageTransport(_appBase.RootStorage, _appBase.RootLogger, "NetClient", argParts(1), argParts(2), "", "HostControl", True)
                AddHandler _transport.ReceivedMessage, Sub(message As NetMessage)
                                                           ProcessMessage(_transport, message)
                                                       End Sub
            End If

            If argParts(0) = "remoteapp" Then
                _ui.Tasks = _core.Tasks.ToArray
                Dim remoteapp As New RemoteAppServer(_transport, "RunMonitorHost", _appBase.RootStorage, _appBase.RootLogger, _ui.UI)

            End If
        Next

        Do
            Threading.Thread.Sleep(3000)
            _core.SingleCheck()
        Loop
    End Sub

    Private Sub ProcessMessage(transport As IMessageTransport, message As NetMessage)
        If message.ToID > "" And message.FromID > "" Then
            If message.Part(0) = "RunMonitorControl" Then
                Dim operation = message.Part(1)
                Select Case operation
                    Case "TaskList"
                        _appBase.RootLogger.AddDebug("Net -> RunMonitorControl " + operation)
                        Dim msg As New NetMessage(message, "RunMonitorControl-TaskList")
                        For Each task In _core.Tasks
                            Dim tasktxt As String = ""
                            tasktxt += "id##=" + task.ID + "#||"
                            tasktxt += "state##=" + task.State.ToString + "#||"
                            tasktxt += "info##=" + task.Info + "#||"
                            tasktxt += "runmonitored##=" + (task.State <> TaskState.Disabled).ToString + "#||"
                            tasktxt += "autostart##=" + task.AutoStart.ToString + "#||"
                            If TypeOf task Is ProcessTask Then
                                Dim prctask As ProcessTask = task
                                tasktxt += "filename##=" + prctask.Parameters.ExecutableFileName + "#||"
                                tasktxt += "arguments##=" + prctask.Parameters.Arguments + "#||"
                                tasktxt += "workdir##=" + prctask.Parameters.WorkingDirectory + "#||"
                                tasktxt += "remotecmd##=" + prctask.Parameters.RedirectInputOutput.ToString + "#||"
                                tasktxt += "processname##=" + prctask.Parameters.ProcessName + "#||"
                                tasktxt += "restartdelay##=" + prctask.Parameters.RestartDelaySecongs.ToString + "#||"
                            End If
                            msg.Part(msg.Count) = tasktxt
                        Next
                        transport.SendMessage(msg)
                    Case "Ping"
                        Dim msg As New NetMessage(message, "RunMonitorControl-Pong")
                        transport.SendMessage(msg)
                End Select
            End If


            If message.Part(0) = "RunMonitorTask" Then
                Dim taskIdWithPrefix = message.Part(1)
                If taskIdWithPrefix.StartsWith("ProcessTask_") Then
                    Try
                        Dim taskName = taskIdWithPrefix.Replace("ProcessTask_", "")
                        Dim operations = message.Part(2)
                        Dim taskparams = message.Part(3).Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                        Dim auxparams = message.Part(4).Split({" "}, StringSplitOptions.RemoveEmptyEntries)
                        Dim taskFolder = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "processes", taskName)
                        If IO.Directory.Exists(taskFolder) = False Then IO.Directory.CreateDirectory(taskFolder)
                        _appBase.RootLogger.AddDebug("Net -> RunMonitorTask " + taskName + " " + operations)

                        Dim foundTask As ProcessTask = Nothing
                        For Each task In _core.Tasks
                            If task.ID.ToLower = taskIdWithPrefix.ToLower Then
                                foundTask = task
                            End If
                        Next
                        If foundTask Is Nothing Then
                            foundTask = New ProcessTask(taskName, New ProcessTaskParameters) With {.State = TaskState.Disabled}
                            _core.Tasks.Add(foundTask)
                            _ui.Tasks = _core.Tasks.ToArray
                        End If

                        foundTask.Transport = _transport

                        If operations.Contains("kill") Then
                            foundTask.RestartAction.KillAllProcesses()
                        End If

                        If operations.Contains("set") Then
                            Dim currentParams = foundTask.Parameters
                            For Each taskparam In taskparams
                                Dim parts = taskparam.Split("=")
                                If parts.Length = 2 Then
                                    Select Case parts(0).Trim.ToLower
                                        Case "filename" : currentParams.ExecutableFileName = parts(1).Replace("\", IO.Path.DirectorySeparatorChar)
                                        Case "arguments" : currentParams.Arguments = parts(1)
                                        Case "process" : currentParams.ProcessName = parts(1)
                                        Case "workdir" : currentParams.WorkingDirectory = parts(1).Replace("\", IO.Path.DirectorySeparatorChar)
                                        Case "autostart" : foundTask.AutoStart = (parts(1) = "True")
                                        Case "runmonitored" : If parts(1) = "True" Then foundTask.State = TaskState.Warning Else foundTask.State = TaskState.Disabled
                                        Case "remotecmd" : currentParams.RedirectInputOutput = (parts(1) = "True")
                                    End Select
                                End If
                            Next
                            If currentParams.WorkingDirectory = "" Then currentParams.WorkingDirectory = taskFolder
                            foundTask.Parameters = currentParams
                        End If

                        If operations.Contains("upload") Then
                            For i = 5 To message.Count - 2 Step 2
                                Dim filename = message.Part(i).Replace("\", IO.Path.DirectorySeparatorChar)
                                If filename > "" Then
                                    Dim bytes = message.PartBytes(i + 1)
                                    Dim fullname = IO.Path.Combine(taskFolder, filename)
                                    Dim dirname = IO.Path.GetDirectoryName(fullname)
                                    If IO.Directory.Exists(dirname) = False Then IO.Directory.CreateDirectory(dirname)
                                    IO.File.WriteAllBytes(fullname, bytes)
                                End If
                            Next
                        End If

                        If operations.Contains("start") Then
                            foundTask.RestartAction.StartProcess()
                        End If
                        Dim response As New NetMessage(message, "RunMonitorTask-Result", "OK")
                        _transport.SendMessage(response)
                    Catch ex As Exception
                        _appBase.RootLogger.AddError("Net -> RunMonitorTask " + message.Part(1) + " " + message.Part(2) + " " + ex.Message)
                        Dim response As New NetMessage(message, "RunMonitorTask-Result", "Error", ex.Message)
                        _transport.SendMessage(response)
                    End Try
                End If
            End If
        End If

    End Sub

End Module
