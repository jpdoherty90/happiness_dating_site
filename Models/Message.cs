using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Match.Models
{
    public class Message : BaseEntity
    {
        public int MessageId { get; set; }

        public string Content { get; set; }

        public int SenderId { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        public int RecieverId { get; set; }

        [ForeignKey("RecieverId")]
        public User Reciever { get; set; }

        public DateTime SentAt { get; set; }
 
    }

}