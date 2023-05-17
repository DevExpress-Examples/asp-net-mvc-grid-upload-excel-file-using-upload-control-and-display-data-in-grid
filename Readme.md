# Grid View for ASP.NET MVC - How to upload an excel file using an upload control and display the file's data in the grid
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t449148/)**
<!-- run online end -->

This example demonstrates how to use an upload control to upload an excel file to the server and handle the control's `FileUploadComplete` event to display the file's data in the grid.

## Overview

Create an upload control, specify its `CallbackRouteValues` action, and call the [GetUploadedFiles](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.UploadControlExtension.GetUploadedFiles.overloads) method to upload an excel file. Save the file's path and use the `e.CallbackData` argument property to pass the path to the client.

```cs
public class HomeController : Controller {
    public ActionResult UploadControlUpload() {
        UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
        return null;
    }
}
public class UploadControlSettings {
    public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings() {
        AllowedFileExtensions = new string[] {".xlsx" },
        MaxFileSize = 40000000
    };
    public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e) {
        const string UploadDirectory = "~/Content/UploadedFiles/";
        if (e.UploadedFile.IsValid) {
            MemoryStream ms = new MemoryStream();
            // ...
            e.UploadedFile.SaveAs(resultFilePath);
            e.CallbackData = resultFilePath;
        }
    }
}
```

Handle the upload control's [FileUploadColmplete](https://docs.devexpress.com/AspNet/js-ASPxClientUploadControl.FileUploadComplete) event. In the handler, call the grid's `PerformCallback` method to send a callback to the server and pass the file's path as a parameter.

```cshtml
    settings.ClientSideEvents.FileUploadComplete = "function(s, e) { GridView1.PerformCallback({ path: e.callbackData}); }";
```

To display the file's data in the grid, use the approach illustrated in the following example: [Grid View for ASp.NET MVC - How to bind the grid to an excel file](https://github.com/DevExpress-Examples/how-to-bind-gridview-with-excel-file-e4458).

## Files to Review

* [HomeController.cs](./CS/T449148/Controllers/HomeController.cs)
* [Model.cs](./CS/T449148/Models/Model.cs)
* [GridViewPartial.cshtml](./CS/T449148/Views/Home/GridViewPartial.cshtml)
* [Index.cshtml](./CS/T449148/Views/Home/Index.cshtml)
