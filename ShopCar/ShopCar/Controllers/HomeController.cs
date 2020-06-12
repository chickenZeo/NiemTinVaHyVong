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
        
        [ChildActionOnly]
        public ActionResult MenuPartial()
        {
            //  var lstLoaiSP = db.LoaiSPs;
            // return View(lstLoaiSP);
            return View(db.LoaiSPs.ToList());
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string txtTenDangNhap,string txtMatKhau)
        {
            KhachHang tv1 = db.KhachHangs.SingleOrDefault(x => x.UserName == txtTenDangNhap && x.Pass == txtMatKhau);
            if (tv1 != null)
            {
                Session["TaiKhoan"] = tv1;
                return Content("<script>window.location.reload();</script>");
            }
            return Content("<script>alert('Sai tài khoản hoặc mật khẩu')</script>");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
        
    }
}