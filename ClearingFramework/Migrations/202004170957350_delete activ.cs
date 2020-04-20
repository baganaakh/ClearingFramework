namespace ClearingFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteactiv : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AdminActiveSessions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdminActiveSessions",
                c => new
                    {
                        id = c.Int(nullable: false),
                        sessionid = c.Int(nullable: false),
                        isactive = c.String(maxLength: 10, fixedLength: true),
                        starttime = c.Time(nullable: false, precision: 7),
                        endtime = c.Time(nullable: false, precision: 7),
                        tduration = c.Time(nullable: false, precision: 7),
                        matched = c.Int(nullable: false),
                        state = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.id, t.sessionid });
            
        }
    }
}
