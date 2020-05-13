Imports System.Data.OleDb
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RefreshGrid()

    End Sub

    Private Sub RefreshGrid()
        'Dim c As New OleDbCommand
        'c.Connection = conn
        'c.CommandText = "select * from Список"

        'Dim ds As New DataSet
        'Dim da As New OleDbDataAdapter(c)
        'da.Fill(ds, "Список")
        'Grid1.DataSource = ds
        'Grid1.DataMember = "Список"

        FillGridDA(Grid1, "select * from Список", "Список", DA1)
        Grid1.Columns("Код").Visible = False
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = New OleDbConnection
        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\База данных\База данных.accdb;Persist Security Info=False"
        conn.Open()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim s1, s2, s3 As String
        Dim r As DialogResult

        Form2.ShowDialog()
        s1 = Form2.TextBox1.Text
        s2 = Form2.TextBox2.Text
        s3 = Form2.TextBox3.Text
        r = Form2.DialogResult

        Form2.Close()

        If r <> DialogResult.OK Then
            Exit Sub
        End If

        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "insert into Список(ФИО, Дата рождения, Телефон) values ('" & s1 & "','" & s2 & "','" & s3 & "')"
        c.ExecuteNonQuery()

        RefreshGrid()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim k As Integer
        Dim c As New OleDbCommand
        c.Connection = conn
        k = Grid1.CurrentRow.Cells("Код").Value
        c.CommandText = "delete from Список where Код = " & k
        c.ExecuteNonQuery()



        RefreshGrid()

    End Sub
    Private Sub Grid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid1.CellClick
        RefreshGrid2()
    End Sub

    Private Sub RefreshGrid2()
        Dim k As Integer
        k = Grid1.CurrentRow.Cells("Код").Value
        FillGrid(Grid2, "select * from События where Код_Список=" & k, "События")
        Grid2.Columns("Код").Visible = False
        Grid2.Columns("Код_Список").Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim s1, s2, s3, s4 As String
        Dim r As DialogResult

        Form3.ShowDialog()
        s1 = Grid1.CurrentRow.Cells("Код").Value
        s2 = Form3.TextBox2.Text
        s3 = Form3.TextBox3.Text
        s4 = Form3.TextBox4.Text
        r = Form3.DialogResult

        Form3.Close()

        If r <> DialogResult.OK Then
            Exit Sub
        End If

        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "insert into События(Код_Список, Экзамен, Зачет, Аттестация) values ('" & s1 & "','" & s2 & "','" & s3 & "','" & s4 & "')"
        c.ExecuteNonQuery()

        RefreshGrid2()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim s1, s2, s3 As String
        Dim r As DialogResult
        Dim k As Integer
        k = Grid1.CurrentRow.Cells("Код").Value
        Form2.TextBox1.Text = Grid1.CurrentRow.Cells("ФИО").Value
        Form2.TextBox2.Text = Grid1.CurrentRow.Cells("Дата рождения").Value
        Form2.TextBox3.Text = Grid1.CurrentRow.Cells("Телефон").Value

        Form2.ShowDialog()

        s1 = Form2.TextBox1.Text
        s2 = Form2.TextBox2.Text
        s3 = Form2.TextBox3.Text
        r = Form2.DialogResult

        Form2.Close()

        If r <> DialogResult.OK Then
            Exit Sub
        End If

        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "update Список set ФИО='" & s1 & "', Дата рождения='" & s2 & "', Телефон= '" & s3 & "' where код=" & k
        c.ExecuteNonQuery()

        RefreshGrid()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Dim ds As New DataSet
        ds = Grid1.DataSource

        Dim tab As DataTable
        tab = ds.Tables(0)

        Dim row As DataRow
        row = tab.NewRow

        Dim r As DialogResult

        Form2.ShowDialog()
        row.Item("ФИО") = Form2.TextBox1.Text
        row.Item("Дата рождения") = Form2.TextBox2.Text
        row.Item("Телефон") = Form2.TextBox3.Text
        r = Form2.DialogResult
        Form2.Close()

        If r <> DialogResult.OK Then
            Exit Sub
        End If

        tab.Rows.Add(row)
        Dim b As New OleDbCommandBuilder(DA1)
        DA1.InsertCommand = b.GetInsertCommand

        DA1.Update(ds, "Список")
        RefreshGrid()


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If MsgBox("Вы хотите удалить строку?", MsgBoxStyle.OkCancel) <> MsgBoxResult.Ok Then
            Exit Sub
        End If
        Dim ds As New DataSet
        ds = Grid1.DataSource

        Dim tab As DataTable
        tab = ds.Tables(0)


        tab.Rows(Grid1.CurrentRow.Index).Delete()

        Dim b As New OleDbCommandBuilder(DA1)
        DA1.DeleteCommand = b.GetDeleteCommand

        DA1.Update(ds, "Список")
        RefreshGrid()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim ds As New DataSet
        ds = Grid1.DataSource

        Dim tab As DataTable
        tab = ds.Tables(0)

        Dim row As DataRow
        row = tab.NewRow

        Dim r As DialogResult

        Dim k As Integer
        k = Grid1.CurrentRow.Cells("Код").Value
        Form2.TextBox1.Text = Grid1.CurrentRow.Cells("ФИО").Value
        Form2.TextBox2.Text = Grid1.CurrentRow.Cells("Дата рождения").Value
        Form2.TextBox3.Text = Grid1.CurrentRow.Cells("Телефон").Value

        row = tab.Rows(Grid1.CurrentRow.Index)
        Form2.ShowDialog()
        row.Item("ФИО") = Form2.TextBox1.Text
        row.Item("Дата рождения") = Form2.TextBox2.Text
        row.Item("Телефон") = Form2.TextBox3.Text
        r = Form2.DialogResult
        Form2.Close()

        If r <> DialogResult.OK Then
            Exit Sub
        End If


        Dim b As New OleDbCommandBuilder(DA1)
        DA1.UpdateCommand = b.GetUpdateCommand

        DA1.Update(ds, "Список")
        RefreshGrid()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim c As New OleDbCommand("select ФИО from Список", conn)
        Dim dr As OleDbDataReader = c.ExecuteReader
        ComboBox1.Items.Clear()
        While dr.Read()
            ComboBox1.Items.Add(dr(0))

        End While

        dr.Close()

    End Sub
End Class
