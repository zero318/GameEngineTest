Imports System.Threading
Public Class GameWindow
    Dim GamePath As String = My.Application.Info.DirectoryPath
    Dim GameAreaGraphics As Graphics
    Dim CustomDoubleBuffer As New BufferedGraphicsContext
    Dim TempRectangle As Rectangle
    Dim CustomGraphicsBuffer As BufferedGraphics
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
    Dim MegamanBlinkRate As Integer = 15
    Dim MegamanRectangle As New RectangleF(50, 400, 100, 100)
    'Dim MegamanRectangleGraphics As Graphics
    Dim MegamanRectangle2 As New RectangleF(50, 400, 100, 100)
    'Dim MegamanRectangle2Graphics As Graphics
    Dim CollisionTestRectangle As New RectangleF(768, 862, 200, 50)
    Dim MegamanRectangleImage As Image
    Dim MegamanLeft As Boolean
    Dim MegamanXVelocity As Double
    Dim MegamanYVelocity As Double
    Dim MegamanAnimation As Byte = 8
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
        CollisionTestRectangle.X = CollisionTestPanel1.Left
        CollisionTestRectangle.Y = CollisionTestPanel1.Top
        CollisionTestRectangle.Width = CollisionTestPanel1.Width
        CollisionTestRectangle.Height = CollisionTestPanel1.Height
        'MegamanPhysicsTimer.Change(TimerStartDelay, TimerInterval)
        MegamanAnimationTimer.Change(TimerStartDelay, TimerInterval)
        If MainWindow.DebugHUDEnabled = True Then
            ThreadCountTimer.Change(TimerStartDelay, TimerInterval)
        Else
            ThreadCountLabel.Visible = False
            ThreadCountTimer.Dispose()
        End If
        'Make some rectangles and crap here for collision.
        Megaman.Dispose()
        CollisionTestPanel1.Dispose()
        Using GameWindowGraphics As Graphics = CreateGraphics() 'This gets the screen resolution.
            ScreenDpiX = GameWindowGraphics.DpiX
            ScreenDpiY = GameWindowGraphics.DpiY
        End Using
        GameAreaGraphics = GameArea.CreateGraphics()
    End Sub
    Private Sub UpdateThreadCount()
        ThreadPool.GetAvailableThreads(Test1, Test2)
        ThreadCountLabel.Text = "Available Workers: " & Test1 & " Available IOs: " & Test2 & vbCrLf & "MegamanAnimation: " & MegamanAnimation & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiX: " & ScreenDpiX & " ScreenDpiY: " & ScreenDpiY & vbCrLf & "MegamanX: " & MegamanRectangle.X & " MegamanY: " & MegamanRectangle.Y & vbCrLf & "Megaman2X: " & MegamanRectangle2.X & " Megaman2Y: " & MegamanRectangle2.Y & vbCrLf & "MegamanLeft: " & MegamanLeft
    End Sub
    Private Sub GameWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode   'Detects the held keys...
            Case Keys.Right
                MegamanXVelocity = 9 '(20 * bytSpeed)
                If ((MegamanAnimation <> 4) And (MegamanAnimation <> 5)) AndAlso RightHeld = False Then   'If the player isn't jumping and the key isn't being held...
                    RightHeld = True
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 2    'Run right
                    MegamanLeft = False
                End If
                'MegamanLeft = False
            Case Keys.Left
                MegamanXVelocity = -9 '(-20 * bytSpeed)
                If ((MegamanAnimation <> 4) And (MegamanAnimation <> 5)) AndAlso LeftHeld = False Then   'If the player isn't jumping and the key isn't being held...
                    LeftHeld = True
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 3    'Run left
                    MegamanLeft = True
                End If
                'MegamanLeft = True
            Case Keys.Up
                If MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height) Then   'If the player is on the ground...
                    MegamanYVelocity = -39.2
                    If ((MegamanAnimation <> 4) And (MegamanAnimation <> 5)) AndAlso MegamanLeft = False Then  'If the player isn't jumping and is facing right...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 4    'Jump right
                    ElseIf ((MegamanAnimation <> 4) And (MegamanAnimation <> 5)) AndAlso MegamanLeft = True Then   'If the player isn't jumping and is facing left...
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
        If ((MegamanAnimation <> 4) And (MegamanAnimation <> 5)) Then    'If the player isn't jumping...
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
        CustomGraphicsBuffer = CustomDoubleBuffer.Allocate(GameAreaGraphics, New Rectangle(0, 0, GameArea.Width, GameArea.Height))
        CustomGraphicsBuffer.Graphics.FillRectangle(SystemBrushes.Control, New Rectangle(0, 0, GameArea.Width, GameArea.Height))
        If MainWindow.DebugBoundingBoxes = False Then
            CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle))
            CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle2))
            CustomGraphicsBuffer.Graphics.FillRectangle(Brushes.Aqua, Rectangle.Round(CollisionTestRectangle))
        Else
            CustomGraphicsBuffer.Graphics.FillRectangle(Brushes.Aqua, Rectangle.Round(CollisionTestRectangle))
            CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(MegamanRectangle))
            CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(MegamanRectangle2))
        End If
        If Not MegamanRectangleImage Is Nothing Then
            Try
                CustomGraphicsBuffer.Graphics.DrawImage(MegamanRectangleImage, ((MegamanRectangle.X + (MegamanRectangle.Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)) / 2), (MegamanRectangle.Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY) - MegamanRectangle.Height)))
            Catch ex As Exception

            End Try
        End If
        CustomGraphicsBuffer.Render(GameAreaGraphics)
        CustomDoubleBuffer.Invalidate()
        CustomGraphicsBuffer.Dispose()
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
        If MegamanAnimation <> 8 AndAlso MegamanAnimation <> 9 Then 'If vertical motion is not 0...
            MegamanRectangle.Y = MegamanRectangle.Y + MegamanYVelocity    'Calculates the new position
            MegamanYVelocity = MegamanYVelocity + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
            If MegamanRectangle.Y > (GameArea.Height - Megaman.Height) Then   'If the player is out of bounds under the bottom...
                MegamanRectangle.Y = (GameArea.Height - Megaman.Height)
                If MegamanAnimation = 4 Or MegamanAnimation = 5 Then
                    If (MegamanAnimation Mod 2) = 0 Then 'MegamanLeft = False Then   'If the player is facing right...
                        If RightHeld = False Then    'If the player is not holding right...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 6    'Land right
                        Else    'If the player is holding right...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 2    'Run right
                        End If
                    Else    'If the player is facing left...
                        If LeftHeld = False Then 'If the player is not holding left...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 7    'Land left
                        Else    'If the player is holding left...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 3    'Run left
                        End If
                    End If
                End If
            ElseIf MegamanRectangle.Y < 0 Then 'If the player is out of bounds above the top...
                MegamanRectangle.Y = 0
            End If
        End If
    End Sub
    Friend Sub AnimateMegamanRectangle()
        Select Case MegamanAnimation
            Case = 0, 1    'Standing
                Select Case MegamanAnimationFrame
                    Case Is <= (MegamanBlinkRate - 3)
                        MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing1.png")
                    Case (MegamanBlinkRate - 2), (MegamanBlinkRate)
                        MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing2.png")
                    Case Is = (MegamanBlinkRate - 1)
                        MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing3.png")
                End Select
                If (MegamanAnimation Mod 2) = 1 Then 'If left...
                    MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX) 'Mirror graphic
                End If
                If MegamanAnimationFrame < MegamanBlinkRate Then
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 2, 3    'Running
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Running" & MegamanAnimationFrame & ".png")
                If (MegamanAnimation Mod 2) = 1 Then 'If left...
                    MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX) 'Mirror graphic
                End If
                If MegamanAnimationFrame < 11 Then
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 4, 5    'Jumping
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Jumping" & MegamanAnimationFrame & ".png")
                If (MegamanAnimation Mod 2) = 1 Then 'If left...
                    MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX) 'Mirror graphics
                End If
                If MegamanAnimationFrame < 5 Then
                    MegamanAnimationFrame += 1
                End If
            Case = 6, 7    'Landing
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Landing" & MegamanAnimationFrame & ".png")
                If (MegamanAnimation Mod 2) = 1 Then 'If left...
                    MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX) 'Mirror graphic
                End If
                If MegamanAnimationFrame < 2 Then
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 0 + (MegamanAnimation Mod 2)    'Stand right
                End If
            Case = 8, 9    'Warping In
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\WarpIn" & MegamanAnimationFrame & ".png")
                If (MegamanAnimation Mod 2) = 1 Then 'If left...
                    MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX) 'Mirror graphic
                End If
                If MegamanAnimationFrame < 7 Then
                    MegamanAnimationFrame += 1
                Else
                    If MegamanRectangle.Y < (GameArea.Height - MegamanRectangle.Height) Then   'If spawning in midair...
                        MegamanAnimationFrame = 5
                        MegamanAnimation = 4 + (MegamanAnimation Mod 2)    'Display falling animation
                    Else    'If spawning on the ground...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 0 + (MegamanAnimation Mod 2)    'Display standing animation
                        MegamanPhysicsTimer.Change(TimerStartDelay, TimerInterval)
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
    End Sub
End Class