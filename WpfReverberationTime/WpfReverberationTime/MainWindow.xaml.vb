Class MainWindow
    'The different properties of the listview
    Private _lstYAbsorbers As New System.Collections.ObjectModel.ObservableCollection(Of Absorber)
    Private _lstXAbsorbers As New System.Collections.ObjectModel.ObservableCollection(Of Absorber)
    Private _lstZAbsorbers As New System.Collections.ObjectModel.ObservableCollection(Of Absorber)

    'Stors all the results after calculations
    Private _lstResult2 As New System.Collections.ObjectModel.ObservableCollection(Of Absorber)

    'Stores the results
    Private Sabine As New Absorber
    Private Eyring As New Absorber
    Private Miller As New Absorber
    Private Fritzroy As New Absorber

    'Some interestiong parameters for the room
    'Calculated using Sabines formula
    Private Schroeder As Double
    Private RoomRadius As Double

    'Room constants
    Private Volume As Double
    Private _Height, _Width, _Length, _TotalSurfaceArea As Double
    Private TotalSufraceArea As Double

    'Acoustic constants for calculations
    Private Humidity As Integer
    Private C0 As Double = 343
    Private rho0 As Double = 1.21

    'Set up binding to the listviews
    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        lstX.ItemsSource = _lstXAbsorbers
        lstY.ItemsSource = _lstYAbsorbers
        lstZ.ItemsSource = _lstZAbsorbers

        'For testing only
        'Dim test As New Absorber
        'test.Name = "Something"
        'test.Area = 50
        'For i As Integer = 0 To test.Count - 1
        '    test.SetItem(i, 0.25)
        'Next

        '_lstXAbsorbers.Add(test)
        '_lstYAbsorbers.Add(test)
        '_lstZAbsorbers.Add(test)

        'txtHeightRoom.Text = 5
        'txtWidthRoom.Text = 5
        'txtLengthRoom.Text = 5

    End Sub

#Region "Add absorbers"
    Private Sub btnZabsorbers_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnZabsorbers.Click
        Dim temp As New Absorber
        Dim dlg As New frmAbsorber(temp)
        With dlg
            If dlg.ShowDialog Then
                _lstZAbsorbers.Add(temp)
            End If
        End With
    End Sub

    Private Sub btnYabsorbers_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnYabsorbers.Click
        Dim temp As New Absorber
        Dim dlg As New frmAbsorber(temp)
        With dlg
            If dlg.ShowDialog Then
                _lstYAbsorbers.Add(temp)
            End If
        End With
    End Sub

    Private Sub btnXabsorbers_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnXabsorbers.Click
        Dim temp As New Absorber
        Dim dlg As New frmAbsorber(temp)
        With dlg
            If dlg.ShowDialog Then
                _lstXAbsorbers.Add(temp)
            End If
        End With
    End Sub
#End Region

#Region "Textbox changes"
    Private Sub txtHeightRoom_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txtHeightRoom.TextChanged
        If Not txtHeightRoom.Text = "" Then
            _Height = CDbl(txtHeightRoom.Text)
            If Not _Height = Nothing And Not _Width = Nothing And Not _Length = Nothing Then
                lblZarea.Content = 2 * _Width * _Length
                lblXrea.Content = 2 * _Width * _Height
                lblYrea.Content = 2 * _Length * _Height
                _TotalSurfaceArea = CDbl(lblZarea.Content) + CDbl(lblXrea.Content) + CDbl(lblYrea.Content)
                btnZabsorbers.IsEnabled = True
                btnXabsorbers.IsEnabled = True
                btnYabsorbers.IsEnabled = True

            ElseIf Not _Width = Nothing And Not _Length = Nothing Then
                'Floor ceiling
                lblZarea.Content = 2 * _Width * _Length
            ElseIf Not _Width = Nothing And Not Height = Nothing Then
                'Front and back walls
                lblXrea.Content = 2 * _Width * _Height
            ElseIf Not _Length = Nothing And Not Height = Nothing Then
                'Side walls
                lblYrea.Content = 2 * _Length * _Height
            End If
        End If
    End Sub

    Private Sub txtWidthRoom_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txtWidthRoom.TextChanged
        If Not txtWidthRoom.Text = "" Then
            _Width = CDbl(txtWidthRoom.Text)
            If Not _Height = Nothing And Not _Width = Nothing And Not _Length = Nothing Then
                lblZarea.Content = 2 * _Width * _Length
                lblXrea.Content = 2 * _Width * _Height
                lblYrea.Content = 2 * _Length * _Height
                _TotalSurfaceArea = CDbl(lblZarea.Content) + CDbl(lblXrea.Content) + CDbl(lblYrea.Content)
                btnZabsorbers.IsEnabled = True
                btnXabsorbers.IsEnabled = True
                btnYabsorbers.IsEnabled = True

            ElseIf Not _Width = Nothing And Not _Length = Nothing Then
                'Floor ceiling
                lblZarea.Content = 2 * _Width * _Length
            ElseIf Not _Width = Nothing And Not Height = Nothing Then
                'Front and back walls
                lblXrea.Content = 2 * _Width * _Height
            ElseIf Not _Length = Nothing And Not Height = Nothing Then
                'Side walls
                lblYrea.Content = 2 * _Length * _Height
            End If
        End If
    End Sub

    Private Sub txtLengthRoom_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles txtLengthRoom.TextChanged
        If Not txtLengthRoom.Text = "" Then
            _Length = CDbl(txtLengthRoom.Text)
            If Not _Height = Nothing And Not _Width = Nothing And Not _Length = Nothing Then
                lblZarea.Content = 2 * _Width * _Length
                lblXrea.Content = 2 * _Width * _Height
                lblYrea.Content = 2 * _Length * _Height
                _TotalSurfaceArea = CDbl(lblZarea.Content) + CDbl(lblXrea.Content) + CDbl(lblYrea.Content)
                btnZabsorbers.IsEnabled = True
                btnXabsorbers.IsEnabled = True
                btnYabsorbers.IsEnabled = True
            ElseIf Not _Width = Nothing And Not _Length = Nothing Then
                'Floor ceiling
                lblZarea.Content = 2 * _Width * _Length
            ElseIf Not _Width = Nothing And Not Height = Nothing Then
                'Front and back walls
                lblXrea.Content = 2 * _Width * _Height
            ElseIf Not _Length = Nothing And Not Height = Nothing Then
                'Side walls
                lblYrea.Content = 2 * _Length * _Height
            End If
        End If
    End Sub
