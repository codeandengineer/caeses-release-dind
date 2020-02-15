'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Explicit On
Imports System.Data
Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.IO
Public Class sesDecryptClass
   Private xSec As sesSecurity
   Private LicKey As String
   Private IByte() As Byte
   Private DecByte() As Byte
   Private AESSec As sesAESSecurity
   Protected Friend Sub DecXMLFile(EncFile As String, ByRef MStr As MemoryStream)
      xSec = New sesSecurity
      LicKey = xSec.Password
      AESSec = New sesAESSecurity(LicKey)
      IByte = File.ReadAllBytes(EncFile)
      DecByte = AESSec.AESDecECB(IByte)
      MStr.Write(DecByte, 0, DecByte.Length)
      MStr.Seek(0, SeekOrigin.Begin)
   End Sub
End Class
