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
    }
}