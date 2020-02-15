'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Code and Engineer                          /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
'Option Strict On
Imports OpenSTAADUI
Imports System.Xml
Imports System.IO
Imports SES.DataTypes
Public Class frmMainInterface
   Private OS As OpenSTAAD
   Private FullFileName As String = ""
   Private SavedFileName As String = ""
   Private Const TG_EXT As String = ".xtg"
   'Private Const CHOOSE_SEC As String = ">>"
   Private EncDecPass As String
   Private frmTG As frmThickGen
   Private frmSecPic As frmSectionPicker
   Private xWriter As XmlWriter
   Private xDoc As XmlDocument
   Private xSec As sesSecurity
   Private ODecrypt As sesDecryptClass
   Private AESSec As sesAESSecurity
   Private IByte() As Byte, EncByte() As Byte, DecByte() As Byte
   Private Sub CreateThickenerGeneratorInstance()
      If Not IsThickenerGeneratorLocked Then
         IsThickenerGeneratorLocked = True
         Me.msiFileSave.Enabled = True
         Me.msiFileSaveAs.Enabled = True
         frmTG = New frmThickGen
         With frmTG
            .MdiParent = Me
            .Location = New Point(0, 0)
            .Show()
         End With
      End If
   End Sub
   Private Sub tsmiNew_Click(sender As Object, e As EventArgs) Handles msiFileNew.Click
      CreateThickenerGeneratorInstance()
   End Sub
   Private Sub tsmiExit_Click(sender As Object, e As EventArgs) Handles msiFileExit.Click
      Dim ExitYesNo As MsgBoxResult
      ExitYesNo = MsgBox("Do you really want to exit?", MsgBoxStyle.YesNo) ', "Exit STAAD Extension Suite")
      If ExitYesNo = MsgBoxResult.Yes Then
         Me.Close()
      End If
   End Sub
   Private Sub tsmiSave_Click(sender As Object, e As EventArgs) Handles msiFileSave.Click
      If IsThickenerGeneratorLocked Then
         GenerateInputDataFile()
      Else
         MsgBox("No instance of thickener generator is running.", MsgBoxStyle.OkOnly)
      End If
   End Sub
   Private Sub CreateNewElement(ByRef xRefWriter As XmlTextWriter, ByVal ElemName As String, ByVal ElemString As String)
      With xRefWriter
         .WriteStartElement(ElemName)
         .WriteString(ElemString)
         .WriteEndElement()
      End With
   End Sub
   Private Sub GenerateInputDataFile()
      Dim OrigStream As New MemoryStream, EncStream As New MemoryStream ', SReader As StreamReader,EncSReader As StreamReader
      Dim xWriter As New XmlTextWriter(OrigStream, System.Text.Encoding.UTF8)
      With xWriter
         .Formatting = Formatting.Indented
         .Indentation = 4
         .WriteStartDocument(True)
         .WriteStartElement("input-data")
         CreateNewElement(xWriter, "xOrigin", frmTG.txtxOrigin.Text)
         CreateNewElement(xWriter, "yOrigin", frmTG.txtyOrigin.Text)
         CreateNewElement(xWriter, "zOrigin", frmTG.txtzOrigin.Text)
         CreateNewElement(xWriter, "dTank", frmTG.txtdTank.Text)
         CreateNewElement(xWriter, "hWall", frmTG.txthWall.Text)
         CreateNewElement(xWriter, "FreeBoard", frmTG.txtFreeBoard.Text)
         CreateNewElement(xWriter, "bLaun", frmTG.txtbLaun.Text)
         CreateNewElement(xWriter, "hLaun", frmTG.txthLaun.Text)
         CreateNewElement(xWriter, "Alpha", frmTG.txtAlpha.Text)
         CreateNewElement(xWriter, "Beta", frmTG.txtBeta.Text)
         CreateNewElement(xWriter, "dCC", frmTG.txtdCC.Text)
         CreateNewElement(xWriter, "hCC", frmTG.txthCC.Text)
         CreateNewElement(xWriter, "hCCTop", frmTG.txthCCTop.Text)
         CreateNewElement(xWriter, "idBP", frmTG.txtidBP.Text)
         CreateNewElement(xWriter, "odBP", frmTG.txtodBP.Text)
         CreateNewElement(xWriter, "pcdBolts", frmTG.txtpcdBolts.Text)
         CreateNewElement(xWriter, "idCP", frmTG.txtidCP.Text)
         CreateNewElement(xWriter, "odCP", frmTG.txtodCP.Text)
         CreateNewElement(xWriter, "dUF", frmTG.txtdUF.Text)
         CreateNewElement(xWriter, "dCR", frmTG.txtdCR.Text)
         CreateNewElement(xWriter, "hCR", frmTG.txthCR.Text)
         CreateNewElement(xWriter, "hDroop", frmTG.txthDroop.Text)
         CreateNewElement(xWriter, "nBoltsCC", frmTG.txtnBoltsCC.Text)
         CreateNewElement(xWriter, "sDelta", frmTG.txtsDelta.Text)
         CreateNewElement(xWriter, "nRB", frmTG.txtnRB.Text)
         CreateNewElement(xWriter, "nColRB", frmTG.txtnColRB.Text)

         CreateNewElement(xWriter, "LCantilever", frmTG.txtLCantilever.Text)
         CreateNewElement(xWriter, "AspectRatio", frmTG.txtAspectRatio.Text)
         CreateNewElement(xWriter, "MaxPlateDim", frmTG.txtMaxPlateDim.Text)
         '/----------- Optional Components ---------------/
         CreateNewElement(xWriter, "CompRing", frmTG.chkCompRing.Checked.ToString)
         CreateNewElement(xWriter, "CRVAnnulus", frmTG.chkCRVAnnulus.Checked.ToString)
         CreateNewElement(xWriter, "CRStiffener", frmTG.chkCRStiffener.Checked.ToString)
         CreateNewElement(xWriter, "CCBottom", frmTG.chkCCBottom.Checked.ToString)
         CreateNewElement(xWriter, "CCBasePlate", frmTG.chkCCBasePlate.Checked.ToString)
         CreateNewElement(xWriter, "CCInterCapPlate", frmTG.chkCCInterCapPlate.Checked.ToString)
         CreateNewElement(xWriter, "CCTop", frmTG.chkCCTop.Checked.ToString)
         CreateNewElement(xWriter, "CCCapPlateTop", frmTG.chkCCCapPlateTop.Checked.ToString)
         CreateNewElement(xWriter, "CCStiffenerTop", frmTG.chkCCStiffenerTop.Checked.ToString)

         If (Not frmTG.btnRBSection Is Nothing) AndAlso (frmTG.btnRBSection.Text <> "") AndAlso _
           (frmTG.btnRBSection.Text <> CHOOSE_SEC) Then
            '/------ Start writing radial beam data ----------/
            .WriteStartElement("radial-beam-data")
            WritePropertyData(xWriter, frmTG.RadBeam)
            .WriteEndElement()
            '/------ End of writing radial beam data ----------/
         End If
         '/----------------- Data Grid View -------------/
         .WriteStartElement("grid-data")
         CreateNewElement(xWriter, "ColumnMethod", frmTG.cmbColumnMethod.Text)
         If frmTG.dgvSupportStructure.RowCount >= 1 Then
            For Each iRow As DataGridViewRow In frmTG.dgvSupportStructure.Rows
               .WriteStartElement("row-data")
               CreateNewElement(xWriter, "index", iRow.Index.ToString())
               CreateNewElement(xWriter, "SlNo", iRow.Cells(indSlNo).Value.ToString())
               CreateNewElement(xWriter, "Description", iRow.Cells(indDesc).Value.ToString())
               CreateNewElement(xWriter, "PCD", iRow.Cells(indPCD).Value.ToString())

               If (Not iRow.Cells(indColSec).Value Is Nothing) AndAlso (iRow.Cells(indColSec).Value.ToString <> "") AndAlso _
                  (iRow.Cells(indColSec).Value.ToString <> CHOOSE_SEC) Then
                  '/------ Start writing column data ----------/
                  .WriteStartElement("column-data")
                  WritePropertyData(xWriter, frmTG.dbCols(iRow.Index))
                  .WriteEndElement()
                  '/------ End of writing column data ----------/
               End If
               CreateNewElement(xWriter, "Elevation", iRow.Cells(indEL).Value.ToString())
               CreateNewElement(xWriter, "CrossBracing", iRow.Cells(indCB).Value.ToString())

               If CBool(iRow.Cells(indCB).Value) AndAlso (Not iRow.Cells(indCBSec).Value Is Nothing) AndAlso _
                  (iRow.Cells(indCBSec).Value.ToString <> "") AndAlso (iRow.Cells(indCBSec).Value.ToString <> CHOOSE_SEC) Then
                  '/------ Start writing cross bracing data ----------/
                  .WriteStartElement("cross-bracing-data")
                  WritePropertyData(xWriter, frmTG.dbCB(iRow.Index))
                  .WriteEndElement()
                  '/------ End of writing cross bracing data ----------/
               End If

               CreateNewElement(xWriter, "CrossBracingPattern", iRow.Cells(indCBPattern).Value.ToString())
               CreateNewElement(xWriter, "RadialBracing", iRow.Cells(indRB).Value.ToString())

               If CBool(iRow.Cells(indRB).Value) AndAlso (Not iRow.Cells(indRBSec).Value Is Nothing) AndAlso _
                 (iRow.Cells(indRBSec).Value.ToString <> "") AndAlso (iRow.Cells(indRBSec).Value.ToString <> CHOOSE_SEC) Then
                  '/------ Start writing radial bracing data ----------/
                  .WriteStartElement("radial-bracing-data")
                  WritePropertyData(xWriter, frmTG.dbRB(iRow.Index))
                  .WriteEndElement()
                  '/------ End of writing radial bracing data ----------/
               End If

               CreateNewElement(xWriter, "RadialBracingPattern", iRow.Cells(indRBPattern).Value.ToString())
               .WriteEndElement()

            Next
         End If
         .WriteEndElement()
         .WriteEndElement()
         .WriteEndDocument()
         .Flush()
      End With
      EncryptMemStream(OrigStream, EncStream)
      CreateFile(EncStream)
      'CreateFile(OrigStream)
      xWriter.Close()
   End Sub
   Private Sub WritePropertyData(ByRef xw As XmlTextWriter, ByRef pd As PropertyData)
      '/------ Start writing member property data ----------/
      With xw
         CreateNewElement(xw, "sectionName", pd.sData.STAADName) 'iRow.Cells(indRBSec).Value.ToString())
         CreateNewElement(xw, "dbName", pd.dbName)
         CreateNewElement(xw, "shape", pd.shape)
         CreateNewElement(xw, "type", CInt(pd.type).ToString)
         CreateNewElement(xw, "shapeTypeKey", pd.shapeTypeKey)
         CreateNewElement(xw, "addSpec1", pd.addSpec1.ToString)
         CreateNewElement(xw, "addSpec2", pd.addSpec2.ToString)
         CreateNewElement(xw, "tableName", pd.tableName)
         CreateNewElement(xw, "nameIndex", CStr(pd.nameIndex))
         '/------ Start writing member section data ----------/
         .WriteStartElement("sdata")
         With pd
            WriteSectionData(xw, .shape, .sData)
         End With
         .WriteEndElement()
         '/------ End writing member section data ----------/
         '/------ Start writing member material data ----------/
         .WriteStartElement("mdata")
         With pd
            WriteMaterialData(xw, .mData)
         End With
         .WriteEndElement()
         '/------ End writing member material data ----------/
      End With
      '/------ End writing member property data ----------/
   End Sub
   Private Sub WriteSectionData(ByRef xw As XmlTextWriter, ByVal shape As String, ByRef sd As SectionData)
      With sd
         CreateNewElement(xw, "Designation", .Designation)
         CreateNewElement(xw, "STAADName", .STAADName)
         CreateNewElement(xw, "Mass", CStr(.Mass))
         CreateNewElement(xw, "Massfps", CStr(.Massfps))
         If shape = "CHS" Then
            CreateNewElement(xw, "OD", CStr(.OD))
            CreateNewElement(xw, "ID", CStr(.ID))
            CreateNewElement(xw, "tw", CStr(.tw))
            CreateNewElement(xw, "A", CStr(.A))
         Else
            CreateNewElement(xw, "h", CStr(.h))
            CreateNewElement(xw, "bf", CStr(.bf))
            CreateNewElement(xw, "tw", CStr(.tw))
            CreateNewElement(xw, "tf", CStr(.tf))
            CreateNewElement(xw, "r1", CStr(.r1))
            CreateNewElement(xw, "r2", CStr(.r2))
            CreateNewElement(xw, "A", CStr(.A))
            CreateNewElement(xw, "hi", CStr(.hi))
            CreateNewElement(xw, "d", CStr(.d))
            CreateNewElement(xw, "alpha", CStr(.Alpha))
            CreateNewElement(xw, "k", CStr(.k))
            CreateNewElement(xw, "k1", CStr(.k1))
            CreateNewElement(xw, "ss", CStr(.ss))
         End If
         CreateNewElement(xw, "ALo", CStr(.ALo))
         CreateNewElement(xw, "ALi", CStr(.ALi))
         CreateNewElement(xw, "AGo", CStr(.AGo))
         CreateNewElement(xw, "AGi", CStr(.AGi))
         CreateNewElement(xw, "Iy", CStr(.Iy))
         CreateNewElement(xw, "Wely", CStr(.Wely))
         CreateNewElement(xw, "Wply", CStr(.Wply))
         CreateNewElement(xw, "ry", CStr(.ry))
         CreateNewElement(xw, "Avz", CStr(.Avz))
         CreateNewElement(xw, "Iz", CStr(.Iz))
         CreateNewElement(xw, "Welz", CStr(.Welz))
         CreateNewElement(xw, "Wplz", CStr(.Wplz))
         CreateNewElement(xw, "rz", CStr(.rz))
         CreateNewElement(xw, "Avy", CStr(.Avy))
         CreateNewElement(xw, "It", CStr(.It))
         CreateNewElement(xw, "Iw", CStr(.Iw))
         CreateNewElement(xw, "Cy", CStr(.Cy))
         CreateNewElement(xw, "ey", CStr(.ey))
         CreateNewElement(xw, "Cz", CStr(.Cz))
         CreateNewElement(xw, "ez", CStr(.ez))
         '/-------- Principal axes properties ----------/
         CreateNewElement(xw, "Iu", CStr(.Iu))
         CreateNewElement(xw, "Iv", CStr(.Iv))
         CreateNewElement(xw, "ru", CStr(.ru))
         CreateNewElement(xw, "rv", CStr(.rv))
      End With
   End Sub
   Private Sub WriteMaterialData(ByRef xw As XmlTextWriter, ByRef md As MaterialData)
      With md
         CreateNewElement(xw, "Grade", .Grade)
         CreateNewElement(xw, "STAADName", .STAADName)
         CreateNewElement(xw, "nameIndex", CStr(.nameIndex))
         CreateNewElement(xw, "dbName", .dbName)
         CreateNewElement(xw, "dbNameIndex", CStr(.dbNameIndex))
         CreateNewElement(xw, "Fy", CStr(.Fy))
         CreateNewElement(xw, "Fu", CStr(.Fu))
         CreateNewElement(xw, "E", CStr(.E))
         CreateNewElement(xw, "Poisson", CStr(.Poisson))
         CreateNewElement(xw, "Alpha", CStr(.Alpha))
         CreateNewElement(xw, "CrDamp", CStr(.CrDamp))
         CreateNewElement(xw, "Density", CStr(.Density))
      End With
   End Sub
   Private Sub ReadPropertyData(ByRef xNode As XmlNode, ByRef pd As PropertyData)
      With pd
         .name = xNode.SelectSingleNode("sectionName").InnerText
         .dbName = xNode.SelectSingleNode("dbName").InnerText
         .shape = xNode.SelectSingleNode("shape").InnerText
         .tableName = xNode.SelectSingleNode("tableName").InnerText
         .nameIndex = CInt(xNode.SelectSingleNode("nameIndex").InnerText)
         .type = CType(xNode.SelectSingleNode("type").InnerText, TypeSpec)
         .shapeTypeKey = xNode.SelectSingleNode("shapeTypeKey").InnerText
         .addSpec1 = CDbl(xNode.SelectSingleNode("addSpec1").InnerText)
         .addSpec2 = CDbl(xNode.SelectSingleNode("addSpec2").InnerText)
         ReadSectionData(xNode.SelectSingleNode("sdata"), .shape, .sData)
         ReadMaterialData(xNode.SelectSingleNode("mdata"), .mData)
      End With
   End Sub
   Private Sub ReadSectionData(ByRef sDataNode As XmlNode, ByVal shape As String, ByRef sd As SectionData)
      With sDataNode
         sd.Designation = .SelectSingleNode("Designation").InnerText
         sd.STAADName = .SelectSingleNode("STAADName").InnerText
         sd.Mass = CDbl(.SelectSingleNode("Mass").InnerText)
         sd.Massfps = CDbl(.SelectSingleNode("Massfps").InnerText)
         If shape = "CHS" Then
            sd.OD = CDbl(.SelectSingleNode("OD").InnerText)
            sd.ID = CDbl(.SelectSingleNode("ID").InnerText)
            sd.tw = CSng(.SelectSingleNode("tw").InnerText)
            sd.A = CDbl(.SelectSingleNode("A").InnerText)
         Else
            sd.h = CSng(.SelectSingleNode("h").InnerText)
            sd.bf = CSng(.SelectSingleNode("bf").InnerText)
            sd.tw = CSng(.SelectSingleNode("tw").InnerText)
            sd.tf = CSng(.SelectSingleNode("tf").InnerText)
            sd.r1 = CSng(.SelectSingleNode("r1").InnerText)
            sd.r2 = CSng(.SelectSingleNode("r2").InnerText)
            sd.A = CDbl(.SelectSingleNode("A").InnerText)
            sd.hi = CSng(.SelectSingleNode("hi").InnerText)
            sd.d = CSng(.SelectSingleNode("d").InnerText)
            sd.Alpha = CDbl(.SelectSingleNode("alpha").InnerText)
            sd.k = CSng(.SelectSingleNode("k").InnerText)
            sd.k1 = CSng(.SelectSingleNode("k1").InnerText)
            sd.ss = CSng(.SelectSingleNode("ss").InnerText)
         End If
         sd.ALo = CDbl(.SelectSingleNode("ALo").InnerText)
         sd.ALi = CDbl(.SelectSingleNode("ALi").InnerText)
         sd.AGo = CDbl(.SelectSingleNode("AGo").InnerText)
         sd.AGi = CDbl(.SelectSingleNode("AGi").InnerText)
         sd.Iy = CDbl(.SelectSingleNode("Iy").InnerText)
         sd.Wely = CDbl(.SelectSingleNode("Wely").InnerText)
         sd.Wply = CDbl(.SelectSingleNode("Wply").InnerText)
         sd.ry = CSng(.SelectSingleNode("ry").InnerText)
         sd.Avz = CDbl(.SelectSingleNode("Avz").InnerText)
         sd.Iz = CDbl(.SelectSingleNode("Iz").InnerText)
         sd.Welz = CDbl(.SelectSingleNode("Welz").InnerText)
         sd.Wplz = CDbl(.SelectSingleNode("Wplz").InnerText)
         sd.rz = CSng(CDbl(.SelectSingleNode("rz").InnerText))
         sd.Avy = CDbl(.SelectSingleNode("Avy").InnerText)
         sd.It = CDbl(.SelectSingleNode("It").InnerText)
         sd.Iw = CDbl(.SelectSingleNode("Iw").InnerText)
         sd.Cy = CDbl(.SelectSingleNode("Cy").InnerText)
         sd.ey = CDbl(.SelectSingleNode("ey").InnerText)
         sd.Cz = CDbl(.SelectSingleNode("Cz").InnerText)
         sd.ez = CDbl(.SelectSingleNode("ez").InnerText)
         '/-------- Principal axes properties ----------/
         sd.Iu = CDbl(.SelectSingleNode("Iu").InnerText)
         sd.Iv = CDbl(.SelectSingleNode("Iv").InnerText)
         sd.ru = CDbl(.SelectSingleNode("ru").InnerText)
         sd.rv = CDbl(.SelectSingleNode("rv").InnerText)
      End With
   End Sub
   Private Sub ReadMaterialData(ByRef mDataNode As XmlNode, ByRef md As MaterialData)
      With mDataNode
         md.Grade = .SelectSingleNode("Grade").InnerText
         md.STAADName = .SelectSingleNode("STAADName").InnerText
         md.dbName = .SelectSingleNode("dbName").InnerText
         md.dbNameIndex = CInt(.SelectSingleNode("dbNameIndex").InnerText)
         md.nameIndex = CInt(.SelectSingleNode("nameIndex").InnerText)
         md.Fy = CDbl(.SelectSingleNode("Fy").InnerText)
         md.Fu = CDbl(.SelectSingleNode("Fu").InnerText)
         md.Alpha = CDbl(.SelectSingleNode("Alpha").InnerText)
         md.CrDamp = CDbl(.SelectSingleNode("CrDamp").InnerText)
         md.Density = CDbl(.SelectSingleNode("Density").InnerText)
         md.E = CDbl(.SelectSingleNode("E").InnerText)
         md.Poisson = CDbl(.SelectSingleNode("Poisson").InnerText)
      End With
   End Sub
   Private Sub EncryptMemStream(ByVal SrcMStream As MemoryStream, ByRef OPMStream As MemoryStream)
      xSec = New sesSecurity
      EncDecPass = xSec.Password
      AESSec = New sesAESSecurity(EncDecPass)
      EncByte = AESSec.AESEncECB(SrcMStream.ToArray)
      OPMStream = New MemoryStream(EncByte)
      OPMStream.Seek(0, SeekOrigin.Begin)
      xSec = Nothing
   End Sub
   Private Sub DecryptMemStream(ByVal SrcMStream As MemoryStream, ByRef OPMStream As MemoryStream)
      xSec = New sesSecurity
      EncDecPass = xSec.Password
      AESSec = New sesAESSecurity(EncDecPass)
      DecByte = AESSec.AESDecECB(SrcMStream.ToArray)
      OPMStream = New MemoryStream(DecByte)
      OPMStream.Seek(0, SeekOrigin.Begin)
      xSec = Nothing
   End Sub
   Private Sub CreateFile(MS As MemoryStream)
      Dim SaveDialog As New SaveFileDialog, tgFile As FileStream
      If SavedFileName = "" Then
         With SaveDialog
            .DefaultExt = ".xtg"
            .FileName = "Thickener Generator"
            .Filter = "Thickener Generator Files (*.xtg)|*.xtg"
            .OverwritePrompt = True
         End With
         If SaveDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            FullFileName = SaveDialog.FileName
            SavedFileName = FullFileName
            tgFile = New FileStream(FullFileName, FileMode.Create, FileAccess.Write)
            MS.WriteTo(tgFile)
            tgFile.Close()
         End If
      Else
         tgFile = New FileStream(FullFileName, FileMode.Create, FileAccess.Write)
         MS.WriteTo(tgFile)
         tgFile.Close()
      End If
   End Sub

   Private Sub OpenSpecifiedFile(ByVal tgFileName As String)
      Dim EncInputFile As New MemoryStream, DecOutputFile As New MemoryStream
      xDoc = New XmlDocument
      Try
         '/----- Load an existing/saved file to the XmlDocument
         Dim br As BinaryReader = New BinaryReader(File.OpenRead(tgFileName))
         EncByte = br.ReadBytes(CInt(br.BaseStream.Length))
         EncInputFile = New MemoryStream(EncByte, 0, EncByte.Length)
         DecryptMemStream(EncInputFile, DecOutputFile)
         'xDoc.Load(FullFileName)
         xDoc.Load(DecOutputFile)
         '/----- If an instance of Thickener Generator is not created already 
         '/----- then create an instance
         If Not IsThickenerGeneratorLocked Then CreateThickenerGeneratorInstance()
         '/----- Fill the data from XML document to the user interface form
         With xDoc
            frmTG.txtxOrigin.Text = .SelectSingleNode("//xOrigin").InnerText
            frmTG.txtyOrigin.Text = .SelectSingleNode("//yOrigin").InnerText
            frmTG.txtzOrigin.Text = .SelectSingleNode("//zOrigin").InnerText
            frmTG.txtdTank.Text = .SelectSingleNode("//dTank").InnerText
            frmTG.txthWall.Text = .SelectSingleNode("//hWall").InnerText
            frmTG.txtFreeBoard.Text = .SelectSingleNode("//FreeBoard").InnerText
            frmTG.txtbLaun.Text = .SelectSingleNode("//bLaun").InnerText
            frmTG.txthLaun.Text = .SelectSingleNode("//hLaun").InnerText
            frmTG.txtAlpha.Text = .SelectSingleNode("//Alpha").InnerText
            frmTG.txtBeta.Text = .SelectSingleNode("//Beta").InnerText
            frmTG.txtdCC.Text = .SelectSingleNode("//dCC").InnerText
            frmTG.txthCC.Text = .SelectSingleNode("//hCC").InnerText
            frmTG.txthCCTop.Text = .SelectSingleNode("//hCCTop").InnerText
            frmTG.txtidBP.Text = .SelectSingleNode("//idBP").InnerText
            frmTG.txtodBP.Text = .SelectSingleNode("//odBP").InnerText
            frmTG.txtpcdBolts.Text = .SelectSingleNode("//pcdBolts").InnerText
            frmTG.txtidCP.Text = .SelectSingleNode("//idCP").InnerText
            frmTG.txtodCP.Text = .SelectSingleNode("//odCP").InnerText
            frmTG.txtdUF.Text = .SelectSingleNode("//dUF").InnerText
            frmTG.txtdCR.Text = .SelectSingleNode("//dCR").InnerText
            frmTG.txthCR.Text = .SelectSingleNode("//hCR").InnerText
            frmTG.txthDroop.Text = .SelectSingleNode("//hDroop").InnerText
            frmTG.txtnBoltsCC.Text = .SelectSingleNode("//nBoltsCC").InnerText
            frmTG.txtsDelta.Text = .SelectSingleNode("//sDelta").InnerText
            frmTG.txtnRB.Text = .SelectSingleNode("//nRB").InnerText
            frmTG.txtnColRB.Text = .SelectSingleNode("//nColRB").InnerText

            frmTG.txtLCantilever.Text = .SelectSingleNode("//LCantilever").InnerText
            frmTG.txtAspectRatio.Text = .SelectSingleNode("//AspectRatio").InnerText
            frmTG.txtMaxPlateDim.Text = .SelectSingleNode("//MaxPlateDim").InnerText
            '/------------------------ Optional Components ------------------------/
            frmTG.chkCompRing.Checked = CBool(.SelectSingleNode("//CompRing").InnerText)
            frmTG.chkCRVAnnulus.Checked = CBool(.SelectSingleNode("//CRVAnnulus").InnerText)
            frmTG.chkCRStiffener.Checked = CBool(.SelectSingleNode("//CRStiffener").InnerText)
            frmTG.chkCCBottom.Checked = CBool(.SelectSingleNode("//CCBottom").InnerText)
            frmTG.chkCCBasePlate.Checked = CBool(.SelectSingleNode("//CCBasePlate").InnerText)
            frmTG.chkCCInterCapPlate.Checked = CBool(.SelectSingleNode("//CCInterCapPlate").InnerText)
            frmTG.chkCCTop.Checked = CBool(.SelectSingleNode("//CCTop").InnerText)
            frmTG.chkCCCapPlateTop.Checked = CBool(.SelectSingleNode("//CCCapPlateTop").InnerText)
            frmTG.chkCCStiffenerTop.Checked = CBool(.SelectSingleNode("//CCStiffenerTop").InnerText)

            If (Not .SelectSingleNode("//radial-beam-data") Is Nothing) Then
               ReadPropertyData(xDoc.SelectSingleNode("//radial-beam-data"), frmTG.RadBeam)
               frmTG.btnRBSection.Text = frmTG.RadBeam.name
            Else
               frmTG.btnRBSection.Text = CHOOSE_SEC
            End If

            Dim rowNum As Integer
            rowNum = 0
            If frmTG.dgvSupportStructure.RowCount = CInt(Val(frmTG.txtnColRB.Text)) Then
               For Each row As XmlElement In .SelectNodes("//grid-data/row-data")
                  With frmTG.dgvSupportStructure
                     .Rows(rowNum).Cells(indDesc).Value = row.SelectSingleNode("Description").InnerText
                     .Rows(rowNum).Cells(indPCD).Value = row.SelectSingleNode("PCD").InnerText
                     If (Not row.SelectSingleNode("column-data") Is Nothing) Then
                        ReadPropertyData(row.SelectSingleNode("column-data"), frmTG.dbCols(rowNum))
                        .Rows(rowNum).Cells(indColSec).Value = frmTG.dbCols(rowNum).name
                     Else
                        .Rows(rowNum).Cells(indColSec).Value = CHOOSE_SEC
                     End If
                     .Rows(rowNum).Cells(indEL).Value = row.SelectSingleNode("Elevation").InnerText
                     .Rows(rowNum).Cells(indCB).Value = CBool(row.SelectSingleNode("CrossBracing").InnerText)

                     If (Not row.SelectSingleNode("cross-bracing-data") Is Nothing) Then
                        ReadPropertyData(row.SelectSingleNode("cross-bracing-data"), frmTG.dbCB(rowNum))
                        .Rows(rowNum).Cells(indCBSec).Value = frmTG.dbCB(rowNum).name
                     Else
                        If CBool(.Rows(rowNum).Cells(indCB).Value) Then
                           .Rows(rowNum).Cells(indCBSec).Value = CHOOSE_SEC
                        Else
                           .Rows(rowNum).Cells(indCBSec).Value = ""
                        End If
                     End If
                     .Rows(rowNum).Cells(indCBPattern).Value = row.SelectSingleNode("CrossBracingPattern").InnerText
                     .Rows(rowNum).Cells(indRB).Value = CBool(row.SelectSingleNode("RadialBracing").InnerText)

                     If (Not row.SelectSingleNode("radial-bracing-data") Is Nothing) Then
                        ReadPropertyData(row.SelectSingleNode("radial-bracing-data"), frmTG.dbRB(rowNum))
                        .Rows(rowNum).Cells(indRBSec).Value = frmTG.dbRB(rowNum).name
                     Else
                        If CBool(.Rows(rowNum).Cells(indRB).Value) Then
                           .Rows(rowNum).Cells(indRBSec).Value = CHOOSE_SEC
                        Else
                           .Rows(rowNum).Cells(indRBSec).Value = ""
                        End If
                     End If

                     .Rows(rowNum).Cells(indRBPattern).Value = row.SelectSingleNode("RadialBracingPattern").InnerText
                  End With
                  rowNum = (rowNum + 1)
               Next
            End If
         End With
         '/----- Set the SaveFileName to the opened file name in order the data to be
         '/----- to the file which is currently opened when 'Save' button is clicked again
         frmTG.Text = TG_TITLE & " (" & GetDoubleClickedFileName(tgFileName) & ")"
         SavedFileName = tgFileName
      Catch ex As Exception
         MsgBox(ex.Message)
      Finally
         EncInputFile = Nothing : DecOutputFile = Nothing
      End Try
   End Sub
   Private Sub tsmiOpen_Click(sender As Object, e As EventArgs) Handles msiFileOpen.Click
      Dim OpenDialog As New OpenFileDialog
      Dim EncInputFile As New MemoryStream, DecOutputFile As New MemoryStream
      With OpenDialog
         .DefaultExt = ".xtg"
         .FileName = ""
         .Filter = "Thickener Generator Files (*.xtg)|*.xtg"
      End With
      If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
         FullFileName = OpenDialog.FileName
         OpenSpecifiedFile(FullFileName)
      Else
         MsgBox("Operation cancelled by user.", MsgBoxStyle.OkOnly)
         Exit Sub
      End If

   End Sub
   Private Sub msiToolsSecPicker_Click(sender As Object, e As EventArgs) Handles msiToolsSecPicker.Click
      If Not IsSecPickerLocked Then
         frmSecPic = New frmSectionPicker(DataTypes.SecPickCallCategory.Isolated)
         With frmSecPic
            .MdiParent = Me
            '.Location = New Point(0, 0)
            .Show()
         End With
      End If
   End Sub
   Private Sub msiFileSaveAs_Click(sender As Object, e As EventArgs) Handles msiFileSaveAs.Click
      If IsThickenerGeneratorLocked Then
         SavedFileName = ""
         GenerateInputDataFile()
      Else
         MsgBox("No instance of thickener generator is running.", MsgBoxStyle.OkOnly)
      End If
   End Sub
   Private Sub frmMainInterface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      OpenFilesByDoubleClicking()
   End Sub

'/---------- New Code for Opening Files by Double Clicking -------------'/
   'This is a function to get the filename used
    Private Function GetDoubleClickedFileName(ByVal StrFullPath As String) As String
        Dim intPos As Integer

        intPos = InStrRev(StrFullPath, "\") 'Get last Index of "\"
        GetDoubleClickedFileName = Mid$(StrFullPath, intPos + 1) 'Get the full name
    End Function
   Private Sub OpenFilesByDoubleClicking()
      Dim strFileExt As String 'Get File Extension

      Dim CommandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs 'Get Command Line Arguments ( &open )

      For i As Integer = 0 To CommandLineArgs.Count - 1 'Loop through all arguments

         strFileExt = System.IO.Path.GetExtension(CommandLineArgs(i)) 'Get the filename extension

         Select Case strFileExt
            Case TG_EXT 'This is to open a file with associated extension *.xtg

               '/-------- Here call the open thickener file function to load the file in to the application --------'/
               OpenSpecifiedFile(CommandLineArgs(i))

         End Select
      Next
   End Sub

'/-----------------------------------------------------------------------'/
End Class

