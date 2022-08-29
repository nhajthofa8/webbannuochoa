using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom18WebBanHoa.Models;

namespace Nhom18WebBanHoa.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanHoaEntities db = new QuanLyBanHoaEntities();
        [HttpPost]
        public ActionResult Search(FormCollection f, int page = 1, int Pagesize = 8)
        {
            string tukhoa = f["txtTimkiem"].ToString();
            ViewBag.TuKhoa = tukhoa;
            List<San_Pham> lsttimkiem = db.San_Pham.Where(n => n.tenSP.Contains(tukhoa)).ToList();
            if (lsttimkiem.Count == 0)
            {
                ViewBag.ThongBaoo = "Không tìm thấy sản phẩm nào";
                return View(db.San_Pham.OrderBy(n => n.tenSP).ToPagedList(page, Pagesize));
            }
            ViewBag.ThongBaoo = "Đã Tìm Thấy " + lsttimkiem.Count + " Kết quả";
            return View(lsttimkiem.OrderBy(n => n.tenSP).ToPagedList(page, Pagesize));
        }
        [HttpGet]
        public ActionResult Search(String tukhoa, int page = 1, int Pagesize = 8)
        {
            ViewBag.TuKhoa = tukhoa;
            List<San_Pham> lsttimkiem = db.San_Pham.Where(n => n.tenSP.Contains(tukhoa)).ToList();
            if (lsttimkiem.Count == 0)
            {
                ViewBag.ThongBaoo = "Không tìm thấy sản phẩm nào";
                return View(db.San_Pham.OrderBy(n => n.tenSP).ToPagedList(page, Pagesize));
            }
            ViewBag.ThongBaoo = "Đã Tìm Thấy " + lsttimkiem.Count + " Kết quả";
            return View(lsttimkiem.OrderBy(n => n.tenSP).ToPagedList(page, Pagesize));
        }
    }
}