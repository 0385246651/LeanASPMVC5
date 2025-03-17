namespace DatabaseProvider
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDB : DbContext
    {
        public MyDB()
            : base("name=MyDB")
        {
        }

        public virtual DbSet<dbo_users> dbo_users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dbo_users>()
                .Property(e => e.Pwd)
                .IsUnicode(false);

            modelBuilder.Entity<dbo_users>()
                .Property(e => e.Fullname)
                .IsUnicode(false);
        }
    }
}
