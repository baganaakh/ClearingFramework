namespace ClearingFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminmembechangetobool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdminMembers", "broker", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AdminMembers", "dealer", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AdminMembers", "ander", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AdminMembers", "nominal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdminMembers", "nominal", c => c.String(maxLength: 20));
            AlterColumn("dbo.AdminMembers", "ander", c => c.String(maxLength: 20));
            AlterColumn("dbo.AdminMembers", "dealer", c => c.String(maxLength: 20));
            AlterColumn("dbo.AdminMembers", "broker", c => c.String(maxLength: 20));
        }
    }
}
