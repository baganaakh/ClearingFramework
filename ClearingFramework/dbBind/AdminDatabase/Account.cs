//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClearingFramework.dbBind.AdminDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public long id { get; set; }
        public long memberid { get; set; }
        public string accNum { get; set; }
        public Nullable<short> accType { get; set; }
        public string LinkAcc { get; set; }
        public Nullable<decimal> collateral { get; set; }
        public System.DateTime modified { get; set; }
        public string mask { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public short state { get; set; }
        public Nullable<decimal> blnc { get; set; }
        public Nullable<decimal> sblnc { get; set; }
    }
}