#End Region

    'Calculatign all the parameters based on information supplied
    Private Sub btnCalculate_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCalculate.Click
        Dim FullOctaveBand() As String
        Dim OctaveBandString As String = "31,63,125,250,500,1000,2000,4000,8000,16000"
        FullOctaveBand = OctaveBandString.Split(",")

        If Not _lstXAbsorbers.Count = 0 And Not _lstYAbsorbers.Count = 0 And Not _lstZAbsorbers.Count = 0 Then
            TotalSufraceArea = _TotalSurfaceArea

            Humidity = CInt(txtHumidityRoom.Text)

            Sabine.Name = "Sabine"
            Eyring.Name = "Eyring"
            Miller.Name = "Millington-Sette"
            Fritzroy.Name = "Fritzroy"

            Volume = _Height * _Length * _Width

            For i As Integer = 0 To Sabine.Count - 1

                Dim SabineTempZ, SabineTempY, SabineTempX As Double
                SabineTempX = 0
                SabineTempY = 0
                SabineTempZ = 0

                Dim EyringTempZ, EyringTempY, EyringTempX As Double
                EyringTempZ = 0
                EyringTempY = 0
                EyringTempX = 0

                Dim MillingtonSetteTempZ, MillingtonSetteTempY, MillingtonSetteTempX As Double
                MillingtonSetteTempZ = 0
                MillingtonSetteTempY = 0
                MillingtonSetteTempX = 0

                For Each p As Absorber In _lstZAbsorbers
                    SabineTempZ += p.Area * p.GetItem(i)
                    EyringTempZ += p.Area * p.GetItem(i) / _TotalSurfaceArea
                    MillingtonSetteTempZ += p.Area * CalculateAlfa(p.GetItem(i))
                Next

                For Each p As Absorber In _lstYAbsorbers
                    SabineTempY += p.Area * p.GetItem(i)
                    EyringTempY += p.Area * p.GetItem(i) / _TotalSurfaceArea
                    MillingtonSetteTempY += p.Area * CalculateAlfa(p.GetItem(i))
                Next

                For Each p As Absorber In _lstXAbsorbers
                    SabineTempX += p.Area * p.GetItem(i)
                    EyringTempX += p.Area * p.GetItem(i) / _TotalSurfaceArea
                    MillingtonSetteTempX += p.Area * CalculateAlfa(p.GetItem(i))
                Next

                'Taken from Fundamentals of Acoustics
                Dim m As Double = 5.5 * 10 ^ (-4) * (50 / Humidity) * (CDbl(FullOctaveBand(i)) / 1000) ^ (1.7)

                'Calculationg total absorbtion areas pluss air annutaion 
                Dim AbsorbtionAreaSabine As Double = (SabineTempX + SabineTempY + SabineTempZ) + 4 * m * Volume
                Dim AbsorbtionAreaEyring As Double = _TotalSurfaceArea * CalculateAlfa(EyringTempX + EyringTempY + EyringTempZ) + 4 * m * Volume
                Dim AbsorbtionAreaMillingtonSette As Double = MillingtonSetteTempX + MillingtonSetteTempY + MillingtonSetteTempZ + 4 * m * Volume

                Dim FritzroyTempZ, FritzroyTempY, FritzroyTempX As Double
                FritzroyTempX = CDbl(lblXrea.Content) / (CalculateAlfa(EyringTempX) + (4 * m * Volume / 3) * (CDbl(lblXrea.Content) / _TotalSurfaceArea ^ 2))
                FritzroyTempY = CDbl(lblYrea.Content) / (CalculateAlfa(EyringTempY) + (4 * m * Volume / 3) * (CDbl(lblYrea.Content) / _TotalSurfaceArea ^ 2))
                FritzroyTempZ = CDbl(lblZarea.Content) / (CalculateAlfa(EyringTempZ) + (4 * m * Volume / 3) * (CDbl(lblZarea.Content) / _TotalSurfaceArea ^ 2))

                'Save the calculated reverberation time in different octave bands
                Sabine.SetItem(i, ReverberationTime(AbsorbtionAreaSabine))
                Eyring.SetItem(i, ReverberationTime(AbsorbtionAreaEyring))
                Miller.SetItem(i, ReverberationTime(AbsorbtionAreaMillingtonSette))
                Fritzroy.SetItem(i, (0.16 * Volume / (_TotalSurfaceArea ^ 2)) * (FritzroyTempX + FritzroyTempY + FritzroyTempZ))
            Next

            Dim AverageReverberationTimeSabine As Double
            AverageReverberationTimeSabine = 0
            For i As Integer = 0 To Sabine.Count - 1
                AverageReverberationTimeSabine += Sabine.GetItem(i) / Sabine.Count
            Next

            'Calculate the Schroeder frequency based on Sabine
            Schroeder = 2000 * Math.Sqrt(AverageReverberationTimeSabine / Volume)

            'Room radius is calculated besed on a omnidirectional source, meaning D = 1
            RoomRadius = Math.Sqrt(55.26 * Volume / (16 * Math.PI * C0 * AverageReverberationTimeSabine))

            lblSchroder.Content = Math.Round(Schroeder, 0).ToString
            lblRoomRadius.Content = Math.Round(RoomRadius, 2).ToString

            _lstResult2.Add(Sabine)
            _lstResult2.Add(Eyring)
            _lstResult2.Add(Miller)
            _lstResult2.Add(Fritzroy)

            lstResult.ItemsSource = _lstResult2

            For Each p As Absorber In _lstResult2
                PlotReverbrationTime(p, False)
            Next

        Else
            MessageBox.Show("missing absorbers")
        End If
    End Sub

    'Using this instead of -ln(1-alfa). This is the result of the taylor expantion of ln(1-alfa) 
    Private Function CalculateAlfa(ByVal N As Double) As Double
        Dim result As Double
        For i As Integer = 1 To 20
            result += (N ^ i) / i
        Next
        Return result
    End Function

    ''' <summary>
    ''' Calculates reverbration time based on absorbtion coefficient
    ''' </summary>
    ''' <param name="Ab"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReverberationTime(ByVal Ab As Double) As Double
        Dim result As Double = 55.26 * Volume / (C0 * Ab)
        Return result
    End Function

