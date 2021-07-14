using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TenisRecorder2.Models;

namespace TenisRecorder2.Controllers.Api
{
    [Authorize]
    public class FriendsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public FriendsController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("Friends/Opponents")]
        public IEnumerable<ApplicationUser> GetOpponents()
        {
            var userId = User.Identity.GetUserId();
            var currentFriends = _context.Friends.Where(f => f.LeftUser.Id == userId || f.RigthUser.Id == userId).ToList();
            var allUsers = _context.Users.Where(u => u.Id != userId).ToList();
            List<ApplicationUser> returnUsers = new List<ApplicationUser>(allUsers);
            foreach (ApplicationUser user in allUsers)
            {
                foreach (FriendsModel friends in currentFriends)
                {
                    if (friends.LeftUser.Id == user.Id || friends.RigthUser.Id == user.Id)
                    {
                        returnUsers.Remove(user);
                        continue;
                    }

                }
            }

            return returnUsers;
        }
        [Route("Friends/SendMessage")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateNewMessage(NewMassage newMassage)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var friends = _context.Friends.Where(f => f.Id == newMassage.ConvId).FirstOrDefault();
            MessageModel newMessageModel = new MessageModel
            {
                Conversation = friends,
                Message = newMassage.Text,
                WhoSent = newMassage.WhoSent
            };
            _context.Messages.Add(newMessageModel);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("Friends/GetAllMessages/{id}")]
        [HttpGet]
        public object GetAllMessages(int id)
        {

            return _context.Messages.Where(m => m.Conversation.Id == id).ToList();
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateFriend(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var userId = User.Identity.GetUserId();
            var userLeft = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            var userRight = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            FriendsModel friends = new FriendsModel
            {
                LeftUser = userLeft,
                RigthUser = userRight,
                Paired = false
            };
            _context.Friends.Add(friends);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("Friends/Pending")]
        public object GetPending()
        {
            var userId = User.Identity.GetUserId();
            var pendingFriends = _context.Friends.Where(f => f.RigthUser.Id == userId && f.Paired == false).ToList();
            List<object> jsonList = new List<object>();
            foreach (var item in pendingFriends)
            {

                object el = new
                {
                    id = item.Id,
                    name = item.LeftUser.Name + " " + item.LeftUser.Surname,
                    email = item.LeftUser.Email
                };
                jsonList.Add(el);

            }
            return jsonList;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var friendInDb = _context.Friends.SingleOrDefault(f => f.Id == id);
            if (friendInDb == null)
                return NotFound();
            _context.Friends.Remove(friendInDb);
            _context.SaveChanges();
            return Ok();
        }
        [Route("Friends/ConfirmFriendship/{id}")]
        [HttpPut]
        public IHttpActionResult ConfirmFriendship(int id)
        {
            var friendInDb = _context.Friends.SingleOrDefault(f => f.Id == id);
            if (friendInDb == null)
                return NotFound();
            friendInDb.Paired = true;
            _context.SaveChanges();
            return Ok();
        }
        [Route("Friends/Paried")]
        [HttpGet]
        public object GetPairedFriends()
        {
            var userId = User.Identity.GetUserId();
            var pairedFriends = _context.Friends.Where(f => (f.RigthUser.Id == userId || f.LeftUser.Id == userId) && f.Paired == true).ToList();
            List<object> jsonList = new List<object>();
            foreach (var item in pairedFriends)
            {
                if (item.RigthUser.Id == userId)
                {
                    object el = new
                    {
                        id = item.Id,
                        name = item.LeftUser.Name + " " + item.LeftUser.Surname + "(" + item.LeftUser.Email + ")",
                        email = item.LeftUser.Email
                    };
                    jsonList.Add(el);
                }
                else
                {
                    object el = new
                    {
                        id = item.Id,
                        name = item.RigthUser.Name + " " + item.RigthUser.Surname + "(" + item.RigthUser.Email+")",
                        email = item.RigthUser.Email
                    };
                    jsonList.Add(el);
                }
            }
            return jsonList;
        }

        [Route("Friends/Notparied")]
        [HttpGet]
        public object GetNotPairedFriends()
        {
            var userId = User.Identity.GetUserId();
            var pairedFriends = _context.Friends.Where(f => (f.RigthUser.Id == userId || f.LeftUser.Id == userId) && f.Paired == true).ToList();
            var users = _context.Users.Where(f => f.Id != userId).ToList();
            var users2 = _context.Users.Where(f => f.Id != userId).ToList(); ;
            foreach (var user in users)
            {
                if (pairedFriends.Any(p => p.LeftUser == user || p.RigthUser == user))
                    users2.Remove(user);
            }
            List<object> jsonList = new List<object>();
            foreach (var item in users2)
            {
                object el = new
                {
                    id = item.Id,
                    name = item.Name + " " + item.Surname + "(" + item.Email + ")",
                    email = item.Email
                };
                jsonList.Add(el);
            }
            return jsonList;
        }
    }
}
