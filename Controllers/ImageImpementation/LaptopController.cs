using CurdApplication.Data;
using CurdApplication.Models.ImageImplementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CurdApplication.Controllers.ImageImpementation
{
    public class LaptopController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;

        public LaptopController(ApplicationContext context, IWebHostEnvironment environment)
        {
            // instance of database to connect from database ApplicationContext context
            this._context = context;
            // instance of IWebHostEnvironment to upload file
            this._environment = environment;
        }
        public IActionResult Index()
        { 
            var data = _context.Laptops.ToList();
            return View(data);
        }
        public IActionResult AddLaptop()     
        {
            return View();  
        }
        [HttpPost]
        public IActionResult AddLaptop(Laptop _model) 
        {
            try 
                
            { 
              if(ModelState.IsValid) 
                    {

                // UlpoadImage method m model provide karenge jis m s data le k aana hai
                     string uniqueFileName = UploadImage(_model);
                     var data = new Laptop()
                     {
                         Brand= _model.Brand,
                         Description= _model.Description,
                         Path= uniqueFileName 
                     };
                    _context.Laptops.Add(data);
                    _context.SaveChanges();
                    TempData["Sucess"]= "Record Sucessfuly saved!";
                    return RedirectToAction("Index");
                    }   
              ModelState.AddModelError(String.Empty,"Model property is not Valid, Please check");
            }
            catch (Exception ex) 
            { 
                ModelState.AddModelError(string.Empty, ex.Message);
             }
           return View(_model);  
        }
        // Create method to uploadImage file
        private string UploadImage(Laptop _model)
        {
            string uniqueFileName = string.Empty;

            if(_model.ImagePath != null) 
                {
                // Upload kaha karenge image ko is k liye wwwroot m ke folder banayenge to laptop name s
                string uploadFolder =Path.Combine(_environment.WebRootPath,"Content/Laptop/");
                // GUID globally unique identifier it give unique name to file
                uniqueFileName = Guid.NewGuid().ToString() + "_" + _model.ImagePath.FileName;
                //_= Guid.NewGuid().ToString() +"_"+_model.ImagePath.FileName;
                //string uniqueFileName = Guid.NewGuid().ToString() + "_" + _model.ImagePath.FileName;
                
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath,FileMode.Create)) 
                    {
                        _model.ImagePath.CopyTo(fileStream);
                    }

                }
          
            return uniqueFileName;
        }

    }
}
