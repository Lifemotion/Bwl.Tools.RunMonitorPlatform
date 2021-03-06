﻿
Imports Bwl.Tools.RunMonitorPlatform

Public Class CommonTask
    Implements ITask

    Private _id As String
    Private _info As String

    Public Property Checks As New ChecksList Implements ITask.Checks

    Public Property Description As String Implements ITask.Description

    Public ReadOnly Property ID As String Implements ITask.ID
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property Info As String Implements ITask.Info
        Get
            Return _info
        End Get
    End Property

    Public Sub New(id As String, shortName As String)
        _id = id
        _ShortName = shortName
    End Sub

    Public Property State As TaskState = TaskState.warning Implements ITask.State

    Public Property FaultActions As New FaultActionsList Implements ITask.FaultActions

    Public Property CheckStats As New CheckStats Implements ITask.CheckStats

    Public Property ExternalInfo As String Implements ITask.ExternalInfo

    Public Property ShortName As String = "" Implements ITask.ShortName

    Public Property ShortState As String = "" Implements ITask.ShortState

    Public Property AutoStart As Boolean = True Implements ITask.AutoStart


End Class
