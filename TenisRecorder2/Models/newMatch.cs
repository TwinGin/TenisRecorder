using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class NewMatch
    {
        public string Id { get; set; }
        public int ScoreLeft { get; set; }
        public int ScoreRight { get; set; }
    }
}