Imports Bwl.Network.ClientServer

Public Structure ProcessTaskParameters
    Public IdAppendix As String
    Public ProcessName As String
    Public ExecutableFileName As String
    Public WorkingDirectory As String
    Public Arguments As String
    Public RestartDelaySecongs As Integer
    Public RedirectInputOutput As Boolean

    Public Sub New(processName As String, executableFileName As String, workingDirectory As String, arguments As String, restartDelaySecongs As Integer)
        Me.ProcessName = processName
        Me.ExecutableFileName = executableFileName
        Me.WorkingDirectory = workingDirectory
        Me.Arguments = arguments
        Me.RestartDelaySecongs = restartDelaySecongs
    End Sub

    Public Sub New(processName As String, executableFileName As String)
        Me.New(processName, executableFileName, "", "", 1)
    End Sub

    Public Sub New(processName As String, executableFileName As String, arguments As String)
        Me.New(processName, executableFileName, "", "", 1)
    End Sub
End Structure

Public Class ProcessTask
    Inherits CommonTask
    private _restartAction as ProcessRestartAction

    Public Property Parameters As ProcessTaskParameters
    'Public ReadOnly Property Process As Process
    'Public ReadOnly Property CmdRemoting As CmdlineServer
    Public Property Transport As IMessageTransport
    Public ReadOnly Property RestartAction As ProcessRestartAction
        Get
            Return _restartAction
        End Get
    End Property

    Friend Sub SetProcess(process As Process, cmdlineServer As CmdlineServer)
       ' _Process = process
       ' _CmdRemoting = cmdlineServer
    End Sub

    Friend Sub SetProcessName(processName As String)
        Dim params = Parameters
        params.ProcessName = processName
        Parameters = params
    End Sub

    Sub New(shortname As String, parameters As ProcessTaskParameters)
        Me.New(shortname, parameters, {})
    End Sub

    Sub New(shortname As String, parameters As ProcessTaskParameters, additionalChecks As IEnumerable(Of ITaskCheck))
        MyBase.New("ProcessTask_" + If(parameters.ProcessName > "", parameters.ProcessName, shortname) + parameters.IdAppendix, shortname)
        _Parameters = parameters
        Checks.Add(New ProcessCheck(Me, True, True, 0))
        Checks.AddRange(additionalChecks)
        _RestartAction = New ProcessRestartAction(Me, 3)
        FaultActions.Add(RestartAction)
    End Sub

    Public Function GetProcesses() As Process()
        Dim name = Parameters.ProcessName
        If name = "mono-sgen" Then name = "mono"
        Dim prcs = Process.GetProcessesByName(Parameters.ProcessName)
        Return prcs
    End Function

End Class
