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
        public List<string> interests { get; set; }
        public List<int> salary { get; set; }
        public List<int> height { get; set; }
        public List<string> build { get; set; }
        public List<string> ethnicity { get; set; }
        public List<string> history { get; set; }
        public List<string> present_kids { get; set; }
        public List<string> future_kids { get; set; }
        public List<string> drinking { get; set; }
        public List<string> marijuana { get; set; }
        public List<string> Trump { get; set; }
        public int memes { get; set; }
        public List<string> religion { get; set; }
        public List<string> horoscope { get; set; }
        public List<string> exercise { get; set; }
        public List<string> education { get; set; }
        public bool tattoos { get; set; }
        public List<string> diet { get; set; }
        public List<string> sex { get; set; }
        public List<string> cigarettes { get; set; }
        public bool chipotle { get; set; }
        public List<string> pets { get; set; }
        public List<string> netflix {get; set;}
       
        public Preference()
        {
            interests = new List<string>();
            build = new List<string>();
            netflix = new List<string>();
            ethnicity = new List<string>();
            history = new List<string>();
            present_kids = new List<string>();
            future_kids = new List<string>();
            drinking = new List<string>();
            marijuana = new List<string>();
            Trump = new List<string>();
            religion = new List<string>();
            horoscope = new List<string>();
            exercise = new List<string>();
            diet = new List<string>();
            education = new List<string>();
            sex = new List<string>();
            cigarettes = new List<string>();
            pets = new List<string>();
            salary = new List<int>();
            height = new List<int>();
        }
    }
}