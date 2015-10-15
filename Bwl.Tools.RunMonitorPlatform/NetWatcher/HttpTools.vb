Imports System.IO
Imports System.Net

Public Class HttpTools
    Shared Function CreateHttpRequest(ByVal url As String) As HttpWebRequest
        Dim req As HttpWebRequest = HttpWebRequest.Create(url)
        req.ContentType = "text/html"
        req.Headers.Add(HttpRequestHeader.Pragma, "no-cache")
        req.Method = "GET"
        req.Timeout = 5000
        req.ContinueTimeout = 5000
        req.ReadWriteTimeout = 5000
        Return req
    End Function

    Shared Function HttpGet(url As String) As String
        Dim req = CreateHttpRequest(url)
        req.Timeout = 10000
        Dim response As String = New StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd()
        Return response
    End Function

    Shared Function HttpHead(url As String) As String
        Dim req = CreateHttpRequest(url)

        Dim response = req.GetResponse.Headers.ToString
        Return response
    End Function
End Class
