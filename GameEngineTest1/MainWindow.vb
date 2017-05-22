Public Class MainWindow
    Friend DebugEnabled As Boolean = True 'This variable merely controls whether the user can access the debug menu.
    Friend MultithreadingEnabled As Boolean = True
    Friend SkipToGame As Boolean = True
    Friend DebugHUDEnabled As Boolean = False
    Friend MissingCrapEnabled As Boolean = False 'This lets us get into all the broken/empty menus.
    Friend InternalAudioLevel As Byte = 100 'This does nothing. :D
    Friend DebugBoundingBoxes As Boolean = False
    Friend AudioEnabled As Boolean = True
    Dim CountingThread1 As Threading.Thread
    Dim CountingThread2 As Threading.Thread
    Friend KeyboardInputThreas As Threading.Thread
    Friend CThread1Active As Boolean = False
    Friend CThread2Active As Boolean = False
    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load 'This replaces some placeholder text at runtime
        DebugStatusLabel.Text = "DEBUG: " & DebugEnabled
        MultithreadingStatusLabel.Text = "Multithreading: " & MultithreadingEnabled
        If DebugEnabled = False Then
            DebugMenuStrip.Visible = False
            DebugMenuStrip.Enabled = False
        End If
        CheckForIllegalCrossThreadCalls = False
        If SkipToGame = True Then
            StartGame()
        End If
    End Sub
    Private Sub DebugDebugMenuMenuStrip_Click(sender As Object, e As EventArgs) Handles DebugDebugMenuMenuStrip.Click
        DebugMenu.ShowDialog() 'This loads the DebugMenu.vb form
        DebugStatusLabel.Text = "DEBUG: " & DebugEnabled
        MultithreadingStatusLabel.Text = "Multithreading: " & MultithreadingEnabled
    End Sub
    Private Sub FileSaveMenuStrip_Click(sender As Object, e As EventArgs) Handles FileSaveMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            SaveFileDialog.ShowDialog() 'This loads the Save dialog
        End If
    End Sub
    Private Sub FileLoadMenuStrip_Click(sender As Object, e As EventArgs) Handles FileLoadMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            OpenFileDialog.ShowDialog() 'This loads the Open dialog
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
            InputSettings.ShowDialog() 'This loads the InputSettings.vb form
        End If
    End Sub
    Private Sub SettingsGraphicsMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsGraphicsMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            GraphicsSettings.ShowDialog() 'This loads the GraphicsSettings.vb form
        End If
    End Sub
    Private Sub SettingsAudioMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsAudioMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            AudioSettings.ShowDialog() 'This loads the AudioSettings.vb form
        End If
    End Sub
    Private Sub SettingsPathsMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsPathsMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            PathsSettings.ShowDialog() 'This loads the PathsSettings.vb form
        End If
    End Sub
    Private Sub SettingsUserInterfaceMenuStrip_Click(sender As Object, e As EventArgs) Handles SettingsUserInterfaceMenuStrip.Click
        If MissingCrapEnabled = False Then
            CrapNotFound()
        Else
            BrokenCrap()
            UserInterfaceSettings.ShowDialog() 'This loads the UserInterfaceSettings.vb form
        End If
    End Sub
    Private Sub AboutMenuStrip_Click(sender As Object, e As EventArgs) Handles AboutMenuStrip.Click
        AboutThisCrap.ShowDialog() 'This loads the AboutThisCrap.vb form
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
        Try
            If MultithreadingEnabled = True Then
                If CThread2Active = False Then
                    CountingThread2 = New Threading.Thread(AddressOf countup2)
                    CountingThread2.Start() 'I'm still not sure how this works, but it does.
                    CThread2Active = True
                End If
            Else
                countup2() 'This is the not multithreaded version
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ThreadingTest_Click(sender As Object, e As EventArgs) Handles ThreadingTest.Click
        Try
            If MultithreadingEnabled = True Then
                If CThread1Active = False Then
                    CountingThread1 = New Threading.Thread(AddressOf countup)
                    CountingThread1.Start()
                    CThread1Active = True
                End If
            Else
                countup()
            End If
        Catch ex As Exception
        End Try
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
    Private Sub MainWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        KeyLabel.Text = Convert.ToChar(e.KeyCode)
    End Sub
    Friend Sub KeyboardInputSub(e As KeyEventArgs)
        KeyLabel.Text = Convert.ToChar(e.KeyCode)
    End Sub
    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        StartGame()
    End Sub
    Private Sub StartGame()
        StartButton.Enabled = False
        Try
            Hide()
            GameWindow.ShowDialog()
        Catch ex As Exception
        End Try
        StartButton.Enabled = True
    End Sub
End Class
