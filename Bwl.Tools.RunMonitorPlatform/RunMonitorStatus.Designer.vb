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
        Me.refreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DatagridLogWriter1 = New Bwl.Framework.DatagridLogWriter()
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
        'RunMonitorStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 500)
        Me.Controls.Add(Me.DatagridLogWriter1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "RunMonitorStatus"
        Me.Text = "RunMonitorStatus"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents refreshTimer As Timer
    Friend WithEvents DatagridLogWriter1 As Framework.DatagridLogWriter
End Class
