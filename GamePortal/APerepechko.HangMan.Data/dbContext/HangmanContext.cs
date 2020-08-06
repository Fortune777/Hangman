using APerepechko.HangMan.Data.dbContext;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;

using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
 


namespace APerepechko.HangMan.Data
{
    public class HangmanContext : DbContext
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
        public DbSet<UserDb> User { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(typeof(HangmanContext).Assembly);
        }
    }
}
