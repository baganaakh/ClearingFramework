﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Non_Member.dbBind
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class clearingEntities1 : DbContext
    {
        public clearingEntities1()
            : base("name=clearingEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual DbSet<clearingDeal> clearingDeals { get; set; }
        public virtual DbSet<deal> deals { get; set; }
        public virtual DbSet<pozit> pozits { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
        public virtual DbSet<lastPrice> lastPrices { get; set; }
    }
}