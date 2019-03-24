using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayOut.Controllers
{
    public class Module1Controller : Controller
    {
        // GET: Module1
        public ActionResult Index()
        {
            return View();
        }
        public String  AddProduct()
        {
            return "Thêm sản phẩm"; // http://localhost:37443/module1/AddProduct vào đường dẫn này để chuyển hướng tới AddProduct
        }

        public String DeleteProduct()
        {
            return "Xóa sản phẩm";
        }

        public String SearchProduct()
        {
            return "Tìm kiếm sản phẩm";
        }

        public String ViewListProduct()
        {
            return "Xem danh sách sản phẩm";
        }
        
    }
}