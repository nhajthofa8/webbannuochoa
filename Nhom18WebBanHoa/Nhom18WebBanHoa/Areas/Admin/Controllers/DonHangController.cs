using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom18WebBanHoa.Models;

namespace Nhom18WebBanHoa.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        QuanLyBanHoaEntities db = new QuanLyBanHoaEntities();
        public ActionResult Index()
        {
            var donhang = db.Don_Hang.ToList();
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View(donhang);
            }

        }
        [HttpGet]



        public ActionResult Details(int id)
        {
            List<Chi_Tiet_Don_Hang> chitiet = db.Chi_Tiet_Don_Hang.Where(n => n.idDonHang == id).ToList();

            return View(chitiet.ToList());
        }
        [HttpGet]





        public ActionResult Edit(int id)
        {
            var don = db.Don_Hang.Where(n => n.id == id).FirstOrDefault();
            ViewBag.idNgDung = new SelectList(db.Nguoi_Dung, "id", "ten", don.idNgDung);
            ViewBag.idNgGiaoHang = new SelectList(db.Nguoi_Giao_Hang, "id", "ten", don.idNgGiaoHang);
            ViewBag.idtrangthai = new SelectList(db.Trang_Thai, "id", "trangthai", don.idtrangthai);


            return View(don);
        }
        [HttpPost]

        public ActionResult Edit(int id, Don_Hang don)
        {
            db.Entry(don).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}