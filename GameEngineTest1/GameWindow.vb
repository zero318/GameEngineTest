﻿Imports System.Threading
Imports System.Math
Imports System.Drawing.Drawing2D
Imports System.IO
Public Class GameWindow
    Dim LoopIndexArray(4) As Integer 'Drawing = 0, Physics = 1, PanelArray = 2, PanelRender = 3, AddedPanels = 4
    Dim StartRendering() As Boolean = {False, False} 'Normal = 0 and Background = 1
    Dim FPS As Integer
    Dim GamePath As String = My.Application.Info.DirectoryPath
    Dim ResourcesPath As String = GamePath & "\Resources\"
    Dim LoadFromFile As Boolean = True 'True = LoadFromFile, False = LoadFromForm
    Dim StartingLevel As String = "DebugRoom"
    Dim Tileset As String
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
    Dim TimerArray() As Timer = {New Timer(New TimerCallback(AddressOf AnimateMegamanRectangle), vbNull, Timeout.Infinite, Timeout.Infinite), New Timer(New TimerCallback(AddressOf MegamanRectanglePhysics), vbNull, Timeout.Infinite, Timeout.Infinite), New Timer(New TimerCallback(AddressOf UpdateThreadCount), vbNull, Timeout.Infinite, Timeout.Infinite), New Timer(New TimerCallback(AddressOf UpdateFPS), vbNull, Timeout.Infinite, Timeout.Infinite)} 'Animation = 0, Physics = 1, DebugLabel = 2, and FPS = 3
    Dim GameAreaRectangle As Rectangle
    Dim CollisionRegionArray() As Region = {New Region, New Region, New Region}
    Dim CollisionRegionLadders() As Region = {New Region, New Region}
    Dim CollisionRegionDoors() As Region = {New Region, New Region}
    Dim CollisionRegionOneWay() As Region = {New Region, New Region, New Region}
    Dim GraphicsRectangleArray() As Rectangle
    Dim GraphicsTextureArray() As Image
    Dim TextureArray(-1) As Image
    Dim TextureBrushArray() As TextureBrush
    Dim SwapUpAndZ As Boolean = True ' False = Up is Jump and Space is Interact, True = Up is Interact and Space is Jump 
    Dim InputArray() As Keys = {Keys.Right, Keys.Left, Keys.Up, Keys.Down, Keys.Z, Keys.X}
    Dim ButtonHeld(InputArray.Length - 1) As Boolean '0 = Right, 1 = Left, 2 = Up, 3 = Down, and 4 = Space
    'Megaman specific variables start here
    Dim MegamanSpawnLocation As Integer = 3
    Dim MegamanBlinkRate As Integer = 15
    Dim MegamanRectangle() As RectangleF = {New RectangleF(50, 400, 100, 100), New RectangleF(50, 400, 100, 100)}
    Dim MegamanCollisionRectangleTempArray() As RectangleF = {New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0), New RectangleF(0, 0, 0, 0)}
    Dim MegamanRectangleImage As Image
    Dim MegamanCollisionRectangleArray(5) As RectangleF
    Dim MegamanCollisionArray(8) As Boolean 'First row: 0, 1, 2 ... Second row: 3, 4, 5 ... Third row: 6, 7, 8
    Dim MegamanCollisionArray2(8) As Integer 'First row: 0, 1, 2 ... Second row: 3, 4, 5 ... Third row: 6, 7, 8
    Dim MegamanCollisionDetectionArray(1) As Integer 'X = 0 and Y = 1
    Dim MegamanCollisionTempArray(1) As Integer 'Y = 0 and X = 1
    Dim MegamanLeft As Boolean
    Dim MegamanOnArray(4) As Boolean 'Ground = 0, Frame = 1, Ladder = 2, Door = 3, and OneWay = 4
    Dim VelocityMultiplier As Integer
    Dim MegamanVelocityArray(1) As Double 'X = 0 and Y = 1
    Dim MegamanAnimationArray() As Byte = {4, 0}
    Dim MegamanAnimationFrame As Byte = 1
    Dim MegamanVelocityVector(1) As Double 'Angle = 0 and Magnitude = 1
    Dim MegamanAngleArray(1) As SByte '30 = 0 and 45 = 1
    Dim MegamanHealth As Integer = 100
    Dim MegamanDead As Boolean
    Dim MegamanEnableGravity As Boolean = True
    'These variables control shooting
    Dim NewBullet As RectangleF
    Dim BulletImage As Image
    Dim BulletIndex(1) As Integer
    Dim BulletList As New List(Of RectangleF)
    Dim BulletLeftList As New List(Of Boolean)
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        'This section overrides how windows draws the screen so that it runs better for a game.
        If PaintSomegroundOnArray(0) = True Then
            MyBase.OnPaintBackground(e)
        End If
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        'This section overrides how windows draws the screen so that it runs better for a game.
        If PaintSomegroundOnArray(1) = True Then
            MyBase.OnPaint(e)
        End If
    End Sub
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'This section initializes the game area for the rest of the code.
        If SwapUpAndZ = False Then
            InputArray(2) = Keys.Z
            InputArray(4) = Keys.Up
        End If
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
        Megaman.Dispose()
        Megaman2.Dispose()
        Megaman3.Dispose()
        GameAreaRectangle = Rectangle.FromLTRB(GameArea.Left, GameArea.Top, GameArea.Left + GameArea.Width, GameArea.Top + GameArea.Height)
        GameAreaGraphics(0) = GameArea.CreateGraphics()
        GameAreaGraphics(1) = GameArea.CreateGraphics()
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
        Using GameWindowGraphics As Graphics = CreateGraphics() 'This gets the screen resolution.
            ScreenDpiArray(0) = GameWindowGraphics.DpiX
            ScreenDpiArray(1) = GameWindowGraphics.DpiY
        End Using
        If ScreenDpiArray(0) >= 100 Then
            VelocityMultiplier = 2
        Else
            VelocityMultiplier = 1
        End If
        'MainWindow.LevelName = StartingLevel
        LoadLevelMap()
    End Sub
    Friend Sub LoadLevelMap()
        'This section reads map data from the specified txt file in the game's resources.
        CollisionRegionArray(0).Exclude(GameAreaRectangle)
        CollisionRegionLadders(0).Exclude(GameAreaRectangle)
        CollisionRegionDoors(0).Exclude(GameAreaRectangle)
        CollisionRegionOneWay(0).Exclude(GameAreaRectangle)
        If LoadFromFile = True Then
            For Each FormPanel As Panel In New List(Of Panel)(GameArea.Controls.OfType(Of Panel))
                FormPanel.Dispose()
            Next
            Dim MapFileLine As String = ""
            Dim SplitStrings() As String
            Dim Delimitters() As Char = {"=", ","}
            Dim AddedPanels(0) As Panel
            Dim EndReached As Boolean
            LoopIndexArray(4) = -1
            Using MapFile As StreamReader = New StreamReader(ResourcesPath & MainWindow.LevelName & "MapData.txt")
                Do
                    ReDim SplitStrings(Nothing)
                    MapFileLine = MapFile.ReadLine()
                    SplitStrings = MapFileLine.Split(Delimitters, StringSplitOptions.None)
                    Select Case SplitStrings(0)
                        Case "Title"
                        Case "Tileset"
                        Case "LoadImage"
                            Array.Resize(TextureArray, TextureArray.Length + 1)
                            TextureArray(TextureArray.Length - 1) = Image.FromFile(ResourcesPath & SplitStrings(2))
                        Case "PlayerStart"
                            If MegamanSpawnLocation = 3 Then
                                Dim MegamanRectangle3 As Rectangle = Rectangle.FromLTRB(SplitStrings(1), SplitStrings(2), SplitStrings(1) + 100, SplitStrings(2) + 100)
                                MegamanRectangle(0) = Rectangle.FromLTRB(MegamanRectangle3.Left, MegamanRectangle3.Top, MegamanRectangle3.Left + MegamanRectangle3.Width, MegamanRectangle3.Top + MegamanRectangle3.Height)
                                MegamanRectangle(1) = Rectangle.FromLTRB(MegamanRectangle3.Left, MegamanRectangle3.Top, MegamanRectangle3.Left + MegamanRectangle3.Width, MegamanRectangle3.Top + MegamanRectangle3.Height)
                            End If
                        Case "CreateTerrain"
                            LoopIndexArray(4) += 1
                            Array.Resize(AddedPanels, LoopIndexArray(4) + 1)
                            AddedPanels(LoopIndexArray(4)) = New Panel
                            With AddedPanels(LoopIndexArray(4))
                                .Name = "Panel" & LoopIndexArray(4)
                                .Left = SplitStrings(1)
                                .Top = SplitStrings(2)
                                .Width = SplitStrings(3)
                                .Height = SplitStrings(4)
                                .Tag = SplitStrings(5)
                                .AccessibleName = SplitStrings(6)
                                .BackColor = Color.FromName(SplitStrings(7))
                            End With
                            GameArea.Controls.Add(AddedPanels(LoopIndexArray(4)))
                        Case "EndOfData"
                            EndReached = True
                        Case Else
                    End Select
                Loop Until (MapFileLine Is Nothing) OrElse (EndReached = True)
            End Using
        Else
            TextureArray = {Image.FromFile(ResourcesPath & "bkMaze.bmp"), Image.FromFile(ResourcesPath & "MazeBlock1.png"), Image.FromFile(ResourcesPath & "MazeBlock2.png"), Image.FromFile(ResourcesPath & "MazeCeilingBlockLayerA.png"), Image.FromFile(ResourcesPath & "MazeCeilingBlockLayerB.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockLayerA.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockLayerB.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockSlopeRightA.png"), Image.FromFile(ResourcesPath & "MazeFloorBlockSlopeRightB.png"), Image.FromFile(ResourcesPath & "MazeFloorSlopeRight.png")}
        End If
        MegamanCollisionRectangleArray(0) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Left
        MegamanCollisionRectangleArray(1) = RectangleF.FromLTRB((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2, MegamanRectangle(0).Top, ((MegamanRectangle(0).Left + MegamanRectangle(0).Right) / 2) + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Vertical
        MegamanCollisionRectangleArray(2) = RectangleF.FromLTRB(MegamanRectangle(0).Right, MegamanRectangle(0).Top, MegamanRectangle(0).Right + 1, MegamanRectangle(0).Top + MegamanRectangle(0).Height) 'Right
        MegamanCollisionRectangleArray(3) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Top, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Top + 1) 'Top
        MegamanCollisionRectangleArray(4) = RectangleF.FromLTRB(MegamanRectangle(0).Left, (MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2, MegamanRectangle(0).Left + MegamanRectangle(0).Width, ((MegamanRectangle(0).Top + MegamanRectangle(0).Bottom) / 2) + 1) 'Horizontal
        MegamanCollisionRectangleArray(5) = RectangleF.FromLTRB(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom, MegamanRectangle(0).Left + MegamanRectangle(0).Width, MegamanRectangle(0).Bottom + 1) 'Bottom
        LoadPanels()
    End Sub
    Friend Sub LoadPanels()
        'This section takes the data from the previous section and uses it to create an arbitrary number of level components.
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
                    CollisionRegionLadders(0).Union(Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width, PanelControl.Top + PanelControl.Height))
                Case "Door"
                    CollisionRegionDoors(0).Union(Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width, PanelControl.Top + PanelControl.Height))
                Case "OneWay"
                    CollisionRegionOneWay(0).Union(Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width, PanelControl.Top + PanelControl.Height))
                Case "SlopeGroundRight"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Left, PanelControl.Bottom), New Point(PanelControl.Right, PanelControl.Bottom), New Point(PanelControl.Right, PanelControl.Top)}, New Byte() {0, 1, 129}))
                Case "SlopeGroundLeft"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Right, PanelControl.Bottom), New Point(PanelControl.Left, PanelControl.Bottom), New Point(PanelControl.Left, PanelControl.Top)}, New Byte() {0, 1, 129}))
                Case "SlopeCeilingRight"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Left, PanelControl.Top), New Point(PanelControl.Right, PanelControl.Top), New Point(PanelControl.Right, PanelControl.Bottom)}, New Byte() {0, 1, 129}))
                Case "SlopeCeilingLeft"
                    CollisionRegionArray(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Right, PanelControl.Top), New Point(PanelControl.Left, PanelControl.Top), New Point(PanelControl.Left, PanelControl.Bottom)}, New Byte() {0, 1, 129}))
                Case "OneWaySlopeRight"
                    CollisionRegionOneWay(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Left, PanelControl.Bottom), New Point(PanelControl.Left, PanelControl.Bottom + 5), New Point(PanelControl.Right, PanelControl.Top + 5), New Point(PanelControl.Right, PanelControl.Top)}, New Byte() {0, 1, 1, 129}))
                Case "OneWaySlopeLeft"
                    CollisionRegionOneWay(0).Union(New GraphicsPath(New Point() {New Point(PanelControl.Right, PanelControl.Bottom), New Point(PanelControl.Right, PanelControl.Bottom + 5), New Point(PanelControl.Left, PanelControl.Top + 5), New Point(PanelControl.Left, PanelControl.Top)}, New Byte() {0, 1, 1, 129}))
                Case Else
                    CollisionRegionArray(0).Union(Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width, PanelControl.Top + PanelControl.Height))
            End Select
            GraphicsRectangleArray(LoopIndexArray(2)) = Rectangle.FromLTRB(PanelControl.Left, PanelControl.Top, PanelControl.Left + PanelControl.Width + 5, PanelControl.Top + PanelControl.Height + 5)
            If IsNumeric(PanelControl.AccessibleName) AndAlso PanelControl.AccessibleName <= (TextureArray.Length - 1) Then
                GraphicsTextureArray(LoopIndexArray(2)) = TextureArray(PanelControl.AccessibleName)
            Else
                GraphicsTextureArray(LoopIndexArray(2)) = Nothing
            End If
            PanelControl.Dispose()
        Next
        CollisionRegionArray(1) = CollisionRegionArray(0).Clone()
        CollisionRegionLadders(1) = CollisionRegionLadders(0).Clone()
        CollisionRegionDoors(1) = CollisionRegionDoors(0).Clone()
        CollisionRegionOneWay(1) = CollisionRegionOneWay(0).Clone()
        StartRendering(1) = True
    End Sub
    Friend Sub UpdateFPS()
        'This section is used only during debugging.
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
        'This section is used only during debugging. I tend to use it as an in-game watch window.
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
        'This section reads when the player starts input from the keyboard.
        If StartRendering(0) = True Then
            If Not MegamanAnimationArray(0) = 4 Then
                Select Case e.KeyCode
                    Case InputArray(0) 'Right
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
                    Case InputArray(1) 'Left
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
                    Case InputArray(2) 'Up
                        If MegamanOnArray(3) = True Then
                            LoadNewArea()
                        ElseIf MegamanOnArray(2) = True Then
                            MegamanOnArray(0) = False
                            MegamanVelocityArray(1) = -25
                            MegamanEnableGravity = False
                            If ButtonHeld(2) = False Then
                                ButtonHeld(2) = True
                                MegamanAnimationFrame = 1
                                MegamanAnimationArray(0) = 5
                            End If
                            'ElseIf MegamanOnArray(0) = True OrElse MegamanEnableGravity = False Then
                            '    MegamanEnableGravity = True
                            '    MegamanVelocityArray(1) = -39.2
                            '    MegamanOnArray(0) = False
                            '    If MegamanAnimationArray(0) <> 2 Then
                            '        MegamanAnimationFrame = 1
                            '        MegamanAnimationArray(0) = 2    'Jump
                            '    End If
                        End If
                    Case InputArray(3) 'Down
                        If MegamanOnArray(2) = True Then
                            MegamanVelocityArray(1) = 25
                            MegamanEnableGravity = False
                        End If
                    Case InputArray(4) 'Z
                        If MegamanOnArray(0) = True OrElse MegamanEnableGravity = False Then
                            MegamanEnableGravity = True
                            MegamanVelocityArray(1) = -39.2
                            MegamanOnArray(0) = False
                            If MegamanAnimationArray(0) <> 2 Then
                                MegamanAnimationFrame = 1
                                MegamanAnimationArray(0) = 2    'Jump
                            End If
                        End If
                    Case InputArray(5) 'X
                        If ButtonHeld(5) = False Then
                            ButtonHeld(5) = True
                            NewBullet = RectangleF.FromLTRB((MegamanRectangle(0).Left + (MegamanRectangle(0).Width / 2)) + 25, (MegamanRectangle(0).Top + (MegamanRectangle(0).Height / 2)) + 12.5, (MegamanRectangle(0).Left + (MegamanRectangle(0).Width / 2)) - 25, (MegamanRectangle(0).Top + (MegamanRectangle(0).Height / 2)) - 12.5)
                            BulletList.Add(NewBullet)
                            BulletLeftList.Add(MegamanLeft)
                        End If
                End Select
            End If
        End If
    End Sub
    Private Sub GameWindow_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        'This section is currently unused.
    End Sub
    Private Sub GameWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        'This section detects when the player ends input from the keyboard.
        If StartRendering(0) = True Then
            Select Case e.KeyCode   'Detects the released keys...
                Case InputArray(0) 'Right
                    ButtonHeld(0) = False
                    If MegamanAnimationArray(0) < 2 Then    'If the player isn't jumping...
                        MegamanAnimationFrame = 1
                        MegamanAnimationArray(0) = 0
                    End If
                Case InputArray(1)  'Left
                    ButtonHeld(1) = False
                    If MegamanAnimationArray(0) < 2 Then    'If the player isn't jumping...
                        MegamanAnimationFrame = 1
                        MegamanAnimationArray(0) = 0
                    End If
                Case InputArray(2) 'Up
                    ButtonHeld(2) = False
                    If MegamanEnableGravity = False Then
                        MegamanVelocityArray(1) = 0
                    End If
                Case InputArray(3) 'Down
                    ButtonHeld(3) = False
                    If MegamanEnableGravity = False Then
                        MegamanVelocityArray(1) = 0
                    End If
                Case InputArray(4) 'Z
                    ButtonHeld(4) = False
                Case InputArray(5) 'X
                    ButtonHeld(5) = False
            End Select
        End If
    End Sub
    Private Sub GameWindow_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        'This section handles rendering of graphics.
        If StartRendering(1) = True Then
            CustomBackgroundBuffer = CustomBackgroundBufferContext.Allocate(GameAreaGraphics(0), GameAreaRectangle)
            CustomGraphicsBuffer = CustomDoubleBuffer.Allocate(GameAreaGraphics(0), GameAreaRectangle)
            StartRendering(1) = False
            StartRendering(0) = True
        End If
        If StartRendering(0) = True Then
            CustomBackgroundBuffer.Graphics.FillRectangle(New TextureBrush(TextureArray(0)), GameAreaRectangle)
            'CustomBackgroundBuffer.Graphics.FillRegion(Brushes.Aqua, CollisionRegionArray(0))
            CustomBackgroundBuffer.Graphics.FillRegion(Brushes.Green, CollisionRegionLadders(0))
            CustomBackgroundBuffer.Graphics.FillRegion(Brushes.Orange, CollisionRegionDoors(0))
            CustomBackgroundBuffer.Graphics.FillRegion(Brushes.Red, CollisionRegionOneWay(0))
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
            If BulletList.Count > 0 Then
                For Each Bullet In BulletList
                    BulletIndex(1) = BulletList.IndexOf(Bullet)
                    BulletImage = Image.FromFile(ResourcesPath & "Shot1.png")
                    If BulletLeftList.Item(BulletIndex(1)) = False Then
                        CustomGraphicsBuffer.Graphics.DrawImageUnscaled(BulletImage, ((Bullet.X + (Bullet.Width / 2)) - (((BulletImage.Width / BulletImage.HorizontalResolution) * ScreenDpiArray(0))) / 2), (Bullet.Y - (((BulletImage.Height / BulletImage.VerticalResolution) * ScreenDpiArray(1)) - Bullet.Height)))
                    Else
                        BulletImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
                        CustomGraphicsBuffer.Graphics.DrawImageUnscaled(BulletImage, ((Bullet.X + (Bullet.Width / 2)) - (((BulletImage.Width / BulletImage.HorizontalResolution) * ScreenDpiArray(0))) / 2), (Bullet.Y - (((BulletImage.Height / BulletImage.VerticalResolution) * ScreenDpiArray(1)) - Bullet.Height)))
                    End If
                Next
            End If
            If MainWindow.DebugBoundingBoxes = True Then
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Black, Rectangle.Ceiling(MegamanRectangle(0)))
                CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Red, Rectangle.Ceiling(MegamanRectangle(1)))
                For LoopIndexArray(0) = 0 To 5
                    CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Ceiling(MegamanCollisionRectangleArray(LoopIndexArray(0))))
                    CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Lime, Rectangle.Ceiling(MegamanCollisionRectangleTempArray(LoopIndexArray(0))))
                Next
                For Each Bullet In BulletList
                    CustomGraphicsBuffer.Graphics.DrawRectangle(Pens.Orange, Rectangle.Ceiling(RectangleF.FromLTRB(Bullet.Left, Bullet.Top, Bullet.Right, Bullet.Bottom)))
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
        'This extremely large section is the main game loop for handling physics/collision/entity management.
        If StartRendering(0) = True Then
            '******************************************
            'Collision is handled in this order:
            '1. Edges of the window
            '2. Gravity
            '3. Intersect with ordinary ground and slopes
            '4. Intersect with ladders
            '5. Intersect with doors
            '6. Bullets
            '******************************************
            '******************************************
            'This section resets a few collision related variables to properly detect collision on the next frame.
            '******************************************
            MegamanOnArray(1) = False
            '******************************************
            'This section processes a few special collision scenarios dealing with edges of the window, gravity, and jumping animations.
            '******************************************
            If MegamanRectangle(0).Y >= (GameArea.Height - MegamanRectangle(0).Height) Then   'If the player is on the bottom of the window...
                MegamanOnArray(1) = True
                MegamanEnableGravity = True
                MegamanRectangle(0).Y = (GameArea.Height - MegamanRectangle(0).Height)
            End If
            If MegamanAnimationArray(0) <> 4 Then 'If not warping in...
                If MegamanOnArray(0) = False Then 'If the player is in midair...
                    MegamanRectangle(0).Y = MegamanRectangle(0).Y + MegamanVelocityArray(1)    'Calculates the new position
                    If MegamanEnableGravity = True Then
                        MegamanVelocityArray(1) = MegamanVelocityArray(1) + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
                    End If
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
            MegamanRectangle(0).X = MegamanRectangle(0).X + (MegamanVelocityArray(0) * VelocityMultiplier)
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
            'This section does some weird stuff involving one-way platfroms that I don't really understand.
            'I copied some of the regular collision code, renamed some variables, and it just sort of worked.
            'I have no idea why this makes collision work despite not measuring with rectangles.
            '******************************************
            If MegamanVelocityArray(1) >= 0 Then
                If CollisionRegionOneWay(1).IsVisible((MegamanRectangle(0).Right - ((3 / 4) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test lower middle left bottom for ground
                    MegamanOnArray(0) = True
                End If
                If CollisionRegionOneWay(1).IsVisible((MegamanRectangle(0).Right - ((1 / 2) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test middle bottom for ground
                    MegamanOnArray(0) = True
                    MegamanCollisionDetectionArray(1) = 0
                    MegamanCollisionArray(7) = True
                End If
                If CollisionRegionOneWay(1).IsVisible((MegamanRectangle(0).Right - ((1 / 4) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test lower middle right for ground
                    MegamanOnArray(0) = True
                End If
                If CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom) = True Then 'Test lower left
                    If (MegamanAngleArray(0) >= 2 AndAlso MegamanAngleArray(0) <= 11) Then
                        MegamanCollisionDetectionArray(1) -= 1
                    End If
                    If ((MegamanAngleArray(0) >= 6 AndAlso MegamanAngleArray(0) <= 10) OrElse MegamanAngleArray(0) = -1) AndAlso MegamanCollisionArray(0) = False Then
                        MegamanOnArray(0) = True
                    End If
                    MegamanCollisionArray(6) = True
                End If
                If CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Bottom) = True Then 'Test lower right
                    If ((MegamanAngleArray(0) >= 1 AndAlso MegamanAngleArray(0) <= 5) OrElse (MegamanAngleArray(0) >= 8 AndAlso MegamanAngleArray(0) <= 12)) Then
                        MegamanCollisionDetectionArray(1) -= 1
                    End If
                    If ((MegamanAngleArray(0) = 1 OrElse (MegamanAngleArray(0) >= 9 AndAlso MegamanAngleArray(0) <= 12)) OrElse MegamanAngleArray(0) = -1) AndAlso MegamanCollisionArray(2) = False Then
                        MegamanOnArray(0) = True
                    End If
                    MegamanCollisionArray(8) = True
                End If
            End If
            '******************************************
            'This section processes the specialized collision rectangles to detect whether the player has moved inside ladders, doors, or one-way platforms.
            '******************************************
            Array.Clear(MegamanCollisionArray2, 0, 9)
            If CollisionRegionDoors(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Top) = True Then 'Test upper left
                MegamanCollisionArray2(0) = 2
            ElseIf CollisionRegionLadders(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Top) = True Then 'Test upper left
                MegamanCollisionArray2(0) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Top) = True Then 'Test upper left
                MegamanCollisionArray2(0) = 3
            End If
            If CollisionRegionDoors(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Top) = True Then 'Test upper right
                MegamanCollisionArray2(2) = 2
            ElseIf CollisionRegionLadders(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Top) = True Then 'Test upper right
                MegamanCollisionArray2(2) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Top) = True Then 'Test upper right
                MegamanCollisionArray2(2) = 3
            End If
            If CollisionRegionDoors(1).IsVisible((MegamanRectangle(0).Right - ((1 / 2) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test middle bottom for ground
                MegamanCollisionArray2(7) = 2
            ElseIf CollisionRegionLadders(1).IsVisible((MegamanRectangle(0).Right - ((1 / 2) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test middle bottom for ground
                MegamanCollisionArray2(7) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible((MegamanRectangle(0).Right - ((1 / 2) * MegamanRectangle(0).Width)), MegamanRectangle(0).Bottom) = True Then 'Test middle bottom for ground
                MegamanCollisionArray2(7) = 3
            End If
            If CollisionRegionDoors(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom) = True Then 'Test lower left
                MegamanCollisionArray2(6) = 2
            ElseIf CollisionRegionLadders(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom) = True Then 'Test lower left
                MegamanCollisionArray2(6) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Left, MegamanRectangle(0).Bottom) = True Then 'Test lower left
                MegamanCollisionArray2(6) = 3
            End If
            If CollisionRegionDoors(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Bottom) = True Then 'Test lower right
                MegamanCollisionArray2(8) = 2
            ElseIf CollisionRegionLadders(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Bottom) = True Then 'Test lower right
                MegamanCollisionArray2(8) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Right, MegamanRectangle(0).Bottom) = True Then 'Test lower right
                MegamanCollisionArray2(8) = 3
            End If
            If CollisionRegionDoors(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), MegamanRectangle(0).Top) = True Then 'Test middle top
                MegamanCollisionArray2(1) = 2
            ElseIf CollisionRegionLadders(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), MegamanRectangle(0).Top) = True Then 'Test middle top
                MegamanCollisionArray2(1) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), MegamanRectangle(0).Top) = True Then 'Test middle top
                MegamanCollisionArray2(1) = 3
            End If
            If CollisionRegionDoors(1).IsVisible(MegamanRectangle(0).Left, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle left
                MegamanCollisionArray2(3) = 2
            ElseIf CollisionRegionLadders(1).IsVisible(MegamanRectangle(0).Left, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle left
                MegamanCollisionArray2(3) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Left, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle left
                MegamanCollisionArray2(3) = 3
            End If
            If CollisionRegionDoors(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test center
                MegamanCollisionArray2(4) = 2
            ElseIf CollisionRegionLadders(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test center
                MegamanCollisionArray2(4) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible((MegamanRectangle(0).Right - (MegamanRectangle(0).Width / 2)), (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test center
                MegamanCollisionArray2(4) = 3
            End If
            If CollisionRegionDoors(1).IsVisible(MegamanRectangle(0).Right, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle right
                MegamanCollisionArray2(5) = 2
            ElseIf CollisionRegionLadders(1).IsVisible(MegamanRectangle(0).Right, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle right
                MegamanCollisionArray2(5) = 1
            ElseIf CollisionRegionOneWay(1).IsVisible(MegamanRectangle(0).Right, (MegamanRectangle(0).Bottom - (MegamanRectangle(0).Height / 2))) = True Then 'Test middle right
                MegamanCollisionArray2(5) = 3
            End If
            '******************************************
            'This section handles special physics for ladders, doors, and one-way platforms.
            '******************************************
            If Not (MegamanOnArray(2) = True AndAlso (MegamanCollisionArray2(1) = 3 OrElse MegamanCollisionArray2(7) = 3) AndAlso (MegamanCollisionArray2(3) = 3 OrElse MegamanCollisionArray2(5) = 3)) Then
                MegamanOnArray(2) = False
            End If
            MegamanOnArray(3) = False
            If MegamanCollisionArray2(1) = 2 AndAlso (MegamanCollisionArray2(3) = 2 OrElse MegamanCollisionArray2(5) = 2) AndAlso MegamanOnArray(0) = True Then
                MegamanOnArray(3) = True
            ElseIf (MegamanCollisionArray2(1) = 1 OrElse MegamanCollisionArray2(7) = 1) AndAlso (MegamanCollisionArray2(3) = 1 OrElse MegamanCollisionArray2(5) = 1) Then
                MegamanOnArray(2) = True
                MegamanEnableGravity = False
            Else
                MegamanEnableGravity = True
            End If
            If (MegamanOnArray(0) = True OrElse MegamanOnArray(2) = False) AndAlso MegamanAnimationArray(0) = 5 Then
                If MegamanOnArray(0) = True Then
                    MegamanAnimationArray(0) = 0
                    MegamanAnimationFrame = 1
                Else
                    MegamanAnimationArray(0) = 2
                    MegamanAnimationFrame = 5
                End If
            End If
            '******************************************
            'Bullet Collision
            '******************************************
            For Each Bullet In BulletList.ToList()
                BulletIndex(0) = BulletList.IndexOf(Bullet)
                If BulletLeftList.Item(BulletIndex(0)) = False Then
                    BulletList.Item(BulletIndex(0)) = RectangleF.FromLTRB(Bullet.Left + (18 * VelocityMultiplier), Bullet.Top, Bullet.Right + (18 * VelocityMultiplier), Bullet.Bottom)
                Else
                    BulletList.Item(BulletIndex(0)) = RectangleF.FromLTRB(Bullet.Left - (18 * VelocityMultiplier), Bullet.Top, Bullet.Right - (18 * VelocityMultiplier), Bullet.Bottom)
                End If
                If (CollisionRegionArray(1).IsVisible(Bullet.Left, Bullet.Top) = True) OrElse (CollisionRegionArray(1).IsVisible(Bullet.Left, Bullet.Bottom)) OrElse (CollisionRegionArray(1).IsVisible(Bullet.Right, Bullet.Top)) OrElse (CollisionRegionArray(1).IsVisible(Bullet.Right, Bullet.Bottom)) Then
                    BulletLeftList.RemoveAt(BulletIndex(0))
                    BulletList.RemoveAt(BulletIndex(0))
                End If
            Next
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
        'This section is the main game loop for handling animations and player states.
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
                Case = 5    'Climbing
                    MegamanRectangleImage = Image.FromFile(ResourcesPath & "Climbing" & MegamanAnimationFrame & ".png")
                    If MegamanVelocityArray(0) <> 0 OrElse MegamanVelocityArray(1) <> 0 Then
                        If MegamanAnimationFrame < 5 Then
                            MegamanAnimationFrame += 1
                        ElseIf MegamanAnimationFrame = 5 Then
                            MegamanAnimationFrame = 2
                        ElseIf MegamanAnimationFrame = 6 Then
                            MegamanAnimationFrame = 7
                        Else
                            MegamanAnimationArray(0) = 0
                            MegamanAnimationFrame = 1
                        End If
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
        'Currently unused
    End Sub
    Private Sub GameOverYeah()
        'Currently unused
    End Sub
    Private Sub LoadNewArea()
        'This section initiates loading a new area.
        'MessageBox.Show("Loading new area!")
        StartRendering(1) = False
        StartRendering(0) = False
        CollisionRegionArray(0).Exclude(GameAreaRectangle)
        CollisionRegionLadders(0).Exclude(GameAreaRectangle)
        CollisionRegionDoors(0).Exclude(GameAreaRectangle)
        CollisionRegionOneWay(0).Exclude(GameAreaRectangle)
        Select Case MainWindow.LevelName
            Case "DebugRoom"
                MainWindow.LevelName = "DebugRoom2"
            Case "DebugRoom2"
                MainWindow.GameExit = True
        End Select
        Dispose()
    End Sub
    Private Sub GameWindow_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'This section closes the launcher when the game is closed.
        MainWindow.GameExit = True
    End Sub
    Private Sub ThreadCountLabel_Click(sender As Object, e As EventArgs) Handles ThreadCountLabel.Click
        'This is only used during debugging
        If LabelVisible(0) = True Then
            LabelVisible(0) = False
        Else
            LabelVisible(0) = True
        End If
    End Sub
    Private Sub FPSLabel_Click(sender As Object, e As EventArgs) Handles FPSLabel.Click
        'This is only used during debugging
        If LabelVisible(1) = True Then
            LabelVisible(1) = False
        Else
            LabelVisible(1) = True
        End If
    End Sub
End Class