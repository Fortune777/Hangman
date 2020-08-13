namespace APerepechko.HangMan.Data.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<APerepechko.HangMan.Data.HangmanContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "APerepechko.HangMan.Data.HangmanContext";
        }

        protected override void Seed(APerepechko.HangMan.Data.HangmanContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            if (context.Roles.Any()) return;

            context.Roles.Add(new IdentityRole("user"));
            context.SaveChanges();

        }
    }
}
