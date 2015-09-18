Public Class EnvironmentInfo

	Public Sub New()
		CommandLine = Environment.CommandLine
		CurrentDirectory = Environment.CurrentDirectory
		Is64BitOperatingSystem = Environment.Is64BitOperatingSystem
		MachineName = Environment.MachineName
		OSVersion = Environment.OSVersion
		UserName = Environment.UserName
		Version = Environment.Version
		WorkingSet = Environment.WorkingSet
	End Sub

	Public Property CommandLine As String
	Public Property CurrentDirectory As String
	Public Property MachineName As String
	Public Property Is64BitOperatingSystem As Boolean
	Public Property UserName As String
	Public Property OSVersion As OperatingSystem
	Public Property Version As Version
	Public Property WorkingSet As Long
End Class
