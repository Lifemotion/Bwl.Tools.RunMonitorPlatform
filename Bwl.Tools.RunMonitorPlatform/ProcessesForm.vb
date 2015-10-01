Public Class ProcessesForm
    Private Sub RefreshButton_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click
        Dim prcs = Process.GetProcesses
        processesList.Rows.Clear()
        For Each prc In prcs
            Dim data As String() = {"", "", "", ""}
            Dim datarow = processesList.Rows(processesList.Rows.Count - 1)
            Try
                data(0) = prc.Id
                data(1) = prc.ProcessName
                data(2) = IO.Path.GetFileName(prc.MainModule.FileName)
            Catch ex As Exception

            End Try
            processesList.Rows.Add(data)
        Next
        processesList.Refresh()
    End Sub

    Private Sub KillWerFaults_Click(sender As Object, e As EventArgs) Handles KillWerFaults.Click
        ProcessTools.KillWindowsErrorReporting()
    End Sub

    Private Sub KillSelected_Click(sender As Object, e As EventArgs) Handles KillSelected.Click
        Try
            Dim prcid = CInt(Val(processesList.SelectedRows(0).Cells(0).Value))
            Dim prc = Process.GetProcessById(prcid)
            ProcessTools.KillProcess(prc, 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class