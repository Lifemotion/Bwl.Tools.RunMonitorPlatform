
Public Class TaskDisplay
    Private _task As ITask

    Public Property Task As ITask
        Set(value As ITask)
            _task = value
        End Set
        Get
            Return _task
        End Get
    End Property

    Public Overrides Sub Refresh()
        GroupBox1.Text = _task.ID + " - " + _task.State.ToString
        Label1.Text = "Info: " + _task.Info
        Label2.Text = "Description: " + _task.Description
        Dim state As New List(Of String)
        For Each check In _task.Checks
            If check.LastCheck.Success Then
                state.Add(check.GetType.Name + " - Ok: " + check.LastCheck.Time.ToLongTimeString)
            Else
                state.Add(check.GetType.Name + " - Error #" + check.LastCheck.FailedAttempts.ToString + ": " + check.LastCheck.Time.ToLongTimeString + ", " + check.LastCheck.ErrorText)
            End If
            If check.Info > "" Then state.Add("---> " + check.Info)
        Next

        For Each action In _task.FaultActions
            state.Add(action.GetType.Name + " after " + action.FaultsToRun.ToString + " err, " + action.LastAttempt.Time)
        Next


        Do While state.Count > stateListbox.Items.Count
            stateListbox.Items.Add("")
        Loop

        For i = 0 To state.Count - 1
            stateListbox.Items(i) = state(i)
        Next

        stateListbox.Refresh()
        MyBase.Refresh()
    End Sub
End Class
