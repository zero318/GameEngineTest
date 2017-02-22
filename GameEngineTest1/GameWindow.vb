Imports System.Threading
Public Class GameWindow
    Dim GamePath As String = My.Application.Info.DirectoryPath
    Dim Test1 As Integer
    Dim Test2 As Integer
    Dim ScreenDpiX As Integer
    Dim ScreenDpiY As Integer
    Dim TimerInterval As Integer = 100
    Dim TimerStartDelay As Integer = 0
    Dim ThreadCountTimerCall As New TimerCallback(AddressOf UpdateThreadCount)
    Dim ThreadCountTimer As New Timer(ThreadCountTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim RightHeld As Boolean
    Dim LeftHeld As Boolean
    'UGH. I'm going to need to be able to run at least 10-15 of all these things at once for each sprite on screen. No wonder old systems were so limited on sprites.
    Dim MegamanRectangle As New RectangleF(50, 400, 100, 100)
    Dim MegamanRectangle2 As New RectangleF(50, 400, 100, 100)
    Dim MegamanRectangleImage As Image
    Dim MegamanLeft As Boolean
    Dim MegamanXVelocity As Double
    Dim MegamanYVelocity As Double
    Dim MegamanAnimation As Byte = 6
    Dim MegamanAnimationFrame As Byte = 1
    Dim MegamanAnimationTimerCall As New TimerCallback(AddressOf AnimateMegamanRectangle)
    Dim MegamanAnimationTimer As New Timer(MegamanAnimationTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim MegamanMegamanPhysicsTimerCall As New TimerCallback(AddressOf MegamanRectanglePhysics)
    Dim MegamanPhysicsTimer As New Timer(MegamanMegamanPhysicsTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MegamanRectangle.X = Megaman.Left
        MegamanRectangle.Y = Megaman.Top
        MegamanRectangle.Width = Megaman.Width
        MegamanRectangle.Height = Megaman.Height
        MegamanRectangle2.X = Megaman.Left
        MegamanRectangle2.Y = Megaman.Top
        MegamanRectangle2.Width = Megaman.Width
        MegamanRectangle2.Height = Megaman.Height
        MegamanPhysicsTimer.Change(TimerStartDelay, TimerInterval)
        MegamanAnimationTimer.Change(TimerStartDelay, TimerInterval)
        If MainWindow.DebugHUDEnabled = True Then
            ThreadCountTimer.Change(TimerStartDelay, TimerInterval)
        Else
            ThreadCountLabel.Visible = False
            ThreadCountTimer.Dispose()
        End If
        'Make some rectangles and crap here for collision.
        Megaman.Dispose()
        Using GameWindowGraphics As Graphics = CreateGraphics() 'This gets the screen resolution.
            ScreenDpiX = GameWindowGraphics.DpiX
            ScreenDpiY = GameWindowGraphics.DpiY
        End Using
    End Sub
    Private Sub UpdateThreadCount()
        'ThreadPool.GetAvailableThreads(Test1, Test2)
        'ThreadCountLabel.Text = Test1 & " " & Test2
        ThreadCountLabel.Text = "MegamanAnimation: " & MegamanAnimation & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiX: " & ScreenDpiX & " ScreenDpiY: " & ScreenDpiY & vbCrLf & "MegamanX: " & MegamanRectangle.X & " MegamanY: " & MegamanRectangle.Y & vbCrLf & "Megaman2X: " & MegamanRectangle2.X & " Megaman2Y: " & MegamanRectangle2.Y
    End Sub
    Private Sub GameWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode   'Detects the held keys...
            Case Keys.Right
                MegamanXVelocity = 9 '(20 * bytSpeed)
                If MegamanAnimation < 4 AndAlso RightHeld = False Then   'If the player isn't jumping and the key isn't being held...
                    RightHeld = True
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 2    'Run right
                End If
                MegamanLeft = False
            Case Keys.Left
                MegamanXVelocity = -9 '(-20 * bytSpeed)
                If MegamanAnimation < 4 AndAlso LeftHeld = False Then   'If the player isn't jumping and the key isn't being held...
                    LeftHeld = True
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 3    'Run left
                End If
                MegamanLeft = True
            Case Keys.Up
                If MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height) Then   'If the player is on the ground...
                    MegamanYVelocity = -39.2
                    If MegamanAnimation < 4 AndAlso MegamanLeft = False Then  'If the player isn't jumping and is facing right...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 4    'Jump right
                    ElseIf MegamanAnimation < 4 AndAlso MegamanLeft = True Then   'If the player isn't jumping and is facing left...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 5    'Jump left
                    End If
                End If
        End Select
    End Sub
    Private Sub GameWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode   'Detects the released keys...
            Case Keys.Right 'If right was released...
                MegamanXVelocity = 0
                RightHeld = False
            Case Keys.Left  'If left was released...
                MegamanXVelocity = 0
                LeftHeld = False
        End Select
        If MegamanAnimation < 4 Then    'If the player isn't jumping...
            If MegamanLeft = False Then   'and is facing right...
                MegamanAnimationFrame = 1
                MegamanAnimation = 0    'Stand right
            Else    'and is facing left...
                MegamanAnimationFrame = 1
                MegamanAnimation = 1    'Stand left
            End If
        End If
    End Sub
    Private Sub GameWindow_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        If MainWindow.DebugBoundingBoxes = False Then
            e.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle))
            e.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle2))
        Else
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(MegamanRectangle))
            e.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(MegamanRectangle2))
        End If
        'e.Graphics.FillRectangle(Brushes.Red, MegamanRectangle)
        If Not MegamanRectangleImage Is Nothing Then
            Try
                'e.Graphics.DrawImage(MegamanRectangleImage, MegamanRectangle.X, MegamanRectangle.Y, MegamanRectangle.Width, MegamanRectangle.Height)
                e.Graphics.DrawImage(MegamanRectangleImage, ((MegamanRectangle.X + (MegamanRectangle.Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)) / 2), (MegamanRectangle.Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY) - MegamanRectangle.Height)))
            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub MegamanRectanglePhysics()
        If MegamanXVelocity <> 0 Then  'If horizontal motion is not 0...
            If MegamanXVelocity > 0 AndAlso MegamanRectangle.X < GameArea.Width Then   'If the player is not out of bounds to the right...
                MegamanRectangle.X = MegamanRectangle.X + MegamanXVelocity
                If MegamanRectangle.X > GameArea.Width Then   'If the player was moved out of bounds to the right...
                    MegamanRectangle.X = GameArea.Width
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 0    'Stand right
                End If
            ElseIf MegamanXVelocity < 0 AndAlso MegamanRectangle.X > 0 Then  'If the player is not out of bounds to the left...
                MegamanRectangle.X = MegamanRectangle.X + MegamanXVelocity
                If MegamanRectangle.X < 0 Then    'If the player was moved out of bounds to the left...
                    MegamanRectangle.X = 0
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 1    'Stand left
                End If
            End If
        End If
        If MegamanAnimation <> 6 AndAlso MegamanAnimation <> 9 Then 'If vertical motion is not 0...
            MegamanRectangle.Y = MegamanRectangle.Y + MegamanYVelocity    'Calculates the new position
            MegamanYVelocity = MegamanYVelocity + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
            If MegamanRectangle.Y > (GameArea.Height - Megaman.Height) Then   'If the player is out of bounds under the bottom...
                MegamanRectangle.Y = (GameArea.Height - Megaman.Height)
                If MegamanLeft = False Then   'If the player is facing right...
                    If RightHeld = False Then    'If the player is not holding right...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 0    'Stand right
                    Else    'If the player is holding right...
                        MegamanAnimation = 2    'Run right
                    End If
                Else    'If the player is facing left...
                    If LeftHeld = False Then 'If the player is not holding left...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 1    'Stand left
                    Else    'If the player is holding left...
                        MegamanAnimation = 3    'Run left
                    End If
                End If
            ElseIf MegamanRectangle.Y < 0 Then 'If the player is out of bounds above the top...
                MegamanRectangle.Y = 0
            End If
        End If
    End Sub
    Friend Sub AnimateMegamanRectangle()
        Select Case MegamanAnimation
            Case = 0    'Standing Right
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 3 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 1    'Standing Left
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 3 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 2    'Running Right
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Running" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 11 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 3    'Running Left
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Running" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 11 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 4    'Jumping Right
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Jumping" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 5 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    'MegamanAnimationFrame = 1   'Removing this prevents the animation looping and causing the arms to flail around.
                End If
            Case = 5    'Jumping Left
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Jumping" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 5 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    'MegamanAnimationFrame = 1   'Removing this prevents the animation looping and causing the arms to flail around.
                End If
            Case = 6    'Warping In Right
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\WarpIn" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    If MegamanRectangle.Y < (GameArea.Height - MegamanRectangle.Height) Then   'If spawning in midair...
                        MegamanAnimationFrame = 5
                        MegamanAnimation = 4    'Jump right
                    Else    'If spawning on the ground...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 0    'Stand right
                    End If
                End If
            Case = 7    'Landing Right  'These were going to be used when landing after a jump, but didn't end up looking very good and caused weird glitches.
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Jumping" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 0    'Stand right
                End If
            Case = 8    'Landing Left   'These were going to be used when landing after a jump, but didn't end up looking very good and caused weird glitches.
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Jumping" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 1    'Stand left
                End If
            Case = 9    'Warping In Left
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\WarpIn" & MegamanAnimationFrame & ".png")   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    If MegamanRectangle.Y < (GameArea.Height - MegamanRectangle.Height) Then   'If spawning in midair...
                        MegamanAnimationFrame = 5
                        MegamanAnimation = 5    'Jump left
                    Else    'If spawning on the ground...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 1    'Run left
                    End If
                End If
        End Select
        Try
            MegamanRectangle2.X = ((MegamanRectangle.X + (MegamanRectangle.Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)) / 2)
            MegamanRectangle2.Y = (MegamanRectangle.Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY) - MegamanRectangle.Height))
            MegamanRectangle2.Width = ((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)
            MegamanRectangle2.Height = ((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY)
        Catch ex As Exception

        End Try
        Invalidate()
    End Sub
End Class