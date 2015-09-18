Imports System.IO
Imports System.Net

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
        Dim result = HttpHead(_address)
        If result.Length < 32 Then Throw New Exception("Response too short: " + result)
        _statusInfo = "Result: " + result.Length.ToString + " bytes, " + Mid(result, 1, 32)
    End Sub

    Shared Function HttpGet(ByVal url As String) As String
        Dim req = HttpWebRequest.Create(url)
        req.ContentType = "text/html"
        req.Headers.Add(HttpRequestHeader.Pragma, "no-cache")
        req.Method = "GET"
        Dim response As String = New StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd()
        Return response
    End Function

    Shared Function HttpHead(ByVal url As String) As String
        Dim req = HttpWebRequest.Create(url)
        req.ContentType = "text/html"
        req.Headers.Add(HttpRequestHeader.Pragma, "no-cache")
        req.Method = "HEAD"
        Dim response = req.GetResponse.Headers.ToString

        Return response
    End Function

End Class
