<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HostClient

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
        Me.gbConnect = New System.Windows.Forms.GroupBox()
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.ExtendedView = False
        Me.logWriter.Location = New System.Drawing.Point(2, 511)
        Me.logWriter.Size = New System.Drawing.Size(853, 191)
        '
        'gbConnect
        '
        Me.gbConnect.Location = New System.Drawing.Point(12, 27)
        Me.gbConnect.Name = "gbConnect"
        Me.gbConnect.Size = New System.Drawing.Size(198, 481)
        Me.gbConnect.TabIndex = 2
        Me.gbConnect.TabStop = False
        Me.gbConnect.Text = "Connect"
        '
        'HostClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(856, 701)
        Me.Controls.Add(Me.gbConnect)
        Me.Name = "HostClient"
        Me.Text = "HostClient"
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.gbConnect, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbConnect As GroupBox
End Class
