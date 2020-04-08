namespace ClearingFramework.dbBind
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminOrder")]
    public partial class AdminOrder
    {
        public long id { get; set; }

        public long? boardId { get; set; }

        public short? side { get; set; }

        public long? memberid { get; set; }

        public long? accountid { get; set; }

        public long? assetid { get; set; }

        public int? qty { get; set; }

        public decimal? price { get; set; }

        public short? state { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime modified { get; set; }

        public short? dealType { get; set; }

        [StringLength(20)]
        public string connect { get; set; }

        public int? day { get; set; }

        public decimal? totSum { get; set; }

        public decimal? toPay { get; set; }

        public decimal? interests { get; set; }

        public decimal? fee { get; set; }

        public long? assetid2 { get; set; }

        public int? qty2 { get; set; }

        public decimal? price2 { get; set; }
    }
}
