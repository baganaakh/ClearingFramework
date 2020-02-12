﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClearingFramework.dbBind.pageDatabase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class demoEntities1 : DbContext
    {
        public demoEntities1()
            : base("name=demoEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActiveSession> ActiveSessions { get; set; }
        public virtual DbSet<algo> algoes { get; set; }
        public virtual DbSet<allowedtype> allowedtypes { get; set; }
        public virtual DbSet<API> APIs { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<BoardInstrument> BoardInstruments { get; set; }
        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<ClearingAccount> ClearingAccounts { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ctype> ctypes { get; set; }
        public virtual DbSet<dayType> dayTypes { get; set; }
        public virtual DbSet<dbo_logs> dbo_logs { get; set; }
        public virtual DbSet<Deal2> Deal2 { get; set; }
        public virtual DbSet<DealerAccount> DealerAccounts { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Dealtype> Dealtypes { get; set; }
        public virtual DbSet<expireTable> expireTables { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<GroupRight> GroupRights { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<isactive> isactives { get; set; }
        public virtual DbSet<IsDealer> IsDealers { get; set; }
        public virtual DbSet<Margin> Margins { get; set; }
        public virtual DbSet<MarketMaker> MarketMakers { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<mtype> mtypes { get; set; }
        public virtual DbSet<netting> nettings { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Ptype> Ptypes { get; set; }
        public virtual DbSet<RefPrice> RefPrices { get; set; }
        public virtual DbSet<Right> Rights { get; set; }
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<side> sides { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }
        public virtual DbSet<SpecialType> SpecialTypes { get; set; }
        public virtual DbSet<Spread> Spreads { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<stype> stypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TickSizeTable> TickSizeTables { get; set; }
        public virtual DbSet<Tran> Trans { get; set; }
        public virtual DbSet<transType> transTypes { get; set; }
        public virtual DbSet<Ttable> Ttables { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<accType> accTypes { get; set; }
    }
}
