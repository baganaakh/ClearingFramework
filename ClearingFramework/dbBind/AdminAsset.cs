namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminAsset
    {
        public int id { get; set; }

        [Required]
        [StringLength(16)]
        public string code { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int? volume { get; set; }

        public string note { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? expireDate { get; set; }

        public short? state { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? modified { get; set; }

        public decimal? ratio { get; set; }

        public decimal? price { get; set; }
    }
}
