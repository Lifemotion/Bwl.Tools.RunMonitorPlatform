Imports System.Threading
Imports Bwl.Framework

Public Class RunMonitorCore
    Private _logger As Logger
    Private _thread As Threading.Thread
    Public ReadOnly Property Tasks As New TasksList

    Public Sub New(logger As Logger)
        _logger = logger
    End Sub

    Public Sub Run()
        Do
            Try
                SingleCheck
            Catch ex As Exception
                _logger.AddError("RunMonitorCore Run Error: " + ex.Message)
            End Try
            Thread.Sleep(1)
        Loop
    End Sub

    Public Sub RunInThread()
        If _thread IsNot Nothing Then Throw New Exception
        _thread = New Thread(AddressOf Run)
        _thread.Start()
    End Sub

    Public Sub StopThread()
        If _thread Is Nothing Then Throw New Exception
        _thread.Abort()
    End Sub

    Public Sub SingleCheck()
        Const timeToAutomaticEnable = 10
        For Each task In Tasks.ToArray
            If task.Checks.Count > 0 Then
                If task.State = TaskState.disabled Then
                    task.ExternalInfo = "Time to autotomatic enable: " + (timeToAutomaticEnable - (Now - task.Checks(0).LastCheck.Time).TotalMinutes).ToString("0.0") + "min"
                    If (Now - task.Checks(0).LastCheck.Time).TotalMinutes > timeToAutomaticEnable Then
                        task.State = TaskState.warning
                        task.ExternalInfo = ""
                    End If
                Else
                    _logger.AddDebug("Checking Task " + task.ID)

                    Dim maximumCheckFails As Integer = 0

                    For Each check In task.Checks
                        If (Now - check.LastCheck.Time).TotalSeconds > check.CheckIntervalSeconds Then
                            Try
                                check.Check()
                                check.LastCheck.SetNowOk("")
                                _logger.AddDebug("Check Task Ok " + task.ID + " - " + check.Name)
                            Catch ex As TaskCheckException
                                _logger.AddWarning("Check Failed " + task.ID + " - " + ex.MainMessage)
                                check.LastCheck.SetNowError("", ex.MainMessage)
                            Catch ex As Exception
                                _logger.AddWarning("Check Failed " + task.ID + " - " + ex.Message)
                                check.LastCheck.SetNowError("", ex.Message)
                            End Try
                        End If
                        If maximumCheckFails < check.LastCheck.FailedAttempts Then maximumCheckFails = check.LastCheck.FailedAttempts
                    Next

                    If maximumCheckFails = 0 Then task.State = TaskState.ok Else task.State = TaskState.warning

                    For Each action In task.FaultActions
                        If maximumCheckFails >= action.FaultsToRun And (Now - action.LastAttempt.Time).TotalSeconds > action.DelayBeforeActionSeconds Then
                            task.State = TaskState.fault
                            Try
                                _logger.AddMessage("Task FaultAction" + task.ID + " - " + action.Name)
                                action.Run()
                                action.LastAttempt.SetNowOk("")
                                _logger.AddDebug("Task FaultAction" + task.ID + " - " + action.Name + " - ok")
                                _logger.AddDebug(action.LastAttempt.Message)
                                _logger.AddDebug(action.LastAttempt.ErrorText)
                            Catch ex As FaultActionException
                                action.LastAttempt.SetNowError("", ex.MainMessage)
                                _logger.AddWarning("Task FaultAction Failed " + task.ID + " - " + ex.MainMessage)
                                _logger.AddDebug(action.LastAttempt.Message)
                                _logger.AddDebug(action.LastAttempt.ErrorText)
                            Catch ex As Exception
                                action.LastAttempt.SetNowError("", ex.Message)
                                _logger.AddWarning("Task FaultAction Failed " + task.ID + " - " + ex.Message)
                                _logger.AddDebug(action.LastAttempt.Message)
                                _logger.AddDebug(action.LastAttempt.ErrorText)
                            End Try
                        End If
                    Next
                End If
            End If
        Next
        Thread.Sleep(1)
    End Sub
End Class
