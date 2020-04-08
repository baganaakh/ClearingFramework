namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminReason")]
    public partial class AdminReason
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(1024)]
        public string description { get; set; }
    }
}
