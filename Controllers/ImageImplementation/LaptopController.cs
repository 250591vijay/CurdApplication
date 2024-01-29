using CurdApplication.Data;
using Microsoft.AspNetCore.Mvc;

namespace CurdApplication.Controllers.ImageImplementation
{
    public class LaptopController : Controller
    {
        private ApplicationContext _context;

        public LaptopController(ApplicationContext context)
        {
            this._context=context;
        }
        public IActionResult Index()
        {
         var data = _context.Laptops.ToList();   
            return View();
        }
    }
}
