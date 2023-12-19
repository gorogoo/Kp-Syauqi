Imports System.Data.OleDb
Public Class Form3
    Sub kosongkan()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub
    Sub databaru()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub
    Sub refeshgrid()
        da = New OleDbDataAdapter("select*from datamenu", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "datamenu")
        DataGridView1.DataSource = (ds.Tables("datamenu"))
    End Sub
    Sub ketemu()
        On Error Resume Next
        TextBox2.Text = dr.Item(1) '=kode_barang
        TextBox3.Text = dr.Item(2) '=nama_barang
    End Sub
    Sub carikode()
        cmd = New OleDbCommand("select*from datamenu where idmenu ='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub tampilgrid()
        Call koneksi()
        da = New OleDbDataAdapter("select*from datamenu ORDER BY idmenu DESC", conn)
        ds = New DataSet
        da.Fill(ds, "datamenu")
        DataGridView1.DataSource = ds.Tables("datamenu")
        'responsive dg
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call databaru()
        Call koneksi()
        Call tampilgrid()
        Call kosongkan()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data belum lengkap", vbInformation, "Maaf!")
            Exit Sub
        Else
            Call carikode()
            If Not dr.HasRows Then
                Dim simpan As String = "insert into datamenu values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
                cmd = New OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                Call refeshgrid()
                Call kosongkan()
                Call tampilgrid()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data belum lengkap", vbInformation, "Maaf!")
            Exit Sub
        Else
            Call carikode()
            If dr.HasRows Then
                Dim edit As String = "update datamenu set namamenu='" & TextBox2.Text & "',hargasatuan='" & TextBox3.Text & "'where idmenu='" & TextBox1.Text & "'"
                cmd = New OleDbCommand(edit, conn)
                cmd.ExecuteNonQuery()
                Call refeshgrid()
                Call kosongkan()
                Call tampilgrid()

            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Call carikode()
        If dr.HasRows Then
            Call ketemu()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Kode barang harus diisi", vbInformation, "Maaf!")
            TextBox1.Text = Focus()
            Exit Sub
        End If
        Call carikode()
        If Not dr.HasRows Then
            MsgBox("Kode barang tidak ditemukan", vbInformation, "Maaf!")
            TextBox1.Focus()
            Exit Sub
        End If
        If MessageBox.Show("Yakin akan di hapus?", "Konfirmasi!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim hapus As String = "delete from datamenu where idmenu='" & TextBox1.Text & "'"
            cmd = New OleDbCommand(hapus, conn)
            cmd.ExecuteNonQuery()
            Call refeshgrid()
            Call kosongkan()
            Call tampilgrid()
        Else
            Call kosongkan()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class