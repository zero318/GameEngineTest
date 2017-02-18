Public Class AudioSettings
    Private Sub AudioSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AudioSlider.Value = MainWindow.InternalAudioLevel
        EnableAudioCheckbox.Checked = MainWindow.AudioEnabled
        AudioSliderLabel.Text = AudioSlider.Value
    End Sub

    Private Sub AudioSlider_Scroll(sender As Object, e As EventArgs) Handles AudioSlider.Scroll
        AudioSliderLabel.Text = AudioSlider.Value
    End Sub

    Private Sub EnableAudioCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles EnableAudioCheckbox.CheckedChanged
        If EnableAudioCheckbox.Checked = False Then
            AudioSlider.Enabled = False
        Else
            AudioSlider.Enabled = True
        End If
    End Sub

    Private Sub ExitAudioSettings_Click(sender As Object, e As EventArgs) Handles ExitAudioSettings.Click
        MainWindow.InternalAudioLevel = AudioSlider.Value
        MainWindow.AudioEnabled = EnableAudioCheckbox.Checked
        Close()
    End Sub
End Class