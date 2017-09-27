using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class LoginViewModel : BaseEntity
    { 
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }
 
        [Required]
        [MinLength(2)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}