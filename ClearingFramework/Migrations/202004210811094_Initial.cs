namespace ClearingFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "bank", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Account", "bank", c => c.Int(nullable: false));
        }
    }
}
