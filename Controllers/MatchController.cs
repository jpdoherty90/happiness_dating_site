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
            if (userNotLoggedIn()) {
                return RedirectToAction("Index", "Home");
            }
            int id = getCurrentUserId();;
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == id);
            ViewBag.myUser = myUser;

            var location = getUserLocation(myUser.zipcode);
            ViewBag.city = location["city"];
            ViewBag.state = location["state"];

            return View();
        }


        private bool userNotLoggedIn() 
        {
            return (HttpContext.Session.GetInt32("currentUser") == null);
        }        


        private int getCurrentUserId() 
        {
            int id = (int)HttpContext.Session.GetInt32("currentUser");
            return id;
        }


        private Dictionary<string, object> getUserLocation(int postalCode) 
        {
            var location = new Dictionary<string, object>();
            WebRequest.GetZipDataAsync(postalCode, ApiResponse => {
                location = ApiResponse;
            }).Wait();
            return location;
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
        public IActionResult TestImg(IFormFile pic)
        {
            User myUser = getCurrentUser();
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


        private User getCurrentUser() {
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            return myUser;
        }


        [HttpGet]
        [Route("profile")]
        public IActionResult LoadUserProfile(IFormFile pic)
        {
            if (userNotLoggedIn()){
                return RedirectToAction("Index", "Home");
            }
            User myUser = getCurrentUser();
            ViewBag.myUser = myUser;
            return View("Dashboard");
        }


        [HttpGet]
        [Route("lovers/{myId}")]
        public IActionResult LoadLoverProfile(int myId)
        {
            if (userNotLoggedIn()){
                return RedirectToAction("Index", "Home");
            }
            int checkId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            ViewBag.myUser = myUser;
            List<Like> myLikes = _context.Likes.Where(like => like.PersonLikedId == myId && like.PersonLikingId == checkId).ToList();
            if (myLikes.Count != 0){
                ViewBag.liking = true;
            } else {
                ViewBag.liking = false;
            }
            var location = getUserLocation(myUser.zipcode);
            ViewBag.city = location["city"];
            ViewBag.state = location["state"];

            return View("Profile");
        }


        [HttpGet]
        [Route("lovers")]
        public IActionResult Matches() 
        {
            if (userNotLoggedIn()) {
                return RedirectToAction("Index", "Home");
            }
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            ViewBag.user = myUser;
            List<LoveMatch> MyMatches = _context.Matches.Where(match=> match.User1Id == myId || match.User2Id == myId).OrderByDescending(match => match.percentage).ToList();
            List<MatchHelper> LoveList = new List<MatchHelper>();
            foreach (var match in MyMatches) {
                MatchHelper newLove = new MatchHelper();
                User loverUser;
                List<Like> myLikes;
                if (match.User1Id != myId) {
                    myLikes = _context.Likes.Where(like => like.PersonLikedId == match.User1Id && like.PersonLikingId == myId).ToList();
                    loverUser = _context.Users.SingleOrDefault(user => user.UserId == match.User1Id);
                } else {
                    myLikes = _context.Likes.Where(like => like.PersonLikedId == match.User2Id && like.PersonLikingId == myId).ToList();
                    loverUser = _context.Users.SingleOrDefault(user => user.UserId == match.User2Id);
                }
                newLove.lover = loverUser;
                newLove.percentage = match.percentage;
                if (myLikes.Count != 0) {
                    newLove.liking = true;
                } else {
                    newLove.liking = false;
                }
                LoveList.Add(newLove);
            }
            ViewBag.myLovers = LoveList;
            return View();
        }


        public class MatchHelper {
            public User lover { get; set; }    
            public int percentage {get;set;}
            public bool liking {get;set;}
        }


        [HttpGet]
        [Route("like/{myId}")]
        public IActionResult LikeLover(int myId)
        {
            int currentId = getCurrentUserId();
            Like newLike = new Like{
                PersonLikingId = currentId,
                PersonLikedId = myId
            };
            _context.Add(newLike);
            _context.SaveChanges();
            return RedirectToAction("Matches");
        }


        [HttpGet]
        [Route("unlike/{myId}")]
        public IActionResult UnlikeLover(int myId)
        {
            int currentId = getCurrentUserId();
            Like likeToDelete = _context.Likes.SingleOrDefault(like => like.PersonLikedId == myId && like.PersonLikingId == currentId);
            _context.Remove(likeToDelete);
            _context.SaveChanges();
            return RedirectToAction("Matches");
        }


        [HttpGet]
        [Route("profunlike/{myId}")]
        public IActionResult ProfUnlikeLover(int myId)
        {
            int currentId = getCurrentUserId();
            Like likeToDelete = _context.Likes.SingleOrDefault(like => like.PersonLikedId == myId && like.PersonLikingId == currentId);
            _context.Remove(likeToDelete);
            _context.SaveChanges();
            int redirectId = myId;
            return RedirectToAction("LoadLoverProfile", new {myId = redirectId});
        }


        [HttpGet]
        [Route("proflike/{myId}")]
        public IActionResult ProfLikeLover(int myId)
        {
            int currentId = getCurrentUserId();
            Like newLike = new Like{
                PersonLikingId = currentId,
                PersonLikedId = myId
            };
            _context.Add(newLike);
            _context.SaveChanges();
            int redirectId = myId;
            return RedirectToAction("LoadLoverProfile", new {myId = redirectId});
        }


        [HttpGet]
        [Route("likes")]
        public IActionResult Likes()
        {
            if (userNotLoggedIn()){
                return RedirectToAction("Index", "Home");
            }
            int myId = getCurrentUserId();
            User myUser = getCurrentUser();
            List<Like> MyLikes = _context.Likes.Include(user => user.PersonLiked).Where(like=> like.PersonLikingId == myId).ToList();
            ViewBag.myLikes = MyLikes;
            return View();
        }

        [HttpGet]
        [Route("likers")]
        public IActionResult Likers()
        {
            if (userNotLoggedIn()){
                return RedirectToAction("Index", "Home");
            }
            int myId = getCurrentUserId();
            User myUser = getCurrentUser();
            List<Like> MyLikers = _context.Likes.Include(user => user.PersonLiking).Where(like=> like.PersonLikedId == myId).ToList();
            List<Like> AllMyLikes = _context.Likes.Where(like => like.PersonLikingId == myId).ToList();
            ViewBag.myLikers = MyLikers;
            ViewBag.AllMyLikes = AllMyLikes;
            return View();
        }

    }

}