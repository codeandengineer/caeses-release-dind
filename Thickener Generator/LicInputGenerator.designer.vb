<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicInputGenerator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLicInputGenerator))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMachineName = New System.Windows.Forms.TextBox()
        Me.dtpLicenseStart = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnGetThisPCProps = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNumDays = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtLicUsername = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtLicCompany = New System.Windows.Forms.TextBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.gbLicenseType = New System.Windows.Forms.GroupBox()
        Me.cmbLicType = New System.Windows.Forms.ComboBox()
        Me.chkAgreement = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.gbLicenseType.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(318, 231)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Machine Name:"
        '
        'txtMachineName
        '
        Me.txtMachineName.Location = New System.Drawing.Point(154, 10)
        Me.txtMachineName.Multiline = True
        Me.txtMachineName.Name = "txtMachineName"
        Me.txtMachineName.Size = New System.Drawing.Size(239, 20)
        Me.txtMachineName.TabIndex = 1
        '
        'dtpLicenseStart
        '
        Me.dtpLicenseStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpLicenseStart.Location = New System.Drawing.Point(154, 64)
        Me.dtpLicenseStart.Name = "dtpLicenseStart"
        Me.dtpLicenseStart.Size = New System.Drawing.Size(111, 22)
        Me.dtpLicenseStart.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(12, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Username (with Domain):"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "License Start Date:"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(154, 37)
        Me.txtUsername.Multiline = True
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(239, 20)
        Me.txtUsername.TabIndex = 3
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.SystemColors.Control
        Me.btnGenerate.Enabled = False
        Me.btnGenerate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnGenerate.Location = New System.Drawing.Point(237, 231)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerate.TabIndex = 13
        Me.btnGenerate.Text = "&Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'btnGetThisPCProps
        '
        Me.btnGetThisPCProps.BackColor = System.Drawing.SystemColors.Control
        Me.btnGetThisPCProps.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnGetThisPCProps.Location = New System.Drawing.Point(15, 231)
        Me.btnGetThisPCProps.Name = "btnGetThisPCProps"
        Me.btnGetThisPCProps.Size = New System.Drawing.Size(128, 23)
        Me.btnGetThisPCProps.TabIndex = 12
        Me.btnGetThisPCProps.Text = "&Get This PC Properties"
        Me.btnGetThisPCProps.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Number of Days:"
        '
        'txtNumDays
        '
        Me.txtNumDays.Enabled = False
        Me.txtNumDays.Location = New System.Drawing.Point(154, 94)
        Me.txtNumDays.Multiline = True
        Me.txtNumDays.Name = "txtNumDays"
        Me.txtNumDays.Size = New System.Drawing.Size(111, 20)
        Me.txtNumDays.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(12, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Licensed Username:"
        '
        'txtLicUsername
        '
        Me.txtLicUsername.Location = New System.Drawing.Point(154, 121)
        Me.txtLicUsername.Multiline = True
        Me.txtLicUsername.Name = "txtLicUsername"
        Me.txtLicUsername.Size = New System.Drawing.Size(239, 20)
        Me.txtLicUsername.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 151)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Licensed Company:"
        '
        'txtLicCompany
        '
        Me.txtLicCompany.Location = New System.Drawing.Point(154, 148)
        Me.txtLicCompany.Multiline = True
        Me.txtLicCompany.Name = "txtLicCompany"
        Me.txtLicCompany.Size = New System.Drawing.Size(239, 20)
        Me.txtLicCompany.TabIndex = 11
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.SystemColors.Control
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Location = New System.Drawing.Point(149, 231)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(82, 23)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "Clea&r"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'gbLicenseType
        '
        Me.gbLicenseType.BackColor = System.Drawing.SystemColors.Control
        Me.gbLicenseType.Controls.Add(Me.cmbLicType)
        Me.gbLicenseType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gbLicenseType.Location = New System.Drawing.Point(271, 66)
        Me.gbLicenseType.Name = "gbLicenseType"
        Me.gbLicenseType.Size = New System.Drawing.Size(122, 48)
        Me.gbLicenseType.TabIndex = 19
        Me.gbLicenseType.TabStop = False
        Me.gbLicenseType.Text = "License Type"
        '
        'cmbLicType
        '
        Me.cmbLicType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLicType.FormattingEnabled = True
        Me.cmbLicType.Location = New System.Drawing.Point(7, 23)
        Me.cmbLicType.Name = "cmbLicType"
        Me.cmbLicType.Size = New System.Drawing.Size(105, 21)
        Me.cmbLicType.TabIndex = 0
        '
        'chkAgreement
        '
        Me.chkAgreement.AutoSize = True
        Me.chkAgreement.Location = New System.Drawing.Point(15, 189)
        Me.chkAgreement.MaximumSize = New System.Drawing.Size(375, 0)
        Me.chkAgreement.Name = "chkAgreement"
        Me.chkAgreement.Size = New System.Drawing.Size(15, 14)
        Me.chkAgreement.TabIndex = 20
        Me.chkAgreement.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(36, 189)
        Me.Label7.MaximumSize = New System.Drawing.Size(360, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(352, 26)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "I understand that the above information is required for the license file generati" & _
    "on and accept to collect the same."
        '
        'frmLicInputGenerator
        '
        Me.AcceptButton = Me.btnGenerate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(404, 281)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chkAgreement)
        Me.Controls.Add(Me.gbLicenseType)
        Me.Controls.Add(Me.dtpLicenseStart)
        Me.Controls.Add(Me.txtLicCompany)
        Me.Controls.Add(Me.txtLicUsername)
        Me.Controls.Add(Me.txtNumDays)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtMachineName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnGetThisPCProps)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.btnCancel)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmLicInputGenerator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "License Input File Generator"
        Me.gbLicenseType.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
   Friend WithEvents btnCancel As System.Windows.Forms.Button
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents txtMachineName As System.Windows.Forms.TextBox
   Friend WithEvents dtpLicenseStart As System.Windows.Forms.DateTimePicker
   Friend WithEvents Label2 As System.Windows.Forms.Label
   Friend WithEvents Label3 As System.Windows.Forms.Label
   Friend WithEvents txtUsername As System.Windows.Forms.TextBox
   Friend WithEvents btnGenerate As System.Windows.Forms.Button
   Friend WithEvents btnGetThisPCProps As System.Windows.Forms.Button
   Friend WithEvents Label4 As System.Windows.Forms.Label
   Friend WithEvents txtNumDays As System.Windows.Forms.TextBox
   Friend WithEvents Label5 As System.Windows.Forms.Label
   Friend WithEvents txtLicUsername As System.Windows.Forms.TextBox
   Friend WithEvents Label6 As System.Windows.Forms.Label
   Friend WithEvents txtLicCompany As System.Windows.Forms.TextBox
   Friend WithEvents btnClear As System.Windows.Forms.Button
   Friend WithEvents gbLicenseType As System.Windows.Forms.GroupBox
    Friend WithEvents cmbLicType As System.Windows.Forms.ComboBox
   Friend WithEvents chkAgreement As System.Windows.Forms.CheckBox
   Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
