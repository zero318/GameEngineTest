Imports System.Threading
Public Class DebugWindow
    Dim TimerInterval As Integer = 100
    Dim TimerStartDelay As Integer = 100
    Dim DebugLabelTimerCall As New TimerCallback(AddressOf UpdateDebugLabel)
    'Dim DebugLabelTimer As New Timer(DebugLabelTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim DebugLabelTimer As New Timer(DebugLabelTimerCall, vbNull, TimerStartDelay, TimerInterval)
    Private Sub DebugWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Shown
        'DebugLabelTimer.Change(TimerStartDelay, TimerInterval)
    End Sub
    Private Sub UpdateDebugLabel()
        'Try
        DebugOutputLabel.Text = GameWindow.ThreadCountLabel.Text
        Refresh()
        'Catch ex As Exception

        'End Try
    End Sub
End Class