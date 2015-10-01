Public Class DiagToolForm
    Inherits Framework.FormAppBase

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub GetProcesses_Click(sender As Object, e As EventArgs) Handles GetProcesses.Click
        Dim prcs As New ProcessesForm
        prcs.Show()
    End Sub
End Class
