using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class PreferenceViewModel : BaseEntity
    { 
        [Display(Name = "Min Age")]
        public int MinAge { get; set; }

        [Display(Name = "Max Age")]
        public int MaxAge { get; set; }

        [Display(Name = "Minimum Salary")]
        public string MinSalary { get; set; }

        [Display(Name = "Min Feet")]
        public int MinimumFeet { get; set; }

        [Display(Name = "Min Inch")]
        public int MinimumInch { get; set; }

        [Display(Name = "Max Feet")]
        public int MaxFeet { get; set; }

        [Display(Name = "Max Inch")]
        public int MaxInch { get; set; }

        [Display(Name = "Body Type")]
        public string Build { get; set; }

        [Display(Name = "Body Type DealBreaker")]
        public bool BuildDealBreaker { get; set; }

        [Display(Name = "Ethnicity")]
        public string Ethnicity { get; set; }

        [Display(Name = "Ethnicity DealBreaker")]
        public string EthnicityDealBreaker { get; set; }

        [Display(Name = "Divorced DealBreaker")]
        public bool DivorcedDealBreaker { get; set; }

        [Display(Name = "Widowed DealBreaker")]
        public bool WidowedDealBreaker { get; set; }

        [Display(Name = "Kids")]
        public string Kids { get; set; }

        [Display(Name = "KidsDealBreaker")]
        public bool KidsDealBreaker { get; set; }

        [Display(Name = "Drinking")]
        public string Drinking { get; set; }

        [Display(Name = "Drinking DealBreaker")]
        public bool DrinkingDealBreaker { get; set; }

        [Display(Name = "Music")]
        public string Music { get; set; }

        [Display(Name = "Music DealBreaker")]
        public bool MusicDealBreaker { get; set; }

        [Display(Name = "Diet")]
        public string Diet { get; set; }

        [Display(Name = "Diet DealBreaker")]
        public bool DietDealBreaker { get; set; }

        [Display(Name = "Pets")]
        public string Pets { get; set; }

        [Display(Name = "Pets DealBreaker")]
        public bool PetsDealBreaker { get; set; }
    }
}