Imports System.Threading
Public Class GameWindow
    Dim Test1 As Integer
    Dim Test2 As Integer
    Dim TimerInterval As Integer = 100
    Dim TimerStartDelay As Integer = 1000
    Dim ThreadCountTimerCall As New TimerCallback(AddressOf UpdateThreadCount)
    Dim ThreadCountTimer As New Timer(ThreadCountTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim RightHeld As Boolean
    Dim LeftHeld As Boolean
    Dim MegamanLeft As Boolean
    Dim MegamanXVelocity As Double
    Dim MegamanYVelocity As Double
    Dim MegamanAnimation As Byte
    Dim MegamanAnimationFrame As Byte
    Dim MegamanAnimationTimerCall As New TimerCallback(AddressOf AnimateMegaman)
    Dim MegamanAnimationTimer As New Timer(MegamanAnimationTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Dim MegamanMegamanPhysicsTimerCall As New TimerCallback(AddressOf MoveYoCrap)
    Dim MegamanPhysicsTimer As New Timer(MegamanMegamanPhysicsTimerCall, vbNull, Timeout.Infinite, Timeout.Infinite)
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MegamanPhysicsTimer.Change(TimerStartDelay, TimerInterval)
        ThreadCountTimer.Change(TimerStartDelay, TimerInterval)
        MegamanAnimationTimer.Change(TimerStartDelay, TimerInterval)
    End Sub
    Private Sub MoveYoCrap()
        If MegamanXVelocity <> 0 Then  'If horizontal motion is not 0...
            If MegamanXVelocity > 0 AndAlso Megaman.Left < GameArea.Width Then   'If the player is not out of bounds to the right...
                Megaman.Left = Megaman.Left + MegamanXVelocity
                If Megaman.Left > GameArea.Width Then   'If the player was moved out of bounds to the right...
                    Megaman.Left = GameArea.Width
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 0    'Stand right
                End If
            ElseIf MegamanXVelocity < 0 AndAlso Megaman.Left > 0 Then  'If the player is not out of bounds to the left...
                Megaman.Left = Megaman.Left + MegamanXVelocity
                If Megaman.Left < 0 Then    'If the player was moved out of bounds to the left...
                    Megaman.Left = 0
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 1    'Stand left
                End If
            End If
        End If
        If MegamanAnimation <> 6 AndAlso MegamanAnimation <> 9 Then 'If vertical motion is not 0...
            Megaman.Top = Megaman.Top + MegamanYVelocity    'Calculates the new position
            MegamanYVelocity = MegamanYVelocity + 9.8   'Applies gravity to the vertical velocity. 9.8m/s is actually the acceleration caused by the gravity of the earth. PHYSICS! :D
            If Megaman.Top > (GameArea.Height - Megaman.Height) Then   'If the player is out of bounds under the bottom...
                Megaman.Top = (GameArea.Height - Megaman.Height)
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
            ElseIf Megaman.Top < 0 Then 'If the player is out of bounds above the top...
                Megaman.Top = 0
            End If
        End If
    End Sub
    Private Sub UpdateThreadCount()
        Threading.ThreadPool.GetAvailableThreads(Test1, Test2)
        ThreadCountLabel.Text = Test1 & " " & Test2
    End Sub
    Private Sub AnimateMegaman()
        Select Case MegamanAnimation
            Case = 0    'Standing Right
                Megaman.Image = My.Resources.ResourceManager.GetObject("Standing" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 3 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 1    'Standing Left
                Megaman.Image = My.Resources.ResourceManager.GetObject("Standing" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                Megaman.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 3 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 2    'Running Right
                Megaman.Image = My.Resources.ResourceManager.GetObject("Running" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 11 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 3    'Running Left
                Megaman.Image = My.Resources.ResourceManager.GetObject("Running" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                Megaman.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 11 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                End If
            Case = 4    'Jumping Right
                Megaman.Image = My.Resources.ResourceManager.GetObject("Jumping" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 5 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    'MegamanAnimationFrame = 1   'Removing this prevents the animation looping and causing the arms to flail around.
                End If
            Case = 5    'Jumping Left
                Megaman.Image = My.Resources.ResourceManager.GetObject("Jumping" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                Megaman.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 5 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    'MegamanAnimationFrame = 1   'Removing this prevents the animation looping and causing the arms to flail around.
                End If
            Case = 6    'Warping In Right
                Megaman.Image = My.Resources.ResourceManager.GetObject("WarpIn" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    'tmrPhysics.Enabled = True   'Starts the physics engine
                    'Beep() 'This was used for debugging to provide feedback when the physics engine started. Then I decided I liked it.
                    If Megaman.Top > (GameArea.Height - Megaman.Height) Then   'If spawning in midair...
                        MegamanAnimationFrame = 5
                        MegamanAnimation = 4    'Jump right
                    Else    'If spawning on the ground...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 0    'Stand right
                    End If
                End If
            Case = 7    'Landing Right  'These were going to be used when landing after a jump, but didn't end up looking very good and caused weird glitches.
                Megaman.Image = My.Resources.ResourceManager.GetObject("Jumping" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 0    'Stand right
                End If
            Case = 8    'Landing Left   'These were going to be used when landing after a jump, but didn't end up looking very good and caused weird glitches.
                Megaman.Image = My.Resources.ResourceManager.GetObject("Jumping" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                Megaman.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    MegamanAnimationFrame = 1
                    MegamanAnimation = 1    'Stand left
                End If
            Case = 9    'Warping In Left
                Megaman.Image = My.Resources.ResourceManager.GetObject("WarpIn" & MegamanAnimationFrame)   'This dyamically reads a variable and parses its value into the resource call. This saves time by skipping several false case checks on higer numbered frames.
                'MegamanTestBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipX)
                If MegamanAnimationFrame < 7 Then   'Increments and resets the frame counter.
                    MegamanAnimationFrame += 1
                Else
                    'tmrPhysics.Enabled = True   'Starts the physics engine
                    'Beep() 'This was used for debugging to provide feedback when the physics engine started. Then I decided I liked it.
                    If Megaman.Top > (GameArea.Height - Megaman.Height) Then   'If spawning in midair...
                        MegamanAnimationFrame = 5
                        MegamanAnimation = 5    'Jump left
                    Else    'If spawning on the ground...
                        MegamanAnimationFrame = 1
                        MegamanAnimation = 1    'Run left
                    End If
                End If
        End Select
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
                If Megaman.Top = (GameArea.Height - Megaman.Height) Then   'If the player is on the ground...
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
    Private Sub GameWindow_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

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
    Private Sub GameWindow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles MyBase.PreviewKeyDown

    End Sub
End Class