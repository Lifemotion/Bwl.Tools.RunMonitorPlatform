Imports Bwl.Tools.RunMonitorPlatform
Imports Bwl.Framework
Imports Bwl.Network.ClientServer

Public Module App
    Dim _logger As New Logger
    Dim _core As New RunMonitorCore(_logger)
    Dim _autoui As New RunMonitorAutoUI(_logger)
    Dim _basedir = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..")

    Sub Main()
        Application.EnableVisualStyles()
        WriteHelp()
        LoadTasks()
        _autoui.Tasks = _core.Tasks.ToArray
        _core.RunInThread()
        Application.Run(_autoui.CreateForm)
        _core.StopThread()
    End Sub

    Private Sub WriteHelp()
        Dim file = IO.File.CreateText(IO.Path.Combine(_basedir, "Help.txt"))
        file.WriteLine("Each Task is *.txt file")
        file.WriteLine("First line - task type, other - pairs of argument=value")
        file.WriteLine()
        file.WriteLine("ProcessTask")
        file.WriteLine("ExecutableFileName = calc.exe")
        file.WriteLine("Arguments = ")
        file.WriteLine("ProcessName = Calculator")
        file.WriteLine("RestartDelaySecongs = 15")
        file.WriteLine("WorkingDirectory = ")
        file.WriteLine("NetClientCheck = host = port")
        file.WriteLine("HttpRequestCheck = httpUrl = goodWords = badWords = mustChange")
        file.WriteLine()
        file.WriteLine("MemWatcherTask")
        file.WriteLine("Limit = 1500")
        file.WriteLine()
        file.WriteLine("NetWatcherTask")
        file.WriteLine("Address = https://ya.ru")
        file.WriteLine("GetOnlyHeaders = True")
        file.Close()
    End Sub

    Private Sub LoadTasks()
        Dim taskFiles = IO.Directory.GetFiles(_basedir, "*.txt", IO.SearchOption.AllDirectories)
        For Each file In taskFiles
            Try
                Dim lines = IO.File.ReadAllLines(file, Text.Encoding.UTF8)
                If lines(0) = "ProcessTask" Then
                    Dim parameters As New ProcessTaskParameters
                    Dim addChecks As New List(Of ITaskCheck)
                    For Each line In lines
                        Dim parts = line.Split("=")
                        If parts.Length >= 2 Then
                            Dim argument = parts(0).Trim.ToLower
                            Dim value = parts(1).Trim
                            If argument = "ExecutableFileName".ToLower Then parameters.ExecutableFileName = value
                            If argument = "Arguments".ToLower Then parameters.Arguments = value
                            If argument = "ProcessName".ToLower Then parameters.ProcessName = value
                            If argument = "RestartDelaySecongs".ToLower Then parameters.RestartDelaySecongs = Val(value)
                            If argument = "WorkingDirectory".ToLower Then parameters.WorkingDirectory = value
                            If argument = "NetClientCheck".ToLower Then
                                If parts.Length >= 3 Then
                                    Dim host = parts(1).Trim
                                    Dim port = Val(parts(2))
                                    addChecks.Add(New NetClientCheck(host, port,
                                                 Sub(check As NetClientCheck)
                                                     Dim result = check.NetClient.SendMessageWaitAnswer(New NetMessage("S", "testrequest"), "testanswer", 5)
                                                     If result Is Nothing Then Throw New TaskCheckException(Nothing, check, "No Net Answer!")
                                                 End Sub))
                                End If
                            End If
                            If argument = "HttpRequestCheck".ToLower Then
                                If parts.Length >= 5 Then
                                    Dim address = parts(1).Trim
                                    Dim goodWords = (parts(2)).Trim
                                    Dim badWords = (parts(3)).Trim
                                    Dim mustChange = CType(parts(4).Trim, Boolean)

                                    addChecks.Add(New HttpRequestCheck(address, goodWords, badWords, mustChange))
                                End If
                            End If
                        End If
                    Next
                    Dim task = New ProcessTask(parameters.ProcessName, parameters, addChecks)
                    _core.Tasks.Add(task)
                End If

                If lines(0) = "MemWatcherTask" Then
                    Dim limit = 1500
                    For Each line In lines
                        Dim parts = line.Split("=")
                        If parts.Length = 2 Then
                            Dim argument = parts(0).Trim.ToLower
                            Dim value = parts(1).Trim
                            If argument = "Limit".ToLower Then limit = Val(value)
                        End If
                    Next
                    Dim task = New MemWatcherTask(limit)
                    _core.Tasks.Add(task)
                End If

                If lines(0) = "NetWatcherTask" Then
                    Dim address = "https://ya.ru"
                    Dim getOnlyHeaders = True

                    For Each line In lines
                        Dim parts = line.Split("=")
                        If parts.Length = 2 Then
                            Dim argument = parts(0).Trim.ToLower
                            Dim value = parts(1).Trim
                            If argument = "Address".ToLower Then address = value
                            If argument = "GetOnlyHeaders".ToLower Then getOnlyHeaders = CType(value.Trim, Boolean)
                        End If
                    Next

                    Dim task = New NetWatcherTaskHttp("Internet", address, getOnlyHeaders)
                    _core.Tasks.Add(task)
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

End Module
