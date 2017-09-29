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
        [Route("lovers/{myId}")]
        public IActionResult LoadLoverProfile(int myId){
            int checkId = (int)HttpContext.Session.GetInt32("currentUser");
            if (HttpContext.Session.GetInt32("currentUser") == null){
                return RedirectToAction("Index", "Home");
            }
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            ViewBag.myUser = myUser;
            List<Like> myLikes = _context.Likes.Where(like => like.PersonLikedId == myId && like.PersonLikingId == checkId).ToList();
            if (myLikes.Count != 0){
                ViewBag.liking = true;
            } else {
                ViewBag.liking = false;
            }
            var Loc = new Dictionary<string, object>();
            WebRequest.GetZipDataAsync(myUser.zipcode, ApiResponse =>
                {
                    Loc = ApiResponse;
                }
            ).Wait();
            ViewBag.city = Loc["city"];
            ViewBag.state = Loc["state"];

            return View("Profile");
        }


        [HttpGet]
        [Route("lovers")]
        public IActionResult Matches()
        {
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            ViewBag.user = myUser;
            List<LoveMatch> MyMatches = _context.Matches.Where(match=> match.User1Id == myId || match.User2Id == myId).OrderByDescending(match => match.percentage).ToList();
            List<User> AllOtherUsers = _context.Users.Where(user => user.UserId != myId).ToList();
            List<MatchHelper> LoveList = new List<MatchHelper>();
            foreach(var match in MyMatches){
                if (match.User1Id != myId) {
                    MatchHelper newLove = new MatchHelper();
                    List<Like> myLikes = _context.Likes.Where(like => like.PersonLikedId == match.User1Id && like.PersonLikingId == myId).ToList();
                    User loverUser = _context.Users.SingleOrDefault(user => user.UserId == match.User1Id);
                    newLove.lover = loverUser;
                    newLove.percentage = match.percentage;
                    if (myLikes.Count != 0) {
                        newLove.liking = true;
                    } else {
                        newLove.liking = false;
                    }
                    LoveList.Add(newLove);
                } else {
                    MatchHelper newLove = new MatchHelper();
                    List<Like> myLikes = _context.Likes.Where(like => like.PersonLikedId == match.User2Id && like.PersonLikingId == myId).ToList();
                    User loverUser = _context.Users.SingleOrDefault(user => user.UserId == match.User2Id);
                    newLove.lover = loverUser;
                    newLove.percentage = match.percentage;
                    if (myLikes.Count != 0) {
                        newLove.liking = true;
                    } else {
                        newLove.liking = false;
                    }
                    LoveList.Add(newLove);
                }
            }

            ViewBag.myLovers = LoveList;
            return View();
        }

        [HttpGet]
        [Route("like/{myId}")]
        public IActionResult LikeLover(int myId){
            int currentId = (int)HttpContext.Session.GetInt32("currentUser");
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
        public IActionResult UnlikeLover(int myId){
            int currentId = (int)HttpContext.Session.GetInt32("currentUser");
            Like byeLike = _context.Likes.SingleOrDefault(like => like.PersonLikedId == myId && like.PersonLikingId == currentId);
            _context.Remove(byeLike);
            _context.SaveChanges();
            return RedirectToAction("Matches");
        }

        [HttpGet]
        [Route("profunlike/{myId}")]
        public IActionResult ProfUnlikeLover(int myId){
            int currentId = (int)HttpContext.Session.GetInt32("currentUser");
            Like byeLike = _context.Likes.SingleOrDefault(like => like.PersonLikedId == myId && like.PersonLikingId == currentId);
            _context.Remove(byeLike);
            _context.SaveChanges();
            int redirectId = myId;
            return RedirectToAction("LoadLoverProfile", new {myId = redirectId});
        }

        [HttpGet]
        [Route("proflike/{myId}")]
        public IActionResult ProfLikeLover(int myId){
            int currentId = (int)HttpContext.Session.GetInt32("currentUser");
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
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            List<Like> MyLikes = _context.Likes.Include(user => user.PersonLiked).Where(like=> like.PersonLikingId == myId).ToList();
            ViewBag.myLikes = MyLikes;
            return View();
        }

        [HttpGet]
        [Route("likers")]
        public IActionResult Likers()
        {
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            List<Like> MyLikers = _context.Likes.Include(user => user.PersonLiking).Where(like=> like.PersonLikedId == myId).ToList();
            List<Like> AllMyLikes = _context.Likes.Where(like => like.PersonLikingId == myId).ToList();
            ViewBag.myLikers = MyLikers;
            ViewBag.AllMyLikes = AllMyLikes;
            return View();
        }

    }

    public class MatchHelper {
        public User lover { get; set; }    
        public int percentage {get;set;}
        public bool liking {get;set;}

    }
}