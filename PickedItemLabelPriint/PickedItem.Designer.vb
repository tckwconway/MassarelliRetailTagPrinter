<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PickedOrder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PickedOrder))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblPricingType = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblDivider1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblDivider2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCurrentDB = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblDivider3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblDefaultDB = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblServer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnPricesAreMissing = New System.Windows.Forms.Button()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUserAmount = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.cbDBList = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefreshDB = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtOrderSearch = New System.Windows.Forms.TextBox()
        Me.btnSaveDefaultDB = New System.Windows.Forms.Button()
        Me.lblLabelList = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnReprint = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.CtlBartender1 = New PickedItemLabelPriint.ctlBartender()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Controls.Add(Me.StatusStrip1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(21, 135)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1366, 799)
        Me.Panel1.TabIndex = 3
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 57)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1366, 720)
        Me.SplitContainer1.SplitterDistance = 211
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 3
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1366, 211)
        Me.DataGridView1.TabIndex = 3
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveRowToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(188, 34)
        '
        'RemoveRowToolStripMenuItem
        '
        Me.RemoveRowToolStripMenuItem.Name = "RemoveRowToolStripMenuItem"
        Me.RemoveRowToolStripMenuItem.Size = New System.Drawing.Size(187, 30)
        Me.RemoveRowToolStripMenuItem.Text = "Remove Row"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(1366, 503)
        Me.DataGridView2.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblPricingType, Me.lblDivider1, Me.lblCount, Me.lblDivider2, Me.lblCurrentDB, Me.lblDivider3, Me.lblDefaultDB, Me.lblServer})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 777)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(2, 0, 21, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1366, 22)
        Me.StatusStrip1.TabIndex = 182
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblPricingType
        '
        Me.lblPricingType.Name = "lblPricingType"
        Me.lblPricingType.Size = New System.Drawing.Size(0, 23)
        '
        'lblDivider1
        '
        Me.lblDivider1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.lblDivider1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.lblDivider1.Name = "lblDivider1"
        Me.lblDivider1.Size = New System.Drawing.Size(4, 23)
        Me.lblDivider1.Visible = False
        '
        'lblCount
        '
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(0, 23)
        '
        'lblDivider2
        '
        Me.lblDivider2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.lblDivider2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.lblDivider2.Name = "lblDivider2"
        Me.lblDivider2.Size = New System.Drawing.Size(4, 23)
        Me.lblDivider2.Visible = False
        '
        'lblCurrentDB
        '
        Me.lblCurrentDB.Name = "lblCurrentDB"
        Me.lblCurrentDB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCurrentDB.Size = New System.Drawing.Size(0, 23)
        Me.lblCurrentDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDivider3
        '
        Me.lblDivider3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.lblDivider3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.lblDivider3.Name = "lblDivider3"
        Me.lblDivider3.Size = New System.Drawing.Size(4, 23)
        '
        'lblDefaultDB
        '
        Me.lblDefaultDB.Name = "lblDefaultDB"
        Me.lblDefaultDB.Size = New System.Drawing.Size(0, 23)
        Me.lblDefaultDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDefaultDB.Visible = False
        '
        'lblServer
        '
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(0, 23)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnPricesAreMissing)
        Me.Panel2.Controls.Add(Me.txtBarcode)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txtUserAmount)
        Me.Panel2.Controls.Add(Me.txtCustomer)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.btnClear)
        Me.Panel2.Controls.Add(Me.btnRefreshDB)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.txtOrderSearch)
        Me.Panel2.Controls.Add(Me.btnSaveDefaultDB)
        Me.Panel2.Controls.Add(Me.lblLabelList)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1366, 57)
        Me.Panel2.TabIndex = 4
        '
        'btnPricesAreMissing
        '
        Me.btnPricesAreMissing.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnPricesAreMissing.Location = New System.Drawing.Point(1194, 12)
        Me.btnPricesAreMissing.Name = "btnPricesAreMissing"
        Me.btnPricesAreMissing.Size = New System.Drawing.Size(168, 34)
        Me.btnPricesAreMissing.TabIndex = 193
        Me.btnPricesAreMissing.Text = "If Prices are missing"
        Me.btnPricesAreMissing.UseVisualStyleBackColor = True
        '
        'txtBarcode
        '
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarcode.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtBarcode.Location = New System.Drawing.Point(1050, 57)
        Me.txtBarcode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.ReadOnly = True
        Me.txtBarcode.Size = New System.Drawing.Size(124, 33)
        Me.txtBarcode.TabIndex = 192
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.MenuText
        Me.Label4.Location = New System.Drawing.Point(909, 62)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 31)
        Me.Label4.TabIndex = 191
        Me.Label4.Text = "Barcode:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserAmount
        '
        Me.txtUserAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtUserAmount.Location = New System.Drawing.Point(501, 57)
        Me.txtUserAmount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUserAmount.Name = "txtUserAmount"
        Me.txtUserAmount.ReadOnly = True
        Me.txtUserAmount.Size = New System.Drawing.Size(124, 33)
        Me.txtUserAmount.TabIndex = 189
        '
        'txtCustomer
        '
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtCustomer.Location = New System.Drawing.Point(168, 57)
        Me.txtCustomer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.Size = New System.Drawing.Size(154, 33)
        Me.txtCustomer.TabIndex = 188
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.MenuText
        Me.Label2.Location = New System.Drawing.Point(332, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 31)
        Me.Label2.TabIndex = 187
        Me.Label2.Text = "User Amount:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.MenuText
        Me.Label1.Location = New System.Drawing.Point(4, 58)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 31)
        Me.Label1.TabIndex = 186
        Me.Label1.Text = "Customer:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Controls.Add(Me.cbDBList)
        Me.Panel3.Location = New System.Drawing.Point(906, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(267, 54)
        Me.Panel3.TabIndex = 185
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(36, 12)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 28)
        Me.Label7.TabIndex = 187
        Me.Label7.Text = "&Data:"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(8, 15)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 23)
        Me.PictureBox2.TabIndex = 186
        Me.PictureBox2.TabStop = False
        '
        'cbDBList
        '
        Me.cbDBList.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDBList.FormattingEnabled = True
        Me.cbDBList.Location = New System.Drawing.Point(111, 6)
        Me.cbDBList.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbDBList.Name = "cbDBList"
        Me.cbDBList.Size = New System.Drawing.Size(151, 36)
        Me.cbDBList.TabIndex = 183
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnClear.Location = New System.Drawing.Point(332, 11)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(154, 38)
        Me.btnClear.TabIndex = 183
        Me.btnClear.Text = "Clear Grids"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefreshDB
        '
        Me.btnRefreshDB.AutoSize = True
        Me.btnRefreshDB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRefreshDB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshDB.Image = CType(resources.GetObject("btnRefreshDB.Image"), System.Drawing.Image)
        Me.btnRefreshDB.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefreshDB.Location = New System.Drawing.Point(816, 6)
        Me.btnRefreshDB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefreshDB.Name = "btnRefreshDB"
        Me.btnRefreshDB.Size = New System.Drawing.Size(30, 30)
        Me.btnRefreshDB.TabIndex = 184
        Me.ToolTip1.SetToolTip(Me.btnRefreshDB, "Refresh with selected Database")
        Me.btnRefreshDB.UseVisualStyleBackColor = True
        Me.btnRefreshDB.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.btnSearch.Location = New System.Drawing.Point(168, 11)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(154, 38)
        Me.btnSearch.TabIndex = 182
        Me.btnSearch.Text = "Order# Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtOrderSearch
        '
        Me.txtOrderSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderSearch.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtOrderSearch.Location = New System.Drawing.Point(0, 11)
        Me.txtOrderSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtOrderSearch.Name = "txtOrderSearch"
        Me.txtOrderSearch.Size = New System.Drawing.Size(154, 33)
        Me.txtOrderSearch.TabIndex = 181
        '
        'btnSaveDefaultDB
        '
        Me.btnSaveDefaultDB.AutoSize = True
        Me.btnSaveDefaultDB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnSaveDefaultDB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveDefaultDB.Image = CType(resources.GetObject("btnSaveDefaultDB.Image"), System.Drawing.Image)
        Me.btnSaveDefaultDB.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSaveDefaultDB.Location = New System.Drawing.Point(866, 6)
        Me.btnSaveDefaultDB.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSaveDefaultDB.Name = "btnSaveDefaultDB"
        Me.btnSaveDefaultDB.Size = New System.Drawing.Size(30, 30)
        Me.btnSaveDefaultDB.TabIndex = 185
        Me.ToolTip1.SetToolTip(Me.btnSaveDefaultDB, "Save As Default Database")
        Me.btnSaveDefaultDB.UseVisualStyleBackColor = True
        Me.btnSaveDefaultDB.Visible = False
        '
        'lblLabelList
        '
        Me.lblLabelList.AutoSize = True
        Me.lblLabelList.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.lblLabelList.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblLabelList.Location = New System.Drawing.Point(495, 11)
        Me.lblLabelList.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLabelList.Name = "lblLabelList"
        Me.lblLabelList.Size = New System.Drawing.Size(218, 38)
        Me.lblLabelList.TabIndex = 179
        Me.lblLabelList.Text = "Retail Label List"
        '
        'Timer1
        '
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Cornsilk
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(1668, 82)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(18, 31)
        Me.btnRefresh.TabIndex = 176
        Me.btnRefresh.Text = "Load Pending"
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Reload confirm picked orders")
        Me.btnRefresh.UseVisualStyleBackColor = False
        Me.btnRefresh.Visible = False
        '
        'btnReprint
        '
        Me.btnReprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReprint.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReprint.Location = New System.Drawing.Point(1716, 65)
        Me.btnReprint.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnReprint.Name = "btnReprint"
        Me.btnReprint.Size = New System.Drawing.Size(15, 43)
        Me.btnReprint.TabIndex = 177
        Me.btnReprint.Text = "Load RePrint"
        Me.ToolTip1.SetToolTip(Me.btnReprint, "Load previouisly printed labels")
        Me.btnReprint.UseVisualStyleBackColor = True
        Me.btnReprint.Visible = False
        '
        'btnRemove
        '
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemove.Location = New System.Drawing.Point(1695, 71)
        Me.btnRemove.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(14, 43)
        Me.btnRemove.TabIndex = 181
        Me.btnRemove.Text = "Remove "
        Me.ToolTip1.SetToolTip(Me.btnRemove, "Removes checked items from the list.  Right Click to reset the warning dialog 'It" &
        "ems will be marked Printed'")
        Me.btnRemove.UseVisualStyleBackColor = True
        Me.btnRemove.Visible = False
        '
        'Timer2
        '
        '
        'Panel4
        '
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 120)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(21, 814)
        Me.Panel4.TabIndex = 182
        '
        'Panel5
        '
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel5.Location = New System.Drawing.Point(1387, 120)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(21, 814)
        Me.Panel5.TabIndex = 183
        '
        'Panel6
        '
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(21, 120)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1366, 15)
        Me.Panel6.TabIndex = 184
        '
        'CtlBartender1
        '
        Me.CtlBartender1.Dock = System.Windows.Forms.DockStyle.Top
        Me.CtlBartender1.Location = New System.Drawing.Point(0, 0)
        Me.CtlBartender1.Margin = New System.Windows.Forms.Padding(6)
        Me.CtlBartender1.Name = "CtlBartender1"
        Me.CtlBartender1.Size = New System.Drawing.Size(1408, 120)
        Me.CtlBartender1.TabIndex = 0
        '
        'PickedOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1408, 934)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnReprint)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.CtlBartender1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "PickedOrder"
        Me.Text = "Retail Tag Printer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents CtlBartender1 As PickedItemLabelPriint.ctlBartender
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblLabelList As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtOrderSearch As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveRowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnRefreshDB As System.Windows.Forms.Button
    Friend WithEvents cbDBList As System.Windows.Forms.ComboBox
    Friend WithEvents btnSaveDefaultDB As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnReprint As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblPricingType As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblDivider1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblDivider2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblCurrentDB As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblDivider3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblDefaultDB As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtUserAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CtlBartender1 As PickedItemLabelPriint.ctlBartender
    Friend WithEvents btnPricesAreMissing As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblServer As ToolStripStatusLabel
End Class
