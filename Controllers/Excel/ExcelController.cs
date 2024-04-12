using CurdApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace CurdApplication.Controllers.Excel
{
    public class ExcelController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;

        public ExcelController(IConfiguration configuration, ApplicationContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Customers.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult ImportExcelFile()
        {
            return View();
        }
        [HttpPost]
        //file le aaeyenge uske liye hum parameter m iformfile class use karenge
        public IActionResult ImportExcelFile(IFormFile formFile)
        {
            try
            {
               
                    var mainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadExcelFile");
                    if (!Directory.Exists(mainPath))
                    {
                        // Agar path nhi hai to ye create kar dega
                        Directory.CreateDirectory(mainPath);
                    }
                    // filePath m main path aur file ka name jo upload karna hai
                    var filePath = Path.Combine(mainPath, formFile.FileName);
                    // File Stream file ko read and write karne ka permission deta hai
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    // fileName jo upload hoga file name get karenge
                    var fileName = formFile.FileName;
                    string extension = Path.GetExtension(fileName);
                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls":
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filePath + ";Extended Properties='Excel 8.0; HDR=YES'";
                            break;
                        case ".xlsx":
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                    }
                    // Temproray data table

                    DataTable dt = new DataTable();
                    conString = string.Format(conString, filePath);

                    // OleDbConnection connection established karega excel sheet s
                    using (OleDbConnection conExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = conExcel;
                                conExcel.Open();
                                DataTable dtExcelSchema = conExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                cmdExcel.CommandText = "SELECT * FROM [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                conExcel.Close();
                            }
                        }
                    }
                    // Ab database ka connection banayenge aur excel file m jo v hai usko map karange
                    conString = _configuration.GetConnectionString("DefaultConnection");
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "Customers";
                            sqlBulkCopy.ColumnMappings.Add("CustomerCode", "CustomerCode");
                            sqlBulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                            sqlBulkCopy.ColumnMappings.Add("LastName", "LastName");
                            sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                            sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                            sqlBulkCopy.ColumnMappings.Add("Age", "Age");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                    TempData["message"] = "File Imported sucessfully, Data Saved into Database ";
                    return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
    }
}
