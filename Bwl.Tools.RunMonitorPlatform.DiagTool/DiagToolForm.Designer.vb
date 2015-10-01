<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiagToolForm
    Inherits Framework.FormAppBase

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
        Me.GetProcesses = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'GetProcesses
        '
        Me.GetProcesses.Location = New System.Drawing.Point(12, 27)
        Me.GetProcesses.Name = "GetProcesses"
        Me.GetProcesses.Size = New System.Drawing.Size(93, 23)
        Me.GetProcesses.TabIndex = 2
        Me.GetProcesses.Text = "GetProcesses"
        Me.GetProcesses.UseVisualStyleBackColor = True
        '
        'DiagToolForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.GetProcesses)
        Me.Name = "DiagToolForm"
        Me.Text = "Bwl RunMonitor DiagToolForm"
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.GetProcesses, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GetProcesses As Button
End Class
