'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Explicit On
Imports System.Data
Imports System.IO
Imports System.Xml
Public Interface IAESSecurity
   Function AESEncCBC(ByVal Text2Encrypt As String) As String
   Function AESDecCBC(ByVal Text2Decrypt As String) As String
   Function AESEncECB(ByVal input As Byte()) As Byte()
   Function AESDecECB(ByVal input As Byte()) As Byte()
End Interface
Public Class sesAESSecurity
   Implements IAESSecurity
   Private PassKey As String
   Sub New()
      PassKey = "EngineeredProgramming"
   End Sub
   Sub New(PassCode As String)
      PassKey = PassCode
   End Sub
   ' CBC and ECB Indicates the Ciphermodes
   Public Function AESEncCBC(ByVal Text2Encrypt As String) As String _
      Implements IAESSecurity.AESEncCBC
      Dim AES As New System.Security.Cryptography.RijndaelManaged
      Dim SHA256 As New System.Security.Cryptography.SHA256Cng
      Dim EncryptedText As String = ""
      Try
         AES.GenerateIV()
         AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(PassKey))

         AES.Mode = Security.Cryptography.CipherMode.CBC
         Dim DESEncrypter As Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
         Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(Text2Encrypt)
         EncryptedText = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

         Return Convert.ToBase64String(AES.IV) & Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

      Catch ex As Exception
         Return ex.Message
      End Try
   End Function
   Public Function AESDecCBC(ByVal EncryptedText As String) As String _
      Implements IAESSecurity.AESDecCBC
      Dim AES As New System.Security.Cryptography.RijndaelManaged
      Dim SHA256 As New System.Security.Cryptography.SHA256Cng
      Dim DecryptedText As String = ""
      Dim iv As String = ""
      Try
         Dim ivct = EncryptedText.Split({"=="}, StringSplitOptions.None)
         iv = ivct(0) & "=="
         EncryptedText = If(ivct.Length = 3, ivct(1) & "==", ivct(1))

         AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(PassKey))
         AES.IV = Convert.FromBase64String(iv)
         AES.Mode = Security.Cryptography.CipherMode.CBC
         Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
         Dim Buffer As Byte() = Convert.FromBase64String(EncryptedText)
         DecryptedText = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
         Return DecryptedText
      Catch ex As Exception
         Return ex.Message
      End Try
   End Function
   Public Function AESEncECB(ByVal input As Byte()) As Byte() _
      Implements IAESSecurity.AESEncECB
      Dim AES As New System.Security.Cryptography.RijndaelManaged
      Dim SHA256 As New System.Security.Cryptography.SHA256Cng
      Dim ciphertext As String = ""
      Try
         AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(PassKey))
         AES.Mode = Security.Cryptography.CipherMode.ECB
         Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
         Dim Buffer As Byte() = input
         Return DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
      Catch ex As Exception
         Return Nothing
      End Try
   End Function
   Public Function AESDecECB(ByVal input As Byte()) As Byte() _
      Implements IAESSecurity.AESDecECB
      Dim AES As New System.Security.Cryptography.RijndaelManaged
      Dim SHA256 As New System.Security.Cryptography.SHA256Cng
      Try
         AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(PassKey))
         AES.Mode = Security.Cryptography.CipherMode.ECB
         Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
         Dim Buffer As Byte() = input
         Return DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
      Catch ex As Exception
         Return Nothing
      End Try
   End Function
End Class
