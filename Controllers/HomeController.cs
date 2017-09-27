using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;
using System.Linq;

namespace Match.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("loginUser")]
        public IActionResult loginUser(LoginViewModel testLogin){
            if(ModelState.IsValid){
                User currentUser = _context.Users.SingleOrDefault(findUser => findUser.email == testLogin.email);
                if(currentUser != null){
                    HttpContext.Session.SetInt32("currentUser", (int)currentUser.UserId);
                    return Redirect("/");
                }
                else{
                    ModelState.AddModelError("email", "Email or Password is incorrect.");
                    return View("Login", testLogin);
                }
            }
            else{
                return View("Login", testLogin);
            }
        }

        [HttpPost]
        [Route("addUser")]
        public IActionResult addUser(UserViewModel model)
        {   
            string Gender = "";
            string Seeking = "";
            if(model.gender == "1"){
                Gender = "Male";
                Seeking = "Female";
            }
            else if(model.gender == "2"){
                Gender = "Male";
                Seeking = "Male";
            }
            else if(model.gender == "3"){
                Gender = "Female";
                Seeking = "Male";
            }
            else if(model.gender == "4"){
                Gender = "Female";
                Seeking = "Female";
            }
            User newUser = new User{
                gender = Gender,
                seeking = Seeking,
                zipcode = model.zipcode,
                name = model.name,
                username = model.username,
                email = model.email,
                password = model.password,
                age = model.age
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            Console.WriteLine("LOOK HERE!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine(model.email);
            User currentUser = _context.Users.SingleOrDefault(user => user.email == newUser.email);
            Console.WriteLine("CURRENT USER:");
            Console.WriteLine(currentUser.name);
            Console.WriteLine(currentUser.username);
            HttpContext.Session.SetInt32("currentUser", (int)currentUser.UserId);
            return Redirect("/");
        }
    }
}
