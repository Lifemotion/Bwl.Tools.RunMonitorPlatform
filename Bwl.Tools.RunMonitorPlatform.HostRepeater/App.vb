Imports System.Threading
Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Module App

    Private Class ClientInfo
        Public Name As String
        Public Address As String
        Public DtRegisterUtc As Long
    End Class

    Private ReadOnly AppBase As New AppBase
    Private ReadOnly Transport As New MultiTransport
    Private ReadOnly ConWriter As New ConsoleLogWriter 'With {.WriteExtended = True}

    Private ReadOnly Clients As New Dictionary(Of String, ClientInfo)
    Private ReadOnly SyncObj As New Object

    Sub Main(args() As String)

        AppBase.RootLogger.ConnectWriter(ConWriter)
        AppBase.RootLogger.AddMessage("Bwl RunMonitor Host Repeater")
        ' ReSharper disable once LocalizableElement
        Console.Title = "Bwl RunMonitor Host Repeater"
        Dim info = GetHostInfo.Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        For Each line In info
            AppBase.RootLogger.AddMessage(line)
        Next
        Console.WriteLine()

        Dim port = 8032
        For Each arg In args

            Dim argParts = arg.Split("=")
            If argParts(0) = "port" AndAlso argParts.Length = 2 Then
                Try
                    port = Integer.Parse(argParts(1))
                Catch ex As Exception
                    AppBase.RootLogger.AddError("Could not set port: " + arg)
                End Try
            End If
        Next

        If port > 0 Then
            Dim server = New NetServer(port)
            server.RegisterMe("LocalServer", "", "HostRepeater", "")
            Transport.AddTransport(server)
            AppBase.RootLogger.AddMessage("Repeater Server created, port: " + port.ToString())
        Else
            Exit Sub
        End If

        AddHandler Transport.ReceivedMessage, Sub(message As NetMessage)
                                                  ProcessMessage(Transport, message)
                                              End Sub
        AddHandler Transport.RegisterClientRequest, Sub(clientInfo As Dictionary(Of String, String), id As String, method As String, password As String, serviceName As String, options As String, ByRef allowRegister As Boolean, ByRef infoToClient As String)
                                                        allowRegister = True
                                                    End Sub

        Do
            SyncLock SyncObj
                Dim oldClients = Clients.ToList().Where(Function(f) (Now.ToUniversalTime() - New DateTime(f.Value.DtRegisterUtc)).TotalSeconds > 60).Select(Function(f) f.Key).ToArray()
                If oldClients.Any() Then
                    For Each oldClient As String In oldClients
                        Clients.Remove(oldClient)
                    Next
                End If
            End SyncLock
            Thread.Sleep(10000)
        Loop

        ' ReSharper disable once FunctionNeverReturns
    End Sub

    Private Function GetHostInfo() As String
        Dim info = ""
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim fvi = FileVersionInfo.GetVersionInfo(assembly.Location)
        info += "RunMonitorHostRepeaterVersion = " + fvi.FileVersion + vbCrLf
        info += "MachineName = " + Environment.MachineName + vbCrLf
        info += "UserName = " + Environment.UserName + vbCrLf
        info += "OSVersion = " + Environment.OSVersion.VersionString + vbCrLf
        info += "CurrentDirectory = " + Environment.CurrentDirectory + vbCrLf
        info += "Time = " + Now.ToString + vbCrLf
        info += "TimeUtc = " + Now.ToUniversalTime.ToString + vbCrLf
        Return info
    End Function

    ' ReSharper disable once ParameterHidesMember
    Private Sub ProcessMessage(transport As IMessageTransport, message As NetMessage)

        Try
            Dim logMessage As String = message.AsString
            AppBase.RootLogger.AddDebug("_transportMessageReceived: " + logMessage)
            If message.Part(0) = "RunMonitorRepeater" Then
                Dim msg As NetMessage = Nothing
                Dim operation = message.Part(1)
                Select Case operation
                    Case "Register"
                        Dim name = "HostControl_" + message.Part(2)
                        Dim address = message.Part(3)
                        Try
                            SyncLock SyncObj
                                If Clients.ContainsKey(name) AndAlso Clients(name).Address <> address Then
                                    Throw New Exception("Already registered")
                                Else
                                    Dim inf = New ClientInfo With {
                                        .Name = name,
                                        .Address = address,
                                        .DtRegisterUtc = Now.ToUniversalTime().Ticks
                                    }
                                    Clients(name) = inf
                                    msg = New NetMessage(message, "RunMonitorRepeater-Success")
                                End If
                            End SyncLock
                        Catch ex As Exception
                            msg = New NetMessage(message, "RunMonitorRepeater-Fail")
                            AppBase.RootLogger.AddError(ex.ToString())
                        End Try
                    Case "Clients"
                        Dim clientsList = New String() {}
                        Try
                            SyncLock SyncObj
                                clientsList = Clients.ToList().Select(Function(f) f.Value.Address + " " + f.Value.Name).ToArray()
                            End SyncLock
                        Catch ex As Exception
                            AppBase.RootLogger.AddError(ex.ToString())
                        End Try
                        msg = New NetMessage(message, "RunMonitorRepeater-Clients")
                        msg.Part(1) = clientsList.Count.ToString()
                        For i = 2 To 1 + clientsList.Count()
                            msg.Part(i) = clientsList(i - 2).Replace(":", ";")
                        Next
                    Case "Ping"
                        msg = New NetMessage(message, "RunMonitorRepeater-Pong", GetHostInfo)
                    Case "HostInfo"
                        msg = New NetMessage(message, "RunMonitorRepeater-HostInfo", GetHostInfo)
                    Case "FastShell"
                        Dim cmd = message.Part(2)
                        Dim args = message.Part(3)
                        Dim workdir = message.Part(4)
                        Try
                            Dim prc As New Process
                            prc.StartInfo.FileName = cmd
                            prc.StartInfo.Arguments = args
                            prc.StartInfo.WorkingDirectory = workdir
                            prc.Start()
                            msg = New NetMessage(message, "RunMonitorRepeater-FastShell", "OK")
                        Catch ex As Exception
                            msg = New NetMessage(message, "RunMonitorRepeater-FastShell", "Error", ex.Message)
                        End Try
                End Select
                If msg IsNot Nothing Then transport.SendMessage(msg)
            End If
        Catch ex As Exception
            AppBase.RootLogger.AddError("_transportMessageReceivedError:" + ex.ToString())
        End Try
    End Sub


End Module
