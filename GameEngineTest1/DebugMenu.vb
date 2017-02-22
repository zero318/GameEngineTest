Public Class DebugMenu
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MultithreadingCheckbox.Checked = MainWindow.MultithreadingEnabled
        DebugHUDCheckbox.Checked = MainWindow.DebugHUDEnabled
        EnableBrokenCrapCheckbox.Checked = MainWindow.MissingCrapEnabled
        BoundingBoxesCheckbox.Checked = MainWindow.DebugBoundingBoxes
    End Sub

    Private Sub ExitDebugMenu_Click(sender As Object, e As EventArgs) Handles ExitDebugMenu.Click
        MainWindow.MultithreadingEnabled = MultithreadingCheckbox.Checked
        MainWindow.DebugHUDEnabled = DebugHUDCheckbox.Checked
        MainWindow.MissingCrapEnabled = EnableBrokenCrapCheckbox.Checked
        MainWindow.DebugBoundingBoxes = BoundingBoxesCheckbox.Checked
        Close()
    End Sub
End Class