using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClockUniverse.Controllers
{
    public class HomeController : Controller
    {
        private CsK23T3bEntities db = new CsK23T3bEntities();
        public ActionResult Index()
        {
            return View(db.ProductTables.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}