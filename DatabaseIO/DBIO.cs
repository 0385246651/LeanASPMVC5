using DatabaseProvider;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseIO
{
    public class DBIO
    {
        //obj kết nối db
        MyDB mydb = new MyDB();

        #region -----------ví dụ Học query dB 2 cách -----------
        //public dbo_users GetObject_User(string uid, string pwd)
        //{
        //    //Không sử dung Parameters
        //    //string SQL = "SELECT * FROM [dbo.users] " +
        //    //    "WHERE Uid = '" + uid + "' AND Pwd = '" + pwd + "'";
        //    // // gọi db
        //    //mydb.Database.SqlQuery<dbo_users>(SQL).FirstOrDefault();

        //    //// Sử dụng Parameters và query đồng bộ
        //    //return mydb.Database.SqlQuery<dbo_users>("SELECT * FROM [dbo.users] WHERE Uid = @Uid AND Pwd = @Pwd",
        //    //    new SqlParameter("@Uid", uid),
        //    //    new SqlParameter("@Pwd", pwd))
        //    //    .FirstOrDefault();

        //    //mydb.dbo_users.Where(x => x.Uid == uid && x.Pwd == pwd).FirstOrDefault();

        //    // KHi Sử dụng IQuery
        //    //Quá trình đọc dữ liệu chỉ đc thực thi chỉ khi bạn gọi method tổng kết
        //    //như ToList(), FirstOrDefault(), SingleOrDefault(), 
        //    return mydb.dbo_users.Where(x => x.Uid == uid && x.Pwd == pwd).FirstOrDefault();

        //}

        //// Query lấy dữ liệu trả về List users
        //public List<dbo_users> GetList_User()
        //{
        //    // Sử dụng IQuery
        //    return mydb.dbo_users.OrderBy(u=> u.Fullname).Take(10).ToList();
        //}



        // Query thêm sửa xóa ko cần trả về dữ liệu
        //public void AddObject_User(string id, string uid, string pwd, string Fullname)
        //{
        //   // Sử dụng truy vấn đồng bộ
        //    //mydb.Database.ExecuteSqlCommand("INSERT INTO [dbo.users] (Id, Uid, Pwd, Fullname) VALUES (@Id, @Uid, @Pwd, @Fullname)",
        //    //    new SqlParameter("@Id", id),
        //    //    new SqlParameter("@Uid", uid),
        //    //    new SqlParameter("@Pwd", pwd),
        //    //    new SqlParameter("@Fullname", Fullname));

        //    // Sử dụng IQuery
        //    // trong cùng 1 kết nối db, nếu thực hiện thêm sửa xóa, cần gọi SaveChanges() để lưu thay đổi
        //    mydb.dbo_users.Add(new dbo_users()
        //    {
        //        ID = id,
        //        Uid = uid,
        //        Pwd = pwd,
        //        Fullname = Fullname
        //    });

        //}

        //public void UpdateObject_User(string id, string uid, string pwd, string Fullname)
        //{
        //    // Sử dụng truy vấn đồng bộ
        //    mydb.Database.ExecuteSqlCommand("UPDATE [dbo.users] SET Uid = @Uid, Pwd = @Pwd, Fullname = @Fullname WHERE Id = @Id",
        //        new SqlParameter("@Id", id),
        //        new SqlParameter("@Uid", uid),
        //        new SqlParameter("@Pwd", pwd),
        //        new SqlParameter("@Fullname", Fullname));
        //    // Sử dụng IQuery
        //    // trong cùng 1 kết nối db, nếu thực hiện thêm sửa xóa, cần gọi SaveChanges() để lưu thay đổi
        //    dbo_users user = mydb.dbo_users.Where(x => x.ID == id).FirstOrDefault();
        //    if (user != null)
        //    {
        //        user.Uid = uid;
        //        user.Pwd = pwd;
        //        user.Fullname = Fullname;
        //    }
        //}
        #endregion


        // method chung Thêm dữ liệu dùng generics
        // thêm 1 object vào table
        public void AddObject<T>(T obj)
        {
            mydb.Set(obj.GetType()).Add(obj);
        }

        //method lấy 1 list object từ table
        //Cụm where T : class đảm bảo rằng T phải là một lớp (class),
        //không phải kiểu dữ liệu nguyên thủy như int, string,...
        //Set<T>() lấy DbSet tương ứng với kiểu T từ DbContext.
        //ví dụ khi dùng : List<dbo_users> users = GetList<dbo_users>();
        public List<T> GetList<T>() where T : class 
        {
            return mydb.Set<T>().ToList();
        }

        //Method xóa 1 obj
        public void DeleteObject<T>(T obj)
        {

            mydb.Set(obj.GetType()).Remove(obj);
        }

        //Method xóa 1 mảng 
        public void DeleteList<T>(List<T> objs) where T : class
        {
            var dbSet = mydb.Set<T>();
            dbSet.RemoveRange(objs);
        }

        //Get detail by id
        public T GetObject_User<T>(string id) where T : class
        {
            return mydb.Set<T>().Find(id);
        }

        //Upate by id



        //method để save mọi thay đổi đang có vào db
        public void SaveChange()
        {
            mydb.SaveChanges();
        }
    }
}
