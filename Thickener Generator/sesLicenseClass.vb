'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Strict On
Imports System.Xml
Imports System.IO
Public Class sesLicenseClass
   Private xDoc As XmlDocument
   Private LicFilePath As String
   Private LicFileName As String
   Private ODecrypt As sesDecryptClass
   Private xCC As sesCommonCodes
   Private LicMemStream As MemoryStream
   Friend ReadOnly Property GetPCName() As String
      Get
         Return System.Environment.MachineName
      End Get
   End Property
   Friend ReadOnly Property GetUserDomain() As String
      Get
         Return System.Environment.UserDomainName
      End Get
   End Property
   Friend Function GetUsername(Optional withDomain As Boolean = False) As String
      Dim Username As String = ""
      Username = Environment.UserName
      If withDomain Then
         Username = GetUserDomain() & "\" & Username
      Else
         Username = Username
      End If
      Return Username
   End Function
   Public Function NumOfDays(ByVal sDate As Date) As Long
      'Dim sDate As Date = "10-07-2018 14:10:15" '#10/7/2018 2:10:15 PM#
      Dim LapsedDays As Long = 0
      LapsedDays = DateDiff(DateInterval.Day, sDate, Now.Date)
      Return LapsedDays
   End Function
   Protected Friend Function GetLicenseStatus() As String
      Dim LicValidityStatus As String = ""
      Const LicMsg As String = "Please contact the developer."
      Dim LFMachineName As String, LFUserName As String, LFLicType As String, LFNumDays As Long, LFEndDate As String
      ODecrypt = New sesDecryptClass
      xCC = New sesCommonCodes
      xDoc = New XmlDocument
      LicFilePath = xCC.GetProgramsDir
      LicFileName = LicFilePath & "\sesLicense.lic"
      If File.Exists(LicFileName) Then
         LicMemStream = New MemoryStream
         ODecrypt.DecXMLFile(LicFileName, LicMemStream)
         Try
            xDoc.Load(LicMemStream)
            LFMachineName = xDoc.SelectSingleNode("//pc-name").InnerText
            LFUserName = xDoc.SelectSingleNode("//username").InnerText
            LFLicType = xDoc.SelectSingleNode("//lic-type").InnerText
            LFEndDate = xDoc.SelectSingleNode("//end-date").InnerText
            LFNumDays = CLng(xDoc.SelectSingleNode("//lic-days").InnerText)
            If LFMachineName <> GetPCName() Then
               LicValidityStatus = "The License is NOT configured for this machine. " & LicMsg
            ElseIf LFUserName <> GetUsername(True) Then
               LicValidityStatus = "The License is NOT configured for this user. " & LicMsg
            ElseIf LFLicType = "Evaluation" Then
               If CDate(Date.Now.ToShortDateString) <= CDate(LFEndDate) Then
                  LicValidityStatus = "Valid"
               Else
                  LicValidityStatus = "License expired. " & LicMsg
               End If
            ElseIf LFLicType = "Annual" Then
               If CDate(Date.Now.ToShortDateString) <= CDate(LFEndDate) Then
                  LicValidityStatus = "Valid"
               Else
                  LicValidityStatus = "License expired. " & LicMsg
               End If
            Else
               LicValidityStatus = "Valid"
            End If
         Catch ex As Exception
            LicValidityStatus = "Invalid license file or the license file contents are modified."
         Finally
            ODecrypt = Nothing : xCC = Nothing : xDoc = Nothing
         End Try
      Else
         LicValidityStatus = "License file does not exist. Please copy the license file to program directory."
      End If
      Return LicValidityStatus
   End Function
   Protected Friend Sub GetLicenseInformation(ByRef LicensedUsername As String,
                                              ByRef LicensedCompany As String, ByRef LicenseType As String)
      ODecrypt = New sesDecryptClass : xCC = New sesCommonCodes : xDoc = New XmlDocument
      LicFilePath = xCC.GetProgramsDir
      LicFileName = LicFilePath & "\sesLicense.lic"
      If File.Exists(LicFileName) Then
         LicMemStream = New MemoryStream
         ODecrypt.DecXMLFile(LicFileName, LicMemStream)
         Try
            xDoc.Load(LicMemStream)
            LicensedUsername = xDoc.SelectSingleNode("//lic-user").InnerText
            LicensedCompany = xDoc.SelectSingleNode("//lic-company").InnerText
            LicenseType = xDoc.SelectSingleNode("//lic-type").InnerText
         Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly)
         Finally
            ODecrypt = Nothing : xCC = Nothing : xDoc = Nothing
         End Try
      Else
         MsgBox("License file does not exist. Please copy the license file to program directory.", MsgBoxStyle.OkOnly, "License file missing")
      End If
   End Sub
End Class
