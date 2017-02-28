Imports System.Threading
Public Class GameWindow
    Dim FPS As Integer
    Dim GamePath As String = My.Application.Info.DirectoryPath
    Dim GameAreaGraphics As Graphics
    Dim GameAreaGraphics2 As Graphics
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
    Dim FPSTimerCall As New TimerCallback(AddressOf UpdateFPS)
    Dim FPSTimer As New Timer(FPSTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim ButtonHeld() As Boolean = {False, False}
    Dim RightHeld As Boolean
    Dim LeftHeld As Boolean
    Dim GameAreaRectangle As Rectangle
    Dim CollisionRegion As New Region()
    Dim CollisionRegion2 As New Region()
    'UGH. I'm going to need to be able to run at least 10-15 of all these things at once for each sprite on screen. No wonder old systems were so limited on sprites.
    Dim MegamanBlinkRate As Integer = 15
    Dim MegamanRectangle As New RectangleF(50, 400, 100, 100)
    'Dim MegamanRectangleGraphics As Graphics
    Dim MegamanRectangle2 As New RectangleF(50, 400, 100, 100)
    Dim MegamanCollisionRectangleTemp As New RectangleF(0, 0, 0, 0)
    Dim MegamanCollisionRectangleTemp2 As New RectangleF(0, 0, 0, 0)
    Dim MegamanCollisionRectangleTemp3 As New RectangleF(0, 0, 0, 0)
    'Dim MegamanRectangle2Graphics As Graphics
    Dim CollisionTestRectangle As New RectangleF(768, 862, 200, 50)
    Dim MegamanRectangleImage As Image
    Dim MegamanCollisionRightRectangle As RectangleF
    Dim MegamanCollisionLeftRectangle As RectangleF
    Dim MegamanCollisionTopRectangle As RectangleF
    Dim MegamanCollisionBottomRectangle As RectangleF
    Dim MegamanCollisionHorizontalRectangle As RectangleF
    Dim MegamanCollisionVerticalRectangle As RectangleF
    Dim MegamanXCollision As Integer
    Dim MegamanYCollision As Integer
    Dim MegamanCollisionTemp As Integer
    Dim MegamanLeft As Boolean
    'Dim MegamanOnGround As Boolean
    Dim MegamanXVelocity As Double
    Dim MegamanYVelocity As Double
    Dim MegamanAnimation As Byte = 8
    Dim MegamanAnimationFrame As Byte = 1
    Dim MegamanAnimationTimerCall As New TimerCallback(AddressOf AnimateMegamanRectangle)
    Dim MegamanAnimationTimer As New Timer(MegamanAnimationTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim MegamanMegamanPhysicsTimerCall As New TimerCallback(AddressOf MegamanRectanglePhysics)
    Dim MegamanPhysicsTimer As New Timer(MegamanMegamanPhysicsTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Megaman.Top = Megaman.Parent.Height
        MegamanRectangle.X = Megaman.Left
        MegamanRectangle.Y = Megaman.Top
        MegamanRectangle.Width = Megaman.Width
        MegamanRectangle.Height = Megaman.Height
        MegamanRectangle2.X = Megaman.Left
        MegamanRectangle2.Y = Megaman.Top
        MegamanRectangle2.Width = Megaman.Width
        MegamanRectangle2.Height = Megaman.Height
        GameAreaRectangle.X = GameArea.Left
        GameAreaRectangle.Y = GameArea.Top
        GameAreaRectangle.Width = GameArea.Width
        GameAreaRectangle.Height = GameArea.Height
        CollisionTestRectangle.X = CollisionTestPanel1.Left
        CollisionTestRectangle.Y = CollisionTestPanel1.Top
        CollisionTestRectangle.Width = CollisionTestPanel1.Width
        CollisionTestRectangle.Height = CollisionTestPanel1.Height
        'MegamanPhysicsTimer.Change(TimerStartDelay, TimerInterval)
        MegamanAnimationTimer.Change(TimerStartDelay, TimerInterval)
        If MainWindow.DebugHUDEnabled = True Then
            ThreadCountTimer.Change(TimerStartDelay, TimerInterval)
            FPSTimer.Change(TimerStartDelay, 1000)
        Else
            FPSLabel.Visible = False
            FPSTimer.Dispose()
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
        GameAreaGraphics2 = GameArea.CreateGraphics()
        CollisionRegion.Exclude(GameAreaRectangle)
        CollisionRegion.Union(CollisionTestRectangle)
        MegamanCollisionRightRectangle.X = MegamanRectangle.Right
        MegamanCollisionRightRectangle.Y = MegamanRectangle.Top
        MegamanCollisionRightRectangle.Width = 1
        MegamanCollisionRightRectangle.Height = MegamanRectangle.Height
        MegamanCollisionLeftRectangle.X = MegamanRectangle.Left
        MegamanCollisionLeftRectangle.Y = MegamanRectangle.Top
        MegamanCollisionLeftRectangle.Width = 1
        MegamanCollisionLeftRectangle.Height = MegamanRectangle.Height
        MegamanCollisionTopRectangle.X = MegamanRectangle.Left
        MegamanCollisionTopRectangle.Y = MegamanRectangle.Top
        MegamanCollisionTopRectangle.Width = MegamanRectangle.Width
        MegamanCollisionTopRectangle.Height = 1
        MegamanCollisionBottomRectangle.X = MegamanRectangle.Left
        MegamanCollisionBottomRectangle.Y = MegamanRectangle.Bottom
        MegamanCollisionBottomRectangle.Width = MegamanRectangle.Width
        MegamanCollisionBottomRectangle.Height = 1
        MegamanCollisionHorizontalRectangle.X = MegamanRectangle.Left
        MegamanCollisionHorizontalRectangle.Y = (MegamanRectangle.Top + MegamanRectangle.Bottom) / 2
        MegamanCollisionHorizontalRectangle.Width = MegamanRectangle.Width
        MegamanCollisionHorizontalRectangle.Height = 1
        MegamanCollisionVerticalRectangle.X = (MegamanRectangle.Left + MegamanRectangle.Right) / 2
        MegamanCollisionVerticalRectangle.Y = MegamanRectangle.Top
        MegamanCollisionVerticalRectangle.Width = 1
        MegamanCollisionVerticalRectangle.Height = MegamanRectangle.Height
    End Sub
    Friend Sub UpdateFPS()
        FPSLabel.Text = "FPS: " & FPS
        FPS = 0
    End Sub
    Private Sub UpdateThreadCount()
        ThreadPool.GetAvailableThreads(Test1, Test2)
        Try
            ThreadCountLabel.Text = "Available Workers: " & Test1 & " Available IOs: " & Test2 & vbCrLf & "MegamanAnimation: " & MegamanAnimation & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiX: " & ScreenDpiX & " ScreenDpiY: " & ScreenDpiY & vbCrLf & "MegamanX: " & MegamanRectangle.X & " MegamanY: " & MegamanRectangle.Y & vbCrLf & "Megaman2X: " & MegamanRectangle2.X & " Megaman2Y: " & MegamanRectangle2.Y & vbCrLf & "MegamanLeft: " & MegamanLeft & vbCrLf & "RectTemp1: " & MegamanCollisionRectangleTemp.Width & " RectTemp2: " & MegamanCollisionRectangleTemp2.Width & " RectTemp3: " & MegamanCollisionRectangleTemp3.Width & vbCrLf & "XCollision: " & MegamanXCollision & " YCollision: " & MegamanYCollision
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GameWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not MegamanAnimation >= 8 Then 'This statement fixes a bug with warping in
            Select Case e.KeyCode   'Detects the held keys...
                Case Keys.Right
                    MegamanXVelocity = 9 '(20 * bytSpeed)
                    If ((MegamanAnimation <> 4) AndAlso (MegamanAnimation <> 5)) AndAlso ButtonHeld(0) = False Then   'If the player isn't jumping and the key isn't being held...
                        ButtonHeld(0) = True
                        'RightHeld = True
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 2    'Run right
                        MegamanLeft = False
                    End If
                'MegamanLeft = False
                Case Keys.Left
                    MegamanXVelocity = -9 '(-20 * bytSpeed)
                    If ((MegamanAnimation <> 4) AndAlso (MegamanAnimation <> 5)) AndAlso ButtonHeld(1) = False Then   'If the player isn't jumping and the key isn't being held...
                        ButtonHeld(1) = True
                        'LeftHeld = True
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 3    'Run left
                        MegamanLeft = True
                    End If
                'MegamanLeft = True
                Case Keys.Up
                    If MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height) Then   'If the player is on the ground...
                        MegamanYVelocity = -39.2
                        If ((MegamanAnimation <> 4) AndAlso (MegamanAnimation <> 5)) AndAlso MegamanLeft = False Then  'If the player isn't jumping and is facing right...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 4    'Jump right
                        ElseIf ((MegamanAnimation <> 4) AndAlso (MegamanAnimation <> 5)) AndAlso MegamanLeft = True Then   'If the player isn't jumping and is facing left...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 5    'Jump left
                        End If
                    End If
            End Select
        End If
    End Sub
    Private Sub GameWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode   'Detects the released keys...
            Case Keys.Right 'If right was released...
                MegamanXVelocity = 0
                ButtonHeld(0) = False
                'RightHeld = False
            Case Keys.Left  'If left was released...
                MegamanXVelocity = 0
                ButtonHeld(1) = False
                'LeftHeld = False
        End Select
        If Not MegamanAnimation >= 4 Then    'If the player isn't jumping...
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
            Try
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle2))
                CustomGraphicsBuffer.Graphics.FillRegion(Brushes.Aqua, CollisionRegion)
                'CustomGraphicsBuffer.Graphics.FillRectangle(Brushes.Aqua, Rectangle.Round(CollisionTestRectangle))
            Catch ex As Exception

            End Try
        Else
            Try
                'CustomGraphicsBuffer.Graphics.FillRectangle(Brushes.Aqua, Rectangle.Round(CollisionTestRectangle))
                CustomGraphicsBuffer.Graphics.FillRegion(Brushes.Aqua, CollisionRegion)
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(MegamanRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(MegamanRectangle2))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionLeftRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionRightRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionTopRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionBottomRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionHorizontalRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionVerticalRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTemp))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTemp2))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTemp3))
            Catch ex As Exception

            End Try
        End If
        If Not MegamanRectangleImage Is Nothing Then
            Try
                CustomGraphicsBuffer.Graphics.DrawImageUnscaled(MegamanRectangleImage, ((MegamanRectangle.X + (MegamanRectangle.Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)) / 2), (MegamanRectangle.Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY) - MegamanRectangle.Height)))
            Catch ex As Exception

            End Try
        End If
        CustomGraphicsBuffer.Render(GameAreaGraphics)
        'CustomDoubleBuffer.Invalidate()
        CustomGraphicsBuffer.Dispose()
        FPS += 1
    End Sub
    Private Sub MegamanRectanglePhysics()
        MegamanXCollision = 0
        MegamanYCollision = 0
        'MegamanOnGround = False
        If MegamanXVelocity <> 0 Then
            'MegamanRectangle.X = MegamanRectangle.X + MegamanXVelocity
            MegamanRectangle.X = MegamanRectangle.X + (MegamanXVelocity * 2)
        End If
        MegamanCollisionRightRectangle.X = MegamanRectangle.Right
        MegamanCollisionRightRectangle.Y = MegamanRectangle.Top
        MegamanCollisionLeftRectangle.X = MegamanRectangle.Left
        MegamanCollisionLeftRectangle.Y = MegamanRectangle.Top
        MegamanCollisionTopRectangle.X = MegamanRectangle.Left
        MegamanCollisionTopRectangle.Y = MegamanRectangle.Top
        MegamanCollisionBottomRectangle.X = MegamanRectangle.Left
        MegamanCollisionBottomRectangle.Y = MegamanRectangle.Bottom
        MegamanCollisionHorizontalRectangle.X = MegamanRectangle.Left
        MegamanCollisionHorizontalRectangle.Y = (MegamanRectangle.Top + MegamanRectangle.Bottom) / 2
        MegamanCollisionVerticalRectangle.X = (MegamanRectangle.Left + MegamanRectangle.Right) / 2
        MegamanCollisionVerticalRectangle.Y = MegamanRectangle.Top
        If CollisionRegion.IsVisible(MegamanRectangle.Left, MegamanRectangle.Bottom) = True Then 'Test lower left
            MegamanXCollision -= 1
            MegamanYCollision -= 1
        End If
        If CollisionRegion.IsVisible(MegamanRectangle.Right, MegamanRectangle.Bottom) = True Then 'Test lower right
            MegamanXCollision += 1
            MegamanYCollision -= 1
        End If
        If CollisionRegion.IsVisible(MegamanRectangle.Left, MegamanRectangle.Top) = True Then 'Test upper left
            MegamanXCollision -= 1
            MegamanYCollision += 1
        End If
        If CollisionRegion.IsVisible(MegamanRectangle.Right, MegamanRectangle.Top) = True Then 'Test upper right
            MegamanXCollision += 1
            MegamanYCollision += 1
        End If
        If CollisionRegion.IsVisible(MegamanRectangle.Left, (MegamanRectangle.Bottom - (MegamanRectangle.Height / 2))) = True Then 'Test middle left
            MegamanXCollision -= 1
        End If
        If CollisionRegion.IsVisible(MegamanRectangle.Right, (MegamanRectangle.Bottom - (MegamanRectangle.Height / 2))) = True Then 'Test middle right
            MegamanXCollision += 1
        End If
        If CollisionRegion.IsVisible((MegamanRectangle.Right - (MegamanRectangle.Width / 2)), MegamanRectangle.Bottom) = True Then 'Test middle bottom
            MegamanYCollision -= 1
            'MegamanOnGround = True
        End If
        If CollisionRegion.IsVisible((MegamanRectangle.Right - (MegamanRectangle.Width / 2)), MegamanRectangle.Top) = True Then 'Test middle top
            MegamanYCollision += 1
        End If
        If MegamanAnimation <> 8 AndAlso MegamanAnimation <> 9 Then 'If vertical motion is not 0...
            MegamanRectangle.Y = MegamanRectangle.Y + MegamanYVelocity    'Calculates the new position
            MegamanYVelocity = MegamanYVelocity + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
            If MegamanRectangle.Y > (GameArea.Height - MegamanRectangle.Height) Then   'If the player is out of bounds under the bottom...
                MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height)
                If MegamanAnimation = 4 OrElse MegamanAnimation = 5 Then 'If the player is in midair...
                    If (MegamanAnimation Mod 2) = 0 Then 'MegamanLeft = False Then   'If the player is facing right...
                        If ButtonHeld(0) = False Then    'If the player is not holding right...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 6    'Land right
                        Else    'If the player is holding right...
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 2    'Run right
                        End If
                    Else    'If the player is facing left...
                        If ButtonHeld(1) = False Then 'If the player is not holding left...
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
        'MegamanRectangle.X = MegamanRectangle.X + (MegamanXVelocity * 2)
        'Select Case MegamanXCollision
        '    Case = 0
        '        'If MegamanXVelocity <> 0 Then
        '        '    'MegamanRectangle.X = MegamanRectangle.X + MegamanXVelocity
        '        '    MegamanRectangle.X = MegamanRectangle.X + (MegamanXVelocity * 2)
        '        'End If
        '    Case > 0
        'If MegamanOnGround = True Then
        '    CollisionRegion2 = CollisionRegion.Clone()
        '    CollisionRegion2.Intersect(MegamanCollisionTopRectangle)
        '    MegamanCollisionRectangleTemp = CollisionRegion2.GetBounds(GameAreaGraphics)
        '    CollisionRegion2 = CollisionRegion.Clone()
        '    'CollisionRegion2.Intersect(MegamanCollisionHorizontalRectangle)
        '    MegamanCollisionRectangleTemp2 = CollisionRegion2.GetBounds(GameAreaGraphics)
        '    'CollisionRegion2 = CollisionRegion.Clone()
        '    'CollisionRegion2.Intersect(MegamanCollisionBottomRectangle)
        '    'MegamanCollisionRectangleTemp3 = CollisionRegion2.GetBounds(GameAreaGraphics)
        '    Select Case MegamanCollisionRectangleTemp.Width
        '        Case = MegamanCollisionRectangleTemp2.Width
        '            MegamanRectangle.X = MegamanRectangle.X - MegamanCollisionRectangleTemp.Width
        '        Case > MegamanCollisionRectangleTemp2.Width
        '            MegamanRectangle.X = MegamanRectangle.X - MegamanCollisionRectangleTemp.Width
        '        Case < MegamanCollisionRectangleTemp2.Width
        '            MegamanRectangle.X = MegamanRectangle.X - MegamanCollisionRectangleTemp2.Width
        '    End Select
        'Else
        CollisionRegion2 = CollisionRegion.Clone()
        CollisionRegion2.Intersect(MegamanCollisionTopRectangle)
        MegamanCollisionRectangleTemp = CollisionRegion2.GetBounds(GameAreaGraphics2)
        CollisionRegion2 = CollisionRegion.Clone()
        CollisionRegion2.Intersect(MegamanCollisionHorizontalRectangle)
        MegamanCollisionRectangleTemp2 = CollisionRegion2.GetBounds(GameAreaGraphics2)
        CollisionRegion2 = CollisionRegion.Clone()
        CollisionRegion2.Intersect(MegamanCollisionBottomRectangle)
        MegamanCollisionRectangleTemp3 = CollisionRegion2.GetBounds(GameAreaGraphics2)
        If MegamanCollisionRectangleTemp.Width = 100 Then
            MegamanCollisionRectangleTemp.Width = 0
        End If
        If MegamanCollisionRectangleTemp2.Width = 100 Then
            MegamanCollisionRectangleTemp2.Width = 0
        End If
        If MegamanCollisionRectangleTemp3.Width = 100 Then
            MegamanCollisionRectangleTemp3.Width = 0
        End If
        MegamanCollisionTemp = Math.Max(MegamanCollisionRectangleTemp.Width, MegamanCollisionRectangleTemp2.Width)
        MegamanCollisionTemp = Math.Max(MegamanCollisionTemp, MegamanCollisionRectangleTemp3.Width)
        'Select Case MegamanCollisionRectangleTemp.Width
        '       Case = MegamanCollisionRectangleTemp2.Width
        MegamanRectangle.X = MegamanRectangle.X - (MegamanCollisionRectangleTemp.Width * MegamanXCollision)
        '       Case > MegamanCollisionRectangleTemp2.Width
        '            MegamanRectangle.X = MegamanRectangle.X - MegamanCollisionRectangleTemp.Width
        '        Case < MegamanCollisionRectangleTemp2.Width
        '            MegamanRectangle.X = MegamanRectangle.X - MegamanCollisionRectangleTemp2.Width
        '    End Select
        'End If
        '    Case < 0
        '        CollisionRegion2 = CollisionRegion.Clone()
        '        CollisionRegion2.Intersect(MegamanCollisionTopRectangle)
        '        MegamanCollisionRectangleTemp = CollisionRegion2.GetBounds(GameAreaGraphics2)
        '        CollisionRegion2 = CollisionRegion.Clone()
        '        CollisionRegion2.Intersect(MegamanCollisionBottomRectangle)
        '        MegamanCollisionRectangleTemp2 = CollisionRegion2.GetBounds(GameAreaGraphics2)
        '        If MegamanCollisionRectangleTemp.Width = 100 Then
        '            MegamanCollisionRectangleTemp.Width = 0
        '        End If
        '        If MegamanCollisionRectangleTemp2.Width = 100 Then
        '            MegamanCollisionRectangleTemp2.Width = 0
        '        End If
        '        Select Case MegamanCollisionRectangleTemp.Width
        '            Case = MegamanCollisionRectangleTemp2.Width
        '                MegamanRectangle.X = MegamanRectangle.X + MegamanCollisionRectangleTemp.Width
        '            Case > MegamanCollisionRectangleTemp2.Width
        '                MegamanRectangle.X = MegamanRectangle.X + MegamanCollisionRectangleTemp.Width
        '            Case < MegamanCollisionRectangleTemp2.Width
        '                MegamanRectangle.X = MegamanRectangle.X + MegamanCollisionRectangleTemp2.Width
        '        End Select
        'End Select
        'Select Case MegamanYCollision
        '    Case = 0
        '        If MegamanAnimation <> 8 AndAlso MegamanAnimation <> 9 Then
        '            MegamanRectangle.Y = MegamanRectangle.Y + MegamanYVelocity
        '            MegamanYVelocity = MegamanYVelocity + 9.8
        '        End If
        '    Case > 0

        '    Case < 0

        'End Select
        'If MegamanXVelocity <> 0 Then  'If horizontal motion is not 0...
        '    If MegamanXVelocity > 0 AndAlso MegamanRectangle.X < GameArea.Width Then   'If the player is not out of bounds to the right...
        '        'MegamanRectangle.X = MegamanRectangle.X + MegamanXVelocity
        '        If MegamanRectangle.X > GameArea.Width Then   'If the player was moved out of bounds to the right...
        '            MegamanRectangle.X = GameArea.Width
        '            MegamanAnimationFrame = 1
        '            MegamanAnimation = 0    'Stand right
        '        End If
        '    ElseIf MegamanXVelocity < 0 AndAlso MegamanRectangle.X > 0 Then  'If the player is not out of bounds to the left...
        '        'MegamanRectangle.X = MegamanRectangle.X + MegamanXVelocity
        '        If MegamanRectangle.X < 0 Then    'If the player was moved out of bounds to the left...
        '            MegamanRectangle.X = 0
        '            MegamanAnimationFrame = 1
        '            MegamanAnimation = 1    'Stand left
        '        End If
        '    End If
        'End If
        'If MegamanAnimation <> 8 AndAlso MegamanAnimation <> 9 Then 'If vertical motion is not 0...
        '    MegamanRectangle.Y = MegamanRectangle.Y + MegamanYVelocity    'Calculates the new position
        '    MegamanYVelocity = MegamanYVelocity + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
        '    If MegamanRectangle.Y > (GameArea.Height - Megaman.Height) Then   'If the player is out of bounds under the bottom...
        '        MegamanRectangle.Y = (GameArea.Height - Megaman.Height)
        '        If MegamanAnimation = 4 OrElse MegamanAnimation = 5 Then 'If the player is in midair...
        '            If (MegamanAnimation Mod 2) = 0 Then 'MegamanLeft = False Then   'If the player is facing right...
        '                If ButtonHeld(0) = False Then    'If the player is not holding right...
        '                    MegamanAnimationFrame = 1
        '                    MegamanAnimation = 6    'Land right
        '                Else    'If the player is holding right...
        '                    MegamanAnimationFrame = 1
        '                    MegamanAnimation = 2    'Run right
        '                End If
        '            Else    'If the player is facing left...
        '                If ButtonHeld(1) = False Then 'If the player is not holding left...
        '                    MegamanAnimationFrame = 1
        '                    MegamanAnimation = 7    'Land left
        '                Else    'If the player is holding left...
        '                    MegamanAnimationFrame = 1
        '                    MegamanAnimation = 3    'Run left
        '                End If
        '            End If
        '        End If
        '    ElseIf MegamanRectangle.Y < 0 Then 'If the player is out of bounds above the top...
        '        MegamanRectangle.Y = 0
        '    End If
        'End If
        MegamanCollisionRightRectangle.X = MegamanRectangle.Right
        MegamanCollisionRightRectangle.Y = MegamanRectangle.Top
        MegamanCollisionLeftRectangle.X = MegamanRectangle.Left
        MegamanCollisionLeftRectangle.Y = MegamanRectangle.Top
        MegamanCollisionTopRectangle.X = MegamanRectangle.Left
        MegamanCollisionTopRectangle.Y = MegamanRectangle.Top
        MegamanCollisionBottomRectangle.X = MegamanRectangle.Left
        MegamanCollisionBottomRectangle.Y = MegamanRectangle.Bottom
        MegamanCollisionHorizontalRectangle.X = MegamanRectangle.Left
        MegamanCollisionHorizontalRectangle.Y = (MegamanRectangle.Top + MegamanRectangle.Bottom) / 2
        MegamanCollisionVerticalRectangle.X = (MegamanRectangle.Left + MegamanRectangle.Right) / 2
        MegamanCollisionVerticalRectangle.Y = MegamanRectangle.Top
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
                    MegamanAnimationFrame = 2 'The first frame is only for starting running
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
                Select Case MegamanAnimationFrame
                    Case Is = 1
                        If ButtonHeld((MegamanAnimation Mod 2)) = True Then
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 1 + (MegamanAnimation Mod 2)    'Stand right
                        Else
                            MegamanAnimationFrame += 1
                        End If
                    Case Is = 2
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 0 + (MegamanAnimation Mod 2)    'Stand right
                End Select
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
        'CustomDoubleBuffer.Invalidate()
    End Sub
    Private Sub GameOverYeah()

    End Sub
End Class