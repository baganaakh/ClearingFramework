namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminSpread
    {
        public int id { get; set; }

        public int? contractid { get; set; }

        public int? sessionid { get; set; }

        public int? rspread { get; set; }

        public int? ispread { get; set; }

        public int? rparam { get; set; }

        public DateTime? modified { get; set; }
    }
}
