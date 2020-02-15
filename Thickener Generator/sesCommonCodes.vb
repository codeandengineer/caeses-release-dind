Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Public Class sesCommonCodes
   Private xDocDB As XmlDocument, xTableNode As XmlNode, sesDataFileName As String, sesDataFileExt As String, FBD As FolderBrowserDialog
   Private ProgramPath As String, ProgramsDir As String, ExitSub As Boolean
   Private sesConfigFName As String
   Protected Friend ReadOnly Property GetProgramsDir() As String
      Get
         Return ProgramsDir
      End Get
   End Property
   Public Sub New()
      ProgramPath = My.Computer.FileSystem.SpecialDirectories.ProgramFiles
      ProgramsDir = ProgramPath & "\" & PUBLISHER_DIR
   End Sub
   Private Sub GetxTableNode()
      xDocDB = New XmlDocument
      sesConfigFName = ProgramsDir & "\xconfig.xml"
      xDocDB.Load(sesConfigFName)
      sesDataFileName = xDocDB.SelectSingleNode("//ses-fname").InnerText
      sesDataFileExt = xDocDB.SelectSingleNode("//ses-ext").InnerText
      xTableNode = xDocDB.SelectSingleNode("//db-directory")
   End Sub
   Private Sub GetDirectoryPath(ByRef DBDir As String)
      FBD = New FolderBrowserDialog : ExitSub = False
      With FBD
         .ShowNewFolderButton = False
         .Description = "Databse files are missing. Either the files are deleted or moved to a different location. Choose the new directory location"
         If .ShowDialog = DialogResult.OK Then
            xTableNode.InnerText = .SelectedPath
            DBDir = .SelectedPath
         Else
            MsgBox("No directory selected. Program will abort now.", MsgBoxStyle.OkOnly, "User cancelled the operation")
            DBDir = ""
            ExitSub = True
            Exit Sub
         End If
      End With
      xDocDB.Save(sesConfigFName)
   End Sub
   Protected Friend Sub FetchDBLocation(ByRef DBDir As String, Optional ByRef sesDFName As String = "")
      GetxTableNode()
      If xTableNode.InnerText = "" Then
         GetDirectoryPath(DBDir)
         If ExitSub Then Exit Sub
      Else
         DBDir = xTableNode.InnerText
      End If
      sesDFName = DBDir & "\" & sesDataFileName & sesDataFileExt
      xDocDB = Nothing
   End Sub
   Protected Friend Sub GetDBLocation(ByRef DBDir As String, Optional ByRef sesDFName As String = "")
      GetxTableNode()
      GetDirectoryPath(DBDir)
      If ExitSub Then Exit Sub
      sesDFName = DBDir & "\" & sesDataFileName & sesDataFileExt
      xDocDB = Nothing
   End Sub
End Class
