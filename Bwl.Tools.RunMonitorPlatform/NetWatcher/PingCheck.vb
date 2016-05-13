Imports System.Net.NetworkInformation
Imports Bwl.Tools.RunMonitorPlatform

Public Class PingCheck
    Inherits CommonTaskCheck

    Private _pingSender As New Ping()
    Private _timeout As Integer
    Private _hosts As IEnumerable(Of String)

    Public Sub New(hosts As IEnumerable(Of String), timeout As Integer)
        MyBase.New("PingCheck")
        _hosts = hosts
        _timeout = timeout
    End Sub

    Public Overrides Sub Check()
        For Each host In _hosts
            Dim pr = _pingSender.Send(host, _timeout)
            If pr.Status = IPStatus.Success Then
                Return
            End If
        Next
        Throw New Exception("Can't ping at least one host!")
    End Sub
End Class
