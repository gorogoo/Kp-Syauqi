Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click_2(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "adminn" And TextBox2.Text = "12345" Then
            Form2.Show()
            Me.Hide()
            MsgBox("login anda berhasil!", vbInformation, "SUKSES!")
            TextBox1.Text = ""
            TextBox2.Text = ""
            CheckBox1.Text = ""
        ElseIf TextBox1.Text = "" And TextBox2.Text = "" Then
            MsgBox("silahkan masukkan username dan password anda!", vbCritical, "GAGAL!")
        ElseIf TextBox1.Text IsNot "adminn" And TextBox2.Text IsNot "12345" Then
            MsgBox("maaf, username atau password anda salah!", vbCritical, "GAGAL!")
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("yakin ingin keluar?", "konfirmasi!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""



    End Sub
End Class
