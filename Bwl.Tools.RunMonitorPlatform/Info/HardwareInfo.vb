Imports System.Net
Imports System.Management
Imports System.IO

Public Class Hardware
	Public Shared Function GetCpuInfo() As CpuInfo
		Dim remoteIP = IPAddress.Loopback
		Dim cpu = "<none>"
		Try
			Dim scope = New ManagementScope(String.Format("\\{0}\root\cimv2", remoteIP))
			scope.Connect()
			Using searcher = New ManagementObjectSearcher(scope, New ObjectQuery("SELECT * FROM Win32_Processor"))
				For Each obj In searcher.Get()
					cpu = obj("Name").ToString()
				Next
			End Using
		Catch
			cpu = "<no identify>"
		End Try
		Dim cpuInfo = New CpuInfo(cpu)

		cpuInfo.ProcessorCount = Environment.ProcessorCount

		Return New CpuInfo(cpu)
	End Function

	''' <summary>
	''' In MB
	''' </summary>
	''' <returns>MB</returns>
	''' <remarks></remarks>
	Public Shared Function GetFreeMemoryInfo() As ULong
		Dim remoteIP = IPAddress.Loopback
		Dim mem = 0UL
		Try
			Dim scope = New ManagementScope(String.Format("\\{0}\root\cimv2", remoteIP))
			scope.Connect()
			Using searcher = New ManagementObjectSearcher(scope, New ObjectQuery("SELECT * FROM Win32_OperatingSystem"))
				mem = (From obj In searcher.Get() _
				Select Convert.ToUInt64(obj("FreePhysicalMemory"))).FirstOrDefault
			End Using
		Catch
		End Try
		Return mem / 1024
	End Function

	Public Shared Function GetMemoryInfo() As MemoryInfo
		Dim remoteIP = IPAddress.Loopback
		Dim memStr = String.Empty
		Try
			Dim scope = New ManagementScope(String.Format("\\{0}\root\cimv2", remoteIP))
			scope.Connect()
			Dim cap = 0UL
			Using searcher = New ManagementObjectSearcher(scope, New ObjectQuery("SELECT * FROM Win32_PhysicalMemory"))
				cap = (From obj In searcher.Get() _
					Select Convert.ToUInt64(obj("Capacity"))).Aggregate(cap, Function(current, tcap) current + tcap)
			End Using
			memStr = String.Format("{0:.} Mb", cap / 1024 / 1024)
		Catch
			memStr = "<no identify>"
		End Try

		Dim memInfo = New MemoryInfo(memStr)
		memInfo.UsedMemory = Convert.ToInt32(Process.GetCurrentProcess().PrivateMemorySize64 / (1024 * 1024)).ToString + " MB"

		Return memInfo
	End Function

	Public Shared Function GetHddInfo() As HddInfo
		Dim hddInfo = New HddInfo

		Dim drives As New List(Of DrvInfo)

		For Each drive In DriveInfo.GetDrives().ToArray
			Try
				Dim drv = New DrvInfo
				drv.Name = drive.Name
				drv.AvailableFreeSpace = (drive.AvailableFreeSpace / (1024 * 1024 * 1024)).ToString + " GB"
				drv.TotalSize = (drive.TotalSize / (1024 * 1024 * 1024)).ToString + " GB"
				drv.VolumeLabel = drive.VolumeLabel

				drives.Add(drv)
			Catch ex As Exception

			End Try

		Next

		hddInfo.Drives = drives
		Return hddInfo
	End Function
End Class
