<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainInterface
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainInterface))
        Me.frmMainMenu = New System.Windows.Forms.MenuStrip()
        Me.msiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiFileNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiFileSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiFileSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.msiFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiEditCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiEditCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.msiEditPreferences = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiToolsSecPicker = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiHelpManual = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiHelpRequestTrial = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.msiHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.sfdSaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.panelDeveloper = New System.Windows.Forms.Panel()
        Me.lblDeveloper = New System.Windows.Forms.Label()
        Me.frmMainMenu.SuspendLayout()
        Me.panelDeveloper.SuspendLayout()
        Me.SuspendLayout()
        '
        'frmMainMenu
        '
        Me.frmMainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiFile, Me.msiEdit, Me.msiTools, Me.msiHelp})
        Me.frmMainMenu.Location = New System.Drawing.Point(0, 0)
        Me.frmMainMenu.Name = "frmMainMenu"
        Me.frmMainMenu.Size = New System.Drawing.Size(883, 24)
        Me.frmMainMenu.TabIndex = 1
        Me.frmMainMenu.Text = "Main Menu"
        '
        'msiFile
        '
        Me.msiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiFileNew, Me.msiFileOpen, Me.msiFileSave, Me.msiFileSaveAs, Me.ToolStripMenuItem2, Me.msiFileExit})
        Me.msiFile.Name = "msiFile"
        Me.msiFile.Size = New System.Drawing.Size(37, 20)
        Me.msiFile.Text = "&File"
        '
        'msiFileNew
        '
        Me.msiFileNew.Image = Global.SES.My.Resources.Resources.xtg
        Me.msiFileNew.Name = "msiFileNew"
        Me.msiFileNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.msiFileNew.Size = New System.Drawing.Size(195, 22)
        Me.msiFileNew.Text = "&New Thickener"
        '
        'msiFileOpen
        '
        Me.msiFileOpen.Image = Global.SES.My.Resources.Resources.folder
        Me.msiFileOpen.Name = "msiFileOpen"
        Me.msiFileOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.msiFileOpen.Size = New System.Drawing.Size(195, 22)
        Me.msiFileOpen.Text = "&Open"
        '
        'msiFileSave
        '
        Me.msiFileSave.Enabled = False
        Me.msiFileSave.Image = CType(resources.GetObject("msiFileSave.Image"), System.Drawing.Image)
        Me.msiFileSave.Name = "msiFileSave"
        Me.msiFileSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.msiFileSave.Size = New System.Drawing.Size(195, 22)
        Me.msiFileSave.Text = "&Save"
        '
        'msiFileSaveAs
        '
        Me.msiFileSaveAs.Enabled = False
        Me.msiFileSaveAs.Name = "msiFileSaveAs"
        Me.msiFileSaveAs.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.msiFileSaveAs.Size = New System.Drawing.Size(195, 22)
        Me.msiFileSaveAs.Text = "Save &As..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(192, 6)
        '
        'msiFileExit
        '
        Me.msiFileExit.Name = "msiFileExit"
        Me.msiFileExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.msiFileExit.Size = New System.Drawing.Size(195, 22)
        Me.msiFileExit.Text = "E&xit"
        '
        'msiEdit
        '
        Me.msiEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiEditCopy, Me.msiEditCut, Me.ToolStripMenuItem1, Me.msiEditPreferences})
        Me.msiEdit.Name = "msiEdit"
        Me.msiEdit.Size = New System.Drawing.Size(39, 20)
        Me.msiEdit.Text = "&Edit"
        '
        'msiEditCopy
        '
        Me.msiEditCopy.Enabled = False
        Me.msiEditCopy.Image = Global.SES.My.Resources.Resources.copy
        Me.msiEditCopy.Name = "msiEditCopy"
        Me.msiEditCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.msiEditCopy.Size = New System.Drawing.Size(144, 22)
        Me.msiEditCopy.Text = "Co&py"
        '
        'msiEditCut
        '
        Me.msiEditCut.Enabled = False
        Me.msiEditCut.Image = Global.SES.My.Resources.Resources.cut
        Me.msiEditCut.Name = "msiEditCut"
        Me.msiEditCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.msiEditCut.Size = New System.Drawing.Size(144, 22)
        Me.msiEditCut.Text = "Cu&t"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(141, 6)
        '
        'msiEditPreferences
        '
        Me.msiEditPreferences.Enabled = False
        Me.msiEditPreferences.Image = Global.SES.My.Resources.Resources.settings
        Me.msiEditPreferences.Name = "msiEditPreferences"
        Me.msiEditPreferences.Size = New System.Drawing.Size(144, 22)
        Me.msiEditPreferences.Text = "P&references"
        '
        'msiTools
        '
        Me.msiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiToolsSecPicker})
        Me.msiTools.Name = "msiTools"
        Me.msiTools.Size = New System.Drawing.Size(46, 20)
        Me.msiTools.Text = "&Tools"
        '
        'msiToolsSecPicker
        '
        Me.msiToolsSecPicker.Image = Global.SES.My.Resources.Resources.pencil
        Me.msiToolsSecPicker.Name = "msiToolsSecPicker"
        Me.msiToolsSecPicker.Size = New System.Drawing.Size(148, 22)
        Me.msiToolsSecPicker.Text = "Section Pic&ker"
        '
        'msiHelp
        '
        Me.msiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiHelpManual, Me.msiHelpRequestTrial, Me.ToolStripMenuItem3, Me.msiHelpAbout})
        Me.msiHelp.Name = "msiHelp"
        Me.msiHelp.Size = New System.Drawing.Size(44, 20)
        Me.msiHelp.Text = "&Help"
        '
        'msiHelpManual
        '
        Me.msiHelpManual.Enabled = False
        Me.msiHelpManual.Name = "msiHelpManual"
        Me.msiHelpManual.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.msiHelpManual.Size = New System.Drawing.Size(182, 22)
        Me.msiHelpManual.Text = "&Manual"
        '
        'msiHelpRequestTrial
        '
        Me.msiHelpRequestTrial.Enabled = False
        Me.msiHelpRequestTrial.Name = "msiHelpRequestTrial"
        Me.msiHelpRequestTrial.Size = New System.Drawing.Size(182, 22)
        Me.msiHelpRequestTrial.Text = "Request &Trial License"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(179, 6)
        '
        'msiHelpAbout
        '
        Me.msiHelpAbout.Enabled = False
        Me.msiHelpAbout.Name = "msiHelpAbout"
        Me.msiHelpAbout.Size = New System.Drawing.Size(182, 22)
        Me.msiHelpAbout.Text = "Abou&t"
        '
        'panelDeveloper
        '
        Me.panelDeveloper.BackColor = System.Drawing.SystemColors.Control
        Me.panelDeveloper.Controls.Add(Me.lblDeveloper)
        Me.panelDeveloper.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelDeveloper.ForeColor = System.Drawing.SystemColors.Control
        Me.panelDeveloper.Location = New System.Drawing.Point(0, 539)
        Me.panelDeveloper.Name = "panelDeveloper"
        Me.panelDeveloper.Size = New System.Drawing.Size(883, 22)
        Me.panelDeveloper.TabIndex = 3
        '
        'lblDeveloper
        '
        Me.lblDeveloper.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeveloper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDeveloper.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblDeveloper.Location = New System.Drawing.Point(0, 0)
        Me.lblDeveloper.Name = "lblDeveloper"
        Me.lblDeveloper.Size = New System.Drawing.Size(883, 22)
        Me.lblDeveloper.TabIndex = 0
        Me.lblDeveloper.Text = "Developed by Srinivasa Rao Masanam"
        Me.lblDeveloper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMainInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 561)
        Me.Controls.Add(Me.panelDeveloper)
        Me.Controls.Add(Me.frmMainMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.frmMainMenu
        Me.Name = "frmMainInterface"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Structural Engineering Solutions"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.frmMainMenu.ResumeLayout(False)
        Me.frmMainMenu.PerformLayout()
        Me.panelDeveloper.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
   Friend WithEvents frmMainMenu As System.Windows.Forms.MenuStrip
   Friend WithEvents msiFile As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiFileNew As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents msiFileExit As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiHelp As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiFileSave As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiFileSaveAs As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiFileOpen As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiHelpManual As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiHelpAbout As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents sfdSaveFile As System.Windows.Forms.SaveFileDialog
   Friend WithEvents msiEdit As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiEditCopy As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiEditCut As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents msiEditPreferences As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiTools As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiToolsSecPicker As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents msiHelpRequestTrial As System.Windows.Forms.ToolStripMenuItem
   Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
   Friend WithEvents panelDeveloper As System.Windows.Forms.Panel
   Friend WithEvents lblDeveloper As System.Windows.Forms.Label

End Class
