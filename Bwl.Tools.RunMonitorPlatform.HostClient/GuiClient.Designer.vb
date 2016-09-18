<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GuiClient

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.gbConnect = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bFindLocalServers = New System.Windows.Forms.Button()
        Me.lbLocalServers = New System.Windows.Forms.ListBox()
        Me.settingHostAddress = New Bwl.Framework.SettingField()
        Me.bHostConnect = New System.Windows.Forms.Button()
        Me.bRunMonitorRemoteUi = New System.Windows.Forms.Button()
        Me.bSaveSettings = New System.Windows.Forms.Button()
        Me.settingTarget = New Bwl.Framework.SettingField()
        Me.lbTargets = New System.Windows.Forms.ListBox()
        Me.gbTasks = New System.Windows.Forms.GroupBox()
        Me.bRunRemoteShell = New System.Windows.Forms.Button()
        Me.bUpdateTasks = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbTask = New System.Windows.Forms.GroupBox()
        Me.tbFile = New System.Windows.Forms.ComboBox()
        Me.bSelectFolder = New System.Windows.Forms.Button()
        Me.bSet = New System.Windows.Forms.Button()
        Me.bUploadStart = New System.Windows.Forms.Button()
        Me.bUpload = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbUploadFrom = New System.Windows.Forms.TextBox()
        Me.tbParameters = New System.Windows.Forms.TextBox()
        Me.tbArguments = New System.Windows.Forms.TextBox()
        Me.bStart = New System.Windows.Forms.Button()
        Me.bKill = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbTaskId = New System.Windows.Forms.TextBox()
        Me.cbAutoStart = New System.Windows.Forms.CheckBox()
        Me.cbMonitor = New System.Windows.Forms.CheckBox()
        Me.cbRemoteCmd = New System.Windows.Forms.CheckBox()
        Me.tUpdateConnectedState = New System.Windows.Forms.Timer(Me.components)
        Me.gbTargets = New System.Windows.Forms.GroupBox()
        Me.bFindTargets = New System.Windows.Forms.Button()
        Me.tUpdateTasks = New System.Windows.Forms.Timer(Me.components)
        Me.selectFolderDialog = New System.Windows.Forms.OpenFileDialog()
        Me.gbConnect.SuspendLayout()
        Me.gbTasks.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTask.SuspendLayout()
        Me.gbTargets.SuspendLayout()
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.ExtendedView = False
        Me.logWriter.Location = New System.Drawing.Point(2, 540)
        Me.logWriter.Size = New System.Drawing.Size(1001, 185)
        '
        'gbConnect
        '
        Me.gbConnect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbConnect.Controls.Add(Me.Label1)
        Me.gbConnect.Controls.Add(Me.bFindLocalServers)
        Me.gbConnect.Controls.Add(Me.lbLocalServers)
        Me.gbConnect.Controls.Add(Me.settingHostAddress)
        Me.gbConnect.Controls.Add(Me.bHostConnect)
        Me.gbConnect.Location = New System.Drawing.Point(12, 28)
        Me.gbConnect.Name = "gbConnect"
        Me.gbConnect.Size = New System.Drawing.Size(261, 218)
        Me.gbConnect.TabIndex = 2
        Me.gbConnect.TabStop = False
        Me.gbConnect.Text = "Host Connect"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Local servers:"
        '
        'bFindLocalServers
        '
        Me.bFindLocalServers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bFindLocalServers.Location = New System.Drawing.Point(9, 113)
        Me.bFindLocalServers.Name = "bFindLocalServers"
        Me.bFindLocalServers.Size = New System.Drawing.Size(240, 23)
        Me.bFindLocalServers.TabIndex = 9
        Me.bFindLocalServers.Text = "Find local servers"
        Me.bFindLocalServers.UseVisualStyleBackColor = True
        '
        'lbLocalServers
        '
        Me.lbLocalServers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbLocalServers.FormattingEnabled = True
        Me.lbLocalServers.Location = New System.Drawing.Point(9, 38)
        Me.lbLocalServers.Name = "lbLocalServers"
        Me.lbLocalServers.Size = New System.Drawing.Size(240, 69)
        Me.lbLocalServers.TabIndex = 8
        '
        'settingHostAddress
        '
        Me.settingHostAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.settingHostAddress.AssignedSetting = Nothing
        Me.settingHostAddress.DesignText = Nothing
        Me.settingHostAddress.Location = New System.Drawing.Point(3, 137)
        Me.settingHostAddress.Name = "settingHostAddress"
        Me.settingHostAddress.Size = New System.Drawing.Size(252, 40)
        Me.settingHostAddress.TabIndex = 7
        '
        'bHostConnect
        '
        Me.bHostConnect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bHostConnect.Location = New System.Drawing.Point(8, 185)
        Me.bHostConnect.Name = "bHostConnect"
        Me.bHostConnect.Size = New System.Drawing.Size(241, 23)
        Me.bHostConnect.TabIndex = 2
        Me.bHostConnect.Text = "Connect"
        Me.bHostConnect.UseVisualStyleBackColor = True
        '
        'bRunMonitorRemoteUi
        '
        Me.bRunMonitorRemoteUi.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bRunMonitorRemoteUi.Location = New System.Drawing.Point(9, 222)
        Me.bRunMonitorRemoteUi.Name = "bRunMonitorRemoteUi"
        Me.bRunMonitorRemoteUi.Size = New System.Drawing.Size(240, 23)
        Me.bRunMonitorRemoteUi.TabIndex = 12
        Me.bRunMonitorRemoteUi.Text = "Open Remote HostControl UI"
        Me.bRunMonitorRemoteUi.UseVisualStyleBackColor = True
        '
        'bSaveSettings
        '
        Me.bSaveSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bSaveSettings.Location = New System.Drawing.Point(9, 250)
        Me.bSaveSettings.Name = "bSaveSettings"
        Me.bSaveSettings.Size = New System.Drawing.Size(240, 23)
        Me.bSaveSettings.TabIndex = 11
        Me.bSaveSettings.Text = "Save Settings"
        Me.bSaveSettings.UseVisualStyleBackColor = True
        '
        'settingTarget
        '
        Me.settingTarget.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.settingTarget.AssignedSetting = Nothing
        Me.settingTarget.DesignText = Nothing
        Me.settingTarget.Location = New System.Drawing.Point(3, 174)
        Me.settingTarget.Name = "settingTarget"
        Me.settingTarget.Size = New System.Drawing.Size(252, 42)
        Me.settingTarget.TabIndex = 6
        '
        'lbTargets
        '
        Me.lbTargets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTargets.FormattingEnabled = True
        Me.lbTargets.Location = New System.Drawing.Point(9, 59)
        Me.lbTargets.Name = "lbTargets"
        Me.lbTargets.Size = New System.Drawing.Size(240, 108)
        Me.lbTargets.TabIndex = 3
        '
        'gbTasks
        '
        Me.gbTasks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTasks.Controls.Add(Me.bRunRemoteShell)
        Me.gbTasks.Controls.Add(Me.bUpdateTasks)
        Me.gbTasks.Controls.Add(Me.DataGridView1)
        Me.gbTasks.Enabled = False
        Me.gbTasks.Location = New System.Drawing.Point(279, 28)
        Me.gbTasks.Name = "gbTasks"
        Me.gbTasks.Size = New System.Drawing.Size(713, 318)
        Me.gbTasks.TabIndex = 3
        Me.gbTasks.TabStop = False
        Me.gbTasks.Text = "Remote Tasks"
        '
        'bRunRemoteShell
        '
        Me.bRunRemoteShell.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bRunRemoteShell.Location = New System.Drawing.Point(6, 289)
        Me.bRunRemoteShell.Name = "bRunRemoteShell"
        Me.bRunRemoteShell.Size = New System.Drawing.Size(117, 23)
        Me.bRunRemoteShell.TabIndex = 11
        Me.bRunRemoteShell.Text = "Run Remote Shell"
        Me.bRunRemoteShell.UseVisualStyleBackColor = True
        '
        'bUpdateTasks
        '
        Me.bUpdateTasks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bUpdateTasks.Location = New System.Drawing.Point(624, 289)
        Me.bUpdateTasks.Name = "bUpdateTasks"
        Me.bUpdateTasks.Size = New System.Drawing.Size(83, 23)
        Me.bUpdateTasks.TabIndex = 10
        Me.bUpdateTasks.Text = "Update"
        Me.bUpdateTasks.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(6, 19)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(701, 264)
        Me.DataGridView1.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Task ID"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 200
        '
        'Column2
        '
        Me.Column2.HeaderText = "State"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 80
        '
        'Column3
        '
        Me.Column3.HeaderText = "Autostart"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 60
        '
        'Column4
        '
        Me.Column4.HeaderText = "RunMon"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 60
        '
        'gbTask
        '
        Me.gbTask.Controls.Add(Me.tbFile)
        Me.gbTask.Controls.Add(Me.bSelectFolder)
        Me.gbTask.Controls.Add(Me.bSet)
        Me.gbTask.Controls.Add(Me.bUploadStart)
        Me.gbTask.Controls.Add(Me.bUpload)
        Me.gbTask.Controls.Add(Me.Label7)
        Me.gbTask.Controls.Add(Me.Label6)
        Me.gbTask.Controls.Add(Me.Label5)
        Me.gbTask.Controls.Add(Me.Label4)
        Me.gbTask.Controls.Add(Me.cbUploadFrom)
        Me.gbTask.Controls.Add(Me.tbParameters)
        Me.gbTask.Controls.Add(Me.tbArguments)
        Me.gbTask.Controls.Add(Me.bStart)
        Me.gbTask.Controls.Add(Me.bKill)
        Me.gbTask.Controls.Add(Me.Label3)
        Me.gbTask.Controls.Add(Me.tbTaskId)
        Me.gbTask.Controls.Add(Me.cbAutoStart)
        Me.gbTask.Controls.Add(Me.cbMonitor)
        Me.gbTask.Controls.Add(Me.cbRemoteCmd)
        Me.gbTask.Enabled = False
        Me.gbTask.Location = New System.Drawing.Point(279, 352)
        Me.gbTask.Name = "gbTask"
        Me.gbTask.Size = New System.Drawing.Size(713, 183)
        Me.gbTask.TabIndex = 4
        Me.gbTask.TabStop = False
        Me.gbTask.Text = "Task"
        '
        'tbFile
        '
        Me.tbFile.FormattingEnabled = True
        Me.tbFile.Location = New System.Drawing.Point(78, 45)
        Me.tbFile.Name = "tbFile"
        Me.tbFile.Size = New System.Drawing.Size(302, 21)
        Me.tbFile.TabIndex = 28
        '
        'bSelectFolder
        '
        Me.bSelectFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bSelectFolder.Location = New System.Drawing.Point(631, 100)
        Me.bSelectFolder.Name = "bSelectFolder"
        Me.bSelectFolder.Size = New System.Drawing.Size(75, 20)
        Me.bSelectFolder.TabIndex = 27
        Me.bSelectFolder.Text = "Select"
        Me.bSelectFolder.UseVisualStyleBackColor = True
        '
        'bSet
        '
        Me.bSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bSet.Location = New System.Drawing.Point(78, 149)
        Me.bSet.Name = "bSet"
        Me.bSet.Size = New System.Drawing.Size(75, 23)
        Me.bSet.TabIndex = 26
        Me.bSet.Text = "Set"
        Me.bSet.UseVisualStyleBackColor = True
        '
        'bUploadStart
        '
        Me.bUploadStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bUploadStart.Location = New System.Drawing.Point(607, 150)
        Me.bUploadStart.Name = "bUploadStart"
        Me.bUploadStart.Size = New System.Drawing.Size(99, 23)
        Me.bUploadStart.TabIndex = 24
        Me.bUploadStart.Text = "Upload && Start"
        Me.bUploadStart.UseVisualStyleBackColor = True
        '
        'bUpload
        '
        Me.bUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bUpload.Location = New System.Drawing.Point(526, 150)
        Me.bUpload.Name = "bUpload"
        Me.bUpload.Size = New System.Drawing.Size(75, 23)
        Me.bUpload.TabIndex = 23
        Me.bUpload.Text = "Upload"
        Me.bUpload.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 103)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Upload from:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Parameters:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(386, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Arguments:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Run File:"
        '
        'cbUploadFrom
        '
        Me.cbUploadFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbUploadFrom.Location = New System.Drawing.Point(78, 100)
        Me.cbUploadFrom.Name = "cbUploadFrom"
        Me.cbUploadFrom.Size = New System.Drawing.Size(547, 20)
        Me.cbUploadFrom.TabIndex = 18
        '
        'tbParameters
        '
        Me.tbParameters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbParameters.Location = New System.Drawing.Point(78, 73)
        Me.tbParameters.Name = "tbParameters"
        Me.tbParameters.Size = New System.Drawing.Size(629, 20)
        Me.tbParameters.TabIndex = 17
        '
        'tbArguments
        '
        Me.tbArguments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbArguments.Location = New System.Drawing.Point(458, 46)
        Me.tbArguments.Name = "tbArguments"
        Me.tbArguments.Size = New System.Drawing.Size(248, 20)
        Me.tbArguments.TabIndex = 16
        '
        'bStart
        '
        Me.bStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bStart.Location = New System.Drawing.Point(445, 150)
        Me.bStart.Name = "bStart"
        Me.bStart.Size = New System.Drawing.Size(75, 23)
        Me.bStart.TabIndex = 13
        Me.bStart.Text = "Start"
        Me.bStart.UseVisualStyleBackColor = True
        '
        'bKill
        '
        Me.bKill.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bKill.Location = New System.Drawing.Point(364, 150)
        Me.bKill.Name = "bKill"
        Me.bKill.Size = New System.Drawing.Size(75, 23)
        Me.bKill.TabIndex = 12
        Me.bKill.Text = "Kill"
        Me.bKill.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Task ID:"
        '
        'tbTaskId
        '
        Me.tbTaskId.Location = New System.Drawing.Point(78, 18)
        Me.tbTaskId.Name = "tbTaskId"
        Me.tbTaskId.Size = New System.Drawing.Size(302, 20)
        Me.tbTaskId.TabIndex = 2
        Me.tbTaskId.Text = "ProcessTask_"
        '
        'cbAutoStart
        '
        Me.cbAutoStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbAutoStart.AutoSize = True
        Me.cbAutoStart.Location = New System.Drawing.Point(543, 18)
        Me.cbAutoStart.Name = "cbAutoStart"
        Me.cbAutoStart.Size = New System.Drawing.Size(68, 17)
        Me.cbAutoStart.TabIndex = 1
        Me.cbAutoStart.Text = "Autostart"
        Me.cbAutoStart.UseVisualStyleBackColor = True
        '
        'cbMonitor
        '
        Me.cbMonitor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbMonitor.AutoSize = True
        Me.cbMonitor.Location = New System.Drawing.Point(417, 18)
        Me.cbMonitor.Name = "cbMonitor"
        Me.cbMonitor.Size = New System.Drawing.Size(117, 17)
        Me.cbMonitor.TabIndex = 0
        Me.cbMonitor.Text = "RunMonitor Enable"
        Me.cbMonitor.UseVisualStyleBackColor = True
        '
        'cbRemoteCmd
        '
        Me.cbRemoteCmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRemoteCmd.AutoSize = True
        Me.cbRemoteCmd.Location = New System.Drawing.Point(620, 18)
        Me.cbRemoteCmd.Name = "cbRemoteCmd"
        Me.cbRemoteCmd.Size = New System.Drawing.Size(84, 17)
        Me.cbRemoteCmd.TabIndex = 25
        Me.cbRemoteCmd.Text = "RemoteCmd"
        Me.cbRemoteCmd.UseVisualStyleBackColor = True
        '
        'tUpdateConnectedState
        '
        Me.tUpdateConnectedState.Enabled = True
        Me.tUpdateConnectedState.Interval = 500
        '
        'gbTargets
        '
        Me.gbTargets.Controls.Add(Me.bFindTargets)
        Me.gbTargets.Controls.Add(Me.bRunMonitorRemoteUi)
        Me.gbTargets.Controls.Add(Me.lbTargets)
        Me.gbTargets.Controls.Add(Me.bSaveSettings)
        Me.gbTargets.Controls.Add(Me.settingTarget)
        Me.gbTargets.Enabled = False
        Me.gbTargets.Location = New System.Drawing.Point(12, 252)
        Me.gbTargets.Name = "gbTargets"
        Me.gbTargets.Size = New System.Drawing.Size(261, 283)
        Me.gbTargets.TabIndex = 12
        Me.gbTargets.TabStop = False
        Me.gbTargets.Text = "Targets"
        '
        'bFindTargets
        '
        Me.bFindTargets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bFindTargets.Location = New System.Drawing.Point(9, 19)
        Me.bFindTargets.Name = "bFindTargets"
        Me.bFindTargets.Size = New System.Drawing.Size(240, 23)
        Me.bFindTargets.TabIndex = 13
        Me.bFindTargets.Text = "Find targets"
        Me.bFindTargets.UseVisualStyleBackColor = True
        '
        'tUpdateTasks
        '
        Me.tUpdateTasks.Interval = 3000
        '
        'selectFolderDialog
        '
        Me.selectFolderDialog.CheckFileExists = False
        Me.selectFolderDialog.FileName = "Folder Selection"
        Me.selectFolderDialog.ValidateNames = False
        '
        'GuiClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 752)
        Me.Controls.Add(Me.gbTargets)
        Me.Controls.Add(Me.gbTask)
        Me.Controls.Add(Me.gbTasks)
        Me.Controls.Add(Me.gbConnect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "GuiClient"
        Me.Text = "Bwl RunMonitor Remote Host Client"
        Me.Controls.SetChildIndex(Me.gbConnect, 0)
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.gbTasks, 0)
        Me.Controls.SetChildIndex(Me.gbTask, 0)
        Me.Controls.SetChildIndex(Me.gbTargets, 0)
        Me.gbConnect.ResumeLayout(False)
        Me.gbConnect.PerformLayout()
        Me.gbTasks.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTask.ResumeLayout(False)
        Me.gbTask.PerformLayout()
        Me.gbTargets.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbConnect As GroupBox
    Friend WithEvents bHostConnect As Button
    Friend WithEvents lbTargets As ListBox
    Friend WithEvents settingHostAddress As Framework.SettingField
    Friend WithEvents settingTarget As Framework.SettingField
    Friend WithEvents Label1 As Label
    Friend WithEvents bFindLocalServers As Button
    Friend WithEvents lbLocalServers As ListBox
    Friend WithEvents gbTasks As GroupBox
    Friend WithEvents bUpdateTasks As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents gbTask As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents tbTaskId As TextBox
    Friend WithEvents cbAutoStart As CheckBox
    Friend WithEvents cbMonitor As CheckBox
    Friend WithEvents bStart As Button
    Friend WithEvents bKill As Button
    Friend WithEvents bUploadStart As Button
    Friend WithEvents bUpload As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cbUploadFrom As TextBox
    Friend WithEvents tbParameters As TextBox
    Friend WithEvents tbArguments As TextBox
    Friend WithEvents cbRemoteCmd As CheckBox
    Friend WithEvents bSet As Button
    Friend WithEvents bSaveSettings As Button
    Friend WithEvents bRunRemoteShell As Button
    Friend WithEvents bRunMonitorRemoteUi As Button
    Friend WithEvents tUpdateConnectedState As Timer
    Friend WithEvents gbTargets As GroupBox
    Friend WithEvents tUpdateTasks As Timer
    Friend WithEvents bFindTargets As Button
    Friend WithEvents bSelectFolder As Button
    Friend WithEvents tbFile As ComboBox
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents selectFolderDialog As OpenFileDialog
End Class
