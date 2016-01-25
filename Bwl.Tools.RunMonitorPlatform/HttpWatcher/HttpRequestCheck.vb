
Public Class HttpRequestCheck
    Inherits CommonTaskCheck
    Private _address As String
    Private _onlyHeaders As Boolean = False
    Private _goodWords As String
    Private _badWords As String
    Private _mustChange As Boolean
    Private _lastResponse As String

    Public Sub New(httpAddress As String, goodWords As String, badWords As String, mustChange As Boolean)
        MyBase.New("HttpRequestCheck_" + httpAddress)
        If httpAddress Is Nothing OrElse httpAddress = "" Then Throw New ArgumentException
        If goodWords Is Nothing Then Throw New ArgumentException
        If badWords Is Nothing Then Throw New ArgumentException

        _address = httpAddress
        _goodWords = goodWords.Trim
        _badWords = badWords.Trim
        _mustChange = mustChange
        _parametersInfo = "Address: " + _address + ", Good: " + _goodWords + ", Bad: " + _badWords + ", Mus change: " + _mustChange.ToString
    End Sub

    Public Overrides Sub Check()
        Dim result = ""
        If _onlyHeaders Then
            result = HttpTools.HttpHead(_address)
        Else
            result = HttpTools.HttpGet(_address)
        End If

        If _goodWords.Length > 0 Then
            If result.ToLower.Contains(_goodWords.ToLower) = False Then Throw New Exception("Response doesn't contain '" + _goodWords + "'")
        End If
        If _badWords.Length > 0 Then
            If result.ToLower.Contains(_goodWords.ToLower) = True Then Throw New Exception("Response contains '" + _badWords + "'")
        End If

        If _mustChange Then
            If _lastResponse IsNot Nothing AndAlso _lastResponse = result Then Throw New Exception("Response doesn't changed")
            _lastResponse = result
        End If

        _statusInfo = "Result: " + result.Length.ToString + " bytes, " + Mid(result, 1, 128)
    End Sub

End Class
