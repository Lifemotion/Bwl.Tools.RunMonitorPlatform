Imports Bwl.Network.ClientServerMessaging
Imports Bwl.Tools.RunMonitorPlatform

Public Class NetClientCheck
    Inherits CommonTaskCheck
    Private _client As New NetClient
    Private _host As String
    Private _port As Integer
    Public Sub New(host As String, port As Integer)
        MyBase.New("NetClientCheck")

        _host = host
        _port = port
    End Sub

    Public Overrides Sub Check()
        If _client.IsConnected = False Then _client.Connect(_host, _port)
        Dim result = _client.SendMessageWaitAnswer(New NetMessage("S", "testrequest"), "testanswer")
        If result Is Nothing Then Throw New TaskCheckException(Nothing, Me, "No NetClient Answer")

    End Sub
End Class
