using System;
using DevExpress.DataAccess.Excel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace T449148.Models
{
    public class InMemoryModel
    {

        public static object OpenExcelFile(string path)
        {
            if (path == string.Empty)
                path = "~/Countries.xlsx";

            string fileName = path.StartsWith("~") ? System.Web.HttpContext.Current.Server.MapPath(path) : path;

            ExcelDataSource excelDataSource = new ExcelDataSource();
            excelDataSource.FileName =  fileName;
            ExcelWorksheetSettings excelWorksheetSettings = new ExcelWorksheetSettings();
            excelWorksheetSettings.WorksheetName = "Sheet1";

            ExcelSourceOptions excelSourceOptions = new ExcelSourceOptions();
            excelSourceOptions.ImportSettings = excelWorksheetSettings;
            excelSourceOptions.SkipHiddenRows = false;
            excelSourceOptions.SkipHiddenColumns = false;
            excelSourceOptions.UseFirstRowAsHeader = true;
            excelDataSource.SourceOptions = excelSourceOptions;

            excelDataSource.Fill();

            DataTable table = excelDataSource.ToDataTable();
            return table;
        }
    }

    public static class ExcelDataSourceExtension {
        public static DataTable ToDataTable(this ExcelDataSource excelDataSource) {
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> properties = dataView.Columns.ToList<PropertyDescriptor>();

            DataTable table = new DataTable();
            for (int i = 0; i < properties.Count; i++) {
                PropertyDescriptor prop = properties[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list) {
                for (int i = 0; i < values.Length; i++) {
                    values[i] = properties[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}