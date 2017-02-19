<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow
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
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.DebugStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MultithreadingStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileSaveMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileLoadMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileExitMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsInputMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsGraphicsMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsAudioMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsPathsMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsUserInterfaceMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugDebugMenuMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutMenuStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.StuffButton1 = New System.Windows.Forms.Button()
        Me.ThreadingTest = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.KeyLabel = New System.Windows.Forms.Label()
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DebugStatusLabel, Me.MultithreadingStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 589)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.StatusStrip.Size = New System.Drawing.Size(1000, 46)
        Me.StatusStrip.SizingGrip = False
        Me.StatusStrip.TabIndex = 2
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'DebugStatusLabel
        '
        Me.DebugStatusLabel.Name = "DebugStatusLabel"
        Me.DebugStatusLabel.Size = New System.Drawing.Size(226, 41)
        Me.DebugStatusLabel.Text = "DEBUG: [Status]"
        '
        'MultithreadingStatusLabel
        '
        Me.MultithreadingStatusLabel.Name = "MultithreadingStatusLabel"
        Me.MultithreadingStatusLabel.Size = New System.Drawing.Size(320, 41)
        Me.MultithreadingStatusLabel.Text = "Multithreading [Status]"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenuStrip, Me.SettingsMenuStrip, Me.DebugMenuStrip, Me.HelpMenuStrip})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1000, 49)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileMenuStrip
        '
        Me.FileMenuStrip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileSaveMenuStrip, Me.FileLoadMenuStrip, Me.FileExitMenuStrip})
        Me.FileMenuStrip.Name = "FileMenuStrip"
        Me.FileMenuStrip.Size = New System.Drawing.Size(75, 45)
        Me.FileMenuStrip.Text = "&File"
        '
        'FileSaveMenuStrip
        '
        Me.FileSaveMenuStrip.Name = "FileSaveMenuStrip"
        Me.FileSaveMenuStrip.Size = New System.Drawing.Size(198, 46)
        Me.FileSaveMenuStrip.Text = "Sa&ve"
        '
        'FileLoadMenuStrip
        '
        Me.FileLoadMenuStrip.Name = "FileLoadMenuStrip"
        Me.FileLoadMenuStrip.Size = New System.Drawing.Size(198, 46)
        Me.FileLoadMenuStrip.Text = "&Load"
        '
        'FileExitMenuStrip
        '
        Me.FileExitMenuStrip.Name = "FileExitMenuStrip"
        Me.FileExitMenuStrip.Size = New System.Drawing.Size(198, 46)
        Me.FileExitMenuStrip.Text = "E&xit"
        '
        'SettingsMenuStrip
        '
        Me.SettingsMenuStrip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsInputMenuStrip, Me.SettingsGraphicsMenuStrip, Me.SettingsAudioMenuStrip, Me.SettingsPathsMenuStrip, Me.SettingsUserInterfaceMenuStrip})
        Me.SettingsMenuStrip.Name = "SettingsMenuStrip"
        Me.SettingsMenuStrip.Size = New System.Drawing.Size(137, 45)
        Me.SettingsMenuStrip.Text = "&Settings"
        '
        'SettingsInputMenuStrip
        '
        Me.SettingsInputMenuStrip.Name = "SettingsInputMenuStrip"
        Me.SettingsInputMenuStrip.Size = New System.Drawing.Size(316, 46)
        Me.SettingsInputMenuStrip.Text = "&Input"
        '
        'SettingsGraphicsMenuStrip
        '
        Me.SettingsGraphicsMenuStrip.Name = "SettingsGraphicsMenuStrip"
        Me.SettingsGraphicsMenuStrip.Size = New System.Drawing.Size(316, 46)
        Me.SettingsGraphicsMenuStrip.Text = "&Graphics"
        '
        'SettingsAudioMenuStrip
        '
        Me.SettingsAudioMenuStrip.Name = "SettingsAudioMenuStrip"
        Me.SettingsAudioMenuStrip.Size = New System.Drawing.Size(316, 46)
        Me.SettingsAudioMenuStrip.Text = "&Audio"
        '
        'SettingsPathsMenuStrip
        '
        Me.SettingsPathsMenuStrip.Name = "SettingsPathsMenuStrip"
        Me.SettingsPathsMenuStrip.Size = New System.Drawing.Size(316, 46)
        Me.SettingsPathsMenuStrip.Text = "&Paths"
        '
        'SettingsUserInterfaceMenuStrip
        '
        Me.SettingsUserInterfaceMenuStrip.Name = "SettingsUserInterfaceMenuStrip"
        Me.SettingsUserInterfaceMenuStrip.Size = New System.Drawing.Size(316, 46)
        Me.SettingsUserInterfaceMenuStrip.Text = "&User Interface"
        '
        'DebugMenuStrip
        '
        Me.DebugMenuStrip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DebugDebugMenuMenuStrip})
        Me.DebugMenuStrip.Name = "DebugMenuStrip"
        Me.DebugMenuStrip.Size = New System.Drawing.Size(120, 45)
        Me.DebugMenuStrip.Text = "&Debug"
        '
        'DebugDebugMenuMenuStrip
        '
        Me.DebugDebugMenuMenuStrip.Name = "DebugDebugMenuMenuStrip"
        Me.DebugDebugMenuMenuStrip.Size = New System.Drawing.Size(308, 46)
        Me.DebugDebugMenuMenuStrip.Text = "Debug &Menu"
        '
        'HelpMenuStrip
        '
        Me.HelpMenuStrip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutMenuStrip})
        Me.HelpMenuStrip.Name = "HelpMenuStrip"
        Me.HelpMenuStrip.Size = New System.Drawing.Size(92, 45)
        Me.HelpMenuStrip.Text = "&Help"
        '
        'AboutMenuStrip
        '
        Me.AboutMenuStrip.Name = "AboutMenuStrip"
        Me.AboutMenuStrip.Size = New System.Drawing.Size(215, 46)
        Me.AboutMenuStrip.Text = "&About"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'StuffButton1
        '
        Me.StuffButton1.Location = New System.Drawing.Point(565, 392)
        Me.StuffButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.StuffButton1.Name = "StuffButton1"
        Me.StuffButton1.Size = New System.Drawing.Size(392, 160)
        Me.StuffButton1.TabIndex = 4
        Me.StuffButton1.Text = "Run Stuff Test"
        Me.StuffButton1.UseVisualStyleBackColor = True
        '
        'ThreadingTest
        '
        Me.ThreadingTest.Location = New System.Drawing.Point(565, 120)
        Me.ThreadingTest.Margin = New System.Windows.Forms.Padding(2)
        Me.ThreadingTest.Name = "ThreadingTest"
        Me.ThreadingTest.Size = New System.Drawing.Size(385, 170)
        Me.ThreadingTest.TabIndex = 5
        Me.ThreadingTest.Text = "Threading Test"
        Me.ThreadingTest.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(115, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 32)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(111, 335)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 32)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Label2"
        '
        'KeyLabel
        '
        Me.KeyLabel.AutoSize = True
        Me.KeyLabel.Location = New System.Drawing.Point(86, 485)
        Me.KeyLabel.Name = "KeyLabel"
        Me.KeyLabel.Size = New System.Drawing.Size(102, 32)
        Me.KeyLabel.TabIndex = 8
        Me.KeyLabel.Text = "Label3"
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(240.0!, 240.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1000, 635)
        Me.Controls.Add(Me.KeyLabel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ThreadingTest)
        Me.Controls.Add(Me.StuffButton1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MainWindow"
        Me.Text = "Game Engine Test 1"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents DebugStatusLabel As ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileMenuStrip As ToolStripMenuItem
    Friend WithEvents FileSaveMenuStrip As ToolStripMenuItem
    Friend WithEvents FileLoadMenuStrip As ToolStripMenuItem
    Friend WithEvents SettingsMenuStrip As ToolStripMenuItem
    Friend WithEvents SettingsInputMenuStrip As ToolStripMenuItem
    Friend WithEvents SettingsGraphicsMenuStrip As ToolStripMenuItem
    Friend WithEvents SettingsAudioMenuStrip As ToolStripMenuItem
    Friend WithEvents SettingsPathsMenuStrip As ToolStripMenuItem
    Friend WithEvents DebugMenuStrip As ToolStripMenuItem
    Friend WithEvents DebugDebugMenuMenuStrip As ToolStripMenuItem
    Friend WithEvents HelpMenuStrip As ToolStripMenuItem
    Friend WithEvents AboutMenuStrip As ToolStripMenuItem
    Friend WithEvents MultithreadingStatusLabel As ToolStripStatusLabel
    Friend WithEvents FileExitMenuStrip As ToolStripMenuItem
    Friend WithEvents SettingsUserInterfaceMenuStrip As ToolStripMenuItem
    Friend WithEvents SaveFileDialog As SaveFileDialog
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents StuffButton1 As Button
    Friend WithEvents ThreadingTest As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents KeyLabel As Label
End Class
