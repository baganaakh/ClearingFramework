namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminAccountDetail
    {
        public int id { get; set; }

        public decimal? freezeValue { get; set; }

        public decimal? amount { get; set; }

        public int? assetId { get; set; }

        public long? accountId { get; set; }
    }
}
