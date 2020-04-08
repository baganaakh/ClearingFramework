namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminInterest
    {
        public int id { get; set; }

        public decimal? interest { get; set; }

        public int? assetid { get; set; }

        public decimal? repoInterset { get; set; }

        public decimal? loanInterset { get; set; }

        public decimal? maxValue { get; set; }

        public decimal? minValue { get; set; }
    }
}
