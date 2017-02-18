<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AudioSettings
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
        Me.AudioSlider = New System.Windows.Forms.TrackBar()
        Me.AudioSliderLabel = New System.Windows.Forms.Label()
        Me.EnableAudioCheckbox = New System.Windows.Forms.CheckBox()
        Me.ExitAudioSettings = New System.Windows.Forms.Button()
        CType(Me.AudioSlider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AudioSlider
        '
        Me.AudioSlider.Location = New System.Drawing.Point(195, 81)
        Me.AudioSlider.Maximum = 100
        Me.AudioSlider.Name = "AudioSlider"
        Me.AudioSlider.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.AudioSlider.Size = New System.Drawing.Size(114, 391)
        Me.AudioSlider.TabIndex = 0
        Me.AudioSlider.Value = 100
        '
        'AudioSliderLabel
        '
        Me.AudioSliderLabel.AutoSize = True
        Me.AudioSliderLabel.Location = New System.Drawing.Point(343, 191)
        Me.AudioSliderLabel.Name = "AudioSliderLabel"
        Me.AudioSliderLabel.Size = New System.Drawing.Size(102, 32)
        Me.AudioSliderLabel.TabIndex = 1
        Me.AudioSliderLabel.Text = "Label1"
        '
        'EnableAudioCheckbox
        '
        Me.EnableAudioCheckbox.AutoSize = True
        Me.EnableAudioCheckbox.Checked = True
        Me.EnableAudioCheckbox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableAudioCheckbox.Location = New System.Drawing.Point(617, 97)
        Me.EnableAudioCheckbox.Name = "EnableAudioCheckbox"
        Me.EnableAudioCheckbox.Size = New System.Drawing.Size(224, 36)
        Me.EnableAudioCheckbox.TabIndex = 2
        Me.EnableAudioCheckbox.Text = "Enable Audio"
        Me.EnableAudioCheckbox.UseVisualStyleBackColor = True
        '
        'ExitAudioSettings
        '
        Me.ExitAudioSettings.Location = New System.Drawing.Point(574, 335)
        Me.ExitAudioSettings.Name = "ExitAudioSettings"
        Me.ExitAudioSettings.Size = New System.Drawing.Size(399, 173)
        Me.ExitAudioSettings.TabIndex = 3
        Me.ExitAudioSettings.Text = "Back"
        Me.ExitAudioSettings.UseVisualStyleBackColor = True
        '
        'AudioSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1047, 566)
        Me.Controls.Add(Me.ExitAudioSettings)
        Me.Controls.Add(Me.EnableAudioCheckbox)
        Me.Controls.Add(Me.AudioSliderLabel)
        Me.Controls.Add(Me.AudioSlider)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AudioSettings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Audio Settings"
        CType(Me.AudioSlider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AudioSlider As TrackBar
    Friend WithEvents AudioSliderLabel As Label
    Friend WithEvents EnableAudioCheckbox As CheckBox
    Friend WithEvents ExitAudioSettings As Button
End Class
