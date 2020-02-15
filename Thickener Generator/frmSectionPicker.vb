Option Strict On
Imports System.Xml
Imports SES.DataTypes
Public Class frmSectionPicker
   Private xCC As New sesCommonCodes
   Private xGen As sesGenSections
   Private xMat As sesMaterials
   Private xDoc As XmlDocument
   Private Const rootKey As String = "dbDatabases"
   Private Const rootText As String = "Databases"
   Private Const fileNameSectionPicker = "databases.xml"
   Private Const lenFormat As String = "0.000"
   Private Const lenUnit As String = "m"
   Private dbFileName As String
   Private resDirectory As String = ""
   Private MyImages As ImageList = New ImageList
   Private _prop As PropertyData
   Private _callCategory As SecPickCallCategory
   Private Sub FillTreeViewData()
      '/----- Enable the below commented line before the application release
        'resDirectory = System.Environment.CurrentDirectory & "\assets"
        resDirectory = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\" & PUBLISHER_DIR & "\assets"
      IsSecPickerLocked = True
      'Me.StartPosition = FormStartPosition.Manual

      Me.lbProfiles.Hide()
      Me.gbIShapeSpecification.Visible = False
      Me.gbCShapeSpecification.Visible = False
      Me.gbLShapeSpecification.Visible = False
      Me.cmbMatDatabases.Enabled = False
      Me.cmbMatGrades.Enabled = False


        'resDirectory = xCC.GetProgramsDir & "\assets"
      Try
         xDoc = New XmlDocument
         dbFileName = resDirectory & "\xml\" & fileNameSectionPicker
         xDoc.Load(dbFileName)
         Dim dbName As String = "", shapeName As String = "", secTypeName As String = ""
         LoadImageList()
         With Me.tvDatabases
            .Nodes.Clear()
            .ImageList = MyImages
            .Nodes.Add(rootKey, rootText, "db")
            For Each iNodeDB As XmlElement In xDoc.GetElementsByTagName("database")
               dbName = iNodeDB.GetAttribute("name").Trim
               With .Nodes(rootKey)
                  If dbName = "US Metric" Or dbName = "US Customary" Then
                     .Nodes.Add("db" & dbName, dbName, "flagAmerican", "flagAmerican")
                  Else
                     .Nodes.Add("db" & dbName, dbName, "flag" & dbName, "flag" & dbName)
                  End If
                  For Each iNodeShape As XmlElement In iNodeDB.GetElementsByTagName("shape")
                     With .Nodes("db" & dbName)
                        shapeName = iNodeShape.GetAttribute("name").Trim
                        .Nodes.Add("shape" & shapeName, shapeName & " Shape", "shape" & shapeName, "shape" & shapeName)
                        For Each iNodeSecType As XmlElement In iNodeShape.GetElementsByTagName("section-type")
                           With .Nodes("shape" & shapeName)
                              secTypeName = iNodeSecType.GetAttribute("name").Trim
                              .Nodes.Add("st" & secTypeName, secTypeName, "shape" & shapeName, "shape" & shapeName)
                           End With
                        Next
                     End With
                  Next
               End With
            Next
         End With
      Catch ex As Exception
         MsgBox(ex.Message)
      End Try
   End Sub
   Private Sub frmSectionPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Dim type As TypeSpec, addSpec1 As Double, addspec2 As Double
      With _prop
         If Not CObj(_prop) Is Nothing And (.name <> CHOOSE_SEC) AndAlso .name <> "" Then
            type = .type
            addSpec1 = .addSpec1
            addspec2 = .addSpec2
            Me.tvDatabases.SelectedNode = Me.tvDatabases.Nodes("dbDatabases").Nodes("db" & .dbName).Nodes("shape" & .shape).Nodes("st" & .shapeTypeKey)
            Me.lbProfiles.SelectedIndex = .nameIndex
            Me.cmbMatDatabases.SelectedIndex = .mData.dbNameIndex
            Me.cmbMatGrades.SelectedIndex = .mData.nameIndex
            If .shape = "I" Then
               Select Case type
                  Case TypeSpec.ST
                     Me.radISingle.Checked = True
                  Case TypeSpec.T
                     Me.radITee.Checked = True
                  Case TypeSpec.D
                     Me.radIDouble.Checked = True
                     Me.txtISpecSP.Text = CStr(addSpec1)
                  Case TypeSpec.CM
                     Me.radIComposite.Checked = True
                     Me.txtISpecCT.Text = CStr(addSpec1)
                     Me.txtISpecFC.Text = CStr(addspec2)
                  Case TypeSpec.TC
                     Me.radITopCover.Checked = True
                     Me.txtISpecWP.Text = CStr(addSpec1)
                     Me.txtISpecTH.Text = CStr(addspec2)
                  Case TypeSpec.BC
                     Me.radIBottomCover.Checked = True
                     Me.txtISpecWP.Text = CStr(addSpec1)
                     Me.txtISpecTH.Text = CStr(addspec2)
                  Case TypeSpec.TB
                     Me.radITopAndBottomCover.Checked = True
                     Me.txtISpecWP.Text = CStr(addSpec1)
                     Me.txtISpecTH.Text = CStr(addspec2)
               End Select
            ElseIf .shape = "C" Then
               Select Case type
                  Case TypeSpec.ST
                     Me.radCSingle.Checked = True
                  Case TypeSpec.D
                     Me.radCDoubleB2B.Checked = True
                     Me.txtCSpecSP.Text = CStr(addSpec1)
                  Case TypeSpec.FR
                     Me.radCDoubleF2F.Checked = True
                     Me.txtCSpecSP.Text = CStr(addSpec1)
                  Case TypeSpec.BA
                     Me.radCDoubleBADisabled.Checked = True
                     Me.txtCSpecSP.Text = CStr(addSpec1)
               End Select
            ElseIf .shape = "L" Then
               Select Case type
                  Case TypeSpec.ST
                     Me.radLSingle.Checked = True
                  Case TypeSpec.RA
                     Me.radLSingleRA.Checked = True
                  Case TypeSpec.LD
                     Me.radLDoubleLD.Checked = True
                     Me.txtLSpecSP.Text = CStr(addSpec1)
                  Case TypeSpec.SD
                     Me.radLDoubleSD.Checked = True
                     Me.txtLSpecSP.Text = CStr(addSpec1)
               End Select
            End If
         End If
      End With
   End Sub
   Private Sub LoadImageList()
      AddImageToImageList("database", "\icons\" & "db.ico")
      AddImageToImageList("flagIndian", "\flags\" & "flag_india.ico")
      AddImageToImageList("flagAmerican", "\flags\" & "flag_usa.ico")
      AddImageToImageList("flagRussian", "\flags\" & "flag_russia.ico")
      AddImageToImageList("flagMexican", "\flags\" & "flag_mexico.ico")
      AddImageToImageList("flagEuropean", "\flags\" & "flag_eu.ico")
      AddImageToImageList("flagJapanese", "\flags\" & "flag_japan.ico")
      AddImageToImageList("flagChilean", "\flags\" & "flag_chile.ico")
      AddImageToImageList("flagBritish", "\flags\" & "flag_uk.ico")
      AddImageToImageList("flagAustralian", "\flags\" & "flag_australia.ico")
      AddImageToImageList("flagChinese", "\flags\" & "flag_china.ico")

      AddImageToImageList("shapeI", "\icons\" & "i.ico")
      AddImageToImageList("shapeC", "\icons\" & "c.ico")
      AddImageToImageList("shapeL", "\icons\" & "l.ico")
      AddImageToImageList("shapeT", "\icons\" & "t.ico")
      AddImageToImageList("shapeCHS", "\icons\" & "chs.ico")
      AddImageToImageList("shapeTube", "\icons\" & "tube.ico")

      'AddImageToImageList("shapeI", "\flags\" & "i.ico")
      'AddImageToImageList("shapeC", "\flags\" & "c.ico")
      'AddImageToImageList("shapeL", "\flags\" & "l.ico")
      'AddImageToImageList("shapeT", "\flags\" & "t.ico")
      'AddImageToImageList("shapeCHS", "\flags\" & "chs.ico")
      'AddImageToImageList("shapeTube", "\flags\" & "tube.ico")
   End Sub
   Private Sub AddImageToImageList(ByVal imgKey As String, ByVal fPath As String)
      MyImages.Images.Add(imgKey, New Icon(resDirectory & fPath, New Size(16, 16)))
   End Sub
   Private Sub tvDatabases_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvDatabases.AfterSelect
      If tvDatabases.SelectedNode.Level = 3 Then
         Dim secTypeNode As TreeNode, shapeNode As TreeNode, dbNode As TreeNode
         Dim dbName As String, secType As String, shape As String, tableName As String ', profile As String
         Dim lstProfiles() As String
         With Me.tvDatabases
            secTypeNode = .SelectedNode
            shapeNode = secTypeNode.Parent
            dbNode = shapeNode.Parent
            dbName = dbNode.Text
            secType = secTypeNode.Text
            shape = Mid(shapeNode.Name, 6)
            _prop.shape = shape
         End With
         If shape = "I" Then
            xGen = New sesGenSections("xISections")
         ElseIf shape = "C" Then
            xGen = New sesGenSections("xCSections")
         ElseIf shape = "L" Then
            xGen = New sesGenSections("xLSections")
         ElseIf shape = "Tube" Then
            xGen = New sesGenSections("xTubeSections")
         ElseIf shape = "CHS" Then
            xGen = New sesGenSections("xPipeSections")
         End If
         tableName = xGen.FetchTableName(dbName, secType)
         _prop.tableName = tableName
         lstProfiles = xGen.GetProfilesFromXML(tableName)
         If (Not lstProfiles Is Nothing) AndAlso UBound(lstProfiles) >= 0 Then
            With lbProfiles
               .Items.Clear()
               For Each Profile In lstProfiles
                  .Items.Add(Profile)
               Next
               .SelectedIndex = 0
               .Show()
               btnSelect.Enabled = True
            End With
            'HideAllSpecificationGroupBoxes()
            ToggleSpecificationGroupBox(shape, True)
            ToggleMaterialComboBoxes(True)
         Else
            btnSelect.Enabled = False
            Me.lbProfiles.Items.Clear()
            Me.lbProfiles.Hide()
            'HideAllSpecificationGroupBoxes()
            ToggleSpecificationGroupBox(shape, False)
            ToggleMaterialComboBoxes(True)
         End If
      Else
         btnSelect.Enabled = False
         Me.lbProfiles.Items.Clear()
         Me.lbProfiles.Hide()
         HideAllSpecificationGroupBoxes()
         ToggleMaterialComboBoxes(False)
      End If
   End Sub
   Private Sub ToggleMaterialComboBoxes(ByVal toggle As Boolean)
      Me.cmbMatDatabases.Enabled = toggle
      Me.cmbMatGrades.Enabled = toggle
      ResetMaterialDatabases(toggle)
   End Sub
   Private Sub ResetMaterialDatabases(ByVal populate As Boolean)
      Dim dbList() As String
      With Me.cmbMatDatabases.Items
         .Clear()
         If populate = True Then
            Try
               If xMat Is Nothing Then xMat = New sesMaterials()
               dbList = xMat.GetDatabasesFromXML
               If UBound(dbList) >= 0 Then
                  Me.cmbMatDatabases.Items.Clear()
                  For Each db As String In dbList
                     cmbMatDatabases.Items.Add(db)
                  Next
                  cmbMatDatabases.SelectedIndex = 0
               End If
            Catch ex As Exception
               MsgBox("[Material Databases]: " & ex.Message, MsgBoxStyle.OkOnly)
            End Try
         End If
      End With
   End Sub
   Private Sub ToggleSpecificationGroupBox(ByVal valShape As String, ByVal toggle As Boolean)
      Select Case valShape
         Case "I"
            If Not Me.gbIShapeSpecification.Visible Then
               Me.gbIShapeSpecification.Visible = toggle
               Me.radISingle.Checked = True
            End If
            If Me.gbCShapeSpecification.Visible Then Me.gbCShapeSpecification.Visible = Not toggle
            If Me.gbLShapeSpecification.Visible Then Me.gbLShapeSpecification.Visible = Not toggle
         Case "C"
            If Not Me.gbCShapeSpecification.Visible Then
               Me.gbCShapeSpecification.Visible = toggle
               Me.radCSingle.Checked = True
            End If
            If Me.gbIShapeSpecification.Visible Then Me.gbIShapeSpecification.Visible = Not toggle
            If Me.gbLShapeSpecification.Visible Then Me.gbLShapeSpecification.Visible = Not toggle
         Case "L"
            If Not Me.gbLShapeSpecification.Visible Then
               Me.gbLShapeSpecification.Visible = toggle
               Me.radLSingle.Checked = True
            End If
            If Me.gbIShapeSpecification.Visible Then Me.gbIShapeSpecification.Visible = Not toggle
            If Me.gbCShapeSpecification.Visible Then Me.gbCShapeSpecification.Visible = Not toggle
         Case "CHS", "Tube"
            If Me.gbIShapeSpecification.Visible Then Me.gbIShapeSpecification.Visible = False
            If Me.gbCShapeSpecification.Visible Then Me.gbCShapeSpecification.Visible = False
            If Me.gbLShapeSpecification.Visible Then Me.gbLShapeSpecification.Visible = False
      End Select
   End Sub
   Private Sub HideAllSpecificationGroupBoxes()
      Me.gbIShapeSpecification.Visible = False
   End Sub
   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      IsSecPickerLocked = False
      PickedSection = ""
      Me.Close()
   End Sub
   Private Sub frmSectionPicker_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
      IsSecPickerLocked = False
   End Sub
   Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
      If IsInputDataComplete() Then
         GetAdditionalSpecData()
         With _prop
            .shapeTypeKey = Me.tvDatabases.SelectedNode.Text
            .dbName = (Me.tvDatabases.SelectedNode.Parent).Parent.Text
            .name = Me.lbProfiles.SelectedItem.ToString
            .nameIndex = Me.lbProfiles.SelectedIndex
            .mData.dbNameIndex = Me.cmbMatDatabases.SelectedIndex
            .mData.nameIndex = Me.cmbMatGrades.SelectedIndex
         End With
         xGen.GetGenSecProps(_prop.sData, _prop.tableName, _prop.name)
         Me.Close()
      Else
         Exit Sub
      End If
   End Sub
   Private Function IsTextBoxEmtpy(ByRef tb As TextBox, ByVal msg As String) As Boolean
      If tb.Text = "" Then
         MsgBox(msg & " shall not be empty", MsgBoxStyle.OkOnly)
         tb.Focus()
         Return True
      End If
      Return False
   End Function
   Private Function IsEntryValid(ByRef tb As TextBox, ByVal msg As String) As Boolean
      If tb.Name = "txtISpecSP" OrElse tb.Name = "txtCSpecSP" OrElse tb.Name = "txtLSpecSP" Then
         If Val(tb.Text) < 0.0 Then
            MsgBox(msg & " value is not valid.", MsgBoxStyle.OkOnly)
            tb.Focus()
            Return True
         End If
      Else
         If Val(tb.Text) <= 0.0 Then
            MsgBox(msg & " value is not valid.", MsgBoxStyle.OkOnly)
            tb.Focus()
            Return True
         End If
      End If
      Return False
   End Function
   Private Function IsInputDataComplete() As Boolean
      Dim b As Boolean = True
      With _prop
         If .shape = "I" Then
            If radIDouble.Checked Then
               If IsTextBoxEmtpy(txtISpecSP, "Clear spacing") Then Return Not b
               If IsEntryValid(txtISpecSP, "Clear spacing input") Then Return Not b
            ElseIf radIComposite.Checked Then
               If IsTextBoxEmtpy(txtISpecCT, "Concrete thickness") Then Return Not b
               If IsEntryValid(txtISpecCT, "Concrete thickness input") Then Return Not b
               If IsTextBoxEmtpy(txtISpecFC, "Concrete grade") Then Return Not b
               If IsEntryValid(txtISpecFC, "Concrete grade input") Then Return Not b
               If IsTextBoxEmtpy(txtISpecCW, "Concrete width") Then Return Not b
               If IsEntryValid(txtISpecCW, "Concrete width input") Then Return Not b
               If IsTextBoxEmtpy(txtISpecCD, "Density of concrete") Then Return Not b
               If IsEntryValid(txtISpecCD, "Density of concrete input") Then Return Not b
            ElseIf radITopCover.Checked OrElse radIBottomCover.Checked OrElse radITopAndBottomCover.Checked Then
               If IsTextBoxEmtpy(txtISpecWP, "Cover plate width") Then Return Not b
               If IsEntryValid(txtISpecWP, "Cover plate width input") Then Return Not b
               If IsTextBoxEmtpy(txtISpecTH, "Cover plate thickness") Then Return Not b
               If IsEntryValid(txtISpecTH, "Cover plate thickness input") Then Return Not b
               If radITopAndBottomCover.Checked Then
                  If IsTextBoxEmtpy(txtISpecBW, "Bottom plate width") Then Return Not b
                  If IsEntryValid(txtISpecBW, "Bottom plate width input") Then Return Not b
                  If IsTextBoxEmtpy(txtISpecBT, "Bottom plate thickness") Then Return Not b
                  If IsEntryValid(txtISpecBT, "Bottom plate thickness input") Then Return Not b
               End If
            End If
         ElseIf .shape = "C" Then
            If radCDoubleB2B.Checked OrElse radCDoubleF2F.Checked OrElse radCDoubleBADisabled.Checked Then
               If IsTextBoxEmtpy(txtCSpecSP, "Clear spacing") Then Return Not b
               If IsEntryValid(txtCSpecSP, "Clear spacing input") Then Return Not b
            End If
         ElseIf .shape = "L" Then
            If radLDoubleLD.Checked OrElse radLDoubleSD.Checked Then
               If IsTextBoxEmtpy(txtLSpecSP, "Clear spacing") Then Return Not b
               If IsEntryValid(txtLSpecSP, "Clear spacing input") Then Return Not b
            End If
         End If
      End With
      Return b
   End Function
   Friend Property PropertyData() As PropertyData
      Set(value As PropertyData)
         _prop = value
      End Set
      Get
         Return _prop
      End Get
   End Property
   'ReadOnly Property GetPropertyData() As PropertyData
   '   Get
   '      Return _prop
   '   End Get
   'End Property
   '/---------- Deny the user from creating an empty New -------------/
   Private Sub New()
      ' This call is required by the designer.
      InitializeComponent()
      ' Add any initialization after the InitializeComponent() call.
   End Sub
   Public Sub New(ByVal callCategory As SecPickCallCategory)
      ' This call is required by the designer.
      InitializeComponent()
      _callCategory = callCategory
      FillTreeViewData()
      ' Add any initialization after the InitializeComponent() call.
   End Sub
   Private Sub GetAdditionalSpecData()
      With _prop
         If .shape = "I" Then 'Me.gbIShapeSpecification.Visible Then
            Select Case True
               Case radISingle.Checked, radITee.Checked
                  .addSpec1 = 0.0
                  .addSpec2 = 0.0
               Case radIDouble.Checked
                  .addSpec1 = CDbl(Val(txtISpecSP.Text))
                  .addSpec2 = 0.0
               Case radIComposite.Checked
                  .addSpec1 = CDbl(Val(txtISpecCT.Text))
                  .addSpec2 = CDbl(Val(txtISpecFC.Text))
               Case radITopCover.Checked, radIBottomCover.Checked
                  .addSpec1 = CDbl(Val(txtISpecWP.Text))
                  .addSpec2 = CDbl(Val(txtISpecTH.Text))
               Case radITopAndBottomCover.Checked
                  .addSpec1 = CDbl(Val(txtISpecWP.Text))
                  .addSpec2 = CDbl(Val(txtISpecTH.Text))
            End Select
         ElseIf .shape = "C" Then 'Me.gbCShapeSpecification.Visible Then
            Select Case True
               Case radCSingle.Checked
                  .addSpec1 = 0.0
                  .addSpec2 = 0.0
               Case radCDoubleB2B.Checked, radCDoubleBADisabled.Checked, radCDoubleF2F.Checked
                  .addSpec1 = CDbl(Val(txtCSpecSP.Text))
                  .addSpec2 = 0.0
            End Select
         ElseIf .shape = "L" Then 'Me.gbLShapeSpecification.Visible Then
            Select Case True
               Case radLSingle.Checked, radLSingleRA.Checked, radLDoubleSA.Checked
                  .addSpec1 = 0.0
                  .addSpec2 = 0.0
               Case radLDoubleLD.Checked, radLDoubleSD.Checked
                  .addSpec1 = CDbl(Val(txtLSpecSP.Text))
                  .addSpec2 = 0.0
            End Select
         End If
      End With
   End Sub
   Private Sub IShapeSpecRadioChanged(sender As Object, e As EventArgs) Handles _
      radISingle.CheckedChanged,
      radITee.CheckedChanged,
      radIDouble.CheckedChanged,
      radIComposite.CheckedChanged,
      radITopCover.CheckedChanged,
      radIBottomCover.CheckedChanged,
      radITopAndBottomCover.CheckedChanged

      With _prop
         Select Case True
            Case radISingle.Checked, radITee.Checked
               txtISpecSP.Enabled = False
               txtISpecCT.Enabled = False
               txtISpecFC.Enabled = False
               txtISpecCW.Enabled = False
               txtISpecCD.Enabled = False
               txtISpecWP.Enabled = False
               txtISpecTH.Enabled = False
               txtISpecBW.Enabled = False
               txtISpecBT.Enabled = False
               If radISingle.Checked Then .type = TypeSpec.ST Else .type = TypeSpec.T
            Case radIDouble.Checked
               txtISpecSP.Enabled = True
               txtISpecCT.Enabled = False
               txtISpecFC.Enabled = False
               txtISpecCW.Enabled = False
               txtISpecCD.Enabled = False
               txtISpecWP.Enabled = False
               txtISpecTH.Enabled = False
               txtISpecBW.Enabled = False
               txtISpecBT.Enabled = False
               .type = TypeSpec.D
            Case radIComposite.Checked
               txtISpecSP.Enabled = False
               txtISpecCT.Enabled = True
               txtISpecFC.Enabled = True
               txtISpecCW.Enabled = True
               txtISpecCD.Enabled = True
               txtISpecWP.Enabled = False
               txtISpecTH.Enabled = False
               txtISpecBW.Enabled = False
               txtISpecBT.Enabled = False
               .type = TypeSpec.CM
            Case radITopCover.Checked, radIBottomCover.Checked
               txtISpecSP.Enabled = False
               txtISpecCT.Enabled = False
               txtISpecFC.Enabled = False
               txtISpecCW.Enabled = False
               txtISpecCD.Enabled = False
               txtISpecWP.Enabled = True
               txtISpecTH.Enabled = True
               txtISpecBW.Enabled = False
               txtISpecBT.Enabled = False
               If radITopCover.Checked Then .type = TypeSpec.TC Else .type = TypeSpec.BC
            Case radITopAndBottomCover.Checked
               txtISpecSP.Enabled = False
               txtISpecCT.Enabled = False
               txtISpecFC.Enabled = False
               txtISpecCW.Enabled = False
               txtISpecCD.Enabled = False
               txtISpecWP.Enabled = True
               txtISpecTH.Enabled = True
               txtISpecBW.Enabled = True
               txtISpecBT.Enabled = True
               .type = TypeSpec.TB
         End Select
      End With
      If Not radIDouble.Checked Then txtISpecSP.Clear()
      If Not radIComposite.Checked Then
         txtISpecCD.Clear()
         txtISpecCT.Clear()
         txtISpecCW.Clear()
         txtISpecFC.Clear()
      End If
      If (Not radITopCover.Checked) And (Not radIBottomCover.Checked) And (Not radITopAndBottomCover.Checked) Then
         txtISpecBW.Clear()
         txtISpecBT.Clear()
         txtISpecWP.Clear()
         txtISpecTH.Clear()
      End If
   End Sub
   Private Sub CShapeSpecRadioChanged(sender As Object, e As EventArgs) Handles _
   radCSingle.CheckedChanged,
   radCDoubleB2B.CheckedChanged,
   radCDoubleBADisabled.CheckedChanged,
   radCDoubleF2F.CheckedChanged
      With _prop
         Select Case True
            Case radCSingle.Checked
               txtCSpecSP.Enabled = False
               .type = TypeSpec.ST
            Case radCDoubleB2B.Checked, radCDoubleBADisabled.Checked, radCDoubleF2F.Checked
               txtCSpecSP.Enabled = True
               If radCDoubleB2B.Checked Then
                  .type = TypeSpec.D
               Else
                  .type = TypeSpec.FR
               End If
         End Select
      End With
      If (Not radCDoubleB2B.Checked) And (Not radCDoubleBADisabled.Checked) And (Not radCDoubleF2F.Checked) Then
         txtCSpecSP.Clear()
      End If
   End Sub
   Private Sub LShapeSpecRadioChanged(sender As Object, e As EventArgs) Handles _
      radLSingle.CheckedChanged,
      radLDoubleLD.CheckedChanged,
      radLDoubleSA.CheckedChanged,
      radLDoubleSD.CheckedChanged,
      radLSingleRA.CheckedChanged
      With _prop
         Select Case True
            Case radLSingle.Checked, radLSingleRA.Checked, radLDoubleSA.Checked
               txtLSpecSP.Enabled = False
               If radLSingle.Checked Then
                  .type = TypeSpec.ST
               ElseIf radLSingleRA.Checked Then
                  .type = TypeSpec.RA
               End If
            Case radLDoubleLD.Checked, radLDoubleSD.Checked
               txtLSpecSP.Enabled = True
               If radLDoubleLD.Checked Then .type = TypeSpec.LD Else .type = TypeSpec.SD
         End Select
      End With
      If (Not radLDoubleLD.Checked) And (Not radLDoubleSD.Checked) Then
         txtLSpecSP.Clear()
      End If
   End Sub
   Private Sub cmbMatDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMatDatabases.SelectedIndexChanged
      If cmbMatDatabases.SelectedIndex <> -1 Then
         '_prop.mData.dbNameIndex = Me.cmbMatDatabases.SelectedIndex
         '_prop.mData.nameIndex = Me.cmbMatGrades.SelectedIndex
         Dim MatGrades() As String
         Try
            If xMat Is Nothing Then xMat = New sesMaterials()
            MatGrades = xMat.GetGradesFromXML(cmbMatDatabases.SelectedItem.ToString)
            If UBound(MatGrades) >= 0 Then
               Me.cmbMatGrades.Items.Clear()
               For Each grade As String In MatGrades
                  cmbMatGrades.Items.Add(grade)
               Next
               cmbMatGrades.SelectedIndex = 0
            End If
         Catch ex As Exception
            MsgBox("[Material Databases]: " & ex.Message, MsgBoxStyle.OkOnly)
         End Try
      End If
   End Sub
   Private Sub UpdateTextBoxOnLeave(sender As Object, e As EventArgs) Handles _
      txtCSpecSP.Leave,
      txtISpecBT.Leave,
      txtISpecBW.Leave,
      txtISpecCT.Leave,
      txtISpecCW.Leave,
      txtISpecSP.Leave,
      txtISpecTH.Leave,
      txtISpecWP.Leave
      Dim tb As TextBox
      tb = CType(sender, TextBox)
      tb.Text = Format(Val(tb.Text), lenFormat) & " " & lenUnit
   End Sub
   Private Sub cmbMatGrades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMatGrades.SelectedIndexChanged
      If cmbMatGrades.SelectedIndex <> -1 Then
         '_prop.mData.dbNameIndex = Me.cmbMatDatabases.SelectedIndex
         '_prop.mData.nameIndex = Me.cmbMatGrades.SelectedIndex
         Try
            If xMat Is Nothing Then xMat = New sesMaterials()
            xMat.FillMaterialData(_prop.mData, cmbMatDatabases.Text, cmbMatGrades.Text)
         Catch ex As Exception
            MsgBox("[Material Grades]: " & ex.Message, MsgBoxStyle.OkOnly)
         End Try
      End If
   End Sub
End Class