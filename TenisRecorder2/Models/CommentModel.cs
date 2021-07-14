using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public virtual MatchModel Match { get; set; }
        [Display(Name = "Treść komentarza")]
        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}