Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports DevExpress.DataAccess.Excel

Public Class InMemoryModel

    Public Shared Function OpenExcelFile(ByVal path As String) As Object
        If path = String.Empty Then path = "~/Countries.xlsx"
        Dim fileName As String = If(path.StartsWith("~"), System.Web.HttpContext.Current.Server.MapPath(path), path)
        Dim excelDataSource As ExcelDataSource = New ExcelDataSource()
        excelDataSource.FileName = fileName
        Dim excelWorksheetSettings As ExcelWorksheetSettings = New ExcelWorksheetSettings()
        excelWorksheetSettings.WorksheetName = "Sheet1"
        Dim excelSourceOptions As ExcelSourceOptions = New ExcelSourceOptions()
        excelSourceOptions.ImportSettings = excelWorksheetSettings
        excelSourceOptions.SkipHiddenRows = False
        excelSourceOptions.SkipHiddenColumns = False
        excelSourceOptions.UseFirstRowAsHeader = True
        excelDataSource.SourceOptions = excelSourceOptions
        excelDataSource.Fill()
        Dim table As DataTable = excelDataSource.ToDataTable()
        Return table
    End Function
End Class

Module ExcelDataSourceExtension

    <Extension>
    Function ToDataTable(ByVal excelDataSource As ExcelDataSource) As DataTable
        Dim list As IList = (CType(excelDataSource, IListSource)).GetList()
        Dim dataView As DevExpress.DataAccess.Native.Excel.DataView = CType(list, DevExpress.DataAccess.Native.Excel.DataView)
        Dim tableData As DataTable = New DataTable()
        For i As Integer = 0 To dataView.Columns.Count - 1
            tableData.Columns.Add(dataView.Columns(i).Name, dataView.Columns(i).PropertyType)
        Next
        Dim values As Object() = New Object(dataView.Columns.Count - 1) {}
        For Each item As DevExpress.DataAccess.Native.Excel.ViewRow In list
            For i As Integer = 0 To values.Length - 1
                values(i) = dataView.Columns(i).GetValue(item)
            Next
            tableData.Rows.Add(values)
        Next
        Return tableData
    End Function
End Module