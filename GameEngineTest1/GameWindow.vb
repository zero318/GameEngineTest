Imports System.Threading
Imports System.Math
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
    Dim SlopeTestPoints(3) As PointF ' = {New PointF(), New PointF(), New PointF()}
    'Dim SlopeTestTypes(3) As Byte ' = {New Byte, New Byte, New Byte}
    Dim SlopeTest As New Drawing2D.GraphicsPath() 'SlopeTestPoints, SlopeTestTypes)
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
    Dim MegamanAnimation As Byte = 8
    Dim MegamanAnimationFrame As Byte = 1
    Dim MegamanAnimationTimerCall As New TimerCallback(AddressOf AnimateMegamanRectangle)
    Dim MegamanAnimationTimer As New Timer(MegamanAnimationTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim MegamanMegamanPhysicsTimerCall As New TimerCallback(AddressOf MegamanRectanglePhysics)
    Dim MegamanPhysicsTimer As New Timer(MegamanMegamanPhysicsTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ThreadCountLabel.Visible = False
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
        'For Each ctrl In GameArea.Controls 'This loads all the panels in the collision area into the collision detection region and then deletes them.
        '    If TypeOf ctrl Is Panel Then
        '        CollisionTestRectangle.X = ctrl.Left
        '        CollisionTestRectangle.Y = ctrl.Top
        '        CollisionTestRectangle.Width = ctrl.Width
        '        CollisionTestRectangle.Height = ctrl.Height
        '        CollisionRegion.Union(CollisionTestRectangle)
        '        ctrl.Dispose()
        '    End If
        'Next
        MegamanAnimationTimer.Change(TimerStartDelay, TimerInterval)
        If MainWindow.DebugHUDEnabled = True Then
            ThreadCountTimer.Change(TimerStartDelay, TimerInterval)
            FPSTimer.Change(TimerStartDelay, 1000)
            'DebugWindow.Show()
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
        'For Each ctrl In GameArea.Controls.OfType(Of Panel) 'This loads all the panels in the collision area into the collision detection region and then deletes them.
        '    If TypeOf ctrl Is Panel Then
        '        CollisionTestRectangle.X = ctrl.Left
        '        CollisionTestRectangle.Y = ctrl.Top
        '        CollisionTestRectangle.Width = ctrl.Width
        '        CollisionTestRectangle.Height = ctrl.Height
        '        CollisionRegion.Union(CollisionTestRectangle)
        '        ctrl.Dispose()
        '    End If
        'Next
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
        'GameArea.Controls.OfType(Of Panel)
    End Sub
    Friend Sub UpdateFPS()
        FPSLabel.Text = "FPS: " & FPS
        FPS = 0
    End Sub
    Private Sub UpdateThreadCount()
        ThreadPool.GetAvailableThreads(Test1, Test2)
        Try
            'DebugWindow.DebugOutputLabel.Text = "Available Workers: " & Test1 & " Available IOs: " & Test2 & vbCrLf & "MegamanAnimation: " & MegamanAnimation & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiX: " & ScreenDpiX & " ScreenDpiY: " & ScreenDpiY & " ScreenX: " & GameArea.Width & " ScreenY: " & GameArea.Height & vbCrLf & "MegamanX: " & MegamanRectangle.X & " MegamanY: " & MegamanRectangle.Y & vbCrLf & "MegamanWidth: " & MegamanRectangle.Width & " MegamanHeight: " & MegamanRectangle.Height & vbCrLf & "Megaman2X: " & MegamanRectangle2.X & " Megaman2Y: " & MegamanRectangle2.Y & vbCrLf & "MegamanLeft: " & MegamanLeft & vbCrLf & "RectTempX1: " & MegamanCollisionRectangleTempX.Height & " RectTempX2: " & MegamanCollisionRectangleTempX2.Height & " RectTempX3: " & MegamanCollisionRectangleTempX3.Height & vbCrLf & "RectTempY1: " & MegamanCollisionRectangleTempY.Height & " RectTempY2: " & MegamanCollisionRectangleTempY2.Height & " RectTempY3: " & MegamanCollisionRectangleTempY3.Height & vbCrLf & "XCollision: " & MegamanXCollision & " YCollision: " & MegamanYCollision & vbCrLf & "CollisionTempX: " & MegamanCollisionTempX & " CollisionTempY: " & MegamanCollisionTempY & vbCrLf & "XVelocity: " & MegamanXVelocity & " YVelocity: " & MegamanYVelocity & vbCrLf & "OnGround: " & MegamanOnGround & vbCrLf & "TempVar: " & MegamanOnFrame
            ThreadCountLabel.Text = "Available Workers: " & Test1 & " Available IOs: " & Test2 & vbCrLf & "MegamanAnimation: " & MegamanAnimation & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiX: " & ScreenDpiX & " ScreenDpiY: " & ScreenDpiY & " ScreenX: " & GameArea.Width & " ScreenY: " & GameArea.Height & vbCrLf & "MegamanX: " & MegamanRectangle.X & " MegamanY: " & MegamanRectangle.Y & vbCrLf & "MegamanWidth: " & MegamanRectangle.Width & " MegamanHeight: " & MegamanRectangle.Height & vbCrLf & "Megaman2X: " & MegamanRectangle2.X & " Megaman2Y: " & MegamanRectangle2.Y & vbCrLf & "MegamanLeft: " & MegamanLeft & vbCrLf & "RectTempX1: " & MegamanCollisionRectangleTempX.Height & " RectTempX2: " & MegamanCollisionRectangleTempX2.Height & " RectTempX3: " & MegamanCollisionRectangleTempX3.Height & vbCrLf & "RectTempY1: " & MegamanCollisionRectangleTempY.Height & " RectTempY2: " & MegamanCollisionRectangleTempY2.Height & " RectTempY3: " & MegamanCollisionRectangleTempY3.Height & vbCrLf & "XCollision: " & MegamanXCollision & " YCollision: " & MegamanYCollision & vbCrLf & "CollisionTempX: " & MegamanCollisionTempX & " CollisionTempY: " & MegamanCollisionTempY & vbCrLf & "XVelocity: " & MegamanXVelocity & " YVelocity: " & MegamanYVelocity & vbCrLf & "OnGround: " & MegamanOnGround & vbCrLf & "TempVar: " & MegamanOnFrame
            'DebugWindow.DebugOutputLabel.Text = ThreadCountLabel.Text
            'DebugWindow.Invalidate()
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
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 2    'Run right
                        MegamanLeft = False
                    End If
                Case Keys.Left
                    MegamanXVelocity = -9 '(-20 * bytSpeed)
                    If ((MegamanAnimation <> 4) AndAlso (MegamanAnimation <> 5)) AndAlso ButtonHeld(1) = False Then   'If the player isn't jumping and the key isn't being held...
                        ButtonHeld(1) = True
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 3    'Run left
                        MegamanLeft = True
                    End If
                Case Keys.Up
                    If MegamanOnGround = True Then '(MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height)) OrElse (MegamanCollisionTempY > 0 AndAlso MegamanYCollision < 0) Then   'If the player is on the ground...
                        MegamanYVelocity = -39.2
                        MegamanOnGround = False
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
            Case Keys.Left  'If left was released...
                MegamanXVelocity = 0
                ButtonHeld(1) = False
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
                'CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle))
                'CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Transparent, Rectangle.Round(MegamanRectangle2))
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
        'CustomDoubleBuffer.Invalidate()
        CustomGraphicsBuffer.Dispose()
        FPS += 1
    End Sub
    Private Sub MegamanRectanglePhysics()
        MegamanOnFrame = False
        MegamanXCollision = 0
        MegamanYCollision = 0
        If MegamanRectangle.Y >= (GameArea.Height - MegamanRectangle.Height) Then
            MegamanOnFrame = True
            MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height)
        End If
        If MegamanAnimation <> 8 AndAlso MegamanAnimation <> 9 Then 'If vertical motion is not 0...
            If MegamanOnGround = False Then
                MegamanRectangle.Y = MegamanRectangle.Y + MegamanYVelocity    'Calculates the new position
                MegamanYVelocity = MegamanYVelocity + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
            Else
                MegamanYVelocity = 0
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
            End If
            If MegamanRectangle.Y < 0 Then 'If the player is out of bounds above the top...
                MegamanRectangle.Y = 0
            End If
        End If
        If MegamanXVelocity <> 0 Then
            MegamanRectangle.X = MegamanRectangle.X + (MegamanXVelocity * MegamanVelocityMultiplier)
        End If
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
        If CollisionRegion2.IsVisible((MegamanRectangle.Right - (3 * (MegamanRectangle.Width / 4))), MegamanRectangle.Bottom) = True Then 'Test lower middle left bottom for ground
            MegamanOnGround = True
        ElseIf CollisionRegion2.IsVisible((MegamanRectangle.Right - (1 * (MegamanRectangle.Width / 2))), MegamanRectangle.Bottom) = True Then 'Test middle bottom for ground
            MegamanOnGround = True
            MegamanCollisionArray(7) = True
        ElseIf CollisionRegion2.IsVisible((MegamanRectangle.Right - (1 * (MegamanRectangle.Width / 4))), MegamanRectangle.Bottom) = True Then 'Test lower middle right for ground
            MegamanOnGround = True
        Else
            MegamanOnGround = False
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Left, MegamanRectangle.Bottom) = True Then 'Test lower left
            If MegamanOnGround = False Then
                MegamanXCollision -= 1
            Else
                MegamanYCollision -= 1
            End If
            MegamanCollisionArray(6) = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Right, MegamanRectangle.Bottom) = True Then 'Test lower right
            If MegamanOnGround = False Then
                MegamanXCollision += 1
            Else
                MegamanYCollision -= 1
            End If
            MegamanCollisionArray(8) = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Left, MegamanRectangle.Top) = True Then 'Test upper left
            MegamanXCollision -= 1
            MegamanYCollision += 1
            MegamanCollisionArray(0) = True
        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Right, MegamanRectangle.Top) = True Then 'Test upper right
            MegamanXCollision += 1
            MegamanYCollision += 1
            MegamanCollisionArray(2) = True
        End If
        If CollisionRegion2.IsVisible((MegamanRectangle.Right - (MegamanRectangle.Width / 2)), MegamanRectangle.Top) = True Then 'Test middle top
            MegamanYCollision += 1
            MegamanXCollision = 0
            MegamanCollisionArray(1) = True
        Else

        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Left, (MegamanRectangle.Bottom - (MegamanRectangle.Height / 2))) = True Then 'Test middle left
            MegamanXCollision -= 1
            MegamanYCollision = 0
            MegamanCollisionArray(3) = True
        Else

        End If
        If CollisionRegion2.IsVisible(MegamanRectangle.Right, (MegamanRectangle.Bottom - (MegamanRectangle.Height / 2))) = True Then 'Test middle right
            MegamanXCollision += 1
            MegamanYCollision = 0
            MegamanCollisionArray(5) = True
        Else

        End If
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionLeftRectangle)
        MegamanCollisionRectangleTempY = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionVerticalRectangle)
        MegamanCollisionRectangleTempY2 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionRightRectangle)
        MegamanCollisionRectangleTempY3 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        If MegamanCollisionRectangleTempY.Height >= MegamanRectangle.Height Then
            MegamanCollisionRectangleTempY.Height = 0
        End If
        If MegamanCollisionRectangleTempY2.Height >= MegamanRectangle.Height Then
            MegamanCollisionRectangleTempY2.Height = 0
        End If
        If MegamanCollisionRectangleTempY3.Height >= MegamanRectangle.Height Then
            MegamanCollisionRectangleTempY3.Height = 0
        End If
        MegamanCollisionTempY = Max(Max(MegamanCollisionRectangleTempY.Height, MegamanCollisionRectangleTempY2.Height), MegamanCollisionRectangleTempY3.Height)
        MegamanRectangle.Y = MegamanRectangle.Y + (MegamanCollisionTempY * Sign(MegamanYCollision))
        If MegamanYCollision < 0 AndAlso MegamanCollisionTempX <> 0 Then
            MegamanOnGround = True
        End If
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionTopRectangle)
        MegamanCollisionRectangleTempX = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionHorizontalRectangle)
        MegamanCollisionRectangleTempX2 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        CollisionRegion3 = CollisionRegion2.Clone()
        CollisionRegion3.Intersect(MegamanCollisionBottomRectangle)
        MegamanCollisionRectangleTempX3 = CollisionRegion3.GetBounds(GameAreaGraphics2)
        If MegamanCollisionRectangleTempX.Width >= MegamanRectangle.Width Then
            MegamanCollisionRectangleTempX.Width = 0
        End If
        If MegamanCollisionRectangleTempX2.Width >= MegamanRectangle.Width Then
            MegamanCollisionRectangleTempX2.Width = 0
        End If
        If MegamanCollisionRectangleTempX3.Width >= MegamanRectangle.Width Then
            MegamanCollisionRectangleTempX3.Width = 0
        End If
        If MegamanRectangle.Y >= (GameArea.Height - MegamanRectangle.Height) Then
            MegamanOnFrame = True
            MegamanRectangle.Y = (GameArea.Height - MegamanRectangle.Height)
        End If
        MegamanRectangle.Y = Round(MegamanRectangle.Y, 0, MidpointRounding.AwayFromZero)
        MegamanCollisionTempX = Max(Max(MegamanCollisionRectangleTempX.Width, MegamanCollisionRectangleTempX2.Width), MegamanCollisionRectangleTempX3.Width)
        MegamanRectangle.X = MegamanRectangle.X - (MegamanCollisionTempX * Sign(MegamanXCollision))
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
            'MegamanRectangle.Y = MegamanRectangle2.Y
            'MegamanRectangle.Height = MegamanRectangle2.Height
        Catch ex As Exception
        End Try
        'GameArea.Invalidate()
        'CustomDoubleBuffer.Invalidate()
    End Sub
    Private Sub GameOverYeah()

    End Sub
    Private Sub GameWindow_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MainWindow.Close()
    End Sub
End Class