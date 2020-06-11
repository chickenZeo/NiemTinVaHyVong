using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopCar.Model;

namespace ShopCar.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        CarShopEntities db = new CarShopEntities();
        // GET: QuanLySanPham
        public ActionResult Index()
        {
            var lsp = db.SanPhams;

            return View(lsp);
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai");
            return View();
        }
        SanPham ktraHinhAnhDauVao(SanPham sp,HttpPostedFileBase URLAnh)
        {
            bool error = false;
                if(URLAnh != null)
                {
                    if (URLAnh.ContentLength > 0)
                    {
                        if (URLAnh.ContentType != "image/jpeg" && URLAnh.ContentType != "image/png" 
                        && URLAnh.ContentType != "image/gif" && URLAnh.ContentType != "image/jpg")
                        {
                            ViewBag.UpLoad1 = "Hình ảnh không hợp lệ!";
                            error = true;
                        }
                        else
                        {
                            var fileName = Path.GetFileName(URLAnh.FileName);
                            var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.Upload = "Hình ảnh đã tồn tại!";
                                error = true;
                            }
                        }
                    }
                }   
            if (error) return null;
            ThemHinhAnhVaoFile(URLAnh);
            sp.URLAnh = URLAnh.FileName;
            return sp;
        }
        bool KtraLoiHinhAnh(HttpPostedFileBase URLAnh)
        {
            bool error = false;
            if (URLAnh != null)
            {
                if (URLAnh.ContentLength > 0)
                {
                    if (URLAnh.ContentType != "image/jpeg" && URLAnh.ContentType != "image/png" 
                        && URLAnh.ContentType != "image/gif" && URLAnh.ContentType != "image/jpg")
                    {
                        ViewBag.UpLoad1 = "Hình ảnh không hợp lệ!";
                        error = true;
                    }
                    else
                    {
                        var fileName = Path.GetFileName(URLAnh.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Upload = "Hình ảnh đã tồn tại!";
                            error = true;
                        }
                    }
                }
            }
            return error;
        }
        void ThemHinhAnhVaoFile(HttpPostedFileBase URLAnh)
        {
           
                var fileName = Path.GetFileName(URLAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                URLAnh.SaveAs(path);
            
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(SanPham sp, HttpPostedFileBase URLAnh)
        {
           
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai");
            sp = ktraHinhAnhDauVao(sp, URLAnh);
            if(sp==null)
            {
                return View(sp);
            }
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(string MaSP)
        {
            if (MaSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                return HttpNotFound();
            }
            
            //load dropdownlist
           
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SanPham sp, HttpPostedFileBase URLAnh)
        {

            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);

            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            sp = ktraHinhAnhDauVao(sp, URLAnh);
            if (sp == null) return View(sp);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //load dropdownlist
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            return View(sp);
        }
        [HttpPost]
        public ActionResult XoaTC(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if(sp==null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}