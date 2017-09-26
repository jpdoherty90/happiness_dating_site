using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class Match : BaseEntity
    {
        public int MatchId { get; set; }

        public int UserOneId { get; set; }

        public User UserOne { get; set; }

        public int UserTwoId { get; set; }

        public User UserTwo { get; set; }

        public int Percentage { get; set; }
        
    }

}