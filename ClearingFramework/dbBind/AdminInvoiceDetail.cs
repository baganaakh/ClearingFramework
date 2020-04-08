namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdminInvoiceDetail
    {
        public long id { get; set; }

        public long? invoiceid { get; set; }

        public int? assetid { get; set; }

        public decimal? qty { get; set; }

        public decimal? price { get; set; }

        public short? state { get; set; }

        public DateTime? modified { get; set; }

        public string note { get; set; }

        public long? dealNo { get; set; }
    }
}
