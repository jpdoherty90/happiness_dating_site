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
            return View();
        }

        [HttpGet]
        [Route("/addUserPreferences")]
        public IActionResult addUserPreferences()
        {
            return View("Preference");
        }

        [HttpPost]
        [Route("/addUserInterest")]
        public IActionResult addUserInterest(InterestViewModel userInterest, string salary, string divorced, string widowed, string ethnicity)
        {
            Console.WriteLine("MADE IT TO ROUTE");
            Console.WriteLine("FEET:" + userInterest.feet);
            Console.WriteLine("INCH:" + userInterest.inch);
            Console.WriteLine("SALARY:" + salary);
            Console.WriteLine("WEED:" + userInterest.weed);
            Console.WriteLine("BODY:" + userInterest.body);
            Console.WriteLine("PETS:" + userInterest.pets);
            Console.WriteLine("DIET:" + userInterest.diet);
            Console.WriteLine("KIDS:" + userInterest.kids);
            Console.WriteLine("DIVORCED:" + divorced);
            Console.WriteLine("WIDOWED:" + widowed);
            Console.WriteLine("ETHNICITY:" + ethnicity);
            Console.WriteLine("DRINKING:" + userInterest.drinking);
            Console.WriteLine("RELIGION:" + userInterest.religion);
            return Redirect("/");
        }
    }
}
