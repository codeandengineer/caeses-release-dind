Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup

            Dim appStarter As ApplicationStarter = New ApplicationStarter
            'appStarter.StartNormal(sender, e)
            appStarter.StartByPassed()

        End Sub

    End Class
    Class ApplicationStarter
        Public Sub StartNormal(sender As Object, e As ApplicationServices.StartupEventArgs)
            Dim Lic As sesLicenseClass = New sesLicenseClass
            Dim LicStatus As String = ""
            LicStatus = Lic.GetLicenseStatus()
            If (LicStatus = "Valid") Then
                StartApplication()
            Else
                Dim ObjLicStat As noLicense = New noLicense
                With ObjLicStat
                    .lblLicStatusDescription.Text = LicStatus.ToString
                    .ShowDialog()
                End With
                e.Cancel = True
            End If
        End Sub

        Public Sub StartByPassed()
            StartApplication()
        End Sub

        Private Sub StartApplication()
            Dim ObjSplash As Splash
            ObjSplash = New Splash
            ObjSplash.ShowDialog()
        End Sub

    End Class

End Namespace

