Imports Bwl.Framework

Public Class RunMonitorStatus
    Private _tasks As ITask()

    Public Sub New(logger As Logger)

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        logger.ConnectWriter(DatagridLogWriter1)
        ' Добавить код инициализации после вызова InitializeComponent().

    End Sub

    Public Property Tasks As ITask()
        Get
            Return _tasks
        End Get
        Set(value As ITask())
            _tasks = value
            Me.Invoke(Sub()
                          Dim tcmp As New TaskDisplay()
                          Me.Width = tcmp.Width + DatagridLogWriter1.Width + 10
                          DatagridLogWriter1.Left = tcmp.Width + 10
                          DatagridLogWriter1.Width = Me.Width - tcmp.Width - 30
                          Dim height = tcmp.Height
                          Me.Height = height * _tasks.Count + 50
                          Dim i As Integer
                          For Each task In _tasks
                              Dim cmp As New TaskDisplay()
                              Controls.Add(cmp)
                              cmp.Left = 0
                              cmp.Top = i * height
                              i += 1
                              cmp.Task = task
                              cmp.Refresh()
                          Next
                      End Sub)
            Refresh()
        End Set
    End Property

    Private Sub refresh_Tick(sender As Object, e As EventArgs) Handles refreshTimer.Tick
        For Each cmp In Controls
            cmp.refresh
        Next
        Refresh()
    End Sub

    Private Sub RunMonitorStatus_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
    End Sub
End Class