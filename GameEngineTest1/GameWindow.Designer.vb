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
        CType(Me.Megaman, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GameArea.SuspendLayout()
        Me.SuspendLayout()
        '
        'Megaman
        '
        Me.Megaman.BackColor = System.Drawing.Color.Transparent
        Me.Megaman.Location = New System.Drawing.Point(0, 325)
        Me.Megaman.Margin = New System.Windows.Forms.Padding(1)
        Me.Megaman.Name = "Megaman"
        Me.Megaman.Size = New System.Drawing.Size(40, 40)
        Me.Megaman.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Megaman.TabIndex = 0
        Me.Megaman.TabStop = False
        '
        'ThreadCountLabel
        '
        Me.ThreadCountLabel.AutoSize = True
        Me.ThreadCountLabel.Location = New System.Drawing.Point(5, 4)
        Me.ThreadCountLabel.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.ThreadCountLabel.Name = "ThreadCountLabel"
        Me.ThreadCountLabel.Size = New System.Drawing.Size(39, 13)
        Me.ThreadCountLabel.TabIndex = 1
        Me.ThreadCountLabel.Text = "Label1"
        '
        'GameArea
        '
        Me.GameArea.BackColor = System.Drawing.Color.Transparent
        Me.GameArea.Controls.Add(Me.FPSLabel)
        Me.GameArea.Controls.Add(Me.CollisionTestPanel1)
        Me.GameArea.Controls.Add(Me.Megaman)
        Me.GameArea.Controls.Add(Me.ThreadCountLabel)
        Me.GameArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GameArea.Location = New System.Drawing.Point(0, 0)
        Me.GameArea.Margin = New System.Windows.Forms.Padding(1)
        Me.GameArea.Name = "GameArea"
        Me.GameArea.Size = New System.Drawing.Size(574, 350)
        Me.GameArea.TabIndex = 2
        '
        'FPSLabel
        '
        Me.FPSLabel.AutoSize = True
        Me.FPSLabel.Location = New System.Drawing.Point(429, 20)
        Me.FPSLabel.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.FPSLabel.Name = "FPSLabel"
        Me.FPSLabel.Size = New System.Drawing.Size(39, 13)
        Me.FPSLabel.TabIndex = 3
        Me.FPSLabel.Text = "Label1"
        '
        'CollisionTestPanel1
        '
        Me.CollisionTestPanel1.BackColor = System.Drawing.Color.Red
        Me.CollisionTestPanel1.Location = New System.Drawing.Point(307, 345)
        Me.CollisionTestPanel1.Margin = New System.Windows.Forms.Padding(1)
        Me.CollisionTestPanel1.Name = "CollisionTestPanel1"
        Me.CollisionTestPanel1.Size = New System.Drawing.Size(80, 20)
        Me.CollisionTestPanel1.TabIndex = 2
        '
        'GameWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(574, 350)
        Me.Controls.Add(Me.GameArea)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(1)
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
End Class
