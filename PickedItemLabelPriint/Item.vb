Imports System
Imports System.Reflection



Public Class Item
    Implements IEnumerator, IEnumerable

    Private mOrderNo As String
    Public Property OrderNo() As String
        Get
            Return mOrderNo
        End Get
        Set(ByVal value As String)
            mOrderNo = value
        End Set
    End Property

    Private mStatus As String
    Public Property Status() As String
        Get
            Return mStatus
        End Get
        Set(ByVal value As String)
            mStatus = value
        End Set
    End Property
    Private mCusName As String
    Public Property CusName() As String
        Get
            Return mCusName
        End Get
        Set(ByVal value As String)
            mCusName = value
        End Set
    End Property
    Private mQty As String
    Public Property Qty() As String
        Get
            Return mQty
        End Get
        Set(ByVal value As String)
            mQty = value
        End Set
    End Property
    Private mItemNo As String
    Public Property ItemNo() As String
        Get
            Return mItemNo
        End Get
        Set(ByVal value As String)
            mItemNo = value
        End Set
    End Property
    Private mDescription As String
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value
        End Set
    End Property
    Private mFinish As String
    Public Property Finish() As String
        Get
            Return mFinish
        End Get
        Set(ByVal value As String)
            mFinish = value
        End Set
    End Property
    Private mUnitPrice As String
    Public Property UnitPrice() As String
        Get
            Return mUnitPrice
        End Get
        Set(ByVal value As String)
            mUnitPrice = value
        End Set
    End Property
    Private mUPC As String
    Public Property UPC() As String
        Get
            Return mUPC
        End Get
        Set(ByVal value As String)
            mUPC = value
        End Set
    End Property
    Private mCatalogPage As String
    Public Property CatalogPage() As String
        Get
            Return mCatalogPage
        End Get
        Set(ByVal value As String)
            mCatalogPage = value
        End Set
    End Property
    Private mDetail_Finish_Type As String
    Public Property Detail_Finish_Type() As String
        Get
            Return mDetail_Finish_Type
        End Get
        Set(ByVal value As String)
            mDetail_Finish_Type = value
        End Set
    End Property
    Private mBarcode As String
    Public Property Barcode() As String
        Get
            Return mBarcode
        End Get
        Set(ByVal value As String)
            mBarcode = value
        End Set
    End Property
    Private mSTARTCusNo As String
    Public Property STARTCusNo() As String
        Get
            Return mSTARTCusNo
        End Get
        Set(ByVal value As String)
            mSTARTCusNo = value
        End Set
    End Property
    Private mSTARTShipToName As String
    Public Property STARTShipToName() As String
        Get
            Return mSTARTShipToName
        End Get
        Set(ByVal value As String)
            mSTARTShipToName = value
        End Set
    End Property
    Private mSTARTOrdNo As String
    Public Property STARTOrdNo() As String
        Get
            Return mSTARTOrdNo
        End Get
        Set(ByVal value As String)
            mSTARTOrdNo = value
        End Set
    End Property


    'Public OrderNo As String
    'Public Status As String
    'Public CusName As String
    'Public Qty As String
    'Public ItemNo As String
    'Public Description As String
    'Public Finish As String
    'Public UnitPrice As String
    'Public UPC As String
    'Public CatalogPage As String
    'Public Detail_Finish_type As String
    'Public Barcode As String
    'Public STARTCusNo As String
    'Public STARTShipToName As String
    'Public STARTOrdNo As String


    Public Sub New( _
       ByVal mOrderNo As String, _
       ByVal mStatus As String, _
       ByVal mCusName As String, _
       ByVal mQty As String, _
       ByVal mItemNo As String, _
       ByVal mDescription As String, _
       ByVal mFinish As String,
       ByVal mUnitPrice As String,
       ByVal mUPC As String,
       ByVal mCatalogPage As String,
       ByVal mDetail_Finish_type As String,
       ByVal mBarcode As String,
       ByVal mSTARTCusNo As String,
       ByVal mSTARTShipToName As String,
       ByVal mSTARTOrdNo As String)
        OrderNo = mOrderNo
        Status = mStatus
        CusName = mCusName
        Qty = mQty
        ItemNo = mItemNo
        Description = mDescription
        Finish = mFinish
        UnitPrice = mUnitPrice
        UPC = mUPC
        CatalogPage = mCatalogPage
        Detail_Finish_type = mDetail_Finish_type
        Barcode = mBarcode
        STARTCusNo = mSTARTCusNo
        STARTShipToName = mSTARTShipToName
        STARTOrdNo = mSTARTOrdNo

    End Sub
    Public Sub New()
        OrderNo = ""
        Status = ""
        CusName = ""
        Qty = ""
        ItemNo = ""
        Description = ""
        Finish = ""
        UnitPrice = ""
        UPC = ""
        CatalogPage = ""
        Detail_Finish_type = ""
        Barcode = ""
        STARTCusNo = ""
        STARTShipToName = ""
        STARTOrdNo = ""

    End Sub

    Public Sub Clear()
        OrderNo = ""
        Status = ""
        CusName = ""
        Qty = ""
        ItemNo = ""
        Description = ""
        Finish = ""
        UnitPrice = ""
        UPC = ""
        CatalogPage = ""
        Detail_Finish_type = ""
        Barcode = ""
        STARTCusNo = ""
        STARTShipToName = ""
        STARTOrdNo = ""

    End Sub

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator

    End Function

    Public ReadOnly Property Current As Object Implements System.Collections.IEnumerator.Current
        Get

        End Get
    End Property

    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext

    End Function

    Public Sub Reset() Implements System.Collections.IEnumerator.Reset

    End Sub
End Class
