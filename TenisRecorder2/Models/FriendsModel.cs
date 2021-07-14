using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class FriendsModel
    {
        public int Id { get; set; }
        public virtual ApplicationUser LeftUser { get; set; }
        public virtual ApplicationUser RigthUser { get; set; }
        public bool Paired { get; set; }
    }
}