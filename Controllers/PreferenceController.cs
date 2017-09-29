using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;
using System.Linq;

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
            User currentUser = _context.Users.SingleOrDefault(findUser => findUser.UserId == currentUserId);

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
            return Redirect("/dashboard");
        }

        [HttpPost]
        [Route("/addUserInterest")]
        public IActionResult addUserInterest(InterestViewModel userInterest, string salary, string divorced, string widowed, string ethnicity)
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
            _context.SaveChanges();
            return Redirect("/preferences");
        }
    }
}
