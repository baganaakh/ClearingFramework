namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminTtable")]
    public partial class AdminTtable
    {
        public long id { get; set; }

        public decimal? arrangePrice { get; set; }

        public decimal? tickSize { get; set; }

        public long? userid { get; set; }

        public long? assetid { get; set; }

        public DateTime? modified { get; set; }

        [StringLength(50)]
        public string name { get; set; }
    }
}
