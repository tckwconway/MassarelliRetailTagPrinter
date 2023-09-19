Imports System.Environment
Imports System.Data.SqlClient
Imports System.Configuration


Module MacolaStartup

    Friend cn As SqlConnection
    Public DefaultServer As String = My.Settings.DefaultSERVER
    Public DefaultDB As String = My.Settings.DefaultDB

    Public Sub MacStartup(db As String)

        Try

            Dim ConnStr As String = "Data Source=" & DefaultServer & ";Initial Catalog=" & DefaultDB & ";Persist Security Info=True;User ID=sa;Password=C@sT1nST0nE"

            cn = New SqlConnection
            cn.ConnectionString = ConnStr
            cn.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim st As Integer = cn.State

    End Sub

End Module
