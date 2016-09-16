Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Public Class HostNetClient
    Private _storage As SettingsStorage
    Private _logger As Logger
    Private _transport As MessageTransport
    Private _targetCheckThread As New Threading.Thread(AddressOf TargetCheck)
    Private _uploadFilterSetting As StringSetting

    Private Sub TargetCheck()
        Do
            Try
                If _transport.IsConnected = False Then
                    _TargetConnected = False
                Else
                    Dim msg As New NetMessage("S", "RunMonitorControl", "Ping")
                    msg.ToID = _transport.TargetSetting.Value
                    msg.FromID = _transport.MyID
                    Dim result = _transport.SendMessageWaitAnswer(msg, "RunMonitorControl-Pong", 5)
                    If result Is Nothing Then
                        _TargetConnected = False
                    Else
                        _TargetConnected = True
                    End If
                End If
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(2000)
        Loop
    End Sub

    Public Sub New(storage As SettingsStorage, logger As Logger)
        _storage = storage
        _uploadFilterSetting = New StringSetting(_storage, "UploadFilter", ".pdb, .bak, .vb, .cs, .log, .ini")
        _logger = logger
        _transport = New MessageTransport(_storage, _logger,, "localhost:8064",, "HostControl", "BwlHostClient", False)
        _targetCheckThread.IsBackground = True
        _targetCheckThread.Start()
    End Sub

    Public ReadOnly Property Transport As MessageTransport
        Get
            Return _transport
        End Get
    End Property

    Public Sub SendProcessTask(id As String, operations As String, filename As String, arguments As String,
                             workdir As String, params As String, autostart As Boolean, runmonitored As Boolean,
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
        taskparams += "runmonitored=" + runmonitored.ToString + vbCrLf
        Dim msg As New NetMessage("S", "RunMonitorTask", id, operations, taskparams, params)
        msg.ToID = _transport.TargetSetting.Value
        msg.FromID = _transport.MyID
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
        Dim result = _transport.SendMessageWaitAnswer(msg, "RunMonitorTask-Result", 120)
        If result Is Nothing Then Throw New Exception("No response")
    End Sub

    Public ReadOnly Property TargetConnected As Boolean

    Public Function CreateShellTask() As String
        Dim id = "ProcessTask__shell"
        SendProcessTask(id, "kill set start", "@shell", "", "", "", False, False, True, "")
        Return id
    End Function

    Public Function CreateRemoteCmdForm(id As String) As CmdlineUi
        Dim cmdclient = New CmdlineClient(Transport, id, Transport.TargetSetting.Value)
        Dim form As New CmdlineUi(cmdclient)
        Return form
    End Function

    Public Function GetTasks() As RemoteTaskInfo()
        Dim list As New List(Of RemoteTaskInfo)
        Dim msg As New NetMessage("S", "RunMonitorControl", "TaskList")
        msg.ToID = _transport.TargetSetting.Value
        msg.FromID = _transport.MyID
        Dim result = _transport.SendMessageWaitAnswer(msg, "RunMonitorControl-TaskList", 20)
        If result Is Nothing Then Throw New Exception("No response")
        For i = 1 To result.Count - 1
            Dim taskitems = result.Part(i).Split({"#||"}, StringSplitOptions.RemoveEmptyEntries)
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
                        Case "runmonitored" : info.Runmonitored = (parts(1) = "True")
                        Case "state" : info.State = parts(1)
                        Case "info"
                            If parts(1) > "" Then
                                info.Info = parts(1).Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                            End If
                    End Select
                End If
            Next
            list.Add(info)
        Next
        Return list.ToArray
    End Function

End Class

Public Class RemoteTaskInfo
    Public Property ID As String = ""
    Public Property Filename As String = ""
    Public Property Arguments As String = ""
    Public Property Workdir As String = ""
    Public Property Parameters As String = ""
    Public Property RemoteCmd As Boolean
    Public Property Autostart As Boolean
    Public Property Runmonitored As Boolean
    Public Property State As String = ""
    Public Property Info As String() = {}
End Class
