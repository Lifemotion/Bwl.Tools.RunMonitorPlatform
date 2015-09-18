Public Class RestartComputerAction
    Inherits CommonFaultAction
    Private _delay As Integer

    Public Property ShowAbortWindow As Boolean = True

    Public Sub New(faultsToRun As Integer, restartDelaySeconds As Integer)
        MyBase.New("RestartComputer" + restartDelaySeconds.ToString + "s", faultsToRun)
        _delay = restartDelaySeconds
        _info = "Ожидание"
        DelayBeforeActionSeconds = 60
    End Sub

    Public Overrides Sub Run()
        Shell("shutdown -r -t " + _delay.ToString)
        If ShowAbortWindow Then
            Try
                If Application.OpenForms.Count > 0 Then
                    Application.OpenForms(0).Invoke(Sub()
                                                        Dim form = (New RestartComputerAbortForm)
                                                        form.Show()
                                                        form.Focus()
                                                    End Sub)
                End If
            Catch ex As Exception

            End Try

        End If
        _lastCall.Message = "Команда перезагрузки выполнена, ожидание перезагрузки"
        _info = "Команда перезагрузки выполнена, ожидание перезагрузки"
    End Sub
End Class
