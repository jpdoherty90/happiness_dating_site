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
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            User currentUser = _context.Users.SingleOrDefault(findUser => findUser.UserId == currentUserId);
            ViewBag.CurrentUser = currentUser;
            return View();
        }

        [HttpGet]
        [Route("/preferences")]
        public IActionResult preferences()
        {
            return View("Preference");
        }

        [HttpPost]
        [Route("/addUserPreference")]
        public IActionResult addUserPreference(PreferenceViewModel userPreference, string salary, string divorced, string widowed, string bodyDealbreaker, string ethnicity, string EthnicityDealBreaker, string KidsDealBreaker, string DrinkingDealBreaker, string MarijuanaDealBreaker, string DietDealBreaker, string PetsDealBreaker)
        {   
            int minHeight = (userPreference.MinimumFeet * 12) + userPreference.MinimumInch;
            int maxHeight = (userPreference.MaxFeet * 12) + userPreference.MaxInch;

            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            User currentUser = _context.Users.Include(u => u.Preference).SingleOrDefault(findUser => findUser.UserId == currentUserId);
            Console.WriteLine("CURRENT USER:");
            Console.WriteLine(currentUser.name);

            currentUser.Preference.min_age =  userPreference.MinAge;
            currentUser.Preference.max_age =  userPreference.MaxAge;
            currentUser.Preference.MinSalary = salary;
            currentUser.Preference.MinHeight = minHeight;
            currentUser.Preference.MaxHeight = maxHeight;

            currentUser.Preference.Build = userPreference.Build;
            if(bodyDealbreaker == null){
                currentUser.Preference.BuildDealBreaker = false;
            }
            else{
                currentUser.Preference.BuildDealBreaker = true;
            }

            currentUser.Preference.Ethnicity = ethnicity;
            if(EthnicityDealBreaker == null){
                currentUser.Preference.EthnicityDealBreaker = false;
            }
            else{
                currentUser.Preference.EthnicityDealBreaker = true;
            }

            if(divorced == null){
                currentUser.Preference.DivorcedDealBreaker = false;
            }
            else{
                currentUser.Preference.DivorcedDealBreaker = true;
            }

            if(widowed == null){
                currentUser.Preference.WidowedDealBreaker = false;
            }
            else{
                currentUser.Preference.WidowedDealBreaker = true;
            }

            currentUser.Preference.Kids = userPreference.Kids;
            if(KidsDealBreaker == null){
                currentUser.Preference.KidsDealBreaker = false;
            }
            else{
                currentUser.Preference.KidsDealBreaker = true;
            }

            currentUser.Preference.Drinking = userPreference.Drinking;
            if(DrinkingDealBreaker == null){
                currentUser.Preference.DrinkingDealBreaker = false;
            }
            else{
                currentUser.Preference.DrinkingDealBreaker = true;
            }

            currentUser.Preference.Marijuana = userPreference.Marijuana;
            if(MarijuanaDealBreaker == null){
                currentUser.Preference.MarijuanaDealBreaker = false;
            }
            else{
                currentUser.Preference.MarijuanaDealBreaker = true;
            }

            currentUser.Preference.Diet = userPreference.Diet;
            if(DietDealBreaker == null){
                currentUser.Preference.DietDealBreaker = false;
            }
            else{
                currentUser.Preference.DietDealBreaker = true;
            }

            currentUser.Preference.Pets = userPreference.Pets;
            if(PetsDealBreaker == null){
                currentUser.Preference.PetsDealBreaker = false;
            }
            else{
                currentUser.Preference.PetsDealBreaker = true;
            }
            _context.SaveChanges();
            
            List<User> AllUsers = _context.Users.Where(u => u.UserId != currentUserId).ToList();
            foreach(var guy in AllUsers) {
                Algorithm(currentUser.UserId, guy.UserId);
            }

            return RedirectToAction("Dashboard", "Match");
        }

        public void Algorithm(int user1id, int user2id)
        {
            Console.WriteLine("STARTING THE ALGORITHM");

            User user1 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user1id);
            User user2 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user2id);

            Preference user1prefs = user1.Preference;
            Preference user2prefs = user2.Preference;

            // First make sure they are the right gender for each other
            if (user1.seeking == user2.gender && user2.seeking == user1.gender)
            {
                Console.WriteLine("MADE IT PAST GENDER");

                // Next make sure they are in the right age range for each other
                if ((user1.age >= user2prefs.min_age && user1.age <= user2prefs.max_age) && (user2.age >= user1prefs.min_age && user2.age <= user1prefs.max_age))
                {
                    Console.WriteLine("MADE IT PAST AGE");

                    // Make sure both are within each other's height ranges
                    if ((user2.height >= user1prefs.MinHeight && user2.height <= user1prefs.MaxHeight) && (user1.height >= user2prefs.MinHeight && user1.height <= user2prefs.MaxHeight))
                    {
                        Console.WriteLine("MADE IT PAST HEIGHT");

                        //Make sure they each make the minimum amount of cash the other is willing to accept
                        int U2sal = Convert.ToInt32(user2.salary.Replace("$", "").Replace(",", "").Replace(".", ""));
                        int U1sal = Convert.ToInt32(user1.salary.Replace("$", "").Replace(",", "").Replace(".", ""));
                        int U2salPref = Convert.ToInt32(user2prefs.MinSalary.Replace("$", "").Replace(",", "").Replace(".", ""));
                        int U1salPref = Convert.ToInt32(user1prefs.MinSalary.Replace("$", "").Replace(",", "").Replace(".", ""));
                        if ((U2sal >= U1salPref) && (U1sal >= U2salPref))
                        {
                            Console.WriteLine("MADE IT PAST SALARY");

                            // Make sure divorce isn't an issue for either party
                            if (!(user1.Divorced == true && user2prefs.DivorcedDealBreaker == true) && !(user2.Divorced == true && user1prefs.DivorcedDealBreaker == true)) 
                            {
                                Console.WriteLine("MADE IT PAST DIVORCE");

                                // Make sure being widowed isn't a big deal for either party
                                if (!(user1.Widowed == true && user2prefs.WidowedDealBreaker == true) && !(user2.Widowed == true && user1prefs.WidowedDealBreaker == true))
                                {
                                    Console.WriteLine("MADE IT PAST WIDOWED");

                                    // Then factor in all the other shit:

                                    int Count = 0;
                                    
                                    bool StringWithDealBreaker(string val, string pref, bool DBreaker) {
                                        if (val == pref) {
                                            Count += 1;
                                        } else if (DBreaker) {
                                            return true;
                                        }
                                        return false;
                                    }

                                    bool u1build = StringWithDealBreaker(user1.build, user2prefs.Build, user2prefs.BuildDealBreaker);
                                    bool u2build = StringWithDealBreaker(user2.build, user1prefs.Build, user1prefs.BuildDealBreaker);
                                    bool u1ethnicity = StringWithDealBreaker(user1.ethnicity, user2prefs.Ethnicity, user2prefs.EthnicityDealBreaker);
                                    bool u2ethnicity = StringWithDealBreaker(user2.ethnicity, user1prefs.Ethnicity, user1prefs.EthnicityDealBreaker);
                                    bool u1Religion = StringWithDealBreaker(user1.religion, user2prefs.Religion, user2prefs.ReligionDealBreaker);
                                    bool u2Religion = StringWithDealBreaker(user2.religion, user1prefs.Religion, user1prefs.ReligionDealBreaker);
                                    bool u1Drinking = StringWithDealBreaker(user1.drinking, user2prefs.Drinking, user2prefs.DrinkingDealBreaker);
                                    bool u2Drinking = StringWithDealBreaker(user2.drinking, user1prefs.Drinking, user1prefs.DrinkingDealBreaker);
                                    bool u1Marijuana = StringWithDealBreaker(user1.marijuana, user2prefs.Marijuana, user2prefs.MarijuanaDealBreaker);
                                    bool u2Marijuana = StringWithDealBreaker(user2.marijuana, user1prefs.Marijuana, user1prefs.MarijuanaDealBreaker);
                                    bool u1Diet = StringWithDealBreaker(user1.diet, user2prefs.Diet, user2prefs.DietDealBreaker);
                                    bool u2Diet = StringWithDealBreaker(user2.diet, user1prefs.Diet, user1prefs.DietDealBreaker);
                                    bool u1Pets = StringWithDealBreaker(user1.pets, user2prefs.Pets, user2prefs.PetsDealBreaker);
                                    bool u2Pets = StringWithDealBreaker(user2.pets, user1prefs.Pets, user1prefs.PetsDealBreaker);
                                    bool u1Kids = StringWithDealBreaker(user1.kids, user2prefs.Kids, user2prefs.KidsDealBreaker);
                                    bool u2Kids = StringWithDealBreaker(user2.kids, user1prefs.Kids, user1prefs.KidsDealBreaker);

                                    if (u1build
                                    || u2build
                                    || u1ethnicity
                                    || u2ethnicity
                                    || u1Religion
                                    || u2Religion
                                    || u1Drinking
                                    || u2Drinking
                                    || u1Marijuana
                                    || u2Marijuana
                                    || u1Kids
                                    || u2Kids
                                    || u1Diet
                                    || u2Diet
                                    || u1Pets
                                    || u2Pets) {
                                        return;
                                    }

                                    int MatchPercent = (int) ((((float)Count)/16.0)*100.0);

                                    Console.WriteLine("MATCH PERCENTAGE");
                                    Console.WriteLine(MatchPercent);

                                    if (MatchPercent >= 70) {

                                        LoveMatch newLove = new LoveMatch
                                        {
                                            percentage = MatchPercent,
                                            User1Id = user1.UserId,
                                            User2Id = user2.UserId

                                        };

                                        _context.Add(newLove);
                                        _context.SaveChanges();



                                    }

                                }

                            }

                        }

                    }

                }

            } 

        }

        [HttpPost]
        [Route("/addUserInterest")]
        public IActionResult addUserInterest(InterestViewModel userInterest, string salary, string divorced, string widowed, string ethnicity, string userBio)
        {
            int? currentUserId = HttpContext.Session.GetInt32("currentUser");
            User currentUser = _context.Users.SingleOrDefault(findUser => findUser.UserId == currentUserId);
            int height = (userInterest.feet * 12) + userInterest.inch;
            currentUser.height = height;
            currentUser.salary = salary; 
            currentUser.marijuana = userInterest.weed; 
            currentUser.build = userInterest.body;
            currentUser.pets = userInterest.pets;
            currentUser.diet = userInterest.diet;
            currentUser.kids = userInterest.kids;
            if(userInterest.divorced == "Yes"){
                currentUser.Divorced = true;
            }else{
                currentUser.Divorced = false;
            }
            if(userInterest.widowed == "Yes"){
                currentUser.Widowed = true;
            }else{
                currentUser.Widowed = false;
            }
            currentUser.ethnicity = userInterest.ethnicity;
            currentUser.drinking = userInterest.drinking;
            currentUser.religion = userInterest.religion;
            currentUser.bio = userBio;
            _context.SaveChanges();
            return Redirect("/preferences");
        }
    }
}
