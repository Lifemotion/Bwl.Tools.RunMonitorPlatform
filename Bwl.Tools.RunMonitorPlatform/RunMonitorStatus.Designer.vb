<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RunMonitorStatus
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RunMonitorStatus))
        Me.refreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DatagridLogWriter1 = New Bwl.Framework.DatagridLogWriter()
        Me.exitButton = New System.Windows.Forms.Button()
        Me.processesToolButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'refreshTimer
        '
        Me.refreshTimer.Enabled = True
        Me.refreshTimer.Interval = 500
        '
        'DatagridLogWriter1
        '
        Me.DatagridLogWriter1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DatagridLogWriter1.FilterText = ""
        Me.DatagridLogWriter1.Location = New System.Drawing.Point(289, 0)
        Me.DatagridLogWriter1.LogEnabled = True
        Me.DatagridLogWriter1.Margin = New System.Windows.Forms.Padding(0)
        Me.DatagridLogWriter1.Name = "DatagridLogWriter1"
        Me.DatagridLogWriter1.ShowDebug = False
        Me.DatagridLogWriter1.ShowErrors = True
        Me.DatagridLogWriter1.ShowInformation = True
        Me.DatagridLogWriter1.ShowMessages = True
        Me.DatagridLogWriter1.ShowWarnings = True
        Me.DatagridLogWriter1.Size = New System.Drawing.Size(723, 501)
        Me.DatagridLogWriter1.TabIndex = 0
        '
        'exitButton
        '
        Me.exitButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.exitButton.Location = New System.Drawing.Point(937, 0)
        Me.exitButton.Name = "exitButton"
        Me.exitButton.Size = New System.Drawing.Size(75, 23)
        Me.exitButton.TabIndex = 1
        Me.exitButton.Text = "Exit"
        Me.exitButton.UseVisualStyleBackColor = True
        '
        'processesToolButton
        '
        Me.processesToolButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.processesToolButton.Location = New System.Drawing.Point(856, 0)
        Me.processesToolButton.Name = "processesToolButton"
        Me.processesToolButton.Size = New System.Drawing.Size(75, 23)
        Me.processesToolButton.TabIndex = 2
        Me.processesToolButton.Text = "Processes"
        Me.processesToolButton.UseVisualStyleBackColor = True
        '
        'RunMonitorStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 500)
        Me.Controls.Add(Me.processesToolButton)
        Me.Controls.Add(Me.exitButton)
        Me.Controls.Add(Me.DatagridLogWriter1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "RunMonitorStatus"
        Me.Text = "Bwl RunMonitor Status"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents refreshTimer As Timer
    Friend WithEvents DatagridLogWriter1 As Framework.DatagridLogWriter
    Friend WithEvents exitButton As Button
    Friend WithEvents processesToolButton As Button
End Class
