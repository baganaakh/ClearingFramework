namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminMarketMaker
    {
        public int id { get; set; }

        public int? contactid { get; set; }

        public int? memberid { get; set; }

        public long? accountid { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? startdate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? enddate { get; set; }

        public int? ticks { get; set; }

        public string description { get; set; }

        public int? orderlimit { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }
    }
}
