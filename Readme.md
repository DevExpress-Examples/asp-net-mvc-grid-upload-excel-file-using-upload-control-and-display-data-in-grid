<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/T449148/Controllers/HomeController.cs)
* [Model.cs](./CS/T449148/Models/Model.cs)
* [GridViewPartial.cshtml](./CS/T449148/Views/Home/GridViewPartial.cshtml)
* [Index.cshtml](./CS/T449148/Views/Home/Index.cshtml)
<!-- default file list end -->
# GridView - How to upload an Excel file via UploadControl and display its data in a grid
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t449148/)**
<!-- run online end -->


<p>This example demonstrates how to upload an Excel file from your computer to the server using <strong>UploadControl</strong> and then display its data in <strong>GridView</strong>.<br><br>For this purpose, save a file uploaded via UploadedControl in its CallbackRouteValues controller action and pass the saved file's path to the client side by using the e.CallbackData parameter. Then, obtain this path in the UploadControl's client-side <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientUploadControl_FileUploadCompletetopic">FileUploadComplete</a> event handler and initiate GridView's callback in order to show the uploaded file's data in the grid:</p>


```cs
    settings.ClientSideEvents.FileUploadComplete = "function(s, e) { GridView1.PerformCallback({ path: e.callbackData}); }";
```


<p>To show the Excel file's data in GridView, you can use the approach illustrated in the <a href="https://www.devexpress.com/Support/Center/p/E4458">How to bind GridView with Excel file</a> example.</p>
<br><strong>See also:</strong><br><a href="https://www.devexpress.com/Support/Center/p/E5199">How to load an excel file to the server using ASPxUploadControl and display its data in ASPxGridView</a>

<br/>


