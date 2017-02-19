Public Class GameWindow
    Dim Test1 As Integer
    Dim Test2 As Integer
    Dim PhysicsTimerInterval As Integer = 100
    Dim PhysicsTimerCall As New Threading.TimerCallback(AddressOf MoveYoCrap)
    Dim PhysicsTimer As New Threading.Timer(PhysicsTimerCall, vbNull, 1000, PhysicsTimerInterval)
    Dim ThreadCountTimerInterval As Integer = 100
    Dim ThreadCountTimerCall As New Threading.TimerCallback(AddressOf UpdateThreadCount)
    Dim ThreadCountTimer As New Threading.Timer(ThreadCountTimerCall, vbNull, 1000, ThreadCountTimerInterval)
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub MoveYoCrap()
        MegaManTestBox.Left = MegaManTestBox.Left + 10
    End Sub
    Private Sub UpdateThreadCount()
        Threading.ThreadPool.GetAvailableThreads(Test1, Test2)
        ThreadCountLabel.Text = Test1 & " " & Test2
    End Sub
End Class