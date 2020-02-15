Imports System.Xml
Imports System.IO
Public Class frmLicInputGenerator
   Private Const EncDecPass As String = "EngineeredProgramming"
   Private Const ShortTrial As Long = 7
   Private Const StandardEvaluationPeriod As Long = 30
   Private Const MinimumEvaluationPeriod As Long = 1
   'Private Const StandardAnnualPeriod As Long = 365
   Private LC As sesLicenseClass, IByte() As Byte, EncByte() As Byte, DecByte() As Byte, AESSec As sesAESSecurity
   Private Sub btnGetThisPCProps_Click(sender As Object, e As EventArgs) Handles btnGetThisPCProps.Click
      LC = New sesLicenseClass
      txtMachineName.Text = Trim(LC.GetPCName)
      txtUsername.Text = Trim(LC.GetUsername(True))
   End Sub
   Private Sub CheckDataValidity()
      If txtMachineName.Text = "" Then
         MsgBox("Machine Name field cannot be empty. Provide a valid name (or) press Get This PC Properties button.", MsgBoxStyle.OkOnly, "Insufficient information")
         txtMachineName.Focus()
      ElseIf txtUsername.Text = "" Then
         MsgBox("Username field cannot be empty. Provide a valid name (or) press Get This PC Properties button.", MsgBoxStyle.OkOnly, "Insufficient information")
         txtUsername.Focus()
      ElseIf dtpLicenseStart.Value < Now.Date Then
         MsgBox("License Start Date field is invalid. Select a date greater than or equal to today's date.", MsgBoxStyle.OkOnly, "Insufficient information")
         dtpLicenseStart.Focus()
      ElseIf cmbLicType.Text = "Evaluation" Then
         If txtNumDays.Text = "" Then
            MsgBox("Number of Days cannot be empty. Please provide a valid value", MsgBoxStyle.OkOnly, "Insufficient information")
            txtNumDays.Focus()
         ElseIf CLng(txtNumDays.Text) < MinimumEvaluationPeriod Then
            MsgBox("Number of Days of shall not be less than " & MinimumEvaluationPeriod & ". Please provide a valid value", MsgBoxStyle.OkOnly, "Insufficient information")
            txtNumDays.Focus()
         Else
            GenerateLicFile()
         End If
      ElseIf txtLicUsername.Text = "" Then
         MsgBox("Licensed Username field cannot be empty. Provide a valid name.", MsgBoxStyle.OkOnly, "Insufficient information")
         txtLicUsername.Focus()
      ElseIf txtLicCompany.Text = "" Then
         MsgBox("Licensed Company field cannot be empty. Provide a valid name.", MsgBoxStyle.OkOnly, "Insufficient information")
         txtLicCompany.Focus()
      Else
         GenerateLicFile()
      End If
   End Sub
   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      Me.Close()
   End Sub

   Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
      CheckDataValidity()
   End Sub

   Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
      txtMachineName.Clear()
      txtUsername.Clear()
      '      txtNumDays.Clear()
      txtLicUsername.Clear()
      txtLicCompany.Clear()
   End Sub

   Private Sub GenerateLicFile()
      Dim OrigStream As New MemoryStream, EncStream As New MemoryStream, LicEndDate As Date ', SReader As StreamReader, EncSReader As StreamReader
      Dim xmlWriter As New XmlTextWriter(OrigStream, System.Text.Encoding.UTF8)
      LicEndDate = DateAdd(DateInterval.Day, CDbl(txtNumDays.Text) - 1, dtpLicenseStart.Value)
      With xmlWriter
         .Formatting = Formatting.Indented
         .Indentation = 4
         .WriteStartDocument(True)
         .WriteStartElement("license-info")
         WriteLicInfo(xmlWriter, "pc-name", Trim(txtMachineName.Text))
         WriteLicInfo(xmlWriter, "username", Trim(txtUsername.Text))
         WriteLicInfo(xmlWriter, "start-date", dtpLicenseStart.Value.ToShortDateString)
         WriteLicInfo(xmlWriter, "end-date", LicEndDate.ToShortDateString)
         WriteLicInfo(xmlWriter, "lic-days", CLng(txtNumDays.Text))
         WriteLicInfo(xmlWriter, "lic-user", Trim(txtLicUsername.Text))
         WriteLicInfo(xmlWriter, "lic-type", Trim(cmbLicType.Text))
         WriteLicInfo(xmlWriter, "lic-company", Trim(txtLicCompany.Text))
         WriteLicInfo(xmlWriter, "agreement", CStr(Trim(Me.chkAgreement.Checked)))
         WriteLicInfo(xmlWriter, "publisher", "VSquare Consulting Engineers")
         .WriteEndElement()
         .WriteEndDocument()
         .Flush()
      End With
      EncryptMemStream(OrigStream, EncStream)
      'CreateFile(OrigStream)
      CreateFile(EncStream)
      'SReader = New StreamReader(OrigStream)
      'OrigStream.Seek(0, SeekOrigin.Begin)
      'txtLicOriginal.Text = SReader.ReadToEnd
      'txtLicOriginal.Select(0, 0)
      'EncSReader = New StreamReader(EncStream)
      'EncStream.Seek(0, SeekOrigin.Begin)
      'txtLicEncrypted.Text = EncSReader.ReadToEnd
      'txtLicEncrypted.Select(0, 0)
      xmlWriter.Close()
   End Sub
   Private Sub CreateFile(MS As MemoryStream)
      Dim SaveDialog As New SaveFileDialog, LicFile As FileStream, FName As String
      With SaveDialog
         .DefaultExt = "lif"
         .FileName = "sesLicenseInput"
         .Filter = "License Input Files (*.lif)|*.lif"
         .OverwritePrompt = True
      End With
      If SaveDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
         FName = SaveDialog.FileName
         LicFile = New FileStream(FName, FileMode.Create, FileAccess.Write)
         MS.WriteTo(LicFile)
         LicFile.Close()
         Me.btnCancel.Text = "&Close"
      End If
   End Sub
   Private Sub WriteLicInfo(ByVal xWriter As XmlTextWriter, ByVal TagName As String, ByVal TagText As String)
      xWriter.WriteStartElement(TagName)
      xWriter.WriteString(TagText)
      xWriter.WriteEndElement()
   End Sub

   Private Sub EncryptMemStream(ByVal SRCMStream As MemoryStream, ByRef OPMStream As MemoryStream)
      AESSec = New sesAESSecurity(EncDecPass)
      EncByte = AESSec.AESEncECB(SRCMStream.ToArray)
      OPMStream = New MemoryStream(EncByte)
   End Sub

   Private Sub frmLicGenerator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      'Me.Text = "License Input File Generator - " & PUBLISHER_NAME
      With Me.cmbLicType
         .Items.Clear()
         .Items.Add("Short Trial")
         .Items.Add("Evaluation")
         .SelectedIndex = 0
      End With
   End Sub

   Private Sub cmbLicType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLicType.SelectedIndexChanged
      Select Case Me.cmbLicType.Text
         Case "Short Trial"
            Me.txtNumDays.Text = CStr(ShortTrial)
         Case "Evaluation"
            Me.txtNumDays.Text = CStr(StandardEvaluationPeriod)
      End Select
   End Sub

   Private Sub txtMachineName_Enter(sender As Object, e As EventArgs) Handles txtMachineName.Enter
      txtMachineName.SelectAll()
   End Sub
   Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
      txtUsername.SelectAll()
   End Sub
   Private Sub txtNumDays_Enter(sender As Object, e As EventArgs) Handles txtNumDays.Enter
      txtNumDays.SelectAll()
   End Sub
   Private Sub txtLicUsername_Enter(sender As Object, e As EventArgs) Handles txtLicUsername.Enter
      txtLicUsername.SelectAll()
   End Sub
   Private Sub txtLicCompany_Enter(sender As Object, e As EventArgs) Handles txtLicCompany.Enter
      txtLicCompany.SelectAll()
   End Sub
   Private Sub chkAgreement_CheckedChanged(sender As Object, e As EventArgs) Handles chkAgreement.CheckedChanged
      If Me.chkAgreement.Checked Then
         Me.btnGenerate.Enabled = True
      Else
         Me.btnGenerate.Enabled = False
      End If
   End Sub
End Class