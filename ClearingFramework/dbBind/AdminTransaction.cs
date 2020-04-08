namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminTransaction")]
    public partial class AdminTransaction
    {
        public long id { get; set; }

        public long? accountId { get; set; }

        public short? type { get; set; }

        public short? type1 { get; set; }

        public int? amount { get; set; }

        public int? assetId { get; set; }

        public int? rate { get; set; }

        public string note { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tdate { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }

        public long? userId { get; set; }

        public int? memberid { get; set; }

        [StringLength(20)]
        public string currency { get; set; }
    }
}
