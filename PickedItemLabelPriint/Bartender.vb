Imports Seagull.BarTender.Print
Imports Seagull.BarTender.Print.Database
Imports Seagull.BarTender.Print.Message
Imports System.IO

Public Class Bartender
    '    Public format As LabelFormatDocument = Nothing ' The format that will be exported.
    '    Public engine As Engine = Nothing ' The BarTender Print Engine.
    '    Public bto As New BartenderOptions

    '#Region "   BarTender   "

    '#Region "   Methods   "

    '    Private Sub OpenBartenderFormat(op As OpenFileDialog)

    '        ' Close the previous format.
    '        Try
    '            If format IsNot Nothing Then
    '                CloseBartenderLabelformat()
    '            End If
    '        Catch ex As Exception

    '        End Try

    '        bto.LabelPathFile = op.FileName

    '    End Sub

    '    Private Sub CloseBartenderLabelformat()
    '        ' Close the previous format.

    '        Try
    '            If format IsNot Nothing Then
    '                format.Close(SaveOptions.DoNotSaveChanges)
    '            End If

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Public Sub PreviewLabel(prntrName As String, fpreviewPath As String, fdataPath As String)

    '        Try
    '            If format IsNot Nothing Then
    '                format.Close(SaveOptions.DoNotSaveChanges)
    '            End If
    '        Catch ex As Exception

    '        End Try


    '        Try
    '            format = engine.Documents.Open(bto.LabelPathFile)
    '            format.PrintSetup.PrinterName = bto.PrinterName

    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try

    '    End Sub

    '    Private Function CreateItemLabelsToPrintDataTable() As DataTable
    '        'Create datatable
    '        Dim oLabelData As New DataTable("LabelData")

    '        oLabelData.Columns.Add("Prnt", GetType(Boolean))
    '        oLabelData.Columns.Add("SKU", GetType(String))
    '        oLabelData.Columns.Add("Description", GetType(String))
    '        oLabelData.Columns.Add("Retail", GetType(Decimal))
    '        oLabelData.Columns.Add("MfgPart", GetType(String))
    '        oLabelData.Columns.Add("MfgFinish", GetType(String))
    '        oLabelData.Columns.Add("QtyOrd", GetType(Decimal))

    '        Return oLabelData

    '    End Function


    '#End Region

    '#End Region

    'End Class
    'Public Class BartenderOptions
    '    Public LabelPathFile As String
    '    Public PrinterName

    '    Public Sub Clear()
    '        LabelPathFile = ""
    '        PrinterName = ""
    '    End Sub

End Class