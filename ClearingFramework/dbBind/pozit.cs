namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pozit
    {
        public int id { get; set; }

        [StringLength(20)]
        public string accNum { get; set; }

        [StringLength(20)]
        public string side { get; set; }

        [StringLength(20)]
        public string assetCode { get; set; }

        public int? qty { get; set; }

        public decimal? price { get; set; }

        public decimal? fee { get; set; }

        public decimal? gainLoss { get; set; }

        public decimal? callDenchin { get; set; }
    }
}
