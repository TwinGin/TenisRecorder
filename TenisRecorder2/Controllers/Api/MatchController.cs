using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TenisRecorder2.Models;

namespace TenisRecorder2.Controllers.Api
{
    [Authorize]
    public class MatchController : ApiController
    {
        //GET /api/match
        private readonly ApplicationDbContext _context;
        public MatchController()
        {
            _context = new ApplicationDbContext();
        }
        public object GetMatches()
        {
            var userId = User.Identity.GetUserId();
            return _context.Matches.Where(m => m.Left.Id == userId || m.Right.Id == userId).ToList().OrderBy(m=>m.Id).Reverse();
        }

        //Get /api/match/1
        public IHttpActionResult GetMatch(int id)
        {
            var match = _context.Matches.SingleOrDefault(m => m.Id == id);
            if (match == null)
              return NotFound();
            return Ok(match);
        }

        //GET /api/match/opponents
        [Route("Match/Opponents")]
        public IEnumerable<ApplicationUser> GetOpponents()
        {
            var userId = User.Identity.GetUserId();
            return _context.Users.Where(u => u.Id != userId).ToList(); 
        }

        //POST /api/match
        [HttpPost]
        public IHttpActionResult CreateMatch(NewMatch newMatch)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            ApplicationUser userRight = _context.Users.Where(u => u.Email == newMatch.Id).FirstOrDefault();
            var userId = User.Identity.GetUserId();
            ApplicationUser userLeft = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            MatchModel match = new MatchModel
            {
                Left = userLeft,
                Right = userRight,
                ScoreLeft = newMatch.ScoreLeft,
                ScoreRight = newMatch.ScoreRight
            };
            _context.Matches.Add(match);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + match.Id), match); 
        }

        //DELETE /api/match/1
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var matchInDb = _context.Matches.SingleOrDefault(m => m.Id == id);
            if (matchInDb == null)
                return NotFound();
            var commentsForMatch = _context.Comments.Where(c => c.Match.Id == matchInDb.Id).ToList();
            foreach (var item in commentsForMatch)
            {
                _context.Comments.Remove(item);
            }
            _context.Matches.Remove(matchInDb);
            _context.SaveChanges();
            return Ok();
        }

        [Route("Matches/All")]
        [HttpGet]
        public object GetAllMatches()
        {
            var userId = User.Identity.GetUserId();
            return _context.Matches.ToList().OrderBy(c=>c.Id).Reverse();
        }
    }
}
