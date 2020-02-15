Public Class Splash
   Private timeLeft As Integer, OLic As sesLicenseClass, LicStatus As String = ""
   Private Const nSecs = 30
   Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Dim LicUser As String = "", LicCompany As String = "", LicType As String = ""
      timeLeft = nSecs
      OLic = New sesLicenseClass
      LicStatus = OLic.GetLicenseStatus
      If LicStatus = "Valid" Then
         Try
            OLic.GetLicenseInformation(LicUser, LicCompany, LicType)
         Catch ex As Exception
            MsgBox("Following Error Encountered:" & ex.Message.ToString)
         End Try
      End If
      With Me
         .lblLicUser.Text = LicUser.ToString
         .lblLicCompany.Text = LicCompany.ToString
         .lblLicType.Text = LicType.ToString
         .lblVersion.Text = "v" & Application.ProductVersion
      End With
      timSplash.Start()
   End Sub

   Private Sub timSplash_Tick(sender As Object, e As EventArgs) Handles timSplash.Tick
      If timeLeft > 0 Then
         timeLeft = (timeLeft - 1)
         Me.lblLoading.Text = "Loading in " & Math.Ceiling(timeLeft / 10) & "s"
      Else
         OLic = Nothing
         timSplash.Stop()
         Me.Close()
      End If
   End Sub
End Class