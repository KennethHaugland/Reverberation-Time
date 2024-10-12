Imports Microsoft.Office.Interop

Public Class frmAbsorber
    Private Abs As Absorber
    Private temp As New System.Collections.ObjectModel.ObservableCollection(Of Absorber)

    Private ImportedFromExcel As New System.Collections.ObjectModel.ObservableCollection(Of Absorber)

    Private Function GetParent(ByVal path As String) As String
            Dim directoryInfo As System.IO.DirectoryInfo
            directoryInfo = System.IO.Directory.GetParent(path)
            Return directoryInfo.FullName
    End Function

    Public Sub New(ByRef _Abs As Absorber)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Abs = _Abs
        temp.Add(Abs)
        lstAbs.ItemsSource = temp
        Dim path As String = GetParent(GetParent(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)))
        Dim filename As String = path & "\AbsorberList.xlsx"

        ' Create new Application.
        Dim excel As Excel.Application = New Excel.Application

        ' Open Excel spreadsheet.
        Dim w As Excel.Workbook = excel.Workbooks.Open(filename)
        Dim sheet As Excel.Worksheet = w.Sheets(1)
        Dim usedRange As Excel.Range = sheet.UsedRange

        Dim darray(,) As Object
        darray = CType(usedRange.Value, Object(,))

        Dim rows As Integer = darray.GetUpperBound(0)
        Dim cols As Integer = darray.GetUpperBound(1)
        For ii As Integer = 2 To rows
            Dim g As New Absorber
            For j As Integer = 1 To cols
                If j = 1 Then
                    g.Name = darray(ii, j).ToString
                ElseIf j > 1 Then
                    g.SetItem(j - 2, CDbl(darray(ii, j).ToString))
                End If
            Next
            ImportedFromExcel.Add(g)
        Next
        w.Close()

        For i As Integer = 0 To ImportedFromExcel.Count - 1
            cmbSelectFromExcel.Items.Add(ImportedFromExcel(i).Name)
        Next

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Abs.Name = temp(0).Name
        Abs.Area = temp(0).Area
        For i As Integer = 0 To temp(0).Count - 1
            Abs.SetItem(i, temp(0).GetItem(i))
        Next
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button2.Click
        Me.DialogResult = False
        Me.Close()
    End Sub

    Private Sub cmbSelectFromExcel_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles cmbSelectFromExcel.SelectionChanged
        Dim something As New Absorber
        something = ImportedFromExcel(cmbSelectFromExcel.SelectedIndex)
        temp(0) = something
    End Sub
End Class
