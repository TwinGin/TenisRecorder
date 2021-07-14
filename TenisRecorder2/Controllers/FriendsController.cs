using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TenisRecorder2.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        // GET: Friends
        public ActionResult Index()
        {
            return View();
        }
    }
}