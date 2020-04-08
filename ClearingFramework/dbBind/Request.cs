namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Request")]
    public partial class Request
    {
        public long id { get; set; }

        [StringLength(20)]
        public string account { get; set; }

        public decimal? balance { get; set; }

        public decimal? remain { get; set; }

        public DateTime? date { get; set; }

        public int? pendingDay { get; set; }
    }
}
