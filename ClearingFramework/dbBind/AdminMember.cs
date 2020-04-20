namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminMember
    {
        public int id { get; set; }

        public short? type { get; set; }

        [Required]
        [StringLength(2)]
        public string code { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }

        public long? partid { get; set; }

        [StringLength(20)]
        public string mask { get; set; }

        [Column(TypeName = "date")]
        public DateTime? startdate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? enddate { get; set; }

        public bool broker { get; set; }

        public bool dealer { get; set; }

        public bool ander { get; set; }

        public bool nominal { get; set; }

        public int? linkMember { get; set; }

        [StringLength(20)]
        public string name { get; set; }
    }
}
