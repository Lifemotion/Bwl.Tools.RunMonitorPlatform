
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
        lbShortMode.Text = _task.ShortName + " - " + _task.State.ToString
        Dim control = stateListbox
        Select Case _task.State
            Case TaskState.ok : control.BackColor = Color.LightGreen
            Case TaskState.warning : control.BackColor = Color.Yellow
            Case TaskState.fault : control.BackColor = Color.LightPink
            Case TaskState.disabled : control.BackColor = Color.Gray
            Case Else : GroupBox1.BackColor = SystemColors.Control
        End Select
        lbDescription.Text = "Description: " + _task.Description
        If _task.Info > "" Or Task.ExternalInfo > "" Then
            lbDescription.Text += "Info: " + Task.ExternalInfo + ";" + _task.Info
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

    Private Sub DisableEnableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisableEnableToolStripMenuItem.Click
        If _task.State = TaskState.disabled Then _task.State = TaskState.warning Else _task.State = TaskState.disabled
    End Sub

    Private Sub DoAction1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DoAction1ToolStripMenuItem.Click
        DoAction(0)
    End Sub
    Private Sub DoAction2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DoAction2ToolStripMenuItem.Click
        DoAction(1)
    End Sub
    Private Sub DoAction3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DoAction3ToolStripMenuItem.Click
        DoAction(2)
    End Sub

    Private Sub DoAction(index As Integer)
        Try
            If _task.FaultActions.Count > index Then
                _task.FaultActions(index).Run()
                MsgBox(_task.FaultActions(index).LastAttempt.Message + "; " + _task.FaultActions(index).LastAttempt.ErrorText)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GroupBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles GroupBox1.MouseDown
        If e.Button = MouseButtons.Right Then ContextMenuStrip1.Show(Me, e.X, e.Y)
    End Sub

    Private Sub TaskDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
