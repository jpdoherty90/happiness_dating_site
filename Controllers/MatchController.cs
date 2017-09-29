using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Match.Controllers
{
    public class MatchController : Controller
    {
        private Context _context;
        private IHostingEnvironment _hostingEnv;
        private string appRootFolder;
        public MatchController(Context context, IHostingEnvironment env)
        {
            _context = context;
            _hostingEnv = env;
            appRootFolder = _hostingEnv.ContentRootPath;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("currentUser") == null){
                return RedirectToAction("Index", "Home");
            }

            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            ViewBag.myUser = myUser;

            var Loc = new Dictionary<string, object>();
            WebRequest.GetZipDataAsync(myUser.zipcode, ApiResponse =>
                {
                    Loc = ApiResponse;
                }
            ).Wait();
            ViewBag.city = Loc["city"];
            ViewBag.state = Loc["state"];

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
        public IActionResult TestImg(IFormFile pic){
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            var filename = appRootFolder + "/wwwroot/images/" + myUser.username + ".jpg";
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
            using (FileStream fs = System.IO.File.Create(filename))
            {
                pic.CopyTo(fs);
                fs.Flush();
            }
            myUser.profile_picture = "~/images/" + myUser.username + ".jpg";
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult LoadUserProfile(IFormFile pic){
            if (HttpContext.Session.GetInt32("currentUser") == null){
                return RedirectToAction("Index", "Home");
            }

            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            ViewBag.myUser = myUser;

            var Loc = new Dictionary<string, object>();
            WebRequest.GetZipDataAsync(myUser.zipcode, ApiResponse =>
                {
                    Loc = ApiResponse;
                }
            ).Wait();
            ViewBag.city = Loc["city"];
            ViewBag.state = Loc["state"];

            return View("Dashboard");
        }


        [HttpGet]
        [Route("lovers")]
        public IActionResult Matches()
        {
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.Include(user=> user.Matches).SingleOrDefault(user => user.UserId == myId);
            ViewBag.user = myUser;
            return View();
        }

    }
}