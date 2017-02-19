Public Class MainWindow
    Friend DebugEnabled As Boolean = True
    Friend MultithreadingEnabled As Boolean = False
    Friend DebugHUDEnabled As Boolean = False
    Friend MissingCrapEnabled As Boolean = False
    Friend InternalAudioLevel As Byte = 100
    Friend AudioEnabled As Boolean = True
    Dim CountingThread1 As Threading.Thread
    Dim CountingThread2 As Threading.Thread
    Friend CThread1Active As Boolean = False
    Friend CThread2Active As Boolean = False
    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DebugStatusLabel.Text = "DEBUG: " & DebugEnabled
        MultithreadingStatusLabel.Text = "Multithreading: " & MultithreadingEnabled
        If DebugEnabled = False Then
            DebugMenuStrip.Visible = False
            DebugMenuStrip.Enabled = False
        End If
        CheckForIllegalCrossThreadCalls = False
    End Sub
    Private Sub DebugDebugMenuMenuStrip_Click(sender As Object, e As EventArgs) Handles DebugDebugMenuMenuStrip.Click
        DebugMenu.ShowDialog()
        DebugStatusLabel.Text = "DEBUG: " & DebugEnabled
        MultithreadingStatusLabel.Text = "Multithreading: " & MultithreadingEnabled
    End Sub

    Private Sub FileSaveMenuStrip_Click(sender As Object, e As EventArgs) Handles FileSaveMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            SaveFileDialog.ShowDialog()
        End If
    End Sub

    Private Sub FileLoadMenuStrip_Click(sender As Object, e As EventArgs) Handles FileLoadMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            OpenFileDialog.ShowDialog()
        End If
    End Sub

    Private Sub FileExitMenuStrip_Click(sender As Object, e As EventArgs) Handles FileExitMenuStrip.Click
        Application.Exit()
    End Sub

    Private Sub SettingsInputMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsInputMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            InputSettings.ShowDialog()
        End If
    End Sub

    Private Sub SettingsGraphicsMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsGraphicsMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            GraphicsSettings.ShowDialog()
        End If
    End Sub

    Private Sub SettingsAudioMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsAudioMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            AudioSettings.ShowDialog()
        End If
    End Sub

    Private Sub SettingsPathsMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsPathsMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            PathsSettings.ShowDialog()
        End If
    End Sub

    Private Sub SettingsUserInterfaceMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsUserInterfaceMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            UserInterfaceSettings.ShowDialog()
        End If
    End Sub

    Private Sub AboutMenuStrip_Click(sender As Object, e As EventArgs) Handles AboutMenuStrip.Click
        AboutThisCrap.ShowDialog()
    End Sub

    Private Sub CrapNotFound()
        Beep()
        MessageBox.Show("This feature hasn't been implemented yet! Since this is only a prototype build, all features are subject to change.", "404 Crap Not Found", MessageBoxButtons.OK, MessageBoxIcon.Question)
    End Sub

    Private Sub BrokenCrap()
        Beep()
        MessageBox.Show("This feature has been implemented, but it doesn't work yet. Hooray!", "Derp", MessageBoxButtons.OK, MessageBoxIcon.Question)
    End Sub

    Private Sub StuffButton1_Click(sender As Object, e As EventArgs) Handles StuffButton1.Click
        If MultithreadingEnabled = True Then
            If CThread2Active = False Then
                CountingThread2 = New Threading.Thread(AddressOf countup2)
                CountingThread2.Start()
                CThread2Active = True
            End If
        Else
            countup2()
        End If
    End Sub

    Private Sub ThreadingTest_Click(sender As Object, e As EventArgs) Handles ThreadingTest.Click
        If MultithreadingEnabled = True Then
            If CThread1Active = False Then
                CountingThread1 = New Threading.Thread(AddressOf countup)
                CountingThread1.Start()
                CThread1Active = True
            End If
        Else
            countup()
        End If
    End Sub

    Public Sub ThreadingInputBeepTest()

    End Sub

    Private Sub countup()
        Try
            Dim i As Integer
            Do Until i = 1000
                i = i + 1
                Label1.Text = i
                Refresh()
            Loop
            CThread1Active = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub countup2()
        Try
            Dim i2 As Integer
            Do Until i2 = 1000
                i2 = i2 + 1
                Label2.Text = i2
                Refresh()
            Loop
            CThread2Active = False
        Catch ex As Exception

        End Try
    End Sub
End Class
