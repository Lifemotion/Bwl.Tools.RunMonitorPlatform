Imports System.Text

Public Class EthernetInfo
	Public Property Info As String

	Public Shared Function FillEthernetInfo() As EthernetInfo
		Dim ei = New EthernetInfo
		ei.Info = RunIpConfig()
		Return ei
	End Function

	Private Shared Function RunIpConfig() As String
		Dim res = ""
		Try
			Dim process = New Process
			process.StartInfo.FileName = "cmd"
			process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866)
			process.StartInfo.UseShellExecute = False
			process.StartInfo.RedirectStandardOutput = True
			process.StartInfo.Arguments = "/C ipconfig"
			process.Start()
			res = process.StandardOutput.ReadToEnd
		Catch ex As Exception
			res = ex.ToString
		End Try
		Return res
	End Function
End Class
