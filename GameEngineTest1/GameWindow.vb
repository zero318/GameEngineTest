Public Class GameWindow
    Dim Test1 As Integer
    Dim Test2 As Integer
    Dim TimerInterval As Integer = 100
    Dim TimerStartDelay As Integer = 1000
    Dim PhysicsTimerCall As New Threading.TimerCallback(AddressOf MoveYoCrap)
    Dim PhysicsTimer As New Threading.Timer(PhysicsTimerCall, vbNull, TimerStartDelay, TimerInterval)
    Dim ThreadCountTimerCall As New Threading.TimerCallback(AddressOf UpdateThreadCount)
    Dim ThreadCountTimer As New Threading.Timer(ThreadCountTimerCall, vbNull, TimerStartDelay, TimerInterval)
    Dim MegaManAnimation As Byte
    Dim MegaManAnimationTimerCall As New Threading.TimerCallback(AddressOf AnimateMegaMan)
    Dim MegaManAnimationTimer As New Threading.Timer(MegaManAnimationTimerCall, vbNull, TimerStartDelay, TimerInterval)
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub MoveYoCrap()
        MegaManTestBox.Left = MegaManTestBox.Left + 10
    End Sub
    Private Sub UpdateThreadCount()
        Threading.ThreadPool.GetAvailableThreads(Test1, Test2)
        ThreadCountLabel.Text = Test1 & " " & Test2
    End Sub
    Private Sub AnimateMegaMan()

    End Sub
    Private Sub GameWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub
    Private Sub GameWindow_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

    End Sub
    Private Sub GameWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp

    End Sub
    Private Sub GameWindow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles MyBase.PreviewKeyDown

    End Sub
End Class