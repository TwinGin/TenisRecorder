using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class MatchCommentsViewModel
    {
        public int MatchId { get; set; }
        public MatchModel Match{ get; set; }
        public List<CommentModel> Comments { get; set; }
        public CommentModel NewComment { get; set; }
    }
}