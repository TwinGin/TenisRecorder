using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class HistoryAndMatchModelView
    {
        public MatchModel NewMatch { get; set; }
        public string SelectedUserId { get; set; }
        public List<ApplicationUser> UserList { get; set; }
        public List<MatchModel> MatchHistory { get; set; }
    }
}