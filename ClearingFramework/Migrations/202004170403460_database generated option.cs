namespace ClearingFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databasegeneratedoption : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AdminInvoices");
            AlterColumn("dbo.AdminInvoices", "invoiceno", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.AdminInvoices", new[] { "id", "invoiceno" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AdminInvoices");
            AlterColumn("dbo.AdminInvoices", "invoiceno", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.AdminInvoices", new[] { "id", "invoiceno" });
        }
    }
}
