<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.FPSLabel = New System.Windows.Forms.Label()
        Me.CollisionTestPanel1 = New System.Windows.Forms.Panel()
        Me.CollisionTestPanel3 = New System.Windows.Forms.Panel()
        Me.CollisionTestPanel2 = New System.Windows.Forms.Panel()
        CType(Me.Megaman, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GameArea.SuspendLayout()
        Me.SuspendLayout()
        '
        'Megaman
        '
        Me.Megaman.BackColor = System.Drawing.Color.Transparent
        Me.Megaman.Location = New System.Drawing.Point(0, 812)
        Me.Megaman.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Megaman.Name = "Megaman"
        Me.Megaman.Size = New System.Drawing.Size(100, 100)
        Me.Megaman.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Megaman.TabIndex = 0
        Me.Megaman.TabStop = False
        '
        'ThreadCountLabel
        '
        Me.ThreadCountLabel.AutoSize = True
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
        Me.GameArea.Controls.Add(Me.CollisionTestPanel2)
        Me.GameArea.Controls.Add(Me.CollisionTestPanel3)
        Me.GameArea.Controls.Add(Me.FPSLabel)
        Me.GameArea.Controls.Add(Me.CollisionTestPanel1)
        Me.GameArea.Controls.Add(Me.Megaman)
        Me.GameArea.Controls.Add(Me.ThreadCountLabel)
        Me.GameArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GameArea.Location = New System.Drawing.Point(0, 0)
        Me.GameArea.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GameArea.Name = "GameArea"
        Me.GameArea.Size = New System.Drawing.Size(1435, 875)
        Me.GameArea.TabIndex = 2
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
        'CollisionTestPanel1
        '
        Me.CollisionTestPanel1.BackColor = System.Drawing.Color.Red
        Me.CollisionTestPanel1.Location = New System.Drawing.Point(768, 862)
        Me.CollisionTestPanel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CollisionTestPanel1.Name = "CollisionTestPanel1"
        Me.CollisionTestPanel1.Size = New System.Drawing.Size(200, 50)
        Me.CollisionTestPanel1.TabIndex = 2
        '
        'CollisionTestPanel3
        '
        Me.CollisionTestPanel3.BackColor = System.Drawing.Color.Red
        Me.CollisionTestPanel3.Location = New System.Drawing.Point(966, 621)
        Me.CollisionTestPanel3.Name = "CollisionTestPanel3"
        Me.CollisionTestPanel3.Size = New System.Drawing.Size(178, 110)
        Me.CollisionTestPanel3.TabIndex = 5
        '
        'CollisionTestPanel2
        '
        Me.CollisionTestPanel2.BackColor = System.Drawing.Color.Red
        Me.CollisionTestPanel2.Location = New System.Drawing.Point(301, 606)
        Me.CollisionTestPanel2.Name = "CollisionTestPanel2"
        Me.CollisionTestPanel2.Size = New System.Drawing.Size(203, 125)
        Me.CollisionTestPanel2.TabIndex = 6
        '
        'GameWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1435, 875)
        Me.Controls.Add(Me.GameArea)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GameWindow"
        Me.Text = "GameWindow"
        CType(Me.Megaman, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GameArea.ResumeLayout(False)
        Me.GameArea.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Megaman As PictureBox
    Friend WithEvents ThreadCountLabel As Label
    Friend WithEvents GameArea As Panel
    Friend WithEvents CollisionTestPanel1 As Panel
    Friend WithEvents FPSLabel As Label
    Friend WithEvents CollisionTestPanel3 As Panel
    Friend WithEvents CollisionTestPanel2 As Panel
End Class
