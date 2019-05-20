using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClockUniverse.Controllers;
using System.Web.Mvc;


namespace ClockUniverse.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            
            return View();
        }
	}
}