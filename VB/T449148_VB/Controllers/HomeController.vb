Imports DevExpress.Web.Mvc
Imports System.Web.Mvc
Imports System.IO

Namespace Controllers
    Public Class HomeController
        Inherits Controller

        ' GET: Home
        Function Index() As ActionResult
            If (Session("DataTableModel") Is Nothing) Then
                Session("DataTableModel") = InMemoryModel.OpenExcelFile("")
            End If
            Return View(Session("DataTableModel"))

        End Function

        Public Function GridViewPartial(ByVal path As String) As ActionResult
            Dim model = Session("DataTableModel")
            If Not String.IsNullOrEmpty(path) Then
                model = InMemoryModel.OpenExcelFile(path)
                Session("DataTableModel") = model
            End If
            Return PartialView(model)
        End Function
        Public Function UploadControlUpload() As ActionResult
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, AddressOf UploadControlSettings.FileUploadComplete)
            Return Nothing
        End Function

    End Class

    Public Class UploadControlSettings
        Public Shared UploadValidationSettings As DevExpress.Web.UploadControlValidationSettings = New DevExpress.Web.UploadControlValidationSettings() With {.AllowedFileExtensions = New String() {".xlsx"}, .MaxFileSize = 40000000}

        Public Shared Sub FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs)
            Const UploadDirectory As String = "~/Content/UploadedFiles/"
            If e.UploadedFile.IsValid Then
                Dim ms As MemoryStream = New MemoryStream()
                Dim resultExtension As String = Path.GetExtension(e.UploadedFile.FileName)
                Dim resultFileName As String = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension)
                Dim resultFileUrl As String = UploadDirectory & resultFileName
                Dim resultFilePath As String = System.Web.HttpContext.Current.Server.MapPath("~/") + e.UploadedFile.FileName
                e.UploadedFile.SaveAs(resultFilePath)
                e.CallbackData = resultFilePath
            End If
        End Sub
    End Class
End Namespace