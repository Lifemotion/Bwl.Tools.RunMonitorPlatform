Imports Bwl.Network.ClientServer

Public Class RemoteCmdTask
    Inherits CommonTask
    Public Property RemoteCmdProcess As CmdlineServer
    Public ReadOnly Property Parameters As ProcessTaskParameters

    Sub New(shortname As String, parameters As ProcessTaskParameters, additionalChecks As IEnumerable(Of ITaskCheck), port As Integer, beaconName As String)
        MyBase.New("RemCmdProcess_" + shortname, shortname)
        RemoteCmdProcess = New CmdlineServer(port, parameters.ExecutableFileName, parameters.Arguments, parameters.WorkingDirectory, beaconName)
        AddNew(additionalChecks)
    End Sub

    Sub New(shortname As String, parameters As ProcessTaskParameters, additionalChecks As IEnumerable(Of ITaskCheck), trasnport As IMessageTransport, prefix As String)
        MyBase.New("RemCmdProcess_" + shortname, shortname)
        RemoteCmdProcess = New CmdlineServer(trasnport, prefix, parameters.ExecutableFileName, parameters.Arguments, parameters.WorkingDirectory)
        AddNew(additionalChecks)
    End Sub

    Private Sub AddNew(additionalChecks As IEnumerable(Of ITaskCheck))
        Checks.Add(New RemoteCmdCheck(Me, True, True, 0))
        Checks.AddRange(additionalChecks)
        FaultActions.Add(New RemoteCmdRestartAction(Me, 3))
    End Sub

    Public Function GetProcesses() As Process()
        Dim prcs = Process.GetProcessesByName(Parameters.ProcessName)
        Return prcs
    End Function
End Class
