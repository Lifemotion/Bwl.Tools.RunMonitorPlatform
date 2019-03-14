Imports Bwl.Network.ClientServer

Public Class MultiTransport
    Implements IMessageTransport

    Public Event ReceivedMessage As IMessageTransport.ReceivedMessageEventHandler Implements IMessageTransport.ReceivedMessage
    Public Event SentMessage As IMessageTransport.SentMessageEventHandler Implements IMessageTransport.SentMessage
    Public Event RegisterClientRequest As IMessageTransport.RegisterClientRequestEventHandler Implements IMessageTransport.RegisterClientRequest

    Private _transports As New List(Of IMessageTransport)

    Public Sub AddTransport(transport As IMessageTransport)
        _transports.Add(transport)
        AddHandler transport.ReceivedMessage, Sub(message As NetMessage)
                                                  RaiseEvent ReceivedMessage(message)
                                              End Sub
        AddHandler transport.SentMessage, Sub(message As NetMessage)
                                              RaiseEvent SentMessage(message)
                                          End Sub
        AddHandler transport.RegisterClientRequest, Sub(clientInfo As Dictionary(Of String, String), id As String, method As String, password As String, serviceName As String, options As String, ByRef allowRegister As Boolean, ByRef infoToClient As String)
                                                        RaiseEvent RegisterClientRequest(clientInfo, id, method, password, serviceName, options, allowRegister, infoToClient)
                                                    End Sub
    End Sub

    Public ReadOnly Property MyID As String Implements IMessageTransport.MyID
        Get
            Throw New NotImplementedException()
            'Return _transports.First.MyID
        End Get
    End Property

    Public ReadOnly Property MyServiceName As String Implements IMessageTransport.MyServiceName
        Get
            Throw New NotImplementedException()
            'Return _transports.First.MyServiceName
        End Get
    End Property

    Public ReadOnly Property IsConnected As Boolean Implements IMessageTransport.IsConnected
        Get
            Throw New NotImplementedException()
            'For Each transport In _transports
            'If transport.IsConnected Then Return True
            'Next
            Return False
        End Get
    End Property

    Public Property IgnoreNotConnected As Boolean Implements IMessageTransport.IgnoreNotConnected
        Get
            Throw New NotImplementedException()
            'Return _transports.First.IgnoreNotConnected
        End Get
        Set(value As Boolean)
            For Each transport In _transports
                transport.IgnoreNotConnected = value
            Next
        End Set
    End Property


    Public Sub SendMessage(message As NetMessage) Implements IMessageTransport.SendMessage
        For Each transport In _transports
            If message.FromID = transport.MyID Or message.FromID = "" Then transport.SendMessage(message)
        Next
    End Sub

    Public Sub RegisterMe(id As String, password As String, serviceName As String, options As String) Implements IMessageTransport.RegisterMe
        For Each transport In _transports
            transport.RegisterMe(id, password, serviceName, options)
        Next
    End Sub

    Public Sub Open(address As String, options As String) Implements IMessageTransport.Open
        For Each transport In _transports
            transport.Open(address, options)
        Next
    End Sub

    Public Sub Close() Implements IMessageTransport.Close
        For Each transport In _transports
            transport.Close()
        Next
    End Sub

    Public Function SendMessageWaitAnswer(message As NetMessage, answerFirstPart As String, Optional timeout As Single = 20) As NetMessage Implements IMessageTransport.SendMessageWaitAnswer
        Throw New NotImplementedException()
    End Function

    Public Function GetClientsList(serviceName As String) As String() Implements IMessageTransport.GetClientsList
        Throw New NotImplementedException()
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        'Throw New NotImplementedException()
    End Sub
End Class
