Public Structure ProcessTaskParameters
    Public ProcessName As String
    Public ExecutableFileName As String
    Public WorkingDirectory As String
    Public Arguments As String

    Public Sub New(processName As String, executableFileName As String, workingDirectory As String, arguments As String)
        Me.ProcessName = processName
        Me.ExecutableFileName = executableFileName
        Me.WorkingDirectory = workingDirectory
        Me.Arguments = arguments
    End Sub

    Public Sub New(processName As String, executableFileName As String)
        Me.New(processName, executableFileName, "", "")
    End Sub

    Public Sub New(processName As String, executableFileName As String, arguments As String)
        Me.New(processName, executableFileName, "", "")
    End Sub
End Structure

Public Class ProcessTask
    Inherits CommonTask
    Public ReadOnly Property Parameters As ProcessTaskParameters

    Sub New(parameters As ProcessTaskParameters)
        Me.New(parameters, {})
    End Sub

    Sub New(parameters As ProcessTaskParameters, additionalChecks As IEnumerable(Of ITaskCheck))
        MyBase.New("ProcessTask_" + parameters.ProcessName)
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
