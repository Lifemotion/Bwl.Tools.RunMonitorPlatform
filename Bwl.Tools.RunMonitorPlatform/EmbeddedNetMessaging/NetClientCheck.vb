Imports Bwl.Tools.RunMonitorPlatform

Public Delegate Sub NetClientCheckDelegate(check As NetClientCheck)

Public Class NetClientCheck
    Inherits CommonTaskCheck
    Protected _client As New NetClient
    Protected _host As String
    Protected _port As Integer
    Protected _delegate As NetClientCheckDelegate

    Public ReadOnly Property NetClient As NetClient
        Get
            Return _client
        End Get
    End Property

    Public Sub New(host As String, port As Integer, checkDelegate As NetClientCheckDelegate)
        MyBase.New("NetClientCheck")
        _host = host
        _port = port
        _delegate = checkDelegate
    End Sub

    Public Overrides Sub Check()
        If _client.IsConnected = False Then _client.Connect(_host, _port)
        _delegate.Invoke(Me)
    End Sub
End Class