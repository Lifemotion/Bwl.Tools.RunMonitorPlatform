Public Structure ProcessTaskParameters
    Public ProcessName As String

    Public ExecutableFileName As String

    Public WorkingDirectory As String

    Public Arguments As String
End Structure

Public Class ProcessTask
    Inherits CommonTask
    Public ReadOnly Property Parameters As ProcessTaskParameters

    Sub New(parameters As ProcessTaskParameters)
        MyBase.New("ProcessTask_" + parameters.ProcessName)
        _Parameters = parameters
        Checks.Add(New ProcessCheck(Me, True, True, 0))
        FaultActions.Add(New ProcessRestartAction(Me, 3))
    End Sub

    Public Function GetProcesses() As Process()
        Dim prcs = Process.GetProcessesByName(Parameters.ProcessName)
        Dim prcs2 = Process.GetProcesses
        Return prcs
    End Function
End Class
