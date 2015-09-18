Public Class RestartComputerAction
    Inherits CommonFaultAction
    Private _delay As Integer = 30

    Public Property ShowAbortWindow As Boolean = False

    Public Sub New(faultsToRun As Integer, restartDelaySeconds As Integer)
        MyBase.New("RestartComputer" + restartDelaySeconds.ToString + "s", faultsToRun)
        _delay = restartDelaySeconds
        _info = "Ожидание"
        DelayBeforeActionSeconds = 20
    End Sub

    Public Overrides Sub Run()
        Shell("shutdown -r -t " + _delay.ToString)
        If ShowAbortWindow Then
            Dim form = (New RestartComputerAbortForm)
            form.Show()
            form.Focus()
        End If
        _lastCall.Message = "Команда перезагрузки выполнена, ожидание перезагрузки"
        _info = "Команда перезагрузки выполнена, ожидание перезагрузки"
    End Sub
End Class
