using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Match.Controllers
{
    public class AlgoController : Controller
    {
        private Context _context;

        public AlgoController(Context context)
        {
            _context = context;
        }

        public void Algorithm(int user1id, int user2id)
        {
            User user1 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user1id);
            User user2 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user2id);

            Preference user1prefs = user1.Preference;
            Preference user2prefs = user2.Preference;

            // First make sure they are the right gender for each other
            if (user1.seeking == user2.gender && user2.seeking == user1.gender)
            {

                // Next make sure they are in the right age range for each other
                if ((user1.age >= user2prefs.min_age && user1.age <= user2prefs.max_age) && (user2.age >= user1prefs.min_age && user2.age <= user1prefs.max_age))
                {

                    // Make sure both are within each other's height ranges
                    if ((user2.height >= user1prefs.MinHeight && user2.height <= user1prefs.MaxHeight) && (user1.height >= user2prefs.MinHeight && user1.height <= user2prefs.MaxHeight))
                    {

                        //Make sure they each make the minimum amount of cash the other is willing to accept
                        if ((user2.salary >= user1prefs.MinSalary) && (user1.salary >= user2prefs.MinSalary))
                        {

                            // Make sure divorce isn't an issue for either party
                            if (!(user1.Divorced == true && user2prefs.DivorcedDealBreaker == true) && !(user2.Divorced == true && user1prefs.DivorcedDealBreaker == true)) 
                            {

                                // Make sure being widowed isn't a big deal for either party
                                if (!(user1.Widowed == true && user2prefs.WidowedDealBreaker == true) && !(user2.Widowed == true && user1prefs.WidowedDealBreaker == true))
                                {

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

                                    if (MatchPercent >= 70) {

                                        Percentage newPercentage = new Percentage
                                        {
                                            Percent = MatchPercent
                                        };

                                        user1.Matches.Add(user2);
                                        user1.MatchPercentages.Add(newPercentage);
                                        user2.Matches.Add(user1);
                                        user2.MatchPercentages.Add(newPercentage);

                                        _context.SaveChanges();



                                    }

                                }

                            }

                        }

                    }

                }

            } 

        }


    }

}