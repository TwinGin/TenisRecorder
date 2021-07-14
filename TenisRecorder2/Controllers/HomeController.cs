using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TenisRecorder2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Strona opisu aplikacji.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Strona kontaktowa.";

            return View();
        }
    }
}