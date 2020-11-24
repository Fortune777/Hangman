using Microsoft.AspNet.Identity.EntityFramework;
using Serilog;
using System.Data.Entity;

namespace APerepechko.HangMan.Data
{
    public class HangmanContext : IdentityDbContext
    {

        public HangmanContext() : base("HangmanCon")
        {
            Database.SetInitializer(new DbContextInitializer());
        }   

        public HangmanContext(ILogger logger) : base("HangmanCon")
        {
            Database.SetInitializer(new DbContextInitializer());
            Database.Log = msg => logger.Debug(msg);
        }


        public DbSet<ThemesDb> Themes { get; set; }
        public DbSet<WordsDb> Words { get; set; }
        public DbSet<UserStatisticsDb> UserStatistics { get; set; }


        //public DbSet<IdentityUserRole> UserRoles { get; set; }
        //public DbSet<IdentityUserClaim> Claims { get; set; }
        //public DbSet<IdentityUserLogin> Logins { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(typeof(HangmanContext).Assembly);
        }
 
    }
}
