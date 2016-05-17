Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Public Class TestAppForm
    Inherits FormAppBase
    Private WithEvents _server As New NetServer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _server.StartServer(5654)
        _logger.AddMessage("Started 5654")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Throw New Exception("Test")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim i = 10
        Dim j = 1
        Do While i > -10
            j = j / i
            i -= 1
        Loop
    End Sub

    Private Sub _server_ReceivedMessage(message As NetMessage, client As ConnectedClient) Handles _server.ReceivedMessage
        _logger.AddMessage("-> " + message.ToString)
        If message.Part(0) = "testrequest" Then
            client.SendMessage(New NetMessage("S", "testanswer"))
        End If
    End Sub

    Private Sub _server_SentMessage(message As NetMessage, client As ConnectedClient) Handles _server.SentMessage
        _logger.AddMessage("<- " + message.ToString)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim i = 10
        Dim j = 1
        Do While i > 0
            j = j / i
        Loop
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim list As New List(Of Bitmap)
        Do While True
            list.Add(New Bitmap(50, 50))
        Loop

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        For Each client In _server.Clients.ToArray
            Try
                client.SendMessage(New NetMessage("A", "testperiodic", Now.ToString))
            Catch ex As Exception
            End Try
        Next
    End Sub
End Class
