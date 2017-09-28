using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;
using System.Linq;
using System.IO;


namespace Match.Controllers
{
    public class MatchController : Controller
    {
        private Context _context;
 
        public MatchController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("testing")]
        public async Task<IActionResult> TestImg(IFormFile pic){
            // int myId = (int)HttpContext.Session.GetInt32("currentUser");
            // User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            using (var ms = new MemoryStream())
            {
                await pic.CopyToAsync(ms);
                // myUser.profile_picture = ms.ToArray();
                ViewBag.test = ms.ToArray();
                Console.WriteLine("WRITING FILE STARTING HERE");
            }
            return View("Dashboard");
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult LoadUserProfile(IFormFile pic){
            return View("Profile");
        }

    }
}