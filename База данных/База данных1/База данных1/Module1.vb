Imports System.Data.OleDb
Module Module1

    Public conn As OleDbConnection
    Public DA1 As OleDbDataAdapter


    Public Sub FillGrid(Grid1 As DataGridView, cmd As String, TableName As String)
        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = cmd

        Dim ds As New DataSet
        Dim da As New OleDbDataAdapter(c)
        da.Fill(ds, TableName)
        Grid1.DataSource = ds
        Grid1.DataMember = TableName


    End Sub

    Public Sub FillGridDA(Grid1 As DataGridView, cmd As String, TableName As String, ByRef da As OleDbDataAdapter)
        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = cmd

        Dim ds As New DataSet
        'Dim da As New OleDbDataAdapter(c)
        da = New OleDbDataAdapter(c)
        da.Fill(ds, TableName)
        Grid1.DataSource = ds
        Grid1.DataMember = TableName


    End Sub


End Module
