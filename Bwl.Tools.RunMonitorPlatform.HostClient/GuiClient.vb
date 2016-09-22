Imports Bwl.Network.ClientServer
Imports Bwl.Framework

Public Class GuiClient
    Inherits FormAppBase
    Private _client As New HostNetClient(_storage, _logger)
    Private _tasks As New RemoteTaskInfo()

    Private Sub HostClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        settingHostAddress.AssignedSetting = _client.Transport.AddressSetting
        settingTarget.AssignedSetting = _client.Transport.TargetSetting
    End Sub

    Private Sub bHostConnect_Click(sender As Object, e As EventArgs) Handles bHostConnect.Click
        Try
            _client.Transport.OpenAndRegister()
            bFindTargets_Click()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub bFindTargets_Click() Handles bFindTargets.Click
        Dim clients = _client.Transport.GetClientsList("HostControl")
        lbTargets.Items.Clear()
        lbTargets.Items.AddRange(clients)
    End Sub

    Private Sub lbTargets_DoubleClick(sender As Object, e As EventArgs) Handles lbTargets.DoubleClick
        If lbTargets.Text > "" Then
            settingTarget.AssignedSetting.ValueAsString = lbTargets.Text
        End If
    End Sub

    Private Sub bFindLocalServers_Click(sender As Object, e As EventArgs) Handles bFindLocalServers.Click
        Dim infos = NetFinder.Find(2000)
        lbLocalServers.Items.Clear()
        For Each info In infos
            If info.Name.Contains("HostControl") Then
                lbLocalServers.Items.Add(info.Address + ":" + info.Port.ToString + " " + info.Name)
            End If
        Next
    End Sub

    Private Sub lbLocalServers_DoubleClick(sender As Object, e As EventArgs) Handles lbLocalServers.DoubleClick
        If lbLocalServers.Text > "" Then
            Dim addr = lbLocalServers.Text.Split(" ")(0)
            settingHostAddress.AssignedSetting.ValueAsString = addr
            bHostConnect_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub bStart_Click(sender As Object, e As EventArgs) Handles bUploadStart.Click, bUpload.Click, bStart.Click, bSet.Click, bKill.Click
        Try
            Dim ops As String = ""
            If Equals(sender, bSet) Then ops = "set"
            If Equals(sender, bKill) Then ops = "kill set"
            If Equals(sender, bStart) Then ops = "kill set start"
            If Equals(sender, bUpload) Then ops = "kill set upload"
            If Equals(sender, bUploadStart) Then ops = "kill set upload start"
            _client.SendProcessTask(tbTaskId.Text, ops, tbFile.Text, tbArguments.Text, "", tbParameters.Text, cbAutoStart.Checked, cbMonitor.Checked, cbRemoteCmd.Checked, cbUploadFrom.Text)
            If cbRemoteCmd.Checked And ops.Contains("start") Then
                _client.CreateRemoteCmdForm(tbTaskId.Text).Show(Me)
            End If
            ShowTasksList()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub bRunRemoteShell_Click(sender As Object, e As EventArgs) Handles bRunRemoteShell.Click
        Try
            _client.CreateRemoteCmdForm(_client.CreateShellTask()).Show(Me)
            ShowTasksList()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub bRunMonitorRemoteUi_Click(sender As Object, e As EventArgs) Handles bRunMonitorRemoteUi.Click
        Dim remoteclient As New RemoteAppClient(_client.Transport, "RunMonitorHost", _client.Transport.TargetSetting.Value)
        remoteclient.CreateAutoUiForm.Show()
    End Sub

    Private Sub ShowTasksList() Handles bUpdateTasks.Click
        DataGridView1.Rows.Clear()
        Try
            Dim tasks = _client.GetTasks
            For Each task In tasks
                DataGridView1.Rows.Add(task.ID, task.State, task.Autostart, task.Runmonitored)
                Dim lastrow = DataGridView1.Rows(DataGridView1.Rows.Count - 1)
                lastrow.Tag = task
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub tUpdateConnectedState_Tick(sender As Object, e As EventArgs) Handles tUpdateConnectedState.Tick
        If _client.Transport.IsConnected Then
            gbTargets.Enabled = True
            If _client.TargetConnected Then
                gbTasks.Enabled = True
                gbTask.Enabled = True
            Else
                gbTasks.Enabled = False
                gbTask.Enabled = False
            End If
        Else
            gbTargets.Enabled = False
            gbTasks.Enabled = False
            gbTask.Enabled = False
        End If
    End Sub

    Private Sub tUpdateTasks_Tick(sender As Object, e As EventArgs) Handles tUpdateTasks.Tick
        If _client.Transport.IsConnected Then
            Dim thread As New Threading.Thread(Sub()
                                                   Me.Invoke(Sub() ShowTasksList())
                                               End Sub)
            thread.Start()
        End If
    End Sub

    Private Sub bSelectFolder_Click(sender As Object, e As EventArgs) Handles bSelectFolder.Click
        If selectFolderDialog.ShowDialog() = DialogResult.OK Then
            Dim path = IO.Path.GetDirectoryName(selectFolderDialog.FileName)
            If path > "" Then
                cbUploadFrom.Text = path
                tbFile.Items.Clear()
                Dim exes = IO.Directory.GetFiles(path, "*.exe", IO.SearchOption.AllDirectories)
                For Each exe In exes
                    exe = exe.Replace(path + IO.Path.DirectorySeparatorChar, "")
                    exe = exe.Replace(path, "")
                    If exe.Contains("vshost") = False Then
                        tbFile.Items.Add(exe)
                    End If
                Next
                If tbFile.Items.Count > 0 Then
                    tbFile.Text = (tbFile.Items(0))
                    Dim id As String = ""
                    For Each ch In IO.Path.GetFileNameWithoutExtension(tbFile.Text)
                        Select Case ch
                            Case "A" To "Z", "a" To "z", "0" To "9", "-", "_"
                                id += ch
                        End Select
                    Next
                    tbTaskId.Text = "ProcessTask_" + id
                Else
                    tbTaskId.Text = "ProcessTask_"
                    tbFile.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged, DataGridView1.CurrentCellChanged
        If DataGridView1.SelectedRows.Count = 0 Then Return
        Dim row = DataGridView1.SelectedRows(0)
        Dim task As RemoteTaskInfo = row.Tag
        If task IsNot Nothing Then
            cbAutoStart.Checked = task.Autostart
            cbMonitor.Checked = task.Runmonitored
            cbRemoteCmd.Checked = task.RemoteCmd
            tbFile.Text = task.Filename
            tbArguments.Text = task.Arguments
            tbTaskId.Text = task.ID
            tbParameters.Text = task.Parameters
            ' tbFil.Text = task.Workdir
        End If
    End Sub

    Private Sub bHostInfo_Click(sender As Object, e As EventArgs) Handles bHostInfo.Click
        Try
            Dim info = _client.GetHostInfo
            For Each line In info
                _logger.AddInformation(line)
            Next
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub
End Class
