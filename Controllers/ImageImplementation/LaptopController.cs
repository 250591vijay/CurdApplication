using CurdApplication.Data;
using CurdApplication.Migrations;
using CurdApplication.Models.ImageImplementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace CurdApplication.Controllers.ImageImplementation
{
    public class LaptopController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;

        public LaptopController(ApplicationContext context, IWebHostEnvironment environment)
        {
            this._context = context;
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
                if (ModelState.IsValid)
                {
                    // is uniqueFileName variable m jo unique id aayega UploadImage s wo show hoga
                    string uniqueFileName = UploadImage(_model);
                    var data = new Laptop()
                    {
                        Brand = _model.Brand,
                        Description = _model.Description,
                        Path = uniqueFileName
                    };
                    _context.Laptops.Add(data);
                    _context.SaveChanges();

                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, "Model property is not valid, please check");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(_model);
        }
        public string UploadImage(Laptop _model)
        {
            string uniqueFileName = string.Empty;
            if (_model.ImagePath != null)
            {
                // IWebHostEnvironment path image ko upload kha karenge iske liye wwwroot folder m laptop naam s folder create karenge
                // It tell ki kaha p hume image to upload karenge
                string uploadFolder = Path.Combine(_environment.WebRootPath, "Content/Laptop/");
                // ye image ka unique id show karega
                uniqueFileName = Guid.NewGuid().ToString() + "_" + _model.ImagePath.FileName;
                //_ = Guid.NewGuid().ToString() + "_" + _model.ImagePath.FileName;
                //string uniqueFileName = Guid.NewGuid().ToString() + "_" + _model.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    _model.ImagePath.CopyTo(fileStream);
                }

            }
            return uniqueFileName;
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = _context.Laptops.Where(e => e.Id == id).SingleOrDefault();
                if (data != null)
                {
                    // Delete image data from wwwroot folder
                    string deleteFromFolder = Path.Combine(_environment.WebRootPath, "Content/Laptop/");
                    string currentImage = Path.Combine(deleteFromFolder, data.Path);
                    if (currentImage != null)
                    {
                        if (System.IO.File.Exists(currentImage))
                        {
                            System.IO.File.Delete(currentImage);
                        }
                    }
                    // Delete record from database
                    _context.Laptops.Remove(data);
                    _context.SaveChanges();
                    TempData["Sucess"] = "Record Deleted Sucessfully";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = _context.Laptops.Where(e => e.Id == id).SingleOrDefault();
            return View(data);
        }
        [HttpGet]
        // To  get image id which we want to delete 
        public IActionResult Edit(int id)
        {
            var data = _context.Laptops.Where(e => e.Id == id).SingleOrDefault();
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Edit(Laptop _model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // To get current id which need to be delete
                    var data = _context.Laptops.Where(e => e.Id == _model.Id).SingleOrDefault();
                    string uniqueFileName = string.Empty;
                    // Delete the data
                    if (_model.ImagePath != null)
                    {
                        if (data.Path != null)
                        {
                            string filePath = Path.Combine(_environment.WebRootPath, "Content/Laptop", data.Path);
                            if (System.IO.File.Exists(filePath))
                            {
                                // This will delete image from wwwroot-Content-Laptop
                                System.IO.File.Delete(filePath);
                            }
                        }
                        uniqueFileName = UploadImage(_model);
                    }
                    data.Brand = _model.Brand;
                    data.Description = _model.Description;
                    if (_model.ImagePath != null)
                    {
                        data.Path = uniqueFileName;
                    }
                    _context.Laptops.Update(data);
                    _context.SaveChanges();
                    TempData["Success"] = "Record Updated Successfully!";
                }
                else
                {
                    return View(_model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
