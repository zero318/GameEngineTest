Public Class MainWindow
    Friend DebugEnabled As Boolean = True
    Friend MultithreadingEnabled As Boolean = False
    Friend DebugHUDEnabled As Boolean = False
    Friend MissingCrapEnabled As Boolean = False
    Friend InternalAudioLevel As Byte = 100
    Friend AudioEnabled As Boolean = True
    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DebugStatusLabel.Text = "DEBUG: " & DebugEnabled
        MultithreadingStatusLabel.Text = "Multithreading: " & MultithreadingEnabled
        If DebugEnabled = False Then
            DebugMenuStrip.Visible = False
            DebugMenuStrip.Enabled = False
        End If
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
        MessageBox.Show("I <3 Paige ^_^")
    End Sub

    Private Sub ThreadingTest_Click(sender As Object, e As EventArgs) Handles ThreadingTest.Click

    End Sub
End Class
