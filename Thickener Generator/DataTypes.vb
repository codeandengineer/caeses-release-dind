'/-------------------------------------------------------------/
'/ Program Name   : ThickenerGenerator                         /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Public Class DataTypes
   Public Enum ControlType
      GridCell
      Button
      Label
      TextBox
      ComboBox
      ListBox
   End Enum
   Public Enum GroupType
      Nodes = 1
      Beams = 2
      Plates = 3
      Solids = 4
      Geometry = 5
      Floor = 6
   End Enum
   Public Enum LengthUnit
      Inch = 0
      Feet = 1
      CentiMeter = 3
      Meter = 4
      MilliMeter = 5
      DeciMeter = 6
      KiloMeter = 7
   End Enum
   Public Enum ForceUnit
      Kilopound = 0
      Pound = 1
      Kilogram = 2
      MetricTon = 3
      Newton = 4
      KiloNewton = 5
      MegaNewton = 6
      DecaNewton = 7
   End Enum
   Public Enum ReleaseEnd
      sEnd = 0
      eEnd = 1
   End Enum
   Public Structure OffsetSpec
      Dim StartOrEnd As OffsetEnd
      Dim LocalOrGlobal As OffsetAxis
      Dim x As Double
      Dim y As Double
      Dim z As Double
   End Structure
   Public Enum OffsetEnd
      sEnd = 0
      eEnd = 1
   End Enum
   Public Enum OffsetAxis
      GlobalAxis = 0
      LocalAxis = 1
   End Enum
   Public Enum TransType
      Inc
      Dec
      None
   End Enum
   Public Enum Component
      LWall
      LFloor
      WLWall
      WFreeBoard
      Wall
      WInterface
      CCTop
      CCBot
      FloorPlate
      UFCone
      CompRing
      CRVertAnnulus
      CRVAS ' Compression Ring Stiffener
      BPOuter
      BPBolts
      BPInner
      CPOuter
      CPInner
      None
   End Enum
   Public Structure MeshData
      Dim NrTop As Long   'Number of plates along length of the segment at top
      Dim NrBot As Long   'Number of plates along length of the segment at bottom
      Dim Nd As Long      'Number of divisions along height of the segment
      Dim Hp As Double    'Height of meshed plate
      Dim LpTop As Double    'Length of meshed plate at top/outer diameter
      Dim LpBot As Double    'Length of meshed plate at bottom/inner diameter
      Dim Hs As Double    'Height of total segment
      Dim LsTop As Double 'Length of total segment at top/outer diameter
      Dim LsBot As Double 'Length of total segment at bottom/inner diameter
   End Structure
   Public Structure SupportCondition
      Dim Rel() As Double
      Dim Spr() As Double
   End Structure
   Public Structure MemRelease
      Dim rEnd As ReleaseEnd
      Dim Rel() As Integer
      Dim Spr() As Integer
   End Structure
   Public Structure Beam
      Dim No As Long
      Dim sNode As NodeCoOrds
      Dim eNode As NodeCoOrds
   End Structure
   Public Structure BeamGroup
      Dim Beams() As Beam
   End Structure
   Public Structure NodeCoOrds
      Dim No As Long
      Dim x As Double
      Dim y As Double
      Dim z As Double
   End Structure
   Public Structure Line
      Dim NodeA As NodeCoOrds
      Dim NodeB As NodeCoOrds
      Dim Length As Double
      Dim a As Double
      Dim b As Double
      Dim c As Double
   End Structure
   Public Structure NodeCoOrdsGroup
      Dim Nodes() As NodeCoOrds
   End Structure
   Public Structure NodeGroups
      Dim LW() As NodeCoOrdsGroup
      Dim LF() As NodeCoOrdsGroup
      Dim WLW() As NodeCoOrdsGroup
      Dim FB() As NodeCoOrdsGroup
      Dim W() As NodeCoOrdsGroup
      Dim Intface() As NodeCoOrdsGroup ' Previous definition: Interface()
      '/------- New definitions -----/
      Dim ZeroDroop() As NodeCoOrdsGroup
      Dim CR() As NodeCoOrdsGroup
      Dim UF() As NodeCoOrdsGroup
      Dim CCBot() As NodeCoOrdsGroup
      Dim CCTop() As NodeCoOrdsGroup
      Dim CRVertAnnulus() As NodeCoOrdsGroup
      Dim CRVAS() As NodeCoOrdsGroup
      Dim tneNear() As NodeCoOrdsGroup
      Dim tneFar() As NodeCoOrdsGroup
      '/------- Optional for full-span bridge case -------'/
      Dim tfeNear() As NodeCoOrdsGroup
      Dim tfeFar() As NodeCoOrdsGroup
      '/------- Optional for half-span bridge case -------'/
      Dim cpNear() As NodeCoOrdsGroup
      Dim cpFar() As NodeCoOrdsGroup
      '/------- New definitions -----/
      Dim BPOuter() As NodeCoOrdsGroup
      Dim BPBolts() As NodeCoOrdsGroup
      Dim BPInner() As NodeCoOrdsGroup
      Dim CPOuter() As NodeCoOrdsGroup
      Dim CPInner() As NodeCoOrdsGroup
      Dim CPInnerBottom() As NodeCoOrdsGroup
   End Structure
   Public Structure MemberGroups
      Dim LW() As Integer
      Dim LF() As Integer
      Dim WLW() As Integer
      Dim FB() As Integer
      Dim W() As Integer
      Dim Intface() As Integer ' Previous definition: Interface()
      Dim CR() As Integer
      Dim UF() As Integer
      Dim CCBot() As Integer
      Dim CCTop() As Integer
      Dim CRVertAnnulus() As Integer
      Dim CRVAS() As Integer
      '/------- New definitions -----/
      Dim BPOuter() As Integer
      Dim BPBolts() As Integer
      Dim BPInner() As Integer
      Dim CPOuter() As Integer
      Dim CPInner() As Integer
      Dim CPInnerBottom() As Integer
   End Structure
   Public Enum SecPickCallCategory
      Coupled
      Isolated
   End Enum
   Public Enum TypeSpec
      ST = 0
      RA = 1
      D = 2
      LD = 3
      SD = 4
      T = 5
      CM = 6
      TC = 7
      BC = 8
      TB = 9
      BA = 10
      FR = 11
      UserDefined = -1
   End Enum
   Public Enum Country
      American = 1
      Australian = 2
      British = 3
      Canadian = 4
      Chinese = 5
      Dutch = 6
      European = 7
      French = 8
      German = 9
      Indian = 10
      Japanese = 11
      Russian = 12
      SouthAfrican = 13
      Spanish = 14
      Venezuelan = 15
      Korean = 16
      Aluminium = 17
      USColdFormed = 18
      ISColdFormed = 19
   End Enum
   Public Structure MaterialData
      Dim ID As Integer
      Dim Grade As String
      Dim STAADName As String
      Dim dbName As String
      Dim dbNameIndex As Integer
      Dim nameIndex As Integer
      Dim Fy As Double
      Dim Fu As Double
      Dim E As Double
      Dim Poisson As Double
      Dim Density As Double
      Dim Alpha As Double
      Dim CrDamp As Double
      ReadOnly Property G As Double
         Get
            If Poisson <> 0 Then
               Return E / (2 * (1 + Poisson))
            Else
               Return 0.0
            End If
         End Get
      End Property
   End Structure
   Public Structure PropertyData
      Dim ID As Integer
      Dim OffSpec As OffsetSpec
      Dim dbName As String
      Dim nTableID As Country
      Dim type As TypeSpec
      Dim shapeTypeKey As String
      Dim addSpec1 As Double
      Dim addSpec2 As Double
      Dim name As String
      Dim nameIndex As Integer
      Dim shape As String
      Dim tableName As String
      Dim sData As SectionData
      Dim mData As MaterialData
   End Structure
   Public Structure SupportStructureInfo
      Dim SlNo As Long
      Dim Desc As String
      Dim PCD As Double
      Dim ColSec As PropertyData
      Dim EL As Double
      Dim HasCB As Boolean
      Dim HasRB As Boolean
      Dim CBPattern As String
      Dim RBPattern As String
      Dim CBSec As PropertyData
      Dim RBSec As PropertyData
      Dim grpPCD() As Integer
      Dim BeamIncPCD() As Beam
      Dim ColumnIncPCD() As Beam
      Dim CBIncPCD() As Beam
      Dim RBIncPCD() As Beam
      Dim grpBeamPCD() As Integer
      Dim grpColumnPCD() As Integer
      Dim grpCBPCD() As Integer
      Dim grpRBPCD() As Integer
      Dim NodesPCD() As NodeCoOrdsGroup
      Dim SupNodes() As Integer 'Long
   End Structure
   Public Structure Plate
      Dim NodeA As Long
      Dim NodeB As Long
      Dim NodeC As Long
      Dim NodeD As Long
   End Structure
   Public Structure GeometryData
      Dim Origin As NodeCoOrds
      Dim Alpha As Double
      Dim Beta As Double
      Dim sDelta As Double
      Dim nRB As Long
      Dim nColRB As Long
      Dim idBP As Double
      Dim odBP As Double
      Dim pcdBolts As Double
      Dim idCP As Double
      Dim odCP As Double
      Dim dCC As Double
      Dim hCC As Double
      Dim hCCTop As Double
      Dim dUF As Double
      Dim hUF As Double
      Dim dCR As Double
      Dim bCR As Double
      Dim hCR As Double
      Dim hDroop As Double
      Dim rDroop As Double
      Dim nBoltsCC As Long
      Dim LHalfChord As Double
      Dim Theta As Double
      Dim dTank As Double
      Dim hWall As Double
      Dim FreeBoard As Double
      Dim Omega As Double
      Dim bLaun As Double
      Dim hLaun As Double
      Dim dLaun As Double
      Dim MaxPlateDim As Double
      Dim AspectRatio As Double
      Dim Phi As Double
      '/---- New Data ----'/
      Dim alphaBridge As Double
      Dim betaBridge As Double
      Dim bBridge As Double
      Dim hBridge As Double
      Dim hStub As Double
      '/---- New Data ----'/
   End Structure
   Public Structure BasicProfile
      Dim sNodeBP As NodeCoOrds
      Dim eNodeBP As NodeCoOrds
      Dim sNodeCC As NodeCoOrds
      Dim eNodeCC As NodeCoOrds
      Dim sNodeUF As NodeCoOrds
      Dim eNodeUF As NodeCoOrds
      Dim sNodeCR As NodeCoOrds
      Dim eNodeCR As NodeCoOrds
      Dim sNodeRB As NodeCoOrds
      Dim eNodeRB As NodeCoOrds
      Dim sNodeTW As NodeCoOrds
      Dim eNodeTW As NodeCoOrds
      Dim sNodeTWL As NodeCoOrds
      Dim eNodeTWL As NodeCoOrds
      Dim sNodeTWFB As NodeCoOrds
      Dim eNodeTWFB As NodeCoOrds
      Dim sNodeLF As NodeCoOrds
      Dim eNodeLF As NodeCoOrds
      Dim sNodeLW As NodeCoOrds
      Dim eNodeLW As NodeCoOrds
   End Structure
   Public Structure SectionData
      Public Designation As String
      Public STAADName As String
      Public Mass As Double
      Public Massfps As Double
      ' Properties Specific to Pipe Sections
      Public OD As Double
      Public ID As Double
      ' Pipe Specific Properties Ends Here
      Public h As Single
      Public bf As Single
      Public tw As Single
      Public tf As Single
      Public r1 As Single
      Public r2 As Single
      Public A As Double
      Public hi As Single
      Public d As Single
      Public phi As Single
      Public pmin As Single
      Public pmax As Single
      Public ALo As Double
      Public ALi As Double
      Public AGo As Double
      Public AGi As Double
      Public Iy As Double
      Public Wely As Double
      Public Wply As Double
      Public ry As Single
      Public Avz As Double
      Public Iz As Double
      Public Welz As Double
      Public Wplz As Double
      Public rz As Single
      Public Avy As Double
      Public ss As Single
      Public It As Double
      Public Iw As Double
      Public k As Single
      Public k1 As Single
      Public Alpha As Double
      Public Cy As Double
      Public ey As Double
      Public Cz As Double
      Public ez As Double
      Public Iu As Double
      Public Iv As Double
      Public ru As Double
      Public rv As Double
   End Structure
End Class