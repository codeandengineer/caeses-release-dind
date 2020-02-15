<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class noLicense
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
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(noLicense))
      Me.lblLicStatusDescription = New System.Windows.Forms.Label()
      Me.btnRequest = New System.Windows.Forms.Button()
      Me.btnCancel = New System.Windows.Forms.Button()
      Me.Label1 = New System.Windows.Forms.Label()
      Me.lnkEmail = New System.Windows.Forms.LinkLabel()
      Me.SuspendLayout()
      '
      'lblLicStatusDescription
      '
      Me.lblLicStatusDescription.AutoSize = True
      Me.lblLicStatusDescription.Location = New System.Drawing.Point(12, 23)
      Me.lblLicStatusDescription.Name = "lblLicStatusDescription"
      Me.lblLicStatusDescription.Size = New System.Drawing.Size(129, 13)
      Me.lblLicStatusDescription.TabIndex = 0
      Me.lblLicStatusDescription.Text = "License status description"
      '
      'btnRequest
      '
      Me.btnRequest.Location = New System.Drawing.Point(334, 113)
      Me.btnRequest.Name = "btnRequest"
      Me.btnRequest.Size = New System.Drawing.Size(115, 23)
      Me.btnRequest.TabIndex = 1
      Me.btnRequest.Text = "&Request Extension"
      Me.btnRequest.UseVisualStyleBackColor = True
      '
      'btnCancel
      '
      Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.btnCancel.Location = New System.Drawing.Point(455, 113)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.Size = New System.Drawing.Size(115, 23)
      Me.btnCancel.TabIndex = 2
      Me.btnCancel.Text = "&Close"
      Me.btnCancel.UseVisualStyleBackColor = True
      '
      'Label1
      '
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(12, 64)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(535, 13)
      Me.Label1.TabIndex = 0
      Me.Label1.Text = "Note: The main application has been terminated. You can request an evaluation lic" & _
    "ense at the following email id:"
      '
      'lnkEmail
      '
      Me.lnkEmail.AutoSize = True
      Me.lnkEmail.Location = New System.Drawing.Point(41, 86)
      Me.lnkEmail.Name = "lnkEmail"
      Me.lnkEmail.Size = New System.Drawing.Size(117, 13)
      Me.lnkEmail.TabIndex = 3
      Me.lnkEmail.TabStop = True
      Me.lnkEmail.Text = "ses.license@gmail.com"
      '
      'noLicense
      '
      Me.AcceptButton = Me.btnRequest
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.CancelButton = Me.btnCancel
      Me.ClientSize = New System.Drawing.Size(582, 148)
      Me.ControlBox = False
      Me.Controls.Add(Me.lnkEmail)
      Me.Controls.Add(Me.btnCancel)
      Me.Controls.Add(Me.btnRequest)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.lblLicStatusDescription)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Name = "noLicense"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "License Verification"
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents lblLicStatusDescription As System.Windows.Forms.Label
   Friend WithEvents btnRequest As System.Windows.Forms.Button
   Friend WithEvents btnCancel As System.Windows.Forms.Button
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents lnkEmail As System.Windows.Forms.LinkLabel
End Class
