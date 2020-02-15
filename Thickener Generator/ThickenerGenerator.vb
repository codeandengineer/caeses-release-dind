'/-------------------------------------------------------------/
'/ Program Name   : ThickenerGenerator                         /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Strict On
Imports SES.DataTypes
Public Class ThickenerGenerator
   Private GD As GeometryData, BP As BasicProfile
   Private Const nPlatesMin As Long = 3
   Private PI As Double
   Private ObjThickGen As frmThickGen
   Sub New(ByRef ftg As frmThickGen)
      ObjThickGen = ftg
      PI = Math.PI
      FillGeometryData()
      CalcBasicProfile()
   End Sub
   Private Sub New()
      '/ Deny user from creating an empty New Object
   End Sub
   Friend ReadOnly Property GetGeometryData() As GeometryData
      Get
         Return GD
      End Get
   End Property
   Friend ReadOnly Property GetBasicProfile() As BasicProfile
      Get
         Return BP
      End Get
   End Property
   Private Sub FillGeometryData()
      ' Fetch the input data from Geometry Input worksheet
      With ObjThickGen
         GD.Origin.x = CDbl(Val(.txtxOrigin.Text))
         GD.Origin.y = CDbl(Val(.txtyOrigin.Text))
         GD.Origin.z = CDbl(Val(.txtzOrigin.Text))
         GD.Alpha = Deg2Rad(CDbl(Val(.txtAlpha.Text))) ' Converted to Radians
         GD.Beta = Deg2Rad(CDbl(Val(.txtBeta.Text))) ' Converted to Radians
         GD.sDelta = Deg2Rad(CDbl(Val(.txtsDelta.Text))) ' Converted to Radians
         GD.nRB = CLng(Val(.txtnRB.Text))
         GD.nColRB = CLng(Val(.txtnColRB.Text))
         GD.idBP = CDbl(Val(.txtidBP.Text))
         GD.odBP = CDbl(Val(.txtodBP.Text))
         GD.pcdBolts = CDbl(Val(.txtpcdBolts.Text))
         GD.idCP = CDbl(Val(.txtidCP.Text))
         GD.odCP = CDbl(Val(.txtodCP.Text))
         GD.dCC = CDbl(Val(.txtdCC.Text))
         GD.hCC = CDbl(Val(.txthCC.Text))
         GD.hCCTop = CDbl(Val(.txthCCTop.Text))
         GD.dUF = CDbl(Val(.txtdUF.Text))
         GD.hUF = CDbl(Val(.txthUF.Text))
         GD.dCR = CDbl(Val(.txtdCR.Text))
         GD.bCR = CDbl(Val(.txtbCR.Text))
         GD.hCR = CDbl(Val(.txthCR.Text))
         GD.hDroop = CDbl(Val(.txthDroop.Text))
         '/--------- Modified -------------/
         If GD.hDroop <> 0.0 Then
            GD.rDroop = CDbl(Val(.txtrDroop.Text))
         End If
         '/--------- Modified -------------/
         GD.nBoltsCC = CLng(Val(.txtnBoltsCC.Text))
         GD.LHalfChord = CDbl(Val(.txtHalfChordLength.Text))
         '/--------- Modified -------------/
         If GD.hDroop <> 0.0 Then
            GD.Theta = Deg2Rad(CDbl(Val(.txtTheta.Text))) ' Converted to Radians
         End If
         '/--------- Modified -------------/
         GD.dTank = CDbl(Val(.txtdTank.Text))
         GD.hWall = CDbl(Val(.txthWall.Text))
         GD.FreeBoard = CDbl(Val(.txtFreeBoard.Text))
         GD.Omega = Deg2Rad(CDbl(Val(.txtOmega.Text)))
         GD.bLaun = CDbl(Val(.txtbLaun.Text))
         GD.hLaun = CDbl(Val(.txthLaun.Text))
         GD.dLaun = (GD.dTank - 2 * GD.bLaun)
         GD.MaxPlateDim = CDbl(Val(.txtMaxPlateDim.Text))
         GD.AspectRatio = CDbl(Val(.txtAspectRatio.Text))
         GD.Phi = (PI / 2 - GD.Omega) / 2
      End With
   End Sub
   Private Sub CalcBasicProfile()
      ' Calculate the basic profile of the Support Structure and Tank
      With GD
         ' Base plate inner and outer nodes
         BP.sNodeBP.x = .Origin.x + (.idBP / 2) * Math.Cos(.sDelta) : BP.sNodeBP.y = .Origin.y : BP.sNodeBP.z = .Origin.z + (.idBP / 2) * Math.Sin(.sDelta)
         BP.eNodeBP.x = .Origin.x + (.odBP / 2) * Math.Cos(.sDelta) : BP.eNodeBP.y = .Origin.y : BP.eNodeBP.z = .Origin.z + (.odBP / 2) * Math.Sin(.sDelta)
         ' Centre columns start and end nodes
         BP.sNodeCC.x = .Origin.x + (.dCC / 2) * Math.Cos(.sDelta) : BP.sNodeCC.y = .Origin.y : BP.sNodeCC.z = .Origin.z + (.dCC / 2) * Math.Sin(.sDelta)
         BP.eNodeCC.x = BP.sNodeCC.x : BP.eNodeCC.y = BP.sNodeCC.y + .hCC : BP.eNodeCC.z = BP.sNodeCC.z
         ' Underflow cone start and end nodes
         BP.sNodeUF.x = BP.eNodeCC.x : BP.sNodeUF.y = BP.eNodeCC.y : BP.sNodeUF.z = BP.eNodeCC.z
         BP.eNodeUF.x = .Origin.x + (.dUF / 2) * Math.Cos(.sDelta) : BP.eNodeUF.y = BP.sNodeUF.y + .hUF : BP.eNodeUF.z = .Origin.z + (.dUF / 2) * Math.Sin(.sDelta)
         ' Compression ring start and end nodes
         BP.sNodeCR.x = BP.eNodeUF.x : BP.sNodeCR.y = BP.eNodeUF.y : BP.sNodeCR.z = BP.eNodeUF.z
         BP.eNodeCR.x = .Origin.x + (.dCR / 2) * Math.Cos(.sDelta) : BP.eNodeCR.y = BP.eNodeUF.y : BP.eNodeCR.z = .Origin.z + (.dCR / 2) * Math.Sin(.sDelta)
         ' Radial beam start and end nodes
         BP.sNodeRB.x = BP.eNodeCR.x : BP.sNodeRB.y = BP.eNodeCR.y : BP.sNodeRB.z = BP.eNodeCR.z
         BP.eNodeRB.x = .Origin.x + (.dTank / 2) * Math.Cos(.sDelta) : BP.eNodeRB.y = BP.sNodeRB.y + ((.dTank - .dCR) / 2) * Math.Tan(.Alpha) : BP.eNodeRB.z = .Origin.z + (.dTank / 2) * Math.Sin(.sDelta)
         ' Tank wall between between the radial beam highest node and bottom of launder wall start and end nodes
         BP.sNodeTW.x = BP.eNodeRB.x : BP.sNodeTW.y = BP.eNodeRB.y : BP.sNodeTW.z = BP.eNodeRB.z
         BP.eNodeTW.x = .Origin.x + (.dTank / 2) * Math.Cos(.sDelta) : BP.eNodeTW.y = BP.sNodeTW.y + (.hWall - .FreeBoard - .hLaun) : BP.eNodeTW.z = .Origin.z + (.dTank / 2) * Math.Sin(.sDelta)
         ' Tank wall launder portion start and end nodes
         BP.sNodeTWL.x = BP.eNodeTW.x : BP.sNodeTWL.y = BP.eNodeTW.y : BP.sNodeTWL.z = BP.eNodeTW.z
         BP.eNodeTWL.x = .Origin.x + (.dTank / 2) * Math.Cos(.sDelta) : BP.eNodeTWL.y = BP.sNodeTWL.y + .hLaun : BP.eNodeTWL.z = .Origin.z + (.dTank / 2) * Math.Sin(.sDelta)
         ' Tank wall free board portion start and end nodes
         BP.sNodeTWFB.x = BP.eNodeTWL.x : BP.sNodeTWFB.y = BP.eNodeTWL.y : BP.sNodeTWFB.z = BP.eNodeTWL.z
         BP.eNodeTWFB.x = .Origin.x + (.dTank / 2) * Math.Cos(.sDelta) : BP.eNodeTWFB.y = BP.sNodeTWFB.y + .FreeBoard : BP.eNodeTWFB.z = .Origin.z + (.dTank / 2) * Math.Sin(.sDelta)
         ' Launder floor portion start and end nodes
         BP.sNodeLF.x = BP.eNodeTW.x : BP.sNodeLF.y = BP.eNodeTW.y : BP.sNodeLF.z = BP.eNodeTW.z
         BP.eNodeLF.x = .Origin.x + (.dTank / 2 - .bLaun) * Math.Cos(.sDelta) : BP.eNodeLF.y = BP.sNodeLF.y : BP.eNodeLF.z = .Origin.z + (.dTank / 2 - .bLaun) * Math.Sin(.sDelta)
         ' Launder wall portion start and end nodes
         BP.sNodeLW.x = BP.eNodeLF.x : BP.sNodeLW.y = BP.eNodeLF.y : BP.sNodeLW.z = BP.eNodeLF.z
         BP.eNodeLW.x = .Origin.x + (.dTank / 2 - .bLaun) * Math.Cos(.sDelta) : BP.eNodeLW.y = BP.sNodeLW.y + .hLaun : BP.eNodeLW.z = .Origin.z + (.dTank / 2 - .bLaun) * Math.Sin(.sDelta)
      End With
   End Sub
   Private Sub sNodeCalc(ByRef sNode As NodeCoOrds,
                         ByVal Div As Long,
                         ByVal iDelta As Double,
                         ByRef Mesh As MeshData,
                         ByVal ShellComp As Component)
      Select Case ShellComp
         Case Component.LFloor
            sNode.x = GD.Origin.x + (GD.dTank / 2 - GD.bLaun + Div * Mesh.Hp) * Math.Cos(iDelta)
            sNode.y = BP.sNodeLF.y
            sNode.z = GD.Origin.z + (GD.dTank / 2 - GD.bLaun + Div * Mesh.Hp) * Math.Sin(iDelta)
         Case Component.WLWall
            sNode.x = GD.Origin.x + (GD.dTank / 2) * Math.Cos(iDelta)
            sNode.y = BP.sNodeTWL.y + Div * Mesh.Hp
            sNode.z = GD.Origin.z + (GD.dTank / 2) * Math.Sin(iDelta)
         Case Component.WFreeBoard
            sNode.x = GD.Origin.x + (GD.dTank / 2) * Math.Cos(iDelta)
            sNode.y = BP.sNodeTWFB.y + Div * Mesh.Hp
            sNode.z = GD.Origin.z + (GD.dTank / 2) * Math.Sin(iDelta)
         Case Component.Wall
            sNode.x = GD.Origin.x + (GD.dTank / 2) * Math.Cos(iDelta)
            sNode.y = BP.sNodeTWL.y - Div * Mesh.Hp
            sNode.z = GD.Origin.z + (GD.dTank / 2) * Math.Sin(iDelta)
         Case Component.WInterface
      End Select
   End Sub
   Private Sub iNodeCalc(ByRef iNode As NodeCoOrds,
                         ByVal Div As Long,
                         ByRef jdDelta As Double,
                         ByRef Mesh As MeshData,
                         ByVal ShellComp As Component)
      Select Case ShellComp
         Case Component.LFloor
            iNode.x = (GD.dTank / 2 - GD.bLaun + Div * Mesh.Hp) * Math.Cos(jdDelta)
            iNode.y = BP.sNodeLW.y
            iNode.z = (GD.dTank / 2 - GD.bLaun + Div * Mesh.Hp) * Math.Sin(jdDelta)
         Case Component.WLWall
            iNode.x = (GD.dTank / 2) * Math.Cos(jdDelta)
            iNode.y = BP.sNodeTWL.y + Div * Mesh.Hp
            iNode.z = (GD.dTank / 2) * Math.Sin(jdDelta)
         Case Component.WFreeBoard
            iNode.x = (GD.dTank / 2) * Math.Cos(jdDelta)
            iNode.y = BP.sNodeTWFB.y + Div * Mesh.Hp
            iNode.z = (GD.dTank / 2) * Math.Sin(jdDelta)
         Case Component.Wall
            iNode.x = (GD.dTank / 2) * Math.Cos(jdDelta)
            iNode.y = BP.sNodeTWL.y - Div * Mesh.Hp
            iNode.z = (GD.dTank / 2) * Math.Sin(jdDelta)
         Case Component.WInterface
      End Select
   End Sub
   Public Sub GenerateMesh(ByRef Mesh As MeshData,
                        ByRef IFMesh As MeshData,
                        ByRef pGr() As Integer,
                        ByRef NodesGr() As NodeCoOrdsGroup,
                        ByRef IFNodesGr() As NodeCoOrdsGroup,
                        ByVal GrName As String,
                        ByRef SeqNodeNum As Long,
                        ByRef SeqPlateNum As Long,
                        ByVal Comp As Component)
      Dim Trans As Long, TrType As TransType, UpperLimit As Long, Div As Integer, PL As Plate
      Dim nNodes As Long 'Number of nodes to iterate for plates creation at each radial beam
      Dim iDelta As Double, jTheta As Double, jYChord As Double, jdDelta As Double, jZChord As Double, jXChord As Double
      Dim sNode As NodeCoOrds, iNode As NodeCoOrds

      '/ Most Important
      '/ Implement an efficient way when No. of transitions are
      '/ more than the No. of divisions available.
      '/ Most Important
      ReDim pGr(0)
      ReDim NodesGr(CInt(Mesh.Nd))
      ' Transfer the interface nodes to next plate segment to be modelled
      NodesGr(0).Nodes = IFNodesGr(CInt(IFMesh.Nd)).Nodes
      ' Identify if transitions are required
      Trans = CLng((UBound(NodesGr(0).Nodes) + 1) / GD.nRB - Mesh.NrTop)
      If Trans > 0 Then
         TrType = TransType.Dec    'Decremental Transitions
      ElseIf Trans < 0 Then
         TrType = TransType.Inc    'Incremental Transitions
      Else
         TrType = TransType.None   'No Transitions
      End If
      For Div = 1 To CInt(Mesh.Nd)
         UpperLimit = CLng(((UBound(NodesGr(0).Nodes) + 1) / GD.nRB))
         ReDim NodesGr(Div).Nodes(0)
         If TrType = TransType.Inc Then
            If Div > (Mesh.Nd - Math.Abs(Trans)) Then
               UpperLimit = UpperLimit + (Div - (Mesh.Nd - Math.Abs(Trans)))
            Else
               UpperLimit = UpperLimit
            End If
         ElseIf TrType = TransType.Dec Then
            If Math.Abs(Trans) >= Div Then
               UpperLimit = UpperLimit - Div
            Else
               UpperLimit = UpperLimit - Math.Abs(Trans)
            End If
         Else
            UpperLimit = UpperLimit
         End If

         For I = 0 To GD.nRB - 1
            iDelta = GD.sDelta + (I * GD.Omega)
            ' Store the Node Number and Coords of First Node of the Segment
            sNodeCalc(sNode, Div, iDelta, Mesh, Comp)
            Call CreateNewNode(SeqNodeNum, NodesGr, Div, sNode.x, sNode.y, sNode.z)
            For J = 1 To UpperLimit - 1
               jTheta = J * (GD.Omega / UpperLimit)
               jdDelta = iDelta + jTheta
               iNodeCalc(iNode, Div, jdDelta, Mesh, Comp)
               jXChord = iNode.x : jYChord = iNode.y : jZChord = iNode.z
               Call CreateNewNode(SeqNodeNum, NodesGr, Div, jXChord, jYChord, jZChord)
            Next J
         Next I
         ReDim Preserve NodesGr(Div).Nodes(UBound(NodesGr(Div).Nodes) - 1)
      Next Div
      If Comp = Component.Wall Then
         Array.Reverse(NodesGr)
         If TrType = TransType.Dec Then
            TrType = TransType.Inc
         ElseIf TrType = TransType.Inc Then
            TrType = TransType.Dec
         Else
            TrType = TransType.None
         End If
      End If
      For Div = 1 To CInt(Mesh.Nd)
         If Div >= 1 Then
            With PL
               If TrType = TransType.Inc Then
                  If Div > (Mesh.Nd - Math.Abs(Trans)) Then
                     For I = 0 To GD.nRB - 1
                        nNodes = CLng(((UBound(NodesGr(Div).Nodes) + 1) / GD.nRB) - 1)
                        For J = 0 To nNodes
                           .NodeB = NodesGr(Div).Nodes(CInt(I * nNodes + J + I)).No
                           If (I * nNodes + J + I) = UBound(NodesGr(Div).Nodes) Then
                              .NodeA = NodesGr(Div - 1).Nodes(0).No : .NodeC = NodesGr(Div).Nodes(0).No
                           Else
                              .NodeA = NodesGr(Div - 1).Nodes(CInt(I * nNodes + J)).No : .NodeC = NodesGr(Div).Nodes(CInt(I * nNodes + (J + 1) + I)).No
                           End If
                           .NodeD = .NodeA
                           Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                           If J < nNodes Then
                              .NodeA = NodesGr(Div - 1).Nodes(CInt(I * nNodes + J)).No : .NodeB = NodesGr(Div).Nodes(CInt(I * nNodes + (J + 1) + I)).No
                              If (I * nNodes + J) = UBound(NodesGr(Div - 1).Nodes) Then
                                 .NodeC = NodesGr(Div - 1).Nodes(0).No
                              Else
                                 .NodeC = NodesGr(Div - 1).Nodes(CInt(I * nNodes + (J + 1))).No
                              End If
                              .NodeD = .NodeA
                              Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                           End If
                        Next J
                     Next I
                  Else
                     For I = 0 To UBound(NodesGr(Div).Nodes)
                        .NodeA = NodesGr(Div - 1).Nodes(I).No : .NodeB = NodesGr(Div).Nodes(I).No
                        If I = UBound(NodesGr(Div).Nodes) Then
                           .NodeC = NodesGr(Div).Nodes(0).No : .NodeD = NodesGr(Div - 1).Nodes(0).No
                        Else
                           .NodeC = NodesGr(Div).Nodes(I + 1).No : .NodeD = NodesGr(Div - 1).Nodes(I + 1).No
                        End If
                        Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     Next I
                  End If
               ElseIf TrType = TransType.Dec Then
                  If Math.Abs(Trans) >= Div Then
                     For I = 0 To GD.nRB - 1
                        nNodes = CLng(((UBound(NodesGr(Div - 1).Nodes) + 1) / GD.nRB) - 1)
                        For J = 0 To nNodes
                           .NodeA = NodesGr(Div - 1).Nodes(CInt(I * nNodes + J + I)).No
                           If (I * nNodes + J + I) = UBound(NodesGr(Div - 1).Nodes) Then
                              .NodeB = NodesGr(Div).Nodes(CInt(I * nNodes + (J - 1))).No
                              .NodeC = NodesGr(Div - 1).Nodes(0).No
                           Else
                              .NodeB = NodesGr(Div).Nodes(CInt(I * nNodes + J)).No
                              .NodeC = NodesGr(Div - 1).Nodes(CInt(I * nNodes + (J + 1) + I)).No
                           End If
                           .NodeD = .NodeA
                           Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                           If J < nNodes Then
                              .NodeA = NodesGr(Div).Nodes(CInt(I * nNodes + J)).No
                              If (I * nNodes + J) = UBound(NodesGr(Div).Nodes) Then
                                 .NodeB = NodesGr(Div).Nodes(0).No : .NodeC = NodesGr(Div - 1).Nodes(0).No
                              Else
                                 .NodeB = NodesGr(Div).Nodes(CInt(I * nNodes + (J + 1))).No : .NodeC = NodesGr(Div - 1).Nodes(CInt(I * nNodes + (J + 1) + I)).No
                              End If
                              .NodeD = .NodeA
                              Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                           End If
                        Next J
                     Next I
                  Else
                     For I = 0 To UBound(NodesGr(Div).Nodes)
                        .NodeA = NodesGr(Div - 1).Nodes(I).No : .NodeB = NodesGr(Div).Nodes(I).No
                        If I = UBound(NodesGr(Div).Nodes) Then
                           .NodeC = NodesGr(Div).Nodes(0).No : .NodeD = NodesGr(Div - 1).Nodes(0).No
                        Else
                           .NodeC = NodesGr(Div).Nodes(I + 1).No : .NodeD = NodesGr(Div - 1).Nodes(I + 1).No
                        End If
                        Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     Next I
                  End If
               Else
                  For I = 0 To UBound(NodesGr(Div).Nodes)
                     .NodeA = NodesGr(Div - 1).Nodes(I).No : .NodeB = NodesGr(Div).Nodes(I).No
                     If I = UBound(NodesGr(Div).Nodes) Then
                        .NodeC = NodesGr(Div).Nodes(0).No : .NodeD = NodesGr(Div - 1).Nodes(0).No
                     Else
                        .NodeC = NodesGr(Div).Nodes(I + 1).No : .NodeD = NodesGr(Div - 1).Nodes(I + 1).No
                     End If
                     Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                  Next I
               End If
            End With
         End If
      Next Div
      ReDim Preserve pGr(UBound(pGr) - 1)
      ' Create Group of the Selected Plate Elements
      OSGeometry.CreateGroupEx(GroupType.Plates, "_" & GrName, UBound(pGr) + 1, pGr)
   End Sub
   Public Sub CalcAdjHeight(ByRef IFMesh As MeshData,
                            ByRef AdjHeight As Double)
      Dim DroopNodesGr(0) As NodeCoOrdsGroup, IFNodesGr(0) As NodeCoOrdsGroup
      ReDim DroopNodesGr(0).Nodes(0) : ReDim IFNodesGr(0).Nodes(0)
      Dim UpperLimit As Long, Div As Long

      Dim iDelta As Double, jTheta As Double, jYChord As Double, jdDelta As Double, jZChord As Double, jXChord As Double
      Dim jLChord As Double, jRChord As Double, ijDelta As Double, RChordMid As Double
      Dim sNode As NodeCoOrds, iNode As NodeCoOrds
      Dim eNodeRB As NodeCoOrds

      UpperLimit = IFMesh.NrTop
      Div = IFMesh.Nd
      For I = 0 To 0
         iDelta = GD.sDelta + (I * GD.Omega)
         ' Store the Node Number and Coords of First Node of the Segment
         With IFNodesGr(0).Nodes(UBound(IFNodesGr(0).Nodes))
            sNodeCalc(sNode, Div, iDelta, IFMesh, Component.Wall)
            .x = sNode.x : .y = sNode.y : .z = sNode.z
         End With
         ReDim Preserve IFNodesGr(0).Nodes(UBound(IFNodesGr(0).Nodes) + 1)
         For J = 1 To 1
            jTheta = J * (GD.Omega / UpperLimit)
            jdDelta = iDelta + jTheta
            iNodeCalc(iNode, Div, jdDelta, IFMesh, Component.Wall)
            jXChord = iNode.x : jYChord = iNode.y : jZChord = iNode.z
            With IFNodesGr(0).Nodes(UBound(IFNodesGr(0).Nodes))
               .x = jXChord : .y = jYChord : .z = jZChord
            End With
            ReDim Preserve IFNodesGr(0).Nodes(UBound(IFNodesGr(0).Nodes) + 1)
         Next J
      Next I
      ReDim Preserve IFNodesGr(0).Nodes(UBound(IFNodesGr(0).Nodes) - 1)
      ' Create droop nodes for the all the segments
      For I = 0 To 0
         iDelta = GD.sDelta + (I * GD.Omega)
         eNodeRB.x = (GD.dTank / 2) * Math.Cos(iDelta) : eNodeRB.y = BP.eNodeRB.y : eNodeRB.z = (GD.dTank / 2) * Math.Sin(iDelta)
         With OSGeometry
            RChordMid = Math.Sqrt((GD.dTank / 2) ^ 2 - GD.LHalfChord ^ 2)
            With DroopNodesGr(0).Nodes(UBound(DroopNodesGr(0).Nodes))
               .x = eNodeRB.x : .y = eNodeRB.y : .z = eNodeRB.z
            End With
            For J = 1 To 1
               With GD
                  jTheta = J * (.Theta / IFMesh.NrBot)
                  If jTheta <= .Theta / 2 Then
                     jLChord = .rDroop * Math.Sin(.Theta / 2 - jTheta)
                     jYChord = eNodeRB.y + (.rDroop - .hDroop) - .rDroop * Math.Cos(.Theta / 2 - jTheta)
                  Else
                     jLChord = .rDroop * Math.Sin(jTheta - .Theta / 2)
                     jYChord = eNodeRB.y + (.rDroop - .hDroop) - .rDroop * Math.Cos(jTheta - .Theta / 2)
                  End If
                  jRChord = Math.Sqrt(RChordMid ^ 2 + jLChord ^ 2)
                  jdDelta = Math.Atan(jLChord / RChordMid)
                  If jTheta <= .Theta / 2 Then
                     ijDelta = iDelta + .Omega / 2 - jdDelta
                  Else
                     ijDelta = iDelta + .Omega / 2 + jdDelta
                  End If
                  jZChord = (.dTank / 2) * Math.Sin(ijDelta)
                  jXChord = (.dTank / 2) * Math.Cos(ijDelta)
                  ReDim Preserve DroopNodesGr(0).Nodes(UBound(DroopNodesGr(0).Nodes) + 1)
                  With DroopNodesGr(0).Nodes(UBound(DroopNodesGr(0).Nodes))
                     .x = jXChord : .y = jYChord : .z = jZChord
                  End With
               End With
            Next J
            ReDim Preserve DroopNodesGr(0).Nodes(UBound(DroopNodesGr(0).Nodes) + 1)
         End With
      Next I
      ReDim Preserve DroopNodesGr(0).Nodes(UBound(DroopNodesGr(0).Nodes) - 1)
      For I = 1 To 1
         Dim vP1TovP2 As Double, cP1TocP2 As Double
         Dim vP1 As NodeCoOrds, vP2 As NodeCoOrds 'Points in droop depth direction
         Dim cP1 As NodeCoOrds, cP2 As NodeCoOrds 'Points on droop curve
         vP1 = IFNodesGr(0).Nodes(I) : vP2 = DroopNodesGr(0).Nodes(I)
         cP1 = DroopNodesGr(0).Nodes(I) : cP2 = DroopNodesGr(0).Nodes(I - 1)
         vP1TovP2 = Math.Sqrt((vP1.x - vP2.x) ^ 2 + (vP1.y - vP2.y) ^ 2 + (vP1.z - vP2.z) ^ 2)
         cP1TocP2 = Math.Sqrt((cP1.x - cP2.x) ^ 2 + (cP1.y - cP2.y) ^ 2 + (cP1.z - cP2.z) ^ 2)
         If cP1TocP2 / vP1TovP2 > GD.AspectRatio Then
            AdjHeight = cP1TocP2 / GD.AspectRatio
         Else
            AdjHeight = 0.0#
         End If
      Next I
   End Sub
   Public Sub GenerateIFMesh(ByVal AdjHeight As Double,
                          ByRef pGr() As Integer,
                          ByRef NodesGr() As NodeCoOrdsGroup,
                          ByRef IFNodesGr() As NodeCoOrdsGroup,
                          ByVal GrName As String,
                          ByRef SeqNodeNum As Long,
                          ByRef SeqPlateNum As Long,
                          ByVal Comp As Component)
      ReDim NodesGr(0) : ReDim NodesGr(0).Nodes(0) : ReDim pGr(0)
      Dim Div As Long, Trans As Long, TrType As TransType, Node As Integer
      Dim nNodes As Long 'Number of nodes to iterate for plates creation at each radial beam
      Dim iDelta As Double, jTheta As Double, jYChord As Double, jdDelta As Double, jZChord As Double, jXChord As Double
      Dim jLChord As Double, jRChord As Double, ijDelta As Double, RChordMid As Double
      Dim eNodeRB As NodeCoOrds, PL As Plate
      Dim LNodes() As NodeCoOrds, RNodes() As NodeCoOrds
      Dim P1 As NodeCoOrds, P2 As NodeCoOrds, P1P2 As Double, nDiv As Long

      nNodes = CLng(UBound(IFNodesGr(0).Nodes) / GD.nRB)
      ' Create droop nodes for the all the segments
      For I = 0 To GD.nRB - 1
         iDelta = GD.sDelta + (I * GD.Omega)
         eNodeRB.x = (GD.dTank / 2) * Math.Cos(iDelta)
         eNodeRB.y = BP.eNodeRB.y
         eNodeRB.z = (GD.dTank / 2) * Math.Sin(iDelta)
         With OSGeometry
            RChordMid = Math.Sqrt((GD.dTank / 2) ^ 2 - GD.LHalfChord ^ 2)
            If AdjHeight <> 0.0# Then
               Call CreateNewNode(SeqNodeNum, NodesGr, 0, eNodeRB.x, eNodeRB.y, eNodeRB.z)
            Else
               NodesGr(0).Nodes(UBound(NodesGr(0).Nodes)) = IFNodesGr(0).Nodes(CInt(I * nNodes))
               ReDim Preserve NodesGr(0).Nodes(UBound(NodesGr(0).Nodes) + 1)
            End If
            For J = 1 To nNodes - 1
               With GD
                  jTheta = J * (.Theta / nNodes)
                  If jTheta <= .Theta / 2 Then
                     jLChord = .rDroop * Math.Sin(.Theta / 2 - jTheta)
                     jYChord = eNodeRB.y + (.rDroop - .hDroop) - .rDroop * Math.Cos(.Theta / 2 - jTheta)
                  Else
                     jLChord = .rDroop * Math.Sin(jTheta - .Theta / 2)
                     jYChord = eNodeRB.y + (.rDroop - .hDroop) - .rDroop * Math.Cos(jTheta - .Theta / 2)
                  End If
                  jRChord = Math.Sqrt(RChordMid ^ 2 + jLChord ^ 2)
                  jdDelta = Math.Atan(jLChord / RChordMid)
                  If jTheta <= .Theta / 2 Then
                     ijDelta = iDelta + .Omega / 2 - jdDelta
                  Else
                     ijDelta = iDelta + .Omega / 2 + jdDelta
                  End If
                  jZChord = (.dTank / 2) * Math.Sin(ijDelta)
                  jXChord = (.dTank / 2) * Math.Cos(ijDelta)
                  Call CreateNewNode(SeqNodeNum, NodesGr, 0, jXChord, jYChord, jZChord)
               End With
            Next J
            ' ReDim Preserve NodesGr(0).Nodes(UBound(NodesGr(0).Nodes) + 1)
         End With
      Next I
      ReDim Preserve NodesGr(0).Nodes(UBound(NodesGr(0).Nodes) - 1)
      For I = 0 To GD.nRB - 1
         ReDim LNodes(0) : ReDim RNodes(0)
         For J = 0 To nNodes - 1
            SeqPlateNum = SeqPlateNum + 1
            If J = 0 Then
               If AdjHeight <> 0.0# Then
                  ReDim LNodes(0 To 1)
                  LNodes(0) = IFNodesGr(0).Nodes(CInt(I * nNodes + J))
                  LNodes(1) = NodesGr(0).Nodes(CInt(I * nNodes + J))
               Else
                  LNodes(0) = IFNodesGr(0).Nodes(CInt(I * nNodes + J))
               End If
            End If
            ' Adjust Right Nodes for the Last Radial Beam Segment
            If I = GD.nRB - 1 And J = nNodes - 1 Then
               RNodes(0) = IFNodesGr(0).Nodes(0)
            Else
               RNodes(0) = IFNodesGr(0).Nodes(CInt(I * nNodes + J + 1))
            End If
            If J < nNodes - 1 Then
               P1 = IFNodesGr(0).Nodes(CInt(I * nNodes + J + 1)) : P2 = NodesGr(0).Nodes(CInt(I * nNodes + J + 1))
               P1P2 = Math.Sqrt((P1.x - P2.x) ^ 2 + (P1.y - P2.y) ^ 2 + (P1.z - P2.z) ^ 2)
               nDiv = CeilingEx(P1P2 / GD.MaxPlateDim, 1) - 1
               If nDiv > 0 Then
                  For Div = 1 To nDiv
                     ' Below (9 Lines) can possibly be refactored. Look in to it.
                     ReDim Preserve RNodes(UBound(RNodes) + 1)
                     With RNodes(UBound(RNodes))
                        SeqNodeNum = SeqNodeNum + 1
                        .No = SeqNodeNum
                        .x = P1.x + Div * (P2.x - P1.x) / (nDiv + 1)
                        .y = P1.y + Div * (P2.y - P1.y) / (nDiv + 1)
                        .z = P1.z + Div * (P2.z - P1.z) / (nDiv + 1)
                        OSGeometry.CreateNode(SeqNodeNum, .x, .y, .z)
                     End With
                  Next Div
               End If
            End If
            If J < nNodes - 1 Then
               ReDim Preserve RNodes(UBound(RNodes) + 1)
               RNodes(UBound(RNodes)) = NodesGr(0).Nodes(CInt(I * nNodes + J + 1))
            Else
               If AdjHeight <> 0.0# Then
                  ' Adjust Right Nodes for the Last Radial Beam Segment
                  If I = GD.nRB - 1 Then
                     ReDim Preserve RNodes(UBound(RNodes) + 1)
                     RNodes(UBound(RNodes)) = NodesGr(0).Nodes(0)
                  Else
                     ReDim Preserve RNodes(UBound(RNodes) + 1)
                     RNodes(UBound(RNodes)) = NodesGr(0).Nodes(CInt(I * nNodes + J + 1))
                  End If
               End If
            End If
            ' Identify the transitions and generate the mesh accordingly
            Trans = UBound(RNodes) - UBound(LNodes)
            If Trans > 0 Then
               TrType = TransType.Inc
            ElseIf Trans < 0 Then
               TrType = TransType.Dec
            Else
               TrType = TransType.None
            End If

            Select Case TrType
               ' Generate mesh for increasing transition
               Case TransType.Inc
                  If UBound(LNodes) = 0 Then
                     For Node = 0 To UBound(RNodes) - 1
                        With PL
                           PL.NodeA = LNodes(0).No : PL.NodeB = RNodes(CInt(Node)).No
                           PL.NodeC = RNodes(CInt(Node + 1)).No : PL.NodeD = .NodeA
                        End With
                        Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     Next Node
                  Else
                     For Node = 0 To UBound(LNodes)
                        With PL
                           .NodeA = LNodes(Node).No : .NodeB = RNodes(Node).No
                           .NodeC = RNodes(Node + 1).No : .NodeD = .NodeA
                        End With
                        Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                        If Node < UBound(LNodes) Then
                           With PL
                              .NodeA = LNodes(Node).No : .NodeB = RNodes(Node + 1).No
                              .NodeC = LNodes(Node + 1).No : .NodeD = .NodeA
                           End With
                           Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                        End If
                     Next Node
                  End If
                  ' Generate mesh for decreasing transition
               Case TransType.Dec
                  If UBound(RNodes) = 0 Then
                     For Node = 0 To UBound(LNodes) - 1
                        With PL
                           .NodeA = LNodes(Node).No : .NodeB = RNodes(0).No
                           .NodeC = LNodes(Node + 1).No : .NodeD = .NodeA
                        End With
                        Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     Next Node
                  Else
                     For Node = 0 To UBound(RNodes)
                        With PL
                           .NodeA = LNodes(Node).No : .NodeB = RNodes(Node).No
                           .NodeC = LNodes(Node + 1).No : .NodeD = .NodeA
                        End With
                        Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                        If Node < UBound(RNodes) Then
                           With PL
                              .NodeA = LNodes(Node + 1).No : .NodeB = RNodes(Node).No
                              .NodeC = RNodes(Node + 1).No : .NodeD = .NodeA
                           End With
                           Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                        End If
                     Next Node
                  End If
                  ' Generate mesh when there is no transition required
               Case TransType.None
                  For Node = 0 To UBound(LNodes) - 1
                     With PL
                        .NodeA = LNodes(Node).No : .NodeB = RNodes(Node).No
                        .NodeC = RNodes(Node + 1).No : .NodeD = LNodes(Node + 1).No
                     End With
                     Call CreateNewPlate(SeqPlateNum, pGr, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                  Next Node
            End Select
            ' Transfer Right Nodes to Left Nodes and Redimension the Right Nodes
            ReDim LNodes(UBound(RNodes))
            LNodes = RNodes
            ReDim RNodes(0)
         Next J
      Next I
      ReDim Preserve pGr(UBound(pGr) - 1)
      ' Create Group of the Selected Plate Elements
      OSGeometry.CreateGroupEx(GroupType.Plates, "_" & GrName, UBound(pGr) + 1, pGr)
   End Sub
   Public Sub GenerateBeams(ByRef NodesGr() As NodeCoOrdsGroup,
                            ByRef Beams() As Beam,
                            ByRef SeqPlateNum As Long)
      Dim Div As Integer, nDiv As Integer, snNodes As Integer, enNodes As Integer
      ReDim Beams(0)
      Array.Reverse(NodesGr)
      nDiv = UBound(NodesGr)
      '/ Upper Bounds of the Node Groups are NOT reduced by '1'. Make the necessary corrections at all the locations
      '/ when this reduction is made
      For Div = 0 To nDiv - 1
         snNodes = CInt((UBound(NodesGr(Div).Nodes) + 1) / GD.nRB)
         enNodes = CInt((UBound(NodesGr(Div + 1).Nodes) + 1) / GD.nRB)
         For I = 0 To GD.nRB - 1
            SeqPlateNum = SeqPlateNum + 1
            With Beams(UBound(Beams))
               .No = SeqPlateNum
               .sNode = NodesGr(Div).Nodes(CInt(I * snNodes))
               .eNode = NodesGr(Div + 1).Nodes(CInt(I * enNodes))
               OSGeometry.CreateBeam(.No, .sNode.No, .eNode.No)
            End With
            ReDim Preserve Beams(UBound(Beams) + 1)
         Next I
      Next Div
      Array.Reverse(NodesGr)
      ReDim Preserve Beams(UBound(Beams) - 1)
   End Sub
   Private Sub GeneratePlates(ByRef NodesGr() As NodeCoOrdsGroup,
                           ByRef PlateGroup() As Integer,
                           ByRef SeqPlateNum As Long,
                           Optional ByVal Com As Component = Component.None,
                           Optional ByVal BPOmega As Double = vbEmpty)
      Dim Div As Integer, nDiv As Integer, snNodes As Integer, enNodes As Integer, NodesGrRev() As NodeCoOrdsGroup, nCounter As Integer
      Dim PL As Plate
      ReDim PlateGroup(0)
      ReDim NodesGrRev(UBound(NodesGr))
      nDiv = UBound(NodesGr)
      If (Com = Component.CCTop) Or (Com = Component.BPOuter) Then
         NodesGrRev = NodesGr
      Else
         '/-------------- Possible Correction Area -----------/
         '/-------------- Check for Array.Reverse ------------/
         For Div = 0 To nDiv
            NodesGrRev(Div) = NodesGr(nDiv - Div)
         Next Div
      End If
      '/ Upper Bounds of the Node Groups are NOT reduced by '1'. Make the necessary corrections at all the locations
      '/ when this reduction is made
      For Div = 0 To nDiv - 1
         If ((Com = Component.BPInner) And (GD.idBP = 0.0#)) Or ((Com = Component.CPInner) And (GD.idCP = 0.0#)) Then
            nCounter = CInt((TOTAL_ANGLE / BPOmega))
         Else
            nCounter = CInt(GD.nRB)
         End If
         snNodes = CInt((UBound(NodesGrRev(Div).Nodes) + 1) / nCounter)
         enNodes = CInt((UBound(NodesGrRev(Div + 1).Nodes) + 1) / nCounter)
         If snNodes < enNodes Then
            For I = 0 To (nCounter - 1)
               With PL
                  For J = 0 To snNodes
                     If (I * snNodes + J) = UBound(NodesGrRev(Div).Nodes) Then
                        .NodeA = NodesGrRev(Div).Nodes(0).No
                        .NodeC = NodesGrRev(Div + 1).Nodes(0).No
                     Else
                        .NodeA = NodesGrRev(Div).Nodes(I * snNodes + J).No
                        .NodeC = NodesGrRev(Div + 1).Nodes(I * snNodes + J + 1 + I).No
                     End If
                     .NodeB = NodesGrRev(Div + 1).Nodes(I * snNodes + J + I).No
                     .NodeD = .NodeA
                     Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     If J <> snNodes Then
                        .NodeA = NodesGrRev(Div).Nodes(I * snNodes + J).No : .NodeB = NodesGrRev(Div + 1).Nodes(I * snNodes + (J + 1) + I).No
                        If (I * snNodes + J) = UBound(NodesGrRev(Div).Nodes) - 1 Then
                           .NodeC = NodesGrRev(Div).Nodes(0).No
                        Else
                           .NodeC = NodesGrRev(Div).Nodes(I * snNodes + J + 1).No
                        End If
                        .NodeD = .NodeA
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     End If
                  Next J
               End With
            Next I
         ElseIf snNodes > enNodes Then
            '/ Implement the code for reduced transition
            '/ Implemented on 02-Aug-2018
            For I = 0 To (nCounter - 1)
               With PL
                  For J = 0 To (snNodes - 1)
                     .NodeA = NodesGrRev(Div).Nodes(I * snNodes + J).No
                     If (I * snNodes + J) = UBound(NodesGrRev(Div).Nodes) - 1 Then
                        .NodeB = NodesGrRev(Div + 1).Nodes(0).No
                        .NodeC = NodesGrRev(Div).Nodes(0).No
                     Else
                        .NodeB = NodesGrRev(Div + 1).Nodes(I * snNodes + J - I).No
                        .NodeC = NodesGrRev(Div).Nodes(I * snNodes + J + 1).No
                     End If
                     .NodeD = .NodeA
                     Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     If J <> (snNodes - 1) Then
                        .NodeA = NodesGrRev(Div + 1).Nodes(I * snNodes + J - I).No
                        If (I * snNodes + J) = UBound(NodesGrRev(Div).Nodes) - 2 Then
                           .NodeB = NodesGrRev(Div + 1).Nodes(0).No
                        Else
                           .NodeB = NodesGrRev(Div + 1).Nodes(I * snNodes + (J + 1) - I).No
                        End If
                        .NodeC = NodesGrRev(Div).Nodes(I * snNodes + J + 1).No
                        .NodeD = .NodeA
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     End If
                  Next J
               End With
            Next I
         Else
            For I = 0 To (nCounter - 1)
               For J = 0 To snNodes - 1
                  With PL
                     .NodeA = NodesGrRev(Div).Nodes(I * snNodes + J).No : .NodeB = NodesGrRev(Div + 1).Nodes(I * snNodes + J).No
                     If (I * snNodes + J) = UBound(NodesGrRev(Div).Nodes) - 1 Then
                        .NodeC = NodesGrRev(Div + 1).Nodes(0).No : .NodeD = NodesGrRev(Div).Nodes(0).No
                     Else
                        .NodeC = NodesGrRev(Div + 1).Nodes(I * snNodes + J + 1).No : .NodeD = NodesGrRev(Div).Nodes(I * snNodes + J + 1).No
                     End If
                  End With
                  Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
               Next J
            Next I
         End If
      Next Div
      ReDim Preserve PlateGroup(UBound(PlateGroup) - 1)
   End Sub
   '/---- In the signature iDiv without access modifier changed to ByRef
   Private Sub GenerateNodes(ByVal iBDiv As Double,
                             ByRef iDiv As Integer,
                             ByRef D() As Double,
                             ByRef NodesGr() As NodeCoOrdsGroup,
                             ByRef ieNode() As NodeCoOrds,
                             ByRef SeqNodeNum As Long,
                             ByRef Lres As Double,
                             ByVal nSegs As Long,
                             ByVal Com As Component,
                             Optional ByVal BPOmega As Double = vbEmpty,
                             Optional ByVal yDiff As Double = vbEmpty) 'Newly added optional parameter
      Dim iR As Double, iLa As Double, inSegs As Long, iDelta As Double, ijDelta As Double, jXChord As Double
      Dim jYChord As Double, jZChord As Double, ihDroop As Double, iRDroop As Double, iQDroop As Double, RChordMid As Double
      Dim jTheta As Double, jLChord As Double, jRChord As Double, jdDelta As Double, nBPSegs As Long
      Dim iLc As Double
      '/ Skip increasing iDiv value in case of CR Stiffener depending on the case
      iDiv = iDiv + 1
ReduceDivWidth:
      If Com = Component.FloorPlate Then
         ReDim Preserve D(iDiv)
         D(iDiv) = D(CInt(iDiv - 1)) - (2 * iBDiv) * Math.Cos(GD.Alpha)
         ihDroop = (GD.hDroop / (GD.dTank - GD.dCR)) * (D(iDiv) - GD.dCR)
         iR = D(iDiv) / 2
      ElseIf (Com = Component.CCTop) Or (Com = Component.CCBot) Then
         iR = GD.dCC / 2
      ElseIf (Com = Component.CRVertAnnulus) Then
         iR = GD.dCR / 2
      ElseIf Com = Component.CompRing Then
         ReDim Preserve D(iDiv)
         D(iDiv) = D(iDiv - 1) - (2 * iBDiv)
         iR = D(iDiv) / 2
      ElseIf (Com = Component.UFCone) Then
         ReDim Preserve D(iDiv)
         D(iDiv) = D(iDiv - 1) - (2 * iBDiv) * Math.Cos(GD.Beta)
         iR = D(iDiv) / 2
      ElseIf (Com = Component.BPOuter) Or (Com = Component.CPOuter) Or (Com = Component.BPBolts) Then
         ReDim Preserve D(iDiv)
         D(iDiv) = D(iDiv - 1) + (2 * iBDiv)
         iR = D(iDiv) / 2
      ElseIf (Com = Component.BPInner) Or (Com = Component.CPInner) Then
         ReDim Preserve D(iDiv)
         D(iDiv) = D(iDiv - 1) - (2 * iBDiv)
         iR = D(iDiv) / 2
      ElseIf (Com = Component.CRVAS) Then
         REM: To be implemented
         ReDim Preserve D(iDiv)
         D(iDiv) = D(iDiv - 1) - (2 * Math.Sqrt(iBDiv ^ 2 - yDiff ^ 2))
         iR = D(iDiv) / 2
      End If
      If Com = Component.FloorPlate Then
         If ihDroop <= Precision Then
            iLa = D(iDiv) / 2 * GD.Omega
            inSegs = CLng(MaxEx(Math.Ceiling(iLa / MinEx(iBDiv * GD.AspectRatio, GD.MaxPlateDim)), nSegs - 1, nPlatesMin))
         Else
            iLc = 2 * (iR) * Math.Sin(GD.Omega / 2)
            iRDroop = (ihDroop ^ 2 + (iLc / 2) ^ 2) / (2 * ihDroop)
            iQDroop = 2 * Math.Asin((iLc / 2) / iRDroop)
            iLa = iRDroop * iQDroop
            inSegs = CLng(MaxEx(Math.Ceiling(MaxEx(iLa / MinEx(iBDiv * GD.AspectRatio, GD.MaxPlateDim))), nSegs - 1, nPlatesMin))
         End If
      Else
         iLa = iR * GD.Omega
         If ((Com = Component.BPInner) And (GD.idBP = 0.0#)) Or ((Com = Component.CPInner) And (GD.idCP = 0.0#)) Then
            inSegs = nSegs - 1
            nBPSegs = CLng((TOTAL_ANGLE / BPOmega))
            BPOmega = Deg2Rad(BPOmega)
         ElseIf (Com = Component.CRVAS) Then
            '/ To be implemented
            inSegs = nSegs
         Else
            inSegs = CLng(MaxEx(Math.Ceiling(MaxEx(iLa / MinEx(iBDiv * GD.AspectRatio, GD.MaxPlateDim))), nSegs - 1, nPlatesMin))
         End If
      End If
      If Com = Component.BPInner Then
         If GD.idBP <> 0.0# Then
            If iBDiv > (iLa / inSegs) * GD.AspectRatio Then
               iBDiv = (iLa / inSegs) * GD.AspectRatio
               GoTo ReduceDivWidth
            End If
         End If
      ElseIf Com = Component.CPInner Then
         If GD.idCP <> 0.0# Then
            If iBDiv > (iLa / inSegs) * GD.AspectRatio Then
               iBDiv = (iLa / inSegs) * GD.AspectRatio
               GoTo ReduceDivWidth
            End If
         End If
      Else
         If iBDiv > (iLa / inSegs) * GD.AspectRatio Then
            iBDiv = (iLa / inSegs) * GD.AspectRatio
            GoTo ReduceDivWidth
         End If
      End If

      ReDim Preserve NodesGr(iDiv)
      ReDim Preserve NodesGr(iDiv).Nodes(0)
      ReDim Preserve ieNode(iDiv)

      If ((Com = Component.BPInner) And (GD.idBP = 0.0#)) Or ((Com = Component.CPInner) And (GD.idCP = 0.0#)) Then
         For I = 0 To nBPSegs - 1
            If inSegs = 0 Then
               If I = 0 Then
                  iDelta = GD.sDelta + (I * BPOmega)
                  ieNode(iDiv).x = iR * Math.Cos(iDelta)
                  ieNode(iDiv).y = ieNode(iDiv - 1).y
                  ieNode(iDiv).z = iR * Math.Sin(iDelta)
                  Call CreateNewNode(SeqNodeNum, NodesGr, iDiv, ieNode(iDiv).x, ieNode(iDiv).y, ieNode(iDiv).z)
               Else
                  NodesGr(iDiv).Nodes(UBound(NodesGr(iDiv).Nodes)) = NodesGr(iDiv).Nodes(UBound(NodesGr(iDiv).Nodes) - 1)
                  ReDim Preserve NodesGr(iDiv).Nodes(UBound(NodesGr(iDiv).Nodes) + 1)
               End If
            Else
               iDelta = GD.sDelta + (I * BPOmega)
               ieNode(iDiv).x = iR * Math.Cos(iDelta)
               ieNode(iDiv).y = ieNode(iDiv - 1).y
               ieNode(iDiv).z = iR * Math.Sin(iDelta)
               Call CreateNewNode(SeqNodeNum, NodesGr, iDiv, ieNode(iDiv).x, ieNode(iDiv).y, ieNode(iDiv).z)
               For J = 1 To inSegs - 1
                  ijDelta = GD.sDelta + (I * BPOmega) + (J * BPOmega / inSegs)
                  jXChord = iR * Math.Cos(ijDelta)
                  jYChord = ieNode(iDiv).y
                  jZChord = iR * Math.Sin(ijDelta)
                  Call CreateNewNode(SeqNodeNum, NodesGr, iDiv, jXChord, jYChord, jZChord)
               Next J
            End If
         Next I
      Else
         For I = 0 To GD.nRB - 1
            iDelta = GD.sDelta + (I * GD.Omega)
            ieNode(iDiv).x = iR * Math.Cos(iDelta)
            If Com = Component.FloorPlate Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y - iBDiv * Math.Sin(GD.Alpha)
            ElseIf Com = Component.CompRing Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y
            ElseIf Com = Component.UFCone Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y - iBDiv * Math.Sin(GD.Beta)
            ElseIf Com = Component.CCBot Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y - iBDiv
            ElseIf Com = Component.CRVertAnnulus Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y - iBDiv
            ElseIf Com = Component.CCTop Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y + iBDiv
            ElseIf (Com = Component.BPOuter) Or (Com = Component.BPInner) Or (Com = Component.BPBolts) Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y
            ElseIf (Com = Component.CPOuter) Or (Com = Component.CPInner) Then
               ieNode(iDiv).y = ieNode(iDiv - 1).y
            ElseIf (Com = Component.CRVAS) Then
               '/ To be implemented
               ieNode(iDiv).y = ieNode(iDiv - 1).y - yDiff
            End If
            ieNode(iDiv).z = iR * Math.Sin(iDelta)
            Call CreateNewNode(SeqNodeNum, NodesGr, iDiv, ieNode(iDiv).x, ieNode(iDiv).y, ieNode(iDiv).z)
            RChordMid = Math.Sqrt(iR ^ 2 - (iLc / 2) ^ 2)
            For J = 1 To inSegs - 1
               With GD
                  If Com = Component.FloorPlate Then
                     If ihDroop <= Precision Then
                        ijDelta = iDelta + J * .Omega / inSegs
                        jXChord = (iR) * Math.Cos(ijDelta)
                        jYChord = ieNode(iDiv).y
                        jZChord = (iR) * Math.Sin(ijDelta)
                     Else
                        jTheta = J * (iQDroop / inSegs)
                        If (jTheta - iQDroop / 2) <= Precision Then
                           jLChord = iRDroop * Math.Sin(iQDroop / 2 - jTheta)
                           jYChord = ieNode(iDiv).y + (iRDroop - ihDroop) - iRDroop * Math.Cos(iQDroop / 2 - jTheta)
                        Else
                           jLChord = iRDroop * Math.Sin(jTheta - iQDroop / 2)
                           jYChord = ieNode(iDiv).y + (iRDroop - ihDroop) - iRDroop * Math.Cos(jTheta - iQDroop / 2)
                        End If
                        jRChord = Math.Sqrt(RChordMid ^ 2 + jLChord ^ 2)
                        jdDelta = Math.Atan(jLChord / RChordMid)
                        If (jTheta - iQDroop / 2) <= Precision Then
                           ijDelta = iDelta + .Omega / 2 - jdDelta
                        Else
                           ijDelta = iDelta + .Omega / 2 + jdDelta
                        End If
                        jXChord = iR * Math.Cos(ijDelta)
                        jZChord = iR * Math.Sin(ijDelta)
                     End If
                  Else
                     ijDelta = .sDelta + (I * .Omega) + (J * .Omega / inSegs)
                     jXChord = iR * Math.Cos(ijDelta)
                     jYChord = ieNode(iDiv).y
                     jZChord = iR * Math.Sin(ijDelta)
                  End If
                  Call CreateNewNode(SeqNodeNum, NodesGr, iDiv, jXChord, jYChord, jZChord)
               End With
            Next J
         Next I
         Lres = (Lres - iBDiv)
      End If
   End Sub
   Public Sub GenerateCompMesh(ByRef NodesGr() As NodeCoOrdsGroup,
                            ByRef IFNodesGr() As NodeCoOrdsGroup,
                            ByRef pGr() As Integer,
                            ByRef SeqNodeNum As Long,
                            ByRef SeqPlateNum As Long,
                            ByVal Com As Component,
                            Optional ByVal OD As Double = vbEmpty,
                            Optional ByVal ID As Double = vbEmpty,
                            Optional ByVal Height As Double = vbEmpty)
      Dim Lres As Double, iBDiv As Double, nDivRes As Long, D() As Double, iDiv As Integer, Div As Long
      Dim ieNode() As NodeCoOrds, MaxDim As Double, K As Integer, nSegs As Long, BPSeg As Long, BPOmega As Double
      Dim P1 As NodeCoOrds, P2 As NodeCoOrds
      ReDim D(0) : ReDim NodesGr(0) : ReDim ieNode(0)
      If Com = Component.CCBot Then
         If Height = vbEmpty Then
            Lres = GD.hCC
         Else
            Lres = Height
         End If
      ElseIf Com = Component.CCTop Then
         If Height = vbEmpty Then
            Lres = GD.hCCTop
         Else
            Lres = Height
         End If
      ElseIf Com = Component.CRVertAnnulus Then
         If Height = vbEmpty Then
            Lres = GD.hCR
         Else
            Lres = Height
         End If
      ElseIf Com = Component.UFCone Then
         If OD = vbEmpty Then OD = GD.dUF
         If ID = vbEmpty Then ID = GD.dCC
         D(0) = OD
         Lres = (OD - ID) / (2 * Math.Cos(GD.Beta))
      ElseIf Com = Component.CompRing Then
         If OD = vbEmpty Then OD = GD.dCR
         If ID = vbEmpty Then ID = GD.dUF
         D(0) = OD
         Lres = (OD - ID) / 2
      ElseIf Com = Component.FloorPlate Then
         If OD = vbEmpty Then OD = GD.dTank
         If ID = vbEmpty Then ID = GD.dCR
         D(0) = OD
         Lres = (OD - ID) / (2 * Math.Cos(GD.Alpha))
      ElseIf Com = Component.BPOuter Then
         If OD = vbEmpty Then OD = GD.pcdBolts
         If ID = vbEmpty Then ID = GD.dCC
         D(0) = ID
         Lres = (OD - ID) / 2
      ElseIf Com = Component.BPBolts Then
         If OD = vbEmpty Then OD = GD.odBP
         If ID = vbEmpty Then ID = GD.pcdBolts
         D(0) = ID
         Lres = (OD - ID) / 2
      ElseIf Com = Component.BPInner Then
         If OD = vbEmpty Then OD = GD.dCC
         If ID = vbEmpty Then ID = GD.idBP
         D(0) = OD
         Lres = (OD - ID) / 2
      ElseIf Com = Component.CPOuter Then
         If OD = vbEmpty Then OD = GD.odCP
         If ID = vbEmpty Then ID = GD.dCC
         D(0) = ID
         Lres = (OD - ID) / 2
      ElseIf Com = Component.CPInner Then
         If OD = vbEmpty Then OD = GD.dCC
         If ID = vbEmpty Then ID = GD.idCP
         D(0) = OD
         Lres = (OD - ID) / 2
      End If

      iDiv = 0
      NodesGr(UBound(NodesGr)) = IFNodesGr(UBound(IFNodesGr))
      ieNode(0).y = NodesGr(0).Nodes(0).y
      If ((Com = Component.BPInner) And (GD.idBP = 0.0#)) Or ((Com = Component.CPInner) And (GD.idCP = 0.0#)) Then
         If (Lres <> 0) Then
            '/ Below (7 lines) has scope for refactoring. Look in to it later.
            nSegs = CLng((UBound(NodesGr(iDiv).Nodes) + 1) / GD.nRB)
            P1 = NodesGr(iDiv).Nodes(0) : P2 = NodesGr(iDiv).Nodes(1)
            MaxDim = Math.Sqrt((P2.x - P1.x) ^ 2 + (P2.y - P1.y) ^ 2 + (P2.z - P1.z) ^ 2)
            For K = 2 To CInt(nSegs)
               P1 = NodesGr(iDiv).Nodes(K - 1) : P2 = NodesGr(iDiv).Nodes(K)
               MaxDim = MaxEx(MaxDim, Math.Sqrt((P2.x - P1.x) ^ 2 + (P2.y - P1.y) ^ 2 + (P2.z - P1.z) ^ 2))
            Next K
            nDivRes = CeilingEx(Lres / MaxDim, 1)
            iBDiv = Lres / nDivRes
            BPOmega = (TOTAL_ANGLE / (GD.nRB * nPlatesMin)) * nDivRes
            For BPSeg = 0 To (nDivRes - 1)
               GenerateNodes(iBDiv, iDiv, D, NodesGr, ieNode, SeqNodeNum, Lres, (nDivRes - BPSeg), Com, BPOmega)
            Next BPSeg
         End If
      Else
         Do While (Lres <> 0)
            nSegs = CLng((UBound(NodesGr(iDiv).Nodes) + 1) / GD.nRB)
            P1 = NodesGr(iDiv).Nodes(0) : P2 = NodesGr(iDiv).Nodes(1)
            MaxDim = Math.Sqrt((P2.x - P1.x) ^ 2 + (P2.y - P1.y) ^ 2 + (P2.z - P1.z) ^ 2)
            For K = 2 To CInt(nSegs)
               P1 = NodesGr(iDiv).Nodes(K - 1) : P2 = NodesGr(iDiv).Nodes(K)
               MaxDim = MaxEx(MaxDim, Math.Sqrt((P2.x - P1.x) ^ 2 + (P2.y - P1.y) ^ 2 + (P2.z - P1.z) ^ 2))
            Next K
            iBDiv = MinEx(MaxDim * GD.AspectRatio, GD.MaxPlateDim)
            nDivRes = CeilingEx(Lres / iBDiv, 1)
            If nDivRes = 1 Then
               iBDiv = Lres
               GenerateNodes(iBDiv, iDiv, D, NodesGr, ieNode, SeqNodeNum, Lres, nSegs, Com)
            ElseIf nDivRes = 2 Then
               iBDiv = Lres / nDivRes
               For Div = 1 To nDivRes
                  GenerateNodes(iBDiv, iDiv, D, NodesGr, ieNode, SeqNodeNum, Lres, nSegs, Com)
               Next Div
            Else
               iBDiv = iBDiv
               GenerateNodes(iBDiv, iDiv, D, NodesGr, ieNode, SeqNodeNum, Lres, nSegs, Com)
            End If
         Loop
      End If
      If ((Com = Component.BPInner) And (GD.idBP = 0.0#)) Or ((Com = Component.CPInner) And (GD.idCP = 0.0#)) Then
         Call GeneratePlates(NodesGr, pGr, SeqPlateNum, Com, BPOmega)
      Else
         Call GeneratePlates(NodesGr, pGr, SeqPlateNum, Com)
      End If
   End Sub
   '/ New Code here onwards
   Public Sub GenerateVAStiffenerMesh(ByRef NodesGr() As NodeCoOrdsGroup,
                                   ByRef NodesCR() As NodeCoOrdsGroup,
                                   ByRef NodesVA() As NodeCoOrdsGroup,
                                   ByRef NodesUF() As NodeCoOrdsGroup,
                                   ByRef pGr() As Integer,
                                   ByRef SeqNodeNum As Long,
                                   ByRef SeqPlateNum As Long)
      Dim Lres As Double, iBDiv As Double, nDivRes As Long, D() As Double
      Dim MaxDim As Double
      Dim I As Integer, J As Integer, K As Integer, nNodesSkip As Integer, inDivRes As Long, Com As Component
      Dim P1 As NodeCoOrds, P2 As NodeCoOrds
      ReDim NodesGr(UBound(NodesVA)) ' Required
      Com = Component.CRVAS
      For I = 0 To UBound(NodesVA)
         If I = 0 Then
            ReDim NodesGr(I).Nodes(0)
            For J = 0 To UBound(NodesCR)
               nNodesSkip = CInt((UBound(NodesCR(J).Nodes) / GD.nRB))
               For K = 0 To UBound(NodesCR(J).Nodes) Step nNodesSkip
                  NodesGr(I).Nodes(UBound(NodesGr(I).Nodes)) = NodesCR(J).Nodes(K)
                  ReDim Preserve NodesGr(I).Nodes(UBound(NodesGr(I).Nodes) + 1)
               Next K
               ReDim Preserve NodesGr(I).Nodes(UBound(NodesGr(I).Nodes) - 1)
            Next J
         Else
            P1 = NodesUF(I).Nodes(0) : P2 = NodesVA(I).Nodes(0)
            Dim yDiffTotal As Double, yDiff As Double
            Lres = Math.Sqrt((P2.x - P1.x) ^ 2 + (P2.y - P1.y) ^ 2 + (P2.z - P1.z) ^ 2)
            yDiffTotal = (P2.y - P1.y)
            For J = 0 To CInt((UBound(NodesGr(I - 1).Nodes) / GD.nRB) - 2)
               P1 = NodesGr(I - 1).Nodes(CInt(J * GD.nRB)) : P2 = NodesGr(I - 1).Nodes(CInt((J + 1) * GD.nRB))
               MaxDim = Math.Sqrt((P2.x - P1.x) ^ 2 + (P2.y - P1.y) ^ 2 + (P2.z - P1.z) ^ 2)
            Next J
            iBDiv = MinEx(MaxDim * GD.AspectRatio, GD.MaxPlateDim)
            nDivRes = CeilingEx(Lres / iBDiv, 1)
            iBDiv = Lres / nDivRes
            ReDim NodesGr(I).Nodes(0)
            ReDim D(0) : D(0) = GD.dCR
            yDiff = (yDiffTotal / nDivRes)
            For inDivRes = 0 To nDivRes
               If inDivRes = 0 Then
                  nNodesSkip = CInt((UBound(NodesVA(I).Nodes) / GD.nRB))
                  For J = 0 To CInt((GD.nRB - 1))
                     NodesGr(I).Nodes(UBound(NodesGr(I).Nodes)) = NodesVA(I).Nodes(J * nNodesSkip)
                     ReDim Preserve NodesGr(I).Nodes(UBound(NodesGr(I).Nodes) + 1)
                  Next J
               ElseIf inDivRes = nDivRes Then
                  nNodesSkip = CInt((UBound(NodesUF(I).Nodes) / GD.nRB))
                  For J = 0 To CInt((GD.nRB - 1))
                     NodesGr(I).Nodes(UBound(NodesGr(I).Nodes)) = NodesUF(I).Nodes(J * nNodesSkip)
                     ReDim Preserve NodesGr(I).Nodes(UBound(NodesGr(I).Nodes) + 1)
                  Next J
               Else
                  GenerateVASNodes(iBDiv, I, D, NodesGr, SeqNodeNum, Component.CRVAS, yDiff)
               End If
            Next inDivRes
            ReDim Preserve NodesGr(I).Nodes(UBound(NodesGr(I).Nodes) - 1)
         End If
      Next I
      GenerateVASPlates(NodesGr, pGr, SeqPlateNum, Component.CRVAS)
   End Sub
   Private Sub GenerateVASNodes(ByVal iBDiv As Double,
                                ByVal iDiv As Integer,
                                ByRef D() As Double,
                                ByRef NodesGr() As NodeCoOrdsGroup,
                                ByRef SeqNodeNum As Long,
                                ByVal Com As Component,
                                Optional ByVal yDiff As Double = vbEmpty)
      Dim iR As Double, iDelta As Double, Counter As Integer, ieNode As NodeCoOrds
      Counter = (UBound(D) + 1)
      ReDim Preserve D(Counter)
      D(Counter) = D(Counter - 1) - (2 * Math.Sqrt(iBDiv ^ 2 - yDiff ^ 2))
      iR = D(Counter) / 2
      ieNode.y = NodesGr(iDiv).Nodes(UBound(NodesGr(iDiv).Nodes) - 1).y - yDiff
      For I = 0 To GD.nRB - 1
         iDelta = GD.sDelta + (I * GD.Omega)
         ieNode.x = iR * Math.Cos(iDelta)
         ieNode.z = iR * Math.Sin(iDelta)
         Call CreateNewNode(SeqNodeNum, NodesGr, iDiv, ieNode.x, ieNode.y, ieNode.z)
      Next I
   End Sub
   Private Sub GenerateVASPlates(ByRef NodesGr() As NodeCoOrdsGroup,
                                 ByRef PlateGroup() As Integer,
                                 ByRef SeqPlateNum As Long,
                                 Optional ByVal Com As Component = Component.None)
      '/ To avoid improper triangular plates formation, check the included angle of each triangular plate if it is less or equal to PI/2
      Dim Div As Integer, nDiv As Integer, snNodes As Integer, enNodes As Integer, NodesGrRev() As NodeCoOrdsGroup, nCounter As Integer, PL As Plate
      ReDim PlateGroup(0)
      ReDim NodesGrRev(UBound(NodesGr))
      NodesGrRev = NodesGr
      nDiv = UBound(NodesGr)
      '/ Upper Bounds of the Node Groups are NOT reduced by '1'. Make the necessary corrections at all the locations
      '/ when this reduction is made
      For Div = 0 To nDiv - 1
         '/--- This If Block is commented because of BPOmega
         '/--- In case of any modelling issues, check the below If block.
         'If ((Com = Component.BPInner) And (GD.idBP = 0.0#)) Or ((Com = Component.CPInner) And (GD.idCP = 0.0#)) Then
         '   nCounter = (TOTAL_ANGLE / BPOmega)
         'Else
         nCounter = CInt(GD.nRB)
         'End If
         snNodes = CInt((UBound(NodesGrRev(Div).Nodes) + 1) / nCounter)
         enNodes = CInt((UBound(NodesGrRev(Div + 1).Nodes) + 1) / nCounter)
         If snNodes < enNodes Then
            For I = 0 To (snNodes - 1)
               With PL
                  For J = 0 To (nCounter - 1)
                     .NodeA = NodesGrRev(Div).Nodes(I * nCounter + J).No : .NodeB = NodesGrRev(Div + 1).Nodes(I * nCounter + J).No
                     .NodeC = NodesGrRev(Div + 1).Nodes((I + 1) * nCounter + J).No : .NodeD = .NodeA
                     Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     If I < (snNodes - 1) Then
                        .NodeA = NodesGrRev(Div).Nodes(I * nCounter + J).No : .NodeB = NodesGrRev(Div + 1).Nodes((I + 1) * nCounter + J).No
                        .NodeC = NodesGrRev(Div).Nodes((I + 1) * nCounter + J).No : .NodeD = .NodeA
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     End If
                  Next J
               End With
            Next I
         ElseIf snNodes > enNodes Then
            '/ Code implemented. Needs validation.
            For I = 0 To (snNodes - 2)
               With PL
                  For J = 0 To (nCounter - 1)
                     With PL
                        .NodeA = NodesGrRev(Div).Nodes(I * nCounter + J).No : .NodeB = NodesGrRev(Div + 1).Nodes(I * nCounter + J).No
                        .NodeC = NodesGrRev(Div + 1).Nodes((I + 1) * nCounter + J).No : .NodeD = .NodeA
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                        .NodeA = NodesGrRev(Div).Nodes(I * nCounter + J).No : .NodeB = NodesGrRev(Div + 1).Nodes((I + 1) * nCounter + J).No
                        .NodeC = NodesGrRev(Div).Nodes((I + 1) * nCounter + J).No : .NodeD = .NodeA
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     End With
                     If I = (snNodes - 2) Then
                        .NodeA = NodesGrRev(Div).Nodes((I + 1) * nCounter + J).No : .NodeB = NodesGrRev(Div + 1).Nodes((I + 1) * nCounter + J).No
                        .NodeC = NodesGrRev(Div).Nodes((I + 2) * nCounter + J).No : .NodeD = .NodeA
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeD)
                     End If
                  Next J
               End With
            Next I
         Else
            For I = 0 To snNodes - 2
               For J = 0 To (nCounter - 1)
                  With PL
                     .NodeA = NodesGrRev(Div).Nodes(I * nCounter + J).No : .NodeB = NodesGrRev(Div + 1).Nodes(I * nCounter + J).No
                     .NodeC = NodesGrRev(Div + 1).Nodes((I + 1) * nCounter + J).No : .NodeD = NodesGrRev(Div).Nodes((I + 1) * nCounter + J).No
                     Dim IncAngle1 As Double, IncAngle2 As Double
                     Calc3DLinesAngle(.NodeA, .NodeB, .NodeC, IncAngle1)
                     Calc3DLinesAngle(.NodeC, .NodeD, .NodeA, IncAngle2)
                     If (IncAngle1 <= PI / 2) And (IncAngle2 <= PI / 2) Then
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeC, PL.NodeA)
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeC, PL.NodeD, PL.NodeA, PL.NodeC)
                     Else
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeA, PL.NodeB, PL.NodeD, PL.NodeA)
                        Call CreateNewPlate(SeqPlateNum, PlateGroup, PL.NodeB, PL.NodeC, PL.NodeD, PL.NodeB)
                     End If
                  End With
               Next J
            Next I
         End If
      Next Div
      ReDim Preserve PlateGroup(UBound(PlateGroup) - 1)
   End Sub
   Private Sub CreateNewPlate(ByRef SeqPlateNum As Long,
                                 ByRef PlateGr() As Integer,
                                 ByVal NodeA As Long,
                                 ByVal NodeB As Long,
                                 ByVal NodeC As Long,
                                 ByVal NodeD As Long)
      SeqPlateNum = SeqPlateNum + 1
      PlateGr(UBound(PlateGr)) = CInt(SeqPlateNum)
      OSGeometry.CreatePlate(SeqPlateNum, NodeA, NodeB, NodeC, NodeD)
      ReDim Preserve PlateGr(UBound(PlateGr) + 1)
   End Sub
   Private Sub CreateNewNode(ByRef SeqNodeNum As Long,
                             ByRef NodesGroup() As NodeCoOrdsGroup,
                             ByVal index As Integer,
                             ByVal x As Double,
                             ByVal y As Double,
                             ByVal z As Double)
      SeqNodeNum = SeqNodeNum + 1
      With NodesGroup(index).Nodes(UBound(NodesGroup(index).Nodes))
         .No = SeqNodeNum : .x = x : .y = y : .z = z
         OSGeometry.CreateNode(.No, .x, .y, .z)
      End With
      ReDim Preserve NodesGroup(index).Nodes(UBound(NodesGroup(index).Nodes) + 1)
   End Sub
End Class
