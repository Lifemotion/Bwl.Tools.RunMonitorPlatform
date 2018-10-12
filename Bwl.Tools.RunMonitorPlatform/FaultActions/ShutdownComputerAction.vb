Public Class ShutdownComputerAction
    Inherits CommonFaultAction
    Private _delay As Integer

    Public Property ShowAbortWindow As Boolean = False

    Public Sub New(faultsToRun As Integer, Optional restartDelaySeconds As Integer = 0)
        MyBase.New("ShutdownComputer" + restartDelaySeconds.ToString + "s", faultsToRun)
        If restartDelaySeconds > 0 Then ShowAbortWindow = True
        _delay = restartDelaySeconds
        DelayBeforeActionSeconds = 60
    End Sub

    Public Overrides Sub Run()
        Shell("shutdown -t " + _delay.ToString + " -f")
        If ShowAbortWindow Then
            Try
                If Application.OpenForms.Count > 0 Then
                    Application.OpenForms(0).Invoke(Sub()
                                                        Dim form = (New ShutdownComputerAbortForm)
                                                        form.Show()
                                                        form.Focus()
                                                    End Sub)
                End If
            Catch ex As Exception

            End Try

        End If
        _lastCall.Message = "Команда выключения выполнена, ожидание выключения"
    End Sub
End Class
