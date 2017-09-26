using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;

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
        public IActionResult addUser(UserViewModel newUser)
        {   
            Console.WriteLine(newUser.gender);
            Console.WriteLine(newUser.zipcode);
            Console.WriteLine(newUser.name);
            Console.WriteLine(newUser.username);
            Console.WriteLine(newUser.email);
            Console.WriteLine(newUser.password);
            Console.WriteLine(newUser.age);
            return Redirect("/");
        }
    }
}
