﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GameWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Megaman = New System.Windows.Forms.PictureBox()
        Me.ThreadCountLabel = New System.Windows.Forms.Label()
        Me.GameArea = New System.Windows.Forms.Panel()
        Me.SlopePanelTest1 = New System.Windows.Forms.Panel()
        Me.CollisionTestPanelLadder = New System.Windows.Forms.Panel()
        Me.CollisionTestPanel2 = New System.Windows.Forms.Panel()
        Me.CollisionTestPanel3 = New System.Windows.Forms.Panel()
        Me.CollisionTestPanel1 = New System.Windows.Forms.Panel()
        Me.FPSLabel = New System.Windows.Forms.Label()
        Me.Megaman3 = New System.Windows.Forms.PictureBox()
        Me.Megaman2 = New System.Windows.Forms.PictureBox()
        CType(Me.Megaman, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GameArea.SuspendLayout()
        CType(Me.Megaman3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Megaman2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Megaman
        '
        Me.Megaman.BackColor = System.Drawing.Color.Transparent
        Me.Megaman.Location = New System.Drawing.Point(668, 514)
        Me.Megaman.Margin = New System.Windows.Forms.Padding(2)
        Me.Megaman.Name = "Megaman"
        Me.Megaman.Size = New System.Drawing.Size(100, 100)
        Me.Megaman.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Megaman.TabIndex = 0
        Me.Megaman.TabStop = False
        '
        'ThreadCountLabel
        '
        Me.ThreadCountLabel.AutoSize = True
        Me.ThreadCountLabel.BackColor = System.Drawing.Color.Transparent
        Me.ThreadCountLabel.Location = New System.Drawing.Point(12, 10)
        Me.ThreadCountLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ThreadCountLabel.Name = "ThreadCountLabel"
        Me.ThreadCountLabel.Size = New System.Drawing.Size(102, 32)
        Me.ThreadCountLabel.TabIndex = 1
        Me.ThreadCountLabel.Text = "Label1"
        '
        'GameArea
        '
        Me.GameArea.BackColor = System.Drawing.Color.Transparent
        Me.GameArea.Controls.Add(Me.SlopePanelTest1)
        Me.GameArea.Controls.Add(Me.CollisionTestPanelLadder)
        Me.GameArea.Controls.Add(Me.CollisionTestPanel2)
        Me.GameArea.Controls.Add(Me.CollisionTestPanel3)
        Me.GameArea.Controls.Add(Me.CollisionTestPanel1)
        Me.GameArea.Controls.Add(Me.FPSLabel)
        Me.GameArea.Controls.Add(Me.Megaman3)
        Me.GameArea.Controls.Add(Me.Megaman2)
        Me.GameArea.Controls.Add(Me.Megaman)
        Me.GameArea.Controls.Add(Me.ThreadCountLabel)
        Me.GameArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GameArea.Location = New System.Drawing.Point(0, 0)
        Me.GameArea.Margin = New System.Windows.Forms.Padding(2)
        Me.GameArea.Name = "GameArea"
        Me.GameArea.Size = New System.Drawing.Size(1435, 875)
        Me.GameArea.TabIndex = 2
        '
        'SlopePanelTest1
        '
        Me.SlopePanelTest1.AccessibleName = "9"
        Me.SlopePanelTest1.BackColor = System.Drawing.Color.Blue
        Me.SlopePanelTest1.Location = New System.Drawing.Point(568, 825)
        Me.SlopePanelTest1.Name = "SlopePanelTest1"
        Me.SlopePanelTest1.Size = New System.Drawing.Size(200, 50)
        Me.SlopePanelTest1.TabIndex = 7
        Me.SlopePanelTest1.Tag = "SlopeGroundRight"
        '
        'CollisionTestPanelLadder
        '
        Me.CollisionTestPanelLadder.AccessibleName = "Color"
        Me.CollisionTestPanelLadder.BackColor = System.Drawing.Color.DarkViolet
        Me.CollisionTestPanelLadder.Location = New System.Drawing.Point(200, 583)
        Me.CollisionTestPanelLadder.Name = "CollisionTestPanelLadder"
        Me.CollisionTestPanelLadder.Size = New System.Drawing.Size(100, 292)
        Me.CollisionTestPanelLadder.TabIndex = 8
        Me.CollisionTestPanelLadder.Tag = "Ladder"
        '
        'CollisionTestPanel2
        '
        Me.CollisionTestPanel2.AccessibleName = "5"
        Me.CollisionTestPanel2.BackColor = System.Drawing.Color.Red
        Me.CollisionTestPanel2.Location = New System.Drawing.Point(300, 583)
        Me.CollisionTestPanel2.Name = "CollisionTestPanel2"
        Me.CollisionTestPanel2.Size = New System.Drawing.Size(203, 125)
        Me.CollisionTestPanel2.TabIndex = 6
        Me.CollisionTestPanel2.Tag = "Ground"
        '
        'CollisionTestPanel3
        '
        Me.CollisionTestPanel3.AccessibleName = "5"
        Me.CollisionTestPanel3.BackColor = System.Drawing.Color.Red
        Me.CollisionTestPanel3.Location = New System.Drawing.Point(966, 621)
        Me.CollisionTestPanel3.Name = "CollisionTestPanel3"
        Me.CollisionTestPanel3.Size = New System.Drawing.Size(178, 110)
        Me.CollisionTestPanel3.TabIndex = 5
        Me.CollisionTestPanel3.Tag = "Ground"
        '
        'CollisionTestPanel1
        '
        Me.CollisionTestPanel1.AccessibleName = "5"
        Me.CollisionTestPanel1.BackColor = System.Drawing.Color.Red
        Me.CollisionTestPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CollisionTestPanel1.Location = New System.Drawing.Point(768, 825)
        Me.CollisionTestPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.CollisionTestPanel1.Name = "CollisionTestPanel1"
        Me.CollisionTestPanel1.Size = New System.Drawing.Size(200, 50)
        Me.CollisionTestPanel1.TabIndex = 2
        Me.CollisionTestPanel1.Tag = "Ground"
        '
        'FPSLabel
        '
        Me.FPSLabel.AutoSize = True
        Me.FPSLabel.Location = New System.Drawing.Point(1072, 50)
        Me.FPSLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.FPSLabel.Name = "FPSLabel"
        Me.FPSLabel.Size = New System.Drawing.Size(102, 32)
        Me.FPSLabel.TabIndex = 3
        Me.FPSLabel.Text = "Label1"
        '
        'Megaman3
        '
        Me.Megaman3.BackColor = System.Drawing.Color.Transparent
        Me.Megaman3.Location = New System.Drawing.Point(344, 750)
        Me.Megaman3.Margin = New System.Windows.Forms.Padding(2)
        Me.Megaman3.Name = "Megaman3"
        Me.Megaman3.Size = New System.Drawing.Size(125, 125)
        Me.Megaman3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Megaman3.TabIndex = 0
        Me.Megaman3.TabStop = False
        '
        'Megaman2
        '
        Me.Megaman2.BackColor = System.Drawing.Color.Transparent
        Me.Megaman2.Location = New System.Drawing.Point(344, 449)
        Me.Megaman2.Margin = New System.Windows.Forms.Padding(2)
        Me.Megaman2.Name = "Megaman2"
        Me.Megaman2.Size = New System.Drawing.Size(100, 100)
        Me.Megaman2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Megaman2.TabIndex = 0
        Me.Megaman2.TabStop = False
        '
        'GameWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1435, 875)
        Me.Controls.Add(Me.GameArea)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GameWindow"
        Me.Text = "GameWindow"
        Me.TransparencyKey = System.Drawing.Color.Transparent
        CType(Me.Megaman, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GameArea.ResumeLayout(False)
        Me.GameArea.PerformLayout()
        CType(Me.Megaman3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Megaman2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Megaman As PictureBox
    Friend WithEvents GameArea As Panel
    Friend WithEvents CollisionTestPanel1 As Panel
    Friend WithEvents FPSLabel As Label
    Friend WithEvents CollisionTestPanel3 As Panel
    Friend WithEvents CollisionTestPanel2 As Panel
    Friend WithEvents SlopePanelTest1 As Panel
    Public WithEvents ThreadCountLabel As Label
    Friend WithEvents Megaman2 As PictureBox
    Friend WithEvents CollisionTestPanelLadder As Panel
    Friend WithEvents Megaman3 As PictureBox
End Class
