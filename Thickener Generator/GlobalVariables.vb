Option Strict On
Imports OpenSTAADUI
Imports SES.DataTypes
Module GlobalVariables
    Friend IsThickenerGeneratorLocked As Boolean = False
    Friend IsSecPickerLocked As Boolean = False
    Friend PickedSection As String = ""
    Friend OS As OpenSTAAD
    Friend OSGeometry As OSGeometryUI
    Friend OSLoad As OSLoadUI
    Friend OSProperty As OSPropertyUI
    Friend OSOutput As OSOutputUI
    Friend OSCommands As OSCommandsUI
    Friend OSDesign As OSDesignUI
    Friend OSSupport As OSSupportUI
    Friend OSTable As OSTableUI
    Friend OSView As OSViewUI
    Friend defFName As String
    Friend AbortProcess As Boolean
    Friend TargetApp As String
    Friend Const PUBLISHER_NAME = "Code and Engineer"
    Friend Const PUBLISHER_DIR = "CaE SES"
    Friend Const CHOOSE_SEC As String = ">>"
    Friend Const TOTAL_ANGLE As Double = 360
    Friend Const Precision As Double = 0.0001
    '/------ Column index numbers of Grid Data ----------/
    Friend Const indSlNo As Integer = 0
    Friend Const indDesc As Integer = 1
    Friend Const indPCD As Integer = 2
    Friend Const indColSec As Integer = 3
    Friend Const indEL As Integer = 4
    Friend Const indCB As Integer = 5
    Friend Const indCBSec As Integer = 6
    Friend Const indCBPattern As Integer = 7
    Friend Const indRB As Integer = 8
    Friend Const indRBSec As Integer = 9
    Friend Const indRBPattern As Integer = 10
    Friend Const TG_TITLE = "Thickener Generator - OpenSTAAD Interface"
    Friend Sub GetOpenSTAADObjects()
        Try
            '/----------------------- Very Important Note ------------------------/
            '/------ In order to use the debugging facility in Visual Studio -----/
            '/------ Start the Visual Studio Solution with Non-Administrator -----/
            OS = CType(GetObject(, "StaadPro.OpenSTAAD"), OpenSTAAD)
            With OS
                OSGeometry = CType(OS.Geometry, OSGeometryUI)
                OSLoad = CType(OS.Load, OSLoadUI)
                OSProperty = CType(OS.Property, OSPropertyUI)
                OSOutput = CType(OS.Output, OSOutputUI)
                OSCommands = CType(OS.Command, OSCommandsUI)
                OSDesign = CType(OS.Design, OSDesignUI)
                OSSupport = CType(OS.Support, OSSupportUI)
                OSTable = CType(OS.Table, OSTableUI)
                OSView = CType(OS.View, OSViewUI)
            End With
            '/--------------------------------------------------------------------/
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & vbCrLf & _
                   "In order to create the OpenSTAAD object, please ensure that the STAAD.Pro application is open.", _
                   MsgBoxStyle.OkOnly, "Get OpenSTAAD Objects")
        End Try
    End Sub
    Friend Sub TerminateOSObjects()
        OS = Nothing
        OSGeometry = Nothing
        OSLoad = Nothing
        OSProperty = Nothing
        OSOutput = Nothing
        OSCommands = Nothing
        OSDesign = Nothing
        OSSupport = Nothing
        OSTable = Nothing
        OSView = Nothing
    End Sub
End Module
