namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lastPrice")]
    public partial class lastPrice
    {
        public int id { get; set; }

        public int? assetid { get; set; }

        public decimal? ePrice { get; set; }
    }
}
