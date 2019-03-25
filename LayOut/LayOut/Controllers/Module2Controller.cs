using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayOut.Controllers
{
    public class Module2Controller : Controller
    {
        // GET: Module2
        public ActionResult Index()
        {
            return View();
        }
        public String AddProduct()
        {
            return "Thêm sản phẩm vào giỏ hàng"; // http://localhost:37443/module2/AddProduct vào đường dẫn này để chuyển hướng tới AddProduct
        }

        public String DeleteProduct()
        {
            return "Xóa sản phẩm khỏi giỏ hàng";
        }

        public String SearchProduct()
        {
            return "Tìm kiếm sản phẩm";
        }

        public String ViewListProduct()
        {
            return "Xem danh sách sản phẩm";
        }

        public String ViewDetailProduct()
        {
            return "Xem thông tin chi tiết sản phẩm";
        }

        public String UpdateProduct()
        {
            return "cập nhập giỏ hàng";
        }
    }
}