Public Class LastCall
    Public Property Time As DateTime
    Public Property Success As Boolean
    Public Property Message As String = ""
    Public Property ErrorText As String = ""
    Public Property FailedAttempts As Integer

    Public Sub SetNowOk(message As String)
        Time = Now
        Me.Message = message
        ErrorText = ""
        FailedAttempts = 0
        Success = True
    End Sub

    Public Sub SetNowError(message As String, errorText As String)
        Time = Now
        Me.Message = message
        Me.ErrorText = errorText
        FailedAttempts += 1
        Success = False
    End Sub
End Class
