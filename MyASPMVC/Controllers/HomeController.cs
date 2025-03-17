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
        public ActionResult Index()
        {
            DBIO db = new DBIO();

            //Insert data
            db.AddObject_User("3", "user2", "123456", "usser 2");

            dbo_users user = db.GetObject_User("haidv2k", "123456");

            if (user == null)
            {
                return View("Error"); // Nếu không tìm thấy user, chuyển hướng đến trang lỗi
            }

            ViewBag.Title = "Your Home page.";

            return View(user);
        }

        public ActionResult About(string id) // thêm tham số id từ url
        {
            // tạo obj từ MyModel
            MyModel myobj = new MyModel();
            // gán giá trị cho thuộc tính Name
            myobj.Name = "Raiden Nguyen";
            // gán giá trị cho thuộc tính Phone
            myobj.Phone = 0385246651;

            ViewBag.Message = "Your application description page." + id;

            // trả về myobj cho ViewBag
            ViewBag.MyObj = myobj;

            return View(myobj);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}