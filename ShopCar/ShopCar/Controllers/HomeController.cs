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
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.lstChanNang = db.SanPhams.Where(x => x.MaLoaiSP == "L0003");
            ViewBag.lstPhuKienKhac = db.SanPhams.Where(x => x.MaLoaiSP == "L0004");
            ViewBag.lstDauNhot = db.SanPhams.Where(x => x.MaLoaiSP == "L0007");
            ViewBag.lstSon = db.SanPhams.Where(x => x.MaLoaiSP == "L0008");

            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string TenTK = f["txtTenDangNhap"].ToString();
            string MatKhau = f["txtMatKhau"].ToString();

            KhachHang tv1 = db.KhachHangs.SingleOrDefault(x => x.UserName == TenTK && x.Pass == MatKhau);
            if (tv1 != null)
            {
                Session["TaiKhoan"] = tv1;
                return Content("<script>window.location.reload();</script>");
            }
            return Content("<script>alert('Sai tài khoản hoặc mật khẩu')</script>");
        }
    }
}