<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProcessesForm
    Inherits System.Windows.Forms.Form

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
        Me.processesList = New System.Windows.Forms.DataGridView()
        Me.RefreshButton = New System.Windows.Forms.Button()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProcessName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.File = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Additional = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KillWerFaults = New System.Windows.Forms.Button()
        Me.KillSelected = New System.Windows.Forms.Button()
        CType(Me.processesList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'processesList
        '
        Me.processesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.processesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.processesList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.ProcessName, Me.File, Me.Additional})
        Me.processesList.Location = New System.Drawing.Point(12, 41)
        Me.processesList.Name = "processesList"
        Me.processesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.processesList.Size = New System.Drawing.Size(554, 418)
        Me.processesList.TabIndex = 0
        '
        'RefreshButton
        '
        Me.RefreshButton.Location = New System.Drawing.Point(12, 12)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(75, 23)
        Me.RefreshButton.TabIndex = 1
        Me.RefreshButton.Text = "Refresh"
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        '
        'ProcessName
        '
        Me.ProcessName.HeaderText = "Name"
        Me.ProcessName.Name = "ProcessName"
        '
        'File
        '
        Me.File.HeaderText = "File"
        Me.File.Name = "File"
        '
        'Additional
        '
        Me.Additional.HeaderText = "Additional"
        Me.Additional.Name = "Additional"
        '
        'KillWerFaults
        '
        Me.KillWerFaults.Location = New System.Drawing.Point(93, 12)
        Me.KillWerFaults.Name = "KillWerFaults"
        Me.KillWerFaults.Size = New System.Drawing.Size(92, 23)
        Me.KillWerFaults.TabIndex = 2
        Me.KillWerFaults.Text = "KillWerFaults"
        Me.KillWerFaults.UseVisualStyleBackColor = True
        '
        'KillSelected
        '
        Me.KillSelected.Location = New System.Drawing.Point(191, 12)
        Me.KillSelected.Name = "KillSelected"
        Me.KillSelected.Size = New System.Drawing.Size(92, 23)
        Me.KillSelected.TabIndex = 3
        Me.KillSelected.Text = "Kill Selected"
        Me.KillSelected.UseVisualStyleBackColor = True
        '
        'ProcessesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 471)
        Me.Controls.Add(Me.KillSelected)
        Me.Controls.Add(Me.KillWerFaults)
        Me.Controls.Add(Me.RefreshButton)
        Me.Controls.Add(Me.processesList)
        Me.Name = "ProcessesForm"
        Me.Text = "Processes"
        CType(Me.processesList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents processesList As DataGridView
    Friend WithEvents RefreshButton As Button
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents ProcessName As DataGridViewTextBoxColumn
    Friend WithEvents File As DataGridViewTextBoxColumn
    Friend WithEvents Additional As DataGridViewTextBoxColumn
    Friend WithEvents KillWerFaults As Button
    Friend WithEvents KillSelected As Button
End Class
