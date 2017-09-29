using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Match.Models;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Match.Controllers
{
    public class MessageController : Controller
    {

        private Context _context;

        public MessageController(Context context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("all")]
        public IActionResult All() {

            Message FakeMsg1 = new Message
            {
                Content = "What's you're number?",
                SenderId = 1, 
                RecieverId = 2,
                SentAt = DateTime.Now
            };

            Message FakeMsg2 = new Message
            {
                Content = "Why don't I just come over?",
                SenderId = 2, 
                RecieverId = 1,
                SentAt = DateTime.Now
            };

            _context.Messages.Add(FakeMsg1);
            _context.Messages.Add(FakeMsg2);
            _context.SaveChanges();

            //int CurrUserId = (int)HttpContext.Session.GetInt32("currentUser");

            //var MsgsSent = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(msg => msg.SenderId == (int)CurrUserId);
            List<Message> MsgsSent = _context.Messages.Include(m => m.Reciever).OrderBy(m => m.SentAt).Where(msg => msg.SenderId == 1).ToList();
            Console.WriteLine("MESSAGES SENT:");
            Console.WriteLine(MsgsSent);

            //var MsgsRecievd = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(msg => msg.RecieverId == (int)CurrUserId);
            List<Message> MsgsRecieved = _context.Messages.Include(m => m.Sender).OrderBy(m => m.SentAt).Where(msg => msg.RecieverId == 1).ToList();
            Console.WriteLine("MESSAGES RECIEVED:");
            Console.WriteLine(MsgsRecieved);

            var CombinedMsgs = MsgsSent.Union(MsgsRecieved).OrderBy(m => m.SentAt);


            // foreach (var msg in MsgsSent) {
            //     Console.WriteLine("NEXT MESSAGE:");
            //     Console.WriteLine(msg.Content);
            // }

            // foreach (var msg in MsgsRecieved) {
            //     Console.WriteLine("NEXT MESSAGE:");
            //     Console.WriteLine(msg.Content);
            // }

            List<int> Recievers = new List<int>();

            foreach (var msg in CombinedMsgs) {
                if(!Recievers.Contains(msg.SenderId)) {
                    Recievers.Add(msg.SenderId);
                }
                if(!Recievers.Contains(msg.RecieverId)) {
                    Recievers.Add(msg.RecieverId);
                }
            }
            //Recievers.RemoveAll(item => item == CurrUserId);
            
            Console.WriteLine(Recievers);

            List<User> MessageFriends = new List<User>();

            for (var i = 0; i < Recievers.Count; i++) {
                MessageFriends.Add(_context.Users.SingleOrDefault(u => u.UserId == Recievers[i]));
            }

            ViewBag.Friends = MessageFriends;

            return View("All");


        }
    }
}