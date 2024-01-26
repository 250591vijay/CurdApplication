using CurdApplication.Data;
using CurdApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurdApplication.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext _context;

        public EmployeeController(ApplicationContext context)
        {
         this._context = context;
            
        }
        // Created action Index() create()
        public IActionResult Index()
        {
            var result = _context.Employees.ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee _model) 
        {
            //_context.Employees.Add(emp);
            if(ModelState.IsValid)
            {
                var emp =new Employee()
                {
                    Name= _model.Name,
                    City = _model.City,
                    State = _model.State,
                    Salary= _model.Salary,

                };
                //_context database ka instance hai 
                _context.Employees.Add(emp);
                _context.SaveChanges();
                TempData["error"]= "Record inserted";
                return RedirectToAction("Index");
               // return View();
            }
            else 
            {
                TempData["message"] = "Empty field cann't be Submit";
                return View();
            }                
        }
        public IActionResult Delete(int id)
        {
            // Linq "concept e.ID is database id and 'id' is selected id coming from Form "
            // singleorDefault data ko fetch karne k liye
            var emp = _context.Employees.SingleOrDefault(e=>e.Id==id);
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            TempData["error"] = "Record deleted";
            return RedirectToAction("Index");
        }
        [HttpGet]
        // id k behalf s data ko get karenge database s
        public IActionResult Edit(int id)
        {
            //Edit ka view hai
            var emp =_context.Employees.SingleOrDefault(e=> e.Id==id);
            var result= new Employee()
            {
                // data ko get karenge jab edit button p click ho ga tab aur sara data fill rahega jo id select karenge
                Name    = emp.Name,
                City    = emp.City,
                State   = emp.State,
                Salary  = emp.Salary,
            };
            // jasie hi edit p click karenge to text box m data fill milega
            return View(result);
        }
        [HttpPost]
        //Submit button post hai
        //httppost m database s data le k aata hai
        // jaise s submit karenge to model humara data le ke aayega to data ko phele data ko get karenge
        //  var emp = new Employee() is k through
        public IActionResult Edit(Employee _model) 
        {
            // Jaise hi submit p click karenge to post method me jitna v model humara data le k aayega us model k data ko humne emp variable m store kara liya
            var emp = new Employee() 
            {
                Id = _model.Id,
                Name = _model.Name, 
                City = _model.City,
                State = _model.State,
                Salary = _model.Salary,
             };
            _context.Employees.Update(emp);    
            _context.SaveChanges();
            TempData["error"] = "Record updated";
            return RedirectToAction("Index");
         }
    }
}
