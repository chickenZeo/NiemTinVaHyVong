using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCar.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        [ChildActionOnly]
        public ActionResult SanPhamPartial()
        {
            return PartialView();
        }
    }
}