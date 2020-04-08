namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AccountDetail
    {
        public int id { get; set; }

        public decimal? freezeValue { get; set; }

        public decimal? totalNumber { get; set; }

        public int? assetId { get; set; }

        [StringLength(20)]
        public string accNum { get; set; }

        [StringLength(20)]
        public string linkAcc { get; set; }

        public DateTime? modified { get; set; }
    }
}
