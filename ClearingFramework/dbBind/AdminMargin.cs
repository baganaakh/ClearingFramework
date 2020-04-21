namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminMargin
    {
        [Key]
        public long contractId { get; set; }

        public decimal? buy { get; set; }

        public decimal? sell { get; set; }

        public short? buytype { get; set; }

        public short? selltype { get; set; }

        public DateTime? modified { get; set; }

        public decimal? mbuy { get; set; }

        public decimal? msell { get; set; }

        public long? coid { get; set; }
    }
}
