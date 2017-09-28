using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class Preference : BaseEntity
    {
        public int id { get; set; }

        public int min_age { get; set; }

        public int max_age { get; set; }

        public string Interests { get; set; }

        public int MinSalary { get; set; }

        public int MinHeight { get; set; }

        public int MaxHeight { get; set; }

        public string Build { get; set; }

        public bool BuildDealBreaker { get; set; }

        public string Ethnicity { get; set; }

        public bool EthnicityDealBreaker { get; set; }

        public string Religion { get; set; }

        public bool ReligionDealBreaker { get; set; }

        public bool DivorcedDealBreaker { get; set; }

        public bool WidowedDealBreaker { get; set; }

        public string PresentKids { get; set; }

        public bool PresentKidsDealBreaker { get; set; }

        public string FutureKids { get; set; }

        public bool FutureKidsDealBreaker { get; set; }

        public string Drinking { get; set; }

        public bool DrinkingDealBreaker { get; set; }

        public string Marijuana { get; set; }

        public bool MarijuanaDealBreaker { get; set; }

        public string Cigarettes { get; set; }

        public bool CigarettesDealBreaker { get; set; }

        public string Tattoos { get; set; }

        public bool TattoosDealBreaker { get; set; }

        public string Diet { get; set; }

        public bool DietDealBreaker { get; set; }

        public string Exercise { get; set; }

        public bool ExerciseDealBreaker { get; set; }

        public string Sex { get; set; }

        public bool SexDealBreaker { get; set; }

        public string Education { get; set; }

        public bool EducationDealBreaker { get; set; }

        public string Pets { get; set; }

        public bool PetsDealBreaker { get; set; }


       
        public Preference()
        {
            min_age = 0;
            max_age = 0;

            MinSalary = 0;
            MinHeight = 0;
            MaxHeight = 0;

            Build = "";
            BuildDealBreaker = false;
            Ethnicity = "";
            EthnicityDealBreaker = false;
            Religion = "";
            ReligionDealBreaker = false;
            Drinking = "";
            DrinkingDealBreaker = false;
            Marijuana = "";
            MarijuanaDealBreaker = false;
            Cigarettes = "";
            CigarettesDealBreaker = false;
            Exercise = "";
            ExerciseDealBreaker = false;
            Diet = "";
            DietDealBreaker = false;
            Education = "";
            EducationDealBreaker = false;
            Sex = "";
            SexDealBreaker = false;
            Pets = "";
            PetsDealBreaker = false;
            

            DivorcedDealBreaker = false;
            WidowedDealBreaker = false;


            PresentKids = "";
            PresentKidsDealBreaker = false;
            FutureKids = "";
            FutureKidsDealBreaker = false;
            Tattoos = "";
            TattoosDealBreaker = false;

            
            Interests = "";

        }
    }
}