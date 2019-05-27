using System.Linq;
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
        public ActionResult Search(string text)
        {
            var itemsz = db.ProductTables.Where(x => x.Watch_Name.ToLower().Contains(text.ToLower())).ToList();
            if (text.Trim().Equals(""))
            {
                return RedirectToAction("Index");
            }

            else if(itemsz.Count() > 0)
            {
                //ViewBag.Message = "";
            }
            else
            {
                ViewBag.Message = "Không tìm thấy được sản phẩm tương ứng";

            }
            ViewData["Item"] = itemsz;
            return View(itemsz);
        }
    }
}