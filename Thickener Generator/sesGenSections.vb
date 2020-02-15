'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 25 Apr 2019                                /
'/ Copyright      : Elevated Terrains Consulting Engineers     /
'/ Developer      : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Explicit On
Imports System.Data
Imports System.Windows.Forms
Imports System.Xml
Imports System.IO
Imports SES.DataTypes
Public Class sesGenSections
   Private xSec As sesSecurity, xConfig As sesConfiguration, xCC As sesCommonCodes
   Private xDoc As XmlDocument, xTableNode As XmlNode, xProfileNodes As XmlNodeList
   Private xFName As String, DBName As String, DBExtension As String
   Private TableName As String, Category As String, xtrDataFName As String
   Private DBDirectory As String
   Sub New(SecCategory As String)
      xConfig = New sesConfiguration : xCC = New sesCommonCodes
      With xConfig
         DBName = .FetchSectionsFileName(SecCategory)
         DBExtension = .FetchFileExtension
         Category = SecCategory
         xCC.FetchDBLocation(DBDirectory, xtrDataFName)
      End With
      xConfig = Nothing : xCC = Nothing
   End Sub
   ''' <summary>
   ''' Gets the TableName used in XML database file based on the selected SectionDatabase and SectionType 
   ''' </summary>
   ''' <param name="SectionDatabase">Database selected by the user</param>
   ''' <param name="SectionType">Type of the section selected by the user</param>
   ''' <returns>Returns a string which representing name of the sections table</returns>
   ''' <remarks></remarks>
   Public Function FetchTableName(ByVal SectionDatabase As String, _
                                  SectionType As String) As String
      Dim ODecrypt As sesDecryptClass, MStream As MemoryStream
      xDoc = New XmlDocument : xCC = New sesCommonCodes : ODecrypt = New sesDecryptClass : MStream = New MemoryStream
      Dim xPath As String
      Try
         If Not File.Exists(xtrDataFName) Then xCC.GetDBLocation(DBDirectory, xtrDataFName)
         ODecrypt.DecXMLFile(xtrDataFName, MStream)
         xDoc.Load(MStream)
         xPath = "//tablenames[@category='" & Category & "']/database[@name='" & SectionDatabase & "']/tablename[@sec-type='" & SectionType & "']"
         TableName = xDoc.SelectSingleNode(xPath).InnerText
      Catch ex As Exception
         MsgBox(ex.Message)
      Finally
         xDoc = Nothing : xCC = Nothing
      End Try
      Return TableName
   End Function
   Public Function GetProfilesFromXML(TableName As String) As String()
      Dim I As Integer, SectionsList() As String, DefaultProfile As String
      Dim ODecrypt As sesDecryptClass, MStream As MemoryStream
      xDoc = New XmlDocument : ODecrypt = New sesDecryptClass : MStream = New MemoryStream
      If xCC Is Nothing Then xCC = New sesCommonCodes
      DefaultProfile = ""
      xFName = DBDirectory & "\" & DBName & DBExtension
      If Not File.Exists(xFName) Then
         xCC.GetDBLocation(DBDirectory, xtrDataFName)
         xFName = DBDirectory & "\" & DBName & DBExtension
      End If
      Try
         ODecrypt.DecXMLFile(xFName, MStream)
         xDoc.Load(MStream)
         xTableNode = xDoc.SelectSingleNode("//SectionsDatabase/SectionsTable[@name='" & TableName & "']")
         xProfileNodes = xTableNode.ChildNodes
         I = 1
         ReDim SectionsList(0)
         For Each xProfileNode As XmlNode In xProfileNodes
            'SectionsList(I - 1) = xProfileNode.SelectSingleNode("Designation").InnerText
            If xProfileNode.SelectSingleNode("STAADName").InnerText <> "Not Listed" Then
               SectionsList(I - 1) = xProfileNode.SelectSingleNode("STAADName").InnerText
               ReDim Preserve SectionsList(UBound(SectionsList) + 1)
               I = I + 1
            End If
         Next
         ReDim Preserve SectionsList(UBound(SectionsList) - 1)
      Catch ex As Exception
         MsgBox("Looks like you don't have the required database!" & vbCrLf & _
                "Please place the following file in the databases directory" & vbCrLf & _
                vbCrLf & vbCrLf & "Missing file name : " & DBName & DBExtension, , _
                "Database file missing - Elevated Terrains Consulting Engineers")
         SectionsList = Nothing
      Finally
         xDoc = Nothing : ODecrypt = Nothing : MStream = Nothing
      End Try
      Return SectionsList
   End Function
   Public Sub GetGenSecProps(ByRef sd As SectionData, ByVal TableName As String, ByVal selectedProfile As String)
      Dim ODecrypt As sesDecryptClass, MStream As MemoryStream
      xDoc = New XmlDocument : ODecrypt = New sesDecryptClass : MStream = New MemoryStream
      If xCC Is Nothing Then xCC = New sesCommonCodes
      xFName = DBDirectory & "\" & DBName & DBExtension
      If Not File.Exists(xFName) Then
         xCC.GetDBLocation(DBDirectory, xtrDataFName)
         xFName = DBDirectory & "\" & DBName & DBExtension
      End If
      Try
         ODecrypt.DecXMLFile(xFName, MStream)
         xDoc.Load(MStream)
         xTableNode = xDoc.SelectSingleNode("//SectionsDatabase/SectionsTable[@name='" & TableName & "']")
         xProfileNodes = xTableNode.ChildNodes
         For Each xProfileNode As XmlNode In xProfileNodes
            If xProfileNode.SelectSingleNode("STAADName").InnerText = selectedProfile Then
               With xProfileNode
                  ' Comments next to the each XML value indicates the
                  ' original unit of the data in the Excel File
                  sd.STAADName = CStr(.SelectSingleNode("STAADName").InnerText)
                  sd.Designation = CStr(.SelectSingleNode("Designation").InnerText)
                  sd.Mass = CDbl(.SelectSingleNode("Mass").InnerText) ' kg/m
                  sd.Massfps = CDbl(.SelectSingleNode("Mass-fps").InnerText) ' pounds/foot
                  If Category = "xPipeSections" Then
                     sd.OD = CSng(.SelectSingleNode("OD").InnerText) ' mm
                     sd.ID = CSng(.SelectSingleNode("ID").InnerText) ' mm
                     sd.tw = CSng(.SelectSingleNode("tw").InnerText) ' mm
                     sd.A = CDbl(.SelectSingleNode("A").InnerText) * 10 ^ 2 ' cm^2
                  Else
                     sd.h = CSng(.SelectSingleNode("h").InnerText) ' mm
                     sd.bf = CSng(.SelectSingleNode("bf").InnerText) ' mm
                     sd.tw = CSng(.SelectSingleNode("tw").InnerText) ' mm
                     sd.tf = CSng(.SelectSingleNode("tf").InnerText) ' mm
                     sd.r1 = CSng(.SelectSingleNode("r1").InnerText) ' mm
                     sd.r2 = CSng(.SelectSingleNode("r2").InnerText) ' mm
                     sd.A = CDbl(.SelectSingleNode("A").InnerText) * 10 ^ 2 ' cm^2
                     sd.hi = CSng(.SelectSingleNode("hi").InnerText) ' mm
                     sd.d = CSng(.SelectSingleNode("d").InnerText) ' mm
                     sd.Alpha = CDbl(.SelectSingleNode("alpha").InnerText) ' Degrees
                     sd.k = CSng((CSng(.SelectSingleNode("h").InnerText) - CDbl(.SelectSingleNode("d").InnerText)) / 2)
                     sd.k1 = CSng(sd.tw / 2 + sd.r1)
                     sd.ss = CSng(.SelectSingleNode("ss").InnerText) ' mm
                  End If
                  sd.ALo = CDbl(.SelectSingleNode("ALO").InnerText) ' m^2/m
                  If .SelectSingleNode("ALI") IsNot Nothing Then
                     sd.ALi = CDbl(.SelectSingleNode("ALI").InnerText) ' m^2/m
                  End If
                  sd.AGo = CDbl(.SelectSingleNode("AGO").InnerText) ' m^2/t
                  If .SelectSingleNode("AGI") IsNot Nothing Then
                     sd.AGi = CDbl(.SelectSingleNode("AGI").InnerText) ' m^2/t
                  End If
                  sd.Iy = CDbl(.SelectSingleNode("Iy").InnerText) * 10 ^ 4 ' cm^4
                  sd.Wely = CDbl(.SelectSingleNode("Wely").InnerText) * 10 ^ 3 ' cm^3
                  sd.Wply = CDbl(.SelectSingleNode("Wply").InnerText) * 10 ^ 3 ' cm^3
                  sd.ry = CSng(.SelectSingleNode("ry").InnerText) * 10 ' cm
                  sd.Avz = CDbl(.SelectSingleNode("Avz").InnerText) * 10 ^ 2 ' cm^2
                  sd.Iz = CDbl(.SelectSingleNode("Iz").InnerText) * 10 ^ 4 ' cm^4
                  sd.Welz = CDbl(.SelectSingleNode("Welz").InnerText) * 10 ^ 3 ' cm^3
                  sd.Wplz = CDbl(.SelectSingleNode("Wplz").InnerText) * 10 ^ 3 ' cm^3
                  sd.rz = CSng(.SelectSingleNode("rz").InnerText) * 10 ' cm
                  sd.Avy = CDbl(.SelectSingleNode("Avy").InnerText) * 10 ^ 2 'cm^2
                  sd.It = CDbl(.SelectSingleNode("It").InnerText) * 10 ^ 4 ' cm^4
                  sd.Iw = CDbl(.SelectSingleNode("Iw").InnerText) * 10 ^ 3 * 10 ^ 6 ' x10^3 cm^6
                  sd.Cy = CDbl(.SelectSingleNode("Cy").InnerText) 'mm
                  sd.ey = CDbl(.SelectSingleNode("ey").InnerText) 'mm
                  sd.Cz = CDbl(.SelectSingleNode("Cz").InnerText) 'mm
                  sd.ez = CDbl(.SelectSingleNode("ez").InnerText) 'mm
                  ' Principal axes properties
                  If .SelectSingleNode("Iu") IsNot Nothing Then
                     sd.Iu = CDbl(.SelectSingleNode("Iu").InnerText) * 10 ^ 4 ' cm^4
                  End If
                  If .SelectSingleNode("Iv") IsNot Nothing Then
                     sd.Iv = CDbl(.SelectSingleNode("Iv").InnerText) * 10 ^ 4 ' cm^4
                  End If

                  If .SelectSingleNode("ru") IsNot Nothing Then
                     sd.ru = CDbl(.SelectSingleNode("ru").InnerText) * 10 ' cm
                  End If
                  If .SelectSingleNode("rv") IsNot Nothing Then
                     sd.rv = CDbl(.SelectSingleNode("rv").InnerText) * 10 ' cm
                  End If
               End With
               Exit For
            End If
         Next
      Catch ex As Exception
         MsgBox("Looks like you don't have the required database!" & vbCrLf & _
         "Please place the following file in the databases directory" & vbCrLf & _
         vbCrLf & vbCrLf & "Missing file name : " & DBName & DBExtension, , _
         "Database file missing - Elevated Terrains Consulting Engineers")
         Exit Sub
      Finally
         xDoc = Nothing : ODecrypt = Nothing : MStream = Nothing
      End Try
   End Sub
End Class
