using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class Like : BaseEntity
    {
        public int LikeId { get; set; }

        public int LikerId { get; set; }

        public User Liker { get; set; }

        public int LikeeId { get; set; }

        public User Likee { get; set; }

    }
    
}