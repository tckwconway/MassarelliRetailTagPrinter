Imports System.Runtime.InteropServices.Marshal
Imports System.Runtime.InteropServices
Imports Seagull.BarTender.Print
Imports Seagull.BarTender.Print.Database
Imports Seagull.BarTender.Print.Message
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Reflection

Public Class ctlBartender

    Public format As LabelFormatDocument ' The format that will be exported.
    Public formatseparator As LabelFormatDocument
    Public engine As New Engine  ' The BarTender Print Engine.
    Public bto As New BartenderOptions
    Private Const appName As String = "Picked Orders Label Printing"
    Public Event PrintLables()

    Private Sub SetUpControls()
        'List the Local Printers
        Dim printers As New Printers()
        For Each printer As Printer In printers
            cboPrinters.Items.Add(printer.PrinterName)
        Next printer


        Try
            engine = New Engine(True)
        Catch exception As PrintEngineException
            ' If the engine is unable to start, a PrintEngineException will be thrown.
            MessageBox.Show(Me, exception.Message, appName)
            'Me.Close() ' Close this app. We cannot run without connection to an engine.
            Return
        End Try

        With My.Settings
            bto.PrinterName = .Printer
            bto.LabelPathFile = .Label
            bto.SeparatorPathFile = .Separator
            txtLabel.Text = My.Settings.Label
            txtSeparator.Text = .Separator
            cboPrinters.Text = .Printer
        End With

        SetButton(btnPrint)
       
    End Sub


#Region "   BarTender   "

#Region "   Methods   "

    Private Sub OpenBartenderFormat(op As OpenFileDialog)

        ' Close the previous format.
        Try
            If format IsNot Nothing Then
                CloseBartenderLabelformat()
            End If
        Catch ex As Exception

        End Try

        bto.LabelPathFile = op.FileName

    End Sub
    Private Sub CloseBartenderLabelformat()
        ' Close the previous format.

        Try
            If format IsNot Nothing Then
                format.Close(SaveOptions.DoNotSaveChanges)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub OpenBartenderSeparator(op As OpenFileDialog)

        ' Close the previous Separator.
        Try
            If formatseparator IsNot Nothing Then
                CloseBartenderLabelSeparator()
            End If
        Catch ex As Exception

        End Try

        bto.SeparatorPathFile = op.FileName

    End Sub


    Private Sub CloseBartenderLabelSeparator()
        ' Close the previous format.

        Try
            If formatseparator IsNot Nothing Then
                formatseparator.Close(SaveOptions.DoNotSaveChanges)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub PreviewLabel(prntrName As String, fpreviewPath As String, fdataPath As String)

        Try
            If format IsNot Nothing Then
                format.Close(SaveOptions.DoNotSaveChanges)
            End If
        Catch ex As Exception

        End Try


        Try
            format = engine.Documents.Open(bto.LabelPathFile)
            format.PrintSetup.PrinterName = bto.PrinterName
            Dim str As String
            For Each substring As SubString In format.SubStrings
                str = substring.Name
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

#End Region

