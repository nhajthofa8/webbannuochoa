using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Nhom18WebBanHoa.Models;

namespace Nhom18WebBanHoa.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHoaEntities db = new QuanLyBanHoaEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View((List<GioHang>)Session["cart"]);
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<GioHang> cart = new List<GioHang>();
                cart.Add(new GioHang { San_Pham = db.San_Pham.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<GioHang> cart = (List<GioHang>)Session["cart"];
                // kiem tra san pha co ton taij trong gio hang chua
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity += quantity;
                }
                else
                {
                    cart.Add(new GioHang { San_Pham = db.San_Pham.Find(id), Quantity = quantity });
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;

                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành Công", JsonRequestBehavior.AllowGet });
        }

        private int isExist(int id)
        {
            List<GioHang> cart = (List<GioHang>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].San_Pham.id.Equals(id))
                    return i;
            }
            return -1;
        }


        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<GioHang>>(cartModel);
            var cart = (List<GioHang>)Session["cart"];
            foreach (var item in cart)
            {
                var jsonItem = jsonCart.SingleOrDefault(n => n.San_Pham.id == item.San_Pham.id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;

                }
            }
            Session["cart"] = cart;
            return Json(new
            {
                status = true
            });
        }


        public JsonResult Remove(int id)
        {
            var cart = (List<GioHang>)Session["cart"];
            cart.RemoveAll(n => n.San_Pham.id == id);
            Session["cart"] = cart;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new
            {
                status = true
            });
        }


        [HttpGet]
        public ActionResult DatHang()
        {
            return View((List<GioHang>)Session["cart"]);


        }
        [HttpPost]
        public ActionResult DatHang(string ten, string SDT, string email, string diachi, string tieude)
        {

            List<GioHang> lst = (List<GioHang>)Session["cart"];
            Don_Hang donhang = new Don_Hang();
            donhang.ngaytao = DateTime.Now;
            donhang.ten = ten;
            donhang.email = email;
            donhang.SDT = SDT;
            donhang.tieude = tieude;
            donhang.diachi = diachi;
            donhang.tongtien = lst.Sum(n => n.Quantity * n.San_Pham.gia);
            if (Session["id"] != null)
            {
                donhang.idNgDung = int.Parse(Session["id"].ToString());
            }
            else
            {
                donhang.idNgDung = null;
            }
            //donhang.idNgDung = int.Parse(Session["id"].ToString());

            Session["tongtien"] = donhang.tongtien;
            Session["tendat"] = donhang.ten;
            Session["SDTdat"] = donhang.SDT;
            Session["tieude"] = donhang.tieude;
            Session["diachidat"] = donhang.diachi;

            Session["ngaydat"] = donhang.ngaytao;

            db.Don_Hang.Add(donhang);
            db.SaveChanges();


            int chitietID = donhang.id;
            List<Chi_Tiet_Don_Hang> lstchitiet = new List<Chi_Tiet_Don_Hang>();

            foreach (var item in lst)
            {
                Chi_Tiet_Don_Hang chitiet = new Chi_Tiet_Don_Hang();
                chitiet.idDonHang = chitietID;
                chitiet.tenSP = item.San_Pham.tenSP;
                chitiet.idSP = item.San_Pham.id;
                chitiet.soluong = item.Quantity;
                chitiet.gia = item.San_Pham.gia;
                chitiet.tonggia = item.Quantity * item.San_Pham.gia;
                lstchitiet.Add(chitiet);
            }
            db.Chi_Tiet_Don_Hang.AddRange(lstchitiet);
            db.SaveChanges();


            string tb = "<html><body><table boder='1'><caption>Thông tin đặt hàng</caption><tr><th>STT</th><th>Tên Sản Phẩm</th><th>Số Lượng</th><th>Đơn Giá</th>Thành Tiền</tr>";
            int i = 0;


            foreach (var item in lst)
            {
                i++;
                tb += "<tr>";
                tb += "<td>" + i.ToString() + "</td>";
                tb += "<td>" + item.San_Pham.tenSP + "</td>";
                tb += "<td>" + item.Quantity.ToString() + "</td>";
                tb += "<td>" + item.San_Pham.gia.ToString() + "</td>";
                tb += "<td>" + String.Format("{0:#,###}", item.Quantity * item.San_Pham.gia) + "</td>";
                tb += "</tr>";

            }
            var tongtien = lst.Sum(n => n.Quantity * n.San_Pham.gia);
            tb += "<tr><th colpan='5'>Tổng Cộng: "
                + String.Format("{0:#,### vnd}", tongtien) + "</th></tr></table>";
            return RedirectToAction("DatHangThanhCong");
        }

        public ActionResult DatHangThanhCong()
        {
            //Session.Clear();
            return View();
        }
    }
}