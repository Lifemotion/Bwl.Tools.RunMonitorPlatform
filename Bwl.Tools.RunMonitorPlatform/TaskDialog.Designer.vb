<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TaskDialog
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
		Me._tbName = New System.Windows.Forms.TextBox()
		Me._tbPath = New System.Windows.Forms.TextBox()
		Me._btnOk = New System.Windows.Forms.Button()
		Me._btnCancel = New System.Windows.Forms.Button()
		Me._btnPath = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me._txtArgs = New System.Windows.Forms.TextBox()
		Me.SuspendLayout()
		'
		'_tbName
		'
		Me._tbName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me._tbName.Location = New System.Drawing.Point(79, 12)
		Me._tbName.Name = "_tbName"
		Me._tbName.Size = New System.Drawing.Size(219, 20)
		Me._tbName.TabIndex = 0
		Me._tbName.Text = "Задача1"
		'
		'_tbPath
		'
		Me._tbPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me._tbPath.Location = New System.Drawing.Point(79, 38)
		Me._tbPath.Name = "_tbPath"
		Me._tbPath.Size = New System.Drawing.Size(219, 20)
		Me._tbPath.TabIndex = 1
		'
		'_btnOk
		'
		Me._btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me._btnOk.Location = New System.Drawing.Point(223, 99)
		Me._btnOk.Name = "_btnOk"
		Me._btnOk.Size = New System.Drawing.Size(75, 23)
		Me._btnOk.TabIndex = 2
		Me._btnOk.Text = "ОК"
		Me._btnOk.UseVisualStyleBackColor = True
		'
		'_btnCancel
		'
		Me._btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me._btnCancel.Location = New System.Drawing.Point(304, 99)
		Me._btnCancel.Name = "_btnCancel"
		Me._btnCancel.Size = New System.Drawing.Size(75, 23)
		Me._btnCancel.TabIndex = 3
		Me._btnCancel.Text = "Отмена"
		Me._btnCancel.UseVisualStyleBackColor = True
		'
		'_btnPath
		'
		Me._btnPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me._btnPath.Location = New System.Drawing.Point(304, 36)
		Me._btnPath.Name = "_btnPath"
		Me._btnPath.Size = New System.Drawing.Size(75, 23)
		Me._btnPath.TabIndex = 4
		Me._btnPath.Text = "Обзор..."
		Me._btnPath.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(13, 15)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(60, 13)
		Me.Label1.TabIndex = 5
		Me.Label1.Text = "Название:"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(13, 41)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(34, 13)
		Me.Label2.TabIndex = 6
		Me.Label2.Text = "Путь:"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(13, 67)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(66, 13)
		Me.Label3.TabIndex = 8
		Me.Label3.Text = "Аргументы:"
		'
		'_txtArgs
		'
		Me._txtArgs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me._txtArgs.Location = New System.Drawing.Point(79, 64)
		Me._txtArgs.Name = "_txtArgs"
		Me._txtArgs.Size = New System.Drawing.Size(219, 20)
		Me._txtArgs.TabIndex = 7
		'
		'TaskDialog
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(391, 134)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me._txtArgs)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me._btnPath)
		Me.Controls.Add(Me._btnCancel)
		Me.Controls.Add(Me._btnOk)
		Me.Controls.Add(Me._tbPath)
		Me.Controls.Add(Me._tbName)
		Me.Name = "TaskDialog"
		Me.ShowIcon = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Новый процесс"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents _btnOk As System.Windows.Forms.Button
	Friend WithEvents _btnCancel As System.Windows.Forms.Button
	Friend WithEvents _btnPath As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Private WithEvents _tbName As System.Windows.Forms.TextBox
	Private WithEvents _tbPath As System.Windows.Forms.TextBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Private WithEvents _txtArgs As System.Windows.Forms.TextBox
End Class
