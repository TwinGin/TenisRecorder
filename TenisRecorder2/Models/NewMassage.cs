using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class NewMassage
    {
        public int ConvId { get; set; }
        public string WhoSent { get; set; }
        public string Text { get; set; }
    }
}