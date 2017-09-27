using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match.Models
{
    public class Percentage : BaseEntity
    {
        public int PercentageId { get; set; }

        public int Percent { get; set; }


    }
    
}