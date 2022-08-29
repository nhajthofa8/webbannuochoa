using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom18WebBanHoa.Models;

namespace Nhom18WebBanHoa.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        QuanLyBanHoaEntities db = new QuanLyBanHoaEntities();
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string taikhoan, string matkhau)
        {

            if (ModelState.IsValid)
            {

                var data = db.Nguoi_Dung.Where(n => n.taikhoan.Equals(taikhoan) && n.matkhau.Equals(matkhau));
                if (data.Count() > 0)
                {
                    Session["ten"] = data.FirstOrDefault().ten;
                    Session["taikhoan"] = data.FirstOrDefault().taikhoan;
                    Session["email"] = data.FirstOrDefault().email;
                    Session["SDT"] = data.FirstOrDefault().SDT;
                    Session["diachi"] = data.FirstOrDefault().diachi;
                    Session["quyen"] = data.FirstOrDefault().quyen;
                    Session["id"] = data.FirstOrDefault().id;
                    if (Session["quyen"] == null)
                    {
                        return Redirect("~");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    ViewBag.error = "Đăng nhập thất bại";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
