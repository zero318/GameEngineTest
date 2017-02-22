<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DebugMenu
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
        Me.MultithreadingCheckbox = New System.Windows.Forms.CheckBox()
        Me.DebugHUDCheckbox = New System.Windows.Forms.CheckBox()
        Me.ExitDebugMenu = New System.Windows.Forms.Button()
        Me.EnableBrokenCrapCheckbox = New System.Windows.Forms.CheckBox()
        Me.BoundingBoxesCheckbox = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'MultithreadingCheckbox
        '
        Me.MultithreadingCheckbox.AutoSize = True
        Me.MultithreadingCheckbox.Location = New System.Drawing.Point(65, 49)
        Me.MultithreadingCheckbox.Name = "MultithreadingCheckbox"
        Me.MultithreadingCheckbox.Size = New System.Drawing.Size(331, 36)
        Me.MultithreadingCheckbox.TabIndex = 0
        Me.MultithreadingCheckbox.Text = "Enable Multithreading"
        Me.MultithreadingCheckbox.UseVisualStyleBackColor = True
        '
        'DebugHUDCheckbox
        '
        Me.DebugHUDCheckbox.AutoSize = True
        Me.DebugHUDCheckbox.Location = New System.Drawing.Point(119, 207)
        Me.DebugHUDCheckbox.Name = "DebugHUDCheckbox"
        Me.DebugHUDCheckbox.Size = New System.Drawing.Size(215, 36)
        Me.DebugHUDCheckbox.TabIndex = 1
        Me.DebugHUDCheckbox.Text = "Debug Mode"
        Me.DebugHUDCheckbox.UseVisualStyleBackColor = True
        '
        'ExitDebugMenu
        '
        Me.ExitDebugMenu.Location = New System.Drawing.Point(146, 470)
        Me.ExitDebugMenu.Name = "ExitDebugMenu"
        Me.ExitDebugMenu.Size = New System.Drawing.Size(502, 67)
        Me.ExitDebugMenu.TabIndex = 2
        Me.ExitDebugMenu.Text = "Back"
        Me.ExitDebugMenu.UseVisualStyleBackColor = True
        '
        'EnableBrokenCrapCheckbox
        '
        Me.EnableBrokenCrapCheckbox.AutoSize = True
        Me.EnableBrokenCrapCheckbox.Location = New System.Drawing.Point(300, 311)
        Me.EnableBrokenCrapCheckbox.Name = "EnableBrokenCrapCheckbox"
        Me.EnableBrokenCrapCheckbox.Size = New System.Drawing.Size(464, 36)
        Me.EnableBrokenCrapCheckbox.TabIndex = 3
        Me.EnableBrokenCrapCheckbox.Text = "Enable Broken/Missing Features"
        Me.EnableBrokenCrapCheckbox.UseVisualStyleBackColor = True
        '
        'BoundingBoxesCheckbox
        '
        Me.BoundingBoxesCheckbox.AutoSize = True
        Me.BoundingBoxesCheckbox.Location = New System.Drawing.Point(477, 134)
        Me.BoundingBoxesCheckbox.Name = "BoundingBoxesCheckbox"
        Me.BoundingBoxesCheckbox.Size = New System.Drawing.Size(330, 36)
        Me.BoundingBoxesCheckbox.TabIndex = 4
        Me.BoundingBoxesCheckbox.Text = "View Bounding Boxes"
        Me.BoundingBoxesCheckbox.UseVisualStyleBackColor = True
        '
        'DebugMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(947, 602)
        Me.Controls.Add(Me.BoundingBoxesCheckbox)
        Me.Controls.Add(Me.EnableBrokenCrapCheckbox)
        Me.Controls.Add(Me.ExitDebugMenu)
        Me.Controls.Add(Me.DebugHUDCheckbox)
        Me.Controls.Add(Me.MultithreadingCheckbox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DebugMenu"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "DebugMenu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MultithreadingCheckbox As CheckBox
    Friend WithEvents DebugHUDCheckbox As CheckBox
    Friend WithEvents ExitDebugMenu As Button
    Friend WithEvents EnableBrokenCrapCheckbox As CheckBox
    Friend WithEvents BoundingBoxesCheckbox As CheckBox
End Class
