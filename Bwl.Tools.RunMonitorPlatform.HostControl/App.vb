Imports System.IO
Imports System.Security.Cryptography
Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Module App
    Private _appBase As New AppBase(True, "RunMonitor", True)
    Private _transport As New MultiTransport
    Private _core As New RunMonitorCore(_appBase.RootLogger)
#If Not NETCOREAPP Then
    Private _ui As New RunMonitorAutoUI(_appBase.RootLogger)
#End If
    Private _conWriter As New ConsoleLogWriter 'With {.WriteExtended = True}

    Private _localPort As Integer

    Sub Main(args() As String)

        _core.AutomaticEnableTasks = False
        _appBase.RootLogger.ConnectWriter(_conWriter)
        _appBase.RootLogger.AddMessage("Bwl RunMonitor Host Control")
        Console.Title = "Bwl RunMonitor Host Control"
        Dim info = GetHostInfo.Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        For Each line In info
            _appBase.RootLogger.AddMessage(line)
        Next
        Console.WriteLine()
        Dim ok As Boolean
        For Each arg In args
            Dim argParts = arg.Split("=")
            If argParts(0) = "localserver" AndAlso (argParts.Length = 3 Or argParts.Length = 2) Then
                _localPort = argParts(1)
                Dim netserver = New NetServer(_localPort)
                netserver.RegisterMe("LocalServer", "", "HostControl", "")
                _transport.AddTransport(netserver)
                Dim beacon As New NetBeacon(argParts(1), "HostControl_" + System.Environment.MachineName, False, True)
                _appBase.RootLogger.AddMessage("Local Server created: " + arg)
                ok = True
            End If
            If argParts(0) = "repeater" AndAlso argParts.Length = 3 Then
                Dim repeater = New MessageTransport(_appBase.RootStorage, _appBase.RootLogger, "NetClient", argParts(1), argParts(2), "", "HostControl", True)
                repeater.AddressSetting.Value = argParts(1)
                repeater.UserSetting.Value = argParts(2)
                _transport.AddTransport(repeater)
                _appBase.RootLogger.AddMessage("Repeater connection created: " + arg)
                ok = True
            End If

#If Not NETCOREAPP Then
            If argParts(0) = "remoteapp" Then
                _ui.Tasks = _core.Tasks.ToArray
                Dim remoteapp As New RemoteAppServer(_transport, "RunMonitorHost", _appBase.RootStorage, _appBase.RootLogger, _ui.UI)
                _appBase.RootLogger.AddMessage("RemoteUI created: " + arg)
            End If
