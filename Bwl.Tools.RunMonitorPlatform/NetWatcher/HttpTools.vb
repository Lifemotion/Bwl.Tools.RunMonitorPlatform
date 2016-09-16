Imports System.IO
Imports System.Net

Public Class HttpTools
    Shared Function CreateHttpRequest(ByVal url As String) As HttpWebRequest
        Dim req As HttpWebRequest = HttpWebRequest.Create(url)
        req.ContentType = "text/html"
        req.Headers.Add(HttpRequestHeader.Pragma, "no-cache")
        req.Method = "GET"
        req.Timeout = 5000
        'TODO: only FW 4 !
        'req.ContinueTimeout = 5000
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

    Shared Function UploadFile(ByVal url As String, ByVal filePath As String) As String

        If Not IO.File.Exists(filePath) Then Throw New Exception("File Not Exist on " + filePath)
        Dim filebytes() As Byte = My.Computer.FileSystem.ReadAllBytes(filePath)
        Return UploadFile(url, IO.Path.GetFileName(filePath), filebytes)

    End Function

    Shared Function UploadFile(ByVal url As String, ByVal fileName As String, fileContent As String) As String
        Dim filebytes() As Byte = Text.Encoding.UTF8.GetBytes(fileContent)
        Return UploadFile(url, fileName, filebytes)
    End Function

    Shared Function UploadFile(ByVal url As String, ByVal fileName As String, fileBytes As Byte()) As String
        If fileBytes Is Nothing Then Throw New Exception("fileBytes ")
        If fileName Is Nothing Then Throw New Exception("fileName ")

        Dim req As Net.HttpWebRequest = Net.HttpWebRequest.Create(url)
        Dim header As New System.Text.StringBuilder()
        Dim boundary As String = IO.Path.GetRandomFileName
        header = New System.Text.StringBuilder()
        header.AppendLine("--" & boundary)
        header.AppendLine("Content-Disposition: form-data; name=""image_name""" & vbNewLine & vbNewLine & fileName)
        header.AppendLine("--" & boundary)
        header.AppendLine("Content-Disposition: form-data; name=""image""; filename=""" + fileName + """")
        header.AppendLine("Content-Type: application/binary")
        header.AppendLine()
        Dim headerbytes() As Byte = System.Text.Encoding.UTF8.GetBytes(header.ToString)
        Dim endboundarybytes() As Byte = System.Text.Encoding.ASCII.GetBytes(vbNewLine & "--" & boundary & "--" & vbNewLine)
        req.Accept = "*/*"
        req.Referer = ""
        req.UserAgent = "Mozilla/4.0"
        req.ContentType = "multipart/form-data; boundary=" & boundary
        req.Headers.Add(HttpRequestHeader.Pragma, "no-cache")
        req.Method = "POST"
        Dim stream As IO.Stream = req.GetRequestStream
        stream.Write(headerbytes, 0, headerbytes.Length)
        stream.Write(fileBytes, 0, fileBytes.Length)
        stream.Write(endboundarybytes, 0, endboundarybytes.Length)
        stream.Close()
        Dim response As String = New StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd()
        Return response
    End Function

End Class
