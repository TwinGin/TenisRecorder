using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class MatchModel
    {
        public int Id { get; set; }
        [Display(Name = "Ty")]
        public int ScoreLeft { get; set; }
        public virtual ApplicationUser Left { get; set; }
        [Display(Name ="Wybierz przeciwnika")]
        public virtual ApplicationUser Right { get; set; }   
        [Display(Name = "Przeciwnik")]
        public int ScoreRight { get; set; }
    }
}