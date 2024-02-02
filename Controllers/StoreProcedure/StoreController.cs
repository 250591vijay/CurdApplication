using CurdApplication.Data;
using CurdApplication.Migrations;
using CurdApplication.Models.ImageImplementation;
using CurdApplication.Models.StoreProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;

namespace CurdApplication.Controllers.StoreProcedure
{
    public class StoreController : Controller
    {
        private readonly ApplicationContext _context;

        public StoreController(ApplicationContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Wendors.FromSqlRaw("exec spGetAllWendorList").ToList();
            return View(data);

        }
        // Insert Wendor
        // To get data from Insert_Wendor form
        public IActionResult Insert_Vendor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert_Vendor(Wendor _model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Database.ExecuteSqlRaw()
                    // This statement also work but variable sequence need to be asp per enter in store procedure
                    //context.Database.ExecuteSqlRaw($"spInsert_Wendo '{model.Name}','{model.Gender}','{model.City}',{model.PinCode}");
                    //'{_model.Name}'karne s space v enter ho jayega

                    //_context.Database.ExecuteSqlRaw($"spInsert_Wendor @name='{_model.Name}',@gender='{_model.Gender}',@city='{_model.City}',@pincode='{_model.PinCode}'");

                    // Other way to insert record by store procedure

                    string parameter = $"spInsert_Wendor @name='{_model.Name}',@gender='{_model.Gender}',@city='{_model.City}',@pincode='{_model.PinCode}'";
                    _context.Database.ExecuteSqlRaw(parameter);
                    TempData["Sucess"] = "Record Inserted Sucessfully";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "ModelState is not Valid!");
            }
            catch (Exception ex)
            {
                // Synatx to show exception message
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            // data base m ja ke operation perform karna hai to ExecuteSqlRaw() method is use for Database
            var data = _context.Database.ExecuteSqlRaw($"spDelete_Wendor '{id}'");
            TempData["Sucess"] = "Record deleted sucessfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                // If sirf table s data ko access jana hai to FormSqlRaw() method use karte hai
                var data = _context.Wendors.FromSqlRaw($"spGetWendorById'{id}'");
                Wendor w = new Wendor();
                foreach (var d in data)
                {
                    w.Id = d.Id;
                    w.Name = d.Name;
                    w.Gender = d.Gender;
                    w.City = d.City;
                    w.PinCode = d.PinCode;

                }
                return View(w);
            }
        }
        [HttpPost]
        // data jo l ke aaenge edit httpget method s use hum update karenge to update karne k liye data hum model s pick karenge
        public IActionResult Edit(Wendor _model)
        {
            if (ModelState.IsValid)
            {
                string parameter = $"spUpdate_Wendor @id ='{_model.Id}',@name='{_model.Name}',@gender='{_model.Gender}',@city='{_model.City}',@pincode='{_model.PinCode}'";
                _context.Database.ExecuteSqlRaw(parameter);
                TempData["Sucess"] = "Record Updated Sucessfully";
                return RedirectToAction("Index");
            }
            return View(_model);
        }
    }
}
