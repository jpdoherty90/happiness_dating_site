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
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            ViewBag.myUser = myUser;
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
            return View("Profile");
        }

    }
}