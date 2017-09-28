using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class InterestViewModel : BaseEntity
    { 
        [EmailAddress]
        [Display(Name = "Feet")]
        public int feet { get; set; }

        [Display(Name = "Inch")]
        public int inch { get; set; }
 
        [Display(Name = "Salary")]
        public int salary { get; set; }

        [Display(Name = "Weed")]
        public string weed { get; set; }

        [Display(Name = "Body")]
        public string body { get; set; }

        [Display(Name = "Pets")]
        public string pets { get; set; }

        [Display(Name = "Diet")]
        public string diet { get; set; }

        [Display(Name = "Kids")]
        public string kids { get; set; }

        [Display(Name = "Divorced")]
        public string divorced { get; set; }

        [Display(Name = "Widowed")]
        public string widowed { get; set; }

        [Display(Name = "Ethnicity")]
        public string ethnicity { get; set; }

        [Display(Name = "Drinking")]
        public string drinking { get; set; }

        [Display(Name = "Religion")]
        public string religion { get; set; }
    }
}