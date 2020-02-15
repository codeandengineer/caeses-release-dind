Option Explicit On
Imports System.Data
Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports SES.DataTypes
Public Class sesMaterials
   Private xConfig As sesConfiguration, xCC As sesCommonCodes ', xSec As xSecurity
   Private xDoc As XmlDocument, xTableNode As XmlNode, xGradeNodes As XmlNodeList
   Private xFName As String, TableName As String, DBName As String, DBExtension As String
   Private DBDirectory As String
   Sub New()
      xConfig = New sesConfiguration : xCC = New sesCommonCodes
      With xConfig
         DBName = .FetchMaterialsFileName
         DBExtension = .FetchFileExtension
         xCC.FetchDBLocation(DBDirectory)
      End With
      xConfig = Nothing : xCC = Nothing
   End Sub
   Public Function GetGradesFromXML(TableName As String) As String()
      Dim I As Integer, GradesList() As String
      Dim ODecrypt As sesDecryptClass, MStream As MemoryStream
      xFName = DBDirectory & "\" & DBName & DBExtension
      xDoc = New XmlDocument : ODecrypt = New sesDecryptClass : MStream = New MemoryStream

      If Not File.Exists(xFName) Then
         xCC.GetDBLocation(DBDirectory)
         xFName = DBDirectory & "\" & DBName & DBExtension
      End If

      Try
         ODecrypt.DecXMLFile(xFName, MStream)
         xDoc.Load(MStream)
         xTableNode = xDoc.SelectSingleNode("//MaterialsDatabase/MaterialTable[@name='" & TableName & "']")
         xGradeNodes = xTableNode.ChildNodes
         I = 1
         ReDim GradesList(0)
         For Each xGradeNode As XmlNode In xGradeNodes
            GradesList(I - 1) = xGradeNode.SelectSingleNode("Classification").InnerText
            ReDim Preserve GradesList(UBound(GradesList) + 1)
            I = I + 1
         Next
         ReDim Preserve GradesList(UBound(GradesList) - 1)
      Catch ex As Exception
         MsgBox("Looks like you don't have the required database!" & vbCrLf & _
         "Please place the following file in the Current directory" & vbCrLf & _
         vbCrLf & vbCrLf & "Missing File Name : " & DBName & DBExtension, , _
         "Database file missing - Elevated Terrains Consulting Engineers")
         GradesList = Nothing
      Finally
         xDoc = Nothing : ODecrypt = Nothing : MStream = Nothing
      End Try
      Return GradesList
   End Function
   Public Function GetDatabasesFromXML() As String()
      Dim I As Integer, DatabasesList() As String
      Dim ODecrypt As sesDecryptClass, MStream As MemoryStream
      xFName = DBDirectory & "\" & DBName & DBExtension
      xDoc = New XmlDocument : ODecrypt = New sesDecryptClass : MStream = New MemoryStream

      If Not File.Exists(xFName) Then
         xCC.GetDBLocation(DBDirectory)
         xFName = DBDirectory & "\" & DBName & DBExtension
      End If

      Try
         ODecrypt.DecXMLFile(xFName, MStream)
         xDoc.Load(MStream)
         I = 1
         ReDim DatabasesList(0)
         For Each xDatabaseNode As XmlNode In xDoc.SelectNodes("//MaterialsDatabase/MaterialTable")
            DatabasesList(I - 1) = xDatabaseNode.Attributes("name").Value
            ReDim Preserve DatabasesList(UBound(DatabasesList) + 1)
            I = I + 1
         Next
         ReDim Preserve DatabasesList(UBound(DatabasesList) - 1)
      Catch ex As Exception
         MsgBox("Looks like you don't have the required database!" & vbCrLf & _
         "Please place the following file in the Current directory" & vbCrLf & _
         vbCrLf & vbCrLf & "Missing File Name : " & DBName & DBExtension, , _
         "Database file missing - Elevated Terrains Consulting Engineers")
         DatabasesList = Nothing
      Finally
         xDoc = Nothing : ODecrypt = Nothing : MStream = Nothing
      End Try
      Return DatabasesList

   End Function
   Public Sub FillMaterialData(ByRef md As MaterialData,
                                ByVal tableName As String,
                                ByVal materialGrade As String)
      Dim ODecrypt As sesDecryptClass, MStream As MemoryStream
      xFName = DBDirectory & "\" & DBName & DBExtension
      xDoc = New XmlDocument : ODecrypt = New sesDecryptClass : MStream = New MemoryStream

      If Not File.Exists(xFName) Then
         xCC.GetDBLocation(DBDirectory)
         xFName = DBDirectory & "\" & DBName & DBExtension
      End If

      Try
         ODecrypt.DecXMLFile(xFName, MStream)
         xDoc.Load(MStream)
         With md
            xTableNode = xDoc.SelectSingleNode("//MaterialsDatabase/common-data")
            .E = CDbl(xTableNode.SelectSingleNode("E").InnerText)
            .Poisson = CDbl(xTableNode.SelectSingleNode("poisson").InnerText)
            .Alpha = CDbl(xTableNode.SelectSingleNode("alpha").InnerText)
            .Density = CDbl(xTableNode.SelectSingleNode("density").InnerText)
            .CrDamp = CDbl(xTableNode.SelectSingleNode("crdamp").InnerText)
         End With
         xTableNode = xDoc.SelectSingleNode("//MaterialsDatabase/MaterialTable[@name='" & tableName & "']")
         xGradeNodes = xTableNode.ChildNodes
         For Each xGradeNode As XmlNode In xGradeNodes
            If xGradeNode.SelectSingleNode("Classification").InnerText = materialGrade Then
               With xGradeNode
                  ' Comments next to the each XML value indicates the
                  ' original unit of the data in the Excel File
                  md.Grade = CStr(.SelectSingleNode("Classification").InnerText)
                  md.Fy = CDbl(.SelectSingleNode("Fy").InnerText) ' N/mm^2
                  md.Fu = CDbl(.SelectSingleNode("Fu").InnerText) ' N/mm^2
                  md.STAADName = CStr(.SelectSingleNode("STAADName").InnerText)
                  md.dbName = tableName
               End With
               Exit For
            End If
         Next
      Catch ex As Exception
         MsgBox("Looks like you don't have the required database!" & vbCrLf & _
         "Please place the following file in the Current directory" & vbCrLf & _
         vbCrLf & vbCrLf & "Missing File Name : " & DBName & DBExtension, , _
         "Database file missing - Elevated Terrains Consulting Engineers")
         Exit Sub
      Finally
         xDoc = Nothing : ODecrypt = Nothing : MStream = Nothing
      End Try
   End Sub
End Class
