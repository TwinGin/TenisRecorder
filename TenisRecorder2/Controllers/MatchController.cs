using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenisRecorder2.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace TenisRecorder2.Controllers
{
    [Authorize]
    public class MatchController : Controller
    {

        private ApplicationDbContext _context;
        public MatchController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Match
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var userLoggedIn = _context.Users.Where(k => k.Id == id).FirstOrDefault();
            var loggedUserMatchHistory = _context.Matches.Where(lu => lu.Left.Email == userLoggedIn.Email || lu.Right.Email == userLoggedIn.Email).ToList();
            var model = new HistoryAndMatchModelView
            {
                UserList = _context.Users.Where(d => d.Id != id).ToList(),
                MatchHistory = loggedUserMatchHistory
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> submitNewMatch(HistoryAndMatchModelView model)
        {
            if (!ModelState.IsValid || model.SelectedUserId == "" || model.SelectedUserId == "Przeciwnik" || model.SelectedUserId == null || model.NewMatch.ScoreLeft < 0 || model.NewMatch.ScoreRight < 0)
            {
                TempData["noValidData"] = "yes";
                return  RedirectToAction("Index");
            }
            string leftUserData = model.SelectedUserId.Split(' ')[2];
            var id = User.Identity.GetUserId();
            var loggedUser = _context.Users.Where(u => u.Id == id).Cast<ApplicationUser>().FirstOrDefault();
            var leftUser = _context.Users.Where(l => l.Email == leftUserData).Cast<ApplicationUser>().FirstOrDefault();
            var newMatch = new MatchModel()
            {
                Left = loggedUser,
                Right = leftUser,
                ScoreLeft = model.NewMatch.ScoreLeft,
                ScoreRight = model.NewMatch.ScoreRight
            };
            _context.Matches.Add(newMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult allmatches()
        {
            return View("allmatches");
        }
    }
}