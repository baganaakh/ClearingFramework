namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminCalendar")]
    public partial class AdminCalendar
    {
        public short id { get; set; }

        [Column(TypeName = "date")]
        public DateTime tdate { get; set; }

        public short type { get; set; }

        [Required]
        [StringLength(1024)]
        public string note { get; set; }

        public short state { get; set; }

        public DateTime modified { get; set; }
    }
}
