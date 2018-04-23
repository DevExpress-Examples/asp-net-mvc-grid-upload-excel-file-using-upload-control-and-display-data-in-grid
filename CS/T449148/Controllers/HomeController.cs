using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T449148.Models;

namespace T449148.Controllers
{
    public class HomeController : Controller
    {
       [HttpGet]
        public ActionResult Index()
        {
            if (Session["DataTableModel"] == null)
                Session["DataTableModel"] = InMemoryModel.OpenExcelFile("");

            return View(Session["DataTableModel"]);   
        }
        [HttpPost]
        public ActionResult GridViewPartial(string path)
        {
            var model = Session["DataTableModel"];
            if (!string.IsNullOrEmpty(path))
            {
                model = InMemoryModel.OpenExcelFile(path);
                Session["DataTableModel"] = model;
            }
            return PartialView(model);
        }

        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
        }
    }
    public class UploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] {".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            const string UploadDirectory = "~/Content/UploadedFiles/";
            if (e.UploadedFile.IsValid)
            {
                MemoryStream ms = new MemoryStream();
                string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
                string resultFileName = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension);
                string resultFileUrl = UploadDirectory + resultFileName;
                string resultFilePath = System.Web.HttpContext.Current.Server.MapPath("~/") + e.UploadedFile.FileName;
                e.UploadedFile.SaveAs(resultFilePath);
                e.CallbackData = resultFilePath;

            }
        }
    }

}