#Region "Plotting"
    Public Class PlotLinePoint
        Private _Value As Double
        Public Property Value() As Double
            Get
                Return _Value
            End Get
            Set(ByVal value As Double)
                _Value = value
            End Set
        End Property

        Private _Freq As String
        Public Property Freq() As String
            Get
                Return _Freq
            End Get
            Set(ByVal value As String)
                _Freq = value
            End Set
        End Property
    End Class

    Private Sub PlotReverbrationTime(ByVal CalculatedReverberationTime As Absorber, Optional ByVal Clear As Boolean = True)
        Dim str() As String
        Dim str2 As String = "31Hz,63Hz,125Hz,250Hz,500Hz,1kHz, 2kHz,4kHz,8kHz,16kHz"
        str = str2.Split(",")

        If Clear Then
            chrt2.Graphs.Clear()
        End If

        Dim TheTotalCurve As New System.Collections.ObjectModel.ObservableCollection(Of PlotLinePoint)

        Dim BindingSource As New Binding
        BindingSource.Source = TheTotalCurve

        For i As Integer = 0 To CalculatedReverberationTime.Count - 1
            Dim IndividualCurvePoint As New PlotLinePoint()
            IndividualCurvePoint.Freq = str(i)
            IndividualCurvePoint.Value = CalculatedReverberationTime.GetItem(i)
            TheTotalCurve.Add(IndividualCurvePoint)
        Next

        Dim NewLineCurve As New AmCharts.Windows.Line.LineChartGraph
        chrt2.Graphs.Add(NewLineCurve)

        NewLineCurve.SetBinding(AmCharts.Windows.Core.SerialGraph.DataItemsSourceProperty, BindingSource)
        NewLineCurve.SeriesIDMemberPath = "Freq"
        NewLineCurve.ValueMemberPath = "Value"

        NewLineCurve.LineThickness = 2
        NewLineCurve.Title = CalculatedReverberationTime.Name

        chrt2.Refresh()
    End Sub
