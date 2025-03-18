using DatabaseIO;
using DatabaseProvider;
using MyASPMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyASPMVC.Controllers
{
    public class HomeController : Controller
    {
        #region -- ví dụ Học query dB 2 cách --
        //public ActionResult Index()
        //{
        //    DBIO db = new DBIO();

        //    //Insert data
        //    //db.AddObject_User("3", "user2", "123456", "usser 2");

        //    //dbo_users user = db.GetObject_User("haidv2k", "123456");

        //    //if (user == null)
        //    //{
        //    //    return View("Error"); // Nếu không tìm thấy user, chuyển hướng đến trang lỗi
        //    //}

        //    // lấy danh sách user
        //    List<dbo_users> list = db.GetList_User();

        //    ViewBag.Title = "Your Home page.";

        //    return View(list);
        //}

        //public ActionResult About(string id) // thêm tham số id từ url
        //{
        //    // tạo obj từ MyModel
        //    MyModel myobj = new MyModel();
        //    // gán giá trị cho thuộc tính Name
        //    myobj.Name = "Raiden Nguyen";
        //    // gán giá trị cho thuộc tính Phone
        //    myobj.Phone = 0385246651;

        //    ViewBag.Message = "Your application description page." + id;

        //    // trả về myobj cho ViewBag
        //    ViewBag.MyObj = myobj;

        //    return View(myobj);
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        #endregion

        public ActionResult Index()
        {
            // kết nối lớp thao tác vs db
            DBIO db = new DBIO();

            // lấy danh sách user
            List<dbo_users> list = db.GetList<dbo_users>();

            return View(list);
        }

        //Thêm user mới
        //Bạn dùng JsonResult trong ASP.NET MVC để trả về dữ liệu JSON
        //cho client-side (JavaScript, AJAX).
        [HttpPost]
        public JsonResult AddUser(FormCollection data)
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
                js.Data = new { status = "OK", message = "Add user success !" , data = user};
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        //Xoá User
        [HttpPost]
        public JsonResult DeleteUser(FormCollection data)
        {
            string id = data["id"];

            JsonResult js = new JsonResult();

            if (string.IsNullOrEmpty(id) )
            {
                js.Data = new { status = "Er", message = "Must be have ID" };
            }
            else
            {
                // kết nối lớp thao tác vs db
                DBIO db = new DBIO();

                // lấy danh sách user
                dbo_users user = db.GetObject_User<dbo_users>(id);
                if (user == null)
                {
                    js.Data = new { status = "Er", message = "Account not found !!" };
                }
                else { 
                 //Xóa dữ liệu
                db.DeleteObject(user);
                db.SaveChange();
                js.Data = new { status = "OK", message = "Delete user success !", data = user };
                }
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        //Edit
        [HttpPost]
        public ActionResult EditUser(FormCollection data) {
            string id = data["id"];
            string uid = data["Uid"];
            string pwd = data["Pwd"];
            string fullname = data["Fullname"];

            JsonResult js = new JsonResult();

            if (string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(uid) ||
                string.IsNullOrEmpty(fullname)
            )
            {
                js.Data = new { status = "Er", message = "Not allow blank data (except password) !" };
                return Json(js, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // kết nối lớp thao tác vs db
                DBIO db = new DBIO();

                // lấy danh sách user
                dbo_users user = db.GetObject_User<dbo_users>(id);
                if (user == null)
                {
                    js.Data = new { status = "Er", message = "Account not found !!" };
                }
                else
                {
                    //cập nhật db
                    user.Uid = uid;
                    user.Fullname = fullname;
                    if (!string.IsNullOrEmpty(pwd)) { 
                        user.Pwd = pwd;
                    }
                    
                    db.SaveChange();
                    js.Data = new { status = "OK", message = "Edit user success !", data = user };
                }
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }
        
    }
}