#End If

        Next

        AddHandler _transport.ReceivedMessage, Sub(message As NetMessage)
                                                   ProcessMessage(_transport, message)
                                               End Sub
        AddHandler _transport.RegisterClientRequest, Sub(clientInfo As Dictionary(Of String, String), id As String, method As String, password As String, serviceName As String, options As String, ByRef allowRegister As Boolean, ByRef infoToClient As String)
                                                         allowRegister = True
                                                     End Sub

        If ok = True Then
            CreateSavedTasks()

            Do
                Threading.Thread.Sleep(3000)
                _core.SingleCheck()
            Loop
        Else
            _appBase.RootLogger.AddError("Usage: localserver=<port> or repeater=<host:port>=<username>")
        End If

    End Sub

    Private Function GetShortHostInfo() As String
        Dim info = ""
        Dim assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim fvi = FileVersionInfo.GetVersionInfo(assembly.Location)
        info += System.Environment.MachineName + ", "
        info += fvi.FileVersion + ", "
        info += Environment.OSVersion.Platform.ToString + ", "
        info += Now.ToShortTimeString + ", "
        info += "Tasks " + _core.Tasks.Count.ToString
        Return info

    End Function

    Private Function GetHostInfo() As String
        Dim info = ""
        Dim assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim fvi = FileVersionInfo.GetVersionInfo(assembly.Location)
        info += "RunMonitorHostControlVersion = " + fvi.FileVersion + vbCrLf
        info += "MachineName = " + System.Environment.MachineName + vbCrLf
        info += "UserName = " + System.Environment.UserName + vbCrLf
        info += "OSVersion = " + System.Environment.OSVersion.VersionString + vbCrLf
        info += "CurrentDirectory = " + System.Environment.CurrentDirectory + vbCrLf
        info += "Time = " + Now.ToString + vbCrLf
        info += "TimeUtc = " + Now.ToUniversalTime.ToString + vbCrLf
        Return info
    End Function

    Private Sub ProcessMessage(transport As IMessageTransport, message As NetMessage)
        Try
            If message.ToID > "" And message.FromID > "" Then
                Dim logMessage As String = message.AsString
                _appBase.RootLogger.AddDebug("_transportMessageReceived: " + logMessage)
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
                                        tasktxt += "processstate##=" + prctask.ProcessState + "#||"
                                        tasktxt += "restartdelay##=" + prctask.Parameters.RestartDelaySecongs.ToString + "#||"
                                    End If
                                    msg.Part(msg.Count) = tasktxt
                                Next
                                transport.SendMessage(msg)
                            Case "Ping"
                                Dim msg As New NetMessage(message, "RunMonitorControl-Pong", GetShortHostInfo)
                                transport.SendMessage(msg)
                            Case "HostInfo"
                                Dim msg As New NetMessage(message, "RunMonitorControl-HostInfo", GetHostInfo)
                                transport.SendMessage(msg)
                            Case "DeleteTask"
                                Dim taskIdWithPrefix = message.Part(2)
                                If taskIdWithPrefix.StartsWith("ProcessTask_") Then
                                    Dim taskName = taskIdWithPrefix.Replace("ProcessTask_", "")
                                    For Each task In _core.Tasks.ToArray
                                        If task.ID.ToLower = taskIdWithPrefix.ToLower Then
                                            Try
                                                CType(task, ProcessTask).RestartAction.KillAllProcesses()
                                            Catch ex As Exception
                                            End Try
                                            _core.Tasks.Remove(task)
                                            DeleteTask(task)
                                            Try
                                                Dim taskPath = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "processes", task.ShortName)
                                                If IO.Directory.Exists(taskPath) = True Then IO.Directory.Delete(taskPath, True)
                                            Catch ex As Exception
                                            End Try
                                            transport.SendMessage(New NetMessage(message, "RunMonitorControl-DeleteTask", "OK"))
                                            Return
                                        End If
                                    Next
                                    transport.SendMessage(New NetMessage(message, "RunMonitorControl-DeleteTask", "Error", "NotFound"))
                                End If
                            Case "FastShell"
                                Dim cmd = message.Part(2)
                                Dim args = message.Part(3)
                                Dim workdir = message.Part(4)
                                Try
                                    Dim prc As New Process
                                    prc.StartInfo.FileName = cmd
                                    prc.StartInfo.Arguments = args
                                    prc.StartInfo.WorkingDirectory = workdir
                                    prc.Start()
                                    transport.SendMessage(New NetMessage(message, "RunMonitorControl-FastShell", "OK"))
                                Catch ex As Exception
                                    transport.SendMessage(New NetMessage(message, "RunMonitorControl-FastShell", "Error", ex.Message))
                                End Try
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
                                Dim taskFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "processes", taskName))
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
#If Not NETCOREAPP Then
                                    _ui.Tasks = _core.Tasks.ToArray
