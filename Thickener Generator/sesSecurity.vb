'/-------------------------------------------------------------/
'/ Program Name   : Structural Engineering Solutions           /
'/ Start Date     : 17 Aug 2017                                /
'/ Last Revised   : 24 Apr 2019                                /
'/ Copy Right     : Elevated Terrains Consulting Engineers     /
'/ Developer Name : Srinivasa Rao Masanam                      /
'/-------------------------------------------------------------/
Option Explicit On
Public Class sesSecurity
   Private LicClass As sesLicenseClass
   Protected Friend ReadOnly Property FontColor As String
      Get
         Return "&K03+000"
      End Get
   End Property

   Protected Friend ReadOnly Property FontProperties As String
      Get
         Return "&""Tahoma,Regular""&8"
      End Get
   End Property

   Protected Friend ReadOnly Property Password As String
      Get
         Return "EngineeredProgramming"
      End Get
   End Property
End Class
