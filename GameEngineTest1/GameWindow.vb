Imports System.Threading
Imports System.Math
Public Class GameWindow
    Dim FPS As Integer
    Dim GamePath As String = My.Application.Info.DirectoryPath
    Dim GameAreaGraphics As Graphics
    Dim GameAreaGraphics2 As Graphics
    Dim PaintThisCrap As PaintEventArgs
    Dim CustomDoubleBuffer As New BufferedGraphicsContext
    Dim TempRectangle As Rectangle
    Dim CustomGraphicsBuffer As BufferedGraphics
    Dim PaintBackgroundOn As Boolean = False
    Dim PaintForegroundOn As Boolean = True
    Dim Test1 As Integer
    Dim Test2 As Integer
    Dim ScreenDpiX As Integer
    Dim ScreenDpiY As Integer
    Dim DebugLabelVisible As Boolean = False
    Dim FPSLabelVisible As Boolean = True
    Dim TimerInterval As Integer = 100
    Dim TimerStartDelay As Integer = 0
    Dim ThreadCountTimerCall As New TimerCallback(AddressOf UpdateThreadCount)
    Dim ThreadCountTimer As New Timer(ThreadCountTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim LoadPanelsTimerCall As New TimerCallback(AddressOf LoadPanels)
    Dim LoadPanelsTimer As New Timer(LoadPanelsTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim FPSTimerCall As New TimerCallback(AddressOf UpdateFPS)
    Dim FPSTimer As New Timer(FPSTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim ButtonHeld() As Boolean = {False, False}
    Dim RightHeld As Boolean
    Dim LeftHeld As Boolean
    Dim GameAreaRectangle As Rectangle
    Dim CollisionRegion As New Region()
    Dim CollisionRegion2 As New Region()
    Dim CollisionRegion3 As New Region()
    Dim SlopeTestPoints(3) As PointF
    Dim SlopeTest As New Drawing2D.GraphicsPath()
    'Megaman specific variables start here
    Dim MegamanBlinkRate As Integer = 15
    Dim MegamanRectangle As New RectangleF(50, 400, 100, 100)
    Dim MegamanRectangle2 As New RectangleF(50, 400, 100, 100)
    Dim MegamanCollisionRectangleTempX As New RectangleF(0, 0, 0, 0)
    Dim MegamanCollisionRectangleTempX2 As New RectangleF(0, 0, 0, 0)
    Dim MegamanCollisionRectangleTempX3 As New RectangleF(0, 0, 0, 0)
    Dim MegamanCollisionRectangleTempY As New RectangleF(0, 0, 0, 0)
    Dim MegamanCollisionRectangleTempY2 As New RectangleF(0, 0, 0, 0)
    Dim MegamanCollisionRectangleTempY3 As New RectangleF(0, 0, 0, 0)
    Dim MegamanOnFrame As Boolean
    Dim CollisionTestRectangle As New RectangleF(768, 862, 200, 50)
    Dim MegamanRectangleImage As Image
    Dim MegamanCollisionRightRectangle As RectangleF
    Dim MegamanCollisionLeftRectangle As RectangleF
    Dim MegamanCollisionTopRectangle As RectangleF
    Dim MegamanCollisionBottomRectangle As RectangleF
    Dim MegamanCollisionHorizontalRectangle As RectangleF
    Dim MegamanCollisionVerticalRectangle As RectangleF
    Dim MegamanCollisionArray(8) As Boolean 'First row: 0, 1, 2 ... Second row: 3, 4, 5 ... Third row: 6, 7, 8
    Dim MegamanXCollision As Integer
    Dim MegamanYCollision As Integer
    Dim MegamanCollisionTempY As Integer
    Dim MegamanCollisionTempX As Integer
    Dim MegamanLeft As Boolean
    Dim MegamanOnGround As Boolean
    Dim MegamanVelocityMultiplier As Integer
    Dim MegamanXVelocity As Double
    Dim MegamanYVelocity As Double
    Dim MegamanAnimation As Byte = 4
    Dim MegamanAnimation2 As Byte
    Dim MegamanAnimationFrame As Byte = 1
    Dim MegamanVelocityVectorMagnitude As Double
    Dim MegamanVelocityVectorAngle As Double
    Dim Megaman30Angle As SByte
    Dim Megaman45Angle As SByte
    Dim MegamanHealth As Integer = 100
    Dim MegamanDead As Boolean
    Dim MegamanAnimationTimerCall As New TimerCallback(AddressOf AnimateMegamanRectangle)
    Dim MegamanAnimationTimer As New Timer(MegamanAnimationTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim MegamanMegamanPhysicsTimerCall As New TimerCallback(AddressOf MegamanRectanglePhysics)
    Dim MegamanPhysicsTimer As New Timer(MegamanMegamanPhysicsTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        If PaintBackgroundOn = True Then
            MyBase.OnPaintBackground(e)
        End If
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If PaintForegroundOn = True Then
            MyBase.OnPaint(e)
        End If
    End Sub
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Megaman.Top = Megaman.Parent.Height - Megaman.Height
        CollisionTestPanel1.Top = CollisionTestPanel1.Parent.Height - CollisionTestPanel1.Height
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
        GameAreaGraphics = GameArea.CreateGraphics()
        GameAreaGraphics2 = GameArea.CreateGraphics()
        CollisionRegion.Exclude(GameAreaRectangle)
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
        Megaman.Dispose()
        Using GameWindowGraphics As Graphics = CreateGraphics() 'This gets the screen resolution.
            ScreenDpiX = GameWindowGraphics.DpiX
            ScreenDpiY = GameWindowGraphics.DpiY
        End Using
        If ScreenDpiX >= 100 Then
            MegamanVelocityMultiplier = 2
        Else
            MegamanVelocityMultiplier = 1
        End If
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
        LoadPanelsTimer.Change(TimerStartDelay * 2, Timeout.Infinite)
    End Sub
    Friend Sub LoadPanels()
        CollisionTestRectangle.X = CollisionTestPanel1.Left
        CollisionTestRectangle.Y = CollisionTestPanel1.Top
        CollisionTestRectangle.Width = CollisionTestPanel1.Width
        CollisionTestRectangle.Height = CollisionTestPanel1.Height
        CollisionRegion.Union(CollisionTestRectangle)
        CollisionTestPanel1.Dispose()
        CollisionTestRectangle.X = CollisionTestPanel2.Left
        CollisionTestRectangle.Y = CollisionTestPanel2.Top
        CollisionTestRectangle.Width = CollisionTestPanel2.Width
        CollisionTestRectangle.Height = CollisionTestPanel2.Height
        CollisionRegion.Union(CollisionTestRectangle)
        CollisionTestPanel2.Dispose()
        CollisionTestRectangle.X = CollisionTestPanel3.Left
        CollisionTestRectangle.Y = CollisionTestPanel3.Top
        CollisionTestRectangle.Width = CollisionTestPanel3.Width
        CollisionTestRectangle.Height = CollisionTestPanel3.Height
        CollisionRegion.Union(CollisionTestRectangle)
        CollisionTestPanel3.Dispose()
        SlopeTestPoints(0) = New PointF(SlopePanelTest1.Left, SlopePanelTest1.Bottom)
        SlopeTestPoints(1) = New PointF(SlopePanelTest1.Right, SlopePanelTest1.Bottom)
        SlopeTestPoints(2) = New PointF(SlopePanelTest1.Right, SlopePanelTest1.Top)
        SlopeTestPoints(3) = New PointF(SlopePanelTest1.Left, SlopePanelTest1.Bottom)
        SlopeTest.AddPolygon(SlopeTestPoints)
        CollisionRegion.Union(SlopeTest)
        SlopeTest.Dispose()
        SlopePanelTest1.Dispose()
        CollisionRegion2 = CollisionRegion.Clone()
        LoadPanelsTimer.Dispose()
    End Sub
    Friend Sub UpdateFPS()
        Try
            If FPSLabelVisible = True Then
                FPSLabel.Text = "FPS: " & FPS
            Else
                FPSLabel.Text = "Click to Turn On FPS Label"
            End If
        Catch ex As Exception
        End Try
        FPS = 0
    End Sub
    Private Sub UpdateThreadCount()
        ThreadPool.GetAvailableThreads(Test1, Test2)
        Try
            If DebugLabelVisible = True Then
                'ThreadCountLabel.Text = "Available Workers: " & Test1 & " Available IOs: " & Test2 & vbCrLf & "MegamanAnimation2: " & MegamanAnimation2 & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiX: " & ScreenDpiX & " ScreenDpiY: " & ScreenDpiY & " ScreenX: " & GameArea.Width & " ScreenY: " & GameArea.Height & vbCrLf & "MegamanX: " & MegamanRectangle.X & " MegamanY: " & MegamanRectangle.Y & vbCrLf & "MegamanWidth: " & MegamanRectangle.Width & " MegamanHeight: " & MegamanRectangle.Height & vbCrLf & "Megaman2X: " & MegamanRectangle2.X & " Megaman2Y: " & MegamanRectangle2.Y & vbCrLf & "MegamanLeft: " & MegamanLeft & vbCrLf & "RectTempX1: " & MegamanCollisionRectangleTempX.Height & " RectTempX2: " & MegamanCollisionRectangleTempX2.Height & " RectTempX3: " & MegamanCollisionRectangleTempX3.Height & vbCrLf & "RectTempY1: " & MegamanCollisionRectangleTempY.Height & " RectTempY2: " & MegamanCollisionRectangleTempY2.Height & " RectTempY3: " & MegamanCollisionRectangleTempY3.Height & vbCrLf & "XCollision: " & MegamanXCollision & " YCollision: " & MegamanYCollision & vbCrLf & "CollisionTempX: " & MegamanCollisionTempX & " CollisionTempY: " & MegamanCollisionTempY & vbCrLf & "XVelocity: " & MegamanXVelocity & " YVelocity: " & MegamanYVelocity & vbCrLf & "OnGround: " & MegamanOnGround & vbCrLf & "OnFrame: " & MegamanOnFrame & vbCrLf & "Velocity Vector Magnitude: " & MegamanVelocityVectorMagnitude & vbCrLf & "Velocity Vector Angle: " & MegamanVelocityVectorAngle & vbCrLf & "30Angle: " & Megaman30Angle & vbCrLf & "45Angle: " & Megaman45Angle
                ThreadCountLabel.Text = "MegamanAnimation2: " & MegamanAnimation2 & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiX: " & ScreenDpiX & " ScreenDpiY: " & ScreenDpiY & " ScreenX: " & GameArea.Width & " ScreenY: " & GameArea.Height & vbCrLf & "MegamanX: " & MegamanRectangle.X & " MegamanY: " & MegamanRectangle.Y & vbCrLf & "CollisionArray(0,1,2): " & MegamanCollisionArray(0).CompareTo(True) + 1 & MegamanCollisionArray(1).CompareTo(True) + 1 & MegamanCollisionArray(2).CompareTo(True) + 1 & vbCrLf & "CollisionArray(3,4,5): " & MegamanCollisionArray(3).CompareTo(True) + 1 & MegamanCollisionArray(4).CompareTo(True) + 1 & MegamanCollisionArray(5).CompareTo(True) + 1 & vbCrLf & "CollisionArray(6,7,8): " & MegamanCollisionArray(6).CompareTo(True) + 1 & MegamanCollisionArray(7).CompareTo(True) + 1 & MegamanCollisionArray(8).CompareTo(True) + 1 & vbCrLf & "XCollision: " & MegamanXCollision & " YCollision: " & MegamanYCollision & vbCrLf & "CollisionTempX: " & MegamanCollisionTempX & " CollisionTempY: " & MegamanCollisionTempY & vbCrLf & "XVelocity: " & MegamanXVelocity & " YVelocity: " & MegamanYVelocity & vbCrLf & "OnGround: " & MegamanOnGround & vbCrLf & "OnFrame: " & MegamanOnFrame & vbCrLf & "Velocity Vector Angle: " & MegamanVelocityVectorAngle & vbCrLf & "30Angle: " & Megaman30Angle
            Else
                ThreadCountLabel.Text = "Click to Turn On Debug Label"
            End If
            ThreadCountLabel.Update()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GameWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not MegamanAnimation >= 4 Then
            Select Case e.KeyCode
                Case Keys.Right
                    If ButtonHeld(0) = False Then
                        ButtonHeld(0) = True
                        If MegamanAnimation < 2 Then
                            MegamanAnimationFrame = 1
                            If ButtonHeld(1) = False Then
                                MegamanAnimation = 1    'Run
                            Else
                                MegamanAnimation = 0    'Stand
                            End If
                        End If
                    End If
                Case Keys.Left
                    If ButtonHeld(1) = False Then
                        ButtonHeld(1) = True
                        If MegamanAnimation < 2 Then
                            MegamanAnimationFrame = 1
                            If ButtonHeld(0) = False Then
                                MegamanAnimation = 1    'Run
                            Else
                                MegamanAnimation = 0    'Stand
                            End If
                        End If
                    End If
                Case Keys.Up
                    If MegamanOnGround = True Then
                        MegamanYVelocity = -39.2
                        MegamanOnGround = False
                        If MegamanAnimation <> 2 Then
                            MegamanAnimationFrame = 1
                            MegamanAnimation = 2    'Jump
                        End If
                    End If
            End Select
        End If
    End Sub
    Private Sub GameWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode   'Detects the released keys...
            Case Keys.Right 'If right was released...
                ButtonHeld(0) = False
            Case Keys.Left  'If left was released...
                ButtonHeld(1) = False
        End Select
        If MegamanAnimation < 2 Then    'If the player isn't jumping...
            MegamanAnimationFrame = 1
            MegamanAnimation = 0
        End If
    End Sub
    Private Sub GameWindow_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        CustomGraphicsBuffer = CustomDoubleBuffer.Allocate(GameAreaGraphics, New Rectangle(0, 0, GameArea.Width, GameArea.Height))
        CustomGraphicsBuffer.Graphics.FillRectangle(SystemBrushes.Control, New Rectangle(0, 0, GameArea.Width, GameArea.Height))
        Try
            CustomGraphicsBuffer.Graphics.FillRegion(Brushes.Aqua, CollisionRegion)
        Catch ex As Exception
        End Try
        If Not MegamanRectangleImage Is Nothing Then
            Try
                CustomGraphicsBuffer.Graphics.DrawImageUnscaled(MegamanRectangleImage, ((MegamanRectangle.X + (MegamanRectangle.Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)) / 2), (MegamanRectangle.Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY) - MegamanRectangle.Height)))
            Catch ex As Exception
            End Try
        End If
        If MainWindow.DebugBoundingBoxes = False Then
            Try

            Catch ex As Exception
            End Try
        Else
            Try
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(MegamanRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(MegamanRectangle2))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionLeftRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionRightRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionTopRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionBottomRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionHorizontalRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Round(MegamanCollisionVerticalRectangle))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTempX))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTempX2))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTempX3))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTempY))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTempY2))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Round(MegamanCollisionRectangleTempY3))
            Catch ex As Exception
            End Try
        End If
        CustomGraphicsBuffer.Render(GameAreaGraphics)
        CustomGraphicsBuffer.Dispose()
        Try
            'Invalidate()
            FPS += 1
        Catch ex As Exception
            FPS = 0
        End Try
    End Sub
    Private Sub MegamanRectanglePhysics()
        '******************************************
        'This section resets a few collision related variables to properly detect collision on the next frame.
        '******************************************
        MegamanOnFrame = False
        '******************************************
        'This section processes a few special collision scenarios dealing with edges of the window, gravity, and jumping animations.
        '******************************************
        If MegamanRectangle.Y >= (GameArea.Height - MegamanRectangle.Height) Then   'If the player is on the bottom of the window...
            MegamanOnFrame = True
            MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height)
        End If
        If MegamanAnimation <> 4 Then 'If not warping in...
            If MegamanOnGround = False Then 'If the player is in midair...
                MegamanRectangle.Y = MegamanRectangle.Y + MegamanYVelocity    'Calculates the new position
                MegamanYVelocity = MegamanYVelocity + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
            ElseIf MegamanYCollision > 0 Then
                MegamanYVelocity = 0
            Else
                MegamanYVelocity = 0
                If MegamanAnimation = 2 Then 'If the player is jumping...
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 3    'Land
                End If
            End If
            If MegamanRectangle.Y < 0 Then 'If the player is out of bounds above the top...
                MegamanRectangle.Y = 0
            End If
        End If
        '******************************************
        'This section sets the horizontal velocity, calculates the new position, and sets running animations based on input.
        '******************************************
        MegamanXVelocity = 0
        If ButtonHeld(0) = True AndAlso ButtonHeld(1) = False Then
            MegamanXVelocity = 9
            If MegamanAnimation <> 2 Then
                MegamanLeft = False
            End If
        End If
        If ButtonHeld(1) = True AndAlso ButtonHeld(0) = False Then
            MegamanXVelocity = -9
            If MegamanAnimation <> 2 Then
                MegamanLeft = True
            End If
        End If
        If ButtonHeld(0) = ButtonHeld(1) Then
            MegamanXVelocity = 0
        End If
        If MegamanAnimation = 0 AndAlso MegamanXVelocity <> 0 Then
            MegamanAnimation = 1
        End If
        MegamanRectangle.X = MegamanRectangle.X + (MegamanXVelocity * MegamanVelocityMultiplier)
        '******************************************
        'This section attempts to calculate a velocity vector.
        '******************************************
        MegamanVelocityVectorMagnitude = Sqrt((MegamanXVelocity) ^ 2 + (MegamanYVelocity) ^ 2)
        MegamanVelocityVectorAngle = 90 - (Atan2(MegamanXVelocity, -MegamanYVelocity) * (180 / PI))
        If MegamanXVelocity = 0 AndAlso MegamanYVelocity = 0 Then
            MegamanVelocityVectorAngle = -1
        ElseIf MegamanVelocityVectorAngle < 0 Then
            MegamanVelocityVectorAngle += 360
        End If
        If MegamanVelocityVectorAngle <> -1 Then
            Megaman30Angle = (MegamanVelocityVectorAngle \ 30) + 1
        Else
            Megaman30Angle = -1
        End If
        If MegamanVelocityVectorAngle <> -1 Then
            Megaman45Angle = (MegamanVelocityVectorAngle \ 45) + 1
        Else
            Megaman45Angle = -1
        End If
        '******************************************
        'This section sets up the specialized collision detection rectangles to be at Megaman's new location.
        '******************************************
        MegamanCollisionRightRectangle.X = MegamanRectangle.Right
        MegamanCollisionRightRectangle.Y = MegamanRectangle.Top
        MegamanCollisionRightRectangle.Height = MegamanRectangle.Height
        MegamanCollisionLeftRectangle.X = MegamanRectangle.Left
        MegamanCollisionLeftRectangle.Y = MegamanRectangle.Top
        MegamanCollisionLeftRectangle.Height = MegamanRectangle.Height
        MegamanCollisionTopRectangle.X = MegamanRectangle.Left
        MegamanCollisionTopRectangle.Y = MegamanRectangle.Top
        MegamanCollisionTopRectangle.Width = MegamanRectangle.Width
        MegamanCollisionBottomRectangle.X = MegamanRectangle.Left
        MegamanCollisionBottomRectangle.Y = MegamanRectangle.Bottom
        MegamanCollisionBottomRectangle.Width = MegamanRectangle.Width
        MegamanCollisionHorizontalRectangle.X = MegamanRectangle.Left
        MegamanCollisionHorizontalRectangle.Y = (MegamanRectangle.Top + MegamanRectangle.Bottom) / 2
        MegamanCollisionHorizontalRectangle.Width = MegamanRectangle.Width
        MegamanCollisionVerticalRectangle.X = (MegamanRectangle.Left + MegamanRectangle.Right) / 2
        MegamanCollisionVerticalRectangle.Y = MegamanRectangle.Top
        MegamanCollisionVerticalRectangle.Height = MegamanRectangle.Height
        '******************************************
        'This section processes the specialized collision rectangles to detect whether the player has moved inside any walls.
        '******************************************
        MegamanOnGround = False
        MegamanXCollision = 0
        MegamanYCollision = 0
        Array.Clear(MegamanCollisionArray, 0, 9)
        If CollisionRegion2.IsVisible(MegamanRectangle.Left, MegamanRectangle.Top) = True Then 'Test upper left
            MegamanXCollision -= 1
            If ((Megaman30Angle >= 1 AndAlso Megaman30Angle <= 5) OrElse (Megaman30Angle >= 8 AndAlso Megaman30Angle <= 12)) Then
                MegamanYCollision += 1
            End If
            MegamanCollisionArray(0) = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Right, MegamanRectangle.Top) = True Then 'Test upper right
            MegamanXCollision += 1
            If (Megaman30Angle >= 2 AndAlso Megaman30Angle <= 11) Then
                MegamanYCollision += 1
            End If
            MegamanCollisionArray(2) = True
        End If
        If CollisionRegion2.IsVisible((MegamanRectangle.Right - ((3 / 4) * MegamanRectangle.Width)), MegamanRectangle.Bottom) = True Then 'Test lower middle left bottom for ground
            MegamanOnGround = True
        End If
        If CollisionRegion2.IsVisible((MegamanRectangle.Right - ((1 / 2) * MegamanRectangle.Width)), MegamanRectangle.Bottom) = True Then 'Test middle bottom for ground
            MegamanOnGround = True
            MegamanYCollision = 0
            MegamanCollisionArray(7) = True
        End If
        If CollisionRegion2.IsVisible((MegamanRectangle.Right - ((1 / 4) * MegamanRectangle.Width)), MegamanRectangle.Bottom) = True Then 'Test lower middle right for ground
            MegamanOnGround = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Left, MegamanRectangle.Bottom) = True Then 'Test lower left
            'MegamanOnGround = True
            If (Megaman30Angle >= 2 AndAlso Megaman30Angle <= 11) Then
                'MegamanOnGround = True
                MegamanYCollision -= 1
            End If
            If ((Megaman30Angle >= 6 AndAlso Megaman30Angle <= 10) OrElse Megaman30Angle = -1) AndAlso MegamanCollisionArray(0) = False Then
                MegamanOnGround = True
            End If
            If (Megaman30Angle = 9 OrElse Megaman30Angle = 10) AndAlso MegamanOnGround = False Then
                'MegamanOnGround = True
                MegamanXCollision -= 1
            End If
            'MegamanYCollision -= 1
            MegamanCollisionArray(6) = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Right, MegamanRectangle.Bottom) = True Then 'Test lower right
            'MegamanOnGround = True
            If ((Megaman30Angle >= 1 AndAlso Megaman30Angle <= 5) OrElse (Megaman30Angle >= 8 AndAlso Megaman30Angle <= 12)) Then
                'MegamanOnGround = True
                MegamanYCollision -= 1
            End If
            If ((Megaman30Angle = 1 OrElse (Megaman30Angle >= 9 AndAlso Megaman30Angle <= 12)) OrElse Megaman30Angle = -1) AndAlso MegamanCollisionArray(2) = False Then
                MegamanOnGround = True
            End If
            If (Megaman30Angle = 9 OrElse Megaman30Angle = 10) AndAlso MegamanOnGround = False Then
                'MegamanOnGround = True
                MegamanXCollision += 1
            End If
            'MegamanYCollision -= 1
            MegamanCollisionArray(8) = True
        End If
        'If CollisionRegion2.IsVisible(MegamanRectangle.Left, MegamanRectangle.Top) = True Then 'Test upper left
        '    MegamanXCollision -= 1
        '    If ((Megaman30Angle >= 1 AndAlso Megaman30Angle <= 5) OrElse (Megaman30Angle >= 8 AndAlso Megaman30Angle <= 12)) Then
        '        MegamanYCollision += 1
        '    End If
        '    MegamanCollisionArray(0) = True
        'End If
        'If CollisionRegion2.IsVisible(MegamanRectangle.Right, MegamanRectangle.Top) = True Then 'Test upper right
        '    MegamanXCollision += 1
        '    If (Megaman30Angle >= 2 AndAlso Megaman30Angle <= 11) Then
        '        MegamanYCollision += 1
        '    End If
        '    MegamanCollisionArray(2) = True
        'End If
        If CollisionRegion2.IsVisible((MegamanRectangle.Right - (MegamanRectangle.Width / 2)), MegamanRectangle.Top) = True Then 'Test middle top
            MegamanYCollision += 1
            MegamanXCollision = 0
            MegamanCollisionArray(1) = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Left, (MegamanRectangle.Bottom - (MegamanRectangle.Height / 2))) = True Then 'Test middle left
            MegamanXCollision -= 1
            MegamanYCollision = 0
            MegamanCollisionArray(3) = True
        End If
        If CollisionRegion2.IsVisible((MegamanRectangle.Right - (MegamanRectangle.Width / 2)), (MegamanRectangle.Bottom - (MegamanRectangle.Height / 2))) = True Then 'Test center
            MegamanHealth -= 1
            If MegamanHealth = 0 Then
                MegamanDead = True
            End If
            MegamanCollisionArray(4) = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Right, (MegamanRectangle.Bottom - (MegamanRectangle.Height / 2))) = True Then 'Test middle right
            MegamanXCollision += 1
            MegamanYCollision = 0
            MegamanCollisionArray(5) = True
        End If
        '******************************************
        'This section attempts to fix a few bugs with overhangs and narrow pits.
        '******************************************
        'If ((MegamanCollisionArray(2) = True) AndAlso (MegamanCollisionArray(5) = False) AndAlso (MegamanCollisionArray(8) = True)) OrElse ((MegamanCollisionArray(0) = True) AndAlso (MegamanCollisionArray(3) = False) AndAlso (MegamanCollisionArray(6) = True)) Then
        '    MegamanYCollision = 0
        'End If
        'If ((MegamanCollisionArray(6) = True) AndAlso (MegamanCollisionArray(7) = False) AndAlso (MegamanCollisionArray(8) = True)) OrElse ((MegamanCollisionArray(0) = True) AndAlso (MegamanCollisionArray(1) = False) AndAlso (MegamanCollisionArray(2) = True)) Then
        '    MegamanXCollision = 0
        'End If
        '******************************************
        'This section creates a duplicate of the current terrain and intersects it with the collision rectangles to determine how far inside a wall the player moved.
        '******************************************
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionLeftRectangle)
        MegamanCollisionRectangleTempY = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionVerticalRectangle)
        MegamanCollisionRectangleTempY2 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionRightRectangle)
        MegamanCollisionRectangleTempY3 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        '******************************************
        'This section compares the collision rectangles to the character size to fix a few collision bugs.
        '******************************************
        'If MegamanCollisionRectangleTempY.Height >= MegamanRectangle.Height OrElse Not (MegamanCollisionArray(0) = False OrElse MegamanCollisionArray(6) = False) Then
        '    MegamanCollisionRectangleTempY.Height = 0
        'ElseIf MegamanCollisionRectangleTempY.Height > (MegamanRectangle.Height / 2) Then
        '    MegamanCollisionRectangleTempY.Height = (MegamanCollisionRectangleTempY.Height - (MegamanRectangle.Height / 2))
        'End If
        'If MegamanCollisionRectangleTempY2.Height >= MegamanRectangle.Height OrElse Not (MegamanCollisionArray(1) = False OrElse MegamanCollisionArray(7) = False) Then
        '    MegamanCollisionRectangleTempY2.Height = 0
        'ElseIf MegamanCollisionRectangleTempY2.Height > (MegamanRectangle.Height / 2) Then
        '    MegamanCollisionRectangleTempY2.Height = (MegamanCollisionRectangleTempY2.Height - (MegamanRectangle.Height / 2))
        'End If
        'If MegamanCollisionRectangleTempY3.Height >= MegamanRectangle.Height OrElse Not (MegamanCollisionArray(2) = False OrElse MegamanCollisionArray(8) = False) Then
        '    MegamanCollisionRectangleTempY3.Height = 0
        'ElseIf MegamanCollisionRectangleTempY3.Height > (MegamanRectangle.Height / 2) Then
        '    MegamanCollisionRectangleTempY3.Height = (MegamanCollisionRectangleTempY3.Height - (MegamanRectangle.Height / 2))
        'End If
        If MegamanCollisionRectangleTempY.Height > (MegamanRectangle.Height / 2) Then
            MegamanCollisionRectangleTempY.Height = 0
        End If
        If MegamanCollisionRectangleTempY2.Height > (MegamanRectangle.Height / 2) Then
            MegamanCollisionRectangleTempY2.Height = 0
        End If
        If MegamanCollisionRectangleTempY3.Height > (MegamanRectangle.Height / 2) Then
            MegamanCollisionRectangleTempY3.Height = 0
        End If
        '******************************************
        'This section calculates the new vertical character position to move outside of any walls.
        '******************************************
        MegamanCollisionTempY = Max(Max(MegamanCollisionRectangleTempY.Height, MegamanCollisionRectangleTempY2.Height), MegamanCollisionRectangleTempY3.Height)
        MegamanRectangle.Y = MegamanRectangle.Y + (MegamanCollisionTempY * Sign(MegamanYCollision))
        If MegamanYCollision < 0 AndAlso MegamanCollisionTempX <> 0 Then
            MegamanOnGround = True
        End If
        '******************************************
        'This section creates a duplicate of the current terrain and intersects it with the collision rectangles to determine how far inside a wall the player moved.
        '******************************************
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionTopRectangle)
        MegamanCollisionRectangleTempX = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionHorizontalRectangle)
        MegamanCollisionRectangleTempX2 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionBottomRectangle)
        MegamanCollisionRectangleTempX3 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        '******************************************
        'This section compares the collision rectangles to the character size to fix a few collision bugs.
        '******************************************
        'If MegamanCollisionRectangleTempX.Width >= MegamanRectangle.Width Then
        '    MegamanCollisionRectangleTempX.Width = 0
        'ElseIf MegamanCollisionRectangleTempX.Width > (MegamanRectangle.Width / 2) Then
        '    MegamanCollisionRectangleTempX.Width = (MegamanCollisionRectangleTempX.Width - (MegamanRectangle.Width / 2))
        'End If
        'If MegamanCollisionRectangleTempX2.Width >= MegamanRectangle.Width Then
        '    MegamanCollisionRectangleTempX2.Width = 0
        'ElseIf MegamanCollisionRectangleTempX2.Width > (MegamanRectangle.Width / 2) Then
        '    MegamanCollisionRectangleTempX2.Width = (MegamanCollisionRectangleTempX2.Width - (MegamanRectangle.Width / 2))
        'End If
        'If MegamanCollisionRectangleTempX3.Width >= MegamanRectangle.Width Then
        '    MegamanCollisionRectangleTempX3.Width = 0
        'ElseIf MegamanCollisionRectangleTempX3.Width > (MegamanRectangle.Width / 2) Then
        '    MegamanCollisionRectangleTempX3.Width = (MegamanCollisionRectangleTempX3.Width - (MegamanRectangle.Width / 2))
        'End If
        If MegamanCollisionRectangleTempX.Width > (MegamanRectangle.Width / 2) Then
            MegamanCollisionRectangleTempX.Width = 0
        End If
        If MegamanCollisionRectangleTempX2.Width > (MegamanRectangle.Width / 2) Then
            MegamanCollisionRectangleTempX2.Width = 0
        End If
        If MegamanCollisionRectangleTempX3.Width > (MegamanRectangle.Width / 2) Then
            MegamanCollisionRectangleTempX3.Width = 0
        End If
        '******************************************
        'This section handles a specialized out of window case.
        '******************************************
        If MegamanRectangle.Y >= (GameArea.Height - MegamanRectangle.Height) Then
            MegamanOnFrame = True
            MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height)
        End If
        '******************************************
        'This section fixes a bug with jittery Y positions.
        '******************************************
        MegamanRectangle.Y = Round(MegamanRectangle.Y, 0, MidpointRounding.AwayFromZero)
        '******************************************
        'This section calculates the new horizontal character position to move outside of any walls.
        '******************************************
        MegamanCollisionTempX = Max(Max(MegamanCollisionRectangleTempX.Width, MegamanCollisionRectangleTempX2.Width), MegamanCollisionRectangleTempX3.Width)
        MegamanRectangle.X = MegamanRectangle.X - (MegamanCollisionTempX * Sign(MegamanXCollision))
        '******************************************
        'This section updates the collision rectangles again.
        '******************************************
        If MainWindow.DebugBoundingBoxes = True Then
            MegamanCollisionRightRectangle.X = MegamanRectangle.Right
            MegamanCollisionRightRectangle.Y = MegamanRectangle.Top
            MegamanCollisionRightRectangle.Height = MegamanRectangle.Height
            MegamanCollisionLeftRectangle.X = MegamanRectangle.Left
            MegamanCollisionLeftRectangle.Y = MegamanRectangle.Top
            MegamanCollisionLeftRectangle.Height = MegamanRectangle.Height
            MegamanCollisionTopRectangle.X = MegamanRectangle.Left
            MegamanCollisionTopRectangle.Y = MegamanRectangle.Top
            MegamanCollisionTopRectangle.Width = MegamanRectangle.Width
            MegamanCollisionBottomRectangle.X = MegamanRectangle.Left
            MegamanCollisionBottomRectangle.Y = MegamanRectangle.Bottom
            MegamanCollisionBottomRectangle.Width = MegamanRectangle.Width
            MegamanCollisionHorizontalRectangle.X = MegamanRectangle.Left
            MegamanCollisionHorizontalRectangle.Y = (MegamanRectangle.Top + MegamanRectangle.Bottom) / 2
            MegamanCollisionHorizontalRectangle.Width = MegamanRectangle.Width
            MegamanCollisionVerticalRectangle.X = (MegamanRectangle.Left + MegamanRectangle.Right) / 2
            MegamanCollisionVerticalRectangle.Y = MegamanRectangle.Top
            MegamanCollisionVerticalRectangle.Height = MegamanRectangle.Height
        End If
        If MegamanDead = True Then
            GameOverYeah()
        End If
    End Sub
    Friend Sub AnimateMegamanRectangle()
        MegamanAnimation2 = MegamanAnimation * 2
        If MegamanLeft = True Then
            MegamanAnimation2 += 1
        End If
        Select Case MegamanAnimation
            Case = 0    'Standing
                Select Case MegamanAnimationFrame
                    Case Is <= (MegamanBlinkRate - 3)
                        MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing1.png")
                    Case (MegamanBlinkRate - 2), (MegamanBlinkRate)
                        MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing2.png")
                    Case Is = (MegamanBlinkRate - 1)
                        MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Standing3.png")
                End Select
                If MegamanAnimationFrame < MegamanBlinkRate Then
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 1    'Running
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Running" & MegamanAnimationFrame & ".png")
                If MegamanAnimationFrame < 11 Then
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 2 'The first frame is only for starting running
                End If
            Case = 2    'Jumping
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Jumping" & MegamanAnimationFrame & ".png")
                If MegamanAnimationFrame < 5 Then
                    MegamanAnimationFrame += 1
                End If
            Case = 3    'Landing
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\Landing" & MegamanAnimationFrame & ".png")
                Select Case MegamanAnimationFrame
                    Case Is = 1
                        If ButtonHeld((MegamanAnimation2 Mod 2)) = True Then
                            MegamanAnimationFrame = 3
                            MegamanAnimation = 1
                        Else
                            MegamanAnimationFrame += 1
                        End If
                    Case Is = 2
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 0
                End Select
            Case = 4    'Warping In
                MegamanRectangleImage = Image.FromFile(GamePath & "\Resources\WarpIn" & MegamanAnimationFrame & ".png")
                If MegamanAnimationFrame < 7 Then
                    MegamanAnimationFrame += 1
                Else
                    If MegamanRectangle.Y < (GameArea.Height - MegamanRectangle.Height) Then   'If spawning in midair...
                        MegamanAnimationFrame = 5
                        MegamanAnimation = 2 '+ (MegamanAnimation Mod 2)    'Display falling animation
                    Else    'If spawning on the ground...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 0 '+ (MegamanAnimation Mod 2)    'Display standing animation
                        MegamanPhysicsTimer.Change(TimerStartDelay, TimerInterval)
                    End If
                End If
        End Select
        If (MegamanAnimation2 Mod 2) = 1 Then 'If left...
            MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX) 'Mirror graphic
        End If
        Try
            MegamanRectangle2.X = ((MegamanRectangle.X + (MegamanRectangle.Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)) / 2)
            MegamanRectangle2.Y = (MegamanRectangle.Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY) - MegamanRectangle.Height))
            MegamanRectangle2.Width = ((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiX)
            MegamanRectangle2.Height = ((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiY)
        Catch ex As Exception
        End Try
        Invalidate()
    End Sub
    Private Sub ResetEverything()

    End Sub
    Private Sub GameOverYeah()

    End Sub
    Private Sub GameWindow_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MainWindow.Close()
    End Sub
    Private Sub ThreadCountLabel_Click(sender As Object, e As EventArgs) Handles ThreadCountLabel.Click
        If DebugLabelVisible = True Then
            DebugLabelVisible = False
        Else
            DebugLabelVisible = True
        End If
    End Sub
    Private Sub FPSLabel_Click(sender As Object, e As EventArgs) Handles FPSLabel.Click
        If FPSLabelVisible = True Then
            FPSLabelVisible = False
        Else
            FPSLabelVisible = True
        End If
    End Sub
End Class