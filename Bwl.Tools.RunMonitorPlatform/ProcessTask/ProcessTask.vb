Public Structure ProcessTaskParameters
    Public IdAppendix As String
    Public ProcessName As String
    Public ExecutableFileName As String
    Public WorkingDirectory As String
    Public Arguments As String
    Public RestartDelaySecongs As Integer

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
    Public ReadOnly Property Parameters As ProcessTaskParameters

    Sub New(shortname As String, parameters As ProcessTaskParameters)
        Me.New(shortname, parameters, {})
    End Sub

    Sub New(shortname As String, parameters As ProcessTaskParameters, additionalChecks As IEnumerable(Of ITaskCheck))
        MyBase.New("ProcessTask_" + parameters.ProcessName + parameters.IdAppendix, shortname)
        _Parameters = parameters
        Checks.Add(New ProcessCheck(Me, True, True, 0))
        Checks.AddRange(additionalChecks)
        FaultActions.Add(New ProcessRestartAction(Me, 3))
    End Sub

    Public Function GetProcesses() As Process()
        Dim prcs = Process.GetProcessesByName(Parameters.ProcessName)
        Dim prcs2 = Process.GetProcesses
        Return prcs
    End Function
End Class
