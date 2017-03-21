<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DebugWindow
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
        Me.DebugOutputLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DebugOutputLabel
        '
        Me.DebugOutputLabel.AutoSize = True
        Me.DebugOutputLabel.Location = New System.Drawing.Point(12, 9)
        Me.DebugOutputLabel.Name = "DebugOutputLabel"
        Me.DebugOutputLabel.Size = New System.Drawing.Size(102, 32)
        Me.DebugOutputLabel.TabIndex = 0
        Me.DebugOutputLabel.Text = "Label1"
        '
        'DebugWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(808, 486)
        Me.Controls.Add(Me.DebugOutputLabel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DebugWindow"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "DebugWindow"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents DebugOutputLabel As Label
End Class
