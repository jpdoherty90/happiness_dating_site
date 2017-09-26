using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
        public class UserViewModel : BaseEntity{
        public int id { get; set; }

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

        [Display(Name = "I am a")]
        public string gender { get; set; }
        public string seeking { get; set; }

        [Display(Name = "Zip / Postal code")]
        public int zipcode { get; set; }
        public List<string> interests { get; set; }
        public List<int> likesId { get; set; }
        public List<User> likes { get; set; }
        public List<int> likersId { get; set; }
        public List<User> likers { get; set; }
        public List<int> messagesId { get; set; }
        public List<Message> messages { get; set; }
        public int salary { get; set; }
        public int height { get; set; }
        public string build { get; set; }
        public string ethnicity { get; set; }
        public string history { get; set; }
        public string present_kids { get; set; }
        public string future_kids { get; set; }
        public string drinking { get; set; }
        public string marijuana { get; set; }
        public string Trump { get; set; }
        public int memes { get; set; }
        public string religion { get; set; }
        public string horoscope { get; set; }
        public string exercise { get; set; }
        public string education { get; set; }
        public bool tattoos { get; set; }
        public string diet { get; set; }
        public string sex { get; set; }
        public string cigarettes { get; set; }
        public bool chipotle { get; set; }
        public string pets { get; set; }
        public string kids { get; set; }
        public List<string> netflix {get; set;}
       
        public UserViewModel(){
            interests = new List<string>();
            netflix = new List<string>();
            messages = new List<Message>();
            messagesId = new List<int>();
            likes = new List<User>();
            likesId = new List<int>();
            likers = new List<User>();
            likersId = new List<int>();
        }
    }
}