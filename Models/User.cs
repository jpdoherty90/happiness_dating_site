using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Image picture { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string seeking { get; set; }
        public List<string> interests { get; set; }
        public List<int> conversationsId{ get; set; }
        public List<Conversation> conversations { get; set; }
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
        public int zipcode { get; set; }
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
       
        public User()
        {
            interests = new List<string>();
            netflix = new List<string>();
            messages = new List<Message>();
            messagesId = new List<int>();
            conversations = new List<Conversation>();
            conversationsId = new List<Conversation>();
            likes = new List<User>();
            likesId = new List<int>();
            likers = new List<User>();
            likersId = new List<int>();
        }
    }
}