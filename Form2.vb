Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim intResponse As Integer
        intResponse = MsgBox("Anda Yakin Ingin Keluar ?",
 vbYesNo + vbQuestion, "Peringatan")
        If intResponse = vbYes Then
            Me.Close()
            Form1.Show()
            MsgBox("Anda Berhasil Keluar", MsgBoxStyle.MsgBoxRight, "Perhatian")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form4.Show()
        Me.Hide()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub
End Class