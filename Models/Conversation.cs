using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Match.Models
{
    public class Conversation : BaseEntity
    {
        public int ConversationId { get; set; }

        public int UserOneId { get; set; }

        public User UserOne { get; set; }

        public int UserTwoId { get; set; }

        public User UserTwo { get; set; }

        public List<Message> Messages { get; set; }

        public Conversation()
        {
            Messages = new List<Message>();
        }
        
    }

}