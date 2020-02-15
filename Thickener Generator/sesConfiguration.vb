'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Explicit On
Imports System.Data
Imports System.Xml
Imports System.IO
Public Class sesConfiguration
   Private Const xConfigFileName As String = "xconfig.xml"
   Private Query As String = "", xFName As String = "", ProgramDir As String
   Private xSec As sesSecurity, xCC As sesCommonCodes, xDoc As XmlDocument
   Private ReadOnly Property Developer As String
      Get
         Query = "developer"
         Return FetchElementData(Query)
      End Get
   End Property
   Private ReadOnly Property CopyRight As String
      Get
         Query = "copyright"
         Return FetchElementData(Query)
      End Get
   End Property
   Protected Friend ReadOnly Property FetchBoltsFileName As String
      Get
         Query = "bolts"
         Return FetchElementData(Query)
      End Get
   End Property
   Protected Friend ReadOnly Property FetchFileExtension As String
      Get
         Query = "file-extension"
         Return FetchElementData(Query)
      End Get
   End Property
   Protected Friend ReadOnly Property FetchMaterialsFileName As String
      Get
         Query = "materials"
         Return FetchElementData(Query)
      End Get
   End Property
   Protected Friend ReadOnly Property FetchSectionsFileName(ByVal Category As String) As String
      Get
         Select Case Category
            Case "xISections"
               Query = "isections"
            Case "xCSections"
               Query = "csections"
            Case "xLSections"
               Query = "lsections"
            Case "xTubeSections"
               Query = "tubesections"
            Case "xPipeSections"
               Query = "pipesections"
         End Select
         Return FetchElementData(Query)
      End Get
   End Property
   Private Function FetchElementData(QueryString As String) As String
      Dim xQueryResult As String = ""
      xCC = New sesCommonCodes
      ProgramDir = xCC.GetProgramsDir
      xFName = ProgramDir & "\" & xConfigFileName
      xDoc = New XmlDocument
      Try
         xDoc.Load(xFName)
         xQueryResult = xDoc.SelectSingleNode("//configuration/" & QueryString).InnerText
         xDoc = Nothing
         Return xQueryResult
      Catch ex As Exception
         Return Nothing
         Exit Function
      End Try
   End Function
End Class
