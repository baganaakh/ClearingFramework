namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminRefPrice")]
    public partial class AdminRefPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long contractId { get; set; }

        public decimal? refprice { get; set; }

        public DateTime? modified { get; set; }

        [StringLength(20)]
        public string name { get; set; }
    }
}
