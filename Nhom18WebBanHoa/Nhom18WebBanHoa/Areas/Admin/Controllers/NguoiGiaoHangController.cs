using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom18WebBanHoa.Models;

namespace Nhom18WebBanHoa.Areas.Admin.Controllers
{
    public class NguoiGiaoHangController : Controller
    {
        // GET: Admin/NguoiGiaoHang

        QuanLyBanHoaEntities db = new QuanLyBanHoaEntities();
        public ActionResult Index()
        {
            var shipper = db.Nguoi_Giao_Hang.ToList();
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View(shipper);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Nguoi_Giao_Hang ship, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName = fileName + extension;
                ship.hinh = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
            }
            db.Nguoi_Giao_Hang.Add(ship);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]

        public ActionResult Details(int id)
        {
            var ship = db.Nguoi_Giao_Hang.Where(n => n.id == id).FirstOrDefault();
            return View(ship);
        }
        [HttpGet]

        public ActionResult Delete(int id)
        {

            var ship = db.Nguoi_Giao_Hang.Where(n => n.id == id).FirstOrDefault();
            return View(ship);
        }
        [HttpPost]

        public ActionResult Delete(Nguoi_Giao_Hang shipp)
        {
            var ship = db.Nguoi_Giao_Hang.Where(n => n.id == shipp.id).FirstOrDefault();
            db.Nguoi_Giao_Hang.Remove(ship);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]

        public ActionResult Edit(int id)
        {

            var ship = db.Nguoi_Giao_Hang.Where(n => n.id == id).FirstOrDefault();
            return View(ship);
        }
        [HttpPost]

        public ActionResult Edit(int id, Nguoi_Giao_Hang ship, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName = fileName + extension;
                ship.hinh = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
            }

            db.Entry(ship).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
