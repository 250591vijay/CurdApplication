using CurdApplication.Data;
using CurdApplication.Models.Account;
using CurdApplication.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CurdApplication.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LoginSignUpViewModel _model)
        {
            if (ModelState.IsValid)

            {
                // var data =context.Users.Where(e=> e.Username = _model.Username).SingleOrDefault();
                var data = _context.Users.Where(e => e.Username == _model.Username).SingleOrDefault();
                if (data != null)
                {
                    bool isValid = data.Username == _model.Username && data.Password == _model.Password;
                    if (isValid)
                    {
                        // IS identity m humne information store karwai user ki user kya le k aa rha hai claimTypes.Name aur name hum model.Username, is m humne user ki information ko 
                        // store karwa diya
                        var identity = new ClaimsIdentity(new[]
                        {new Claim(ClaimTypes.Name,_model.Username)},
                        CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal= new ClaimsPrincipal(identity);
                        //Signs in the current user.
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
                        //HttpContext.Session.SetString store data in key pair value and here Username is key and _model.Username is pair 
                        HttpContext.Session.SetString("Username",_model.Username);
                        // Session ka use hum sensative data like user and password ko store karne k liye karte hai 
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        TempData["errorPasswordMessage"] = "Invalid Password";
                        return View(_model);

                    }
                }
                else
                {
                    TempData["errorUserMessage"] = "User name not found";
                    return View(_model);
                }
            }
            else
            {
                return View(_model);
            }

        }
        [HttpGet]
        public IActionResult LogOut()
        {
            //Signs out the current user.
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // To destory cookies
            var storeCookies= Request.Cookies.Keys;
            foreach (var cookies in storeCookies)
            {
                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("LogIn","Account");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel _model)
        {
            //Model validation
            if (ModelState.IsValid)
            {
                var data = new User()
                {
                    Email = _model.Email,
                    Password = _model.Password,
                    Username = _model.Username,
                    Mobile = _model.Mobile,
                    IsActive = _model.IsActive,
                };
                _context.Users.Add(data);
                _context.SaveChanges();
                //return View(_model);
                TempData["SucessMessage"] = "You are eligibale to Login.Please fill credential to Login ";
                return RedirectToAction("LogIn");
            }
            else
            {
                TempData["errorMessage"] = "Empty form can not be filled";
                return View();
            }

        }
       
    }
}
