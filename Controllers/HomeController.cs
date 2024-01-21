using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = ".";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Dobrodošli na stranicu za kontakt Turističke agencije TravelWithUs!";

            return View();
        }
    }
}