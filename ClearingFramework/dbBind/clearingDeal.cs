namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("clearingDeal")]
    public partial class clearingDeal
    {
        public long id { get; set; }

        public short? boardid { get; set; }

        [StringLength(16)]
        public string dealno { get; set; }

        public short? side { get; set; }

        public int? memberid { get; set; }

        public long? accountid { get; set; }

        public int? assetid { get; set; }

        public decimal? qty { get; set; }

        public decimal? price { get; set; }

        public decimal? totalPrice { get; set; }

        public short? state { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified { get; set; }

        public decimal? fee { get; set; }

        public decimal? m2m { get; set; }

        public decimal? refPrice { get; set; }

        public short? dealType { get; set; }
    }
}
