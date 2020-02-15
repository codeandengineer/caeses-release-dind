<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class composeEmail
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
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(composeEmail))
      Me.lblName = New System.Windows.Forms.Label()
      Me.lblSubject = New System.Windows.Forms.Label()
      Me.lblInputFile = New System.Windows.Forms.Label()
      Me.txtInputFile = New System.Windows.Forms.TextBox()
      Me.txtSubject = New System.Windows.Forms.TextBox()
      Me.txtFromEmail = New System.Windows.Forms.TextBox()
      Me.lblMessage = New System.Windows.Forms.Label()
      Me.txtToEmail = New System.Windows.Forms.TextBox()
      Me.btnAttach = New System.Windows.Forms.Button()
      Me.btnGenerateInput = New System.Windows.Forms.Button()
      Me.btnCancel = New System.Windows.Forms.Button()
      Me.btnSend = New System.Windows.Forms.Button()
      Me.gbCompose = New System.Windows.Forms.GroupBox()
      Me.lblFromEmail = New System.Windows.Forms.Label()
      Me.lblToEmail = New System.Windows.Forms.Label()
      Me.txtMessage = New System.Windows.Forms.TextBox()
      Me.txtName = New System.Windows.Forms.TextBox()
      Me.lblDesc = New System.Windows.Forms.Label()
      Me.ttLicenseRequest = New System.Windows.Forms.ToolTip(Me.components)
      Me.gbCompose.SuspendLayout()
      Me.SuspendLayout()
      '
      'lblName
      '
      Me.lblName.AutoSize = True
      Me.lblName.Location = New System.Drawing.Point(16, 26)
      Me.lblName.Name = "lblName"
      Me.lblName.Size = New System.Drawing.Size(38, 13)
      Me.lblName.TabIndex = 0
      Me.lblName.Text = "Name:"
      '
      'lblSubject
      '
      Me.lblSubject.AutoSize = True
      Me.lblSubject.Location = New System.Drawing.Point(16, 130)
      Me.lblSubject.Name = "lblSubject"
      Me.lblSubject.Size = New System.Drawing.Size(46, 13)
      Me.lblSubject.TabIndex = 9
      Me.lblSubject.Text = "Subject:"
      '
      'lblInputFile
      '
      Me.lblInputFile.AutoSize = True
      Me.lblInputFile.Location = New System.Drawing.Point(16, 104)
      Me.lblInputFile.Name = "lblInputFile"
      Me.lblInputFile.Size = New System.Drawing.Size(50, 13)
      Me.lblInputFile.TabIndex = 6
      Me.lblInputFile.Text = "Input file:"
      '
      'txtInputFile
      '
      Me.txtInputFile.Location = New System.Drawing.Point(86, 101)
      Me.txtInputFile.Name = "txtInputFile"
      Me.txtInputFile.Size = New System.Drawing.Size(311, 20)
      Me.txtInputFile.TabIndex = 7
      '
      'txtSubject
      '
      Me.txtSubject.Location = New System.Drawing.Point(86, 127)
      Me.txtSubject.Name = "txtSubject"
      Me.txtSubject.Size = New System.Drawing.Size(399, 20)
      Me.txtSubject.TabIndex = 10
      '
      'txtFromEmail
      '
      Me.txtFromEmail.Location = New System.Drawing.Point(86, 49)
      Me.txtFromEmail.Name = "txtFromEmail"
      Me.txtFromEmail.Size = New System.Drawing.Size(399, 20)
      Me.txtFromEmail.TabIndex = 3
      '
      'lblMessage
      '
      Me.lblMessage.AutoSize = True
      Me.lblMessage.Location = New System.Drawing.Point(16, 156)
      Me.lblMessage.Name = "lblMessage"
      Me.lblMessage.Size = New System.Drawing.Size(53, 13)
      Me.lblMessage.TabIndex = 11
      Me.lblMessage.Text = "Message:"
      '
      'txtToEmail
      '
      Me.txtToEmail.Enabled = False
      Me.txtToEmail.Location = New System.Drawing.Point(86, 75)
      Me.txtToEmail.Name = "txtToEmail"
      Me.txtToEmail.Size = New System.Drawing.Size(399, 20)
      Me.txtToEmail.TabIndex = 5
      '
      'btnAttach
      '
      Me.btnAttach.Location = New System.Drawing.Point(403, 99)
      Me.btnAttach.Name = "btnAttach"
      Me.btnAttach.Size = New System.Drawing.Size(82, 23)
      Me.btnAttach.TabIndex = 8
      Me.btnAttach.Text = "A&ttach"
      Me.btnAttach.UseVisualStyleBackColor = True
      '
      'btnGenerateInput
      '
      Me.btnGenerateInput.Location = New System.Drawing.Point(86, 309)
      Me.btnGenerateInput.Name = "btnGenerateInput"
      Me.btnGenerateInput.Size = New System.Drawing.Size(132, 23)
      Me.btnGenerateInput.TabIndex = 13
      Me.btnGenerateInput.Text = "Generate Input File"
      Me.btnGenerateInput.UseVisualStyleBackColor = True
      '
      'btnCancel
      '
      Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.btnCancel.Location = New System.Drawing.Point(381, 309)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.Size = New System.Drawing.Size(104, 23)
      Me.btnCancel.TabIndex = 15
      Me.btnCancel.Text = "&Cancel"
      Me.btnCancel.UseVisualStyleBackColor = True
      '
      'btnSend
      '
      Me.btnSend.Enabled = False
      Me.btnSend.Location = New System.Drawing.Point(273, 309)
      Me.btnSend.Name = "btnSend"
      Me.btnSend.Size = New System.Drawing.Size(102, 23)
      Me.btnSend.TabIndex = 14
      Me.btnSend.Text = "&Send"
      Me.btnSend.UseVisualStyleBackColor = True
      '
      'gbCompose
      '
      Me.gbCompose.Controls.Add(Me.btnCancel)
      Me.gbCompose.Controls.Add(Me.lblName)
      Me.gbCompose.Controls.Add(Me.lblSubject)
      Me.gbCompose.Controls.Add(Me.btnSend)
      Me.gbCompose.Controls.Add(Me.lblMessage)
      Me.gbCompose.Controls.Add(Me.btnGenerateInput)
      Me.gbCompose.Controls.Add(Me.lblFromEmail)
      Me.gbCompose.Controls.Add(Me.btnAttach)
      Me.gbCompose.Controls.Add(Me.lblInputFile)
      Me.gbCompose.Controls.Add(Me.txtToEmail)
      Me.gbCompose.Controls.Add(Me.txtFromEmail)
      Me.gbCompose.Controls.Add(Me.lblToEmail)
      Me.gbCompose.Controls.Add(Me.txtInputFile)
      Me.gbCompose.Controls.Add(Me.txtMessage)
      Me.gbCompose.Controls.Add(Me.txtName)
      Me.gbCompose.Controls.Add(Me.txtSubject)
      Me.gbCompose.Location = New System.Drawing.Point(38, 61)
      Me.gbCompose.Name = "gbCompose"
      Me.gbCompose.Size = New System.Drawing.Size(505, 350)
      Me.gbCompose.TabIndex = 1
      Me.gbCompose.TabStop = False
      Me.gbCompose.Text = "Compose Message"
      '
      'lblFromEmail
      '
      Me.lblFromEmail.AutoSize = True
      Me.lblFromEmail.Location = New System.Drawing.Point(16, 52)
      Me.lblFromEmail.Name = "lblFromEmail"
      Me.lblFromEmail.Size = New System.Drawing.Size(33, 13)
      Me.lblFromEmail.TabIndex = 2
      Me.lblFromEmail.Text = "From:"
      '
      'lblToEmail
      '
      Me.lblToEmail.AutoSize = True
      Me.lblToEmail.Location = New System.Drawing.Point(16, 78)
      Me.lblToEmail.Name = "lblToEmail"
      Me.lblToEmail.Size = New System.Drawing.Size(23, 13)
      Me.lblToEmail.TabIndex = 4
      Me.lblToEmail.Text = "To:"
      '
      'txtMessage
      '
      Me.txtMessage.Location = New System.Drawing.Point(86, 153)
      Me.txtMessage.Multiline = True
      Me.txtMessage.Name = "txtMessage"
      Me.txtMessage.Size = New System.Drawing.Size(399, 150)
      Me.txtMessage.TabIndex = 12
      '
      'txtName
      '
      Me.txtName.Location = New System.Drawing.Point(86, 23)
      Me.txtName.Name = "txtName"
      Me.txtName.Size = New System.Drawing.Size(399, 20)
      Me.txtName.TabIndex = 1
      '
      'lblDesc
      '
      Me.lblDesc.AutoSize = True
      Me.lblDesc.Location = New System.Drawing.Point(35, 22)
      Me.lblDesc.MaximumSize = New System.Drawing.Size(530, 0)
      Me.lblDesc.Name = "lblDesc"
      Me.lblDesc.Size = New System.Drawing.Size(526, 26)
      Me.lblDesc.TabIndex = 0
      Me.lblDesc.Text = "Please complete all the required information and send the email along with the at" & _
    "tachment in order to generate the license file."
      '
      'composeEmail
      '
      Me.AcceptButton = Me.btnSend
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.CancelButton = Me.btnCancel
      Me.ClientSize = New System.Drawing.Size(584, 441)
      Me.Controls.Add(Me.lblDesc)
      Me.Controls.Add(Me.gbCompose)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Name = "composeEmail"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "Compose Email - License Request"
      Me.gbCompose.ResumeLayout(False)
      Me.gbCompose.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents lblName As System.Windows.Forms.Label
   Friend WithEvents lblSubject As System.Windows.Forms.Label
   Friend WithEvents lblInputFile As System.Windows.Forms.Label
   Friend WithEvents txtInputFile As System.Windows.Forms.TextBox
   Friend WithEvents txtSubject As System.Windows.Forms.TextBox
   Friend WithEvents txtFromEmail As System.Windows.Forms.TextBox
   Friend WithEvents lblMessage As System.Windows.Forms.Label
   Friend WithEvents txtToEmail As System.Windows.Forms.TextBox
   Friend WithEvents btnAttach As System.Windows.Forms.Button
   Friend WithEvents btnGenerateInput As System.Windows.Forms.Button
   Friend WithEvents btnCancel As System.Windows.Forms.Button
   Friend WithEvents btnSend As System.Windows.Forms.Button
   Friend WithEvents gbCompose As System.Windows.Forms.GroupBox
   Friend WithEvents lblToEmail As System.Windows.Forms.Label
   Friend WithEvents lblFromEmail As System.Windows.Forms.Label
   Friend WithEvents txtMessage As System.Windows.Forms.TextBox
   Friend WithEvents txtName As System.Windows.Forms.TextBox
   Friend WithEvents lblDesc As System.Windows.Forms.Label
   Friend WithEvents ttLicenseRequest As System.Windows.Forms.ToolTip
End Class
