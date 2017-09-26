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
        [Route("addUser")]
        public IActionResult addUser(UserViewModel model)
        {   
            Console.WriteLine(model.gender);
            Console.WriteLine(model.zipcode);
            Console.WriteLine(model.name);
            Console.WriteLine(model.username);
            Console.WriteLine(model.email);
            Console.WriteLine(model.password);
            Console.WriteLine(model.age);
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
            User currentUser = _context.Users.SingleOrDefault(user => user.email == model.email);
            HttpContext.Session.SetInt32("currentUser", (int)currentUser.UserId);
            return Redirect("/");
        }
    }
}
