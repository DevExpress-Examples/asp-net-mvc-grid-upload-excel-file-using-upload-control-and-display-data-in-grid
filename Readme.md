<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128550806/16.1.11%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T449148)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Grid View for ASP.NET MVC - How to upload an Excel file using an upload control and display the file's data in the grid

This example demonstrates how to use an upload control to upload an Excel file to the server and handle the control's `FileUploadComplete` event to display the file's data in the grid.

## Overview

Create an upload control, specify its `CallbackRouteValues` action, and call the [GetUploadedFiles](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.UploadControlExtension.GetUploadedFiles.overloads) method to upload an Excel file. Save the file's path and use the `e.CallbackData` argument property to pass the path to the client.

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

To display the file's data in the grid, use the approach illustrated in the following example: [Grid View for ASP.NET MVC - How to bind the grid to an excel file](https://github.com/DevExpress-Examples/how-to-bind-gridview-with-excel-file-e4458).

## Files to Review

* [HomeController.cs](./CS/T449148/Controllers/HomeController.cs)
* [Model.cs](./CS/T449148/Models/Model.cs)
* [GridViewPartial.cshtml](./CS/T449148/Views/Home/GridViewPartial.cshtml)
* [Index.cshtml](./CS/T449148/Views/Home/Index.cshtml)

## More Examples

* [Grid View for ASP.NET Web Forms - How to display data from an uploaded Excel file](https://github.com/DevExpress-Examples/aspxgridview-upload-and-display-excel-file)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-mvc-grid-upload-excel-file-using-upload-control-and-display-data-in-grid&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-mvc-grid-upload-excel-file-using-upload-control-and-display-data-in-grid&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
