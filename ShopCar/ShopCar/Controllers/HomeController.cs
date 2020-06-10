using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    public class HomeController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        public ActionResult Index()
        {
            ViewBag.lstChanNang = db.SanPhams.Where(x => x.MaLoaiSP == "L0003");

            ViewBag.lstPhuKienKhac = db.SanPhams.Where(x => x.MaLoaiSP == "L0004");
            ViewBag.lstDongCo = db.SanPhams.Where(x => x.MaLoaiSP == "L0005");
            ViewBag.lstPhanh = db.SanPhams.Where(x => x.MaLoaiSP == "L0006");
            ViewBag.lstDauNhot= db.SanPhams.Where(x => x.MaLoaiSP == "L0007");
            ViewBag.lstSon = db.SanPhams.Where(x => x.MaLoaiSP == "L0008");

            return View();
        }

    }
}