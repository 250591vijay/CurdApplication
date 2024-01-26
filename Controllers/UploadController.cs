using CurdApplication.Data;
using CurdApplication.Models;
using CurdApplication.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq.Expressions;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace CurdApplication.Controllers
{
    public class UploadController : Controller
    {
        // instance is created of context
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;

        public UploadController(ApplicationContext context, IWebHostEnvironment environment)
        {
            // instance is created of context
            this._context = context;
            this._environment = environment;
        }
        public IActionResult Index()
        {
            var data = _context.Images.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadImage(ImageCreateModel _model)
        {  
            if(ModelState.IsValid) 
            {
                var path = _environment.WebRootPath;
                var filePath = "Content/Image/"+_model.ImagePath.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadFile(_model.ImagePath,fullPath);
                // file jo upload karenge wo Image naam k table me sstore karenge in list form
                var data= new Image() 
                { 
                    Name = _model.Name,
                    ImagePath = filePath,
                    
                };
                _context.Images.Add(data);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
            }
            else
            {
                return View(_model);
            }  
        }
        //function to upload image file
       public void UploadFile(IFormFile file, string path)
        {
            try 
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                file.CopyTo(fileStream);
             
            }

            catch (Exception ex)
            {
                //return RedirectToAction("Index");
                // Handle the exception (log, display error message, etc.)
            }
        }
       
    }
}
