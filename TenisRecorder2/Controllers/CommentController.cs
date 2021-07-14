using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TenisRecorder2.Models;

namespace TenisRecorder2.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _contex;
        public CommentController()
        {
            _contex = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }
        // GET: Comment
        public ActionResult Index(int matchId)
        {
            var match = _contex.Matches.Where(m => m.Id == matchId).FirstOrDefault();
            var commments = _contex.Comments.Where(c => c.Match.Id == matchId).ToList();
            var model = new MatchCommentsViewModel()
            {
                MatchId = matchId,
                Match = match,
                Comments = commments
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddNewComment(MatchCommentsViewModel model,string matchId)
        {
            var id = User.Identity.GetUserId();
            var match_id = Int32.Parse(matchId);
            var user = _contex.Users.Where(u => u.Id == id).FirstOrDefault();
            var match = _contex.Matches.Where(m => m.Id == match_id).FirstOrDefault();
            CommentModel comm = new CommentModel()
            {
                Match = match,
                Date = DateTime.Now,
                Comment = model.NewComment.Comment,
                Author = user
            };
            _contex.Comments.Add(comm);
            await _contex.SaveChangesAsync();
            return RedirectToAction("Index", new { matchId = match_id });
        }
    }
}