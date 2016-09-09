Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Module App
    Private _appBase As New AppBase
    Private _transport As IMessageTransport
    Private _core As New RunMonitorCore(_appBase.RootLogger)

    Sub Main(args() As String)
        For Each arg In args
            Dim argParts = arg.Split("=")
            If argParts(0) = "localserver" AndAlso argParts.Length = 2 Then
                _transport = New NetServer(argParts(1))
                AddHandler _transport.ReceivedMessage, Sub(message As NetMessage)
                                                           ProcessMessage(_transport, message)
                                                       End Sub
                AddHandler _transport.RegisterClientRequest, Sub(clientInfo As Dictionary(Of String, String), id As String, method As String, password As String, serviceName As String, options As String, ByRef allowRegister As Boolean, ByRef infoToClient As String)
                                                                 allowRegister = True
                                                             End Sub
            End If
            If argParts(0) = "repeater" AndAlso argParts.Length = 3 Then
                _transport = New MessageTransport(_appBase.RootStorage, _appBase.RootLogger, "NetClient", argParts(1), argParts(2), "", "HostControl" + argParts(2), True)
                AddHandler _transport.ReceivedMessage, Sub(message As NetMessage)
                                                           ProcessMessage(_transport, message)
                                                       End Sub
            End If
        Next
    End Sub

    Private Sub ProcessMessage(transport As IMessageTransport, message As NetMessage)

    End Sub

End Module
