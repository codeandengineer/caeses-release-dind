<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSectionPicker
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
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSectionPicker))
      Me.tvDatabases = New System.Windows.Forms.TreeView()
      Me.btnSelect = New System.Windows.Forms.Button()
      Me.btnCancel = New System.Windows.Forms.Button()
      Me.lbProfiles = New System.Windows.Forms.ListBox()
      Me.gbIShapeSpecification = New System.Windows.Forms.GroupBox()
      Me.txtISpecBT = New System.Windows.Forms.TextBox()
      Me.txtISpecCD = New System.Windows.Forms.TextBox()
      Me.txtISpecBW = New System.Windows.Forms.TextBox()
      Me.txtISpecCW = New System.Windows.Forms.TextBox()
      Me.Label9 = New System.Windows.Forms.Label()
      Me.txtISpecTH = New System.Windows.Forms.TextBox()
      Me.Label5 = New System.Windows.Forms.Label()
      Me.Label8 = New System.Windows.Forms.Label()
      Me.txtISpecFC = New System.Windows.Forms.TextBox()
      Me.txtISpecWP = New System.Windows.Forms.TextBox()
      Me.Label4 = New System.Windows.Forms.Label()
      Me.Label7 = New System.Windows.Forms.Label()
      Me.txtISpecCT = New System.Windows.Forms.TextBox()
      Me.Label3 = New System.Windows.Forms.Label()
      Me.lblISpecWP = New System.Windows.Forms.Label()
      Me.txtISpecSP = New System.Windows.Forms.TextBox()
      Me.Label2 = New System.Windows.Forms.Label()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.radITopCover = New System.Windows.Forms.RadioButton()
      Me.radITopAndBottomCover = New System.Windows.Forms.RadioButton()
      Me.radIBottomCover = New System.Windows.Forms.RadioButton()
      Me.radIComposite = New System.Windows.Forms.RadioButton()
      Me.radIDouble = New System.Windows.Forms.RadioButton()
      Me.radITee = New System.Windows.Forms.RadioButton()
      Me.radISingle = New System.Windows.Forms.RadioButton()
      Me.gbSections = New System.Windows.Forms.GroupBox()
      Me.gbMaterials = New System.Windows.Forms.GroupBox()
      Me.Label10 = New System.Windows.Forms.Label()
      Me.Label6 = New System.Windows.Forms.Label()
      Me.cmbMatGrades = New System.Windows.Forms.ComboBox()
      Me.cmbMatDatabases = New System.Windows.Forms.ComboBox()
      Me.gbCShapeSpecification = New System.Windows.Forms.GroupBox()
      Me.txtCSpecSP = New System.Windows.Forms.TextBox()
      Me.Label19 = New System.Windows.Forms.Label()
      Me.radCDoubleF2F = New System.Windows.Forms.RadioButton()
      Me.radCDoubleB2B = New System.Windows.Forms.RadioButton()
      Me.radCDoubleBADisabled = New System.Windows.Forms.RadioButton()
      Me.radCSingle = New System.Windows.Forms.RadioButton()
      Me.gbLShapeSpecification = New System.Windows.Forms.GroupBox()
      Me.txtLSpecSP = New System.Windows.Forms.TextBox()
      Me.Label11 = New System.Windows.Forms.Label()
      Me.radLDoubleSD = New System.Windows.Forms.RadioButton()
      Me.radLDoubleLD = New System.Windows.Forms.RadioButton()
      Me.radLDoubleSA = New System.Windows.Forms.RadioButton()
      Me.radLSingleRA = New System.Windows.Forms.RadioButton()
      Me.radLSingle = New System.Windows.Forms.RadioButton()
      Me.gbIShapeSpecification.SuspendLayout()
      Me.gbSections.SuspendLayout()
      Me.gbMaterials.SuspendLayout()
      Me.gbCShapeSpecification.SuspendLayout()
      Me.gbLShapeSpecification.SuspendLayout()
      Me.SuspendLayout()
      '
      'tvDatabases
      '
      Me.tvDatabases.HideSelection = False
      Me.tvDatabases.Indent = 20
      Me.tvDatabases.Location = New System.Drawing.Point(6, 15)
      Me.tvDatabases.Name = "tvDatabases"
      Me.tvDatabases.Size = New System.Drawing.Size(200, 342)
      Me.tvDatabases.TabIndex = 0
      '
      'btnSelect
      '
      Me.btnSelect.Enabled = False
      Me.btnSelect.Location = New System.Drawing.Point(339, 418)
      Me.btnSelect.Name = "btnSelect"
      Me.btnSelect.Size = New System.Drawing.Size(132, 23)
      Me.btnSelect.TabIndex = 5
      Me.btnSelect.Text = "&Select"
      Me.btnSelect.UseVisualStyleBackColor = True
      '
      'btnCancel
      '
      Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.btnCancel.Location = New System.Drawing.Point(477, 418)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.Size = New System.Drawing.Size(132, 23)
      Me.btnCancel.TabIndex = 6
      Me.btnCancel.Text = "&Cancel"
      Me.btnCancel.UseVisualStyleBackColor = True
      '
      'lbProfiles
      '
      Me.lbProfiles.FormattingEnabled = True
      Me.lbProfiles.Location = New System.Drawing.Point(212, 15)
      Me.lbProfiles.Name = "lbProfiles"
      Me.lbProfiles.Size = New System.Drawing.Size(100, 342)
      Me.lbProfiles.TabIndex = 1
      '
      'gbIShapeSpecification
      '
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecBT)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecCD)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecBW)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecCW)
      Me.gbIShapeSpecification.Controls.Add(Me.Label9)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecTH)
      Me.gbIShapeSpecification.Controls.Add(Me.Label5)
      Me.gbIShapeSpecification.Controls.Add(Me.Label8)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecFC)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecWP)
      Me.gbIShapeSpecification.Controls.Add(Me.Label4)
      Me.gbIShapeSpecification.Controls.Add(Me.Label7)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecCT)
      Me.gbIShapeSpecification.Controls.Add(Me.Label3)
      Me.gbIShapeSpecification.Controls.Add(Me.lblISpecWP)
      Me.gbIShapeSpecification.Controls.Add(Me.txtISpecSP)
      Me.gbIShapeSpecification.Controls.Add(Me.Label2)
      Me.gbIShapeSpecification.Controls.Add(Me.Label1)
      Me.gbIShapeSpecification.Controls.Add(Me.radITopCover)
      Me.gbIShapeSpecification.Controls.Add(Me.radITopAndBottomCover)
      Me.gbIShapeSpecification.Controls.Add(Me.radIBottomCover)
      Me.gbIShapeSpecification.Controls.Add(Me.radIComposite)
      Me.gbIShapeSpecification.Controls.Add(Me.radIDouble)
      Me.gbIShapeSpecification.Controls.Add(Me.radITee)
      Me.gbIShapeSpecification.Controls.Add(Me.radISingle)
      Me.gbIShapeSpecification.Location = New System.Drawing.Point(340, 12)
      Me.gbIShapeSpecification.Name = "gbIShapeSpecification"
      Me.gbIShapeSpecification.Size = New System.Drawing.Size(270, 400)
      Me.gbIShapeSpecification.TabIndex = 2
      Me.gbIShapeSpecification.TabStop = False
      Me.gbIShapeSpecification.Text = "Type Specification for I Shapes"
      '
      'txtISpecBT
      '
      Me.txtISpecBT.Location = New System.Drawing.Point(167, 369)
      Me.txtISpecBT.Name = "txtISpecBT"
      Me.txtISpecBT.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecBT.TabIndex = 24
      '
      'txtISpecCD
      '
      Me.txtISpecCD.Location = New System.Drawing.Point(167, 205)
      Me.txtISpecCD.Name = "txtISpecCD"
      Me.txtISpecCD.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecCD.TabIndex = 13
      '
      'txtISpecBW
      '
      Me.txtISpecBW.Location = New System.Drawing.Point(167, 343)
      Me.txtISpecBW.Name = "txtISpecBW"
      Me.txtISpecBW.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecBW.TabIndex = 22
      '
      'txtISpecCW
      '
      Me.txtISpecCW.Location = New System.Drawing.Point(167, 179)
      Me.txtISpecCW.Name = "txtISpecCW"
      Me.txtISpecCW.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecCW.TabIndex = 11
      '
      'Label9
      '
      Me.Label9.AutoSize = True
      Me.Label9.Location = New System.Drawing.Point(23, 372)
      Me.Label9.Name = "Label9"
      Me.Label9.Size = New System.Drawing.Size(142, 13)
      Me.Label9.TabIndex = 23
      Me.Label9.Text = "BT (Bottom Plate Thickness)"
      '
      'txtISpecTH
      '
      Me.txtISpecTH.Location = New System.Drawing.Point(167, 317)
      Me.txtISpecTH.Name = "txtISpecTH"
      Me.txtISpecTH.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecTH.TabIndex = 20
      '
      'Label5
      '
      Me.Label5.AutoSize = True
      Me.Label5.Location = New System.Drawing.Point(24, 208)
      Me.Label5.Name = "Label5"
      Me.Label5.Size = New System.Drawing.Size(124, 13)
      Me.Label5.TabIndex = 12
      Me.Label5.Text = "CD (Density of Concrete)"
      '
      'Label8
      '
      Me.Label8.AutoSize = True
      Me.Label8.Location = New System.Drawing.Point(23, 346)
      Me.Label8.Name = "Label8"
      Me.Label8.Size = New System.Drawing.Size(125, 13)
      Me.Label8.TabIndex = 21
      Me.Label8.Text = "BW (Bottom Plate Width)"
      '
      'txtISpecFC
      '
      Me.txtISpecFC.Location = New System.Drawing.Point(167, 153)
      Me.txtISpecFC.Name = "txtISpecFC"
      Me.txtISpecFC.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecFC.TabIndex = 9
      '
      'txtISpecWP
      '
      Me.txtISpecWP.Location = New System.Drawing.Point(167, 291)
      Me.txtISpecWP.Name = "txtISpecWP"
      Me.txtISpecWP.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecWP.TabIndex = 18
      '
      'Label4
      '
      Me.Label4.AutoSize = True
      Me.Label4.Location = New System.Drawing.Point(24, 182)
      Me.Label4.Name = "Label4"
      Me.Label4.Size = New System.Drawing.Size(108, 13)
      Me.Label4.TabIndex = 10
      Me.Label4.Text = "CW (Concrete Width)"
      '
      'Label7
      '
      Me.Label7.AutoSize = True
      Me.Label7.Location = New System.Drawing.Point(23, 320)
      Me.Label7.Name = "Label7"
      Me.Label7.Size = New System.Drawing.Size(138, 13)
      Me.Label7.TabIndex = 19
      Me.Label7.Text = "TH (Cover Plate Thickness)"
      '
      'txtISpecCT
      '
      Me.txtISpecCT.Location = New System.Drawing.Point(167, 127)
      Me.txtISpecCT.Name = "txtISpecCT"
      Me.txtISpecCT.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecCT.TabIndex = 7
      '
      'Label3
      '
      Me.Label3.AutoSize = True
      Me.Label3.Location = New System.Drawing.Point(24, 156)
      Me.Label3.Name = "Label3"
      Me.Label3.Size = New System.Drawing.Size(104, 13)
      Me.Label3.TabIndex = 8
      Me.Label3.Text = "FC (Concrete Grade)"
      '
      'lblISpecWP
      '
      Me.lblISpecWP.AutoSize = True
      Me.lblISpecWP.Location = New System.Drawing.Point(23, 294)
      Me.lblISpecWP.Name = "lblISpecWP"
      Me.lblISpecWP.Size = New System.Drawing.Size(120, 13)
      Me.lblISpecWP.TabIndex = 17
      Me.lblISpecWP.Text = "WP (Cover Plate Width)"
      '
      'txtISpecSP
      '
      Me.txtISpecSP.Location = New System.Drawing.Point(167, 83)
      Me.txtISpecSP.Name = "txtISpecSP"
      Me.txtISpecSP.Size = New System.Drawing.Size(75, 20)
      Me.txtISpecSP.TabIndex = 4
      '
      'Label2
      '
      Me.Label2.AutoSize = True
      Me.Label2.Location = New System.Drawing.Point(24, 130)
      Me.Label2.Name = "Label2"
      Me.Label2.Size = New System.Drawing.Size(125, 13)
      Me.Label2.TabIndex = 6
      Me.Label2.Text = "CT (Concrete Thickness)"
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(24, 86)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(96, 13)
      Me.Label1.TabIndex = 3
      Me.Label1.Text = "SP (Clear Spacing)"
      '
      'radITopCover
      '
      Me.radITopCover.AutoSize = True
      Me.radITopCover.Enabled = False
      Me.radITopCover.Location = New System.Drawing.Point(6, 228)
      Me.radITopCover.Name = "radITopCover"
      Me.radITopCover.Size = New System.Drawing.Size(125, 17)
      Me.radITopCover.TabIndex = 14
      Me.radITopCover.TabStop = True
      Me.radITopCover.Text = "T&C (Top Cover Plate)"
      Me.radITopCover.UseVisualStyleBackColor = True
      '
      'radITopAndBottomCover
      '
      Me.radITopAndBottomCover.AutoSize = True
      Me.radITopAndBottomCover.Enabled = False
      Me.radITopAndBottomCover.Location = New System.Drawing.Point(6, 274)
      Me.radITopAndBottomCover.Name = "radITopAndBottomCover"
      Me.radITopAndBottomCover.Size = New System.Drawing.Size(170, 17)
      Me.radITopAndBottomCover.TabIndex = 16
      Me.radITopAndBottomCover.TabStop = True
      Me.radITopAndBottomCover.Text = "&TB (Top && Bottom Cover Plate)"
      Me.radITopAndBottomCover.UseVisualStyleBackColor = True
      '
      'radIBottomCover
      '
      Me.radIBottomCover.AutoSize = True
      Me.radIBottomCover.Enabled = False
      Me.radIBottomCover.Location = New System.Drawing.Point(6, 251)
      Me.radIBottomCover.Name = "radIBottomCover"
      Me.radIBottomCover.Size = New System.Drawing.Size(139, 17)
      Me.radIBottomCover.TabIndex = 15
      Me.radIBottomCover.TabStop = True
      Me.radIBottomCover.Text = "&BC (Bottom Cover Plate)"
      Me.radIBottomCover.UseVisualStyleBackColor = True
      '
      'radIComposite
      '
      Me.radIComposite.AutoSize = True
      Me.radIComposite.Enabled = False
      Me.radIComposite.Location = New System.Drawing.Point(6, 109)
      Me.radIComposite.Name = "radIComposite"
      Me.radIComposite.Size = New System.Drawing.Size(138, 17)
      Me.radIComposite.TabIndex = 5
      Me.radIComposite.TabStop = True
      Me.radIComposite.Text = "C&M (Composite Section)"
      Me.radIComposite.UseVisualStyleBackColor = True
      '
      'radIDouble
      '
      Me.radIDouble.AutoSize = True
      Me.radIDouble.Location = New System.Drawing.Point(6, 65)
      Me.radIDouble.Name = "radIDouble"
      Me.radIDouble.Size = New System.Drawing.Size(108, 17)
      Me.radIDouble.TabIndex = 2
      Me.radIDouble.TabStop = True
      Me.radIDouble.Text = "&D (Double Profile)"
      Me.radIDouble.UseVisualStyleBackColor = True
      '
      'radITee
      '
      Me.radITee.AutoSize = True
      Me.radITee.Location = New System.Drawing.Point(6, 42)
      Me.radITee.Name = "radITee"
      Me.radITee.Size = New System.Drawing.Size(171, 17)
      Me.radITee.TabIndex = 1
      Me.radITee.TabStop = True
      Me.radITee.Text = "&T (Tee Section Cut from Beam)"
      Me.radITee.UseVisualStyleBackColor = True
      '
      'radISingle
      '
      Me.radISingle.AutoSize = True
      Me.radISingle.Location = New System.Drawing.Point(6, 19)
      Me.radISingle.Name = "radISingle"
      Me.radISingle.Size = New System.Drawing.Size(169, 17)
      Me.radISingle.TabIndex = 0
      Me.radISingle.TabStop = True
      Me.radISingle.Text = "&ST (Single Section from Table)"
      Me.radISingle.UseVisualStyleBackColor = True
      '
      'gbSections
      '
      Me.gbSections.Controls.Add(Me.tvDatabases)
      Me.gbSections.Controls.Add(Me.lbProfiles)
      Me.gbSections.Location = New System.Drawing.Point(12, 12)
      Me.gbSections.Name = "gbSections"
      Me.gbSections.Size = New System.Drawing.Size(321, 363)
      Me.gbSections.TabIndex = 0
      Me.gbSections.TabStop = False
      Me.gbSections.Text = "Section Profiles"
      '
      'gbMaterials
      '
      Me.gbMaterials.Controls.Add(Me.Label10)
      Me.gbMaterials.Controls.Add(Me.Label6)
      Me.gbMaterials.Controls.Add(Me.cmbMatGrades)
      Me.gbMaterials.Controls.Add(Me.cmbMatDatabases)
      Me.gbMaterials.Location = New System.Drawing.Point(12, 381)
      Me.gbMaterials.Name = "gbMaterials"
      Me.gbMaterials.Size = New System.Drawing.Size(321, 60)
      Me.gbMaterials.TabIndex = 1
      Me.gbMaterials.TabStop = False
      Me.gbMaterials.Text = "Materials"
      '
      'Label10
      '
      Me.Label10.AutoSize = True
      Me.Label10.Location = New System.Drawing.Point(170, 27)
      Me.Label10.Name = "Label10"
      Me.Label10.Size = New System.Drawing.Size(36, 13)
      Me.Label10.TabIndex = 2
      Me.Label10.Text = "Grade"
      '
      'Label6
      '
      Me.Label6.AutoSize = True
      Me.Label6.Location = New System.Drawing.Point(3, 27)
      Me.Label6.Name = "Label6"
      Me.Label6.Size = New System.Drawing.Size(53, 13)
      Me.Label6.TabIndex = 0
      Me.Label6.Text = "Database"
      '
      'cmbMatGrades
      '
      Me.cmbMatGrades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.cmbMatGrades.FormattingEnabled = True
      Me.cmbMatGrades.Location = New System.Drawing.Point(212, 24)
      Me.cmbMatGrades.Name = "cmbMatGrades"
      Me.cmbMatGrades.Size = New System.Drawing.Size(103, 21)
      Me.cmbMatGrades.TabIndex = 3
      '
      'cmbMatDatabases
      '
      Me.cmbMatDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.cmbMatDatabases.FormattingEnabled = True
      Me.cmbMatDatabases.Location = New System.Drawing.Point(61, 24)
      Me.cmbMatDatabases.Name = "cmbMatDatabases"
      Me.cmbMatDatabases.Size = New System.Drawing.Size(103, 21)
      Me.cmbMatDatabases.TabIndex = 1
      '
      'gbCShapeSpecification
      '
      Me.gbCShapeSpecification.Controls.Add(Me.txtCSpecSP)
      Me.gbCShapeSpecification.Controls.Add(Me.Label19)
      Me.gbCShapeSpecification.Controls.Add(Me.radCDoubleF2F)
      Me.gbCShapeSpecification.Controls.Add(Me.radCDoubleB2B)
      Me.gbCShapeSpecification.Controls.Add(Me.radCDoubleBADisabled)
      Me.gbCShapeSpecification.Controls.Add(Me.radCSingle)
      Me.gbCShapeSpecification.Location = New System.Drawing.Point(340, 12)
      Me.gbCShapeSpecification.Name = "gbCShapeSpecification"
      Me.gbCShapeSpecification.Size = New System.Drawing.Size(270, 400)
      Me.gbCShapeSpecification.TabIndex = 3
      Me.gbCShapeSpecification.TabStop = False
      Me.gbCShapeSpecification.Text = "Type Specification for C Shapes"
      '
      'txtCSpecSP
      '
      Me.txtCSpecSP.Location = New System.Drawing.Point(167, 106)
      Me.txtCSpecSP.Name = "txtCSpecSP"
      Me.txtCSpecSP.Size = New System.Drawing.Size(75, 20)
      Me.txtCSpecSP.TabIndex = 5
      '
      'Label19
      '
      Me.Label19.AutoSize = True
      Me.Label19.Location = New System.Drawing.Point(24, 109)
      Me.Label19.Name = "Label19"
      Me.Label19.Size = New System.Drawing.Size(96, 13)
      Me.Label19.TabIndex = 4
      Me.Label19.Text = "SP (Clear Spacing)"
      '
      'radCDoubleF2F
      '
      Me.radCDoubleF2F.AutoSize = True
      Me.radCDoubleF2F.Location = New System.Drawing.Point(6, 88)
      Me.radCDoubleF2F.Name = "radCDoubleF2F"
      Me.radCDoubleF2F.Size = New System.Drawing.Size(190, 17)
      Me.radCDoubleF2F.TabIndex = 3
      Me.radCDoubleF2F.TabStop = True
      Me.radCDoubleF2F.Text = "&FR (Double Channel Front-to-Front)"
      Me.radCDoubleF2F.UseVisualStyleBackColor = True
      '
      'radCDoubleB2B
      '
      Me.radCDoubleB2B.AutoSize = True
      Me.radCDoubleB2B.Location = New System.Drawing.Point(6, 65)
      Me.radCDoubleB2B.Name = "radCDoubleB2B"
      Me.radCDoubleB2B.Size = New System.Drawing.Size(186, 17)
      Me.radCDoubleB2B.TabIndex = 2
      Me.radCDoubleB2B.TabStop = True
      Me.radCDoubleB2B.Text = "D (Double Channel Back-to-Back)"
      Me.radCDoubleB2B.UseVisualStyleBackColor = True
      '
      'radCDoubleBADisabled
      '
      Me.radCDoubleBADisabled.AutoSize = True
      Me.radCDoubleBADisabled.Enabled = False
      Me.radCDoubleBADisabled.Location = New System.Drawing.Point(6, 42)
      Me.radCDoubleBADisabled.Name = "radCDoubleBADisabled"
      Me.radCDoubleBADisabled.Size = New System.Drawing.Size(192, 17)
      Me.radCDoubleBADisabled.TabIndex = 1
      Me.radCDoubleBADisabled.TabStop = True
      Me.radCDoubleBADisabled.Text = "BA (Double Channel Back-to-Back)"
      Me.radCDoubleBADisabled.UseVisualStyleBackColor = True
      '
      'radCSingle
      '
      Me.radCSingle.AutoSize = True
      Me.radCSingle.Location = New System.Drawing.Point(6, 19)
      Me.radCSingle.Name = "radCSingle"
      Me.radCSingle.Size = New System.Drawing.Size(169, 17)
      Me.radCSingle.TabIndex = 0
      Me.radCSingle.TabStop = True
      Me.radCSingle.Text = "&ST (Single Section from Table)"
      Me.radCSingle.UseVisualStyleBackColor = True
      '
      'gbLShapeSpecification
      '
      Me.gbLShapeSpecification.Controls.Add(Me.txtLSpecSP)
      Me.gbLShapeSpecification.Controls.Add(Me.Label11)
      Me.gbLShapeSpecification.Controls.Add(Me.radLDoubleSD)
      Me.gbLShapeSpecification.Controls.Add(Me.radLDoubleLD)
      Me.gbLShapeSpecification.Controls.Add(Me.radLDoubleSA)
      Me.gbLShapeSpecification.Controls.Add(Me.radLSingleRA)
      Me.gbLShapeSpecification.Controls.Add(Me.radLSingle)
      Me.gbLShapeSpecification.Location = New System.Drawing.Point(340, 12)
      Me.gbLShapeSpecification.Name = "gbLShapeSpecification"
      Me.gbLShapeSpecification.Size = New System.Drawing.Size(270, 400)
      Me.gbLShapeSpecification.TabIndex = 4
      Me.gbLShapeSpecification.TabStop = False
      Me.gbLShapeSpecification.Text = "Type Specification for L Shapes"
      '
      'txtLSpecSP
      '
      Me.txtLSpecSP.Location = New System.Drawing.Point(167, 106)
      Me.txtLSpecSP.Name = "txtLSpecSP"
      Me.txtLSpecSP.Size = New System.Drawing.Size(75, 20)
      Me.txtLSpecSP.TabIndex = 5
      '
      'Label11
      '
      Me.Label11.AutoSize = True
      Me.Label11.Location = New System.Drawing.Point(24, 109)
      Me.Label11.Name = "Label11"
      Me.Label11.Size = New System.Drawing.Size(96, 13)
      Me.Label11.TabIndex = 4
      Me.Label11.Text = "SP (Clear Spacing)"
      '
      'radLDoubleSD
      '
      Me.radLDoubleSD.AutoSize = True
      Me.radLDoubleSD.Location = New System.Drawing.Point(6, 88)
      Me.radLDoubleSD.Name = "radLDoubleSD"
      Me.radLDoubleSD.Size = New System.Drawing.Size(237, 17)
      Me.radLDoubleSD.TabIndex = 3
      Me.radLDoubleSD.TabStop = True
      Me.radLDoubleSD.Text = "&SD (Long Leg Back-to-Back. Double Angles)"
      Me.radLDoubleSD.UseVisualStyleBackColor = True
      '
      'radLDoubleLD
      '
      Me.radLDoubleLD.AutoSize = True
      Me.radLDoubleLD.Location = New System.Drawing.Point(6, 65)
      Me.radLDoubleLD.Name = "radLDoubleLD"
      Me.radLDoubleLD.Size = New System.Drawing.Size(236, 17)
      Me.radLDoubleLD.TabIndex = 2
      Me.radLDoubleLD.TabStop = True
      Me.radLDoubleLD.Text = "&LD (Long Leg Back-to-Back. Double Angles)"
      Me.radLDoubleLD.UseVisualStyleBackColor = True
      '
      'radLDoubleSA
      '
      Me.radLDoubleSA.AutoSize = True
      Me.radLDoubleSA.Enabled = False
      Me.radLDoubleSA.Location = New System.Drawing.Point(6, 132)
      Me.radLDoubleSA.Name = "radLDoubleSA"
      Me.radLDoubleSA.Size = New System.Drawing.Size(167, 17)
      Me.radLDoubleSA.TabIndex = 6
      Me.radLDoubleSA.TabStop = True
      Me.radLDoubleSA.Text = "S&A (Star Angle. Double Angle)"
      Me.radLDoubleSA.UseVisualStyleBackColor = True
      '
      'radLSingleRA
      '
      Me.radLSingleRA.AutoSize = True
      Me.radLSingleRA.Location = New System.Drawing.Point(6, 42)
      Me.radLSingleRA.Name = "radLSingleRA"
      Me.radLSingleRA.Size = New System.Drawing.Size(215, 17)
      Me.radLSingleRA.TabIndex = 1
      Me.radLSingleRA.TabStop = True
      Me.radLSingleRA.Text = "&RA (Single Angle with Reverse Y-Z Axis)"
      Me.radLSingleRA.UseVisualStyleBackColor = True
      '
      'radLSingle
      '
      Me.radLSingle.AutoSize = True
      Me.radLSingle.Location = New System.Drawing.Point(6, 19)
      Me.radLSingle.Name = "radLSingle"
      Me.radLSingle.Size = New System.Drawing.Size(169, 17)
      Me.radLSingle.TabIndex = 0
      Me.radLSingle.TabStop = True
      Me.radLSingle.Text = "&ST (Single Section from Table)"
      Me.radLSingle.UseVisualStyleBackColor = True
      '
      'frmSectionPicker
      '
      Me.AcceptButton = Me.btnSelect
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.CancelButton = Me.btnCancel
      Me.ClientSize = New System.Drawing.Size(619, 451)
      Me.Controls.Add(Me.gbMaterials)
      Me.Controls.Add(Me.gbIShapeSpecification)
      Me.Controls.Add(Me.gbCShapeSpecification)
      Me.Controls.Add(Me.btnCancel)
      Me.Controls.Add(Me.btnSelect)
      Me.Controls.Add(Me.gbSections)
      Me.Controls.Add(Me.gbLShapeSpecification)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Name = "frmSectionPicker"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.Text = "Section Profiles"
      Me.gbIShapeSpecification.ResumeLayout(False)
      Me.gbIShapeSpecification.PerformLayout()
      Me.gbSections.ResumeLayout(False)
      Me.gbMaterials.ResumeLayout(False)
      Me.gbMaterials.PerformLayout()
      Me.gbCShapeSpecification.ResumeLayout(False)
      Me.gbCShapeSpecification.PerformLayout()
      Me.gbLShapeSpecification.ResumeLayout(False)
      Me.gbLShapeSpecification.PerformLayout()
      Me.ResumeLayout(False)

   End Sub
   Friend WithEvents btnSelect As System.Windows.Forms.Button
   Friend WithEvents tvDatabases As System.Windows.Forms.TreeView
   Friend WithEvents btnCancel As System.Windows.Forms.Button
   Friend WithEvents lbProfiles As System.Windows.Forms.ListBox
   Friend WithEvents gbIShapeSpecification As System.Windows.Forms.GroupBox
   Friend WithEvents radITopCover As System.Windows.Forms.RadioButton
   Friend WithEvents radIBottomCover As System.Windows.Forms.RadioButton
   Friend WithEvents radIComposite As System.Windows.Forms.RadioButton
   Friend WithEvents radIDouble As System.Windows.Forms.RadioButton
   Friend WithEvents radITee As System.Windows.Forms.RadioButton
   Friend WithEvents radISingle As System.Windows.Forms.RadioButton
   Friend WithEvents radITopAndBottomCover As System.Windows.Forms.RadioButton
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents txtISpecSP As System.Windows.Forms.TextBox
   Friend WithEvents txtISpecCT As System.Windows.Forms.TextBox
   Friend WithEvents Label2 As System.Windows.Forms.Label
   Friend WithEvents txtISpecCD As System.Windows.Forms.TextBox
   Friend WithEvents txtISpecCW As System.Windows.Forms.TextBox
   Friend WithEvents Label5 As System.Windows.Forms.Label
   Friend WithEvents txtISpecFC As System.Windows.Forms.TextBox
   Friend WithEvents Label4 As System.Windows.Forms.Label
   Friend WithEvents Label3 As System.Windows.Forms.Label
   Friend WithEvents txtISpecBT As System.Windows.Forms.TextBox
   Friend WithEvents txtISpecBW As System.Windows.Forms.TextBox
   Friend WithEvents Label9 As System.Windows.Forms.Label
   Friend WithEvents txtISpecTH As System.Windows.Forms.TextBox
   Friend WithEvents Label8 As System.Windows.Forms.Label
   Friend WithEvents txtISpecWP As System.Windows.Forms.TextBox
   Friend WithEvents Label7 As System.Windows.Forms.Label
   Friend WithEvents lblISpecWP As System.Windows.Forms.Label
   Friend WithEvents gbSections As System.Windows.Forms.GroupBox
   Friend WithEvents gbMaterials As System.Windows.Forms.GroupBox
   Friend WithEvents cmbMatDatabases As System.Windows.Forms.ComboBox
   Friend WithEvents Label10 As System.Windows.Forms.Label
   Friend WithEvents Label6 As System.Windows.Forms.Label
   Friend WithEvents cmbMatGrades As System.Windows.Forms.ComboBox
   Friend WithEvents gbCShapeSpecification As System.Windows.Forms.GroupBox
   Friend WithEvents txtCSpecSP As System.Windows.Forms.TextBox
   Friend WithEvents Label19 As System.Windows.Forms.Label
   Friend WithEvents radCDoubleF2F As System.Windows.Forms.RadioButton
   Friend WithEvents radCDoubleBADisabled As System.Windows.Forms.RadioButton
   Friend WithEvents radCSingle As System.Windows.Forms.RadioButton
   Friend WithEvents radCDoubleB2B As System.Windows.Forms.RadioButton
   Friend WithEvents gbLShapeSpecification As System.Windows.Forms.GroupBox
   Friend WithEvents txtLSpecSP As System.Windows.Forms.TextBox
   Friend WithEvents Label11 As System.Windows.Forms.Label
   Friend WithEvents radLDoubleSD As System.Windows.Forms.RadioButton
   Friend WithEvents radLDoubleLD As System.Windows.Forms.RadioButton
   Friend WithEvents radLSingleRA As System.Windows.Forms.RadioButton
   Friend WithEvents radLSingle As System.Windows.Forms.RadioButton
   Friend WithEvents radLDoubleSA As System.Windows.Forms.RadioButton
End Class
