using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyASPMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Sẽ ưu tiền route đầu tiên, nếu ko tìm thấy sẽ chạy route tiếp theo
            // Ví dụ: /san-pham va san-pham-moi sẽ chạy route đầu tiên mactch

            // thêm cấu hình route 
            routes.MapRoute(
                name: "List of book", // tên route
                url: "tim-sach", // url của route
                defaults: new { controller = "Book", action = "List" }
            );

            // chi tiết
            routes.MapRoute(
                name: "Detail of book", // tên route
                url: "chi-tiet-sach/{id}", // url của route
                defaults: new { controller = "Book", action = "Detail", id = UrlParameter.Optional }
            );

            // danh sách sách theo thể loại
            routes.MapRoute(
                name: "List of book by category", // tên route
                url: "sach-theo-the-loai/{category}", // url của route
                defaults: new { controller = "Book", action = "ListByCategory", category = UrlParameter.Optional }
            );

            // nếu ko tìm thấy bất kỳ cấu hình route nào thì sẽ chạy cái này
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
