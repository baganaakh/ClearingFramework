//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Non_Member.dbBind.AdminDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Member
    {
        public int id { get; set; }
        public Nullable<short> type { get; set; }
        public string code { get; set; }
        public Nullable<short> state { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public Nullable<long> partid { get; set; }
        public string mask { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public string broker { get; set; }
        public string dealer { get; set; }
        public string ander { get; set; }
        public string nominal { get; set; }
    }
}
