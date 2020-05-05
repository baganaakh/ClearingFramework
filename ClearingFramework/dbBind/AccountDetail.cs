namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AccountDetail
    {
        public long id { get; set; }

        public int? freezeValue { get; set; }

        public int? totalNumber { get; set; }

        public int? assetId { get; set; }
                
        public long accountId { get; set; }

        [StringLength(20)]
        public string linkAcc { get; set; }

        public DateTime? modified { get; set; }
    }
}
