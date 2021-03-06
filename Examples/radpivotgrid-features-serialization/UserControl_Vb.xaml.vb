' #region radpivotgrid-features-serialization_1
<DataContract> _
Public Class DataProviderSettings
	<DataMember> _
	Public Property Aggregates() As Object()

	<DataMember> _
	Public Property Filters() As Object()

	<DataMember> _
	Public Property Rows() As Object()

	<DataMember> _
	Public Property Columns() As Object()

	<DataMember> _
	Public Property AggregatesLevel() As Integer

	<DataMember> _
	Public Property AggregatesPosition() As PivotAxis
End Class
' #endregion

' #region radpivotgrid-features-serialization_2
Public MustInherit Class DataProviderSerializer
	Public MustOverride ReadOnly Property KnownTypes() As IEnumerable(Of Type)

	Public Function Serialize(ByVal context As Object) As String
		Dim serialized As String = String.Empty

		Dim dataProvider As IDataProvider = TryCast(context, IDataProvider)
		If dataProvider IsNot Nothing Then
			Dim stream As New MemoryStream()

			Dim settings As New DataProviderSettings() With {.Aggregates = dataProvider.Settings.AggregateDescriptions.OfType(Of Object)().ToArray(), .Filters = dataProvider.Settings.FilterDescriptions.OfType(Of Object)().ToArray(), .Rows = dataProvider.Settings.RowGroupDescriptions.OfType(Of Object)().ToArray(), .Columns = dataProvider.Settings.ColumnGroupDescriptions.OfType(Of Object)().ToArray(), .AggregatesLevel = dataProvider.Settings.AggregatesLevel, .AggregatesPosition = dataProvider.Settings.AggregatesPosition}

			Dim serializer As New DataContractSerializer(GetType(DataProviderSettings), KnownTypes)
			serializer.WriteObject(stream, settings)

			stream.Position = 0
			Dim streamReader = New StreamReader(stream)
			serialized &= streamReader.ReadToEnd()
		End If

		Return serialized
	End Function

	Public Sub Deserialize(ByVal context As Object, ByVal savedValue As String)
		Dim dataProvider As IDataProvider = TryCast(context, IDataProvider)
		If dataProvider IsNot Nothing Then
			Dim stream = New MemoryStream()
			Dim tw = New StreamWriter(stream)
			tw.Write(savedValue)
			tw.Flush()
			stream.Position = 0

			Dim serializer As New DataContractSerializer(GetType(DataProviderSettings), KnownTypes)
			Dim result = serializer.ReadObject(stream)

			dataProvider.Settings.AggregateDescriptions.Clear()
			For Each aggregateDescription In (TryCast(result, DataProviderSettings)).Aggregates
				dataProvider.Settings.AggregateDescriptions.Add(aggregateDescription)
			Next aggregateDescription

			dataProvider.Settings.FilterDescriptions.Clear()
			For Each filterDescription In (TryCast(result, DataProviderSettings)).Filters
				dataProvider.Settings.FilterDescriptions.Add(filterDescription)
			Next filterDescription

			dataProvider.Settings.RowGroupDescriptions.Clear()
			For Each rowDescription In (TryCast(result, DataProviderSettings)).Rows
				dataProvider.Settings.RowGroupDescriptions.Add(rowDescription)
			Next rowDescription

			dataProvider.Settings.ColumnGroupDescriptions.Clear()
			For Each columnDescription In (TryCast(result, DataProviderSettings)).Columns
				dataProvider.Settings.ColumnGroupDescriptions.Add(columnDescription)
			Next columnDescription

			dataProvider.Settings.AggregatesPosition = (TryCast(result, DataProviderSettings)).AggregatesPosition
			dataProvider.Settings.AggregatesLevel = (TryCast(result, DataProviderSettings)).AggregatesLevel
		End If
	End Sub
End Class


Public Class LocalDataSourceSerializer
	Inherits DataProviderSerializer

	Public Overrides ReadOnly Property KnownTypes() As IEnumerable(Of Type)
		Get
			Return PivotSerializationHelper.KnownTypes
		End Get
	End Property
End Class
' #endregion

' #region radpivotgrid-features-serialization_3
Dim lastSerializadProvider As String

'serializiation
Dim provider As New LocalDataSourceSerializer()
Me.lastSerializadProvider = provider.Serialize(Me.pivotGrid.DataProvider)

'deserialization
Dim provider As New LocalDataSourceSerializer()
provider.Deserialize(Me.pivotGrid.DataProvider, Me.lastSerializadProvider)
' #endregion

' #region radpivotgrid-features-serialization_4
<DataContract> _
Public Class CustomGroupDescription
	Inherits PropertyGroupDescriptionBase

	<DataMember> _
	Public Property MyCustomProperty() As String

	Protected Overrides Sub CloneOverride(ByVal source As Cloneable)
		Me.MyCustomProperty = (TryCast(source, CustomGroupDescription)).MyCustomProperty
	End Sub

	Protected Overrides Function CreateInstanceCore() As Cloneable
		Return New CustomGroupDescription()
	End Function
End Class
' #endregion

' #region radpivotgrid-features-serialization_5
Public Class LocalDataSourceSerializer
	Inherits DataProviderSerializer

	Private myKnownTypes As IEnumerable(Of Type) = PivotSerializationHelper.KnownTypes.Concat(New List(Of Type)() From {GetType(CustomGroupDescription)})

	Public Overrides ReadOnly Property KnownTypes() As IEnumerable(Of Type)
		Get
			Return myKnownTypes
		End Get
	End Property
End Class
' #endregion