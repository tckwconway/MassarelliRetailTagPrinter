Public Class Order


    Public OrderNo As String
    Public PickedDt As String
    Public ShipToName As String
    Public ShipToState As String
   
    Public Sub New( _
       ByVal mOrderNo As String, _
       ByVal mPickedDt As String, _
       ByVal mShipToName As String, _
       ByVal mShipToState As String)
        OrderNo = mOrderNo
        PickedDt = mPickedDt
        ShipToName = mShipToName
        ShipToState = mShipToState
    End Sub

    Public Sub Clear()
        OrderNo = ""
        PickedDt = ""
        ShipToName = ""
        ShipToState = ""
    End Sub
End Class
