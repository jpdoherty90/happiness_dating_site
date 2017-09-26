using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class Message : BaseEntity
    {
        public int MessageId { get; set; }

        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; }

        public int SenderId { get; set; }

        public User Sender { get; set; }

        public int RevieverId { get; set; }

        public User Reciever { get; set; }

        public DateTime SentAt { get; set; }
 
    }

}