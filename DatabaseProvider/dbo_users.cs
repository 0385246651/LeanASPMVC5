namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("[dbo.users]")]
    public partial class dbo_users
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động tăng
        public string ID { get; set; }  // 👈 Đảm bảo không có [StringLength] ở đây!

        [Required]
        [StringLength(250)]
        public string Uid { get; set; }

        [Required]
        [StringLength(250)]
        public string Pwd { get; set; }

        [Required]
        [StringLength(250)]
        public string Fullname { get; set; }
    }
}