#End Region

    Private Sub ctlBartender_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SetUpControls()
    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        RaiseEvent PrintLables()
    End Sub

    Private Sub cboPrinters_DisplayMemberChanged1(sender As Object, e As System.EventArgs) Handles cboPrinters.DisplayMemberChanged
        My.Settings.Printer = cboPrinters.Text
        My.Settings.Save()
        bto.PrinterName = cboPrinters.Text
        SetButton(btnPrint)
    End Sub


    Private Sub cboPrinters_DisplayMemberChanged(sender As Object, e As System.EventArgs)
        My.Settings.Printer = cboPrinters.Text
        My.Settings.Save()
        bto.PrinterName = cboPrinters.Text
    End Sub
    Private Sub btnLoadBartenderLabel_Click(sender As System.Object, e As System.EventArgs) Handles btnLoadBartenderLabel.Click
        Dim op As OpenFileDialog = DirectCast(Me.OpenFileDialogBartender, OpenFileDialog)

        If op.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        Else
            OpenBartenderFormat(op)
            op.Dispose()
        End If
        My.Settings.Label = op.FileName
        My.Settings.Save()
        txtLabel.Text = My.Settings.Label
        bto.LabelPathFile = My.Settings.Label
        SetButton(btnPrint)
    End Sub
    Private Sub btnLoadBartenderSeparator_Click(sender As System.Object, e As System.EventArgs) Handles btnLoadBartenderSeparator.Click
        Dim op As OpenFileDialog = DirectCast(Me.OpenFileDialogBartender, OpenFileDialog)

        If op.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        Else
            OpenBartenderFormat(op)
            op.Dispose()
        End If
        My.Settings.Separator = op.FileName
        My.Settings.Save()
        txtSeparator.Text = My.Settings.Separator
        bto.SeparatorPathFile = My.Settings.Separator
        SetButton(btnPrint)
    End Sub
    Private Sub SetButton(btn As Button)
        'If btn.Name = "btnPrint" Then
        '    If My.Settings.Label > "" And My.Settings.Printer > "" Then
        '        btn.Enabled = True
        '    Else
        '        btn.Enabled = False
        '    End If
        'End If
    End Sub


    Public Sub Print(itm As Item)
        Try
            If format IsNot Nothing Then
                format.Close(SaveOptions.DoNotSaveChanges)
            End If
        Catch ex As Exception

        End Try

        Try
            format = engine.Documents.Open(bto.LabelPathFile)
            format.PrintSetup.PrinterName = bto.PrinterName
            For i As Integer = 0 To CInt(itm.Qty)
                Dim str As String
                For Each substring As SubString In format.SubStrings
                    str = substring.Name
                    Select Case str
                        Case "item_no"
                            substring.Value = itm.ItemNo
                        Case "ship_to_name"
                            substring.Value = itm.CusName
                        Case "item_desc"
                            substring.Value = itm.Description
                        Case "finish"
                            substring.Value = itm.Finish
                        Case "unit_price"
                            substring.Value = itm.UnitPrice
                        Case "status"
                            substring.Value = itm.Status
                        Case "upc"
                            substring.Value = itm.UPC
                    End Select
                Next
                format.Print()

            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            

            MsgBox("")
        End Try

    End Sub

    Public Function Print(dt As DataTable, cn As SqlConnection) As Boolean
        Dim itm As New Item
        Dim ctr As Integer = 0
        Dim str As String
        Dim counter As Integer = 0
        Dim labelcount As Integer = 0
        Dim ord_no As String = ""
        Try

            If format IsNot Nothing Then
                Try
                    format.Close(SaveOptions.DoNotSaveChanges)
                Catch ex As Exception

                End Try

            End If
            If formatseparator IsNot Nothing Then
                Try
                    formatseparator.Close(SaveOptions.DoNotSaveChanges)
                Catch ex As Exception

                End Try

            End If

        Catch ex As Exception

        End Try


        Try
            format = engine.Documents.Open(bto.LabelPathFile)
            format.PrintSetup.PrinterName = bto.PrinterName
            formatseparator = engine.Documents.Open(bto.SeparatorPathFile)
            formatseparator.PrintSetup.PrinterName = bto.PrinterName

            'Print Separator ...

            If dt.Rows(0)("START_ship_to_name") Is DBNull.Value Then itm.STARTShipToName = "" Else itm.STARTShipToName = dt.Rows(0)("START_ship_to_name").trim
            If dt.Rows(0)("START_cus_no") Is DBNull.Value Then itm.STARTCusNo = "" Else itm.STARTCusNo = dt.Rows(0)("START_cus_no").trim
            If dt.Rows(0)("START_ord_no") Is DBNull.Value Then itm.STARTOrdNo = "" Else itm.STARTOrdNo = dt.Rows(0)("START_ord_no").trim
            ' Debug.Print(dt.Rows(0)(2).ToString)
            ord_no = dt.Rows(0)(2).ToString
            For Each substring As SubString In formatseparator.SubStrings
                Str = substring.Name
                Select Case str
                    Case "START_ship_to_name"
                        substring.Value = itm.STARTShipToName
                    Case "START_cus_no"
                        substring.Value = itm.STARTCusNo
                    Case "START_ord_no"
                        substring.Value = itm.STARTOrdNo
                End Select
            Next

            formatseparator.Print()

            With itm
                'populate itm with the values from the data table
                For Each rw As DataRow In dt.Rows
                    Debug.Print(dt.Rows(0)(2).ToString)
                    Debug.Print(rw("item_no").ToString)

                    If rw("selected") Is DBNull.Value Or rw("selected") = 0 Then Continue For 'If the row is checked, go ahead and print the tag...

                    If rw("ship_to_name") Is DBNull.Value Then itm.CusName = "" Else itm.CusName = rw("ship_to_name")
                    If rw("ord_no") Is DBNull.Value Then itm.OrderNo = "" Else itm.OrderNo = rw("ord_no")
                    If rw("qty_to_ship") Is DBNull.Value Then itm.Qty = "" Else itm.Qty = rw("qty_to_ship")
                    If rw("item_no") Is DBNull.Value Then itm.ItemNo = "" Else itm.ItemNo = rw("item_no")
                    If rw("item_desc") Is DBNull.Value Then itm.Description = "" Else itm.Description = rw("item_desc")
                    If rw("finish") Is DBNull.Value Then itm.Finish = "" Else itm.Finish = rw("finish")
                    If rw("unit_price") Is DBNull.Value Then itm.UnitPrice = "" Else itm.UnitPrice = rw("unit_price") '.ToString.Substring(1, rw("unit_price").ToString.Length - 5)
                    If rw("upc") Is DBNull.Value Then itm.UPC = "" Else itm.UPC = rw("upc")
                    If rw("CatalogPage") Is DBNull.Value Then itm.CatalogPage = "" Else itm.CatalogPage = rw("CatalogPage")
                    If rw("Detail_Finish_type") Is DBNull.Value Then itm.Detail_Finish_Type = "" Else itm.Detail_Finish_Type = rw("Detail_Finish_type")
                    If rw("status") Is DBNull.Value Then itm.Status = "" Else itm.Status = rw("status")
                    If rw("barcode") Is DBNull.Value Then itm.Barcode = "" Else itm.Barcode = rw("barcode").trim
                    If rw("START_ship_to_name") Is DBNull.Value Then itm.STARTShipToName = "" Else itm.STARTShipToName = rw("START_ship_to_name").trim
                    If rw("START_cus_no") Is DBNull.Value Then itm.STARTCusNo = "" Else itm.STARTCusNo = rw("START_cus_no").trim
                    If rw("START_ord_no") Is DBNull.Value Then itm.STARTOrdNo = "" Else itm.STARTOrdNo = rw("START_ord_no").trim

                    'print labels based on qty ordered - NOTE: Use For i = 1 rather than i = 0, this allows printing START and END (each with qty of 1), 
                    'and allows actual ordered qty of item, (if i = 0, then it loops twice, 0 and 1, since i has to equal the Qty ordered).
                    For i As Integer = 1 To CInt(itm.Qty)
                        Debug.Print(rw("item_no").ToString)
                        Try

                            For Each substring As SubString In format.SubStrings
                                str = substring.Name.Trim
                                Select Case str
                                    Case "item_no"
                                        substring.Value = itm.ItemNo
                                    Case "ship_to_name"
                                        substring.Value = itm.CusName
                                    Case "item_desc_1"
                                        substring.Value = itm.Description
                                    Case "pick_seq"
                                        substring.Value = itm.Finish
                                    Case "Retail_Price"
                                        substring.Value = itm.UnitPrice
                                    Case "unit_price"
                                        substring.Value = itm.UnitPrice
                                    Case "status"
                                        substring.Value = itm.Status
                                    Case "UPCBarcode"
                                        If itm.Barcode = "Y" Then
                                            substring.Value = itm.UPC
                                        Else
                                            substring.Value = ""
                                        End If
                                    Case "CatalogPage"
                                        substring.Value = itm.CatalogPage
                                    Case "Detail_Finish_type"
                                        substring.Value = itm.Detail_Finish_Type.ToString.Trim
                                    Case "START_ship_to_name"
                                        substring.Value = itm.STARTShipToName
                                    Case "START_cus_no"
                                        substring.Value = itm.STARTCusNo
                                    Case "START_ord_no"
                                        substring.Value = itm.STARTOrdNo
                                End Select

                            Next
                        Catch ex As Exception

                        End Try
                        'For Each substring As SubString In format.SubStrings
                        '    str = substring.Name
                        '    Select Case str
                        '        Case "item_no"
                        '            substring.Value = itm.ItemNo
                        '        Case "ship_to_name"
                        '            substring.Value = itm.CusName
                        '        Case "item_desc_1"
                        '            substring.Value = itm.Description
                        '        Case "pick_seq"
                        '            substring.Value = itm.Finish
                        '        Case "Retail_Price"
                        '            substring.Value = itm.UnitPrice
                        '        Case "unit_price"
                        '            substring.Value = itm.UnitPrice
                        '        Case "status"
                        '            substring.Value = itm.Status
                        '        Case "UPCBarcode"
                        '            If itm.Barcode = "Y" Then
                        '                substring.Value = itm.UPC
                        '            Else
                        '                substring.Value = ""
                        '            End If
                        '        Case "CatalogPage"
                        '            substring.Value = itm.CatalogPage
                        '        Case "Detail_Finish_type"
                        '            substring.Value = itm.Detail_Finish_Type
                        '        Case "START_ship_to_name"
                        '            substring.Value = itm.STARTShipToName
                        '        Case "START_cus_no"
                        '            substring.Value = itm.STARTCusNo
                        '        Case "START_ord_no"
                        '            substring.Value = itm.STARTOrdNo
                        '    End Select

                        'Next
                        counter = counter + 1
                        ' format.ExportPrintPreviewToFile("c:\backup", "LabelTest", ImageType.JPEG, ColorDepth.ColorDepth16, Seagull.BarTender.Print.Resolution.AutoDimension, Color.AliceBlue, OverwriteOptions.DoNotOverwrite, False, False, Seagull.BarTender.Print.Message)
                        Try
                            format.Print()
                        Catch ex As Exception
                            MsgBox(ex.Message)

                        End Try
                        
                    Next
                    labelcount = labelcount + counter
                Next

            End With
            MsgBox("Total Labels Printed: " & labelcount.ToString)
            MarkAsPrinted(itm.OrderNo, cn)

        Catch ex As Exception

            ' get the item information if there's an error
            Dim itmerr As New StringBuilder
            Dim itmtype As Type = itm.GetType()
            Dim itmproperties() As PropertyInfo = itmtype.GetProperties '(flags)

            For Each itmproperty As PropertyInfo In itmproperties
                With itmerr
                    .Append(itmproperty.Name & ": " & itmproperty.GetValue(itm, Nothing) & vbCrLf)
                End With
            Next

            MsgBox("Error - Label Details on the Item that Failed: " & vbCrLf & vbCrLf & itmerr.ToString)

            MsgBox(ex.Message)

            Return True
        End Try
        'MsgBox("Done: Order# " & ord_no & " printed " & counter.ToString & " labels.")
        Return False

    End Function

    Public Sub MarkAsPrinted(ord_no As String, cn As SqlConnection)
        Dim sSQL As String = "Update OEORDHDR_SQL set User_def_fld_1 = 'Y' where ord_no = '" & ord_no.Trim & "'"
        DAC.Execute_NonSQL(sSQL, cn)
    End Sub

End Class

Public Class BartenderOptions
    Public LabelPathFile As String
    Public SeparatorPathFile As String
    Public PrinterName As String
    Public Label As String
    Public Separator As String


    Public Sub Clear()
        LabelPathFile = ""
        PrinterName = ""
    End Sub
    Public Sub New()
        LabelPathFile = ""
        PrinterName = ""
        Label = ""
        SeparatorPathFile = ""
        Separator = ""
    End Sub

End Class
