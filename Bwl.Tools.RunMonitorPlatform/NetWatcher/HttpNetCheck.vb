Imports System.IO
Imports System.Net

Public Class HttpNetCheck
    Inherits CommonTaskCheck
    Private _address As String

    Public Sub New(httpAddress As String)
        MyBase.New("HttpNetCheck_" + httpAddress)
        _address = httpAddress
    End Sub

    Public Overrides Sub Check()
        Dim result = HttpGet(_address)
        If result.Length < 32 Then Throw New Exception("Response too short: " + result)
    End Sub

    Shared Function HttpGet(ByVal url As String) As String
        Dim req = HttpWebRequest.Create(url)
        req.ContentType = "text/html"
        req.Headers.Add(HttpRequestHeader.Pragma, "no-cache")
        req.Method = "GET"
        Dim response As String = New StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd()
        Return response
    End Function
End Class
