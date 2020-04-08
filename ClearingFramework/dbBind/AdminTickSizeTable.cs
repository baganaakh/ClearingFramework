namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminTickSizeTable")]
    public partial class AdminTickSizeTable
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }
    }
}
