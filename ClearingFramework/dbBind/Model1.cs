namespace ClearingFramework.dbBind
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Windows;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
            Database.SetInitializer<Model1>(new CreateDatabaseIfNotExists<Model1>());
        }
        public class Model1DBInit : CreateDatabaseIfNotExists<Model1>
        {
            protected override void Seed(Model1 db)
            {
                Account acct = new Account
                {
                    accNum = "1",
                    idNum = "sd5669889",
                    lname = "last",
                    fname = "First",
                    phone = "7879879",
                    mail = "sdadas",
                    state = Convert.ToInt16(1),
                    modified = DateTime.Now,
                    secAcc = "s123123",
                    fee = Convert.ToDecimal(0.56),
                    denchinPercent = Convert.ToDecimal(0.35),
                    contractFee = Convert.ToDecimal(0.94),
                    pozFee = Convert.ToDecimal(0.68),
                    memId = 1,
                    bank = 1,
                };
                AdminUser au = new AdminUser()
                {
                    uname = "baganaakh",
                    password = "baganaakh",
                    modified = DateTime.Now,
                    role = "user",
                    memId = 1
                };
                db.AdminUsers.Add(au);
                db.Accounts.Add(acct);
                db.SaveChanges();
                //var query = db.AdminUsers.Where(s => s.uname == "baganaakh").FirstOrDefault<AdminUser>();
                //MessageBox.Show(query.role);
                base.Seed(db);
            }
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual DbSet<AdminAccount> AdminAccounts { get; set; }
        public virtual DbSet<AdminAccountDetail> AdminAccountDetails { get; set; }
        public virtual DbSet<AdminActiveSession> AdminActiveSessions { get; set; }
        public virtual DbSet<AdminAsset> AdminAssets { get; set; }
        public virtual DbSet<AdminBoard> AdminBoards { get; set; }
        public virtual DbSet<AdminCalendar> AdminCalendars { get; set; }
        public virtual DbSet<AdminContract> AdminContracts { get; set; }
        public virtual DbSet<AdminDeal> AdminDeals { get; set; }
        public virtual DbSet<AdminFee> AdminFees { get; set; }
        public virtual DbSet<AdminInterest> AdminInterests { get; set; }
        public virtual DbSet<AdminInvoiceDetail> AdminInvoiceDetails { get; set; }
        public virtual DbSet<AdminInvoice> AdminInvoices { get; set; }
        public virtual DbSet<AdminMargin> AdminMargins { get; set; }
        public virtual DbSet<AdminMarketMaker> AdminMarketMakers { get; set; }
        public virtual DbSet<AdminMember> AdminMembers { get; set; }
        public virtual DbSet<Adminmtype> Adminmtypes { get; set; }
        public virtual DbSet<AdminOrder> AdminOrders { get; set; }
        public virtual DbSet<AdminReason> AdminReasons { get; set; }
        public virtual DbSet<AdminRefPrice> AdminRefPrices { get; set; }
        public virtual DbSet<AdminSecurity> AdminSecurities { get; set; }
        public virtual DbSet<AdminSession> AdminSessions { get; set; }
        public virtual DbSet<AdminSpread> AdminSpreads { get; set; }
        public virtual DbSet<AdminTickSizeTable> AdminTickSizeTables { get; set; }
        public virtual DbSet<AdminTransaction> AdminTransactions { get; set; }
        public virtual DbSet<AdminTtable> AdminTtables { get; set; }
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<clearingDeal> clearingDeals { get; set; }
        public virtual DbSet<deal> deals { get; set; }
        public virtual DbSet<lastPrice> lastPrices { get; set; }
        public virtual DbSet<pozit> pozits { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.fee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Account>()
                .Property(e => e.denchinPercent)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Account>()
                .Property(e => e.contractFee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Account>()
                .Property(e => e.pozFee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AccountDetail>()
                .Property(e => e.freezeValue)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AccountDetail>()
                .Property(e => e.totalNumber)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminAccountDetail>()
                .Property(e => e.freezeValue)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminAccountDetail>()
                .Property(e => e.amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminActiveSession>()
                .Property(e => e.isactive)
                .IsFixedLength();

            modelBuilder.Entity<AdminActiveSession>()
                .Property(e => e.state)
                .IsFixedLength();

            modelBuilder.Entity<AdminAsset>()
                .Property(e => e.ratio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<AdminAsset>()
                .Property(e => e.price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminContract>()
                .Property(e => e.lot)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminContract>()
                .Property(e => e.refpriceParam)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.qty)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.totalPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.fee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.m2m)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.refPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.interests)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminDeal>()
                .Property(e => e.toPay)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminFee>()
                .Property(e => e.Value)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AdminInterest>()
                .Property(e => e.interest)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInterest>()
                .Property(e => e.repoInterset)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInterest>()
                .Property(e => e.loanInterset)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInterest>()
                .Property(e => e.maxValue)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInterest>()
                .Property(e => e.minValue)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInvoiceDetail>()
                .Property(e => e.qty)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInvoiceDetail>()
                .Property(e => e.price)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminInvoice>()
                .Property(e => e.qty)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInvoice>()
                .Property(e => e.totalPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminInvoice>()
                .Property(e => e.fee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminMargin>()
                .Property(e => e.buy)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminMargin>()
                .Property(e => e.sell)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminMargin>()
                .Property(e => e.mbuy)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminMargin>()
                .Property(e => e.msell)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminMarketMaker>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<Adminmtype>()
                .Property(e => e.minValue)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminOrder>()
                .Property(e => e.price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminOrder>()
                .Property(e => e.totSum)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminOrder>()
                .Property(e => e.toPay)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminOrder>()
                .Property(e => e.interests)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminOrder>()
                .Property(e => e.fee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminOrder>()
                .Property(e => e.price2)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminRefPrice>()
                .Property(e => e.refprice)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminSecurity>()
                .Property(e => e.firstPrice)
                .HasPrecision(18, 6);

            modelBuilder.Entity<AdminSecurity>()
                .Property(e => e.intRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<AdminSession>()
                .Property(e => e.tduration)
                .IsFixedLength();

            modelBuilder.Entity<AdminTtable>()
                .Property(e => e.arrangePrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AdminTtable>()
                .Property(e => e.tickSize)
                .HasPrecision(18, 6);

            modelBuilder.Entity<clearingDeal>()
                .Property(e => e.qty)
                .HasPrecision(18, 4);

            modelBuilder.Entity<clearingDeal>()
                .Property(e => e.price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<clearingDeal>()
                .Property(e => e.totalPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<clearingDeal>()
                .Property(e => e.fee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<clearingDeal>()
                .Property(e => e.m2m)
                .HasPrecision(18, 4);

            modelBuilder.Entity<clearingDeal>()
                .Property(e => e.refPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<deal>()
                .Property(e => e.qty)
                .HasPrecision(18, 4);

            modelBuilder.Entity<deal>()
                .Property(e => e.price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<deal>()
                .Property(e => e.totalPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<deal>()
                .Property(e => e.fee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<deal>()
                .Property(e => e.m2m)
                .HasPrecision(18, 4);

            modelBuilder.Entity<deal>()
                .Property(e => e.refPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<lastPrice>()
                .Property(e => e.ePrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<pozit>()
                .Property(e => e.price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<pozit>()
                .Property(e => e.fee)
                .HasPrecision(18, 4);

            modelBuilder.Entity<pozit>()
                .Property(e => e.gainLoss)
                .HasPrecision(18, 4);

            modelBuilder.Entity<pozit>()
                .Property(e => e.callDenchin)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Request>()
                .Property(e => e.balance)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Request>()
                .Property(e => e.remain)
                .HasPrecision(18, 4);

            modelBuilder.Entity<transaction>()
                .Property(e => e.value)
                .HasPrecision(18, 4);
        }
    }
}
