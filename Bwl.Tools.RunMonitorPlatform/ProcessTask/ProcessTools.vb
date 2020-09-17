#If Not NETCOREAPP Then
Imports System.Management
#End If

Public Class ProcessTools
    Public Shared Function FindProcesses(fullPath As String, filename As String) As Process()
        Dim prcs = Process.GetProcesses()
        Dim result As New List(Of Process)
        For Each prc In prcs
            Try
                If fullPath > "" AndAlso prc.MainModule.FileName.ToLower = fullPath.ToLower Then result.Add(prc)
                If filename > "" AndAlso IO.Path.GetFileName(prc.MainModule.FileName).ToLower = filename.ToLower Then result.Add(prc)
            Catch ex As Exception
            End Try
        Next
        Return result.ToArray
    End Function


    Public Shared Sub KillWindowsErrorReporting()
        Dim prcs = Process.GetProcessesByName("WerFault")
        For Each prc In prcs
            prc.Kill()
        Next
    End Sub

    Public Shared Sub KillProcessAndChildren(pid As Integer)
        '''TODO
#If Not NETCOREAPP Then
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid.ToString)
        Dim moc As ManagementObjectCollection = searcher.Get()
        For Each mo In moc
            KillProcessAndChildren(Convert.ToInt32(mo("ProcessID")))
        Next
#End If
        Try
            Dim proc As Process = Process.GetProcessById(pid)
            proc.Kill()
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub TaskKillCmd(pid As Integer)
        Try
            Dim prci As ProcessStartInfo = New ProcessStartInfo("taskkill", "/F /T /PID " + pid.ToString) With
            {
            .WindowStyle = ProcessWindowStyle.Hidden,
                .CreateNoWindow = True,
                .UseShellExecute = False,
                .WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True
            }
            Process.Start(prci)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub KillProcess(prc As Process, method As Integer)
        Try
            Select Case method
                Case 0
                    prc.Kill()
                Case 1
                    ProcessTools.KillProcessAndChildren(prc.Id)
                Case 2
                    ProcessTools.TaskKillCmd(prc.Id)
                Case Else
                    prc.Kill()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function ProcessExited(prc As Process)
        Dim success = prc.HasExited
        If Not success Then
            Threading.Thread.Sleep(500)
            success = prc.HasExited
        End If
        Return success
    End Function

End Class
