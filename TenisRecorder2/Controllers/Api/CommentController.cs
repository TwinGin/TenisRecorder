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
    public class CommentController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CommentController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetComment()
        {
            return Ok();
        }
        [HttpGet]
        [Route("Comment/Get/{id}")]
        public IEnumerable<CommentModel> Comments(int id)
        {
            return _context.Comments.Where(c => c.Match.Id == id).ToList();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddNewComment(NewComment cm)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var id = User.Identity.GetUserId();
            ApplicationUser user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            MatchModel match = _context.Matches.Where(m => m.Id == cm.MatchId).FirstOrDefault();
            CommentModel comment = new CommentModel
            {
                Match = match,
                Author = user,
                Date = DateTime.Now,
                Comment = cm.CommentContent
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            
            return Ok();

        }
    }
}