#End Region

End Class

Public Class Absorber
    'http://www.cirrusresearch.co.uk/blog/2011/11/what-are-octave-and-third-octave-band-filters-on-a-sound-level-meter/
    '   31Hz	 63Hz 	125Hz	 250Hz	500Hz	1kHz 	2kHz	4kHz	8kHz	16kHz
    '22/07/2010

    Sub New()

    End Sub

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Area As Double
    Public Property Area() As Double
        Get
            Return _Area
        End Get
        Set(ByVal value As Double)
            _Area = value
        End Set
    End Property

    Private _Procent As Double
    Public Property Procent() As Double
        Get
            Return _Procent
        End Get
        Set(ByVal value As Double)
            _Procent = value
        End Set
    End Property

#Region "Frequency"
    Private _31Hz As Double
    Public Property f31Hz() As Double
        Get
            Return _31Hz
        End Get
        Set(ByVal value As Double)
            _31Hz = value
        End Set
    End Property

    Private _63Hz As Double
    Public Property f63Hz() As Double
        Get
            Return _63Hz
        End Get
        Set(ByVal value As Double)
            _63Hz = value
        End Set
    End Property

    Private _125Hz As Double
    Public Property f125Hz() As Double
        Get
            Return _125Hz
        End Get
        Set(ByVal value As Double)
            _125Hz = value
        End Set
    End Property

    Private _250Hz As Double
    Public Property f250Hz() As Double
        Get
            Return _250Hz
        End Get
        Set(ByVal value As Double)
            _250Hz = value
        End Set
    End Property

    Private _500Hz As Double
    Public Property f500Hz() As Double
        Get
            Return _500Hz
        End Get
        Set(ByVal value As Double)
            _500Hz = value
        End Set
    End Property

    Private _1000Hz As Double
    Public Property f1000Hz() As Double
        Get
            Return _1000Hz
        End Get
        Set(ByVal value As Double)
            _1000Hz = value
        End Set
    End Property

    Private _2000Hz As Double
    Public Property f2000Hz() As Double
        Get
            Return _2000Hz
        End Get
        Set(ByVal value As Double)
            _2000Hz = value
        End Set
    End Property

    Private _4000Hz As Double
    Public Property f4000Hz() As Double
        Get
            Return _4000Hz
        End Get
        Set(ByVal value As Double)
            _4000Hz = value
        End Set
    End Property

    Private _8000Hz As Double
    Public Property f8000Hz() As Double
        Get
            Return _8000Hz
        End Get
        Set(ByVal value As Double)
            _8000Hz = value
        End Set
    End Property

    Private _16000Hz As Double
    Public Property f16000Hz() As Double
        Get
            Return _16000Hz
        End Get
        Set(ByVal value As Double)
            _16000Hz = value
        End Set
    End Property

#End Region

    Public Function Count() As Integer
        Return 10
    End Function

    Public Function GetItem(ByVal i As Integer) As Double
        Select Case i
            Case 0
                Return _31Hz
            Case 1
                Return _63Hz
            Case 2
                Return _125Hz
            Case 3
                Return _250Hz
            Case 4
                Return _500Hz
            Case 5
                Return _1000Hz
            Case 6
                Return _2000Hz
            Case 7
                Return _4000Hz
            Case 8
                Return _8000Hz
            Case 9
                Return _16000Hz
            Case Else
                Throw New IndexOutOfRangeException
        End Select
    End Function

    Public Sub SetItem(ByVal i As Integer, ByVal Value As Double)
        Select Case i
            Case 0
                _31Hz = Value
            Case 1
                _63Hz = Value
            Case 2
                _125Hz = Value
            Case 3
                _250Hz = Value
            Case 4
                _500Hz = Value
            Case 5
                _1000Hz = Value
            Case 6
                _2000Hz = Value
            Case 7
                _4000Hz = Value
            Case 8
                _8000Hz = Value
            Case 9
                _16000Hz = Value
            Case Else
                Throw New IndexOutOfRangeException
        End Select
    End Sub
End Class