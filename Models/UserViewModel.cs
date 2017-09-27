using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class UserViewModel : BaseEntity
    {
        [Display(Name = "I am a")]
        public string gender { get; set; }

        [Display(Name = "Zip / Postal code")]
        public int zipcode { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Username")]
        public string username { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Age")]
        public int age { get; set; }
    }
}