@Code
    ViewData("Title") = "Index"
End Code
@ModelType System.Data.DataTable
<h2>Index</h2>



@Using (Html.BeginForm("UploadControlUpload", "Home", FormMethod.Post))
    @Html.DevExpress().UploadControl(Sub(settings)
                                          settings.Name = "UploadControlFile"
                                          settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "UploadControlUpload"}
                                          settings.ShowUploadButton = True
                                          settings.ShowProgressPanel = True
                                          settings.NullText = "Click here to browse files..."
                                          settings.ClientSideEvents.FileUploadComplete = "function(s, e) { GridView1.PerformCallback({ path: e.callbackData}); }"
                                      End Sub).GetHtml()
End Using


@Html.Partial("GridViewPartial", Model)