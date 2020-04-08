namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminFee")]
    public partial class AdminFee
    {
        public int id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public decimal? Value { get; set; }

        public DateTime? modified { get; set; }
    }
}
