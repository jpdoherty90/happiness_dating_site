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
        private static Context _context;
        public PreferenceController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/preference")]
        public IActionResult Index()
        {
            if (userNotLoggedIn()) { return RedirectToAction("Index", "Home"); }
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
            if (userNotLoggedIn()) { return RedirectToAction("Index", "Home"); }
            return View("Preference");
        }

        [HttpPost]
        [Route("/addUserPreference")]
        public IActionResult addUserPreference(PreferenceViewModel userPreference, string salary, string divorced, 
                                                string widowed, string bodyDealbreaker, string ethnicity, 
                                                string EthnicityDealBreaker, string KidsDealBreaker, 
                                                string DrinkingDealBreaker, string MarijuanaDealBreaker, 
                                                string DietDealBreaker, string PetsDealBreaker)
        {   
            int minHeight = (userPreference.MinimumFeet * 12) + userPreference.MinimumInch;
            int maxHeight = (userPreference.MaxFeet * 12) + userPreference.MaxInch;

            User currentUser = getCurrentUserWithPreferences();

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

            foreach(var person in AllUsers) 
            {
                MatchingAlgorithm(currentUser.UserId, person.UserId);
            }
            return RedirectToAction("Dashboard", "Match");
        }

        private User getCurrentUserWithPreferences() {
            int myId = getCurrentUserId();
            User myUser = _context.Users.Include(u => u.Preference).SingleOrDefault(findUser => findUser.UserId == myId);
            return myUser;
        }

        private int getCurrentUserId() {
            int id = (int)HttpContext.Session.GetInt32("currentUser");
            return id;
        } 

        public static void MatchingAlgorithm(int user1id, int user2id)
        {
            User user1 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user1id);
            User user2 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user2id);

            Preference user1Preferences = user1.Preference;
            Preference user2Preferences = user2.Preference;

            if (!usersAreSeekingEachOther(user1, user2)
                || !usersInEachOthersAgeRange(user1, user2, user1Preferences, user2Preferences)
                || !usersInEachOthersHeightRanges(user1, user2, user1Preferences, user2Preferences)) {
                return;
            }

            int u2Salary = Convert.ToInt32(user2.salary.Replace("$", "").Replace(",", "").Replace(".", ""));
            int u1Salary = Convert.ToInt32(user1.salary.Replace("$", "").Replace(",", "").Replace(".", ""));
            int u2SalalryPreference = Convert.ToInt32(user2Preferences.MinSalary.Replace("$", "").Replace(",", "").Replace(".", ""));
            int u1SalaryPreference = Convert.ToInt32(user1Preferences.MinSalary.Replace("$", "").Replace(",", "").Replace(".", ""));
            if ((u2Salary >= u1SalaryPreference) && (u1Salary >= u2SalalryPreference))
            {
                if (!(user1.Divorced == true && user2Preferences.DivorcedDealBreaker == true) && !(user2.Divorced == true && user1Preferences.DivorcedDealBreaker == true)) 
                {
                    if (!(user1.Widowed == true && user2Preferences.WidowedDealBreaker == true) && !(user2.Widowed == true && user1Preferences.WidowedDealBreaker == true))
                    {
                        int count = determineCount(user1, user2, user1Preferences, user2Preferences);
                        if (count == -1) {
                            return;
                        }
                        int MatchPercent = (int) ((((float)count)/16.0)*100.0);
                        if (MatchPercent >= 50) {
                            generateNewMatch(MatchPercent, user1.UserId, user2.UserId);
                        }
                    }
                }
            }
        }

        private static bool usersAreSeekingEachOther(User user1, User user2) {
            return (user1.seeking == user2.gender && user2.seeking == user1.gender);
        }

        private static bool usersInEachOthersAgeRange(User user1, User user2, Preference user1Pref, Preference user2Pref) {
            bool user1Passes = (user1.age >= user2Pref.min_age && user1.age <= user2Pref.max_age);
            bool user2Passes = (user2.age >= user1Pref.min_age && user2.age <= user1Pref.max_age);
            return (user1Passes && user2Passes);
        }

        private static bool usersInEachOthersHeightRanges(User user1, User user2, Preference user1Pref, Preference user2Pref) {
            bool user1Passes = (user1.height >= user2Pref.MinHeight && user1.height <= user2Pref.MaxHeight);
            bool user2Passes = (user2.height >= user1Pref.MinHeight && user2.height <= user1Pref.MaxHeight);
            return (user1Passes && user2Passes);
        }

        private static int determineCount(User user1, User user2, Preference u1Prefs, Preference u2Prefs) 
        {
            int count = 0;
            bool thereIsADealbreaker = false;

            incrementCount(user1.build, u2Prefs.Build, u2Prefs.BuildDealBreaker);
            incrementCount(user2.build, u1Prefs.Build, u1Prefs.BuildDealBreaker);
            incrementCount(user1.ethnicity, u2Prefs.Ethnicity, u2Prefs.EthnicityDealBreaker);
            incrementCount(user2.ethnicity, u1Prefs.Ethnicity, u1Prefs.EthnicityDealBreaker);
            incrementCount(user1.religion, u2Prefs.Religion, u2Prefs.ReligionDealBreaker);
            incrementCount(user2.religion, u1Prefs.Religion, u1Prefs.ReligionDealBreaker);
            incrementCount(user1.drinking, u2Prefs.Drinking, u2Prefs.DrinkingDealBreaker);
            incrementCount(user2.drinking, u1Prefs.Drinking, u1Prefs.DrinkingDealBreaker);
            incrementCount(user1.marijuana, u2Prefs.Marijuana, u2Prefs.MarijuanaDealBreaker);
            incrementCount(user2.marijuana, u1Prefs.Marijuana, u1Prefs.MarijuanaDealBreaker);
            incrementCount(user1.diet, u2Prefs.Diet, u2Prefs.DietDealBreaker);
            incrementCount(user2.diet, u1Prefs.Diet, u1Prefs.DietDealBreaker);
            incrementCount(user1.pets, u2Prefs.Pets, u2Prefs.PetsDealBreaker);
            incrementCount(user2.pets, u1Prefs.Pets, u1Prefs.PetsDealBreaker);
            incrementCount(user1.kids, u2Prefs.Kids, u2Prefs.KidsDealBreaker);
            incrementCount(user2.kids, u1Prefs.Kids, u1Prefs.KidsDealBreaker);

            void incrementCount(string value, string pref, bool dealbreaker) {
                if (value == pref) {
                    count += 1;
                } else if (dealbreaker) {
                    thereIsADealbreaker = true;
                }
            }
            if (thereIsADealbreaker) {
                return -1;
            }
            else {
                return count;
            }

        }

        private static void generateNewMatch(int matchPercent, int id1, int id2) {
            LoveMatch newMatch = new LoveMatch {
                percentage = matchPercent,
                User1Id = id1,
                User2Id = id2
            };
            _context.Add(newMatch);
            _context.SaveChanges();
        }

        [HttpPost]
        [Route("/addUserInterest")]
        public IActionResult addUserInterest(InterestViewModel userInterest, string salary, string divorced, string widowed,
                                                string ethnicity, string userBio)
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