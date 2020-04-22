namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminUser
    {
        public int id { get; set; }

        [StringLength(30)]
        public string uname { get; set; }

        [StringLength(30)]
        public string password { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime modified { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        public int? memId { get; set; }
    }
}
