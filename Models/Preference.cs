using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class Preference : BaseEntity
    {
        public int PreferenceId { get; set; }

        public int min_age { get; set; }

        public int max_age { get; set; }

        public string MinSalary { get; set; }

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

        public string Kids { get; set; }

        public bool KidsDealBreaker { get; set; }

        public string Drinking { get; set; }

        public bool DrinkingDealBreaker { get; set; }

        public string Marijuana { get; set; }

        public bool MarijuanaDealBreaker { get; set; }

        public string Diet { get; set; }

        public bool DietDealBreaker { get; set; }

        public string Pets { get; set; }

        public bool PetsDealBreaker { get; set; }

        public Preference()
        {
            min_age = 0;
            max_age = 0;
            MinSalary = "";
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
            Diet = "";
            DietDealBreaker = false;
            Pets = "";
            PetsDealBreaker = false;
            DivorcedDealBreaker = false;
            WidowedDealBreaker = false;
        }
    }
}