using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenisRecorder2.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public FriendsModel Conversation { get; set; }
        public string WhoSent { get; set; }
        public string Message { get; set; }
    }
}