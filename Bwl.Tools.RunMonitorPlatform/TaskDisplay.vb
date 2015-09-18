
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
        Dim control = stateListbox
        Select Case _task.State
            Case TaskState.ok : control.BackColor = Color.LightGreen
            Case TaskState.warning : control.BackColor = Color.Yellow
            Case TaskState.fault : control.BackColor = Color.LightPink
            Case TaskState.disabled : control.BackColor = Color.Gray
            Case Else : GroupBox1.BackColor = SystemColors.Control
        End Select
        Label1.Text = "Description: " + _task.Description
        If _task.Info > "" Then
            Label2.Text = "Info: " + _task.Info
            Label2.Visible = True
        Else
            Label2.Visible = False
        End If
        Dim state As New List(Of String)
        For Each check In _task.Checks
            If check.LastCheck.Success Then
                state.Add(check.GetType.Name + " - Ok: " + check.LastCheck.Time.ToLongTimeString)
            Else
                state.Add(check.GetType.Name + " - Error #" + check.LastCheck.FailedAttempts.ToString + ": " + check.LastCheck.Time.ToLongTimeString + ", " + check.LastCheck.ErrorText)
            End If
            If check.ParametersInfo > "" Then state.Add("--> Params: " + check.ParametersInfo)
            If check.StatusInfo > "" Then state.Add("--> Status: " + check.StatusInfo)
        Next

        For Each action In _task.FaultActions
            state.Add(action.GetType.Name + " after " + action.FaultsToRun.ToString + " err, " + action.LastAttempt.Time)
        Next


        Do While state.Count > stateListbox.Items.Count
            stateListbox.Items.Add("")
        Loop

        For i = 0 To state.Count - 1
            If stateListbox.Items(i) <> state(i) Then
                stateListbox.Items(i) = state(i)
            End If
        Next

        '     stateListbox.Refresh()
        MyBase.Refresh()
    End Sub
End Class
