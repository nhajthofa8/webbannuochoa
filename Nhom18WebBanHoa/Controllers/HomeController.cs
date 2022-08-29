using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Nhom18WebBanHoa.Models;

namespace Nhom18WebBanHoa.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanHoaEntities db = new QuanLyBanHoaEntities();
        public ActionResult Index(int page = 1, int Pagesize = 8)
        {
            return View(db.San_Pham.ToList().ToPagedList(page, Pagesize));
        }

        public ActionResult Details(int id)
        {
            San_Pham sanpham = db.San_Pham.SingleOrDefault(n => n.id == id);
            return View(sanpham);
        }
        [HttpGet]
        // Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Nguoi_Dung user)
        {
            if (ModelState.IsValid)
            {
                var checkmail = db.Nguoi_Dung.FirstOrDefault(n => n.email == user.email);
                var checktaikhoan = db.Nguoi_Dung.FirstOrDefault(n => n.taikhoan == user.taikhoan);
                if (checkmail == null && checktaikhoan == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Nguoi_Dung.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("DangKyThanhCong");
                }
                else if (checktaikhoan != null)
                {
                    ViewBag.Error = " Tài khoản đã tồn tại";
                    return View();
                }
                else
                {
                    ViewBag.Errorr = " Email đã tồn tại";
                    return View();
                }
            }
            return View();
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
                var tk = db.Nguoi_Dung.Where(n => n.taikhoan.Equals(taikhoan));
                var mk = db.Nguoi_Dung.Where(n => n.matkhau.Equals(matkhau));
                if (tk.Count() > 0 && mk.Count() > 0)
                {
                    Session["ten"] = tk.FirstOrDefault().ten;
                    Session["taikhoan"] = tk.FirstOrDefault().taikhoan;
                    Session["email"] = tk.FirstOrDefault().email;
                    Session["SDT"] = tk.FirstOrDefault().SDT;
                    Session["diachi"] = tk.FirstOrDefault().diachi;
                    Session["quyen"] = tk.FirstOrDefault().quyen;
                    Session["id"] = tk.FirstOrDefault().id;
                    if (Session["quyen"] != null)
                    {
                        return Redirect("~/Admin/Home/Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                else if (tk.Count() > 0 || mk.Count() < 0)
                {
                    ViewBag.errormk = "Mật khẩu không đúng";
                    return View();
                }
                else if (tk.Count() < 0 || mk.Count() > 0)
                {
                    ViewBag.errortk = "Tài khoản không đúng";
                    return View();
                }
                else
                {
                    ViewBag.errortk = "Tài khoản không đúng";
                    ViewBag.errormk = "Mật khẩu không đúng";
                    return View();
                }

            }
            return View();


        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult DangKyThanhCong()
        {
            return View();
        }
    }
}