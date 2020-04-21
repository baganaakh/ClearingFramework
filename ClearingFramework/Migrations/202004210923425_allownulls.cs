namespace ClearingFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allownulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdminActiveSessions", "sessionid", c => c.Int());
            AlterColumn("dbo.AdminActiveSessions", "starttime", c => c.Time(precision: 7));
            AlterColumn("dbo.AdminActiveSessions", "endtime", c => c.Time(precision: 7));
            AlterColumn("dbo.AdminActiveSessions", "tduration", c => c.Time(precision: 7));
            AlterColumn("dbo.AdminActiveSessions", "matched", c => c.Int());
            AlterColumn("dbo.AdminAssets", "volume", c => c.Int());
            AlterColumn("dbo.AdminAssets", "expireDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminAssets", "state", c => c.Short());
            AlterColumn("dbo.AdminAssets", "modified", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminAssets", "ratio", c => c.Decimal(precision: 10, scale: 2));
            AlterColumn("dbo.AdminBoards", "type", c => c.Short());
            AlterColumn("dbo.AdminBoards", "state", c => c.Short());
            AlterColumn("dbo.AdminBoards", "modified", c => c.DateTime());
            AlterColumn("dbo.AdminCalendar", "tdate", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.AdminCalendar", "type", c => c.Short());
            AlterColumn("dbo.AdminCalendar", "note", c => c.String(maxLength: 1024));
            AlterColumn("dbo.AdminCalendar", "state", c => c.Short());
            AlterColumn("dbo.AdminCalendar", "modified", c => c.DateTime());
            AlterColumn("dbo.AdminMargins", "buy", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.AdminMargins", "sell", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.AdminMargins", "buytype", c => c.Short());
            AlterColumn("dbo.AdminMargins", "selltype", c => c.Short());
            AlterColumn("dbo.AdminMargins", "modified", c => c.DateTime());
            AlterColumn("dbo.AdminMargins", "mbuy", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.AdminMargins", "msell", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.AdminMargins", "coid", c => c.Long());
            AlterColumn("dbo.AdminMarketMakers", "contactid", c => c.Int());
            AlterColumn("dbo.AdminMarketMakers", "memberid", c => c.Int());
            AlterColumn("dbo.AdminMarketMakers", "accountid", c => c.Long());
            AlterColumn("dbo.AdminMarketMakers", "startdate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminMarketMakers", "enddate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminMarketMakers", "ticks", c => c.Int());
            AlterColumn("dbo.AdminMarketMakers", "description", c => c.String(maxLength: 128, fixedLength: true));
            AlterColumn("dbo.AdminMarketMakers", "orderlimit", c => c.Int());
            AlterColumn("dbo.AdminMarketMakers", "state", c => c.Short());
            AlterColumn("dbo.AdminMarketMakers", "modified", c => c.DateTime());
            AlterColumn("dbo.AdminMembers", "broker", c => c.Boolean());
            AlterColumn("dbo.AdminMembers", "dealer", c => c.Boolean());
            AlterColumn("dbo.AdminMembers", "ander", c => c.Boolean());
            AlterColumn("dbo.AdminMembers", "nominal", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdminMembers", "nominal", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AdminMembers", "ander", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AdminMembers", "dealer", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AdminMembers", "broker", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AdminMarketMakers", "modified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AdminMarketMakers", "state", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminMarketMakers", "orderlimit", c => c.Int(nullable: false));
            AlterColumn("dbo.AdminMarketMakers", "description", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            AlterColumn("dbo.AdminMarketMakers", "ticks", c => c.Int(nullable: false));
            AlterColumn("dbo.AdminMarketMakers", "enddate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminMarketMakers", "startdate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminMarketMakers", "accountid", c => c.Long(nullable: false));
            AlterColumn("dbo.AdminMarketMakers", "memberid", c => c.Int(nullable: false));
            AlterColumn("dbo.AdminMarketMakers", "contactid", c => c.Int(nullable: false));
            AlterColumn("dbo.AdminMargins", "coid", c => c.Long(nullable: false));
            AlterColumn("dbo.AdminMargins", "msell", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AlterColumn("dbo.AdminMargins", "mbuy", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AlterColumn("dbo.AdminMargins", "modified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AdminMargins", "selltype", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminMargins", "buytype", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminMargins", "sell", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AlterColumn("dbo.AdminMargins", "buy", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AlterColumn("dbo.AdminCalendar", "modified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AdminCalendar", "state", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminCalendar", "note", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.AdminCalendar", "type", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminCalendar", "tdate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.AdminBoards", "modified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AdminBoards", "state", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminBoards", "type", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminAssets", "ratio", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.AdminAssets", "modified", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminAssets", "state", c => c.Short(nullable: false));
            AlterColumn("dbo.AdminAssets", "expireDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AdminAssets", "volume", c => c.Int(nullable: false));
            AlterColumn("dbo.AdminActiveSessions", "matched", c => c.Int(nullable: false));
            AlterColumn("dbo.AdminActiveSessions", "tduration", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.AdminActiveSessions", "endtime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.AdminActiveSessions", "starttime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.AdminActiveSessions", "sessionid", c => c.Int(nullable: false));
        }
    }
}
