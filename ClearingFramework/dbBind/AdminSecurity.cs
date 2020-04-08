namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminSecurity
    {
        public int id { get; set; }

        public int? partid { get; set; }

        public short? type { get; set; }

        [StringLength(16)]
        public string code { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? totalQty { get; set; }

        public decimal? firstPrice { get; set; }

        public decimal? intRate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? sdate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? edate { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }

        public int? assetId { get; set; }
    }
}
