Imports System.Net.Mail
Imports System.Text.RegularExpressions

Public Class composeEmail
   Private AES As IAESSecurity = New sesAESSecurity
   Private Const emailKey As String = "P7L03YZW+qjRlxy9TyXjlg==1wABx7RXl2iWT8Yz1RixGE5elWKS/UcNZuRt2OswOYs="
   Private Const emailValue As String = "TGJDIdZqjgf8GEwfKkIK2Q==1gIemnNbXc63b9IRq6v4bg=="
   Private Const emailSub As String = "Request for evaluation (or) trial license for SES"
   Private Const emailMes As String = "Hi," & vbCrLf & "Please arrange for an evaluation license for SES."
   Private Sub composeEmail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      'Me.Text = "Compose Email - License Request - " & PUBLISHER_NAME
      With Me
         .txtToEmail.Text = AES.AESDecCBC(emailKey)
         .txtSubject.Text = emailSub
         .txtMessage.Text = emailMes
      End With
      SetToolTips()
   End Sub

   Private Sub SetToolTips()
      Me.ttLicenseRequest.SetToolTip(Me.btnSend, "Sending direct email is disabled in the current version. Please send the email separately by attaching the input file.")
   End Sub

   Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
      If CheckComposeValidity() Then
         Try
            Dim SmtpServer As New SmtpClient
            Dim eMail As MailMessage
            With SmtpServer
               .UseDefaultCredentials = False
               .Credentials = New Net.NetworkCredential(AES.AESDecCBC(emailKey), AES.AESDecCBC(emailValue))
               .Port = 587
               .EnableSsl = True
               .Host = "smtp.gmail.com"
            End With
            eMail = New MailMessage
            With eMail
               .Attachments.Add(New Attachment(Me.txtInputFile.Text))
               .From = New MailAddress(Me.txtFromEmail.Text)
               .To.Add(Me.txtToEmail.Text)
               .Subject = Me.txtSubject.Text
               .Body = Me.txtMessage.Text
               .Body = .Body & vbCrLf & _
                        "Name: " & Me.txtName.Text & vbCrLf & _
                        "From: " & Me.txtFromEmail.Text
            End With
            SmtpServer.Send(eMail)
            Me.btnCancel.Text = "&Close"
            MsgBox("Email sent successfully. Thank you for contacting us. We will revert to you shortly.", MsgBoxStyle.OkOnly)
            Me.Close()
         Catch ex As Exception
            Me.btnCancel.Text = "&Cancel"
            MsgBox("Encountered an error" & vbCrLf & ex.Message, MsgBoxStyle.OkOnly)
         End Try
      End If
   End Sub
   Private Sub SelectTextBoxContents(sender As Object, e As EventArgs) Handles _
      txtName.Enter, txtFromEmail.Enter, txtToEmail.Enter, txtSubject.Enter, txtInputFile.Enter
      Dim tb As TextBox
      tb = CType(sender, TextBox)
      tb.SelectAll()
   End Sub
   Private Function CheckComposeValidity() As Boolean
      Select Case True
         Case Me.txtName.Text = ""
            MsgBox("Name filed shall not be empty. Please complete all the input fields.", MsgBoxStyle.OkOnly)
            Me.txtName.Focus()
            Return False
         Case Me.txtFromEmail.Text = ""
            MsgBox("From filed shall not be empty. Please complete all the input fields.", MsgBoxStyle.OkOnly)
            Me.txtFromEmail.Focus()
            Return False
         Case Not IsEmailValid(Me.txtFromEmail.Text)
            MsgBox("Invalid email address. Please provide a valid email address. This is required for sending the generated license file.", MsgBoxStyle.OkOnly)
            Me.txtFromEmail.Focus()
            Return False
         Case Me.txtToEmail.Text = ""
            MsgBox("To filed shall not be empty. Please complete all the input fields.", MsgBoxStyle.OkOnly)
            Me.txtToEmail.Focus()
            Return False
         Case Me.txtInputFile.Text = ""
            MsgBox("Please choose license input file if generated already. To generate an input file, please click on the Generate Input File button.", MsgBoxStyle.OkOnly)
            Me.txtInputFile.Focus()
            Return False
         Case Me.txtSubject.Text = ""
            MsgBox("From filed shall not be empty. Please complete all the input fields.", MsgBoxStyle.OkOnly)
            Me.txtSubject.Focus()
            Return False
         Case Me.txtMessage.Text = ""
            MsgBox("From filed shall not be empty. Please complete all the input fields.", MsgBoxStyle.OkOnly)
            Me.txtMessage.Focus()
            Return False
      End Select
      Return True
   End Function

   Private Function IsEmailValid(ByVal emailInput As String) As Boolean
      Dim p As String
      p = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z]*@[0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
      If Regex.IsMatch(emailInput, p) Then
         Return True
      Else
         Return False
      End If
   End Function

   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      Me.Close()
   End Sub
   Private Sub btnGenerateInput_Click(sender As Object, e As EventArgs) Handles btnGenerateInput.Click
      Dim lig As frmLicInputGenerator = New frmLicInputGenerator
      lig.ShowDialog()
   End Sub
   Private Sub btnAttach_Click(sender As Object, e As EventArgs) Handles btnAttach.Click
      Dim ofd As OpenFileDialog = New OpenFileDialog
      With ofd
         .Title = "Choose the SES License Input File"
         .DefaultExt = "lif"
         .FileName = "sesLicenseInput"
         .Filter = "License Input Files (*.lif)|*.lif"
      End With
      If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
         Me.txtInputFile.Text = ofd.FileName
      End If
   End Sub
End Class