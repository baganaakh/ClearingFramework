namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminActiveSession
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sessionid { get; set; }

        [Required]
        [StringLength(10)]
        public string isactive { get; set; }

        public TimeSpan starttime { get; set; }

        public TimeSpan endtime { get; set; }

        public TimeSpan tduration { get; set; }

        public int matched { get; set; }

        [Required]
        [StringLength(10)]
        public string state { get; set; }
    }
}
