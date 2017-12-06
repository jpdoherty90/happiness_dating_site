using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Match.Controllers
{
    public class PreferenceController : Controller
    {
        private Context _context;
        public PreferenceController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/preference")]
        public IActionResult Index()
        {
            if (userNotLoggedIn()){
                return RedirectToAction("Index", "Home");
            }
            User currentUser = getCurrentUser();
            ViewBag.CurrentUser = currentUser;
            return View();
        }

        private bool userNotLoggedIn() {
            return (HttpContext.Session.GetInt32("currentUser") == null);
        }  

        private User getCurrentUser() {
            int myId = (int)HttpContext.Session.GetInt32("currentUser");
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == myId);
            return myUser;
        }

        [HttpGet]
        [Route("/preferences")]
        public IActionResult preferences()
        {
            if (userNotLoggedIn()){
                return RedirectToAction("Index", "Home");
            }
            return View("Preference");
        }


        [HttpPost]
        [Route("/addUserPreference")]
        public IActionResult addUserPreference(PreferenceViewModel userPreference, string salary, string divorced, string widowed, 
                                                string bodyDealbreaker, string ethnicity, string EthnicityDealBreaker, 
                                                string KidsDealBreaker, string DrinkingDealBreaker, string MarijuanaDealBreaker, 
                                                string DietDealBreaker, string PetsDealBreaker)
        {   
            int minHeight = (userPreference.MinimumFeet * 12) + userPreference.MinimumInch;
            int maxHeight = (userPreference.MaxFeet * 12) + userPreference.MaxInch;

            User currentUser = getCurrentUser();

            currentUser.Preference.min_age =  userPreference.MinAge;
            currentUser.Preference.max_age =  userPreference.MaxAge;
            currentUser.Preference.MinSalary = salary;
            currentUser.Preference.MinHeight = minHeight;
            currentUser.Preference.MaxHeight = maxHeight;
            currentUser.Preference.Build = userPreference.Build;
            currentUser.Preference.Ethnicity = ethnicity;
            currentUser.Preference.Kids = userPreference.Kids;
            currentUser.Preference.Drinking = userPreference.Drinking;
            currentUser.Preference.Marijuana = userPreference.Marijuana;
            currentUser.Preference.Diet = userPreference.Diet;
            currentUser.Preference.Pets = userPreference.Pets;

            currentUser.Preference.BuildDealBreaker = (bodyDealbreaker != null);
            currentUser.Preference.EthnicityDealBreaker = (EthnicityDealBreaker != null);
            currentUser.Preference.DivorcedDealBreaker = (divorced != null);
            currentUser.Preference.WidowedDealBreaker = (widowed != null);
            currentUser.Preference.KidsDealBreaker = (KidsDealBreaker != null);
            currentUser.Preference.DrinkingDealBreaker = (DrinkingDealBreaker != null);
            currentUser.Preference.MarijuanaDealBreaker = (MarijuanaDealBreaker != null);
            currentUser.Preference.DietDealBreaker = (DietDealBreaker == null);
            currentUser.Preference.PetsDealBreaker = (PetsDealBreaker != null);

            _context.SaveChanges();

            int currentUserId = getCurrentUserId();            
            List<User> AllUsers = _context.Users.Where(u => u.UserId != currentUserId).ToList();
            foreach(var person in AllUsers) {
                MatchingAlgorithm.Algorithm(currentUser.UserId, person.UserId);
            }

            return RedirectToAction("Dashboard", "Match");
        }

        private int getCurrentUserId() {
            int id = (int)HttpContext.Session.GetInt32("currentUser");
            return id;
        } 
        
        [HttpPost]
        [Route("/addUserInterest")]
        public IActionResult addUserInterest(InterestViewModel userInterest, string salary, string divorced, string widowed, string ethnicity, string userBio)
        {
            User currentUser = getCurrentUser();
            int height = (userInterest.feet * 12) + userInterest.inch;

            currentUser.height = height;
            currentUser.salary = salary; 
            currentUser.marijuana = userInterest.weed; 
            currentUser.build = userInterest.body;
            currentUser.pets = userInterest.pets;
            currentUser.diet = userInterest.diet;
            currentUser.kids = userInterest.kids;

            currentUser.Divorced = (userInterest.divorced == "Yes");
            currentUser.Widowed = (userInterest.widowed == "Yes");

            currentUser.ethnicity = userInterest.ethnicity;
            currentUser.drinking = userInterest.drinking;
            currentUser.religion = userInterest.religion;
            currentUser.bio = userBio;
            _context.SaveChanges();
            return Redirect("/preferences");
        }
    }
}