namespace ClearingFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accounttableaddthreemorecolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "bank", c => c.Int(nullable: false));
            AddColumn("dbo.Account", "bankAccName", c => c.String());
            AddColumn("dbo.Account", "bankAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "bankAccount");
            DropColumn("dbo.Account", "bankAccName");
            DropColumn("dbo.Account", "bank");
        }
    }
}
