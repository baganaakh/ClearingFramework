namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminAccountDetail
    {
        public long id { get; set; }

        public int? freezeValue { get; set; }

        public int? amount { get; set; }

        public int? assetId { get; set; }

        public long? accountId { get; set; }
    }
}
