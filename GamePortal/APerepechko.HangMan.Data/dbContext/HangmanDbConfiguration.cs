 
using System.Data.Entity.ModelConfiguration;

namespace APerepechko.HangMan.Data.dbContext
{
   

    internal class WordsDbConfiguration : EntityTypeConfiguration<WordsDb>
    {
        public WordsDbConfiguration()
        {
            HasKey(x => x.WordId).Property(x => x.Word).IsRequired().HasMaxLength(20);
            HasOptional(x => x.ThemeId).WithMany(p => p.Words).Map(m => m.MapKey("ThemeId"));
        }
    }

    internal class ThemesDbConfiguration : EntityTypeConfiguration<ThemesDb>
    {
        public ThemesDbConfiguration()
        {
            HasKey(x => x.ThemeId).Property(x => x.Theme).IsRequired().HasMaxLength(15);
        }
    }

    //internal class UserDbConfiguration : EntityTypeConfiguration<UserDb>
    //{
    //    public UserDbConfiguration()
    //    {
    //        HasKey(x => x.UserId).HasRequired(c => c.UserStatisticsId).WithRequiredPrincipal(p => p.UserId);
    //    }
    //}

    internal class UserStatisticsDbConfiguration : EntityTypeConfiguration<UserStatisticsDb>
    {
        public UserStatisticsDbConfiguration()
        {
            HasKey(x => x.StatisticsId);

            HasKey(x => x.StatisticsId).Property(x => x.WinCount).IsRequired();
            Property(x => x.LossCount).IsRequired();
            Property(x => x.Score).IsRequired();
            Property(x => x.StatisticsId).IsRequired();
        }
    }


}
