Imports Bwl.Network.ClientServer
Imports Bwl.Framework
Imports Bwl.Tools.RunMonitorPlatform.HostClient

Public Class GuiClient
    Inherits FormAppBase
    Private WithEvents _client As New HostNetClient(_storage, _logger)
    Private _tasks As New RemoteTaskInfo()

    Private Sub HostClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        settingHostAddress.AssignedSetting = _client.Transport.AddressSetting
        settingTarget.AssignedSetting = _client.Transport.TargetSetting
    End Sub

    Private Sub bHostConnect_Click(sender As Object, e As EventArgs) Handles bHostConnect.Click
        Try
            If _client.Transport.IsConnected Then
                _client.Transport.Close()
                Threading.Thread.Sleep(500)
            End If
        Catch ex As Exception
        End Try
        Try
            _client.Connect
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
        SearchLocalServersAndUIs()
    End Sub

    Private Sub SearchLocalServersAndUIs()
        Dim target = settingHostAddress.AssignedSetting.ValueAsString
        Dim targetParts = target.Split(":")
        Dim targetHost = "unknown"
        If targetParts.Length = 2 Then
            targetHost = targetParts(0)
        End If
        Dim thread As New Threading.Thread(Sub()
                                               Try
                                                   Dim infos = NetFinder.Find(2000)
                                                   Me.Invoke(Sub()

                                                                 lbLocalServers.Items.Clear()
                                                                 lbRemoteUIs.Items.Clear()
                                                                 For Each info In infos
                                                                     If info.Name.Contains("HostControl") Then
                                                                         lbLocalServers.Items.Add(info.Address + ":" + info.Port.ToString + " " + info.Name)
                                                                     End If
                                                                     If info.Address.ToLower = targetHost.ToLower And info.Name.Contains("HostControl") = False Then
                                                                         lbRemoteUIs.Items.Add(info.Address + ":" + info.Port.ToString + " " + info.Name)
                                                                     End If
                                                                 Next
                                                             End Sub)
                                               Catch ex As Exception

                                               End Try

                                           End Sub)
        thread.Start()
    End Sub


    Private Sub bStart_Click(sender As Object, e As EventArgs) Handles bUploadStart.Click, bUpload.Click, bStart.Click, bSet.Click, bKill.Click
        Try
            Dim ops As String = ""
            If Equals(sender, bSet) Then ops = "set"
            If Equals(sender, bKill) Then ops = "kill set"
            If Equals(sender, bStart) Then ops = "kill set start"
            If Equals(sender, bUpload) Then ops = "kill set upload"
            If Equals(sender, bUploadStart) Then ops = "kill set upload start"
            Dim rms = "Ok"
            If cbMonitor.Checked = False Then rms = "Disabled"
            _client.SendProcessTask(tbTaskId.Text, ops, tbFile.Text, tbArguments.Text, "", tbParameters.Text, cbAutoStart.Checked, rms, cbRemoteCmd.Checked, cbUploadFrom.Text)

            If Equals(sender, bUpload) Or Equals(sender, bUploadStart) Then
                _client.SendProcessTaskSlowUpload(tbTaskId.Text, ops, tbFile.Text, tbArguments.Text, "", tbParameters.Text, cbAutoStart.Checked, rms, cbRemoteCmd.Checked, cbUploadFrom.Text)
            Else
                _client.SendProcessTask(tbTaskId.Text, ops, tbFile.Text, tbArguments.Text, "", tbParameters.Text, cbAutoStart.Checked, rms, cbRemoteCmd.Checked, cbUploadFrom.Text)
            End If

            If cbRemoteCmd.Checked And ops.Contains("start") Then
                _client.CreateRemoteCmdForm(tbTaskId.Text, Text.Split(",")(0)).Show(Me)
            End If
            ShowTasksList()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub bRunRemoteShell_Click(sender As Object, e As EventArgs) Handles bRunRemoteShell.Click
        Try
            _client.CreateRemoteCmdForm(_client.CreateShellTask(), Text.Split(",")(0)).Show(Me)
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
        Try
            _client.GetTasks()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub tUpdateConnectedState_Tick(sender As Object, e As EventArgs) Handles tUpdateConnectedState.Tick
        If _client.Transport.IsConnected Then
            gbTargets.Enabled = True
            If _client.TargetConnected Then
                gbTasks.Enabled = True
                gbTask.Enabled = True
                gbTarget.Enabled = True
            Else
                gbTarget.Enabled = False
                gbTasks.Enabled = False
                gbTask.Enabled = False
                Me.Text = "Bwl RunMonitor Remote Host Client (not connected)"
                DataGridView1.Rows.Clear()
            End If
        Else
            gbTarget.Enabled = False
            gbTargets.Enabled = False
            gbTasks.Enabled = False
            gbTask.Enabled = False
            DataGridView1.Rows.Clear()
            Me.Text = "Bwl RunMonitor Remote Host Client (not connected)"
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
                    IO.File.WriteAllText(IO.Path.Combine(AppBase.DataFolder, tbTaskId.Text + "-uploadpath.txt"), path)
                Else
                    tbTaskId.Text = "ProcessTask_"
                    tbFile.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub bHostInfo_Click(sender As Object, e As EventArgs) Handles bHostInfo.Click
        Try
            _client.GetHostInfo()
        Catch ex As Exception
            _logger.AddError(ex.Message)
        End Try
    End Sub

    Private Sub _client_TaskListReceived(tasks() As RemoteTaskInfo) Handles _client.TaskListReceived
        Me.Invoke(Sub()
                      gbTasks.Text = "Remote Tasks (updated " + Now.ToLongTimeString + ")"
                      DataGridView1.Rows.Clear()
                      For Each task In tasks
                          DataGridView1.Rows.Add(task.ID, task.ProcessState, task.RunMonitorState, task.Autostart)
                          Dim lastrow = DataGridView1.Rows(DataGridView1.Rows.Count - 1)
                          lastrow.Tag = task
                      Next
                  End Sub)
    End Sub

    Private Sub _client_SendProcessTaskResultReceived(ok As Boolean, info As String) Handles _client.SendProcessTaskResultReceived
        If ok Then
            _logger.AddInformation("Операция выполнена " & info)
        Else
            _logger.AddError("Операция не выполнена " & info)
        End If
    End Sub

    Private Sub _client_HostInfoReceived(info() As String) Handles _client.HostInfoReceived
        For Each line In info
            _logger.AddInformation(line)
        Next
    End Sub

    Private Sub bRemoteCmd_Click(sender As Object, e As EventArgs) Handles bRemoteCmd.Click
        _client.CreateRemoteCmdForm(tbTaskId.Text, Text.Split(",")(0)).Show(Me)
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex < 0 Then Return
        Dim row = DataGridView1.Rows(e.RowIndex)
        Dim task As RemoteTaskInfo = row.Tag
        If task IsNot Nothing Then
            cbAutoStart.Checked = task.Autostart
            cbMonitor.Checked = task.RunMonitorState <> "Disabled"
            cbRemoteCmd.Checked = task.RemoteCmd
            tbFile.Text = task.Filename
            tbArguments.Text = task.Arguments
            tbTaskId.Text = task.ID
            tbParameters.Text = task.Parameters
            Try
                cbUploadFrom.Text = ""
                Dim path = IO.File.ReadAllText(IO.Path.Combine(AppBase.DataFolder, tbTaskId.Text + "-uploadpath.txt"))
                cbUploadFrom.Text = path
            Catch ex As Exception

            End Try

            ' tbFil.Text = task.Workdir
        End If
    End Sub

    Private Sub bDelete_Click(sender As Object, e As EventArgs) Handles bDelete.Click
        If _client.Transport.IsConnected Then
            _client.DeleteTask(tbTaskId.Text)
            Dim thread As New Threading.Thread(Sub()
                                                   Me.Invoke(Sub() ShowTasksList())
                                               End Sub)
            thread.Start()
        End If
    End Sub

    Private Sub tScanLocalServers_Tick(sender As Object, e As EventArgs) Handles tScanLocalServers.Tick
        SearchLocalServersAndUIs()
    End Sub

    Private Sub _client_ShortHostInfoReceived(info As String) Handles _client.ShortHostInfoReceived
        Me.Invoke(Sub()
                      tbShortHostInfo.Text = info
                      Me.Text = "RMHC: " + info
                  End Sub)
    End Sub

    Private Sub tbFile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbFile.SelectedIndexChanged
        If tbFile.Text > "" Then
            Dim id As String = ""
            For Each ch In IO.Path.GetFileNameWithoutExtension(tbFile.Text)
                Select Case ch
                    Case "A" To "Z", "a" To "z", "0" To "9", "-", "_"
                        id += ch
                End Select
            Next
            tbTaskId.Text = "ProcessTask_" + id
        End If
    End Sub


    Private Sub lbRemoteUIs_DoubleClick(sender As Object, e As EventArgs) Handles lbRemoteUIs.DoubleClick
        If lbRemoteUIs.Text > "" Then
            Dim addr = lbRemoteUIs.Text.Split(" ")(0)
            TextBox1.Text = addr
        End If
    End Sub

    Private Sub bConnectAutoUi_Click(sender As Object, e As EventArgs) Handles bConnectAutoUi.Click
        If TextBox1.Text > "" Then
            Dim remoteclient As New RemoteAppClient
            remoteclient.MessageTransport.Open(TextBox1.Text, "")
            remoteclient.MessageTransport.RegisterMe("User", "", "RemoteAppClient", "")
            remoteclient.CreateAutoUiForm.Show()
        End If
    End Sub

    Private Sub bFastshell_Click(sender As Object, e As EventArgs) Handles bFastshell.Click
        Dim all = tbFastshell.Text
        If all > "" Then
            Dim cmd = all.Split(" ")(0)
            Dim args = all.Replace(cmd + " ", "")
            Dim workdir = ""
            _client.FastShellCommand(cmd, args, workdir)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        DataGridView1.ClearSelection()
    End Sub

    Private Sub lbLocalServers_Click(sender As Object, e As EventArgs) Handles lbLocalServers.Click
        If lbLocalServers.Text > "" Then
            Dim addr = lbLocalServers.Text.Split(" ")(0)
            settingHostAddress.AssignedSetting.ValueAsString = addr
        End If
        lbLocalServers.ClearSelected()
    End Sub
End Class
