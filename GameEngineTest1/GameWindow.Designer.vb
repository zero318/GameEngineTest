<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Megaman = New System.Windows.Forms.PictureBox()
        Me.ThreadCountLabel = New System.Windows.Forms.Label()
        CType(Me.Megaman, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Megaman
        '
        Me.Megaman.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Megaman.BackColor = System.Drawing.Color.Transparent
        Me.Megaman.Image = Global.GameEngineTest1.My.Resources.Resources.Standing1
        Me.Megaman.Location = New System.Drawing.Point(109, 397)
        Me.Megaman.Name = "Megaman"
        Me.Megaman.Size = New System.Drawing.Size(100, 100)
        Me.Megaman.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Megaman.TabIndex = 0
        Me.Megaman.TabStop = False
        '
        'ThreadCountLabel
        '
        Me.ThreadCountLabel.AutoSize = True
        Me.ThreadCountLabel.Location = New System.Drawing.Point(216, 107)
        Me.ThreadCountLabel.Name = "ThreadCountLabel"
        Me.ThreadCountLabel.Size = New System.Drawing.Size(102, 32)
        Me.ThreadCountLabel.TabIndex = 1
        Me.ThreadCountLabel.Text = "Label1"
        '
        'GameWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(968, 562)
        Me.Controls.Add(Me.ThreadCountLabel)
        Me.Controls.Add(Me.Megaman)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GameWindow"
        Me.Text = "GameWindow"
        CType(Me.Megaman, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Megaman As PictureBox
    Friend WithEvents ThreadCountLabel As Label
End Class
