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

            int CurrUserId = (int)HttpContext.Session.GetInt32("currentUser");

            List<Message> MsgsSent = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(msg => msg.SenderId == (int)CurrUserId).ToList();

            List<Message> MsgsRecieved = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(msg => msg.RecieverId == (int)CurrUserId).ToList();

            List<Message> CombinedMsgs = MsgsSent.Union(MsgsRecieved).OrderBy(m => m.SentAt).ToList();

            List<int> Recievers = new List<int>();
            List<Message> Convos = new List<Message>();

            // foreach (var msg in CombinedMsgs) {
            //     if(!Recievers.Contains(msg.SenderId) && msg.SenderId != CurrUserId) {
            //         Recievers.Add(msg.SenderId);
            //         Convos.Add(msg);
            //     }
            //     if(!Recievers.Contains(msg.RecieverId) && msg.RecieverId != CurrUserId) {
            //         Recievers.Add(msg.RecieverId);
            //         Convos.Add(msg);
            //     }
            // }
            

            for (var i = CombinedMsgs.Count() - 1; i > -1; i--) {
                if(!Recievers.Contains(CombinedMsgs[i].SenderId) && CombinedMsgs[i].SenderId != CurrUserId) {
                    Recievers.Add(CombinedMsgs[i].SenderId);
                    Convos.Add(CombinedMsgs[i]);
                }
                if(!Recievers.Contains(CombinedMsgs[i].RecieverId) && CombinedMsgs[i].RecieverId != CurrUserId) {
                    Recievers.Add(CombinedMsgs[i].RecieverId);
                    Convos.Add(CombinedMsgs[i]);
                }
            }

            List<User> MessageFriends = new List<User>();

            for (var i = 0; i < Recievers.Count; i++) {
                MessageFriends.Add(_context.Users.SingleOrDefault(u => u.UserId == Recievers[i]));
            }

            ViewBag.Friends = MessageFriends;
            ViewBag.AllConvos = Convos;

            return View("All");

        }

        [HttpGet]
        [Route("conversation/{friendId}")]
        public IActionResult Conversation(int friendId) {

            int CurrUserId = (int)HttpContext.Session.GetInt32("currentUser");

            List<Message> MsgsSent = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(msg => msg.SenderId == (int)CurrUserId).Where(msg => msg.RecieverId == friendId).ToList();

            List<Message> MsgsRecieved = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(msg => msg.RecieverId == (int)CurrUserId).Where(msg => msg.SenderId == friendId).ToList();


            if (MsgsSent.Count > 0 && MsgsRecieved.Count > 0) {
                var CombinedMsgs = MsgsSent.Union(MsgsRecieved).OrderByDescending(m => m.SentAt);
                ViewBag.MsgsInConvo = CombinedMsgs;    
            } else if (MsgsSent.Count > 0) {
                ViewBag.MsgsInConvo = MsgsSent;
            } else if (MsgsRecieved.Count > 0) {
                ViewBag.MsgsInConvo = MsgsRecieved;
            }

            ViewBag.Curr = CurrUserId;
            ViewBag.FriendId = friendId;
            ViewBag.Friend = _context.Users.SingleOrDefault(u => u.UserId == friendId);

            return View("Conversation");
        }

        [HttpPost]
        [Route("write_message/{friendId}")]
        public IActionResult Write(int friendId, string textContent) {

            int CurrUserId = (int)HttpContext.Session.GetInt32("currentUser");

            Message newMsg = new Message{
                Content = textContent,
                SenderId = CurrUserId,
                RecieverId = friendId,
                SentAt = DateTime.Now
            };

            _context.Messages.Add(newMsg);
            _context.SaveChanges();

            return Redirect($"/conversation/{friendId}");
        }

    }
}