namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminSession")]
    public partial class AdminSession
    {
        public long id { get; set; }

        public long? boardid { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public TimeSpan? stime { get; set; }

        public int? duration { get; set; }

        public short? algorithm { get; set; }

        public int? match { get; set; }

        public short? allowedtypes { get; set; }

        [StringLength(1024)]
        public string description { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }

        public bool? isactive { get; set; }

        [Column(TypeName = "date")]
        public DateTime? starttime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? endtime { get; set; }

        [StringLength(10)]
        public string tduration { get; set; }

        public long? matched { get; set; }

        public bool? editorder { get; set; }

        public bool? delorder { get; set; }

        public short? markettype { get; set; }
    }
}
