using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class Like : BaseEntity
    {
        public int LikeId { get; set; }

        public int PersonLikingId { get; set; }

        public User PersonLiking { get; set; }

        public int PersonLikedId { get; set; }

        public User PersonLiked { get; set; }

    }
    
}