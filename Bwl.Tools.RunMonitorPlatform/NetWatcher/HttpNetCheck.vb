
Public Class HttpNetCheck
    Inherits CommonTaskCheck
    Private _address As String
    Private _onlyHeaders As Boolean

    Public Sub New()
        Me.New("https://ya.ru", True)
    End Sub

    Public Sub New(httpAddress As String, getHeadersOnly As Boolean)
        MyBase.New("HttpNetCheck_" + httpAddress)
        _address = httpAddress
        _onlyHeaders = getHeadersOnly
        _parametersInfo = "Address: " + _address + ", OnlyHeaders: " + _onlyHeaders.ToString
    End Sub

    Public Overrides Sub Check()
        Dim result = ""
        If _onlyHeaders Then
            result = HttpTools.HttpHead(_address)
        Else
            result = HttpTools.HttpGet(_address)
        End If
        If result.Length < 32 Then Throw New Exception("Response too short: " + result)
        _statusInfo = "Result: " + result.Length.ToString + " bytes, " + Mid(result, 1, 32)
    End Sub



End Class
