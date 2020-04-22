namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminBoard
    {
        public short id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public short? type { get; set; }

        [StringLength(128)]
        public string tdays { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }

        [StringLength(1024)]
        public string description { get; set; }

        public short? dealType { get; set; }

        public TimeSpan? expTime { get; set; }

        public short? expDate { get; set; }
    }
}
