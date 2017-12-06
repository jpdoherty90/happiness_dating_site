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
    public static class MatchingAlgorithm
    {
        private static Context _context;

        public static void Algorithm(int user1id, int user2id)
        {
            User user1 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user1id);
            User user2 = _context.Users.Include(u => u.Preference).SingleOrDefault(user => user.UserId == user2id);

            Preference user1Preferences = user1.Preference;
            Preference user2Preferences = user2.Preference;

            if (user1.seeking == user2.gender && user2.seeking == user1.gender)
            {
                if ((user1.age >= user2Preferences.min_age && user1.age <= user2Preferences.max_age) && (user2.age >= user1Preferences.min_age && user2.age <= user1Preferences.max_age))
                {
                    if ((user2.height >= user1Preferences.MinHeight && user2.height <= user1Preferences.MaxHeight) && (user1.height >= user2Preferences.MinHeight && user1.height <= user2Preferences.MaxHeight))
                    {
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
                                    int Count = 0;

                                    bool isDealBreaker(string val, string pref, bool DBreaker) {
                                        if (val == pref) {
                                            Count += 1;
                                        } else if (DBreaker) {
                                            return true;
                                        }
                                        return false;
                                    }

                                    bool u1build = isDealBreaker(user1.build, user2Preferences.Build, user2Preferences.BuildDealBreaker);
                                    bool u2build = isDealBreaker(user2.build, user1Preferences.Build, user1Preferences.BuildDealBreaker);
                                    bool u1ethnicity = isDealBreaker(user1.ethnicity, user2Preferences.Ethnicity, user2Preferences.EthnicityDealBreaker);
                                    bool u2ethnicity = isDealBreaker(user2.ethnicity, user1Preferences.Ethnicity, user1Preferences.EthnicityDealBreaker);
                                    bool u1Religion = isDealBreaker(user1.religion, user2Preferences.Religion, user2Preferences.ReligionDealBreaker);
                                    bool u2Religion = isDealBreaker(user2.religion, user1Preferences.Religion, user1Preferences.ReligionDealBreaker);
                                    bool u1Drinking = isDealBreaker(user1.drinking, user2Preferences.Drinking, user2Preferences.DrinkingDealBreaker);
                                    bool u2Drinking = isDealBreaker(user2.drinking, user1Preferences.Drinking, user1Preferences.DrinkingDealBreaker);
                                    bool u1Marijuana = isDealBreaker(user1.marijuana, user2Preferences.Marijuana, user2Preferences.MarijuanaDealBreaker);
                                    bool u2Marijuana = isDealBreaker(user2.marijuana, user1Preferences.Marijuana, user1Preferences.MarijuanaDealBreaker);
                                    bool u1Diet = isDealBreaker(user1.diet, user2Preferences.Diet, user2Preferences.DietDealBreaker);
                                    bool u2Diet = isDealBreaker(user2.diet, user1Preferences.Diet, user1Preferences.DietDealBreaker);
                                    bool u1Pets = isDealBreaker(user1.pets, user2Preferences.Pets, user2Preferences.PetsDealBreaker);
                                    bool u2Pets = isDealBreaker(user2.pets, user1Preferences.Pets, user1Preferences.PetsDealBreaker);
                                    bool u1Kids = isDealBreaker(user1.kids, user2Preferences.Kids, user2Preferences.KidsDealBreaker);
                                    bool u2Kids = isDealBreaker(user2.kids, user1Preferences.Kids, user1Preferences.KidsDealBreaker);

                                    if (u1build || u2build || u1ethnicity || u2ethnicity || u1Religion || u2Religion
                                    || u1Drinking || u2Drinking || u1Marijuana || u2Marijuana || u1Kids || u2Kids 
                                    || u1Diet || u2Diet || u1Pets || u2Pets) {
                                        return;
                                    }

                                    int MatchPercent = (int) ((((float)Count)/16.0)*100.0);

                                    if (MatchPercent >= 50) {
                                        generateNewMatch(MatchPercent, user1.UserId, user2.UserId);
                                    }

                                }
                            }
                        }
                    }
                }
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

    }
}