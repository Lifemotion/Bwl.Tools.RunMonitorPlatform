Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Public Structure ProcessTaskParameters
    Public IdAppendix As String
    Public ProcessName As String
    Public ProcessMonoCmdline As String
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
    Public Property Transport As IMessageTransport
    Public Property ProcessState As String

    Public ReadOnly Property RestartAction As ProcessRestartAction
        Get
            Return _restartAction
        End Get
    End Property

    Friend Sub SetProcessName(processName As String)
        Dim params = Parameters
        params.ProcessName = processName
        Parameters = params
    End Sub

    Sub New(shortname As String, parameters As ProcessTaskParameters)
        Me.New(shortname, parameters, {})
    End Sub

    Sub New(shortname As String, parameters As ProcessTaskParameters, additionalChecks As IEnumerable(Of ITaskCheck))
        MyBase.New("ProcessTask_" + shortname, shortname)
        _Parameters = parameters
        Checks.Add(New ProcessCheck(Me, True, True, 0))
        Checks.AddRange(additionalChecks)
        _RestartAction = New ProcessRestartAction(Me, 3)
        FaultActions.Add(RestartAction)
    End Sub

    Public Function GetProcesses() As Process()
        Dim name = Parameters.ProcessName
        If name Is Nothing Then Return {}

        If System.Environment.OSVersion.VersionString.Contains("Unix") Then
            If name.ToLower = "mono" Or name.ToLower = "mono-sgen" Then
                Dim prcs = Process.GetProcesses
                Dim result As New List(Of Process)
                For Each prc In prcs
                    Try
                        Dim cmdline = IO.File.ReadAllText("/proc/" + prc.Id.ToString() + "/cmdline")
                        If cmdline.Contains(Parameters.ExecutableFileName) Then
                            result.Add(prc)
                        End If
                    Catch ex As Exception
                    End Try
                Next
                Return result.ToArray
            Else
                Dim prcs = Process.GetProcessesByName(Parameters.ProcessName)
                Return prcs
            End If
        Else
            Dim prcs = Process.GetProcessesByName(Parameters.ProcessName)
            Return prcs
        End If

    End Function

End Class
