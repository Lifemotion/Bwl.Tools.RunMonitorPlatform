Public Class ShutdownComputerAbortForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Shell("shutdown -a")
    End Sub
End Class