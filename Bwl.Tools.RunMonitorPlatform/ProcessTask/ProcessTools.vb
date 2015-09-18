Public Class ProcessTools
    Public Shared Function FindProcesses(fullPath As String, filename As String) As Process()
        Dim prcs = Process.GetProcesses()
        Dim result As New List(Of Process)
        For Each prc In prcs
            Try
                If fullPath > "" AndAlso prc.MainModule.FileName.ToLower = fullPath.ToLower Then result.Add(prc)
                If filename > "" AndAlso IO.Path.GetFileName(prc.MainModule.FileName).ToLower = filename.ToLower Then result.Add(prc)
            Catch ex As Exception
            End Try
        Next
        Return result.ToArray
    End Function
End Class
