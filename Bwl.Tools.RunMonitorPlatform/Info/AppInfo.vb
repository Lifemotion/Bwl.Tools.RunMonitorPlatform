
Public Class AppInfo
	Public Property Path As String = ""
	Public Property Args As String = ""
	Public Property Basedir As String = ""
	Public Property ID As String = ""
	Public Property LoadDelay As Integer
	Public Property ReloadDelay As Integer
	Public Property LoadedFrom As String = ""

    Public Shared Function CreateFrom(path As String) As AppInfo
        Throw New NotImplementedException
    End Function

    Public Sub SaveTo(path As String)
        'Dim str = JsonConvert.SerializeObject(Me, Formatting.Indented)
        '       System.IO.File.WriteAllText(path, str, System.Text.Encoding.UTF8)
    End Sub
End Class
