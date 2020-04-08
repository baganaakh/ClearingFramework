namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminContract
    {
        public long id { get; set; }

        public long? securityId { get; set; }

        public short? type { get; set; }

        [StringLength(16)]
        public string code { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public decimal? lot { get; set; }

        public int? tickTable { get; set; }

        [Column(TypeName = "date")]
        public DateTime? sdate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? edate { get; set; }

        public short? groupId { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }

        public int? mmorderLimit { get; set; }

        public int? orderLimit { get; set; }

        public decimal? refpriceParam { get; set; }

        public long? bid { get; set; }
    }
}
