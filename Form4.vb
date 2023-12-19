Imports System.Data.OleDb
Imports System.Drawing.Printing
Public Class Form4
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim t_Harga As Long
    Dim t_Qty As Long
    Dim panjang As Integer
    Dim longpaper As Integer
    Sub kosongkan()
        TextBox5.Clear()
        ComboBox1.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        DataGridView1.ClearSelection()
    End Sub

    Sub databaru()
        TextBox5.Clear()
        ComboBox1.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
    End Sub
    Sub refeshgrid()
        da = New OleDbDataAdapter("select*from keranjang", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "keranjnag")
        DataGridView1.DataSource = (ds.Tables("keranjang"))
    End Sub
    Sub ketemu()
        On Error Resume Next
        ComboBox1.Text = dr.Item(1) '=
        TextBox1.Text = dr.Item(2) '=
        TextBox2.Text = dr.Item(3) '=
        TextBox3.Text = dr.Item(4) '=
        TextBox4.Text = dr.Item(5) '=
    End Sub
    Sub carikode()
        cmd = New OleDbCommand("select*from keranjang where idtransaksi ='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub simpandata()
        Call koneksi()
        da = New OleDbDataAdapter("select * from datamenu ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
    End Sub
    Sub itemcomboo()
        Call koneksi()
        da = New OleDbDataAdapter("select * from datamenu ", conn)
        ds = New DataSet
        da.Fill(ds)
        re.DataSource = ds
        re.DataMember = ds.Tables(0).ToString
        Dim a As DataRow
        ComboBox1.Items.Clear()
        For Each a In ds.Tables(0).Rows
            ComboBox1.Items.Add(a.Item(0))
        Next a
    End Sub
    Sub tampilgrid()
        Call koneksi()
        da = New OleDbDataAdapter("select*from keranjang ORDER BY idtransaksi DESC", conn)
        ds = New DataSet
        da.Fill(ds, "keranjang")
        DataGridView1.DataSource = ds.Tables("keranjang")
        'responsive dg
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Sub hitung()
        Dim sum As Decimal = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            sum += DataGridView1.Rows(i).Cells(5).Value
        Next
        TextBox8.Text = sum
    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim angka1 As Integer = Integer.Parse(TextBox8.Text)
        Dim angka2 As Integer = Integer.Parse(TextBox6.Text)

        If angka1 > angka2 Then
            MsgBox("nominal tidak cukup")
            TextBox6.Text = ""
        Else
            Dim hasil As Integer = angka2 - angka1
            TextBox7.Text = hasil.ToString
        End If

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call databaru()
        Call koneksi()
        Call tampilgrid()
        Call kosongkan()
        Call itemcomboo()
        Call simpandata()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call carikode()
        If Not dr.HasRows Then
            Dim simpan As String = "insert into keranjang values ('" & TextBox5.Text & "','" & ComboBox1.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
            cmd = New OleDbCommand(simpan, conn)
            cmd.ExecuteNonQuery()
            Call refeshgrid()
            Call kosongkan()
            Call tampilgrid()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        cmd = New OleDb.OleDbCommand("select*from datamenu where idmenu='" & ComboBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        TextBox1.Text = dr.Item(1) '=nama
        TextBox2.Text = dr.Item(2) '=perusahaan
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Call kosongkan()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox4.Text = Val(TextBox2.Text) * Val(TextBox3.Text)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        On Error Resume Next
        cmd = New OleDbCommand("select * from keranjang", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            MsgBox("tidak ada data untuk di hapus", vbInformation)
            Exit Sub
        Else
            cmd = New OleDbCommand("delete from keranjang where idtransaksi='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'", conn)
            cmd.ExecuteNonQuery()
            MsgBox("data berhasil di hapus")
            Call tampilgrid()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        changelongpaper()
        PPD.Document = PD
        PPD.ShowDialog()
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f14 As New Font("Times New Roman", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center

        Dim garis As String
        garis = "------------------------------------------------------------------"

        e.Graphics.DrawString("SABANA FRIED CHIKEN", f14, Brushes.Black, centermargin, 5, tengah)
        e.Graphics.DrawString("JL.Raya Mangun Jaya, Tambun Selatan, Bekasi," & vbNewLine & " Kab Bekasi, Jawa Barat, 17510", f10, Brushes.Black, centermargin, 30, tengah)
        e.Graphics.DrawString("Hp: 0812-1341-4229", f10, Brushes.Black, centermargin, 65, tengah)

        e.Graphics.DrawString("Nama Kasir :", f10, Brushes.Black, 0, 90)
        e.Graphics.DrawString("Syauqi", f10, Brushes.Black, 75, 90)

        e.Graphics.DrawString(Date.Now(), f10, Brushes.Black, 0, 105)
        e.Graphics.DrawString("Nama", f10, Brushes.Black, 0, 125)
        e.Graphics.DrawString("Qty", f10, Brushes.Black, 100, 125)
        e.Graphics.DrawString("Harga", f10, Brushes.Black, 150, 125)
        e.Graphics.DrawString("Total", f10, Brushes.Black, rightmargin, 125, kanan)
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, 130)
        Dim tinggi As Integer
        Dim total As Integer
        Dim pembayaran As Integer
        Dim kembalian As Integer
        For Each baris As DataGridViewRow In DataGridView1.Rows
            If Not baris.IsNewRow Then
                tinggi += 15
                e.Graphics.DrawString(baris.Cells(2).Value, f10, Brushes.Black, 0, 130 + tinggi)
                e.Graphics.DrawString(baris.Cells(4).Value, f10, Brushes.Black, 100, 130 + tinggi)
                e.Graphics.DrawString(baris.Cells(3).Value, f10, Brushes.Black, 150, 130 + tinggi)

                e.Graphics.DrawString(baris.Cells(5).Value, f10, Brushes.Black, rightmargin, 130 + tinggi, kanan)
                total += CDbl(baris.Cells(5).Value)
                pembayaran = CDbl(TextBox6.Text)
                kembalian = CDbl(TextBox7.Text)
            End If
        Next
        tinggi = 140 + tinggi
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString("Subtotal :" & FormatCurrency(total), f10b, Brushes.Black, 130, 15 + tinggi)
        e.Graphics.DrawString("Uang :" & FormatCurrency(pembayaran), f10b, Brushes.Black, 130, 35 + tinggi)
        e.Graphics.DrawString("Kembalian :" & FormatCurrency(kembalian), f10b, Brushes.Black, 130, 55 + tinggi)
    End Sub


    Sub hitungtotal()

        Dim hitung As Long = 0

        For baris As Long = 0 To DataGridView1.RowCount - 1

            hitung = hitung + DataGridView1.Rows(baris).Cells(3).Value
        Next
        t_Harga = hitung

        Dim hitung2 As Long = 0
        For baris As Long = 0 To DataGridView1.RowCount - 1

            hitung2 = hitung2 + DataGridView1.Rows(baris).Cells(1).Value
        Next
        t_Qty = hitung2
    End Sub
    Sub changelongpaper()
        Dim rowcount As Integer
        longpaper = 0
        rowcount = DataGridView1.Rows.Count
        longpaper = rowcount * 15
        longpaper = longpaper + 240
    End Sub
    Private Sub PD_BeginPrint(sender As Object, e As PrintEventArgs) Handles PD.BeginPrint
        'Dim pagesetup As New PageSettings
        'pagesetup.PaperSize = New PaperSize("Custom", 300, 500) 'fixed size
        Dim paperSize As New PaperSize("Custom", 300, 500)
        'pagesetup.PaperSize = New PaperSize("Custom", 250, longpaper)
        PD.DefaultPageSettings.PaperSize = paperSize

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call hitung()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If MessageBox.Show("Apakah Anda Ingin Menyelesaikan Transaksi?", "Konfirmasi!", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim hapus As String = "delete from keranjang"
            cmd = New OleDbCommand(hapus, conn)
            cmd.ExecuteNonQuery()
            Call refeshgrid()
            Call kosongkan()
            Call tampilgrid()
        Else
            Call kosongkan()
        End If
    End Sub
End Class