using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match.Models
{
        public class User : BaseEntity{
        public int UserId { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string seeking { get; set; }
        public byte[] profile_picture {get; set;}

        public int PreferenceId { get; set; }
        public Preference Preference {get; set;}

        [InverseProperty("PersonLiking")]
        public List<Like> likes { get; set; }

        [InverseProperty("PersonLiked")]
        public List<Like> likers { get; set; }
        public List<User> Matches { get; set; }
        public List<Percentage> MatchPercentages { get; set; }

        [InverseProperty("Sender")]
        public List<Message> messagesSent { get; set; }
        
        [InverseProperty("Reciever")]
        public List<Message> messagesRecieved { get; set; }
        public int salary { get; set; }
        public int height { get; set; }
        public string build { get; set; }
        public string ethnicity { get; set; }
        public int zipcode { get; set; }
        public bool Divorced { get; set; }
        public bool Widowed { get; set; }
        public string kids { get; set; }
        public string drinking { get; set; }
        public string marijuana { get; set; }
        public string religion { get; set; }
        public string diet { get; set; }
        public string pets { get; set; }
       
        public User()
        {
            messagesRecieved = new List<Message>();
            messagesSent = new List<Message>();
            likes = new List<Like>();
            likers = new List<Like>();
            Matches = new List<User>();
            MatchPercentages = new List<Percentage>();
            Preference = new Preference();
            salary = 0;
            height = 0;
            build = "";
            ethnicity = "";
            zipcode = 0;
            Divorced = false;
            Widowed = false;
            drinking = "";
            marijuana = "";
            religion = "";
            diet = "";
            pets = "";

        }
    }
}