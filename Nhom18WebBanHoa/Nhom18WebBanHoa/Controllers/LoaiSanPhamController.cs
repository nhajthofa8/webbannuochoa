using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom18WebBanHoa.Models;

namespace Nhom18WebBanHoa.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: LoaiSanPham
        QuanLyBanHoaEntities db = new QuanLyBanHoaEntities();
        public ActionResult _LoaiSanPham()
        {
            return PartialView(db.Loai_San_Pham.ToList());
        }
        public ActionResult _LoaiSanPhamDuoi()
        {
            return PartialView(db.Loai_San_Pham.ToList());
        }
        public ViewResult SanPhamTheoLoai(int id, int page = 1, int Pagesize = 4)
        {

            List<San_Pham> sanpham = db.San_Pham.Where(n => n.idloaiSP == id).ToList();

            return View(sanpham.ToList().ToPagedList(page, Pagesize));
        }
        public ViewResult SanPhamTheoLoaiDuoi(int id, int page = 1, int Pagesize = 4)
        {

            List<San_Pham> sanpham = db.San_Pham.Where(n => n.idloaiSP == id).ToList();

            return View(sanpham.ToList().ToPagedList(page, Pagesize));
        }
    }
}