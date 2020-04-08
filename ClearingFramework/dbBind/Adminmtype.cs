namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adminmtype")]
    public partial class Adminmtype
    {
        public short id { get; set; }

        [Required]
        [StringLength(20)]
        public string mtype { get; set; }

        public decimal? minValue { get; set; }
    }
}
