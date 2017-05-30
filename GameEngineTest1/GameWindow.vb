Imports System.Threading
Imports System.Math
Imports System.Drawing.Drawing2D
Public Class GameWindow
    Dim LoopIndexArray(3) As Integer 'Drawing = 0, Physics = 1, PanelArray = 2, and PanelRender = 3
    Dim StartRendering() As Boolean = {False, False} 'Normal = 0 and Background = 1
    Dim FPS As Integer
    Dim GamePath As String = My.Application.Info.DirectoryPath
    Dim ResourcesPath As String = GamePath & "\Resources\"
    Dim GameAreaGraphics(1) As Graphics
    Dim CustomDoubleBuffer As New BufferedGraphicsContext
    Dim CustomBackgroundBufferContext As New BufferedGraphicsContext
    Dim CustomBackgroundBuffer As BufferedGraphics
    Dim CustomGraphicsBuffer As BufferedGraphics
    Dim PaintSomegroundOnArray() As Boolean = {False, True} 'Background = 0 and Foreground = 1
    Dim ThreadInfoArray(1) As Integer
    Dim ScreenDpiArray(1) As Integer 'X = 0 and Y = 1
    Dim LabelVisible() As Boolean = {False, True} 'DebugLabel = 0 and FPSLabel = 1
    Dim TimerData() As Integer = {100, 0} 'Interval = 0 and StartDelay = 1
    Dim TimerArray() As Timer = {New Timer(New TimerCallback(AddressOf AnimateMegamanRectangle), vbNull, Timeout.Infinite, Timeout.Infinite), New Timer(New TimerCallback(AddressOf MegamanRectanglePhysics), vbNull, Timeout.Infinite, Timeout.Infinite), New Timer(New TimerCallback(AddressOf UpdateThreadCount), vbNull, Timeout.Infinite, Timeout.Infinite), New Timer(New TimerCallback(AddressOf UpdateFPS), vbNull, Timeout.Infinite, Timeout.Infinite), New Timer(New TimerCallback(AddressOf LoadPanels), vbNull, Timeout.Infinite, Timeout.Infinite)} 'Animation = 0, Physics = 1, DebugLabel = 2, FPS = 3, and LoadPanels = 4
    Dim ButtonHeld() As Boolean = {False, False}
    Dim GameAreaRectangle As Rectangle
    Dim CollisionRegionArray() As Region = {New Region, New Region, New Region}
    Dim CollisionRegionLadders As New Region
    Dim GraphicsRectangleArray() As Rectangle
    Dim GraphicsTextureArray() As Image
    Dim TextureArray() As Image = {Image.FromFile(ResourcesPath & "bkMaze.bmp"), Image.FromFile(ResourcesPath & "MazeBlock1.png"), Image.FromFile(ResourcesPath & "MazeBlock2.png"), Image.FromFile(ResourcesPath & "MazeCeilingBlockLayerA.png"), Image.FromFile(ResourcesPath & "MazeCeilingBlockLayerB.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockLayerA.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockLayerB.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockSlopeRightA.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockSlopeRightB.png"), Image.FromFile(ResourcesPath & "MazeFloorSlopeRight.png")}
    Dim TextureBrushArray() As TextureBrush
    'Megaman specific variables start here
    Dim MegamanSpawnLocation As Integer = 2
    Dim MegamanBlinkRate As Integer = 15
    Dim MegamanRectangle() As RectangleF = {New RectangleF(50, 400, 100, 100), New RectangleF(50, 400, 100, 100)}
    Dim MegamanCollisionRectangleTempArray() As RectangleF = {New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0)}
    Dim MegamanRectangleImage As Image
    Dim MegamanCollisionRectangleArray(5) As RectangleF
    Dim MegamanCollisionArray(8) As Boolean 'First row: 0, 1, 2 ... Second row: 3, 4, 5 ... Third row: 6, 7, 8
    Dim MegamanCollisionDetectionArray(1) As Integer 'X = 0 and Y = 1
    Dim MegamanCollisionTempArray(1) As Integer 'Y = 0 and X = 1
    Dim MegamanLeft As Boolean
    Dim MegamanOnArray(1) As Boolean 'Ground = 0 and Frame = 1
    Dim MegamanVelocityMultiplier As Integer
    Dim MegamanVelocityArray(1) As Double 'X = 0 and Y = 1
    Dim MegamanAnimationArray() As Byte = {4, 0}
    Dim MegamanAnimationFrame As Byte = 1
    Dim MegamanVelocityVector(1) As Double 'Angle = 0 and Magnitude = 1
    Dim MegamanAngleArray(1) As SByte '30 = 0 and 45 = 1
    Dim MegamanHealth As Integer = 100
    Dim MegamanDead As Boolean
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        If PaintSomegroundOnArray(0) = True Then
            MyBase.OnPaintBackground(e)
        End If
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If PaintSomegroundOnArray(1) = True Then
            MyBase.OnPaint(e)
        End If
    End Sub
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Megaman.Top = Megaman.Parent.Height - Megaman.Height
        CollisionTestPanel1.Top = CollisionTestPanel1.Parent.Height - CollisionTestPanel1.Height
        Select Case MegamanSpawnLocation
            Case Is = 1
                MegamanRectangle(0) = Rectangle.FromLTRB(Megaman.Left, Megaman.Top, Megaman.Left + Megaman.Width, Megaman.Top + Megaman.Height)
                MegamanRectangle(1) = Rectangle.FromLTRB(Megaman.Left, Megaman.Top, Megaman.Left + Megaman.Width, Megaman.Top + Megaman.Height)
            Case Is = 2
                MegamanRectangle(0) = Rectangle.FromLTRB(Megaman2.Left, Megaman2.Top, Megaman2.Left + Megaman2.Width, Megaman2.Top + Megaman2.Height)
                MegamanRectangle(1) = Rectangle.FromLTRB(Megaman2.Left, Megaman2.Top, Megaman2.Left + Megaman2.Width, Megaman2.Top + Megaman2.Height)
        End Select
        GameAreaRectangle = Rectangle.FromLTRB(GameArea.Left, GameArea.Top, GameArea.Left + GameArea.Width, GameArea.Top + GameArea.Height)
        GameAreaGraphics(0) = GameArea.CreateGraphics()
        GameAreaGraphics(1) = GameArea.CreateGraphics()
        CollisionRegionArray(0).Exclude(GameAreaRectangle)
        CollisionRegionLadders.Exclude(GameAreaRectangle)
        TimerArray(0).Change(TimerData(1), TimerData(0))
        If MainWindow.DebugHUDEnabled = True Then
            TimerArray(2).Change(TimerData(1), TimerData(0))
            TimerArray(3).Change(TimerData(1), 1000)
        Else
            FPSLabel.Visible = False
            TimerArray(3).Dispose()
            ThreadCountLabel.Visible = False
            TimerArray(2).Dispose()
        End If
        Megaman.Dispose()
        Megaman2.Dispose()
        Using GameWindowGraphics As Graphics = CreateGraphics() 'This gets the screen resolution.
            ScreenDpiArray(0) = GameWindowGraphics.DpiX
            ScreenDpiArray(1) = GameWindowGraphics.DpiY
        End Using
        If ScreenDpiArray(0) >= 100 Then
            MegamanVelocityMultiplier = 2
        Else
            MegamanVelocityMultiplier = 1
        End If
        MegamanCollisionRectangleArray(0) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Left
        MegamanCollisionRectangleArray(1) = RectangleF.FromLTRB((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2, MegamanRectangle(0).Top, ((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2) + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Vertical
        MegamanCollisionRectangleArray(2) = RectangleF.FromLTRB(MegamanRectangle(0).Right, MegamanRectangle(0).Top, MegamanRectangle(0).Right + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Right
        MegamanCollisionRectangleArray(3) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Top + 1) 'Top
        MegamanCollisionRectangleArray(4) = RectangleF.FromLTRB(MegamanRectangle(0).Left, (MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2, MegamanRectangle(0).Left + MegamanRectangle(0).Width, ((MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2) + 1) 'Horizontal
        MegamanCollisionRectangleArray(5) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Bottom + 1) 'Bottom
        TimerArray(4).Change(TimerData(1), Timeout.Infinite)
    End Sub
    Friend Sub LoadPanels()
        Dim PanelList As List(Of Panel) = New List(Of Panel)(GameArea.Controls.OfType(Of Panel))
        ReDim GraphicsRectangleArray(PanelList.Count)
        ReDim GraphicsTextureArray(PanelList.Count)
        LoopIndexArray(2) = -1
        For Each PanelControl As Panel In PanelList
            LoopIndexArray(2) += 1
            GraphicsRectangleArray(LoopIndexArray(2)) = New Rectangle
            Select Case PanelControl.Tag
                Case "Ground"
                    CollisionRegionArray(0).Union(Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width, PanelControl.Top + PanelControl.Height))
                Case "Ladder"
                    CollisionRegionLadders.Union(Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width, PanelControl.Top + PanelControl.Height))
                Case "SlopeGroundRight"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Left, PanelControl.Bottom), New Point(PanelControl.Right, PanelControl.Bottom), New Point(PanelControl.Right, PanelControl.Top)}, New Byte() {0, 1, 129}))
                Case "SlopeGroundLeft"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Right, PanelControl.Bottom), New Point(PanelControl.Left, PanelControl.Bottom), New Point(PanelControl.Left, PanelControl.Top)}, New Byte() {0, 1, 129}))
                Case "SlopeCeilingRight"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Left, PanelControl.Top), New Point(PanelControl.Right, PanelControl.Top), New Point(PanelControl.Right, PanelControl.Bottom)}, New Byte() {0, 1, 129}))
                Case "SlopeCeilingLeft"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Right, PanelControl.Top), New Point(PanelControl.Left, PanelControl.Top), New Point(PanelControl.Left, PanelControl.Bottom)}, New Byte() {0, 1, 129}))
                Case Else
                    CollisionRegionArray(0).Union(Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width, PanelControl.Top + PanelControl.Height))
            End Select
            GraphicsRectangleArray(LoopIndexArray(2)) = Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width + 5, PanelControl.Top + PanelControl.Height + 5)
            Select Case PanelControl.AccessibleName
                Case "blMaze.bmp"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(0)
                Case "MazeBlock1.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(1)
                Case "MazeBlock2.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(2)
                Case "MazeCeilingBlockLayerA.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(3)
                Case "MazeCeilingBlockLayerB.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(4)
                Case "MazeFloorBlockLayerA.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(5)
                Case "MazeFloorBlockLayerB.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(6)
                Case "MazeFloorBlockSlopeRightA.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(7)
                Case "MazeFloorBlockSlopeRightB.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(8)
                Case "MazeFloorSlopeRight.png"
                    GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(9)
                Case "Color"
                    GraphicsTextureArray(LoopIndexArray(2)) = Nothing
                Case Else
                    GraphicsTextureArray(LoopIndexArray(2)) = Nothing
            End Select
            PanelControl.Dispose()
        Next
        CollisionRegionArray(1) = CollisionRegionArray(0).Clone()
        TimerArray(4).Dispose()
        StartRendering(1) = True
    End Sub
    Friend Sub UpdateFPS()
        If StartRendering(0) = True Then
            Try
                If LabelVisible(1) = True Then
                    FPSLabel.Text = "FPS: " & FPS
                Else
                    FPSLabel.Text = "Click to Turn On FPS Label"
                End If
            Catch ex As Exception
            End Try
            FPS = 0
        End If
    End Sub
    Private Sub UpdateThreadCount()
        If StartRendering(0) = True Then
            ThreadPool.GetAvailableThreads(ThreadInfoArray(0), ThreadInfoArray(1))
            Try
                If LabelVisible(0) = True Then
                    'ThreadCountLabel.Text = "Available Workers: " & ThreadInfoArray(0) & " Available IOs: " & ThreadInfoArray(1) & vbCrLf & "MegamanAnimationArray(1): " & MegamanAnimationArray(1) & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiArray(0): " & ScreenDpiArray(0) & " ScreenDpiArray(1): " & ScreenDpiArray(1) & " ScreenX: " & GameArea.Width & " ScreenY: " & GameArea.Height & vbCrLf & "MegamanX: " & MegamanRectangle(0).X & " MegamanY: " & MegamanRectangle(0).Y & vbCrLf & "MegamanWidth: " & MegamanRectangle(0).Width & " MegamanHeight: " & MegamanRectangle(0).Height & vbCrLf & "Megaman2X: " & MegamanRectangle(1).X & " Megaman2Y: " & MegamanRectangle(1).Y & vbCrLf & "MegamanLeft: " & MegamanLeft & vbCrLf & "RectTempX1: " & MegamanCollisionRectangleTempX.Height & " RectTempX2: " & MegamanCollisionRectangleTempX2.Height & " RectTempX3: " & MegamanCollisionRectangleTempX3.Height & vbCrLf & "RectTempY1: " & MegamanCollisionRectangleTempY.Height & " RectTempY2: " & MegamanCollisionRectangleTempY2.Height & " RectTempY3: " & MegamanCollisionRectangleTempY3.Height & vbCrLf & "XCollision: " & MegamanCollisionDetectionArray(0) & " YCollision: " & MegamanCollisionDetectionArray(1) & vbCrLf & "CollisionTempX: " & MegamanCollisionTempArray(1) & " CollisionTempY: " & MegamanCollisionTempArray(0) & vbCrLf & "XVelocity: " & MegamanVelocityArray(0) & " YVelocity: " & MegamanVelocityArray(1) & vbCrLf & "OnGround: " & MegamanOnArray(0) & vbCrLf & "OnFrame: " & MegamanOnArray(1) & vbCrLf & "Velocity Vector Magnitude: " & MegamanVelocityVector(1) & vbCrLf & "Velocity Vector Angle: " & MegamanVelocityVector(0) & vbCrLf & "30Angle: " & MegamanAngleArray(0) & vbCrLf & "45Angle: " & MegamanAngleArray(1)
                    ThreadCountLabel.Text = "MegamanAnimationArray(1): " & MegamanAnimationArray(1) & " MegamanAnimationFrame: " & MegamanAnimationFrame & vbCrLf & "ScreenDpiArray(0): " & ScreenDpiArray(0) & " ScreenDpiArray(1): " & ScreenDpiArray(1) & " ScreenX: " & GameArea.Width & " ScreenY: " & GameArea.Height & vbCrLf & "MegamanX: " & MegamanRectangle(0).X & " MegamanY: " & MegamanRectangle(0).Y & vbCrLf & "CollisionArray(0,1,2): " & MegamanCollisionArray(0).CompareTo(True) + 1 & MegamanCollisionArray(1).CompareTo(True) + 1 & MegamanCollisionArray(2).CompareTo(True) + 1 & vbCrLf & "CollisionArray(3,4,5): " & MegamanCollisionArray(3).CompareTo(True) + 1 & MegamanCollisionArray(4).CompareTo(True) + 1 & MegamanCollisionArray(5).CompareTo(True) + 1 & vbCrLf & "CollisionArray(6,7,8): " & MegamanCollisionArray(6).CompareTo(True) + 1 & MegamanCollisionArray(7).CompareTo(True) + 1 & MegamanCollisionArray(8).CompareTo(True) + 1 & vbCrLf & "XCollision: " & MegamanCollisionDetectionArray(0) & " YCollision: " & MegamanCollisionDetectionArray(1) & vbCrLf & "CollisionTempX: " & MegamanCollisionTempArray(1) & " CollisionTempY: " & MegamanCollisionTempArray(0) & vbCrLf & "XVelocity: " & MegamanVelocityArray(0) & " YVelocity: " & MegamanVelocityArray(1) & vbCrLf & "OnGround: " & MegamanOnArray(0) & vbCrLf & "OnFrame: " & MegamanOnArray(1) & vbCrLf & "Velocity Vector Angle: " & MegamanVelocityVector(0) & vbCrLf & "30Angle: " & MegamanAngleArray(0)
                Else
                    ThreadCountLabel.Text = "Click to Turn On Debug Label"
                End If
                ThreadCountLabel.Update()
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub GameWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If StartRendering(0) = True Then
            If Not MegamanAnimationArray(0) >= 4 Then
                Select Case e.KeyCode
                    Case Keys.Right
                        If ButtonHeld(0) = False Then
                            ButtonHeld(0) = True
                            If MegamanAnimationArray(0) < 2 Then
                                MegamanAnimationFrame = 1
                                If ButtonHeld(1) = False Then
                                    MegamanAnimationArray(0) = 1    'Run
                                Else
                                    MegamanAnimationArray(0) = 0    'Stand
                                End If
                            End If
                        End If
                    Case Keys.Left
                        If ButtonHeld(1) = False Then
                            ButtonHeld(1) = True
                            If MegamanAnimationArray(0) < 2 Then
                                MegamanAnimationFrame = 1
                                If ButtonHeld(0) = False Then
                                    MegamanAnimationArray(0) = 1    'Run
                                Else
                                    MegamanAnimationArray(0) = 0    'Stand
                                End If
                            End If
                        End If
                    Case Keys.Up
                        If MegamanOnArray(0) = True Then
                            MegamanVelocityArray(1) = -39.2
                            MegamanOnArray(0) = False
                            If MegamanAnimationArray(0) <> 2 Then
                                MegamanAnimationFrame = 1
                                MegamanAnimationArray(0) = 2    'Jump
                            End If
                        End If
                    Case Keys.Down

                End Select
            End If
        End If
    End Sub
    Private Sub GameWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If StartRendering(0) = True Then
            Select Case e.KeyCode   'Detects the released keys...
                Case Keys.Right 'If right was released...
                    ButtonHeld(0) = False
                Case Keys.Left  'If left was released...
                    ButtonHeld(1) = False
            End Select
            If MegamanAnimationArray(0) < 2 Then    'If the player isn't jumping...
                MegamanAnimationFrame = 1
                MegamanAnimationArray(0) = 0
            End If
        End If
    End Sub
    Private Sub GameWindow_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        If StartRendering(1) = True Then
            CustomBackgroundBuffer = CustomBackgroundBufferContext.Allocate(GameAreaGraphics(0), GameAreaRectangle)
            CustomGraphicsBuffer = CustomDoubleBuffer.Allocate(GameAreaGraphics(0), GameAreaRectangle)
            StartRendering(1) = False
            StartRendering(0) = True
        End If
        If StartRendering(0) = True Then
            CustomBackgroundBuffer.Graphics.FillRectangle(New TextureBrush(TextureArray(0)), GameAreaRectangle)
            'CustomBackgroundBuffer.Graphics.FillRegion(Brushes.Aqua, CollisionRegionArray(0))
            CustomBackgroundBuffer.Graphics.FillRegion(Brushes.Green, CollisionRegionLadders)
            For LoopIndexArray(3) = 0 To (GraphicsRectangleArray.Length - 1)
                If Not GraphicsTextureArray(LoopIndexArray(3)) Is Nothing Then
                    CustomBackgroundBuffer.Graphics.DrawImage(GraphicsTextureArray(LoopIndexArray(3)), GraphicsRectangleArray(LoopIndexArray(3))) '.GetBounds(GameAreaGraphics(0))) 'FillPath(TextureArray(GraphicsPathTextureArray(LoopIndexArray(3))), GraphicsPathLocationArray(LoopIndexArray(3)))
                End If
            Next
            CustomDoubleBuffer = CustomBackgroundBufferContext
            CustomGraphicsBuffer = CustomBackgroundBuffer
            If Not MegamanRectangleImage Is Nothing Then
                CustomGraphicsBuffer.Graphics.DrawImageUnscaled(MegamanRectangleImage, ((MegamanRectangle(0).X + (MegamanRectangle(0).Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiArray(0))) / 2), (MegamanRectangle(0).Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiArray(1)) - MegamanRectangle(0).Height)))
            End If
            If MainWindow.DebugBoundingBoxes = True Then
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Black, Rectangle.Ceiling(MegamanRectangle(0)))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Red, Rectangle.Ceiling(MegamanRectangle(1)))
                For LoopIndexArray(0) = 0 To 5
                    CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Ceiling(MegamanCollisionRectangleArray(LoopIndexArray(0))))
                    CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Ceiling(MegamanCollisionRectangleTempArray(LoopIndexArray(0))))
                Next
            End If
            CustomGraphicsBuffer.Render(GameAreaGraphics(0))
            Try
                FPS += 1
            Catch ex As Exception
                FPS = 0
            End Try
        End If
    End Sub
    Private Sub MegamanRectanglePhysics()
        If StartRendering(0) = True Then
            '******************************************
            'This section resets a few collision related variables to properly detect collision on the next frame.
            '******************************************
            MegamanOnArray(1) = False
            '******************************************
            'This section processes a few special collision scenarios dealing with edges of the window, gravity, and jumping animations.
            '******************************************
            If MegamanRectangle(0).Y >= (GameArea.Height - MegamanRectangle(0).Height) Then   'If the player is on the bottom of the window...
                MegamanOnArray(1) = True
                MegamanRectangle(0).Y = (GameArea.Height - MegamanRectangle(0).Height)
            End If
            If MegamanAnimationArray(0) <> 4 Then 'If not warping in...
                If MegamanOnArray(0) = False Then 'If the player is in midair...
                    MegamanRectangle(0).Y = MegamanRectangle(0).Y + MegamanVelocityArray(1)    'Calculates the new position
                    MegamanVelocityArray(1) = MegamanVelocityArray(1) + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
                ElseIf MegamanCollisionDetectionArray(1) > 0 Then
                    MegamanVelocityArray(1) = 0
                Else
                    MegamanVelocityArray(1) = 0
                    If MegamanAnimationArray(0) = 2 Then 'If the player is jumping...
                        MegamanAnimationFrame = 1
                        MegamanAnimationArray(0) = 3    'Land
                    End If
                End If
                If MegamanRectangle(0).Y < 0 Then 'If the player is out of bounds above the top...
                    MegamanRectangle(0).Y = 0
                End If
            End If
            '******************************************
            'This section sets the horizontal velocity, calculates the new position, and sets running animations based on input.
            '******************************************
            MegamanVelocityArray(0) = 0
            If ButtonHeld(0) = True AndAlso ButtonHeld(1) = False Then
                MegamanVelocityArray(0) = 9
                If MegamanAnimationArray(0) <> 2 Then
                    MegamanLeft = False
                End If
            End If
            If ButtonHeld(1) = True AndAlso ButtonHeld(0) = False Then
                MegamanVelocityArray(0) = -9
                If MegamanAnimationArray(0) <> 2 Then
                    MegamanLeft = True
                End If
            End If
            If ButtonHeld(0) = ButtonHeld(1) Then
                MegamanVelocityArray(0) = 0
            End If
            If MegamanAnimationArray(0) = 0 AndAlso MegamanVelocityArray(0) <> 0 Then
                MegamanAnimationArray(0) = 1
            End If
            MegamanRectangle(0).X = MegamanRectangle(0).X + (MegamanVelocityArray(0) * MegamanVelocityMultiplier)
            '******************************************
            'This section attempts to calculate a velocity vector.
            '******************************************
            MegamanVelocityVector(1) = Sqrt((MegamanVelocityArray(0)) ^ 2 + (MegamanVelocityArray(1)) ^ 2)
            MegamanVelocityVector(0) = 90 - (Atan2(MegamanVelocityArray(0), -MegamanVelocityArray(1)) * (180 / PI))
            If MegamanVelocityArray(0) = 0 AndAlso MegamanVelocityArray(1) = 0 Then
                MegamanVelocityVector(0) = -1
            ElseIf MegamanVelocityVector(0) < 0 Then
                MegamanVelocityVector(0) += 360
            End If
            If MegamanVelocityVector(0) <> -1 Then
                MegamanAngleArray(0) = (MegamanVelocityVector(0) \ 30) + 1
                MegamanAngleArray(1) = (MegamanVelocityVector(0) \ 45) + 1
            Else
                MegamanAngleArray(0) = -1
                MegamanAngleArray(1) = -1
            End If
            '******************************************
            'This section sets up the specialized collision detection rectangles to be at Megaman's new location.
            '******************************************
            MegamanCollisionRectangleArray(0) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Left
            MegamanCollisionRectangleArray(1) = RectangleF.FromLTRB((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2, MegamanRectangle(0).Top, ((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2) + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Vertical
            MegamanCollisionRectangleArray(2) = RectangleF.FromLTRB(MegamanRectangle(0).Right, MegamanRectangle(0).Top, MegamanRectangle(0).Right + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Right
            MegamanCollisionRectangleArray(3) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Top + 1) 'Top
            MegamanCollisionRectangleArray(4) = RectangleF.FromLTRB(MegamanRectangle(0).Left, (MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2, MegamanRectangle(0).Left + MegamanRectangle(0).Width, ((MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2) + 1) 'Horizontal
            MegamanCollisionRectangleArray(5) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Bottom + 1) 'Bottom
            '******************************************
            'This section processes the specialized collision rectangles to detect whether the player has moved inside any walls.
            '******************************************
            MegamanOnArray(0) = False
            MegamanCollisionDetectionArray(0) = 0
            MegamanCollisionDetectionArray(1) = 0
            Array.Clear(MegamanCollisionArray, 0, 9)
            If CollisionRegionArray(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Top) = True Then 'Test upper left
                MegamanCollisionDetectionArray(0) -= 1
                If ((MegamanAngleArray(0) >= 1 AndAlso MegamanAngleArray(0) <= 5) OrElse (MegamanAngleArray(0) >= 8 AndAlso MegamanAngleArray(0) <= 12)) Then
                    MegamanCollisionDetectionArray(1) += 1
                End If
                MegamanCollisionArray(0) = True
            End If
            If CollisionRegionArray(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Top) = True Then 'Test upper right
                MegamanCollisionDetectionArray(0) += 1
                If (MegamanAngleArray(0) >= 2 AndAlso MegamanAngleArray(0) <= 11) Then
                    MegamanCollisionDetectionArray(1) += 1
                End If
                MegamanCollisionArray(2) = True
            End If
            If CollisionRegionArray(1).IsVisible((MegamanRectangle(0).Right - ((3 / 4) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test lower middle left bottom for ground
                MegamanOnArray(0) = True
            End If
            If CollisionRegionArray(1).IsVisible((MegamanRectangle(0).Right - ((1 / 2) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test middle bottom for ground
                MegamanOnArray(0) = True
                MegamanCollisionDetectionArray(1) = 0
                MegamanCollisionArray(7) = True
            End If
            If CollisionRegionArray(1).IsVisible((MegamanRectangle(0).Right - ((1 / 4) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test lower middle right for ground
                MegamanOnArray(0) = True
            End If
            If CollisionRegionArray(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom) = True Then 'Test lower left
                If (MegamanAngleArray(0) >= 2 AndAlso MegamanAngleArray(0) <= 11) Then
                    MegamanCollisionDetectionArray(1) -= 1
                End If
                If ((MegamanAngleArray(0) >= 6 AndAlso MegamanAngleArray(0) <= 10) OrElse MegamanAngleArray(0) = -1) AndAlso MegamanCollisionArray(0) = False Then
                    MegamanOnArray(0) = True
                End If
                If (MegamanAngleArray(0) = 9 OrElse MegamanAngleArray(0) = 10) AndAlso MegamanOnArray(0) = False Then
                    MegamanCollisionDetectionArray(0) -= 1
                End If
                MegamanCollisionArray(6) = True
            End If
            If CollisionRegionArray(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Bottom) = True Then 'Test lower right
                If ((MegamanAngleArray(0) >= 1 AndAlso MegamanAngleArray(0) <= 5) OrElse (MegamanAngleArray(0) >= 8 AndAlso MegamanAngleArray(0) <= 12)) Then
                    MegamanCollisionDetectionArray(1) -= 1
                End If
                If ((MegamanAngleArray(0) = 1 OrElse (MegamanAngleArray(0) >= 9 AndAlso MegamanAngleArray(0) <= 12)) OrElse MegamanAngleArray(0) = -1) AndAlso MegamanCollisionArray(2) = False Then
                    MegamanOnArray(0) = True
                End If
                If (MegamanAngleArray(0) = 9 OrElse MegamanAngleArray(0) = 10) AndAlso MegamanOnArray(0) = False Then
                    MegamanCollisionDetectionArray(0) += 1
                End If
                MegamanCollisionArray(8) = True
            End If
            If CollisionRegionArray(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), MegamanRectangle(0).Top) = True Then 'Test middle top
                MegamanCollisionDetectionArray(1) += 1
                MegamanCollisionDetectionArray(0) = 0
                MegamanCollisionArray(1) = True
            End If
            If CollisionRegionArray(1).IsVisible(MegamanRectangle(0).Left, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle left
                MegamanCollisionDetectionArray(0) -= 1
                MegamanCollisionDetectionArray(1) = 0
                MegamanCollisionArray(3) = True
            End If
            If CollisionRegionArray(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test center
                MegamanHealth -= 1
                If MegamanHealth = 0 Then
                    MegamanDead = True
                End If
                MegamanCollisionArray(4) = True
            End If
            If CollisionRegionArray(1).IsVisible(MegamanRectangle(0).Right, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle right
                MegamanCollisionDetectionArray(0) += 1
                MegamanCollisionDetectionArray(1) = 0
                MegamanCollisionArray(5) = True
            End If
            '******************************************
            'This section creates a duplicate of the current terrain and intersects it with the collision rectangles to determine how far inside a wall the player moved
            'and then compares the collision rectangles to the character size to fix a few collision bugs.
            '******************************************
            For LoopIndexArray(1) = 0 To 2
                CollisionRegionArray(2) = CollisionRegionArray(1).Clone()
                CollisionRegionArray(2).Intersect(MegamanCollisionRectangleArray(LoopIndexArray(1)))
                MegamanCollisionRectangleTempArray(LoopIndexArray(1)) = CollisionRegionArray(2).GetBounds(GameAreaGraphics(1))
                If MegamanCollisionRectangleTempArray(LoopIndexArray(1)).Height > (MegamanRectangle(0).Height / 2) Then
                    MegamanCollisionRectangleTempArray(LoopIndexArray(1)).Height = 0
                End If
            Next
            '******************************************
            'This section calculates the new vertical character position to move outside of any walls.
            '******************************************
            MegamanCollisionTempArray(0) = Max(Max(MegamanCollisionRectangleTempArray(0).Height, MegamanCollisionRectangleTempArray(1).Height), MegamanCollisionRectangleTempArray(2).Height)
            MegamanRectangle(0).Y = MegamanRectangle(0).Y + (MegamanCollisionTempArray(0) * Sign(MegamanCollisionDetectionArray(1)))
            If MegamanCollisionDetectionArray(1) < 0 AndAlso MegamanCollisionTempArray(1) <> 0 Then
                MegamanOnArray(0) = True
            End If
            '******************************************
            'This section creates a duplicate of the current terrain and intersects it with the collision rectangles to determine how far inside a wall the player moved.
            'and then compares the collision rectangles to the character size to fix a few collision bugs.
            '******************************************
            For LoopIndexArray(1) = 3 To 5
                CollisionRegionArray(2) = CollisionRegionArray(1).Clone()
                CollisionRegionArray(2).Intersect(MegamanCollisionRectangleArray(3))
                MegamanCollisionRectangleTempArray(LoopIndexArray(1)) = CollisionRegionArray(2).GetBounds(GameAreaGraphics(1))
                If MegamanCollisionRectangleTempArray(LoopIndexArray(1)).Width > (MegamanRectangle(0).Width / 2) Then
                    MegamanCollisionRectangleTempArray(LoopIndexArray(1)).Width = 0
                End If
            Next
            '******************************************
            'This section handles a specialized out of window case.
            '******************************************
            If MegamanRectangle(0).Y >= (GameArea.Height - MegamanRectangle(0).Height) Then
                MegamanOnArray(1) = True
                MegamanRectangle(0).Y = (GameArea.Height - MegamanRectangle(0).Height)
            End If
            '******************************************
            'This section fixes a bug with jittery Y positions.
            '******************************************
            MegamanRectangle(0).Y = Round(MegamanRectangle(0).Y, 0, MidpointRounding.AwayFromZero)
            '******************************************
            'This section calculates the new horizontal character position to move outside of any walls.
            '******************************************
            MegamanCollisionTempArray(1) = Max(Max(MegamanCollisionRectangleTempArray(3).Width, MegamanCollisionRectangleTempArray(4).Width), MegamanCollisionRectangleTempArray(5).Width)
            MegamanRectangle(0).X = MegamanRectangle(0).X - (MegamanCollisionTempArray(1) * Sign(MegamanCollisionDetectionArray(0)))
            '******************************************
            'This section updates the collision rectangles again.
            '******************************************
            If MainWindow.DebugBoundingBoxes = True Then
                MegamanCollisionRectangleArray(0) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Left
                MegamanCollisionRectangleArray(1) = RectangleF.FromLTRB((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2, MegamanRectangle(0).Top, ((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2) + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Vertical
                MegamanCollisionRectangleArray(2) = RectangleF.FromLTRB(MegamanRectangle(0).Right, MegamanRectangle(0).Top, MegamanRectangle(0).Right + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Right
                MegamanCollisionRectangleArray(3) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Top + 1) 'Top
                MegamanCollisionRectangleArray(4) = RectangleF.FromLTRB(MegamanRectangle(0).Left, (MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2, MegamanRectangle(0).Left + MegamanRectangle(0).Width, ((MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2) + 1) 'Horizontal
                MegamanCollisionRectangleArray(5) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Bottom + 1) 'Bottom
            End If
            If MegamanDead = True Then
                GameOverYeah()
            End If
        End If
    End Sub
    Friend Sub AnimateMegamanRectangle()
        If StartRendering(0) = True Then
            MegamanAnimationArray(1) = MegamanAnimationArray(0) * 2
            If MegamanLeft = True Then
                MegamanAnimationArray(1) += 1
            End If
            Select Case MegamanAnimationArray(0)
                Case = 0    'Standing
                    Select Case MegamanAnimationFrame
                        Case Is <= (MegamanBlinkRate - 3)
                            MegamanRectangleImage = Image.FromFile(ResourcesPath & "Standing1.png")
                        Case (MegamanBlinkRate - 2), (MegamanBlinkRate)
                            MegamanRectangleImage = Image.FromFile(ResourcesPath & "Standing2.png")
                        Case Is = (MegamanBlinkRate - 1)
                            MegamanRectangleImage = Image.FromFile(ResourcesPath & "Standing3.png")
                    End Select
                    If MegamanAnimationFrame < MegamanBlinkRate Then
                        MegamanAnimationFrame += 1
                    Else
                        MegamanAnimationFrame = 1
                    End If
                Case = 1    'Running
                    MegamanRectangleImage = Image.FromFile(ResourcesPath & "Running" & MegamanAnimationFrame & ".png")
                    If MegamanAnimationFrame < 11 Then
                        MegamanAnimationFrame += 1
                    Else
                        MegamanAnimationFrame = 2 'The first frame is only for starting running
                    End If
                Case = 2    'Jumping
                    MegamanRectangleImage = Image.FromFile(ResourcesPath & "Jumping" & MegamanAnimationFrame & ".png")
                    If MegamanAnimationFrame < 5 Then
                        MegamanAnimationFrame += 1
                    End If
                Case = 3    'Landing
                    MegamanRectangleImage = Image.FromFile(ResourcesPath & "Landing" & MegamanAnimationFrame & ".png")
                    Select Case MegamanAnimationFrame
                        Case Is = 1
                            If ButtonHeld((MegamanAnimationArray(1) Mod 2)) = True Then
                                MegamanAnimationFrame = 3
                                MegamanAnimationArray(0) = 1
                            Else
                                MegamanAnimationFrame += 1
                            End If
                        Case Is = 2
                            MegamanAnimationFrame = 1
                            MegamanAnimationArray(0) = 0
                    End Select
                Case = 4    'Warping In
                    MegamanRectangleImage = Image.FromFile(ResourcesPath & "WarpIn" & MegamanAnimationFrame & ".png")
                    If MegamanAnimationFrame < 7 Then
                        MegamanAnimationFrame += 1
                    Else
                        If MegamanRectangle(0).Y < (GameArea.Height - MegamanRectangle(0).Height) Then   'If spawning in midair...
                            MegamanAnimationFrame = 5
                            MegamanAnimationArray(0) = 2    'Display falling animation
                        Else    'If spawning on the ground...
                            MegamanAnimationFrame = 1
                            MegamanAnimationArray(0) = 0    'Display standing animation
                        End If
                        TimerArray(1).Change(TimerData(1), TimerData(0))
                    End If
            End Select
            If (MegamanAnimationArray(1) Mod 2) = 1 Then 'If left...
                MegamanRectangleImage.RotateFlip(RotateFlipType.RotateNoneFlipX) 'Mirror graphic
            End If
            Try
                MegamanRectangle(1) = RectangleF.FromLTRB(((MegamanRectangle(0).X + (MegamanRectangle(0).Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiArray(0))) / 2), (MegamanRectangle(0).Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiArray(1)) - MegamanRectangle(0).Height)), (((MegamanRectangle(0).X + (MegamanRectangle(0).Width / 2)) - (((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiArray(0))) / 2)) + ((MegamanRectangleImage.Width / MegamanRectangleImage.HorizontalResolution) * ScreenDpiArray(0)), ((MegamanRectangle(0).Y - (((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiArray(1)) - MegamanRectangle(0).Height))) + ((MegamanRectangleImage.Height / MegamanRectangleImage.VerticalResolution) * ScreenDpiArray(1)))
            Catch ex As Exception
            End Try
            Refresh()
            'Update()
        End If
    End Sub
    Private Sub ResetEverything()

    End Sub
    Private Sub GameOverYeah()

    End Sub
    Private Sub GameWindow_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MainWindow.Close()
    End Sub
    Private Sub ThreadCountLabel_Click(sender As Object, e As EventArgs) Handles ThreadCountLabel.Click
        If LabelVisible(0) = True Then
            LabelVisible(0) = False
        Else
            LabelVisible(0) = True
        End If
    End Sub
    Private Sub FPSLabel_Click(sender As Object, e As EventArgs) Handles FPSLabel.Click
        If LabelVisible(1) = True Then
            LabelVisible(1) = False
        Else
            LabelVisible(1) = True
        End If
    End Sub
End Class