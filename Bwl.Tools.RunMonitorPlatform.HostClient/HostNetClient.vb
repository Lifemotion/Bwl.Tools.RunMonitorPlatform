Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Public Class HostNetClient
    Public ReadOnly Property Transport As MessageTransport
    Public ReadOnly Property TargetConnected As Boolean
    Public Event HostInfoReceived(info As String())
    Public Event ShortHostInfoReceived(info As String)
    Public Event TaskListReceived(tasks As RemoteTaskInfo())
    Public Event SendProcessTaskResultReceived(ok As Boolean, info As String)
    Private _storage As SettingsStorage
    Private _logger As Logger
    Private _targetCheckThread As New Threading.Thread(AddressOf TargetCheck)
    Private _uploadFilterSetting As StringSetting
    Private _lastPongResponse As DateTime
    Private _remoteCmdForms As New List(Of CmdlineUi)

    Public Sub New(storage As SettingsStorage, logger As Logger)
        _storage = storage
        _uploadFilterSetting = New StringSetting(_storage, "UploadFilter", ".pdb, .bak, .vb, .cs, .log, .ini")
        _logger = logger
        _Transport = New MessageTransport(_storage, _logger,, "localhost:8064",, "HostControl", "BwlHostClient", False)
        AddHandler Transport.ReceivedMessage, AddressOf ReceivedMessageHandler
        _targetCheckThread.IsBackground = True
        _targetCheckThread.Start()
    End Sub

    Public Sub Connect()
        Transport.OpenAndRegister()
        _lastPongResponse = Now
    End Sub

    Private Sub ReceivedMessageHandler(message As NetMessage)
        Select Case message.Part(0)
            Case "RunMonitorControl-Pong"
                _lastPongResponse = Now
                RaiseEvent ShortHostInfoReceived(message.Part(1))
            Case "RunMonitorControl-HostInfo"
                Dim result = message.Part(1).Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                _lastPongResponse = Now
                RaiseEvent HostInfoReceived(result)
            Case "RunMonitorControl-TaskList"
                _lastPongResponse = Now
                Dim list As New List(Of RemoteTaskInfo)
                For i = 1 To message.Count - 1
                    Dim taskitems = message.Part(i).Split({"#||"}, StringSplitOptions.RemoveEmptyEntries)
                    Dim info As New RemoteTaskInfo
                    For Each taskitem In taskitems
                        Dim parts = taskitem.Split({"##="}, StringSplitOptions.RemoveEmptyEntries)
                        If parts.Length = 2 Then
                            Select Case parts(0)
                                Case "id" : info.ID = parts(1)
                                Case "filename" : info.Filename = parts(1)
                                Case "arguments" : info.Arguments = parts(1)
                                Case "workdir" : info.Workdir = parts(1)
                                Case "params" : info.Parameters = parts(1)
                                Case "remotecmd" : info.RemoteCmd = (parts(1) = "True")
                                Case "autostart" : info.Autostart = (parts(1) = "True")
                                Case "processstate" : info.ProcessState = parts(1)
                                Case "state" : info.RunMonitorState = parts(1)
                                Case "info"
                                    If parts(1) > "" Then
                                        info.Info = parts(1).Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                                    End If
                            End Select
                        End If
                    Next
                    list.Add(info)
                Next
                RaiseEvent TaskListReceived(list.ToArray)
            Case "RunMonitorTask-Result"
                _lastPongResponse = Now
                RaiseEvent SendProcessTaskResultReceived(message.Part(1) = "OK", message.Part(2))
        End Select
    End Sub

    Private Sub TargetCheck()
        Do
            Try
                If _Transport.IsConnected = False Then
                    _TargetConnected = False
                Else
                    Dim msg As New NetMessage("S", "RunMonitorControl", "Ping")
                    msg.ToID = _Transport.TargetSetting.Value
                    msg.FromID = _Transport.MyID
                    _Transport.SendMessage(msg)
                    _TargetConnected = (Now - _lastPongResponse).TotalSeconds < 10
                End If
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(2000)
        Loop
    End Sub

    Public Sub SendProcessTask(id As String, operations As String, filename As String, arguments As String,
                             workdir As String, params As String, autostart As Boolean, runmonitorstate As String,
                             remoteCmd As Boolean, uploadFrom As String)
        If id.StartsWith("ProcessTask_") = False Then Throw New Exception("Id must start with 'ProcessTask_'")
        Dim name = id.Replace("ProcessTask_", "")
        If name.Length < 1 Then Throw New Exception("Id must not be empty")
        For Each smb In name
            Select Case smb
                Case "a"c To "z"c, "A"c To "Z"c, "0"c To "9"c, "-", ".", "_"
                Case Else
                    Throw New Exception("Id must contains only digits, letters, <-> and <_>")
            End Select
        Next
        If filename Is Nothing OrElse filename.Length < 1 Then Throw New Exception("Filename must not be empty")
        Dim taskparams As String = ""
        taskparams += "filename=" + filename + vbCrLf
        taskparams += "arguments=" + arguments + vbCrLf
        taskparams += "workdir=" + workdir + vbCrLf
        taskparams += "remotecmd=" + remoteCmd.ToString + vbCrLf
        taskparams += "autostart=" + autostart.ToString + vbCrLf
        taskparams += "state=" + runmonitorstate + vbCrLf
        Dim msg As New NetMessage("S", "RunMonitorTask", id, operations, taskparams, params)
        msg.ToID = _Transport.TargetSetting.Value
        msg.FromID = _Transport.MyID
        If operations.Contains("upload") Then
            If IO.Directory.Exists(uploadFrom) = False Then Throw New Exception("Folder upload from not found: " + uploadFrom)
            Dim filelist As New List(Of String)
            Try
                Dim files = IO.Directory.GetFiles(uploadFrom, "*.*", IO.SearchOption.AllDirectories)
                For Each file In files
                    Dim fileGood As Boolean = True
                    For Each filterName In _uploadFilterSetting.Value.Split({","}, StringSplitOptions.RemoveEmptyEntries)
                        filterName = filterName.Trim
                        If filterName > "" Then
                            If file.Contains(filterName) Then fileGood = False
                        End If
                    Next
                    If fileGood Then
                        filelist.Add(file)
                    End If
                Next
            Catch ex As Exception
                Write("Error: " + ex.Message)
                Return
            End Try
            Dim msgIndex = 5
            Dim bytes As Long
            For Each file In filelist
                Dim relpath = IO.Path.GetFullPath(file)
                relpath = relpath.Replace(uploadFrom + IO.Path.DirectorySeparatorChar, "")
                relpath = relpath.Replace(uploadFrom, "")
                msg.Part(msgIndex) = relpath
                Dim fileBytes = IO.File.ReadAllBytes(file)
                msg.PartBytes(msgIndex + 1) = fileBytes
                bytes += fileBytes.Length
                msgIndex += 2
            Next
            _logger.AddInformation("Files to copy: " + filelist.Count.ToString + ", " + (bytes / 1024 / 1024).ToString("0.000") + " Mb")
        End If
        Dim thr As New Threading.Thread(Sub()
                                            _Transport.SendMessage(msg)

                                        End Sub)
        thr.Start()
    End Sub

    Public Function CreateShellTask() As String
        Dim id = "ProcessTask__shell"
        SendProcessTask(id, "kill set start", "@shell", "", "", "", False, False, True, "")
        Return id
    End Function

    Public Sub DeleteTask(id As String)
        Dim msg As New NetMessage("S", "RunMonitorControl", "DeleteTask", id)
        msg.ToID = _Transport.TargetSetting.Value
        msg.FromID = _Transport.MyID
        _Transport.SendMessage(msg)
    End Sub

    Public Function CreateRemoteCmdForm(id As String, title As String) As CmdlineUi
        For Each existingForm In _remoteCmdForms
            If existingForm.Tag = id Then
                existingForm.Hide()
                Return existingForm
            End If
        Next
        Dim cmdclient = New CmdlineClient(Transport, id, Transport.TargetSetting.Value)
        Dim form As New CmdlineUi(cmdclient)
        form.Tag = id
        form.Text = id.Replace("ProcessTask_", "") + " - " + title
        _remoteCmdForms.Add(form)
        AddHandler form.FormClosing, Sub()
                                         _remoteCmdForms.Remove(form)
                                     End Sub
        Return form
    End Function

    Public Sub GetHostInfo()
        Dim list As New List(Of RemoteTaskInfo)
        Dim msg As New NetMessage("S", "RunMonitorControl", "HostInfo")
        msg.ToID = _Transport.TargetSetting.Value
        msg.FromID = _Transport.MyID
        _Transport.SendMessage(msg)
    End Sub

    Public Sub GetTasks()
        Dim msg As New NetMessage("S", "RunMonitorControl", "TaskList")
        msg.ToID = _Transport.TargetSetting.Value
        msg.FromID = _Transport.MyID
        _Transport.SendMessage(msg)
    End Sub

    Public Sub FastShellCommand(cmd As String, args As String, workdir As String)
        Dim msg As New NetMessage("S", "RunMonitorControl", "FastShell", cmd, args, workdir)
        msg.ToID = _Transport.TargetSetting.Value
        msg.FromID = _Transport.MyID
        _Transport.SendMessage(msg)
    End Sub
End Class

Public Class RemoteTaskInfo
    Public Property ID As String = ""
    Public Property Filename As String = ""
    Public Property Arguments As String = ""
    Public Property Workdir As String = ""
    Public Property Parameters As String = ""
    Public Property RemoteCmd As Boolean
    Public Property Autostart As Boolean
    Public Property RunMonitorState As String = ""
    Public Property ProcessState As String = ""
    Public Property Info As String() = {}
End Class
