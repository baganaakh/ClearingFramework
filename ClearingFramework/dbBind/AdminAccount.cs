namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminAccount")]
    public partial class AdminAccount
    {
        public long id { get; set; }

        public long? memberid { get; set; }

        [StringLength(16)]
        public string accNumber { get; set; }

        public short? accountType { get; set; }

        public long? LinkAccount { get; set; }

        public DateTime? modified { get; set; }

        [StringLength(20)]
        public string mask { get; set; }

        [Column(TypeName = "date")]
        public DateTime? startdate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? enddate { get; set; }

        public short? state { get; set; }
    }
}
