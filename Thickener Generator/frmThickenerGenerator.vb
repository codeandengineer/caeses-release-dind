Option Strict On
Imports SES.DataTypes
Public Class frmThickGen
   Private FullFileName As String
   Private AbortProcess As Boolean
   Private DataValidity As Boolean
   Private HasCompRing As Boolean, HasCRVertAnnulus As Boolean, HasCRStiffener As Boolean, HasCCBottom As Boolean, HasCCBottomCapPlate As Boolean, HasCCBottomBasePlate As Boolean
   Private HasCCTop As Boolean, HasCCTopCapPlate As Boolean, HasCCStiffener As Boolean, HasCapPlate As Boolean
   Private Const MsgAbortProcess As String = vbCrLf & "Process will now be aborted."
   Private Const Precision As Double = 0.01
   Private Const minLCant As Double = 0.5
   Private Const maxLCant As Double = 1.2
   Private Const TotalAngle As Long = 360
   Private Const LowerMaxPlateDim As Double = 0.05
   Private Const UpperMaxPlateDim As Double = 0.5
   Private Const MinAspectRatio As Integer = 1
   Private Const MaxAspectRatio As Integer = 4
   Private Const PI As Double = Math.PI
   Private GD As GeometryData
   Private BP As BasicProfile
   Private TG As ThickenerGenerator
   Private MyMdiParent As frmMainInterface
   Private secPicker As frmSectionPicker
   Private _nTableID As Integer
   Protected Friend dbCols() As PropertyData, dbCB() As PropertyData, dbRB() As PropertyData, RadBeam As PropertyData
   Private Sub frmThickGen_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
      MyMdiParent.msiFileSave.Enabled = False
      IsThickenerGeneratorLocked = False
   End Sub
   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      Me.Close()
   End Sub
   Private Sub frmThickGen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
      Dim ExitYesNo As MsgBoxResult
      ExitYesNo = MsgBox("Do you really want to exit?", MsgBoxStyle.YesNo, "Thickener Generator")
      If ExitYesNo = MsgBoxResult.Yes Then
         MyMdiParent.msiFileSave.Enabled = False
         MyMdiParent.msiFileSaveAs.Enabled = False
         IsThickenerGeneratorLocked = False
      Else
         e.Cancel = True
      End If
   End Sub
   Private Sub frmThickGen_Load(sender As Object, e As EventArgs) Handles Me.Load
      Me.Text = TG_TITLE
      Me.txtxOrigin.Text = "0.000"
      Me.txtyOrigin.Text = "0.000"
      Me.txtzOrigin.Text = "0.000"
      SetDefaultOptionalCompsAndSupportConditions()
      SetTooltips()
      Me.dgvSupportStructure.AllowUserToAddRows = False
      Me.dgvSupportStructure.AllowUserToDeleteRows = False
      MyMdiParent = CType(Me.MdiParent, frmMainInterface)
   End Sub
   Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
      If Not IsDataComplete() Then
         Exit Sub
      End If
      If Not IsDataValid() Then
         Exit Sub
      End If
      If Not IsGridDataValid() Then
         Exit Sub
      End If
      GetOptionalElementsValues()
      GetOpenSTAADObjects()
      If Not OS Is Nothing Then
         Me.MdiParent.WindowState = FormWindowState.Minimized
         GenerateThickenerGeometry()
         TerminateOSObjects()
         Me.MdiParent.WindowState = FormWindowState.Maximized
      End If
   End Sub
   '/----------- Read-only text boxes -----------------------------------/
   '/ txtOmega, txthUF, txtbCR, txtrDroop, txtHalfChordLength, txtTheta
   Private Sub TextBox_Enter(sender As Object, e As EventArgs) Handles _
      txtxOrigin.Enter, txtyOrigin.Enter, txtzOrigin.Enter, txtAlpha.Enter, txtAspectRatio.Enter, txtbCR.Enter, _
      txtBeta.Enter, txtbLaun.Enter, txtdCC.Enter, txtdCR.Enter, txtdTank.Enter, txtdUF.Enter, _
      txtFreeBoard.Enter, txtHalfChordLength.Enter, txthCC.Enter, txthCCTop.Enter, txthCR.Enter, txthDroop.Enter, _
      txthLaun.Enter, txthUF.Enter, txthWall.Enter, txtidBP.Enter, txtidCP.Enter, txtMaxPlateDim.Enter, txtnBoltsCC.Enter, _
      txtnColRB.Enter, txtnRB.Enter, txtodBP.Enter, txtodCP.Enter, txtpcdBolts.Enter, txtrDroop.Enter, _
      txtsDelta.Enter, txtTheta.Enter, txtLCantilever.Enter
      CType(sender, TextBox).SelectAll()
   End Sub
   Private Sub LengthTextBox_Leave(sender As Object, e As EventArgs) Handles txthWall.Leave, txtFreeBoard.Leave, _
      txtxOrigin.Leave, txtyOrigin.Leave, txtzOrigin.Leave, txtbLaun.Leave, txthLaun.Leave, txtbCR.Leave, txtdCC.Leave, _
      txtdCR.Leave, txtdUF.Leave, txtHalfChordLength.Leave, txthCC.Leave, txthCCTop.Leave, txthCR.Leave, txthDroop.Leave, _
      txthUF.Leave, txtidBP.Leave, txtidCP.Leave, txtMaxPlateDim.Leave, txtodBP.Leave, txtodCP.Leave, txtrDroop.Leave, _
      txtLCantilever.Leave, txtpcdBolts.Leave, txtdTank.Leave
      Dim tbControl As TextBox
      tbControl = CType(sender, TextBox)
      If tbControl.Text <> "" Then
         tbControl.Text = Format(CDbl(Val(tbControl.Text)), "0.000") & " m"
      End If
   End Sub
   Private Sub AngleTextBox_Leave(sender As Object, e As EventArgs) Handles txtAlpha.Leave, txtBeta.Leave, _
      txtsDelta.Leave, txtTheta.Leave
      Dim tbControl As TextBox
      tbControl = CType(sender, TextBox)
      If tbControl.Text <> "" Then
         tbControl.Text = Format(CDbl(Val(tbControl.Text)), "0.000") & "°"
      End If
   End Sub
   Private Sub IntegralTextBox_Leave(sender As Object, e As EventArgs) Handles txtnBoltsCC.Leave, txtnColRB.Leave, txtnRB.Leave
      Dim tbControl As TextBox
      tbControl = CType(sender, TextBox)
      If tbControl.Text <> "" Then
         tbControl.Text = Format(CLng(Val(tbControl.Text)), "0") & " Nos"
      End If
   End Sub
   Private Sub txtnRB_TextChanged(sender As Object, e As EventArgs) Handles txtnRB.TextChanged
      If CLng(Val(Me.txtnRB.Text)) > 0 Then
         Me.txtOmega.Text = Format(TotalAngle / CLng(Val(Me.txtnRB.Text)), "0.000") & "°"
      Else
         Me.txtOmega.Clear()
      End If
   End Sub
   Private Function CheckIncompleteDataStatus(ByRef Sender As TextBox, ByVal tpPage As String) As Boolean
      Dim Result As Boolean = True
      If (Sender.Text = "") Then
         If (tpPage <> "") Then
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages(tpPage)
         End If
         MsgBox("Please complete all the required input fields.", MsgBoxStyle.OkOnly, "Data Completeness Verification")
         Sender.Focus()
         Result = False
      End If
      Return Result
   End Function
   Private Function IsDataComplete() As Boolean
      Dim DataCompleteness As Boolean = True
      '/------------------ tpTank Tab Page ------------------/
      If Not CheckIncompleteDataStatus(txtdTank, "tpTank") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txthWall, "tpTank") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtFreeBoard, "tpTank") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtbLaun, "tpTank") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txthLaun, "tpTank") Then
         DataCompleteness = False
         '/-----------------------------------------------------/
         '/--------------- tpTankFloor Tab Page ----------------/
      ElseIf Not CheckIncompleteDataStatus(txtAlpha, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtBeta, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtdCC, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txthCC, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txthCCTop, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtidBP, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtodBP, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtpcdBolts, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtidCP, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtodCP, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtdUF, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtdCR, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txthCR, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txthDroop, "tpTankFloor") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtnBoltsCC, "tpTankFloor") Then
         DataCompleteness = False
         '/-----------------------------------------------------/
         '/-------------- tpSSGeometry Tab Page ----------------/
      ElseIf Not CheckIncompleteDataStatus(txtsDelta, "tpSSGeometry") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtnRB, "tpSSGeometry") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtnColRB, "tpSSGeometry") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtLCantilever, "tpSSGeometry") Then
         DataCompleteness = False
         '/-----------------------------------------------------/
         '/-------------- tpModelling Tab Page ----------------/
      ElseIf Not CheckIncompleteDataStatus(txtAspectRatio, "tpModelling") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtMaxPlateDim, "tpModelling") Then
         DataCompleteness = False
         '/-----------------------------------------------------/

         'CheckIncompleteDataStatus(txtbCR, "tpModelling")
         'CheckIncompleteDataStatus(txtHalfChordLength, "tpModelling")
         'CheckIncompleteDataStatus(txthUF, "tpModelling")
         'CheckIncompleteDataStatus(txtOmega, "tpModelling")
         'CheckIncompleteDataStatus(txtrDroop, "tpModelling")
         'CheckIncompleteDataStatus(txtTheta, "tpModelling")
         '/-----------------------------------------------------/
      ElseIf Not CheckIncompleteDataStatus(txtxOrigin, "") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtyOrigin, "") Then
         DataCompleteness = False
      ElseIf Not CheckIncompleteDataStatus(txtzOrigin, "") Then
         DataCompleteness = False
         '/-----------------------------------------------------/
      End If
      Return DataCompleteness
   End Function
   Private Function IsDataValid() As Boolean
      DataValidity = True
      With Me
         If CLng(Val(.txtnRB.Text)) Mod 2 <> 0 Or CLng(Val(.txtnRB.Text)) <= 0 Then
            MsgBox("Please provide an even number greater than zero for Number of Radial Beams." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpSSGeometry")
            .txtnRB.Focus()
            DataValidity = False
         ElseIf CLng(Val(.txtnColRB.Text)) <= 0 Then
            MsgBox("Number of columns per radial beam shall be greater than or equal to one." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpSSGeometry")
            .txtnColRB.Focus()
            DataValidity = False
         ElseIf (HasCCBottomCapPlate Or HasCCTopCapPlate) And CDbl(Val(.txtodCP.Text)) <= CDbl(Val(.txtdCC.Text)) Then
            MsgBox("Outer Diameter of the Cap Plate shall be greater than the Diameter of Centre Column." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtodCP.Focus()
            DataValidity = False
         ElseIf (HasCCBottomCapPlate Or HasCCTopCapPlate) And CDbl(Val(.txtidCP.Text)) >= CDbl(Val(.txtdCC.Text)) Then
            MsgBox("Inner Diameter of the Cap Plate shall be less than the Diameter of Centre Column." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtidCP.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txtodBP.Text)) <= CDbl(Val(.txtdCC.Text)) Then
            MsgBox("Outer Diameter of the Base Plate shall be greater than the Diameter of Centre Column." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtodBP.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txtidBP.Text)) >= CDbl(Val(.txtdCC.Text)) Then
            MsgBox("Inner Diameter of the Base Plate shall be less than the Diameter of Centre Column." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtidBP.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txtdUF.Text)) < CDbl(Val(.txtdCC.Text)) Then
            MsgBox("Diameter of the Underflow Cone shall be greater than the Diameter of Centre Column." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtdUF.Focus()
            DataValidity = False
         ElseIf (HasCompRing And CDbl(Val(.txtdCR.Text)) <= CDbl(Val(.txtdUF.Text))) Then
            MsgBox("Diameter of the Compression Ring shall be greater than the Diameter of Underflow Cone." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtdCR.Focus()
            DataValidity = False
         ElseIf (HasCompRing And CDbl(Val(.txtdTank.Text)) < CDbl(Val(.txtdCR.Text))) Then
            MsgBox("Diameter of the Tank shall be greater than the Diameter of Compression Ring." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTank")
            .txtdTank.Focus()
            DataValidity = False
         ElseIf (Not HasCompRing And CDbl(Val(.txtdTank.Text)) < CDbl(Val(.txtdUF.Text))) Then
            MsgBox("Diameter of the Tank shall be greater than the Diameter of Underflow Cone." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTank")
            .txtdTank.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txthWall.Text)) < (CDbl(Val(.txtFreeBoard.Text)) + CDbl(Val(.txthLaun.Text))) Then
            MsgBox("Tank Wall height shall not be less than the sum of Free Board and Launder Wall height." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTank")
            .txthWall.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txthCR.Text)) > CDbl(Val(.txthUF.Text)) Then
            MsgBox("Height of Compression Ring vertical annulus shall be less than Height of Underflow Cone." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txthCR.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txtpcdBolts.Text)) >= CDbl(Val(.txtodBP.Text)) Then
            MsgBox("Centre Column Bolts PCD shall be less than the Outer Diameter of Base Plate." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtpcdBolts.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txtpcdBolts.Text)) <= CDbl(Val(.txtdCC.Text)) Then
            MsgBox("Centre Column Bolts PCD shall be greater than the Diameter of Centre Column." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpTankFloor")
            .txtpcdBolts.Focus()
            DataValidity = False
         ElseIf CDbl(Val(.txtLCantilever.Text)) < minLCant Or CDbl(Val(.txtLCantilever.Text)) > maxLCant Then
            MsgBox("Radial beam cantilever length shall be betweeen " & minLCant & "m and " & maxLCant & "m." & _
                MsgAbortProcess, vbOKOnly, "Invalid user input data")
            Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpSSGeometry")
            .txtLCantilever.Focus()
            DataValidity = False
         ElseIf Me.radCustom.Checked Then
            If (Me.chkTx.Checked) And (Me.chkTy.Checked) And (Me.chkTz.Checked) And (Me.chkRx.Checked) And _
               (Me.chkRy.Checked) And (Me.chkRz.Checked) Then
               MsgBox("Custom Support Condition. Atleast One Degree of Freedom shall be Released." & _
                   MsgAbortProcess, vbOKOnly, "Invalid user input data")
               Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpSupports")
               Me.radCustom.Focus()
               DataValidity = False
            End If
            If (Not Me.chkTx.Checked) And (Not Me.chkTy.Checked) And (Not Me.chkTz.Checked) And _
                (Not Me.chkRx.Checked) And (Not Me.chkRy.Checked) And (Not Me.chkRz.Checked) Then
               MsgBox("Custom Support Condition. Atleast One Degree of Freedom shall be Restrained." & _
                   MsgAbortProcess, vbOKOnly, "Invalid user input data")
               Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpSupports")
               Me.radCustom.Focus()
               DataValidity = False
            End If
         ElseIf (Me.chkCCBasePlate.Checked) And (Me.radCustomCC.Checked) Then
            If (Me.chkTxCC.Checked) And (Me.chkTyCC.Checked) And (Me.chkTzCC.Checked) And _
                (Me.chkRxCC.Checked) And (Me.chkRyCC.Checked) And (Me.chkRzCC.Checked) Then
               MsgBox("Custom Support Condition. Atleast One Degree of Freedom shall be Released." & _
                   MsgAbortProcess, vbOKOnly, "Invalid user input data")
               Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpSupports")
               Me.radCustomCC.Focus()
               DataValidity = False
            End If
            If (Not Me.chkTxCC.Checked) And (Not Me.chkTyCC.Checked) And (Not Me.chkTzCC.Checked) And _
                (Not Me.chkRxCC.Checked) And (Not Me.chkRyCC.Checked) And (Not Me.chkRzCC.Checked) Then
               MsgBox("Custom Support Condition. Atleast One Degree of Freedom shall be Restrained." & _
                   MsgAbortProcess, vbOKOnly, "Invalid user input data")
               Me.tcThickenerInput.SelectedTab = Me.tcThickenerInput.TabPages("tpSupports")
               Me.radCustomCC.Focus()
               DataValidity = False
            End If
         ElseIf CInt(Val(txtAspectRatio.Text)) < MinAspectRatio Then
            txtAspectRatio.Text = MinAspectRatio.ToString
         ElseIf CInt(Val(txtAspectRatio.Text)) > MaxAspectRatio Then
            txtAspectRatio.Text = MaxAspectRatio.ToString
         ElseIf CDbl(Val(txtMaxPlateDim.Text)) < LowerMaxPlateDim Then
            txtMaxPlateDim.Text = Format(LowerMaxPlateDim, "0.000") & " m"
         ElseIf CDbl(Val(txtMaxPlateDim.Text)) > UpperMaxPlateDim Then
            txtMaxPlateDim.Text = Format(UpperMaxPlateDim, "0.000") & " m"
         Else
            DataValidity = True
         End If
      End With
      Return DataValidity
   End Function
   Private Function IsGridDataValid() As Boolean
      Dim GridDataValidity As Boolean = True, nColRB As Integer, dTank As Double, dCR As Double, LCantilever As Double
      nColRB = CInt(Val(Me.txtnColRB.Text))
      dTank = CDbl(Val(Me.txtdTank.Text))
      dCR = CDbl(Val(Me.txtdCR.Text))
      LCantilever = CDbl(Val(Me.txtLCantilever.Text))
      For iRow = 0 To (nColRB - 1)
         With Me.dgvSupportStructure
            If (Me.btnRBSection.Text = ">>") Or (Me.btnRBSection.Text = "") Then
               GridDataValidity = False
               MsgBox("Please choose the radial beam section.", MsgBoxStyle.OkOnly, TG_TITLE)
               Me.btnRBSection.Focus()
               Return GridDataValidity
            End If
            For iCell = indDesc To indRBPattern
               If (iCell = indColSec) Then
                  If (.Rows(iRow).Cells(iCell).Value Is Nothing) OrElse (.Rows(iRow).Cells(iCell).Value.ToString = ">>") Then
                     GridDataValidity = False
                     MsgBox("Please choose the column section.", MsgBoxStyle.OkOnly, TG_TITLE)
                     .CurrentCell = .Rows(iRow).Cells(iCell)
                     Return GridDataValidity
                  End If
               ElseIf (iCell = indCBSec) Then
                  If CBool(.Rows(iRow).Cells(indCB).Value) And (.Rows(iRow).Cells(iCell).Value Is Nothing OrElse .Rows(iRow).Cells(iCell).Value.ToString = ">>") Then
                     GridDataValidity = False
                     MsgBox("Please choose the cross bracing section.", MsgBoxStyle.OkOnly, TG_TITLE)
                     .CurrentCell = .Rows(iRow).Cells(iCell)
                     Return GridDataValidity
                  End If
               ElseIf (iCell = indRBSec) Then
                  If CBool(.Rows(iRow).Cells(indRB).Value) And (.Rows(iRow).Cells(iCell).Value Is Nothing OrElse .Rows(iRow).Cells(iCell).Value.ToString = ">>") Then
                     GridDataValidity = False
                     MsgBox("Please choose the radial bracing section.", MsgBoxStyle.OkOnly, TG_TITLE)
                     .CurrentCell = .Rows(iRow).Cells(iCell)
                     Return GridDataValidity
                  End If
               Else
                  If (iCell <> indCB) And (iCell <> indRB) And (.Rows(iRow).Cells(iCell).Value.ToString = "") Then
                     GridDataValidity = False
                     MsgBox("Please complete all the required fields.", MsgBoxStyle.OkOnly, TG_TITLE)
                     .CurrentCell = .Rows(iRow).Cells(iCell)
                     Return GridDataValidity
                  End If
               End If
            Next iCell
            If (iRow < (nColRB - 1)) Then
               If (CDbl(.Rows(iRow).Cells(2).Value) <= CDbl(.Rows(iRow + 1).Cells(2).Value)) Then
                  GridDataValidity = False
                  MsgBox("Invalid PCD data. PCD" & (iRow + 2) & " shall be less than PCD" & (iRow + 1) & ".", MsgBoxStyle.OkOnly, TG_TITLE)
                  .CurrentCell = .Rows(iRow).Cells(2)
                  Return GridDataValidity
               End If
            End If
            If CDbl(.Rows(iRow).Cells(2).Value) > (dTank - 2 * LCantilever) Then
               GridDataValidity = False
               MsgBox("Invalid PCD data. PCD" & (iRow + 1) & " shall be less than or equal to " & Format((dTank - 2 * LCantilever), "0.000") & " m.", MsgBoxStyle.OkOnly, TG_TITLE)
               .CurrentCell = .Rows(iRow).Cells(2)
            ElseIf CDbl(.Rows(iRow).Cells(2).Value) <= dCR Then
               GridDataValidity = False
               MsgBox("Invalid PCD data. PCD" & (iRow + 1) & " shall be greater than " & Format(dCR, "0.000") & " m.", MsgBoxStyle.OkOnly, TG_TITLE)
               .CurrentCell = .Rows(iRow).Cells(2)
            End If
         End With
         If Not GridDataValidity Then Exit For
      Next iRow
      Return GridDataValidity
   End Function
   Private Sub SetDefaultOptionalCompsAndSupportConditions()
      Me.chkCompRing.Checked = True
      Me.chkCRVAnnulus.Checked = True
      Me.chkCRStiffener.Checked = True
      Me.chkCCBottom.Checked = True
      Me.chkCCTop.Checked = True
      Me.radPinned.Checked = True
      Me.radPinnedCC.Checked = True
      Me.cmbColumnMethod.Text = "Program Generated"
   End Sub
   'Private Sub DefToolTip_Draw(sender As Object, e As DrawToolTipEventArgs) Handles DefToolTip.Draw
   '   e.Graphics.Clear(SystemColors.Info)
   '   e.Graphics.DrawImage(SystemIcons.WinLogo.ToBitmap(), New Point(16, 16))
   '   TextRenderer.DrawText(e.Graphics, e.ToolTipText, e.Font, _
   '                         New Rectangle(64, 8, e.Bounds.Width - 72, e.Bounds.Height - 16), _
   '                         SystemColors.InfoText, Color.Empty, _
   '                         TextFormatFlags.WordBreak Or TextFormatFlags.VerticalCenter)
   'End Sub
   'Private Sub ToolTip_Popup(sender As Object, e As PopupEventArgs) Handles DefToolTip.Popup
   '   e.ToolTipSize = New Size(200, 64)
   'End Sub
   Private Sub SetTooltips()
      DefToolTip.SetToolTip(Me.txtBeta, "Measured with horizontal (strictly in degrees)")
      DefToolTip.SetToolTip(Me.txtAlpha, "Measured with horizontal (strictly in degrees)")
      DefToolTip.SetToolTip(Me.txthDroop, "Preferred droop radius is 13m. Hence, adjust droop depth if necessary.")
      DefToolTip.SetToolTip(Me.txtAspectRatio, "For better analysis results use an aspect ratio between " & MinAspectRatio & " and " & MaxAspectRatio & ".")
      DefToolTip.SetToolTip(Me.txtMaxPlateDim, "For optimal number of plate elements use the maximum plate dimension between " & LowerMaxPlateDim & " and " & UpperMaxPlateDim & ".")
   End Sub
   Private Sub chkCompRing_CheckedChanged(sender As Object, e As EventArgs) Handles chkCompRing.CheckedChanged
      If Not Me.chkCompRing.Checked Then
         Me.chkCRStiffener.Checked = False
         Me.chkCRStiffener.Enabled = False
         Me.chkCRVAnnulus.Checked = False
         Me.chkCRVAnnulus.Enabled = False
         Me.txtdCR.Text = Me.txtdUF.Text
         Me.txtdCR.Enabled = False
      Else
         'Me.chkCRStiffener.Enabled = True
         Me.chkCRVAnnulus.Enabled = True
         Me.txtdCR.Enabled = True
      End If
   End Sub
   Private Sub chkCRVAnnulus_CheckedChanged(sender As Object, e As EventArgs) Handles chkCRVAnnulus.CheckedChanged
      If Me.chkCompRing.Checked And Me.chkCRVAnnulus.Checked Then
         Me.chkCRStiffener.Enabled = True
      Else
         Me.chkCRStiffener.Checked = False
         Me.chkCRStiffener.Enabled = False
      End If
   End Sub
   Private Sub chkCCBottom_CheckedChanged() Handles chkCCBottom.CheckedChanged
      If Me.chkCCBottom.Checked Then
         Me.chkCCTop.Enabled = True
         Me.chkCCInterCapPlate.Enabled = True
         Me.chkCCBasePlate.Enabled = True
      Else
         Me.chkCCTop.Checked = False
         Me.chkCCTop.Enabled = False
         Me.chkCCInterCapPlate.Checked = False
         Me.chkCCInterCapPlate.Enabled = False
         Me.chkCCBasePlate.Checked = False
         Me.chkCCBasePlate.Enabled = False
      End If
   End Sub
   Private Sub chkCCTop_CheckedChanged() Handles chkCCTop.CheckedChanged
      If Me.chkCCTop.Checked Then
         Me.chkCCInterCapPlate.Checked = False
         Me.chkCCInterCapPlate.Enabled = False
         Me.chkCCCapPlateTop.Enabled = True
         Me.chkCCStiffenerTop.Enabled = True
      Else
         Me.chkCCInterCapPlate.Enabled = True
         Me.chkCCCapPlateTop.Checked = False
         Me.chkCCCapPlateTop.Enabled = False
         Me.chkCCStiffenerTop.Checked = False
         Me.chkCCStiffenerTop.Enabled = False
      End If
   End Sub
   Private Sub radFixed_CheckedChanged(sender As Object, e As EventArgs) Handles radFixed.CheckedChanged
      If Me.radFixed.Checked Then
         ToggleCheckedAllDegreesOfFreedom("Other", True)
         ToggleEnabledAllDegreesOfFreedom("Other", False)
      End If
   End Sub
   Private Sub radPinned_CheckedChanged(sender As Object, e As EventArgs) Handles radPinned.CheckedChanged
      If Me.radPinned.Checked Then
         CheckPinnedDegreesOfFreedom("Other")
         ToggleEnabledAllDegreesOfFreedom("Other", False)
      End If
   End Sub
   Private Sub radCustom_CheckedChanged(sender As Object, e As EventArgs) Handles radCustom.CheckedChanged
      If Me.radCustom.Checked Then
         ToggleCheckedAllDegreesOfFreedom("Other", False)
         ToggleEnabledAllDegreesOfFreedom("Other", True)
      End If
   End Sub
   Private Sub radPinnedCC_CheckedChanged(sender As Object, e As EventArgs) Handles radPinnedCC.CheckedChanged
      If Me.radPinnedCC.Checked Then
         CheckPinnedDegreesOfFreedom("CC")
         ToggleEnabledAllDegreesOfFreedom("CC", False)
      End If
   End Sub
   Private Sub radCustomCC_CheckedChanged(sender As Object, e As EventArgs) Handles radCustomCC.CheckedChanged
      If Me.radCustomCC.Checked Then
         ToggleCheckedAllDegreesOfFreedom("CC", False)
         ToggleEnabledAllDegreesOfFreedom("CC", True)
      End If
   End Sub
   Private Sub ToggleEnabledAllDegreesOfFreedom(ByVal ColName As String, ByVal Toggle As Boolean)
      If ColName = "CC" Then
         Me.chkTxCC.Enabled = Toggle : Me.chkTyCC.Enabled = Toggle : Me.chkTzCC.Enabled = Toggle : Me.chkRxCC.Enabled = Toggle : Me.chkRyCC.Enabled = Toggle : Me.chkRzCC.Enabled = Toggle
      Else
         Me.chkTx.Enabled = Toggle : Me.chkTy.Enabled = Toggle : Me.chkTz.Enabled = Toggle : Me.chkRx.Enabled = Toggle : Me.chkRy.Enabled = Toggle : Me.chkRz.Enabled = Toggle
      End If
   End Sub
   Private Sub ToggleCheckedAllDegreesOfFreedom(ByVal ColName As String, ByVal Toggle As Boolean)
      If ColName = "CC" Then
         Me.chkTxCC.Checked = Toggle : Me.chkTyCC.Checked = Toggle : Me.chkTzCC.Checked = Toggle : Me.chkRxCC.Checked = Toggle : Me.chkRyCC.Checked = Toggle : Me.chkRzCC.Checked = Toggle
      Else
         Me.chkTx.Checked = Toggle : Me.chkTy.Checked = Toggle : Me.chkTz.Checked = Toggle : Me.chkRx.Checked = Toggle : Me.chkRy.Checked = Toggle : Me.chkRz.Checked = Toggle
      End If
   End Sub
   Private Sub CheckPinnedDegreesOfFreedom(ByVal ColName As String)
      If ColName = "CC" Then
         Me.chkTxCC.Checked = True : Me.chkTyCC.Checked = True : Me.chkTzCC.Checked = True : Me.chkRxCC.Checked = False : Me.chkRyCC.Checked = False : Me.chkRzCC.Checked = False
      Else
         Me.chkTx.Checked = True : Me.chkTy.Checked = True : Me.chkTz.Checked = True : Me.chkRx.Checked = False : Me.chkRy.Checked = False : Me.chkRz.Checked = False
      End If
   End Sub
   Private Sub CalcHeightOfUFCone(sender As Object, e As EventArgs) Handles txtdUF.TextChanged, _
      txtdCC.TextChanged, txtBeta.TextChanged
      Dim dCC As Double, dUF As Double, Beta As Double, hUF As Double
      dCC = CDbl(Val(Me.txtdCC.Text))
      dUF = CDbl(Val(Me.txtdUF.Text))
      Beta = CDbl(Val(Me.txtBeta.Text))
      hUF = ((dUF - dCC) / 2) * Math.Tan(Deg2Rad(Beta))
      Me.txthUF.Text = Format(hUF, "0.000") & " m"
   End Sub
   Private Sub UpdateCRDiameter(sender As Object, e As EventArgs) Handles txtdUF.TextChanged
      If Not Me.chkCompRing.Checked Then
         Me.txtdCR.Text = Me.txtdUF.Text
      End If
   End Sub
   Private Sub CalcCRWidth(sender As Object, e As EventArgs) Handles txtdUF.TextChanged, txtdCR.TextChanged
      Dim dUF As Double, dCR As Double, bCR As Double
      dCR = CDbl(Val(Me.txtdCR.Text))
      dUF = CDbl(Val(Me.txtdUF.Text))
      bCR = ((dCR - dUF) / 2)
      Me.txtbCR.Text = Format(bCR, "0.000") & " m"
   End Sub
   Private Sub CalcDroopRadius(sender As Object, e As EventArgs) Handles txtdTank.TextChanged, txthDroop.TextChanged, _
      txtnRB.TextChanged
      Dim dTank As Double, hDroop As Double, nRB As Double, LHalfChord As Double, rDroop As Double, incAngTotal As Double
      dTank = CDbl(Val(txtdTank.Text))
      hDroop = CDbl(Val(txthDroop.Text))
      nRB = CLng(Val(txtnRB.Text))
      If (hDroop > 0) And (nRB > 0) Then
         LHalfChord = (dTank / 2) * Math.Sin(Deg2Rad((1 / 2) * TotalAngle / nRB))
         rDroop = (hDroop ^ 2 + LHalfChord ^ 2) / (2 * hDroop)
         incAngTotal = 2 * Rad2Deg(Math.Asin(LHalfChord / rDroop))
         Me.txtHalfChordLength.Text = Format(LHalfChord, "0.000") & "m"
         Me.txtrDroop.Text = Format(rDroop, "0.000") & "m"
         Me.txtTheta.Text = Format(incAngTotal, "0.0000") & "°"
      Else
         Me.txtHalfChordLength.Clear()
         '/------------- Modified -------------/
         If hDroop = 0.0 Then
            Me.txtrDroop.Text = "-"
            Me.txtTheta.Text = "-"
         Else
            Me.txtrDroop.Clear()
            Me.txtTheta.Clear()
         End If
         '/------------- Modified -------------/
      End If
   End Sub
   Private Sub GenerateGrid() Handles txtnColRB.TextChanged, txtdTank.TextChanged, _
      txtdCR.TextChanged, txtLCantilever.TextChanged ', cmbColumnMethod.SelectedValueChanged
      UpdateGridView()
   End Sub
   Private Sub cmbColumnMethod_SelectedValueChanged() Handles cmbColumnMethod.SelectedValueChanged
      If Me.dgvSupportStructure.RowCount >= 1 Then
         ToggleGridViewReadOnlyColumns()
      End If
   End Sub
   Private Sub UpdateGridView()
      Dim SlNo As Integer, Desc As String, PCD As String, ELofBP As String, CBYesNo As Boolean, CBPattern As String, _
          RBYesNo As Boolean, RBPattern As String
      Dim LCantilever As Double, dTank As Double, dCR As Double, nColRB As Integer, EquiSpace As Double
      LCantilever = CDbl(Val(Me.txtLCantilever.Text))
      dTank = CDbl(Val(Me.txtdTank.Text))
      dCR = CDbl(Val(Me.txtdCR.Text))
      nColRB = CInt(Val(Me.txtnColRB.Text))
      If (LCantilever = 0.0) OrElse (dTank = 0.0) OrElse (dCR = 0.0) OrElse (nColRB <= 0) Then
         Me.dgvSupportStructure.Rows.Clear()
         Exit Sub
      Else
         EquiSpace = ((dTank - 2 * LCantilever - dCR) / 2) / nColRB
      End If
      With Me.dgvSupportStructure
         .Rows.Clear()
         For iRow = 0 To (nColRB - 1)
            SlNo = (iRow + 1)
            Desc = "PCD" & SlNo
            PCD = Format((dTank - (2 * LCantilever) - (2 * iRow * EquiSpace)), "0.000")
            ELofBP = Format(0, "0.000")
            If iRow = 0 Then
               CBYesNo = True : CBPattern = "Alternate" : RBYesNo = True : RBPattern = "Alternate"
            Else
               CBYesNo = False : CBPattern = "Continuous" : RBYesNo = False : RBPattern = "Continuous"
            End If
            .Rows.Add(SlNo, Desc, PCD.ToString, ">>", ELofBP.ToString, CBYesNo, "", CBPattern, RBYesNo, "", RBPattern)
            If CBYesNo Then
               .Rows(iRow).Cells(indCBSec).Value = ">>"
            End If
            If RBYesNo Then
               .Rows(iRow).Cells(indRBSec).Value = ">>"
            End If
         Next iRow
         .Rows(nColRB - 1).Cells(indRB).ReadOnly = True
         .Rows(nColRB - 1).Cells(indRBPattern).ReadOnly = True

         ToggleGridViewReadOnlyColumns()


         ReDim dbCols(0 To CInt(Val(Me.txtnColRB.Text)))
         ReDim dbCB(0 To CInt(Val(Me.txtnColRB.Text)))
         ReDim dbRB(0 To CInt(Val(Me.txtnColRB.Text)))


      End With
   End Sub
   Private Sub ToggleGridViewReadOnlyColumns()
      With Me.dgvSupportStructure
         If Me.cmbColumnMethod.Text = "User Defined" Then
            .Columns("colDesc").ReadOnly = False
            .Columns("colPCD").ReadOnly = False
         Else
            .Columns("colDesc").ReadOnly = True
            .Columns("colPCD").ReadOnly = True
         End If
      End With
   End Sub
   Private Sub GetOptionalElementsValues()
      HasCompRing = chkCompRing.Checked
      HasCRVertAnnulus = chkCRVAnnulus.Checked
      HasCRStiffener = chkCRStiffener.Checked
      HasCCBottom = chkCCBottom.Checked
      HasCCBottomCapPlate = chkCCInterCapPlate.Checked
      HasCCTop = chkCCTop.Checked
      HasCCTopCapPlate = chkCCCapPlateTop.Checked
      HasCCStiffener = chkCCStiffenerTop.Checked
      HasCCBottomBasePlate = chkCCBasePlate.Checked
   End Sub
   '/---------------- Code to be modified - starts here ---------------/
   Private Sub GenerateMeshData(ByRef ComMesh As MeshData,
                                ByVal Com As Component)
      With ComMesh
         Select Case Com
            Case Component.LWall
               .Hs = GD.hLaun
               .LsBot = (PI * GD.dLaun / GD.nRB)
            Case Component.LFloor
               .Hs = GD.bLaun
               .LsBot = (PI * GD.dLaun / GD.nRB)
            Case Component.WFreeBoard
               .Hs = GD.FreeBoard
               .LsBot = (PI * GD.dTank / GD.nRB)
            Case Component.WLWall
               .Hs = GD.hLaun
               .LsBot = (PI * GD.dTank / GD.nRB)
            Case Component.Wall
               .Hs = GD.hWall - GD.FreeBoard - GD.hLaun
               .LsBot = (PI * GD.dTank / GD.nRB)
         End Select
         If Com = Component.LWall Then
            .LsTop = (PI * (GD.dLaun + 2 * GD.bLaun) / GD.nRB)
         Else
            .LsTop = .LsBot
         End If
         .Nd = CeilingEx(.Hs / GD.MaxPlateDim, 1)
         .Hp = .Hs / .Nd
         .LpBot = Math.Min(GD.AspectRatio * .Hp, GD.MaxPlateDim)
         .LpTop = .LpBot
         If Com = Component.LFloor Then
            .NrBot = CeilingEx(.LsBot / .LpTop, 1)
            .NrTop = .NrBot
         ElseIf Com = Component.WLWall Then
            .NrBot = CeilingEx(.LsBot / .LpBot, 1)
            .NrTop = .NrBot
         Else
            .NrBot = CeilingEx(.LsBot / .LpBot, 1)
            .NrTop = CeilingEx(.LsTop / .LpTop, 1)
         End If
         .LpBot = .LsBot / .NrBot
         If Com = Component.LWall Then
            .LpTop = .LpBot
         Else
            .LpTop = .LsTop / .NrTop
         End If
      End With
   End Sub
   Private Sub GenerateThickenerGeometry()
      Dim Div As Integer ' Iterator for each division nodes creation
      ' Plate Node Numbers for Modelling the Plates
      Dim PL As Plate
      Dim I As Integer
      Dim jYChord As Double ' Y-Coodinate (at droop) of the jth point
      Dim jXChord As Double ' X-Coodinate (at droop) of the jth point
      Dim jZChord As Double ' Z-Coodinate (at droop) of the jth point
      Dim jTheta As Double ' Included angle (at droop) of jth point.
      Dim iDelta As Double ' Angle with Respect to Positive X-axis of the STAAD.Pro.
      Dim jdDelta As Double
      Dim SeqNodeNum As Long, SeqPlateNum As Long

      ' Variable for storing important Nodes list
      ' Variables for storing important Groups list
      Dim Groups As New MemberGroups
      Dim Nodes As New NodeGroups

      TG = New ThickenerGenerator(Me)

      'Initialize the Node, Beam and Plate numbers with zero
      SeqNodeNum = 0 : SeqPlateNum = 0

      GD = TG.GetGeometryData : BP = TG.GetBasicProfile

GetFileName:
      ' Get the filename to save with complete path
      defFName = GD.dTank & "m-" & GD.hWall & "m-" & GD.hDroop & "m (" & GD.nRB & " x " & GD.nColRB & ")-" & Rad2Deg(GD.Alpha) & "° Thickener.std"
      sfdSaveFile.Filter = "STAAD.Pro Files (*.std)|*.std"
      sfdSaveFile.FileName = defFName
      If sfdSaveFile.ShowDialog = Windows.Forms.DialogResult.OK Then
         FullFileName = sfdSaveFile.FileName
      Else
         Me.MdiParent.WindowState = FormWindowState.Maximized
         MsgBox("User cancelled the operation. The process will now be aborted.", vbOKOnly, "Operation cancelled by user")
         Exit Sub
      End If
      With OS
         .NewSTAADFile(FullFileName, LengthUnit.Meter, ForceUnit.KiloNewton)
         .ShowApplication()
      End With

      ' Generation of Launder Wall - Starts Here
      Dim LWMesh As MeshData
      GenerateMeshData(LWMesh, Component.LWall)

      ReDim Groups.LW(0)

      ReDim Nodes.LW(CInt(LWMesh.Nd))
      For Div = 0 To CInt(LWMesh.Nd)
         ReDim Nodes.LW(Div).Nodes(0)
         For I = 0 To CInt(GD.nRB - 1)
            iDelta = GD.sDelta + (I * GD.Omega)
            SeqNodeNum = SeqNodeNum + 1
            ' Store the Node Number and Coords of First Node of the Segment
            With Nodes.LW(Div).Nodes(UBound(Nodes.LW(Div).Nodes))
               .No = SeqNodeNum
               .x = GD.Origin.x + (GD.dTank / 2 - GD.bLaun) * Math.Cos(iDelta)
               .y = BP.sNodeLW.y + GD.hLaun - (Div * LWMesh.Hp)
               .z = GD.Origin.z + (GD.dTank / 2 - GD.bLaun) * Math.Sin(iDelta)
            End With
            ReDim Preserve Nodes.LW(Div).Nodes(UBound(Nodes.LW(Div).Nodes) + 1)
            For J = 1 To LWMesh.NrBot - 1
               SeqNodeNum = SeqNodeNum + 1
               jTheta = J * (GD.Omega / LWMesh.NrBot)
               jYChord = BP.sNodeLW.y + GD.hLaun - (Div * LWMesh.Hp)
               jdDelta = iDelta + jTheta
               jZChord = (GD.dTank / 2 - GD.bLaun) * Math.Sin(jdDelta)
               jXChord = (GD.dTank / 2 - GD.bLaun) * Math.Cos(jdDelta)
               With Nodes.LW(Div).Nodes(UBound(Nodes.LW(Div).Nodes))
                  .No = SeqNodeNum
                  .x = jXChord
                  .y = jYChord
                  .z = jZChord
               End With
               ReDim Preserve Nodes.LW(Div).Nodes(UBound(Nodes.LW(Div).Nodes) + 1)
            Next J
         Next I
         ReDim Preserve Nodes.LW(Div).Nodes(UBound(Nodes.LW(Div).Nodes) - 1)
      Next Div
      For Div = 0 To CInt(LWMesh.Nd)
         For I = 0 To UBound(Nodes.LW(Div).Nodes)
            OSGeometry.CreateNode(Nodes.LW(Div).Nodes(I).No, Nodes.LW(Div).Nodes(I).x, Nodes.LW(Div).Nodes(I).y, Nodes.LW(Div).Nodes(I).z)
         Next I
         If Div >= 1 Then
            For I = 0 To UBound(Nodes.LW(Div).Nodes)
               SeqPlateNum = SeqPlateNum + 1
               With PL
                  If I = UBound(Nodes.LW(Div).Nodes) Then
                     .NodeA = Nodes.LW(Div - 1).Nodes(I).No : .NodeB = Nodes.LW(Div - 1).Nodes(0).No : .NodeC = Nodes.LW(Div).Nodes(0).No : .NodeD = Nodes.LW(Div).Nodes(I).No
                  Else
                     .NodeA = Nodes.LW(Div - 1).Nodes(I).No : .NodeB = Nodes.LW(Div - 1).Nodes(I + 1).No : .NodeC = Nodes.LW(Div).Nodes(I + 1).No : .NodeD = Nodes.LW(Div).Nodes(I).No
                  End If
               End With
               OSGeometry.CreatePlate(SeqPlateNum, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
               Groups.LW(UBound(Groups.LW)) = CInt(SeqPlateNum)
               ReDim Preserve Groups.LW(UBound(Groups.LW) + 1)
            Next I
         End If
      Next Div
      ReDim Preserve Groups.LW(UBound(Groups.LW) - 1)
      ' Create Group of the Plate Elements in Launder Wall Portion
      OSGeometry.CreateGroupEx(GroupType.Plates, "_LWall", UBound(Groups.LW) + 1, Groups.LW)

      ' Generation of Launder Floor - Starts Here
      Dim LFMesh As MeshData
      GenerateMeshData(LFMesh, Component.LFloor)

      Call TG.GenerateMesh(LFMesh, LWMesh, Groups.LF, Nodes.LF, Nodes.LW, "LFloor", SeqNodeNum, SeqPlateNum, Component.LFloor)
      ' Generation of Tank Wall in the Free Board Portion - Starts Here
      Dim FBMesh As MeshData
      GenerateMeshData(FBMesh, Component.WFreeBoard)

      ' Generation of Tank Wall in Launder Wall Portion - Starts Here
      Dim WLWMesh As MeshData
      GenerateMeshData(WLWMesh, Component.WLWall)

      Call TG.GenerateMesh(WLWMesh, LFMesh, Groups.WLW, Nodes.WLW, Nodes.LF, "WLWall", SeqNodeNum, SeqPlateNum, Component.WLWall)
      Call TG.GenerateMesh(FBMesh, WLWMesh, Groups.FB, Nodes.FB, Nodes.WLW, "FBWall", SeqNodeNum, SeqPlateNum, Component.WFreeBoard)

      ' Generation of Tank Wall Before Droop Interface - Starts Here
      Dim WMesh As MeshData
      GenerateMeshData(WMesh, Component.Wall)

      Dim AdjHeight As Double

      Call TG.CalcAdjHeight(WMesh, AdjHeight)

      With WMesh
         .Hs = GD.hWall - GD.FreeBoard - GD.hLaun - AdjHeight
         .Nd = CeilingEx(.Hs / GD.MaxPlateDim, 1)
         .Hp = .Hs / .Nd
      End With

      Call TG.GenerateMesh(WMesh, LFMesh, Groups.W, Nodes.W, Nodes.LF, "Wall", SeqNodeNum, SeqPlateNum, Component.Wall)
      If GD.hDroop <> 0.0# Then
         Call TG.GenerateIFMesh(AdjHeight, Groups.Intface, Nodes.Intface, Nodes.W, "Droop", SeqNodeNum, SeqPlateNum, Component.WInterface)
      End If

      Dim SSI() As SupportStructureInfo, nPCDs As Integer, intPCD As Integer
      Dim ColumnsSupport As SupportCondition, sMyMz As MemRelease, eMyMz As MemRelease
      Dim ColumnsSupportNum As Long, sRelMyMz As Long, eRelMyMz As Long

      'Dim OffSpecStart As Integer, OffSpecEnd As Integer, CBStep As Integer, RBStep As Integer

      With ColumnsSupport
         If radFixed.Checked Then
            ColumnsSupportNum = CLng(OSSupport.CreateSupportFixed)
         Else
            ReDim .Rel(0 To 5) : ReDim .Spr(0 To 5)
            .Rel(0) = Math.Abs(CDbl(Not chkTx.Checked)) : .Rel(1) = Math.Abs(CDbl(Not chkTy.Checked)) : .Rel(2) = Math.Abs(CDbl(Not chkTz.Checked))
            .Rel(3) = Math.Abs(CDbl(Not chkRx.Checked)) : .Rel(4) = Math.Abs(CDbl(Not chkRy.Checked)) : .Rel(5) = Math.Abs(CDbl(Not chkRz.Checked))
            .Spr(0) = 0.0# : .Spr(1) = 0.0# : .Spr(2) = 0.0# : .Spr(3) = 0.0# : .Spr(4) = 0.0# : .Spr(5) = 0.0#
            ColumnsSupportNum = CLng(OSSupport.CreateSupportFixedBut(.Rel, .Spr))
         End If
      End With
      With sMyMz
         ReDim .Rel(0 To 5) : ReDim .Spr(0 To 5)
         .rEnd = ReleaseEnd.sEnd
         .Rel(0) = 0 : .Rel(1) = 0 : .Rel(2) = 0 : .Rel(3) = 0 : .Rel(4) = 1 : .Rel(5) = 1
         .Spr(0) = 0 : .Spr(1) = 0 : .Spr(2) = 0 : .Spr(3) = 0 : .Spr(4) = 0 : .Spr(5) = 0
         sRelMyMz = CLng(OSProperty.CreateMemberReleaseSpec(.rEnd, .Rel, .Spr))
      End With
      With eMyMz
         ReDim .Rel(0 To 5) : ReDim .Spr(0 To 5)
         .rEnd = ReleaseEnd.eEnd : .Rel = sMyMz.Rel : .Spr = sMyMz.Spr
         eRelMyMz = CLng(OSProperty.CreateMemberReleaseSpec(.rEnd, .Rel, .Spr))
      End With

      nPCDs = CInt(Val(Me.txtnColRB.Text))
      ReDim SSI(nPCDs)
      For intPCD = 0 To UBound(SSI)
         If intPCD < UBound(SSI) Then
            With SSI(intPCD)
               .SlNo = CInt(Me.dgvSupportStructure.Rows(intPCD).Cells(indSlNo).Value)
               .Desc = CStr(Me.dgvSupportStructure.Rows(intPCD).Cells(indDesc).Value)
               .PCD = CDbl(Val(Me.dgvSupportStructure.Rows(intPCD).Cells(indPCD).Value))
               .EL = CDbl(Val(Me.dgvSupportStructure.Rows(intPCD).Cells(indEL).Value))
               .HasCB = CBool(Me.dgvSupportStructure.Rows(intPCD).Cells(indCB).Value)
               If .HasCB Then
                  .CBSec = dbCB(intPCD)
               End If
               .CBPattern = CStr(Me.dgvSupportStructure.Rows(intPCD).Cells(indCBPattern).Value)
               .HasRB = CBool(Me.dgvSupportStructure.Rows(intPCD).Cells(indRB).Value)
               If .HasRB Then
                  .RBSec = dbRB(intPCD)
               End If
               .RBPattern = CStr(Me.dgvSupportStructure.Rows(intPCD).Cells(indRBPattern).Value)
            End With
         End If
         If intPCD = 0 Then
            With SSI(intPCD)
               '/------- Modified -----/
               If GD.hDroop <> 0.0 Then
                  Call TG.GenerateCompMesh(.NodesPCD, Nodes.Intface, .grpPCD, SeqNodeNum, SeqPlateNum, Component.FloorPlate, GD.dTank, .PCD)
               Else
                  ReDim Nodes.ZeroDroop(0)
                  Nodes.ZeroDroop(0) = Nodes.W(0)
                  Call TG.GenerateCompMesh(.NodesPCD, Nodes.ZeroDroop, .grpPCD, SeqNodeNum, SeqPlateNum, Component.FloorPlate, GD.dTank, .PCD)
               End If
            End With
         ElseIf intPCD = UBound(SSI) Then
            With SSI(intPCD)
               Call TG.GenerateCompMesh(.NodesPCD, SSI(intPCD - 1).NodesPCD, .grpPCD, SeqNodeNum, _
                                       SeqPlateNum, Component.FloorPlate, SSI(intPCD - 1).PCD, GD.dCR)
            End With
         Else
            With SSI(intPCD)
               Call TG.GenerateCompMesh(.NodesPCD, SSI(intPCD - 1).NodesPCD, .grpPCD, SeqNodeNum, _
                                       SeqPlateNum, Component.FloorPlate, SSI(intPCD - 1).PCD, .PCD)
            End With
         End If
         Call TG.GenerateBeams(SSI(intPCD).NodesPCD, SSI(intPCD).BeamIncPCD, SeqPlateNum)
         Dim nBeams As Long
         'Dim sNodeCol As NodeCoOrds, eNodeCol As NodeCoOrds
         nBeams = CLng((UBound(SSI(intPCD).BeamIncPCD) + 1) / GD.nRB)
         ReDim SSI(intPCD).ColumnIncPCD(CInt(GD.nRB - 1)) : ReDim SSI(intPCD).grpColumnPCD(CInt(GD.nRB - 1)) : ReDim SSI(intPCD).SupNodes(CInt(GD.nRB - 1))
         'If intPCD < UBound(SSI) Then
         '   For I = 0 To CInt(GD.nRB - 1)
         '      With SSI(intPCD).BeamIncPCD(I)
         '         eNodeCol = .sNode
         '         SeqNodeNum = SeqNodeNum + 1
         '         sNodeCol.No = SeqNodeNum : sNodeCol.x = eNodeCol.x : sNodeCol.y = GD.Origin.y + SSI(intPCD).EL : sNodeCol.z = eNodeCol.z
         '         OSGeometry.CreateNode(SeqNodeNum, sNodeCol.x, sNodeCol.y, sNodeCol.z)
         '         SeqPlateNum = SeqPlateNum + 1
         '         With SSI(intPCD).ColumnIncPCD(I)
         '            .No = SeqPlateNum
         '            .sNode = sNodeCol
         '            .eNode = eNodeCol
         '            SSI(intPCD).grpColumnPCD(I) = CInt(.No)
         '            SSI(intPCD).SupNodes(I) = CInt(.sNode.No)
         '            'Columns are created below
         '            OSGeometry.CreateBeam(.No, .sNode.No, .eNode.No)
         '            OSProperty.AssignBetaAngle(.No, (-1 * Rad2Deg(GD.sDelta + I * GD.Omega)))
         '            OSSupport.AssignSupportToNode(.sNode.No, ColumnsSupportNum)
         '         End With
         '      End With
         '   Next I
         'End If
         With SSI(intPCD)
            If intPCD = UBound(SSI) Then
               .Desc = "PCD" & (UBound(SSI) + 1)
            End If
            OSGeometry.CreateGroupEx(GroupType.Plates, "_FP_" & .Desc, UBound(.grpPCD) + 1, .grpPCD)
            'ReDim .grpBeamPCD(UBound(.BeamIncPCD))
            'For I = 0 To UBound(.BeamIncPCD)
            '   .grpBeamPCD(I) = CInt(.BeamIncPCD(I).No)
            'Next I
            'If intPCD = 0 Then
            '   CreateSTAADMemberProperty(RadBeam)
            '   With RadBeam.OffSpec
            '      .LocalOrGlobal = OffsetAxis.GlobalAxis
            '      .StartOrEnd = OffsetEnd.sEnd
            '      .x = 0.0 : .y = (-1) * (RadBeam.sData.h / 2 / 1000) / Math.Cos(Deg2Rad(Val(Me.txtAlpha.Text))) : .z = 0.0
            '      OffSpecStart = CInt(OSProperty.CreateMemberOffsetSpec(.StartOrEnd, .LocalOrGlobal, .x, .y, .z))
            '      .StartOrEnd = OffsetEnd.eEnd
            '      OffSpecEnd = CInt(OSProperty.CreateMemberOffsetSpec(.StartOrEnd, .LocalOrGlobal, .x, .y, .z))
            '   End With
            '   With RadBeam.mData
            '      .ID = CInt(OSProperty.CreateIsotropicMaterialProperties(.STAADName, .E, .Poisson, .G, .Density, .Alpha, .CrDamp))
            '   End With
            'End If
            'OSGeometry.CreateGroupEx(GroupType.Beams, "_RB_" & .Desc, UBound(.grpBeamPCD) + 1, .grpBeamPCD)
            'OSProperty.AssignBeamProperty(.grpBeamPCD, RadBeam.ID)

            'AssignMaterialPropertyToBeams(.grpBeamPCD, RadBeam.mData.STAADName)

            'OSProperty.AssignMemberSpecToBeam(.grpBeamPCD, OffSpecStart)
            'OSProperty.AssignMemberSpecToBeam(.grpBeamPCD, OffSpecEnd)
            'If intPCD < UBound(SSI) Then
            '   OSGeometry.CreateGroupEx(GroupType.Beams, "_COL_" & .Desc, UBound(.grpColumnPCD) + 1, .grpColumnPCD)
            '   OSGeometry.CreateGroupEx(GroupType.Nodes, "_SUP_" & .Desc, UBound(.SupNodes) + 1, .SupNodes)
            '   dbCols(intPCD).nTableID = CType(GetTableID(dbCols(intPCD).dbName), Country)
            '   .ColSec = dbCols(intPCD)
            '   CreateSTAADMemberProperty(.ColSec)
            '   OSProperty.AssignBeamProperty(.grpColumnPCD, .ColSec.ID)
            '   OSProperty.AssignMemberSpecToBeam(.grpColumnPCD, OffSpecEnd)
            '   With .ColSec.mData
            '      .ID = CInt(OSProperty.CreateIsotropicMaterialProperties(.STAADName, .E, .Poisson, .G, .Density, .Alpha, .CrDamp))
            '   End With
            '   AssignMaterialPropertyToBeams(.grpColumnPCD, .ColSec.mData.STAADName)
            'End If

            '' Generate Cross Bracings
            'If .HasCB Then
            '   Dim sNodeCB1 As NodeCoOrds, eNodeCB1 As NodeCoOrds, mNodeCB As NodeCoOrds
            '   Dim sNodeCB2 As NodeCoOrds, eNodeCB2 As NodeCoOrds
            '   Select Case .CBPattern
            '      Case "Continuous"
            '         CBStep = 1
            '      Case "Alternate"
            '         CBStep = 2
            '      Case "Skip 2 Bays"
            '         CBStep = 3
            '      Case "Skip 3 Bays"
            '         CBStep = 4
            '      Case "Skip 4 Bays"
            '         CBStep = 5
            '   End Select
            '   ReDim .CBIncPCD(0)
            '   For I = 0 To CInt(GD.nRB - 1) Step CBStep
            '      sNodeCB1 = .ColumnIncPCD(I).eNode
            '      sNodeCB2 = .ColumnIncPCD(I).sNode
            '      If I = GD.nRB - 1 Then
            '         eNodeCB1 = .ColumnIncPCD(0).sNode
            '         eNodeCB2 = .ColumnIncPCD(0).eNode
            '      Else
            '         eNodeCB1 = .ColumnIncPCD(I + 1).sNode
            '         eNodeCB2 = .ColumnIncPCD(I + 1).eNode
            '      End If
            '      SeqNodeNum = SeqNodeNum + 1
            '      With mNodeCB
            '         .No = SeqNodeNum : .x = (sNodeCB1.x + eNodeCB1.x) / 2 : .y = (sNodeCB1.y + eNodeCB1.y) / 2 : .z = (sNodeCB1.z + eNodeCB1.z) / 2
            '         OSGeometry.CreateNode(.No, .x, .y, .z)
            '      End With
            '      Call CreateNewBeam(SeqPlateNum, SSI(intPCD).CBIncPCD, sNodeCB1.No, mNodeCB.No, sRelMyMz, eRelMyMz)
            '      Call CreateNewBeam(SeqPlateNum, SSI(intPCD).CBIncPCD, mNodeCB.No, eNodeCB1.No, sRelMyMz, eRelMyMz)
            '      Call CreateNewBeam(SeqPlateNum, SSI(intPCD).CBIncPCD, sNodeCB2.No, mNodeCB.No, sRelMyMz)
            '      Call CreateNewBeam(SeqPlateNum, SSI(intPCD).CBIncPCD, mNodeCB.No, eNodeCB2.No, , eRelMyMz)
            '   Next I
            '   ReDim Preserve .CBIncPCD(UBound(.CBIncPCD) - 1)
            '   ReDim Preserve .grpCBPCD(UBound(.CBIncPCD))
            '   For I = 0 To UBound(.CBIncPCD)
            '      .grpCBPCD(I) = CInt(.CBIncPCD(I).No)
            '   Next I
            '   OSGeometry.CreateGroupEx(GroupType.Beams, "_CBRACE_" & .Desc, UBound(.grpCBPCD) + 1, .grpCBPCD)
            '   dbCB(intPCD).nTableID = CType(GetTableID(dbCB(intPCD).dbName), Country)
            '   .CBSec = dbCB(intPCD)
            '   CreateSTAADMemberProperty(.CBSec)
            '   OSProperty.AssignBeamProperty(.grpCBPCD, .CBSec.ID)
            '   With .CBSec.mData
            '      .ID = CInt(OSProperty.CreateIsotropicMaterialProperties(.STAADName, .E, .Poisson, .G, .Density, .Alpha, .CrDamp))
            '   End With
            '   AssignMaterialPropertyToBeams(.grpCBPCD, .CBSec.mData.STAADName)
            '   'AssignMaterialPropertyToBeams(.grpCBPCD, A53GrB.Grade)
            'End If
            '' End of Cross Bracings Generation
         End With
      Next intPCD
      '' Generate Radial Bracings
      'For intPCD = 0 To UBound(SSI) - 1
      '   With SSI(intPCD)
      '      If .HasRB And (intPCD <> UBound(SSI) - 1) Then
      '         Dim sNodeRB1 As NodeCoOrds, eNodeRB1 As NodeCoOrds, mNodeRB As NodeCoOrds
      '         Dim sNodeRB2 As NodeCoOrds, eNodeRB2 As NodeCoOrds
      '         Select Case .RBPattern
      '            Case "Continuous"
      '               RBStep = 1
      '            Case "Alternate"
      '               RBStep = 2
      '            Case "Skip 2 Bays"
      '               RBStep = 3
      '            Case "Skip 3 Bays"
      '               RBStep = 4
      '            Case "Skip 4 Bays"
      '               RBStep = 5
      '         End Select
      '         ReDim .RBIncPCD(0)
      '         For I = 0 To CInt(GD.nRB - 1) Step RBStep
      '            sNodeRB1 = .ColumnIncPCD(I).eNode
      '            sNodeRB2 = .ColumnIncPCD(I).sNode
      '            eNodeRB1 = SSI(intPCD + 1).ColumnIncPCD(I).sNode
      '            eNodeRB2 = SSI(intPCD + 1).ColumnIncPCD(I).eNode

      '            SeqNodeNum = SeqNodeNum + 1
      '            GetIntersectionNode(sNodeRB1, eNodeRB1, sNodeRB2, eNodeRB2, mNodeRB)
      '            With mNodeRB
      '               .No = SeqNodeNum
      '               OSGeometry.CreateNode(.No, .x, .y, .z)
      '            End With
      '            Call CreateNewBeam(SeqPlateNum, SSI(intPCD).RBIncPCD, sNodeRB1.No, mNodeRB.No, sRelMyMz, eRelMyMz)
      '            Call CreateNewBeam(SeqPlateNum, SSI(intPCD).RBIncPCD, mNodeRB.No, eNodeRB1.No, sRelMyMz, eRelMyMz)
      '            Call CreateNewBeam(SeqPlateNum, SSI(intPCD).RBIncPCD, sNodeRB2.No, mNodeRB.No, sRelMyMz)
      '            Call CreateNewBeam(SeqPlateNum, SSI(intPCD).RBIncPCD, mNodeRB.No, eNodeRB2.No, , eRelMyMz)
      '         Next I
      '         ReDim Preserve .RBIncPCD(UBound(.RBIncPCD) - 1)
      '         ReDim Preserve .grpRBPCD(UBound(.RBIncPCD))
      '         For I = 0 To UBound(.RBIncPCD)
      '            .grpRBPCD(I) = CInt(.RBIncPCD(I).No)
      '         Next I
      '         OSGeometry.CreateGroupEx(GroupType.Beams, "_RBRACE_" & .Desc, UBound(.grpRBPCD) + 1, .grpRBPCD)
      '         dbRB(intPCD).nTableID = CType(GetTableID(dbRB(intPCD).dbName), Country)
      '         .RBSec = dbRB(intPCD)
      '         CreateSTAADMemberProperty(.RBSec)
      '         OSProperty.AssignBeamProperty(.grpRBPCD, .RBSec.ID)
      '         With .RBSec.mData
      '            .ID = CInt(OSProperty.CreateIsotropicMaterialProperties(.STAADName, .E, .Poisson, .G, .Density, .Alpha, .CrDamp))
      '         End With
      '         AssignMaterialPropertyToBeams(.grpRBPCD, .RBSec.mData.STAADName)
      '         'AssignMaterialPropertyToBeams(.grpRBPCD, A53GrB.Grade)
      '      End If
      '   End With
      'Next intPCD
      '' End of Radial Bracings Generation
      'If HasCompRing Then
      '   Call TG.GenerateCompMesh(Nodes.CR, SSI(UBound(SSI)).NodesPCD, Groups.CR, SeqNodeNum, SeqPlateNum, Component.CompRing)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_CR_TOP", UBound(Groups.CR) + 1, Groups.CR)

      '   Call TG.GenerateCompMesh(Nodes.UF, Nodes.CR, Groups.UF, SeqNodeNum, SeqPlateNum, Component.UFCone)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_UFCONE", UBound(Groups.UF) + 1, Groups.UF)

      '   If HasCRVertAnnulus Then
      '      Call TG.GenerateCompMesh(Nodes.CRVertAnnulus, SSI(UBound(SSI)).NodesPCD, Groups.CRVertAnnulus, SeqNodeNum, SeqPlateNum, Component.CRVertAnnulus)
      '      OSGeometry.CreateGroupEx(GroupType.Plates, "_CR_VRING", UBound(Groups.CRVertAnnulus) + 1, Groups.CRVertAnnulus)
      '      If HasCRStiffener Then
      '         Call TG.GenerateVAStiffenerMesh(Nodes.CRVAS, Nodes.CR, Nodes.CRVertAnnulus, Nodes.UF, Groups.CRVAS, SeqNodeNum, SeqPlateNum)
      '         OSGeometry.CreateGroupEx(GroupType.Plates, "_CR_VAS", UBound(Groups.CRVAS) + 1, Groups.CRVAS)
      '      End If
      '   End If
      'Else
      '   Call TG.GenerateCompMesh(Nodes.UF, SSI(UBound(SSI)).NodesPCD, Groups.UF, SeqNodeNum, SeqPlateNum, Component.UFCone)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_UFCONE", UBound(Groups.UF) + 1, Groups.UF)
      'End If

      '' Check and Generate the Optional Components
      'If HasCCBottom Then
      '   Call TG.GenerateCompMesh(Nodes.CCBot, Nodes.UF, Groups.CCBot, SeqNodeNum, SeqPlateNum, Component.CCBot)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_CC_BOT", UBound(Groups.CCBot) + 1, Groups.CCBot)
      'End If
      'If HasCCTop Then
      '   Call TG.GenerateCompMesh(Nodes.CCTop, Nodes.UF, Groups.CCTop, SeqNodeNum, SeqPlateNum, Component.CCTop)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_CC_TOP", UBound(Groups.CCTop) + 1, Groups.CCTop)
      'End If
      'If HasCCBottomBasePlate Then
      '   Call TG.GenerateCompMesh(Nodes.BPOuter, Nodes.CCBot, Groups.BPOuter, SeqNodeNum, SeqPlateNum, Component.BPOuter, GD.pcdBolts, GD.dCC)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_BP_OUTER", UBound(Groups.BPOuter) + 1, Groups.BPOuter)

      '   Call TG.GenerateCompMesh(Nodes.BPBolts, Nodes.BPOuter, Groups.BPBolts, SeqNodeNum, SeqPlateNum, Component.BPBolts, GD.odBP, GD.pcdBolts)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_BP_BOLTS", UBound(Groups.BPBolts) + 1, Groups.BPBolts)

      '   ' This block generates the supports for centre column base plate
      '   Dim CCSupport As SupportCondition, CCSupportNum As Integer, iBolt As Integer, BoltStep As Integer, grpCCSupNodes() As Long
      '   With CCSupport
      '      ReDim .Rel(0 To 5) : ReDim .Spr(0 To 5)
      '      .Rel(0) = Math.Abs(CDbl(Not chkTxCC.Checked)) : .Rel(1) = Math.Abs(CDbl(Not chkTyCC.Checked)) : .Rel(2) = Math.Abs(CDbl(Not chkTzCC.Checked))
      '      .Rel(3) = Math.Abs(CDbl(Not chkRxCC.Checked)) : .Rel(4) = Math.Abs(CDbl(Not chkRyCC.Checked)) : .Rel(5) = Math.Abs(CDbl(Not chkRzCC.Checked))
      '      .Spr(0) = 0.0# : .Spr(1) = 0.0# : .Spr(2) = 0.0# : .Spr(3) = 0.0# : .Spr(4) = 0.0# : .Spr(5) = 0.0#
      '      CCSupportNum = CInt(OSSupport.CreateSupportFixedBut(.Rel, .Spr))
      '   End With
      '   If (UBound(Nodes.BPBolts(0).Nodes) Mod CInt(Val(Me.txtnBoltsCC.Text)) = 0) Then
      '      BoltStep = CInt(UBound(Nodes.BPBolts(0).Nodes) / CInt(Val(Me.txtnBoltsCC.Text)))
      '      ReDim grpCCSupNodes(0)
      '      For iBolt = 0 To UBound(Nodes.BPBolts(0).Nodes) - 1 Step BoltStep
      '         grpCCSupNodes(UBound(grpCCSupNodes)) = Nodes.BPBolts(0).Nodes(iBolt).No
      '         OSSupport.AssignSupportToNode(Nodes.BPBolts(0).Nodes(iBolt).No, CCSupportNum)
      '         ReDim Preserve grpCCSupNodes(UBound(grpCCSupNodes) + 1)
      '      Next iBolt
      '      ReDim Preserve grpCCSupNodes(UBound(grpCCSupNodes) - 1)
      '      OSGeometry.CreateGroupEx(GroupType.Nodes, "_SUP_CC", UBound(grpCCSupNodes) + 1, grpCCSupNodes)
      '   Else
      '      MsgBox("Error: CC supports cannot be generated. Number of CC bolts is NOT divisible with number of available nodes. " & vbCrLf & _
      '              "Total number of nodes available are : " & UBound(Nodes.BPBolts(0).Nodes) & ". Please revise the number of bolts in the input.", vbOKOnly, "Cannot generate the CC supports")
      '   End If
      '   ' End of centre column supports generation
      '   Call TG.GenerateCompMesh(Nodes.BPInner, Nodes.CCBot, Groups.BPInner, SeqNodeNum, SeqPlateNum, Component.BPInner, GD.dCC, GD.idBP)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_BP_INNER", UBound(Groups.BPInner) + 1, Groups.BPInner)
      'End If
      'If HasCCBottomCapPlate Then
      '   Call TG.GenerateCompMesh(Nodes.CPInnerBottom, Nodes.UF, Groups.CPInnerBottom, SeqNodeNum, SeqPlateNum, Component.CPInner, GD.dCC, GD.idCP)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_CP_INNER", UBound(Groups.CPInnerBottom) + 1, Groups.CPInnerBottom)
      'End If
      'If HasCCTopCapPlate Then
      '   Call TG.GenerateCompMesh(Nodes.CPOuter, Nodes.CCTop, Groups.CPOuter, SeqNodeNum, SeqPlateNum, Component.CPOuter, GD.odCP, GD.dCC)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_CP_OUTER", UBound(Groups.CPOuter) + 1, Groups.CPOuter)

      '   Call TG.GenerateCompMesh(Nodes.CPInner, Nodes.CCTop, Groups.CPInner, SeqNodeNum, SeqPlateNum, Component.CPInner, GD.dCC, GD.idCP)
      '   OSGeometry.CreateGroupEx(GroupType.Plates, "_CP_INNER", UBound(Groups.CPInner) + 1, Groups.CPInner)
      'End If

      ' Terminate the instantiated OpenSTAAD objects
      TG = Nothing
      TerminateOSObjects()
   End Sub
   Private Sub AssignMaterialPropertyToBeams(ByRef beams() As Integer, ByVal MatName As String)
      For Each iBeam As Integer In beams
         OSProperty.AssignMaterialToMember(MatName, iBeam)
      Next
   End Sub
   Private Sub CreateSTAADMemberProperty(ByRef mprop As PropertyData)
      With mprop
         .nTableID = CType(GetTableID(.dbName), Country)
         Select Case .shape
            Case "I"
               .ID = CInt(OSProperty.CreateBeamPropertyFromTable(.nTableID, .name, .type, .addSpec1, .addSpec2))
            Case "C"
               .ID = CInt(OSProperty.CreateChannelPropertyFromTable(.nTableID, .name, .type, .addSpec1))
            Case "L"
               .ID = CInt(OSProperty.CreateAnglePropertyFromTable(.nTableID, .name, .type, .addSpec1))
            Case "Tube"
               .ID = CInt(OSProperty.CreateTubePropertyFromTable(.nTableID, .name, TypeSpec.ST, 0.0, 0.0, 0.0))
            Case "CHS"
               .ID = CInt(OSProperty.CreatePipePropertyFromTable(.nTableID, .name, TypeSpec.ST, 0.0, 0.0))
         End Select
      End With
   End Sub
   Private Sub CreateNewBeam(ByRef SeqPlateNum As Long,
                             ByRef BeamIncPCD() As Beam,
                             ByVal sNode As Long,
                             ByVal eNode As Long,
                             Optional ByVal sRelease As Long = -1,
                             Optional ByVal eRelease As Long = -1)
      SeqPlateNum = SeqPlateNum + 1
      With BeamIncPCD(UBound(BeamIncPCD))
         .No = SeqPlateNum : .sNode.No = sNode : .eNode.No = eNode
         OSGeometry.CreateBeam(.No, .sNode.No, .eNode.No)
         If sRelease <> (-1) Then OSProperty.AssignMemberSpecToBeam(SeqPlateNum, sRelease)
         If eRelease <> (-1) Then OSProperty.AssignMemberSpecToBeam(SeqPlateNum, eRelease)
      End With
      ReDim Preserve BeamIncPCD(UBound(BeamIncPCD) + 1)
   End Sub
   '/---------------- Code to be modified - ends here ---------------/
   Private Sub InitializePropertyData() Handles txtnColRB.TextChanged
      ReDim dbCols(0 To CInt(Val(Me.txtnColRB.Text)) - 1)
      ReDim dbCB(0 To CInt(Val(Me.txtnColRB.Text)) - 1)
      ReDim dbRB(0 To CInt(Val(Me.txtnColRB.Text)) - 1)
   End Sub
   Private Sub dgvSupportStructure_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSupportStructure.CellClick
      If (e.RowIndex = -1) Then Exit Sub
      If (e.RowIndex = (dgvSupportStructure.RowCount - 1)) And (e.ColumnIndex = indRB) Then
         MsgBox("Radial Bracings cannot be provided at the innermost PCD and hence the option is disabled.")
         Exit Sub
      End If
      If Not IsSecPickerLocked Then secPicker = New frmSectionPicker(SecPickCallCategory.Coupled)
      With Me.dgvSupportStructure
         If e.ColumnIndex = indColSec Then
            If dbCols(e.RowIndex).name <> "" AndAlso dbCols(e.RowIndex).name <> CHOOSE_SEC Then secPicker.PropertyData = dbCols(e.RowIndex)
            secPicker.ShowDialog()
            dbCols(e.RowIndex) = secPicker.PropertyData
            With dbCols(e.RowIndex)
               If .name <> "" Then .nTableID = CType(GetTableID(.dbName), Country)
            End With
            .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dbCols(e.RowIndex).name
         End If
         If e.ColumnIndex = indCBSec Then
            If CBool(.Rows(e.RowIndex).Cells(indCB).Value) Then
               If dbCB(e.RowIndex).name <> "" AndAlso dbCB(e.RowIndex).name <> CHOOSE_SEC Then secPicker.PropertyData = dbCB(e.RowIndex)
               secPicker.ShowDialog()
               dbCB(e.RowIndex) = secPicker.PropertyData
               With dbCB(e.RowIndex)
                  If .name <> "" Then .nTableID = CType(GetTableID(.dbName), Country)
               End With
               .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dbCB(e.RowIndex).name
            End If
         End If
         If e.ColumnIndex = indRBSec Then
            If CBool(.Rows(e.RowIndex).Cells(indRB).Value) Then
               If dbRB(e.RowIndex).name <> "" AndAlso dbRB(e.RowIndex).name <> CHOOSE_SEC Then secPicker.PropertyData = dbRB(e.RowIndex)
               secPicker.ShowDialog()
               dbRB(e.RowIndex) = secPicker.PropertyData
               With dbRB(e.RowIndex)
                  If .name <> "" Then .nTableID = CType(GetTableID(.dbName), Country)
               End With
               .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dbRB(e.RowIndex).name
            End If
         End If
      End With
   End Sub
   Private Sub CellValueChange(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSupportStructure.CellValueChanged
      If IsThickenerGeneratorLocked AndAlso (dgvSupportStructure.RowCount > 1) Then
         With Me.dgvSupportStructure
            If e.ColumnIndex = indCB Then
               If CBool(.Rows(e.RowIndex).Cells(indCB).Value) Then
                  .Rows(e.RowIndex).Cells(indCBSec).Value = ">>"
               Else
                  .Rows(e.RowIndex).Cells(indCBSec).Value = ""
               End If
            End If
            If e.ColumnIndex = indRB Then
               If CBool(.Rows(e.RowIndex).Cells(indRB).Value) Then
                  .Rows(e.RowIndex).Cells(indRBSec).Value = ">>"
               Else
                  .Rows(e.RowIndex).Cells(indRBSec).Value = ""
               End If
            End If
         End With
      End If
   End Sub
   '/------ Ends Edit Mode So CellValueChanged Event Can be Triggered ----------/
   Private Sub EndEditMode(sender As Object, e As EventArgs) Handles dgvSupportStructure.CurrentCellDirtyStateChanged
      '/------ If current cell of grid is dirty, commits edit -------------/
      If dgvSupportStructure.IsCurrentCellDirty Then
         dgvSupportStructure.CommitEdit(DataGridViewDataErrorContexts.Commit)
      End If
   End Sub
   Private ReadOnly Property GetTableID(ByVal dbName As String) As Integer
      Get
         Select Case dbName
            Case "US Metric", "US Customary"
               _nTableID = Country.American
            Case "Australian"
               _nTableID = Country.Australian
            Case "British"
               _nTableID = Country.British
            Case "Canadian"
               _nTableID = Country.Canadian
            Case "Chinese"
               _nTableID = Country.Chinese
            Case "Dutch"
               _nTableID = Country.Dutch
            Case "European"
               _nTableID = Country.European
            Case "French"
               _nTableID = Country.French
            Case "German"
               _nTableID = Country.German
            Case "Indian"
               _nTableID = Country.Indian
            Case "Japanese"
               _nTableID = Country.Japanese
            Case "Russian"
               _nTableID = Country.Russian
            Case "South African"
               _nTableID = Country.SouthAfrican
            Case "Spanish"
               _nTableID = Country.Spanish
            Case "Venezuelan"
               _nTableID = Country.Venezuelan
            Case "Korean"
               _nTableID = Country.Korean
            Case "Aluminium"
               _nTableID = Country.Aluminium
            Case "US Cold Formed"
               _nTableID = Country.USColdFormed
            Case "IS Colde Formed"
               _nTableID = Country.ISColdFormed
         End Select
         Return _nTableID
      End Get
   End Property
   Private Sub btnRBSection_Click(sender As Object, e As EventArgs) Handles btnRBSection.Click
      If Not IsSecPickerLocked Then secPicker = New frmSectionPicker(SecPickCallCategory.Coupled)
      If RadBeam.name <> "" AndAlso RadBeam.name <> CHOOSE_SEC Then secPicker.PropertyData = RadBeam
      secPicker.ShowDialog()
      RadBeam = secPicker.PropertyData
      With RadBeam
         If .name <> "" Then .nTableID = CType(GetTableID(.dbName), Country)
      End With
      Me.btnRBSection.Text = RadBeam.name
   End Sub
End Class