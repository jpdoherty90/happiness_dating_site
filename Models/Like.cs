using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match.Models
{
    public class Like : BaseEntity
    {
        public int LikeId { get; set; }

        public int PersonLikingId { get; set; }

        [ForeignKey("PersonLikingId")]
        public User PersonLiking { get; set; }

        public int PersonLikedId { get; set; }

        [ForeignKey("PersonLikedId")]
        public User PersonLiked { get; set; }

    }
    
}