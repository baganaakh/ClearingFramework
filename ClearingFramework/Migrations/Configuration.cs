namespace ClearingFramework.Migrations
{
    using ClearingFramework.dbBind;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClearingFramework.dbBind.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Model1";
            AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(ClearingFramework.dbBind.Model1 context)
        {
            //  This method will be called after migrating to the latest version.
            context.AdminUsers.AddOrUpdate(x => x.id,
                new AdminUser() { id = 1, uname = "baganaakh", password = "baganaakh", role = "done" , memId = 1}
                );
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
