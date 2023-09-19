Imports Seagull.BarTender.Print
Imports Seagull.BarTender.Print.Database
Imports Seagull.BarTender.Print.Message
Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Configuration


Public Class PickedOrder
    Private TotalCount As Integer = 0
    'Private cOptionalCriteria As OptionalCriteria
    Const sCheckMark1 As Char = ChrW(&H2611)  'Check with box
    Const sCheckMark2 As Char = ChrW(&H2713)  'Light check mark
    Const sCheckMark3 As Char = ChrW(&H2714)  'Heavy check mark
    Const sGlyphDown As Char = ChrW(&H25BC) 'Glyph (down pointing triangle)
    Const sGlyphUp As Char = ChrW(&H25B2) 'Glyph (up pointing triangle)
    Const sHeavyMultiplicationX As Char = ChrW(&H2716) 'Heavy multiplication x

    Private dtItems As New DataTable
    Private dtOrders As New DataTable
    Private dtPrintItems As DataTable
    Private dvItems As DataView
    Private dtItemsReprint As DataTable
    Private dtOrdersReprint As DataTable
    Private dtPrintItemsReprint As DataTable
    Private dtReprint As DataTable

    Private BtnColor As Color = Color.Cornsilk

    Private bIsLoading As Boolean = True
    Private bLoading As Boolean = True
    Private bCancelPrint As Boolean = False
    Private bIsPrinting As Boolean = False
    Private bClearAll As Boolean = True
    Private bEnableRefreshDB As Boolean '= False
    Private bEnableSaveDB As Boolean '= True
    Private bEnableCtls As Boolean '= True

    Private iLoadType As Integer
    Private iLastRowSelected As Integer
    Private cOptionalCriteria = New OptionalCriteria
    Private header_style As New DataGridViewCellStyle

    Private ordno As String

    Private srt As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending

    Private Enum LoadType As Integer
        LoadPrint = 1
        LoadRePrint = 2
    End Enum
    ' added error handling here to so we can see issues when they come up 
    Public Sub New()
        AddHandler Application.ThreadException, AddressOf OnThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionEventRaised

        InitializeComponent()
    End Sub

    Private Sub UnhandledExceptionEventRaised(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        If e.IsTerminating Then
            Dim o As Object = e.ExceptionObject
            MessageBox.Show(o.ToString) ' use EventLog instead
        End If
    End Sub

    Private Sub OnThreadException(ByVal sender As Object,
                       ByVal e As ThreadExceptionEventArgs)
        ' This is where you handle the exception
        MessageBox.Show(e.Exception.Message)
    End Sub

    Private Sub LoadGrids()

        With DataGridView1

            .AllowUserToResizeRows = False
            .AllowUserToAddRows = False
            .RowHeadersVisible = False
            .DataSource = Nothing
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect

            If .RowCount > 0 Then .Rows.Clear()
            If .ColumnCount > 0 Then .Columns.Clear()

            Dim chkColumn As New DataGridViewCheckBoxColumn
            With chkColumn
                .DataPropertyName = "selected"
                .Name = "Selected"
                .HeaderText = "   " & sCheckMark3

                '.HeaderText = sGlyphUp
                .MinimumWidth = 22
                .ToolTipText = "Click to Sort : Dbl-Click to Check/UnCheck All"
                .Width = 45

                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(chkColumn)


            With .Columns(.Columns.Add("Printed", "Printed"))
                .MinimumWidth = 30
                .ToolTipText = "Labels have previously been printed"
                .DataPropertyName = "printed"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 45
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("OrderNo", "Order No"))
                .MinimumWidth = 45
                .ToolTipText = "Order No"
                .DataPropertyName = "ord_no"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 80
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("PickedDt", "Picked Date"))
                .MinimumWidth = 100
                .DataPropertyName = "picked_dt"
                .ToolTipText = "Picked Date"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 50
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("ShipToName", "Ship To Name"))
                .MinimumWidth = 100
                .DataPropertyName = "ship_to_name"
                .ToolTipText = "Ship To Name"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 80
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("ShipToState", "State"))
                .ToolTipText = "Ship To State"
                .DataPropertyName = "ship_to_state"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 60
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("ShipToCusNo", "Cust #"))
                .ToolTipText = "Ship To Customer Number"
                .DataPropertyName = "ship_to_cus_no"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 90
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("RetailLabels", "Lbls"))
                .ToolTipText = "Retail Labels Requested"
                .DataPropertyName = "retail_labels"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 35
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("Barcode", "Barcode"))
                .ToolTipText = "Barcode Required"
                .DataPropertyName = "barcode"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    '.WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 55
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("Amount", "Amt"))
                .ToolTipText = "Retail Amount"
                .DataPropertyName = "user_amount"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                    .WrapMode = DataGridViewTriState.True
                    .Format = "N2"
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 50
                .ReadOnly = True
            End With


            'Turn of sorting
            Dim i As Integer
            For i = 0 To .Columns.Count - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i
        End With

        LoadDataGridView2(header_style)

        'With DataGridView2

        '    .ReadOnly = False
        '    .AllowUserToResizeRows = False
        '    .AllowUserToAddRows = False
        '    .RowHeadersVisible = False
        '    .DataSource = Nothing
        '    If .RowCount > 0 Then .Rows.Clear()
        '    If .ColumnCount > 0 Then .Columns.Clear()

        '    Dim chkItemCol As New DataGridViewCheckBoxColumn
        '    With chkItemCol
        '        .DataPropertyName = "selected"
        '        .Name = "Selected"
        '        .HeaderText = "   " & sCheckMark3
        '        .MinimumWidth = 22
        '        .ToolTipText = "Click to Sort : Dbl-Click to Check/UnCheck All"
        '        .Width = 45
        '        .SortMode = DataGridViewColumnSortMode.NotSortable
        '    End With
        '    .Columns.Add(chkItemCol)

        '    With .Columns(.Columns.Add("ShipToName", "Ship To Name"))
        '        .MinimumWidth = 100
        '        .DataPropertyName = "ship_to_name"
        '        .ToolTipText = "Ship To Name"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleLeft
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        '.Width = 180
        '        .Visible = False
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("OrderNo", "OrderNo"))
        '        .DataPropertyName = "ord_no"
        '        .Visible = False
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("Quantity", "Qty"))
        '        .ToolTipText = "Quantity"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        .DataPropertyName = "qty_to_ship"
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '            .WrapMode = DataGridViewTriState.True
        '            .Format = "N0"
        '        End With
        '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 55
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("ItemNo", "ItemNo"))
        '        .ToolTipText = "ItemNo"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        .DataPropertyName = "item_no"
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleLeft
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 80
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("Description", "Description"))
        '        .ToolTipText = "Description"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        .DataPropertyName = "item_desc"
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleLeft
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        '.Width = 280
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("UnitPrice", "UnitPrice"))
        '        .ToolTipText = "UnitPrice"
        '        .DataPropertyName = "unit_price"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleRight
        '            .WrapMode = DataGridViewTriState.True
        '            .Format = "N2"
        '            '.Format = FA_Formatting.FormatString(FA_Formatting.FA_DateTypes.YtoD)
        '        End With
        '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 70
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("UPC", "UPC"))
        '        .ToolTipText = "UPC"
        '        .DataPropertyName = "upc"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleRight
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        ' .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 90
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("FnshCd", "Code"))
        '        .ToolTipText = "Finish Color Code"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        .DataPropertyName = "finish"
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 55
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("Finish", "Finish"))
        '        .MinimumWidth = 80
        '        .ToolTipText = "Finish/Color Description"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        .DataPropertyName = "Detail_Finish_type"
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleLeft
        '            .WrapMode = DataGridViewTriState.False
        '        End With
        '        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 55
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("Page", "Page"))
        '        .MinimumWidth = 40
        '        .ToolTipText = "Catalog Page Number"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        .DataPropertyName = "CatalogPage"
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 60
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("Status", sGlyphUp & " Status"))
        '        .MinimumWidth = 30
        '        .ToolTipText = "Default Sort is Status : It puts START, Items, END in correct order."
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        .DataPropertyName = "status"
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 80
        '        .ReadOnly = True
        '        .HeaderCell.Style = header_style
        '        '.DisplayIndex = 11
        '    End With

        '    With .Columns(.Columns.Add("Barcode", "Brcd"))
        '        .ToolTipText = "Barcode Required"
        '        .DataPropertyName = "barcode"
        '        .HeaderCell.ToolTipText = .ToolTipText
        '        With .DefaultCellStyle
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '            .WrapMode = DataGridViewTriState.True
        '        End With
        '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        '        .Width = 55
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("START_ShipToName", "START_ShipToName"))
        '        .DataPropertyName = "START_ship_to_name"
        '        .Visible = False
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("START_OrderNo", "START_OrderNo"))
        '        .DataPropertyName = "START_ord_no"
        '        .Visible = False
        '        .ReadOnly = True
        '    End With

        '    With .Columns(.Columns.Add("START_CustNo", "START_CustNo"))
        '        .DataPropertyName = "START_cus_no"
        '        .Visible = False
        '        .ReadOnly = True
        '    End With

        '    'Turn of sorting
        '    Dim i As Integer
        '    For i = 0 To .Columns.Count - 1
        '        .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        '    Next i

        '    .EnableHeadersVisualStyles = False

        'End With

        With dtItems
            .Columns.Add("selected", GetType(Integer))
            .Columns.Add("ship_to_name", GetType(String))
            .Columns.Add("ord_no", GetType(String))
            .Columns.Add("qty_to_ship", GetType(String))
            .Columns.Add("item_no", GetType(String))
            .Columns.Add("item_desc", GetType(String))
            .Columns.Add("finish", GetType(String))
            .Columns.Add("unit_price", GetType(String))
            .Columns.Add("upc", GetType(String))
            .Columns.Add("Detail_Finish_type", GetType(String))
            .Columns.Add("CatalogPage", GetType(String))
            .Columns.Add("status", GetType(String))
            .Columns.Add("barcode", GetType(String))
            .Columns.Add("START_ship_to_name", GetType(String))
            .Columns.Add("START_cus_no", GetType(String))
            .Columns.Add("START_ord_no", GetType(String))
        End With

        With dtOrders
            .Columns.Add("selected", GetType(Integer))
            .Columns.Add("printed", GetType(String))
            .Columns.Add("ord_no", GetType(String))
            .Columns.Add("picked_dt", GetType(String))
            .Columns.Add("ship_to_cus_no", GetType(String))
            .Columns.Add("ship_to_name", GetType(String))
            .Columns.Add("ship_to_state", GetType(String))
            .Columns.Add("retail_labels", GetType(String))
            .Columns.Add("barcode", GetType(String))
            .Columns.Add("user_amount", GetType(String))
        End With

    End Sub

    Private Sub LoadDataGridView2(header_style As DataGridViewCellStyle)
        With DataGridView2

            .ReadOnly = False
            .AllowUserToResizeRows = False
            .AllowUserToAddRows = False
            .RowHeadersVisible = False
            .DataSource = Nothing
            If .RowCount > 0 Then .Rows.Clear()
            If .ColumnCount > 0 Then .Columns.Clear()

            Dim chkItemCol As New DataGridViewCheckBoxColumn
            With chkItemCol
                .DataPropertyName = "selected"
                .Name = "Selected"
                .HeaderText = "   " & sCheckMark3
                .MinimumWidth = 22
                .ToolTipText = "Click to Sort : Dbl-Click to Check/UnCheck All"
                .Width = 45
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            .Columns.Add(chkItemCol)

            With .Columns(.Columns.Add("ShipToName", "Ship To Name"))
                .MinimumWidth = 100
                .DataPropertyName = "ship_to_name"
                .ToolTipText = "Ship To Name"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.Width = 180
                .Visible = False
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("OrderNo", "OrderNo"))
                .DataPropertyName = "ord_no"
                .Visible = False
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("Quantity", "Qty"))
                .ToolTipText = "Quantity"
                .HeaderCell.ToolTipText = .ToolTipText
                .DataPropertyName = "qty_to_ship"
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    .WrapMode = DataGridViewTriState.True
                    .Format = "N0"
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 55
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("ItemNo", "ItemNo"))
                .ToolTipText = "ItemNo"
                .HeaderCell.ToolTipText = .ToolTipText
                .DataPropertyName = "item_no"
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 80
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("Description", "Description"))
                .ToolTipText = "Description"
                .HeaderCell.ToolTipText = .ToolTipText
                .DataPropertyName = "item_desc"
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.True
                End With
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.Width = 280
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("UnitPrice", "UnitPrice"))
                .ToolTipText = "UnitPrice"
                .DataPropertyName = "unit_price"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                    .WrapMode = DataGridViewTriState.True
                    .Format = "N2"
                    '.Format = FA_Formatting.FormatString(FA_Formatting.FA_DateTypes.YtoD)
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 70
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("UPC", "UPC"))
                .ToolTipText = "UPC"
                .DataPropertyName = "upc"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                    .WrapMode = DataGridViewTriState.True
                End With
                ' .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 90
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("FnshCd", "Code"))
                .ToolTipText = "Finish Color Code"
                .HeaderCell.ToolTipText = .ToolTipText
                .DataPropertyName = "finish"
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 55
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("Finish", "Finish"))
                .MinimumWidth = 80
                .ToolTipText = "Finish/Color Description"
                .HeaderCell.ToolTipText = .ToolTipText
                .DataPropertyName = "Detail_Finish_type"
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .WrapMode = DataGridViewTriState.False
                End With
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 55
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("Page", "Page"))
                .MinimumWidth = 40
                .ToolTipText = "Catalog Page Number"
                .HeaderCell.ToolTipText = .ToolTipText
                .DataPropertyName = "CatalogPage"
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 60
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("Status", sGlyphUp & " Status"))
                .MinimumWidth = 30
                .ToolTipText = "Default Sort is Status : It puts START, Items, END in correct order."
                .HeaderCell.ToolTipText = .ToolTipText
                .DataPropertyName = "status"
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 80
                .ReadOnly = True
                .HeaderCell.Style = header_style
                '.DisplayIndex = 11
            End With

            With .Columns(.Columns.Add("Barcode", "Barcode"))
                .ToolTipText = "Barcode Required"
                .DataPropertyName = "barcode"
                .HeaderCell.ToolTipText = .ToolTipText
                With .DefaultCellStyle
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                    .WrapMode = DataGridViewTriState.True
                End With
                '.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 55
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("START_ShipToName", "START_ShipToName"))
                .DataPropertyName = "START_ship_to_name"
                .Visible = False
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("START_OrderNo", "START_OrderNo"))
                .DataPropertyName = "START_ord_no"
                .Visible = False
                .ReadOnly = True
            End With

            With .Columns(.Columns.Add("START_CustNo", "START_CustNo"))
                .DataPropertyName = "START_cus_no"
                .Visible = False
                .ReadOnly = True
            End With

            'Turn of sorting
            Dim i As Integer
            For i = 0 To .Columns.Count - 1
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            .EnableHeadersVisualStyles = False

        End With
    End Sub

    'Shared Sub ShowConnectionStrings()

    '    ' Get the application configuration file.
    '    Dim config _
    'As System.Configuration.Configuration =
    'ConfigurationManager.OpenExeConfiguration(
    'ConfigurationUserLevel.None)

    '    ' Get the conectionStrings section.
    '    Dim csSection _
    'As ConnectionStringsSection =
    'config.ConnectionStrings

    '    Dim i As Integer
    '    For i = 0 To ConfigurationManager.ConnectionStrings.Count
    '        Dim cs As ConnectionStringSettings =
    '    csSection.ConnectionStrings(i)

    '        Console.WriteLine(
    '    "  Connection String: ""{0}""", cs.ConnectionString)
    '        Console.WriteLine("#{0}", i)
    '        Console.WriteLine("  Name: {0}", cs.Name)

    '        Console.WriteLine(
    '    "  Provider Name: {0}", cs.ProviderName)

    '    Next i

    'End Sub 'ShowConnectionStrings





    Private Sub PickedOrder_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MacStartup(cOptionalCriteria.DBName)
        lblCurrentDB.Text = MacolaStartup.DefaultDB.ToString
        lblDefaultDB.Text = MacolaStartup.DefaultDB.ToString
        lblServer.Text = MacolaStartup.DefaultServer

        cOptionalCriteria.DBName = MacolaStartup.DefaultDB.ToString

        'MacStartup("DATA")
        'LoadAutoComplete()
        lblLabelList.Left = (Me.ClientSize.Width / 2) - (lblLabelList.Width / 2)
        header_style.BackColor = Color.Yellow
        'btnSortCheckBox.Text = "Sort " & sCheckMark3
        btnRemove.Text = "Remove " & sCheckMark2 & " Items"

        LoadGrids()
        listSQLDatabases()
        cbDBList.Text = My.Settings.DefaultDB

        With Timer1
            iLoadType = LoadType.LoadPrint
            .Interval = 50
            .Enabled = True
        End With

        lblCurrentDB.Text = My.Settings.DefaultDB
        cbDBList.Text = My.Settings.DefaultDB
        'Panel2.Height = 109
    End Sub

    Private Sub listSQLDatabases()
        On Error Resume Next

        Dim cmd As New SqlCommand("", cn)
        Dim rdr As SqlDataReader
        cmd.CommandText = "exec sys.sp_databases"

        rdr = cmd.ExecuteReader()
        With cbDBList
            While (rdr.Read())
                If rdr.GetString(0).Substring(0, 4) = "DATA" Then .Items.Add(rdr.GetString(0))

            End While
        End With
        rdr.Dispose()
        cmd.Dispose()

    End Sub

    Private Sub LoadAutoComplete()
        Dim sSQL As String
        Dim dt As New DataTable
        Dim OrderAutoCmplt As New AutoCompleteStringCollection

        'sSQL = "Select distinct RTrim(lTrim(Substring(itm.item_no, 2, 15))) as item_no from IMITMIDX_SQL itm where left(item_no, 1) = 'M' "

        sSQL = "Select Distinct Cast(Cast(rtrim(ltrim(A.ord_no)) as int) as varchar(8)) as ord_no " & vbCrLf _
        & "     from OEORDHDR_SQL A " & vbCrLf _
        & "     join ARCUSFIL_SQL D " & vbCrLf _
        & "     on A.cus_no = D.cus_no " & vbCrLf _
        & "     join OEORDLIN_SQL B " & vbCrLf _
        & "     on A.ord_no = B.ord_no " & vbCrLf _
        & "     and A.ord_type = B.ord_type " & vbCrLf _
        & "     join IMITMIDX_SQL C " & vbCrLf _
        & "     on B.item_no = C.item_no " & vbCrLf _
        & "     left join VBPRDFIN_SQL E " & vbCrLf _
        & "     on E.Pick_Sec = B.pick_seq " & vbCrLf _
        & "     join ARCUSFIL_SQL F " & vbCrLf _
        & "     on F.cus_no = dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) " & vbCrLf _
        & "     where " & vbCrLf _
        & "     lTrim(rTrim(C.prod_cat)) not in  ('001','153','152','154','156','390','200','408','600','700','801','900') " & vbCrLf _
        & "     and D.note_3 = 'Y' " & vbCrLf _
        & "     Order By Cast(Cast(rtrim(ltrim(A.ord_no)) as int) as varchar(8)) " & vbCrLf


        Try
            dt = DAC.ExecuteSQL_DataTable(sSQL, cn, "OrdersAutoComplete")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'Use Link to get the Array ...
        Dim itms = (From row In dt Select itmno = row(0).ToString).ToArray

        OrderAutoCmplt.AddRange(itms)
        With txtOrderSearch
            .AutoCompleteCustomSource = OrderAutoCmplt
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
        End With

    End Sub

    Private Function GetRetailLableYN(ordno As String) As String
        Dim onote_3 As Object = Nothing
        Dim note_3 As String = "" ' Retail Label YN
        Dim sSQL As String = ""
        Dim cusno As String = ""
        Dim altcd As String = ""

        sSQL = " select cus_no,  isnull(cus_alt_adr_cd, '') cus_alt_adr_cd " & vbCrLf _
             & " from oeordhdr_sql where ord_no = '" & ordno & "' " & vbCrLf _
             & " UNION " & vbCrLf _
             & " select cus_no,  isnull(cus_alt_adr_cd, '') cus_alt_adr_cd " & vbCrLf _
             & " from oehdrhst_sql where ord_no = '" & ordno & "' " & vbCrLf

        Dim dt As DataTable = DAC.ExecuteSQL_DataTable(sSQL, cn, "RetailLabelYN")
        If dt.Rows.Count = 0 Then
            Return "Order " & ordno & " was not found.  Check that the Order Number is correct."
        End If
        cusno = dt(0)(0).ToString.Trim

        If dt(0)(1).ToString.Trim = "" Then
            altcd = ""
        Else
            altcd = dt(0)(1).ToString.Trim.Substring(dt(0)(1).ToString.Trim.Length - 12)
        End If

        'This handles the various Address possibilities ....
        ' 1 - If a cus_no for and alt_cus_addr_cd, then try the cus_no and cus_alt_adr_cd combined to find note_3 on the parent cus_no record
        '     ...need to add '000' to the altaddr for the 15 char cus_alt_adr_cd...
        If altcd > "" Then
            sSQL = "select A.note_3 from ARCUSFIL_SQL A join ARALTADR_SQL B on A.cus_no = B.cus_no where A.cus_no = '" & cusno & "' and B.cus_alt_adr_cd = '000" & altcd & "'"
            note_3 = DAC.Execute_Scalar(sSQL, cn)
            If note_3 Is Nothing And (cusno <> "000000008553" Or cusno <> "000000008554" Or cusno <> "000000008504") Then
                ' 2 - Sometimes the Distributor does not have retail labels, but the customer record does.  So check the Alt_cus_adr_cd with the cus_no and warn them first before printing
                sSQL = "select A.note_3 from ARCUSFIL_SQL A where A.cus_no = '" & altcd & "'"
            End If
        Else
            ' 3 - No Alt Addr code, so look directly at the cus_no in ARCUSFIL_SQL for note_3
            sSQL = "select isnull(A.note_3, '') note_3 from ARCUSFIL_SQL A where A.cus_no = '" & cusno & "'"
            note_3 = DAC.Execute_Scalar(sSQL, cn).ToString.Trim
        End If


        '' 1 - There is and Alt Addr (altcd > ""), see if the Alt Addr has a corresponding CustID in ARCUSFIL_SQL which will have note_3 populated
        'If altcd > "" Then
        '    sSQL = "Select cus.note_3 from ARCUSFIL_SQL cus join ARALTADR_SQL alt on cus.cus_no = Substring(alt.cus_alt_adr_cd , 4, 12)  " _
        '             & "Where alt.cus_no = '" & cusno & "' and alt.cus_alt_adr_cd = '000" & altcd & "'"
        '    note_3 = DAC.Execute_Scalar(sSQL, cn)
        '    ' 2 - If a corresponding cus_no for the alt_cus_addr_cd does not exist, then try the cus_no and cus_alt_adr_cd combined to find note_3 on the parent cus_no record
        '    '     ...need to add '000' to the altaddr for the 15 char cus_alt_adr_cd...
        '    If note_3 Is Nothing Then
        '        sSQL = "select IsNull(A.note_3, '') as note_3 from ARCUSFIL_SQL A where A.cus_no = '" & altcd & "'"
        '        note_3 = DAC.Execute_Scalar(sSQL, cn)
        '    End If
        'Else

        'End If

        ''This handles the various Address possibilities ....
        '' 1 - There is and Alt Addr (altcd > ""), see if the Alt Addr has a corresponding CustID in ARCUSFIL_SQL which will have note_3 populated
        'If altcd > "" Then
        '    sSQL = "Select cus.note_3 from ARCUSFIL_SQL cus join ARALTADR_SQL alt on cus.cus_no = Substring(alt.cus_alt_adr_cd , 4, 12)  " _
        '             & "Where alt.cus_no = '" & cusno & "' and alt.cus_alt_adr_cd = '000" & altcd & "'"
        '    note_3 = DAC.Execute_Scalar(sSQL, cn)
        '    ' 2 - If a corresponding cus_no for the alt_cus_addr_cd does not exist, then try the cus_no and cus_alt_adr_cd combined to find note_3 on the parent cus_no record
        '    '     ...need to add '000' to the altaddr for the 15 char cus_alt_adr_cd...
        '    If note_3 Is Nothing Then
        '        sSQL = "select IsNull(A.note_3, '') as note_3 from ARCUSFIL_SQL A where A.cus_no = '" & altcd & "'"
        '        note_3 = DAC.Execute_Scalar(sSQL, cn)
        '    End If
        'Else
        '    ' 3 - No Alt Addr code, so look directly at the cus_no in ARCUSFIL_SQL for note_3
        '    sSQL = "select A.note_3 from ARCUSFIL_SQL A where A.cus_no = '" & cusno & "'"
        '    note_3 = DAC.Execute_Scalar(sSQL, cn)
        'End If
       
        If note_3 = Nothing Then
            Return Nothing
        Else
            Return note_3.ToString.Trim
        End If


    End Function

    Private Sub LoadData(orderNo As String)
        Dim sSQL As String
        Dim dr As DataRow
        Dim dto As New DataTable
        Dim dti As New DataTable
        Dim bAllowImport As Boolean = True

        dto = dtOrders.Clone
        dti = dtItems.Clone

        ' Pulls data from OEORDHDR and OEHDRHST
        Dim note_3 As String = GetRetailLableYN(orderNo)
        If note_3 Is Nothing Then
            MsgBox("Customer RETAIL LABEL option not set to Y.", MsgBoxStyle.OkOnly, "RETAIL LABEL OPTION NOT SET")
            Exit Sub
        ElseIf note_3.Trim = "N" Then
            MsgBox("Customer RETAIL LABEL option set to N.", MsgBoxStyle.OkOnly, "RETAIL LABEL OPTION SET TO N")
            Exit Sub
        Else
            'MsgBox(note_3, MsgBoxStyle.OkOnly, "ORDER NOT FOUND")
        End If
        'note_3 is where Retail Label YN is stored 
        If note_3 = "Y" Then

            'Load Items First ....
            sSQL = " select 1 as selected, " _
                 & " rtrim(lTrim(ship_to_name)) as ship_to_name, rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
                 & " qty_to_ship, rtrim(ltrim(B.item_no)) as item_no, rtrim(ltrim(B.item_desc_1)) as item_desc, " & vbCrLf _
                 & " rtrim(ltrim(IsNull(B.pick_seq,''))) as finish, " & vbCrLf _
                 & " case " & vbCrLf _
                 & "   When IsNull(F.cus_no, '') = '' then " & vbCrLf _
                 & "		 case " & vbCrLf _
                 & "			When D.user_amount = 0  then '' " & vbCrLf _
                 & "			else Cast((Ceiling(B.unit_price * D.user_amount)) as varchar(20)) + '.00' " & vbCrLf _
                 & "		 end " & vbCrLf _
                 & "   When F.user_amount = 0  then '' " & vbCrLf _
                 & "   Else Cast((Ceiling(B.unit_price * F.user_amount)) as varchar(20)) + '.00' " & vbCrLf _
                 & " End as unit_price,  " & vbCrLf _
                 & " case " & vbCrLf _
                 & "   when IsNull(B.pick_seq, '') = '' then rtrim(ltrim(IsNull(C.note_1 , '')))  " & vbCrLf _
                 & "   when B.pick_seq  like '%DS%' then rtrim(ltrim(IsNull(C.note_2, ''))) " & vbCrLf _
                 & "   else rtrim(ltrim(IsNull(C.note_1, '')))   " & vbCrLf _
                 & " end as upc, " & vbCrLf _
                 & " IsNull(E.Detail_Finish_type, '') Detail_Finish_type, " & vbCrLf _
                 & " dbo.fncatalogpage(c.user_def_fld_1) as CatalogPage, " & vbCrLf _
                 & " rtrim(ltrim(A.status)) as status, " & vbCrLf _
                 & " (Select note_4 from ARCUSFIL_SQL where cus_no = dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, ''))) as barcode, " & vbCrLf _
                 & " --rtrim(ltrim(D.note_4)) as barcode, " & vbCrLf _
                 & " A.ship_to_name as START_ship_to_name, " & vbCrLf _
                 & " dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as START_cus_no, " & vbCrLf _
                 & " A.ord_no as START_ord_no " & vbCrLf _
                 & " from OEORDHDR_SQL A " & vbCrLf _
                 & " join ARCUSFIL_SQL D " & vbCrLf _
                 & "   on A.cus_no = D.cus_no " & vbCrLf _
                 & " left join ARCUSFIL_SQL F  " & vbCrLf _
                 & "   on Substring(A.cus_alt_adr_cd, 4, 12) = F.cus_no " & vbCrLf _
                 & " join OEORDLIN_SQL B " & vbCrLf _
                 & "  on A.ord_no = B.ord_no " & vbCrLf _
                 & "  and A.ord_type = B.ord_type " & vbCrLf _
                 & " join IMITMIDX_SQL C " & vbCrLf _
                 & "  on B.item_no = C.item_no " & vbCrLf _
                 & " left join VBPRDFIN_SQL E " & vbCrLf _
                 & "  on E.Pick_Sec = B.pick_seq " & vbCrLf _
                 & " where A.ord_no = '" & orderNo & "' " & vbCrLf _
                 & " and lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','152','154','156','390','200','408','600','700','800','801','900') " & vbCrLf _
                 & " UNION " & vbCrLf _
                 & " select 1 as selected, rtrim(lTrim(ship_to_name)) as ship_to_name, rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
                 & " qty_to_ship, rtrim(ltrim(B.item_no)) as item_no, rtrim(ltrim(B.item_desc_1)) as item_desc, " & vbCrLf _
                 & " rtrim(ltrim(IsNull(B.pick_seq,''))) as finish, " & vbCrLf _
                 & " case " & vbCrLf _
                 & "   When IsNull(F.cus_no, '') = '' then " & vbCrLf _
                 & "		 case " & vbCrLf _
                 & "			When D.user_amount = 0  then '' " & vbCrLf _
                 & "			else Cast((Ceiling(B.unit_price * D.user_amount)) as varchar(20)) + '.00' " & vbCrLf _
                 & "		 end " & vbCrLf _
                 & "   When F.user_amount = 0  then '' " & vbCrLf _
                 & "   Else Cast((Ceiling(B.unit_price * F.user_amount)) as varchar(20)) + '.00' " & vbCrLf _
                 & " End as unit_price,  " & vbCrLf _
                 & " case " & vbCrLf _
                 & "   when IsNull(B.pick_seq, '') = '' then rtrim(ltrim(IsNull(C.note_1 , '')))  " & vbCrLf _
                 & "   when B.pick_seq  like '%DS%' then rtrim(ltrim(IsNull(C.note_2, ''))) " & vbCrLf _
                 & "   else rtrim(ltrim(IsNull(C.note_1, '')))   " & vbCrLf _
                 & " end as upc, " & vbCrLf _
                 & " IsNull(E.Detail_Finish_type, '') Detail_Finish_type, " & vbCrLf _
                 & " dbo.fncatalogpage(c.user_def_fld_1) as CatalogPage, " & vbCrLf _
                 & " rtrim(ltrim(A.status)) as status, " & vbCrLf _
                 & " (Select note_4 from ARCUSFIL_SQL where cus_no = dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, ''))) as barcode, " & vbCrLf _
                 & " --rtrim(ltrim(D.note_4)) as barcode, " & vbCrLf _
                 & " A.ship_to_name as START_ship_to_name, " & vbCrLf _
                 & " dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as START_cus_no, " & vbCrLf _
                 & " A.ord_no as START_ord_no " & vbCrLf _
                 & " from OEHDRHST_SQL A " & vbCrLf _
                 & " join ARCUSFIL_SQL D " & vbCrLf _
                 & "   on A.cus_no = D.cus_no " & vbCrLf _
                 & " left join ARCUSFIL_SQL F  " & vbCrLf _
                 & "   on Substring(A.cus_alt_adr_cd, 4, 12) = F.cus_no " & vbCrLf _
                 & " join OELINHST_SQL B " & vbCrLf _
                 & "  on A.ord_no = B.ord_no " & vbCrLf _
                 & "  and A.ord_type = B.ord_type " & vbCrLf _
                 & " join IMITMIDX_SQL C " & vbCrLf _
                 & "  on B.item_no = C.item_no " & vbCrLf _
                 & " left join VBPRDFIN_SQL E " & vbCrLf _
                 & "  on E.Pick_Sec = B.pick_seq " & vbCrLf _
                 & " where A.ord_no = '" & orderNo & "' " & vbCrLf _
                 & " and lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','150','152','154','156','390','200','408','600','700','800','801','900') " & vbCrLf

            dti = DAC.ExecuteSQL_DataTable(sSQL, cn, "PickedItems")
            If dti.Rows.Count >= 1 Then
                'Validate to be sure the same order isn't coming in more than once ...
                bAllowImport = ValidateDataTableImport(dtItems, dti, orderNo)

                If bAllowImport = False Then
                    MsgBox("Order No " & orderNo & " has is already in the grid.  ", MsgBoxStyle.OkOnly, "Order already loaded.")
                    Exit Sub
                End If

                With dti
                    For i As Integer = 0 To .Rows.Count - 1
                        dr = dtItems.NewRow
                        dr = dti(i)
                        dtItems.ImportRow(dr)
                    Next
                End With

                ' Next Load Orders ...

                sSQL = "  select Distinct 1 as selected, " & vbCrLf _
                  & "     Case When A.user_def_fld_1 is null then '' When lTrim(rTrim(A.user_def_fld_1)) = ''  then '' When  lTrim(rTrim(A.user_def_fld_1)) = 'Y' then NCHAR(0x2716) End as printed,  " & vbCrLf _
                  & "     rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
                  & "     Substring(Cast(A.picked_dt as varchar(8)), 5, 2) + '/' +  Substring(Cast(A.picked_dt as varchar(8)), 7, 2) + '/' + Substring(Cast(A.picked_dt as varchar(8)), 1, 4) picked_dt, " & vbCrLf _
                  & "     dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_cus_no, " & vbCrLf _
                  & "     rtrim(lTrim(ship_to_name)) as ship_to_name, " & vbCrLf _
                  & "     dbo.fnShipToState(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_state, " & vbCrLf _
                  & "     rtrim(ltrim(F.note_3)) as retail_labels, " & vbCrLf _
                  & "     rtrim(ltrim(IsNull(F.note_4, 'N'))) as barcode, " & vbCrLf _
                  & "     rtrim(ltrim(F.user_amount)) as user_amount " & vbCrLf _
                  & "     from OEORDHDR_SQL A " & vbCrLf _
                  & "     join ARCUSFIL_SQL D " & vbCrLf _
                  & "     on A.cus_no = D.cus_no " & vbCrLf _
                  & "     join OEORDLIN_SQL B " & vbCrLf _
                  & "     on A.ord_no = B.ord_no " & vbCrLf _
                  & "     and A.ord_type = B.ord_type " & vbCrLf _
                  & "     join IMITMIDX_SQL C " & vbCrLf _
                  & "     on B.item_no = C.item_no " & vbCrLf _
                  & "     left join VBPRDFIN_SQL E " & vbCrLf _
                  & "     on E.Pick_Sec = B.pick_seq " & vbCrLf _
                  & "     join ARCUSFIL_SQL F " & vbCrLf _
                  & "     on F.cus_no = dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) " & vbCrLf _
                  & "     where " & vbCrLf _
                  & "     lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','152','154','156','390','200','408','600','700','800','801','900') " & vbCrLf _
                  & "     and A.ord_no = '" & orderNo & "' " & vbCrLf _
                  & "     Group by A.ord_no, A.picked_dt, A.cus_alt_adr_cd, ship_to_name, A.cus_no, F.note_3, F.user_amount,  F.note_4,  A.user_def_fld_1 " & vbCrLf _
                  & "     Having(COUNT(B.item_no) >= 1) " & vbCrLf _
                  & "     UNION " & vbCrLf _
                  & "     select Distinct 1 as selected, " & vbCrLf _
                  & "     Case When A.user_def_fld_1 is null then '' When lTrim(rTrim(A.user_def_fld_1)) = ''  then '' When  lTrim(rTrim(A.user_def_fld_1)) = 'Y' then NCHAR(0x2716) End as printed,  " & vbCrLf _
                  & "     rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
                  & "     Substring(Cast(A.picked_dt as varchar(8)), 5, 2) + '/' +  Substring(Cast(A.picked_dt as varchar(8)), 7, 2) + '/' + Substring(Cast(A.picked_dt as varchar(8)), 1, 4) picked_dt, " & vbCrLf _
                  & "     dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_cus_no, " & vbCrLf _
                  & "     rtrim(lTrim(ship_to_name)) as ship_to_name, " & vbCrLf _
                  & "     dbo.fnShipToState(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_state, " & vbCrLf _
                  & "     rtrim(ltrim(F.note_3)) as retail_labels, " & vbCrLf _
                  & "     rtrim(ltrim(IsNull(F.note_4, 'N'))) as barcode, " & vbCrLf _
                  & "     rtrim(ltrim(F.user_amount)) as user_amount " & vbCrLf _
                  & "     from OEHDRHST_SQL A " & vbCrLf _
                  & "     join ARCUSFIL_SQL D " & vbCrLf _
                  & "     on A.cus_no = D.cus_no " & vbCrLf _
                  & "     join OELINHST_SQL B " & vbCrLf _
                  & "     on A.ord_no = B.ord_no " & vbCrLf _
                  & "     and A.ord_type = B.ord_type " & vbCrLf _
                  & "     join IMITMIDX_SQL C " & vbCrLf _
                  & "     on B.item_no = C.item_no " & vbCrLf _
                  & "     left join VBPRDFIN_SQL E " & vbCrLf _
                  & "     on E.Pick_Sec = B.pick_seq " & vbCrLf _
                  & "     join ARCUSFIL_SQL F " & vbCrLf _
                  & "     on F.cus_no = dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) " & vbCrLf _
                  & "     where " & vbCrLf _
                  & "     lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','152','154','156','390','200','408','600','700','800','801','900') " & vbCrLf _
                  & "     and A.ord_no = '" & orderNo & "' " & vbCrLf _
                  & "     Group by A.ord_no, A.picked_dt, A.cus_alt_adr_cd, ship_to_name, A.cus_no, F.note_3, F.user_amount,  F.note_4,  A.user_def_fld_1  " & vbCrLf _
                  & "     Having(COUNT(B.item_no) >= 1) " & vbCrLf _
                  & "     Order by rtrim(ltrim(A.ord_no)) " & vbCrLf


                dto = DAC.ExecuteSQL_DataTable(sSQL, cn, "PickedOrders")


                With dto
                    For i As Integer = 0 To .Rows.Count - 1
                        dr = dtOrders.NewRow
                        dr = dto(i)

                        dtOrders.ImportRow(dr)
                    Next
                End With

                DataGridView1.DataSource = dtOrders
            End If
        End If

    End Sub

    Private Function ValidateDataTableImport(dtExisting As DataTable, dtToImport As DataTable, ord_no As String) As Boolean

        Dim dvExisting As DataView = dtExisting.DefaultView
        dvExisting.RowFilter = "ord_no = '" & ord_no & "'"

        Dim countExisting As Integer = dvExisting.Count
        Dim countToImport As Integer = dtToImport.Rows.Count

        If countExisting = countToImport Then
            Return False
        Else
            Return True
        End If

    End Function

    'Private Sub LoadData()
    '    Dim sSQL As String
    '    Dim sFilter As String

    '    sFilter = ""

    '    sSQL = "  select Distinct 1 as selected, rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
    '      & "     Substring(Cast(A.picked_dt as varchar(8)), 5, 2) + '/' +  Substring(Cast(A.picked_dt as varchar(8)), 7, 2) + '/' + Substring(Cast(A.picked_dt as varchar(8)), 1, 4) picked_dt, " & vbCrLf _
    '      & "     dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_cus_no, " & vbCrLf _
    '      & "     rtrim(lTrim(ship_to_name)) as ship_to_name, " & vbCrLf _
    '      & "     dbo.fnShipToState(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_state, " & vbCrLf _
    '      & "     rtrim(ltrim(F.note_3)) as retail_labels, " & vbCrLf _
    '      & "     rtrim(ltrim(IsNull(F.note_4, 'N'))) as barcode, " & vbCrLf _
    '      & "     rtrim(ltrim(F.user_amount)) as user_amount " & vbCrLf _
    '      & "     from OEORDHDR_SQL A " & vbCrLf _
    '      & "     join ARCUSFIL_SQL D " & vbCrLf _
    '      & "     on A.cus_no = D.cus_no " & vbCrLf _
    '      & "     join OEORDLIN_SQL B " & vbCrLf _
    '      & "     on A.ord_no = B.ord_no " & vbCrLf _
    '      & "     and A.ord_type = B.ord_type " & vbCrLf _
    '      & "     join IMITMIDX_SQL C " & vbCrLf _
    '      & "     on B.item_no = C.item_no " & vbCrLf _
    '      & "     left join VBPRDFIN_SQL E " & vbCrLf _
    '      & "     on E.Pick_Sec = B.pick_seq " & vbCrLf _
    '      & "     join ARCUSFIL_SQL F " & vbCrLf _
    '      & "     on F.cus_no = dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) " & vbCrLf _
    '      & "     where A.status = '5' " & vbCrLf _
    '      & "     and lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','152','154','156','390','200','408','600','700','801','900') " & vbCrLf _
    '      & "     and A.user_def_fld_1 is null " & vbCrLf _
    '      & "     and D.note_3 = 'Y' " & vbCrLf _
    '      & "     Order by rtrim(ltrim(A.ord_no)) " & vbCrLf

    '    dtOrders = DAC.ExecuteSQL_DataTable(sSQL, cn, "PickedOrders")

    '    sSQL = " select 1 as selected, rtrim(lTrim(ship_to_name)) as ship_to_name, rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
    '         & " qty_to_ship, rtrim(ltrim(B.item_no)) as item_no, rtrim(ltrim(B.item_desc_1)) as item_desc, " & vbCrLf _
    '         & " rtrim(ltrim(IsNull(B.pick_seq,''))) as finish, " & vbCrLf _
    '         & " case " & vbCrLf _
    '         & "   When D.user_amount = 0 then '' " & vbCrLf _
    '         & "   Else  Cast((Ceiling(B.unit_price * D.user_amount)) as varchar(20)) + '.00'  " & vbCrLf _
    '         & " End as unit_price, " & vbCrLf _
    '         & " case " & vbCrLf _
    '         & "   when IsNull(B.pick_seq, '') = '' then rtrim(ltrim(IsNull(C.note_1 , ''))) " & vbCrLf _
    '         & "   when B.pick_seq = 'DS' then rtrim(ltrim(IsNull(C.note_3, ''))) " & vbCrLf _
    '         & "   else rtrim(ltrim(IsNull(C.note_2, '')))  " & vbCrLf _
    '         & " end as upc, " & vbCrLf _
    '         & " IsNull(E.Detail_Finish_type, '') Detail_Finish_type, " & vbCrLf _
    '         & " dbo.fncatalogpage(c.user_def_fld_1) as CatalogPage, " & vbCrLf _
    '         & " rtrim(ltrim(A.status)) as status, " & vbCrLf _
    '         & " rtrim(ltrim(D.note_4)) as barcode, " & vbCrLf _
    '         & " A.ship_to_name as START_ship_to_name,  " & vbCrLf _
    '         & " dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as START_cus_no, " & vbCrLf _
    '         & " A.ord_no as START_ord_no " & vbCrLf _
    '         & " from OEORDHDR_SQL A " & vbCrLf _
    '         & " join ARCUSFIL_SQL D " & vbCrLf _
    '         & "   on A.cus_no = D.cus_no " & vbCrLf _
    '         & " join OEORDLIN_SQL B " & vbCrLf _
    '         & "  on A.ord_no = B.ord_no " & vbCrLf _
    '         & "  and A.ord_type = B.ord_type " & vbCrLf _
    '         & " join IMITMIDX_SQL C " & vbCrLf _
    '         & "  on B.item_no = C.item_no " & vbCrLf _
    '         & " left join VBPRDFIN_SQL E " & vbCrLf _
    '         & "  on E.Pick_Sec = B.pick_seq " & vbCrLf _
    '         & " where A.status = '5' " & vbCrLf _
    '         & " and lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','152','154','156','390','200','408','600','700','801','900') " & vbCrLf _
    '         & " and A.user_def_fld_1 is null " & vbCrLf _
    '         & " and D.note_3 = 'Y' " & vbCrLf
    '    '& " UNION " & vbCrLf _
    '    '& " select  1 as selected, '' as ship_to_name " & vbCrLf _
    '    '& " ,A.ord_no as ord_no " & vbCrLf _
    '    '& " , 1 as qty_to_ship, '' as item_no " & vbCrLf _
    '    '& " ,'  ******  START  ******  '  as item_desc " & vbCrLf _
    '    '& " ,'' as finish " & vbCrLf _
    '    '& " ,'' as unit_price " & vbCrLf _
    '    '& " ,'' as upc " & vbCrLf _
    '    '& " ,'' as Detail_Finish_type " & vbCrLf _
    '    '& " ,'' as CatalogPage" & vbCrLf _
    '    '& " , '0' as status " & vbCrLf _
    '    '& " , '' as barcode " & vbCrLf _
    '    '& " , A.ship_to_name as START_ship_to_name " & vbCrLf _
    '    '& " , dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as START_cus_no " & vbCrLf _
    '    '& " , A.ord_no as START_ord_no " & vbCrLf _
    '    '& " from OEORDHDR_SQL A  " & vbCrLf _
    '    '& " join OEORDLIN_SQL B  " & vbCrLf _
    '    '& " on A.ord_no = B.ord_no  " & vbCrLf _
    '    '& " and A.ord_type = B.ord_type  " & vbCrLf _
    '    '& " join IMITMIDX_SQL C  " & vbCrLf _
    '    '& "  on B.item_no = C.item_no  " & vbCrLf _
    '    '& " join ARCUSFIL_SQL D " & vbCrLf _
    '    '& "  on A.cus_no = D.cus_no " & vbCrLf _
    '    '& " where A.status = '5'  and A.user_def_fld_1 is null  " & vbCrLf _
    '    '& " and D.note_3 = 'Y' " & vbCrLf _
    '    '& " and lTrim(rTrim(C.prod_cat)) not in  ('001','153','152','154','156','390','200','408','600','700','801','900') " & vbCrLf _
    '    '& "  Order by ord_no, status"

    '    ' sSQL = Replace(sSQL, "'  ******  START  ******  '", "''")
    '    dtItems = DAC.ExecuteSQL_DataTable(sSQL, cn, "PickedItems")


    '    DataGridView1.DataSource = dtOrders

    'End Sub
    'Private Sub LoadRePrintData()
    '    Dim sSQL As String
    '    Dim sFilter As String

    '    sFilter = ""

    '    sSQL = " select Distinct 1 as selected, rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
    '     & "     Substring(Cast(A.picked_dt as varchar(8)), 5, 2) + '/' +  Substring(Cast(A.picked_dt as varchar(8)), 7, 2) + '/' + Substring(Cast(A.picked_dt as varchar(8)), 1, 4) picked_dt, " & vbCrLf _
    '     & "     dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_cus_no, " & vbCrLf _
    '     & "     rtrim(lTrim(ship_to_name)) as ship_to_name, " & vbCrLf _
    '     & "     dbo.fnShipToState(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as ship_to_state, " & vbCrLf _
    '     & "     rtrim(ltrim(F.note_3)) as retail_labels, " & vbCrLf _
    '     & "     rtrim(ltrim(IsNull(F.note_4, 'N'))) as barcode, " & vbCrLf _
    '     & "     rtrim(ltrim(F.user_amount)) as user_amount " & vbCrLf _
    '     & "     from OEORDHDR_SQL A " & vbCrLf _
    '     & "     join ARCUSFIL_SQL D " & vbCrLf _
    '     & "     on A.cus_no = D.cus_no " & vbCrLf _
    '     & "     join OEORDLIN_SQL B " & vbCrLf _
    '     & "     on A.ord_no = B.ord_no " & vbCrLf _
    '     & "     and A.ord_type = B.ord_type " & vbCrLf _
    '     & "     join IMITMIDX_SQL C " & vbCrLf _
    '     & "     on B.item_no = C.item_no " & vbCrLf _
    '     & "     left join VBPRDFIN_SQL E " & vbCrLf _
    '     & "     on E.Pick_Sec = B.pick_seq " & vbCrLf _
    '     & "     join ARCUSFIL_SQL F " & vbCrLf _
    '     & "     on F.cus_no = dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) " & vbCrLf _
    '     & "     where A.status = '5' " & vbCrLf _
    '     & "     and lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','152','154','156','390','200','408','600','700','801','900') " & vbCrLf _
    '     & "     and A.user_def_fld_1  = 'Y' " & vbCrLf _
    '     & "     and D.note_3 = 'Y' " & vbCrLf _
    '     & "     Order by rtrim(ltrim(A.ord_no)) " & vbCrLf

    '    dtOrdersReprint = DAC.ExecuteSQL_DataTable(sSQL, cn, "PickedOrders")

    '    sSQL = " select 1 as selected, rtrim(lTrim(ship_to_name)) as ship_to_name, rtrim(ltrim(A.ord_no)) as ord_no, " & vbCrLf _
    '        & " qty_to_ship, rtrim(ltrim(B.item_no)) as item_no, rtrim(ltrim(B.item_desc_1)) as item_desc, " & vbCrLf _
    '        & " rtrim(ltrim(IsNull(B.pick_seq,''))) as finish, " & vbCrLf _
    '        & " case " & vbCrLf _
    '        & "   When D.user_amount = 0 then '' " & vbCrLf _
    '        & "   Else  Cast((Ceiling(B.unit_price * D.user_amount)) as varchar(20)) + '.00'  " & vbCrLf _
    '        & " End as unit_price, " & vbCrLf _
    '        & " case " & vbCrLf _
    '        & "   when IsNull(B.pick_seq, '') = '' then rtrim(ltrim(IsNull(C.note_1 , ''))) " & vbCrLf _
    '        & "   when B.pick_seq = 'DS' then rtrim(ltrim(IsNull(C.note_3, ''))) " & vbCrLf _
    '        & "   else rtrim(ltrim(IsNull(C.note_2, '')))  " & vbCrLf _
    '        & " end as upc, " & vbCrLf _
    '        & " IsNull(E.Detail_Finish_type, '') Detail_Finish_type, " & vbCrLf _
    '        & " dbo.fncatalogpage(c.user_def_fld_1) as CatalogPage, " & vbCrLf _
    '        & " rtrim(ltrim(A.status)) as status, " & vbCrLf _
    '        & " rtrim(ltrim(D.note_4)) as barcode, " & vbCrLf _
    '        & "  A.ship_to_name as START_ship_to_name,  " & vbCrLf _
    '        & " dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as START_cus_no, " & vbCrLf _
    '        & " A.ord_no as START_ord_no " & vbCrLf _
    '        & " from OEORDHDR_SQL A " & vbCrLf _
    '        & " join ARCUSFIL_SQL D " & vbCrLf _
    '        & "   on A.cus_no = D.cus_no " & vbCrLf _
    '        & " join OEORDLIN_SQL B " & vbCrLf _
    '        & "  on A.ord_no = B.ord_no " & vbCrLf _
    '        & "  and A.ord_type = B.ord_type " & vbCrLf _
    '        & " join IMITMIDX_SQL C " & vbCrLf _
    '        & "  on B.item_no = C.item_no " & vbCrLf _
    '        & " left join VBPRDFIN_SQL E " & vbCrLf _
    '        & "  on E.Pick_Sec = B.pick_seq " & vbCrLf _
    '        & " where A.status = '5' " & vbCrLf _
    '        & " and lTrim(rTrim(C.prod_cat)) not in  ('001','150','153','152','154','156','390','200','408','600','700','801','900') " & vbCrLf _
    '        & " and A.user_def_fld_1 = 'Y'   " & vbCrLf _
    '        & " and D.note_3 = 'Y' " & vbCrLf
    '    '& " UNION " & vbCrLf _
    '    '& " select  1 as selected, '' as ship_to_name " & vbCrLf _
    '    '& " , A.ord_no as ord_no " & vbCrLf _
    '    '& " , 1 as qty_to_ship, '' as item_no " & vbCrLf _
    '    '& " ,'  ******  START  ******  '  as item_desc " & vbCrLf _
    '    '& " ,'' as finish " & vbCrLf _
    '    '& " ,'' as unit_price " & vbCrLf _
    '    '& " ,'' as upc " & vbCrLf _
    '    '& " ,'' as Detail_Finish_type " & vbCrLf _
    '    '& " ,'' as CatalogPage" & vbCrLf _
    '    '& " , '0' as status " & vbCrLf _
    '    '& " , '' as barcode " & vbCrLf _
    '    '& " , A.ship_to_name as START_ship_to_name " & vbCrLf _
    '    '& " , dbo.fnShipToCusNo(A.ord_no,A.cus_no, IsNull(A.cus_alt_adr_cd, '')) as START_cus_no " & vbCrLf _
    '    '& " , A.ord_no as START_ord_no " & vbCrLf _
    '    '& " from OEORDHDR_SQL A  " & vbCrLf _
    '    '& " join OEORDLIN_SQL B  " & vbCrLf _
    '    '& " on A.ord_no = B.ord_no  " & vbCrLf _
    '    '& " and A.ord_type = B.ord_type  " & vbCrLf _
    '    '& " join IMITMIDX_SQL C  " & vbCrLf _
    '    '& "  on B.item_no = C.item_no  " & vbCrLf _
    '    '& " join ARCUSFIL_SQL D " & vbCrLf _
    '    '& "  on A.cus_no = D.cus_no " & vbCrLf _
    '    '& " where A.status = '5'  and A.user_def_fld_1 = 'Y'  " & vbCrLf _
    '    '& " and D.note_3 = 'Y' " & vbCrLf _
    '    '& " and lTrim(rTrim(C.prod_cat)) not in  ('001','153','152','154','156','390','200','408','600','700','801','900') " & vbCrLf _
    '    '& "  Order by ord_no, status"

    '    dtItemsReprint = DAC.ExecuteSQL_DataTable(sSQL, cn, "PickedItems")

    '    DataGridView1.DataSource = dtOrdersReprint

    'End Sub

    Private Function LoadItemData(ord_no As String) As DataTable
        Dim rw() As DataRow
        Dim addrow As DataRow
        Dim dt As DataTable
        If iLoadType = LoadType.LoadPrint Then
            dt = dtItems.Clone
            rw = dtItems.Select("ord_no = " & ord_no & " AND selected = 1")
        Else
            dt = dtItemsReprint.Clone
            rw = dtItemsReprint.Select("ord_no = " & ord_no & " AND selected = 1")
        End If

        For Each r As DataRow In rw
            addrow = dt.NewRow
            addrow.ItemArray = r.ItemArray
            dt.Rows.Add(addrow)
        Next

        Return dt

    End Function

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        txtOrderSearch.Focus()

        bLoading = False
        bIsLoading = False
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick ', DataGridView2.CellMouseClick
        If bLoading Then
            bLoading = False
            Exit Sub
        End If
        If e.RowIndex = -1 Then Exit Sub

        Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim items As DataTable
        Dim ordno As String = DataGridView1.Rows(e.RowIndex).Cells("OrderNo").Value
        If ordno = "" Then Exit Sub
        If ordno Is Nothing Then Exit Sub


        If iLastRowSelected <> e.RowIndex Then
            bClearAll = False
            Clear(bClearAll)
            DataGridView1.Refresh()
        End If

        If e.ColumnIndex = 0 Then
            iLastRowSelected = e.RowIndex
            Exit Sub
        End If

        If bLoading = True Then Exit Sub

        Select Case iLoadType
            Case LoadType.LoadPrint
                items = dtItems
                Dim query = _
                    From item In items.AsEnumerable() _
                    Where item.Field(Of String)("ord_no") = ordno _
                    Order By item.Field(Of String)("status") _
                    Select item
                dvItems = query.AsDataView()
            Case LoadType.LoadRePrint
                items = dtItemsReprint
                Dim query = _
                    From item In items.AsEnumerable() _
                    Where item.Field(Of String)("ord_no") = ordno _
                    Order By item.Field(Of String)("status") _
                    Select item
                dvItems = query.AsDataView()
        End Select

        DataGridView2.DataSource = dvItems

        If My.Computer.Keyboard.ShiftKeyDown Then
            With Timer2
                .Interval = 50
                .Enabled = True
            End With
        End If
        iLastRowSelected = e.RowIndex
    End Sub

    Private Sub DataGridView_ColumnHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) _
                                                  Handles DataGridView1.ColumnHeaderMouseClick, DataGridView2.ColumnHeaderMouseClick

        Dim dgv As DataGridView = CType(sender, DataGridView)

        If Not e.ColumnIndex = 0 Then 'Do not sort CheckBox on mouse click
            With dgv
                Dim col As DataGridViewColumn = dgv.Columns(e.ColumnIndex)
                col.SortMode = DataGridViewColumnSortMode.Programmatic

                If srt = System.ComponentModel.ListSortDirection.Ascending Then
                    srt = System.ComponentModel.ListSortDirection.Descending
                    col.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Descending

                Else
                    srt = System.ComponentModel.ListSortDirection.Ascending
                    col.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Ascending
                    col.Width += SystemInformation.Border3DSize.Width
                End If
                .Sort(col, srt)
                .EndEdit()
            End With
        End If

        'Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim chk As Boolean
        Dim selected As Integer
        With dgv
            .EndEdit()
            Select Case dgv.Name
                Case "DataGridView1"
                    If e.ColumnIndex = 0 Then
                        If dgv.Rows.Count = 0 Then Exit Sub 'check for empty datagrid, if so, exit...
                        If .Rows(0).Cells(0).Value Is DBNull.Value Then chk = False Else chk = (.Rows(0).Cells(0).Value)
                        selected = IIf(chk = True, 0, 1)

                        If iLoadType = LoadType.LoadPrint Then
                            For i As Integer = 0 To dtOrders.Rows.Count - 1
                                dtOrders(i)(0) = selected
                            Next
                        Else
                            For i As Integer = 0 To dtOrdersReprint.Rows.Count - 1
                                dtOrdersReprint(i)(0) = selected
                            Next
                        End If

                    End If

                Case "DataGridView2"

                    If e.ColumnIndex = 0 Then
                        If dgv.Rows.Count = 0 Then Exit Sub 'check for empty datagrid, if so, exit...
                        If .Rows(0).Cells(0).Value Is Nothing Then chk = False Else chk = (.Rows(0).Cells(0).Value)
                        selected = IIf(chk = True, 0, 1)

                        'For i As Integer = 0 To dtPrintItems.Rows.Count - 1
                        '    dtPrintItems(i)(0) = selected
                        'Next
                        For i As Integer = 0 To dvItems.Count - 1
                            dvItems(i)(0) = selected
                        Next
                    End If

                    dgv.Refresh()

            End Select

        End With

        dgv.Refresh()

    End Sub


    Private Sub btnSortCheckBox_Click(sender As System.Object, e As System.EventArgs)

        Dim dgv As DataGridView = CType(DataGridView1, DataGridView)
        Dim col As DataGridViewColumn = dgv.Columns(0)

        With dgv
            .EndEdit()


            col.SortMode = DataGridViewColumnSortMode.Programmatic

            If srt = System.ComponentModel.ListSortDirection.Ascending Then
                srt = System.ComponentModel.ListSortDirection.Descending
                col.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Descending

            Else
                srt = System.ComponentModel.ListSortDirection.Ascending
                col.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Ascending
                col.Width += SystemInformation.Border3DSize.Width
            End If
            .Sort(col, srt)
            .EndEdit()

        End With
        col.SortMode = DataGridViewColumnSortMode.NotSortable

    End Sub

    Public Sub PrintLabels() Handles CtlBartender1.PrintLables

        With DataGridView2
            Dim col As DataGridViewColumn = .Columns("status")
            .Sort(col, System.ComponentModel.ListSortDirection.Ascending)
        End With

        'Loop through orders
        If iLoadType = LoadType.LoadPrint Then
            Debug.Print(LoadType.LoadPrint.ToString)
            For Each row As DataRow In dtOrders.Rows
                Debug.Print(row(2).ToString)
                If row("selected") Is DBNull.Value OrElse row("selected") = 0 Then
                    Continue For
                Else
                    row("selected") = 0
                    PrintItemLabels(row("ord_no"))
                End If
                If bCancelPrint = True Then
                    bCancelPrint = False
                    Exit Sub
                End If
            Next
        Else
            For Each row As DataRow In dtOrdersReprint.Rows
                If row("selected") Is DBNull.Value OrElse row("selected") = 0 Then
                    Continue For
                Else
                    PrintItemLabels(row("ord_no"))
                End If
                If bCancelPrint = True Then
                    bCancelPrint = False
                    Exit Sub
                End If
            Next
        End If

    End Sub

    Private Sub PrintItemLabels(ord_no As String)

        Dim dt As New DataTable
        dt = LoadItemData(ord_no)
        bCancelPrint = CtlBartender1.Print(dt, cn)

    End Sub
    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnReprint.Click, btnRefresh.Click
        Dim btn As Button = CType(sender, Button)
        bClearAll = True
        Clear(bClearAll)
        If btn.Name = btnRefresh.Name Then
            iLoadType = LoadType.LoadPrint
            btn.BackColor = BtnColor
            btnReprint.BackColor = SystemColors.ButtonFace
        Else
            iLoadType = LoadType.LoadRePrint
            btn.BackColor = BtnColor
            btnRefresh.BackColor = SystemColors.ButtonFace
        End If
        With Timer1
            bLoading = True
            .Interval = 50
            .Enabled = True
        End With
    End Sub

    Private Sub Clear(bClearAll As Boolean)
        'If Not dvItems Is Nothing Then
        '    dvItems.Dispose()
        '    'dtItems.AsEnumerable().ToList.ForEach(dtItems >= dtItems.delete())
        'For i As Integer = 0 To dtItems.Rows.Count - 1
        '    dtItems(i).Delete()
        'Next

        'For Each rw As DataRow In dtItems.Rows
        '    dtItems.Rows.Remove(
        'Next

        'For i As Integer = dtItems.Rows.Count - 1 To 0 Step -1
        '    dtItems.Rows.Remove(dtItems.Rows(i))
        'Next

        DataGridView2.DataSource = Nothing
        LoadDataGridView2(header_style)
        DataGridView2.Refresh()
        If bClearAll Then
            If Not (dtOrders Is Nothing) Then dtOrders.Rows.Clear()
            DataGridView1.DataSource = dtOrders
            For i As Integer = dtItems.Rows.Count - 1 To 0 Step -1
                dtItems.Rows.Remove(dtItems.Rows(i))
            Next
        End If

    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        'Loop through orders

        If iLoadType = LoadType.LoadPrint Then
            With dtOrders
                For i As Integer = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i)("selected") Is DBNull.Value OrElse .Rows(i)("selected") = 0 Then
                        Continue For
                    Else
                        If Not My.Settings.Dialog1DoNotShow = True Then
                            Dim res As Integer = Dialog1.ShowDialog
                            If Dialog1.chkDontShowThisAgain.Checked = True Then
                                My.Settings.Dialog1DoNotShow = True
                                My.Settings.Save()
                            End If
                            If res = DialogResult.Cancel Then Exit Sub
                        Else

                        End If

                        CtlBartender1.MarkAsPrinted(.Rows(i)("ord_no"), cn)
                        .Rows.RemoveAt(i)
                    End If
                Next
                DataGridView1.DataSource = dtOrders

            End With
        Else
            With dtOrdersReprint
                For i As Integer = .Rows.Count - 1 To 0 Step -1
                    If .Rows(i)("selected") Is DBNull.Value OrElse .Rows(i)("selected") = 0 Then
                        Continue For
                    Else
                        CtlBartender1.MarkAsPrinted(.Rows(i)("ord_no"), cn)
                        .Rows.RemoveAt(i)
                        If Not dtPrintItems Is Nothing Then dtPrintItems.Rows.Clear()
                    End If
                Next
                DataGridView1.DataSource = dtOrdersReprint
            End With
        End If

    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Selected = True Then
                row.Cells("selected").Value = 1
            End If
        Next

    End Sub

    Private Sub btnRemove_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles btnRemove.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim res As Integer = MsgBox("Dialog Orders with be marked to 'Printed' is currently set to 'Do Not Show.'  Do you want to reset it to Show.'", MsgBoxStyle.YesNo, "Reset Dialog to Show")
            If res = MsgBoxResult.Yes Then
                My.Settings.Dialog1DoNotShow = False
                My.Settings.Save()
            End If
        End If
    End Sub

    Private Sub GetOrder()
        
        ordno = ("00000000" & txtOrderSearch.Text.Trim).Substring(Len("00000000" & txtOrderSearch.Text.Trim) - 8)
        LoadData(ordno)
        'ValidateCustomerAmountField(ordno)

    End Sub
    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        iLoadType = LoadType.LoadPrint
        bLoading = True
        GetOrder()

        bLoading = False
    End Sub

    Private Sub ValidateCustomerAmountField(ord_no As String)
        Dim dAmount As Decimal = 0
        Dim sSQL As String = "Select * from dbo.fnUserAmountByOrderNo('" & ord_no & "') "
        Try
            Dim dt As New DataTable
            dt = DAC.ExecuteSQL_DataTable(sSQL, cn, "UserInfo")
            cOptionalCriteria.CustNo = (dt(0)(0))
            cOptionalCriteria.UserAmount = dt(0)(1)
            cOptionalCriteria.Barcode = dt(0)(2)
            txtCustomer.Text = cOptionalCriteria.custno
            txtUserAmount.Text = cOptionalCriteria.UserAmount
            txtBarcode.Text = cOptionalCriteria.barcode
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtOrderSearch_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtOrderSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            iLoadType = LoadType.LoadPrint
            GetOrder()
            txtOrderSearch.Text = ""
        End If
    End Sub


    Private Sub RemoveRowToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RemoveRowToolStripMenuItem.Click
        Dim rw As Integer
        rw = DataGridView1.CurrentRow.Index

        dtOrders.Rows.RemoveAt(rw)

    End Sub

    'Private Sub DataGridView1_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick

    'End Sub

   
    Private Sub DataGridView1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim clickedCell As DataGridViewCell = Nothing


        'dgv.Rows(dgv.CurrentRow.Index).Selected = True
        If e.Button = MouseButtons.Right Then
            Dim hit As DataGridView.HitTestInfo = _
                dgv.HitTest(e.X, e.Y)
            If hit.Type = DataGridViewHitTestType.Cell Then
                clickedCell = _
                    dgv.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                UnselectDataGridViewRows(dgv)
                dgv.Rows(clickedCell.RowIndex).Selected = True
            End If

        End If

    End Sub

    Private Sub UnselectDataGridViewRows(ByVal dgv As DataGridView)
        For Each rw As DataGridViewRow In dgv.Rows
            rw.Selected = False
        Next
    End Sub


    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        bClearAll = True
        Clear(bClearAll)

    End Sub

    'Private Sub btnSaveDefaultDB_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveDefaultDB.Click

    '    If My.Settings.DefaultDB = cbDBList.SelectedItem.ToString Then
    '        Exit Sub
    '    Else

    '        My.Settings.DefaultDB = cbDBList.SelectedItem.ToString
    '        My.Settings.Save()

    '        With cOptionalCriteria
    '            .DBName = My.Settings.DefaultDB
    '            MacStartup(.DBName)
    '            lblCurrentDB.Text = .CurrentDB
    '            lblDefaultDB.Text = .DefaultDB
    '        End With

    '        bEnableRefreshDB = False
    '        bEnableSaveDB = False
    '        bEnableCtls = True
    '        SetControlState(bEnableRefreshDB, bEnableSaveDB, bEnableCtls)

    '    End If

    'End Sub

    Private Sub RefreshDB()
        Dim cbo As ComboBox = CType(cbDBList, ComboBox)
        With cOptionalCriteria
            .CurrentDb = cbo.SelectedItem.ToString
            .DBName = cbo.SelectedItem.ToString
        End With
        MacStartup(cOptionalCriteria.DBName.ToString)
        lblCurrentDB.Text = cOptionalCriteria.CurrentDB
    End Sub
    Private Sub btnRefreshDB_Click(sender As System.Object, e As System.EventArgs) Handles btnRefreshDB.Click
        Dim cbo As ComboBox = CType(cbDBList, ComboBox)
        With cOptionalCriteria
            .CurrentDb = cbo.SelectedItem.ToString
            .DBName = cbo.SelectedItem.ToString
        End With
        MacStartup(cOptionalCriteria.DBName.ToString)
        lblCurrentDB.Text = cOptionalCriteria.CurrentDB

        'LoadGrids()

        bEnableRefreshDB = False
        bEnableSaveDB = True
        bEnableCtls = True
        SetControlState(bEnableRefreshDB, bEnableSaveDB, bEnableCtls)
    End Sub

    Private Sub btnSaveDefaultDB_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveDefaultDB.Click
        Dim cbo As ComboBox = CType(cbDBList, ComboBox)
        With cOptionalCriteria
            .CurrentDb = cbo.SelectedItem.ToString
            .DBName = cbo.SelectedItem.ToString
            .DefaultDB = cbo.SelectedItem.ToString
            My.Settings.DefaultDB = .DefaultDB
            My.Settings.Save()
        End With

        MacStartup(cOptionalCriteria.DBName)
        lblCurrentDB.Text = cOptionalCriteria.CurrentDB

        'LoadGrids()

        bEnableRefreshDB = False
        bEnableSaveDB = True
        bEnableCtls = True
        SetControlState(bEnableRefreshDB, bEnableSaveDB, bEnableCtls)
    End Sub
    Private Sub SetControlState(enableRefresh As Boolean, enableSave As Boolean, enablectls As Boolean)

        btnSaveDefaultDB.Enabled = enableSave
        btnRefreshDB.Enabled = enableRefresh

    End Sub

    Private Sub cbDBList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbDBList.SelectedIndexChanged
        If bIsLoading Then Exit Sub
        bClearAll = True
        Clear(bClearAll)
        RefreshDB()

        'Dim cbo As ComboBox = CType(sender, ComboBox)
        'Dim db As String = cbo.SelectedItem.ToString

        'bEnableRefreshDB = CBool(IIf(cbo.SelectedItem.ToString = My.Settings.DefaultDB, False, True))
        'bEnableSaveDB = CBool(IIf(cbo.SelectedItem.ToString = My.Settings.DefaultDB, False, True))
        'bEnableCtls = CBool(IIf(cbo.SelectedItem.ToString = My.Settings.DefaultDB, True, False))

        'SetControlState(bEnableRefreshDB, bEnableSaveDB, bEnableCtls)
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnPricesAreMissing.Click
        MsgBox("If prices are not appearing on the label check the following: " & vbCrLf & vbCrLf & _
               "  See if prices are on the grid.  If not, check the customer record in Macola, User Notes." & vbCrLf & vbCrLf & _
               "    - Retail Labels field must be set to Y " & vbCrLf & vbCrLf & _
               "    - Amount must be a number greater than 0.  " & vbCrLf & vbCrLf & _
               "  If prices are on the grid but not on the label, then it's the label.  " & vbCrLf & vbCrLf & _
               "    - Call for assistance or try a different label.  ", MsgBoxStyle.OkOnly, "Missing Prices")
    End Sub

    Private Sub CtlBartender1_Load(sender As System.Object, e As System.EventArgs) Handles CtlBartender1.Load

    End Sub
End Class

Public Class OptionalCriteria
    Public DBName As String
    Public DefaultDB As String
    Public CurrentDB As String
    Public CustNo As String
    Public UserAmount As Decimal
    Public Barcode As String
    'Public mOrderNo As String
    'Public mShipToName As String
    'Public mShipToAddr1 As String
    'Public mShipToAddr2 As String
    'Public mShipToAddr3 As String
    'Public mShipToCountry As String
    'Public mShipAddressForLabel As String
    'Public mShippingDate As String
    'Public mShippingMonth As String
    'Public mPalletCount As String
    'Public mNofPalletCount As String

    Public Sub Clear()
        CustNo = ""
        UserAmount = 0
        Barcode = ""
        'mOrderNo = ""
        'mShipToName = ""
        'mShipToAddr1 = ""
        'mShipToAddr2 = ""
        'mShipToAddr3 = ""
        'mShipToCountry = ""
        'mShippingDate = ""
        'mShippingMonth = ""
        'mPalletCount = ""
        'mNofPalletCount = ""
    End Sub

End Class