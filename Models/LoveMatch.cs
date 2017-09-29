using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match.Models

{
        public class LoveMatch : BaseEntity{

            public int Id { get; set; }
            public int User1Id { get; set; }
            
            public int User2Id { get; set; }
            
            public int percentage {get;set;}
        }
        
        
}