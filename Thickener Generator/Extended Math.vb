'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 12 May 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Strict On
Imports SES.DataTypes
Module Extended_Math
   Private Structure LineVector
      Dim x As Double
      Dim y As Double
      Dim z As Double
      Dim a As Double
      Dim b As Double
      Dim c As Double
      Dim t As Double
   End Structure
   '/------------------------ Extended Math Functions ----------------------/
   Friend Function Deg2Rad(ByVal inputDeg As Double) As Double
      Dim Result As Double
      Result = inputDeg * Math.PI / (TOTAL_ANGLE / 2)
      Return Result
   End Function
   Friend Function Rad2Deg(ByVal inputRad As Double) As Double
      Dim Result As Double
      Result = inputRad * (TOTAL_ANGLE / 2) / Math.PI
      Return Result
   End Function
   Friend Function MaxEx(ByVal ParamArray args() As Double) As Double
      Dim index As Integer, MaxValue As Double
      If args.Length <= 0 Then Return Nothing
      MaxValue = args(0)
      For index = 1 To UBound(args, 1)
         If MaxValue >= args(index) Then MaxValue = MaxValue Else MaxValue = args(index)
      Next index
      Return MaxValue
   End Function
   Friend Function MaxEx(ByVal ParamArray args() As Long) As Long
      Dim index As Integer, MaxValue As Long
      If args.Length <= 0 Then Return Nothing
      MaxValue = args(0)
      For index = 1 To UBound(args, 1)
         If MaxValue >= args(index) Then MaxValue = MaxValue Else MaxValue = args(index)
      Next index
      Return MaxValue
   End Function
   Friend Function MaxEx(ByVal ParamArray args() As Integer) As Integer
      Dim index As Integer, MaxValue As Integer
      If args.Length <= 0 Then Return Nothing
      MaxValue = args(0)
      For index = 1 To UBound(args, 1)
         If MaxValue >= args(index) Then MaxValue = MaxValue Else MaxValue = args(index)
      Next index
      Return MaxValue
   End Function
   Friend Function MinEx(ByVal ParamArray args() As Double) As Double
      Dim index As Integer, MinValue As Double
      If args.Length <= 0 Then Return Nothing
      MinValue = args(0)
      For index = 1 To UBound(args, 1)
         If MinValue <= args(index) Then MinValue = MinValue Else MinValue = args(index)
      Next index
      Return MinValue
   End Function
   Friend Function MinEx(ByVal ParamArray args() As Long) As Long
      Dim index As Integer, MinValue As Long
      If args.Length <= 0 Then Return Nothing
      MinValue = args(0)
      For index = 1 To UBound(args, 1)
         If MinValue <= args(index) Then MinValue = MinValue Else MinValue = args(index)
      Next index
      Return MinValue
   End Function
   Friend Function MinEx(ByVal ParamArray args() As Integer) As Integer
      Dim index As Integer, MinValue As Integer
      If args.Length <= 0 Then Return Nothing
      MinValue = args(0)
      For index = 1 To UBound(args, 1)
         If MinValue <= args(index) Then MinValue = MinValue Else MinValue = args(index)
      Next index
      Return MinValue
   End Function
   Friend Function CeilingEx(ByVal input As Double,
                             ByVal bound As Double) As Double
      Return CDbl(Math.Truncate(input / bound) * bound + bound)
   End Function
   Friend Function CeilingEx(ByVal input As Double,
                             ByVal bound As Long) As Long
      If (input / bound) - Math.Truncate(input / bound) <= Precision Then
         Return CLng(input)
      Else
         Return CLng(Math.Truncate(input / bound) * bound + bound)
      End If
   End Function
   Friend Function CeilingEx(ByVal input As Double,
                             ByVal bound As Integer) As Integer
      If (input / bound) - Math.Truncate(input / bound) <= Precision Then
         Return CInt(input)
      Else
         Return CInt(Math.Truncate(input / bound) * bound + bound)
      End If
   End Function
   Friend Sub GetIntersectionNode(ByRef sNodeL1 As NodeCoOrds,
                                  ByRef eNodeL1 As NodeCoOrds,
                                  ByRef sNodeL2 As NodeCoOrds,
                                  ByRef eNodeL2 As NodeCoOrds,
                                  ByRef intNode As NodeCoOrds)
      Dim L1 As LineVector, L2 As LineVector

      L1.x = sNodeL1.x : L1.y = sNodeL1.y : L1.z = sNodeL1.z
      L2.x = sNodeL2.x : L2.y = sNodeL2.y : L2.z = sNodeL2.z

      L1.a = (sNodeL1.x - eNodeL1.x) : L1.b = (sNodeL1.y - eNodeL1.y) : L1.c = (sNodeL1.z - eNodeL1.z)
      L2.a = (sNodeL2.x - eNodeL2.x) : L2.b = (sNodeL2.y - eNodeL2.y) : L2.c = (sNodeL2.z - eNodeL2.z)

      If (L1.a = 0.0# And L2.a = 0.0#) Then
         ' Special case - Plane is vertical @ x = 0
         L2.t = ((L2.y - L1.y) / (L1.b - L2.b)) : L1.t = L2.t
      ElseIf (L1.b = 0.0# And L2.b = 0.0#) Then
         ' Special case - Plane is horizontal @ y = 0
         L2.t = ((L2.x - L1.x) / (L1.a - L2.a)) : L1.t = L2.t
      Else
         ' General case
         L2.t = (L1.a * (L2.y - L1.y) - L1.b * (L2.x - L1.x)) / (L2.a * L1.b - L1.a * L2.b)
         L1.t = ((L2.x - L1.x) + L2.a * L2.t) / L1.a
      End If

      With intNode
         .x = L1.x + (L1.a * L1.t) : .y = L1.y + (L1.b * L1.t) : .z = L1.z + (L1.c * L1.t)
      End With

   End Sub
   Friend Sub Calc3DLinesAngle(ByVal P1Num As Long,
                               ByVal PIntNum As Long,
                               ByVal P2Num As Long,
                               ByRef IncAngle As Double)
      '      Dim P1 As NodeCoOrds, PInt As NodeCoOrds, P2 As NodeCoOrds
      Dim L1 As Line, L2 As Line
      L1.NodeA.No = PIntNum : L1.NodeB.No = P1Num
      L2.NodeA.No = PIntNum : L2.NodeB.No = P2Num
      With OSGeometry
         .GetNodeCoordinates(L1.NodeA.No, CDbl(L1.NodeA.x), CDbl(L1.NodeA.y), CDbl(L1.NodeA.z))
         .GetNodeCoordinates(L1.NodeB.No, CDbl(L1.NodeB.x), CDbl(L1.NodeB.y), CDbl(L1.NodeB.z))
         .GetNodeCoordinates(L2.NodeA.No, CDbl(L2.NodeA.x), CDbl(L2.NodeA.y), CDbl(L2.NodeA.z))
         .GetNodeCoordinates(L2.NodeB.No, CDbl(L2.NodeB.x), CDbl(L2.NodeB.y), CDbl(L2.NodeB.z))
      End With
      L1.a = (L1.NodeA.x - L1.NodeB.x) : L1.b = (L1.NodeA.y - L1.NodeB.y) : L1.c = (L1.NodeA.z - L1.NodeB.z)
      L2.a = (L2.NodeA.x - L2.NodeB.x) : L2.b = (L2.NodeA.y - L2.NodeB.y) : L2.c = (L2.NodeA.z - L2.NodeB.z)
      L1.Length = Math.Sqrt(L1.a ^ 2 + L1.b ^ 2 + L1.c ^ 2) : L2.Length = Math.Sqrt(L2.a ^ 2 + L2.b ^ 2 + L2.c ^ 2)
      IncAngle = (L1.a * L2.a + L1.b * L2.b + L1.c * L2.c) / (Math.Sqrt(L1.a ^ 2 + L1.b ^ 2 + L1.c ^ 2) * Math.Sqrt(L2.a ^ 2 + L2.b ^ 2 + L2.c ^ 2))
      IncAngle = Math.Acos(IncAngle)
   End Sub
   '/-----------------------------------------------------------------------/

End Module
