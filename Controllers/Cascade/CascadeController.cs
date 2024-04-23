using CurdApplication.Data;
using CurdApplication.Models.Cascade;
using Microsoft.AspNetCore.Mvc;

namespace CurdApplication.Controllers.Cascade
{
    public class CascadeController : Controller
    {
        private ApplicationContext _context;

        public CascadeController(ApplicationContext context)
        {
            this._context = context;
            
        }
        // to get country information usk liye action banenge Country()
        // JSON It is universal data exchange formate hai 
        // Meaning if you want to send something from one language to another or want to send something from server side to client side they may be different language in both the place.So how will the variable of one be undersatnd by the other?. So we use JSON


        public JsonResult Country()
        {
            var cnt = _context.Countries.ToList();
            return new JsonResult(cnt);
        }
        // to get State information usk liye action banenge State()
        // and get data in foriegn key from country
        public JsonResult State(int id)
        {
            var st= _context.States.Where(e=>e.Country.Id==id).ToList();
            return new JsonResult(st);
        }
        // to get City information usk liye action banenge City()
        // and get data in foriegn key from State
        public JsonResult City(int id)
        {
            var ct =_context.Cities.Where(e=>e.State.Id==id).ToList();
            return new JsonResult(ct);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CascadeDropdown()
        {
            return View();
        }
    }
}
