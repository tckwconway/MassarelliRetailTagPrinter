<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctlBartender
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlBartender))
        Me.txtLabel = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picBartender = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnLoadBartenderLabel = New System.Windows.Forms.Button()
        Me.OpenFileDialogBartender = New System.Windows.Forms.OpenFileDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSeparator = New System.Windows.Forms.TextBox()
        Me.btnLoadBartenderSeparator = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboPrinters = New System.Windows.Forms.ComboBox()
        CType(Me.picBartender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtLabel
        '
        Me.txtLabel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabel.Location = New System.Drawing.Point(72, 3)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.ReadOnly = True
        Me.txtLabel.Size = New System.Drawing.Size(608, 20)
        Me.txtLabel.TabIndex = 181
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 180
        Me.Label1.Text = "&Label:"
        '
        'picBartender
        '
        Me.picBartender.Image = CType(resources.GetObject("picBartender.Image"), System.Drawing.Image)
        Me.picBartender.Location = New System.Drawing.Point(9, 5)
        Me.picBartender.Name = "picBartender"
        Me.picBartender.Size = New System.Drawing.Size(17, 16)
        Me.picBartender.TabIndex = 179
        Me.picBartender.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(9, 53)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 15)
        Me.PictureBox2.TabIndex = 177
        Me.PictureBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(26, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 176
        Me.Label7.Text = "&Printer:"
        '
        'btnPrint
        '
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(709, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(84, 41)
        Me.btnPrint.TabIndex = 175
        Me.btnPrint.Text = " Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnLoadBartenderLabel
        '
        Me.btnLoadBartenderLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadBartenderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadBartenderLabel.Image = CType(resources.GetObject("btnLoadBartenderLabel.Image"), System.Drawing.Image)
        Me.btnLoadBartenderLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadBartenderLabel.Location = New System.Drawing.Point(683, 3)
        Me.btnLoadBartenderLabel.Name = "btnLoadBartenderLabel"
        Me.btnLoadBartenderLabel.Size = New System.Drawing.Size(18, 18)
        Me.btnLoadBartenderLabel.TabIndex = 182
        Me.btnLoadBartenderLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnLoadBartenderLabel.UseVisualStyleBackColor = True
        '
        'OpenFileDialogBartender
        '
        Me.OpenFileDialogBartender.DefaultExt = "btw"
        Me.OpenFileDialogBartender.Filter = "BarTender Label Formats (*.btw)|*.btw"
        Me.OpenFileDialogBartender.Title = "Open BarTender Label Format"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 183
        Me.Label2.Text = "&Separator:"
        '
        'txtSeparator
        '
        Me.txtSeparator.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeparator.Location = New System.Drawing.Point(72, 26)
        Me.txtSeparator.Name = "txtSeparator"
        Me.txtSeparator.ReadOnly = True
        Me.txtSeparator.Size = New System.Drawing.Size(608, 20)
        Me.txtSeparator.TabIndex = 184
        '
        'btnLoadBartenderSeparator
        '
        Me.btnLoadBartenderSeparator.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadBartenderSeparator.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadBartenderSeparator.Image = CType(resources.GetObject("btnLoadBartenderSeparator.Image"), System.Drawing.Image)
        Me.btnLoadBartenderSeparator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadBartenderSeparator.Location = New System.Drawing.Point(683, 26)
        Me.btnLoadBartenderSeparator.Name = "btnLoadBartenderSeparator"
        Me.btnLoadBartenderSeparator.Size = New System.Drawing.Size(18, 18)
        Me.btnLoadBartenderSeparator.TabIndex = 185
        Me.btnLoadBartenderSeparator.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnLoadBartenderSeparator.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cboPrinters)
        Me.Panel1.Location = New System.Drawing.Point(71, 51)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(630, 22)
        Me.Panel1.TabIndex = 186
        '
        'cboPrinters
        '
        Me.cboPrinters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Location = New System.Drawing.Point(0, 0)
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(628, 21)
        Me.cboPrinters.TabIndex = 179
        '
        'ctlBartender
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnLoadBartenderSeparator)
        Me.Controls.Add(Me.txtSeparator)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnLoadBartenderLabel)
        Me.Controls.Add(Me.txtLabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.picBartender)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnPrint)
        Me.Name = "ctlBartender"
        Me.Size = New System.Drawing.Size(799, 80)
        CType(Me.picBartender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picBartender As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnLoadBartenderLabel As System.Windows.Forms.Button
    Private WithEvents OpenFileDialogBartender As System.Windows.Forms.OpenFileDialog
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSeparator As System.Windows.Forms.TextBox
    Friend WithEvents btnLoadBartenderSeparator As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboPrinters As System.Windows.Forms.ComboBox

End Class
