namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("transaction")]
    public partial class transaction
    {
        public int id { get; set; }

        [StringLength(20)]
        public string accNum { get; set; }

        public short? transType { get; set; }

        public decimal? value { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        public short? side { get; set; }

        public DateTime? modified { get; set; }

        public int? assetid { get; set; }
    }
}
