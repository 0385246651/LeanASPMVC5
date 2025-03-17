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
        MyDB mydb = new MyDB();
        
        public dbo_users GetObject_User(string uid, string pwd)
        {
            //Không sử dung Parameters
            //string SQL = "SELECT * FROM [dbo.users] " +
            //    "WHERE Uid = '" + uid + "' AND Pwd = '" + pwd + "'";
            // // gọi db
            //mydb.Database.SqlQuery<dbo_users>(SQL).FirstOrDefault();

            // Sử dụng Parameters 
            return mydb.Database.SqlQuery<dbo_users>("SELECT * FROM [dbo.users] WHERE Uid = @Uid AND Pwd = @Pwd",
                new SqlParameter("@Uid", uid),
                new SqlParameter("@Pwd", pwd))
                .FirstOrDefault();

            mydb.dbo_users.Where(x => x.Uid == uid && x.Pwd == pwd).FirstOrDefault();

        }

        // Query thêm sửa xóa ko cần trả về dữ liệu
        public void AddObject_User(string id, string uid, string pwd, string Fullname)
        {
           //

            mydb.Database.ExecuteSqlCommand("INSERT INTO [dbo.users] (Id, Uid, Pwd, Fullname) VALUES (@Id, @Uid, @Pwd, @Fullname)",
                new SqlParameter("@Id", id),
                new SqlParameter("@Uid", uid),
                new SqlParameter("@Pwd", pwd),
                new SqlParameter("@Fullname", Fullname));
        }

        
    }
}