#End If
                                End If

                                foundTask.Transport = _transport
                                Dim response As New NetMessage(message, "RunMonitorTask-Result", "OK")

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
                                                Case "state"
                                                    If parts(1) = "Enabled" Then foundTask.State = TaskState.Warning
                                                    If parts(1) = "Warning" Then foundTask.State = TaskState.Warning
                                                    If parts(1) = "Ok" Then foundTask.State = TaskState.Ok
                                                    If parts(1) = "Fault" Then foundTask.State = TaskState.Fault
                                                    If parts(1) = "Disabled" Then foundTask.State = TaskState.Disabled
                                                Case "remotecmd" : currentParams.RedirectInputOutput = (parts(1) = "True")
                                            End Select
                                        End If
                                    Next
                                    If currentParams.WorkingDirectory = "" Then currentParams.WorkingDirectory = taskFolder
                                    foundTask.Parameters = currentParams
                                    If taskName.Contains("@") = False Then
                                        Try
                                            SaveTask(foundTask)
                                        Catch ex As Exception
                                            _appBase.RootLogger.AddError("Save task error: " + taskName + " " + ex.Message)
                                        End Try
                                    End If
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

                                If operations.Contains("hashlist") Then
                                    response = New NetMessage(message, "RunMonitorFileList", taskIdWithPrefix)
                                    Dim fileList = New List(Of KeyValuePair(Of String, String))
                                    Dim files = New DirectoryInfo(taskFolder).GetFiles("*", SearchOption.AllDirectories).Select(Function(f) f.FullName)

                                    For Each file In files
                                        Dim hash = ""
                                        Dim relpath = IO.Path.GetFullPath(file)
                                        relpath = relpath.Replace(taskFolder + IO.Path.DirectorySeparatorChar, "")
                                        relpath = relpath.Replace(taskFolder, "")
                                        Using hasher = MD5.Create()
                                            Using fileStream = IO.File.OpenRead(file)
                                                hash = BitConverter.ToString(hasher.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant()
                                            End Using
                                        End Using
                                        fileList.Add(New KeyValuePair(Of String, String)(relpath, hash))
                                    Next

                                    Dim answer = Serializer.SaveObjectToJsonString(fileList)
                                    response.Part(2) = answer
                                End If

                                If operations.Contains("start") Then
                                    foundTask.RestartAction.StartProcess()
                                End If
                                If response IsNot Nothing Then _transport.SendMessage(response)
                            Catch ex As Exception
                                _appBase.RootLogger.AddError("Net -> RunMonitorTask " + message.Part(1) + " " + message.Part(2) + " " + ex.Message)
                                Dim response As New NetMessage(message, "RunMonitorTask-Result", "Error", ex.Message)
                                _transport.SendMessage(response)
                            End Try
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _appBase.RootLogger.AddError("_transportMessageReceivedError:" + ex.ToString())
        End Try
    End Sub

    Public Sub DeleteTask(task As ProcessTask)
        Dim fname = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "data", task.ID + ".prctask")
        Try
            IO.File.Delete(fname)
        Catch ex As Exception
            _appBase.RootLogger.AddError(ex.Message)
        End Try
    End Sub

    Public Sub SaveTask(task As ProcessTask)
        If task.ShortName.StartsWith("_") Then Return
        Dim fname = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "data", task.ID + ".prctask")
        Dim tasktxt As String = ""
        tasktxt += "id##=" + task.ID + vbCrLf
        tasktxt += "runmonitored##=" + (task.State <> TaskState.Disabled).ToString + vbCrLf
        tasktxt += "autostart##=" + task.AutoStart.ToString + vbCrLf
        tasktxt += "filename##=" + task.Parameters.ExecutableFileName + vbCrLf
        tasktxt += "arguments##=" + task.Parameters.Arguments + vbCrLf
        tasktxt += "workdir##=" + task.Parameters.WorkingDirectory + vbCrLf
        tasktxt += "remotecmd##=" + task.Parameters.RedirectInputOutput.ToString + vbCrLf
        tasktxt += "processname##=" + task.Parameters.ProcessName + vbCrLf
        tasktxt += "restartdelay##=" + task.Parameters.RestartDelaySecongs.ToString + vbCrLf
        IO.File.WriteAllText(fname, tasktxt, System.Text.Encoding.UTF8)
    End Sub

    Public Sub CreateSavedTasks()
        Dim fdir = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "data")
        Dim tasks = IO.Directory.GetFiles(fdir, "*.prctask")
        Dim id As String = ""
        Dim runmonitored As Boolean
        Dim autostart As Boolean
        Dim filename As String = ""
        Dim arguments As String = ""
        Dim workdir As String = ""
        Dim remotecmd As Boolean
        Dim restartdelay As Integer
        For Each file In tasks
            Try
                Dim lines = IO.File.ReadAllText(file).Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                For Each line In lines
                    Dim parts = line.Split({"##="}, StringSplitOptions.None)
                    If parts.Length = 2 Then
                        Select Case parts(0).Trim.ToLower
                            Case "id" : id = parts(1)
                            Case "filename" : filename = parts(1)
                            Case "arguments" : arguments = parts(1)
                            Case "workdir" : workdir = parts(1)
                            Case "autostart" : autostart = (parts(1) = "True")
                            Case "runmonitored" : runmonitored = (parts(1) = "True")
                            Case "remotecmd" : remotecmd = (parts(1) = "True")
                            Case "restartdelay" : restartdelay = Val(parts(1))
                        End Select
                    End If
                Next
                If id > "" And filename > "" Then
                    Dim taskName = id.Replace("ProcessTask_", "")
                    Dim psp As New ProcessTaskParameters
                    psp.ExecutableFileName = filename
                    psp.Arguments = arguments
                    psp.WorkingDirectory = workdir
                    psp.RedirectInputOutput = remotecmd
                    psp.RestartDelaySecongs = restartdelay

                    Dim foundTask = New ProcessTask(taskName, psp) With {.State = TaskState.Disabled}
                    foundTask.AutoStart = autostart
                    foundTask.Transport = _transport
                    If runmonitored Then foundTask.State = TaskState.Warning Else foundTask.State = TaskState.Disabled
                    _appBase.RootLogger.AddMessage("Task " + taskName + " loaded sucessfully")
                    _core.Tasks.Add(foundTask)
                    If autostart Then foundTask.RestartAction.StartProcess()
                End If
            Catch ex As Exception
                _appBase.RootLogger.AddError("Task " + file + " load error " + ex.Message)
            End Try
        Next
    End Sub


End Module
