
Imports System.Windows.Forms

Public Class TaskDialog

    Public Sub New(appInfo As AppInfo)
        InitializeComponent()
        If appInfo IsNot Nothing Then
            _tbPath.Text = appInfo.Path
            _tbName.Text = appInfo.ID
            _txtArgs.Text = appInfo.Args
            Text = "Редактирование"
        End If
    End Sub

    Private Sub bPath_Click(sender As Object, e As EventArgs) Handles _btnPath.Click
        Dim dialog As New OpenFileDialog()
        dialog.CheckPathExists = True
        dialog.CheckFileExists = True
        dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
        If DialogResult.OK = dialog.ShowDialog Then
            _tbPath.Text = dialog.FileName
        End If
    End Sub

    Public ReadOnly Property Path As String
        Get
            Return _tbPath.Text
        End Get
    End Property

    Public ReadOnly Property AppName As String
        Get
            Return _tbName.Text
        End Get
    End Property

    Public ReadOnly Property Args As String
        Get
            Return _txtArgs.Text
        End Get
    End Property
End Class