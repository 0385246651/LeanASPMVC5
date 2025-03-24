using DatabaseIO;
using DatabaseProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyASPMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(FormCollection data)
        {
            string uid = data["uid"];
            string pwd = data["pass"];

            // Tìm kiếm trong DB bằng username
            JsonResult result = new JsonResult();
            DBIO db = new DBIO();
            dbo_users user = db.GetObject_UserName(uid);

            if (user == null) {
                result.Data = new { status = "Er", message = "User not found !" };
            }
            else
            {
                if(user.Pwd == pwd)
                {
                    Session["user"] = user;
                    result.Data = new { status = "OK", message = "Login success !", data = user };
                }
                else
                {
                    result.Data = new { status = "Er", message = "Password not correct !" };
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Register()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult RegisterUser(FormCollection data)
        {
            string uid = data["Uid"];
            string pwd = data["Pwd"];
            string fullname = data["Fullname"];

            JsonResult js = new JsonResult();

            if (string.IsNullOrEmpty(uid) ||
                string.IsNullOrEmpty(pwd) ||
                string.IsNullOrEmpty(fullname)
            )
            {
                js.Data = new { status = "Er", message = "Not allow blank data !" };
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // kết nối lớp thao tác vs db
                DBIO db = new DBIO();
                dbo_users user = new dbo_users();
                // trả về 1 chuỗi GUID ngẫu nhiên chưa tồn tại
                user.ID = Guid.NewGuid().ToString();
                user.Uid = uid;
                user.Pwd = pwd;
                user.Fullname = fullname;
                // thêm user mới
                db.AddObject(user);

                db.SaveChange();
                js.Data = new { status = "OK", message = "Register success !", data = user };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}