Imports System.Net.Mail
Public Class noLicense
   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      Me.Close()
   End Sub
   Private Sub btnRequest_Click(sender As Object, e As EventArgs) Handles btnRequest.Click
      Dim comEmail As composeEmail
      comEmail = New composeEmail
      comEmail.ShowDialog()
   End Sub
   Private Sub lnkEmail_LinkClicked(sender As Object, e As Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkEmail.LinkClicked
      ''Dim emailMessage As New MailMessage
      ''Try
      ''   With emailMessage
      ''      .From = New MailAddress("sriinivas.masanam@outlook.com")
      ''      .To.Add("nivas.re@gmail.com")
      ''      .Subject = "SES - Request for evaluation license"
      ''      .Body = "Please provide us an evaluation license."
      ''      Dim smtp As New SmtpClient("smtp.gmail.com")
      ''      smtp.Port = 587
      ''      smtp.EnableSsl = True
      ''      smtp.Credentials = New System.Net.NetworkCredential("srinivasarao.masanam@outlook.com", "Advitha2634")
      ''      smtp.Send(emailMessage)
      ''   End With
      ''Catch ex As Exception
      ''   MsgBox(ex.Message)
      ''End Try
      'System.Diagnostics.Process.Start("mailto:administrator@forum.vsquarece.in")
      'Me.Close()
   End Sub

   'Private Sub noLicense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
   '   Me.Text = "License Verification - " & PUBLISHER_NAME
   'End Sub
End Class