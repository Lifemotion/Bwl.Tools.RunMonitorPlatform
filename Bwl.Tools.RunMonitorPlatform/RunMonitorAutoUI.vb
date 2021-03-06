﻿Imports Bwl.Framework
Imports Bwl.Tools.RunMonitorPlatform

Public Class RunMonitorAutoUI
    Public Property RefreshTasksDelay As Integer

    Private _logger As Logger
    Private _settings As SettingsStorageRoot
    Private _tasks As ITask() = {}
    Private _formDescriptor As AutoFormDescriptor
    Private _ui As New AutoUI

    Public ReadOnly Property FormDescriptor As AutoFormDescriptor
        Get
            Return _formDescriptor
        End Get
    End Property

    Public ReadOnly Property UI As AutoUI
        Get
            Return _ui
        End Get
    End Property

    Public Sub New(logger As Logger)
        SettingUp(logger, New SettingsStorageRoot())
    End Sub

    Public Sub New(logger As Logger, settings As SettingsStorageRoot)
        SettingUp(logger, settings)
    End Sub

    Private Sub SettingUp(logger As Logger, settings As SettingsStorageRoot)
        Me._logger = logger
        Me._settings = settings
        RefreshTasksDelay = 1000
        _formDescriptor = New AutoFormDescriptor(UI, "wdt-form")
        Dim refreshThread As New Threading.Thread(AddressOf RefreshThreadSub)
        refreshThread.IsBackground = True
        refreshThread.Name = "RunMonitorAutoUI_Refresh"
        refreshThread.Start()
    End Sub

    Private Sub RefreshThreadSub()
        Do
            Try
                For Each task In _tasks.ToArray
                    RefreshTask(task)
                Next
            Catch ex As Exception
                _logger.AddError(ex.Message)
            End Try
            Threading.Thread.Sleep(RefreshTasksDelay)
        Loop
    End Sub

    Public Function CreateForm() As AutoUIForm
        Dim form = New AutoUIForm(If(_settings IsNot Nothing, _settings, New SettingsStorageRoot), _logger, UI)
        Return form
    End Function

    Private Sub RefreshTask(_task As ITask)
        Dim listbox As AutoListbox = Nothing
        For Each cmp In UI.Elements
            If cmp.Info.ID = _task.ID + "_list" Then
                listbox = cmp
            End If
        Next

        If listbox IsNot Nothing Then
            Dim items = New List(Of String)

            Dim backColor As Color = Color.White
            Select Case _task.State
                Case TaskState.Ok : backColor = Color.LightGreen
                Case TaskState.Warning : backColor = Color.Yellow
                Case TaskState.Fault : backColor = Color.LightPink
                Case TaskState.Disabled : backColor = Color.Gray
            End Select
            If listbox.Info.BackColor <> backColor Then listbox.Info.BackColor = backColor
            If listbox.Info.Caption <> _task.ShortName + " - " + _task.State.ToString Then listbox.Info.Caption = _task.ShortName + " - " + _task.State.ToString

            If _task.Description > "" Then items.Add("Description: " + _task.Description)
            If _task.Info > "" Then items.Add("Info: " + _task.Description)
            If _task.ExternalInfo > "" Then items.Add("ExternalInfo: " + _task.Description)

            For Each check In _task.Checks
                If check.LastCheck.Success Then
                    items.Add(check.GetType.Name + " - Ok: " + check.LastCheck.Time.ToLongTimeString)
                Else
                    items.Add(check.GetType.Name + " - Error #" + check.LastCheck.FailedAttempts.ToString + ": " + check.LastCheck.Time.ToLongTimeString + ", " + check.LastCheck.ErrorText)
                End If
                If check.ParametersInfo > "" Then items.Add("--> Params: " + check.ParametersInfo)
                If check.StatusInfo > "" Then items.Add("--> Status: " + check.StatusInfo)
            Next

            For Each action In _task.FaultActions
                items.Add(action.GetType.Name + " after " + action.FaultsToRun.ToString + " err, " + action.LastAttempt.Time)
            Next

            listbox.Items.Replace(items.ToArray)
        End If
    End Sub

    Public Property Tasks As ITask()
        Get
            Return _tasks
        End Get
        Set(value As ITask())
            _tasks = value
            UI.Elements.RemoveAll(Function(elem As IUIElementLocal) elem.GetType = GetType(AutoListbox))
            For Each task In _tasks
                Dim listBox As New AutoListbox(UI, task.ID + "_list")
                listBox.Info.Caption = task.ShortName
                listBox.Info.Height = 350
                listBox.AutoHeight = True
                AddHandler listBox.DoubleClick, Sub()
                                                    If task.State = TaskState.Disabled Then
                                                        task.State = TaskState.Warning
                                                    Else
                                                        task.State = TaskState.Disabled
                                                    End If
                                                End Sub
            Next
        End Set
    End Property
End Class
