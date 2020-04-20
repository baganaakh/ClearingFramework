namespace ClearingFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        freezeValue = c.Decimal(precision: 18, scale: 4),
                        totalNumber = c.Decimal(precision: 18, scale: 4),
                        assetId = c.Int(),
                        accNum = c.String(maxLength: 20),
                        linkAcc = c.String(maxLength: 20),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        accNum = c.String(nullable: false, maxLength: 20),
                        idNum = c.String(nullable: false, maxLength: 10),
                        lname = c.String(maxLength: 30),
                        fname = c.String(maxLength: 30),
                        phone = c.String(maxLength: 20),
                        mail = c.String(maxLength: 50),
                        linkAcc = c.String(maxLength: 20),
                        brokerCode = c.String(maxLength: 50),
                        state = c.Short(),
                        modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        secAcc = c.String(maxLength: 20),
                        fee = c.Decimal(precision: 18, scale: 4),
                        denchinPercent = c.Decimal(precision: 18, scale: 4),
                        contractFee = c.Decimal(precision: 18, scale: 4),
                        pozFee = c.Decimal(precision: 18, scale: 4),
                        memId = c.Int(),
                        bank = c.Int(nullable: false),
                        bankAccName = c.String(),
                        bankAccount = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminAccountDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        freezeValue = c.Decimal(precision: 18, scale: 4),
                        amount = c.Decimal(precision: 18, scale: 4),
                        assetId = c.Int(),
                        accountId = c.Long(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminAccount",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        memberid = c.Long(),
                        accNumber = c.String(maxLength: 16),
                        accountType = c.Short(),
                        LinkAccount = c.Long(),
                        modified = c.DateTime(),
                        mask = c.String(maxLength: 20),
                        startdate = c.DateTime(storeType: "date"),
                        enddate = c.DateTime(storeType: "date"),
                        state = c.Short(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminAssets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 16),
                        name = c.String(nullable: false, maxLength: 50),
                        volume = c.Int(nullable: false),
                        note = c.String(),
                        expireDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        state = c.Short(nullable: false),
                        modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ratio = c.Decimal(nullable: false, precision: 10, scale: 2),
                        price = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminBoards",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        type = c.Short(nullable: false),
                        tdays = c.String(nullable: false, maxLength: 128),
                        state = c.Short(nullable: false),
                        modified = c.DateTime(nullable: false),
                        description = c.String(nullable: false, maxLength: 1024),
                        dealType = c.Short(),
                        expTime = c.Time(precision: 7),
                        expDate = c.Short(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminCalendar",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        tdate = c.DateTime(nullable: false, storeType: "date"),
                        type = c.Short(nullable: false),
                        note = c.String(nullable: false, maxLength: 1024),
                        state = c.Short(nullable: false),
                        modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminContracts",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        securityId = c.Long(),
                        type = c.Short(),
                        code = c.String(maxLength: 16),
                        name = c.String(maxLength: 50),
                        lot = c.Decimal(precision: 18, scale: 6),
                        tickTable = c.Int(),
                        sdate = c.DateTime(storeType: "date"),
                        edate = c.DateTime(storeType: "date"),
                        groupId = c.Short(),
                        state = c.Short(),
                        modified = c.DateTime(),
                        mmorderLimit = c.Int(),
                        orderLimit = c.Int(),
                        refpriceParam = c.Decimal(precision: 18, scale: 6),
                        bid = c.Long(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminDeals",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        boardid = c.Short(),
                        dealno = c.String(maxLength: 16),
                        side = c.Short(),
                        memberid = c.Long(),
                        accountid = c.Long(),
                        assetid = c.Long(),
                        qty = c.Decimal(precision: 18, scale: 4),
                        price = c.Decimal(precision: 18, scale: 4),
                        totalPrice = c.Decimal(precision: 18, scale: 4),
                        state = c.Short(),
                        modified = c.DateTime(),
                        fee = c.Decimal(precision: 18, scale: 4),
                        m2m = c.Decimal(precision: 18, scale: 4),
                        refPrice = c.Decimal(precision: 18, scale: 4),
                        dealType = c.Short(),
                        day = c.Int(),
                        interests = c.Decimal(precision: 18, scale: 4),
                        toPay = c.Decimal(precision: 18, scale: 4),
                        connect = c.String(maxLength: 16),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminFee",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Value = c.Decimal(precision: 5, scale: 2),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminInterests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        interest = c.Decimal(precision: 18, scale: 4),
                        assetid = c.Int(),
                        repoInterset = c.Decimal(precision: 18, scale: 4),
                        loanInterset = c.Decimal(precision: 18, scale: 4),
                        maxValue = c.Decimal(precision: 18, scale: 4),
                        minValue = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminInvoiceDetails",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        invoiceid = c.Long(),
                        assetid = c.Int(),
                        qty = c.Decimal(precision: 18, scale: 4),
                        price = c.Decimal(precision: 18, scale: 6),
                        state = c.Short(),
                        modified = c.DateTime(),
                        note = c.String(),
                        dealNo = c.Long(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminInvoices",
                c => new
                    {
                        id = c.Long(nullable: false),
                        invoiceno = c.Long(nullable: false),
                        boardid = c.Long(),
                        dealno = c.Long(),
                        side = c.Short(),
                        accountid = c.Long(),
                        assetid = c.Long(),
                        dealType = c.Short(),
                        qty = c.Decimal(precision: 18, scale: 4),
                        totalPrice = c.Decimal(precision: 18, scale: 4),
                        state = c.Short(),
                        fee = c.Decimal(precision: 18, scale: 4),
                        modified = c.DateTime(),
                        invoicedate = c.DateTime(storeType: "date"),
                        expiredate = c.DateTime(storeType: "date"),
                        expiretime = c.Time(precision: 7),
                    })
                .PrimaryKey(t => new { t.id, t.invoiceno });
            
            CreateTable(
                "dbo.AdminMargins",
                c => new
                    {
                        contractId = c.Long(nullable: false, identity: true),
                        buy = c.Decimal(nullable: false, precision: 18, scale: 6),
                        sell = c.Decimal(nullable: false, precision: 18, scale: 6),
                        buytype = c.Short(nullable: false),
                        selltype = c.Short(nullable: false),
                        modified = c.DateTime(nullable: false),
                        mbuy = c.Decimal(nullable: false, precision: 18, scale: 6),
                        msell = c.Decimal(nullable: false, precision: 18, scale: 6),
                        coid = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.contractId);
            
            CreateTable(
                "dbo.AdminMarketMakers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contactid = c.Int(nullable: false),
                        memberid = c.Int(nullable: false),
                        accountid = c.Long(nullable: false),
                        startdate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        enddate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ticks = c.Int(nullable: false),
                        description = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        orderlimit = c.Int(nullable: false),
                        state = c.Short(nullable: false),
                        modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminMembers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.Short(),
                        code = c.String(nullable: false, maxLength: 2),
                        state = c.Short(),
                        modified = c.DateTime(),
                        partid = c.Long(),
                        mask = c.String(maxLength: 20),
                        startdate = c.DateTime(storeType: "date"),
                        enddate = c.DateTime(storeType: "date"),
                        broker = c.Boolean(nullable: false),
                        dealer = c.Boolean(nullable: false),
                        ander = c.Boolean(nullable: false),
                        nominal = c.Boolean(nullable: false),
                        linkMember = c.Int(),
                        name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Adminmtype",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        mtype = c.String(nullable: false, maxLength: 20),
                        minValue = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminOrder",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        boardId = c.Long(),
                        side = c.Short(),
                        memberid = c.Long(),
                        accountid = c.Long(),
                        assetid = c.Long(),
                        qty = c.Int(),
                        price = c.Decimal(precision: 18, scale: 4),
                        state = c.Short(),
                        modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        dealType = c.Short(),
                        connect = c.String(maxLength: 20),
                        day = c.Int(),
                        totSum = c.Decimal(precision: 18, scale: 4),
                        toPay = c.Decimal(precision: 18, scale: 4),
                        interests = c.Decimal(precision: 18, scale: 4),
                        fee = c.Decimal(precision: 18, scale: 4),
                        assetid2 = c.Long(),
                        qty2 = c.Int(),
                        price2 = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminReason",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                        description = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminRefPrice",
                c => new
                    {
                        contractId = c.Long(nullable: false),
                        refprice = c.Decimal(precision: 18, scale: 6),
                        modified = c.DateTime(),
                        name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.contractId);
            
            CreateTable(
                "dbo.AdminSecurities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        partid = c.Int(),
                        type = c.Short(),
                        code = c.String(maxLength: 16),
                        name = c.String(maxLength: 50),
                        totalQty = c.Int(),
                        firstPrice = c.Decimal(precision: 18, scale: 6),
                        intRate = c.Decimal(precision: 10, scale: 2),
                        sdate = c.DateTime(storeType: "date"),
                        edate = c.DateTime(storeType: "date"),
                        state = c.Short(),
                        modified = c.DateTime(),
                        assetId = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminSession",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        boardid = c.Long(),
                        name = c.String(maxLength: 50),
                        stime = c.Time(precision: 7),
                        duration = c.Int(),
                        algorithm = c.Short(),
                        match = c.Int(),
                        allowedtypes = c.Short(),
                        description = c.String(maxLength: 1024),
                        state = c.Short(),
                        modified = c.DateTime(),
                        isactive = c.Boolean(),
                        starttime = c.DateTime(storeType: "date"),
                        endtime = c.DateTime(storeType: "date"),
                        tduration = c.String(maxLength: 10, fixedLength: true),
                        matched = c.Long(),
                        editorder = c.Boolean(),
                        delorder = c.Boolean(),
                        markettype = c.Short(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminSpreads",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contractid = c.Int(),
                        sessionid = c.Int(),
                        rspread = c.Int(),
                        ispread = c.Int(),
                        rparam = c.Int(),
                        modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminTickSizeTable",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminTransaction",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        accountId = c.Long(),
                        type = c.Short(),
                        type1 = c.Short(),
                        amount = c.Int(),
                        assetId = c.Int(),
                        rate = c.Int(),
                        note = c.String(),
                        tdate = c.DateTime(storeType: "date"),
                        state = c.Short(),
                        modified = c.DateTime(),
                        userId = c.Long(),
                        memberid = c.Int(),
                        currency = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminTtable",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        arrangePrice = c.Decimal(precision: 18, scale: 4),
                        tickSize = c.Decimal(precision: 18, scale: 6),
                        userid = c.Long(),
                        assetid = c.Long(),
                        modified = c.DateTime(),
                        name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AdminUsers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        uname = c.String(nullable: false, maxLength: 30),
                        password = c.String(nullable: false, maxLength: 30),
                        modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        role = c.String(nullable: false, maxLength: 50),
                        memId = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.clearingDeal",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        boardid = c.Short(),
                        dealno = c.String(maxLength: 16),
                        side = c.Short(),
                        memberid = c.Int(),
                        accountid = c.Long(),
                        assetid = c.Int(),
                        qty = c.Decimal(precision: 18, scale: 4),
                        price = c.Decimal(precision: 18, scale: 4),
                        totalPrice = c.Decimal(precision: 18, scale: 4),
                        state = c.Short(),
                        modified = c.DateTime(storeType: "date"),
                        fee = c.Decimal(precision: 18, scale: 4),
                        m2m = c.Decimal(precision: 18, scale: 4),
                        refPrice = c.Decimal(precision: 18, scale: 4),
                        dealType = c.Short(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.deal",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        boardid = c.Short(),
                        side = c.Short(),
                        memberid = c.Int(),
                        accountid = c.Long(),
                        assetid = c.Int(),
                        qty = c.Decimal(precision: 18, scale: 4),
                        price = c.Decimal(precision: 18, scale: 4),
                        totalPrice = c.Decimal(precision: 18, scale: 4),
                        state = c.Short(),
                        modified = c.DateTime(),
                        fee = c.Decimal(precision: 18, scale: 4),
                        m2m = c.Decimal(precision: 18, scale: 4),
                        refPrice = c.Decimal(precision: 18, scale: 4),
                        dealType = c.Short(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.lastPrice",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        assetid = c.Int(),
                        ePrice = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.pozits",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        accNum = c.String(maxLength: 20),
                        side = c.String(maxLength: 20),
                        assetCode = c.String(maxLength: 20),
                        qty = c.Int(),
                        price = c.Decimal(precision: 18, scale: 4),
                        fee = c.Decimal(precision: 18, scale: 4),
                        gainLoss = c.Decimal(precision: 18, scale: 4),
                        callDenchin = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        account = c.String(maxLength: 20),
                        balance = c.Decimal(precision: 18, scale: 4),
                        remain = c.Decimal(precision: 18, scale: 4),
                        date = c.DateTime(),
                        pendingDay = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.transaction",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        accNum = c.String(maxLength: 20),
                        transType = c.Short(),
                        value = c.Decimal(precision: 18, scale: 4),
                        note = c.String(maxLength: 255),
                        side = c.Short(),
                        modified = c.DateTime(),
                        assetid = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.transaction");
            DropTable("dbo.Request");
            DropTable("dbo.pozits");
            DropTable("dbo.lastPrice");
            DropTable("dbo.deal");
            DropTable("dbo.clearingDeal");
            DropTable("dbo.AdminUsers");
            DropTable("dbo.AdminTtable");
            DropTable("dbo.AdminTransaction");
            DropTable("dbo.AdminTickSizeTable");
            DropTable("dbo.AdminSpreads");
            DropTable("dbo.AdminSession");
            DropTable("dbo.AdminSecurities");
            DropTable("dbo.AdminRefPrice");
            DropTable("dbo.AdminReason");
            DropTable("dbo.AdminOrder");
            DropTable("dbo.Adminmtype");
            DropTable("dbo.AdminMembers");
            DropTable("dbo.AdminMarketMakers");
            DropTable("dbo.AdminMargins");
            DropTable("dbo.AdminInvoices");
            DropTable("dbo.AdminInvoiceDetails");
            DropTable("dbo.AdminInterests");
            DropTable("dbo.AdminFee");
            DropTable("dbo.AdminDeals");
            DropTable("dbo.AdminContracts");
            DropTable("dbo.AdminCalendar");
            DropTable("dbo.AdminBoards");
            DropTable("dbo.AdminAssets");
            DropTable("dbo.AdminAccount");
            DropTable("dbo.AdminAccountDetails");
            DropTable("dbo.Account");
            DropTable("dbo.AccountDetails");
        }
    }
}
