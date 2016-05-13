<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TaskDisplay
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.stateListbox = New System.Windows.Forms.ListBox()
        Me.lbDescription = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisableEnableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DoAction1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DoAction2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DoAction3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lbShortMode = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lbShortMode)
        Me.GroupBox1.Controls.Add(Me.stateListbox)
        Me.GroupBox1.Controls.Add(Me.lbDescription)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 171)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'stateListbox
        '
        Me.stateListbox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.stateListbox.FormattingEnabled = True
        Me.stateListbox.HorizontalScrollbar = True
        Me.stateListbox.Location = New System.Drawing.Point(5, 56)
        Me.stateListbox.Name = "stateListbox"
        Me.stateListbox.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.stateListbox.Size = New System.Drawing.Size(282, 108)
        Me.stateListbox.TabIndex = 3
        '
        'lbDescription
        '
        Me.lbDescription.AutoEllipsis = True
        Me.lbDescription.AutoSize = True
        Me.lbDescription.Location = New System.Drawing.Point(7, 38)
        Me.lbDescription.Name = "lbDescription"
        Me.lbDescription.Size = New System.Drawing.Size(60, 13)
        Me.lbDescription.TabIndex = 0
        Me.lbDescription.Text = "Description"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisableEnableToolStripMenuItem, Me.DoAction1ToolStripMenuItem, Me.DoAction2ToolStripMenuItem, Me.DoAction3ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 92)
        '
        'DisableEnableToolStripMenuItem
        '
        Me.DisableEnableToolStripMenuItem.Name = "DisableEnableToolStripMenuItem"
        Me.DisableEnableToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DisableEnableToolStripMenuItem.Text = "Disable\Enable"
        '
        'DoAction1ToolStripMenuItem
        '
        Me.DoAction1ToolStripMenuItem.Name = "DoAction1ToolStripMenuItem"
        Me.DoAction1ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DoAction1ToolStripMenuItem.Text = "Do Action #1"
        '
        'DoAction2ToolStripMenuItem
        '
        Me.DoAction2ToolStripMenuItem.Name = "DoAction2ToolStripMenuItem"
        Me.DoAction2ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DoAction2ToolStripMenuItem.Text = "Do Action #2"
        '
        'DoAction3ToolStripMenuItem
        '
        Me.DoAction3ToolStripMenuItem.Name = "DoAction3ToolStripMenuItem"
        Me.DoAction3ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DoAction3ToolStripMenuItem.Text = "Do Action #3"
        '
        'lbShortMode
        '
        Me.lbShortMode.AutoSize = True
        Me.lbShortMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lbShortMode.Location = New System.Drawing.Point(6, 14)
        Me.lbShortMode.Name = "lbShortMode"
        Me.lbShortMode.Size = New System.Drawing.Size(105, 20)
        Me.lbShortMode.TabIndex = 5
        Me.lbShortMode.Text = "Name-State"
        '
        'TaskDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "TaskDisplay"
        Me.Size = New System.Drawing.Size(300, 177)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lbDescription As Label
    Friend WithEvents stateListbox As ListBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents DisableEnableToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DoAction1ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DoAction2ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DoAction3ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lbShortMode As Label
End Class
