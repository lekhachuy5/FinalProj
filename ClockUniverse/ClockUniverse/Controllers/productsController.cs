using System.Linq;
using System.Web.Mvc;

namespace ClockUniverse.Controllers
{
    public class productsController : Controller
    {
        //
        CsK23T3bEntities db = new CsK23T3bEntities();
        // GET: /products/
        public ActionResult Index()
        {
            return View(db.ProductTables.ToList());
        }
        public ViewResult product_detail(int watch_ID = 0)
        {
            // Tra về đôi tượng với điều kiện
            ProductTable product = db.ProductTables.SingleOrDefault(n => n.Watch_ID == watch_ID);
            if (product == null)
            {
                // Trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        public ActionResult ListItem(int type)
        {
            var itemlist = db.ProductTables.Where(x => x.WatchType_ID == type).Select(x => new { x.Watch_Name, x.Watch_Description, x.Selling_Price }).ToList();
            ViewBag.itemList = itemlist;
            ViewBag.type = type;
            return View();
        }
    }
}