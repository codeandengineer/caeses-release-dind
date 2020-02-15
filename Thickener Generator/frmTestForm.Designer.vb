<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestForm
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
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTestForm))
      Me.btnGetOS = New System.Windows.Forms.Button()
      Me.SuspendLayout()
      '
      'btnGetOS
      '
      Me.btnGetOS.Location = New System.Drawing.Point(12, 226)
      Me.btnGetOS.Name = "btnGetOS"
      Me.btnGetOS.Size = New System.Drawing.Size(357, 23)
      Me.btnGetOS.TabIndex = 0
      Me.btnGetOS.Text = "Get OpenSTAAD"
      Me.btnGetOS.UseVisualStyleBackColor = True
      '
      'frmTestForm
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(381, 261)
      Me.Controls.Add(Me.btnGetOS)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.Name = "frmTestForm"
      Me.Text = "Form for Testing OpenSTAAD"
      Me.ResumeLayout(False)

   End Sub
   Friend WithEvents btnGetOS As System.Windows.Forms.Button
End Class
