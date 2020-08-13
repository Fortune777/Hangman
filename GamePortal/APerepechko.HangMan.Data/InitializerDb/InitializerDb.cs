using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Data
{
    public class DbContextInitializer : CreateDatabaseIfNotExists<HangmanContext>
    {
        protected override void Seed(HangmanContext db)
        {

            #region спорт слова
            ThemesDb themes = new ThemesDb
            {
                Theme = "Cпорт"
            };

            List<WordsDb> words = new List<WordsDb>()
            {
                new WordsDb() { Word ="бег",ThemeId = themes },
                new WordsDb() { Word ="борьба",ThemeId = themes },
                new WordsDb() { Word ="бокс",ThemeId = themes },
                new WordsDb() { Word ="гимнастика",ThemeId = themes },
                new WordsDb() { Word ="гонка",ThemeId = themes },
                new WordsDb() { Word ="гребля",ThemeId = themes }
            };

            db.Themes.Add(themes);
            db.Words.AddRange(words);
            #endregion

            #region Горы слова
            themes = new ThemesDb
            {
                Theme = "Горы"
            };

            words = new List<WordsDb>()
            {
                new WordsDb() { Word ="альпы",ThemeId = themes },
                new WordsDb() { Word ="аннапурна",ThemeId = themes },
                new WordsDb() { Word ="нангапарбат",ThemeId = themes },
                new WordsDb() { Word ="манаслу",ThemeId = themes },
                new WordsDb() { Word ="дхаулагири",ThemeId = themes },
                new WordsDb() { Word ="чо ойю",ThemeId = themes },
                new WordsDb() { Word ="макалу",ThemeId = themes },
                new WordsDb() { Word ="лхоцзе",ThemeId = themes },
                new WordsDb() { Word ="канченджанга",ThemeId = themes },
                new WordsDb() { Word ="чогори",ThemeId = themes }
            };

            db.Themes.Add(themes);
            db.Words.AddRange(words);
            #endregion

            #region Животные слова
            themes = new ThemesDb
            {
                Theme = "Животные"
            };

            words = new List<WordsDb>()
            {
                new WordsDb() { Word ="бизон",ThemeId = themes },
                new WordsDb() { Word ="дельфин",ThemeId = themes },
                new WordsDb() { Word ="орёл",ThemeId = themes },
                new WordsDb() { Word ="пони",ThemeId = themes },
                new WordsDb() { Word ="омар",ThemeId = themes },
                new WordsDb() { Word ="корова",ThemeId = themes },
                new WordsDb() { Word ="обезьяна",ThemeId = themes },
                new WordsDb() { Word ="утка",ThemeId = themes },
                new WordsDb() { Word ="индейка",ThemeId = themes },
                new WordsDb() { Word ="лев",ThemeId = themes }
            };

            db.Themes.Add(themes);
            db.Words.AddRange(words);
            #endregion

            #region Страны слова
            themes = new ThemesDb
            {
                Theme = "Страны"
            };

            words = new List<WordsDb>()
            {
                new WordsDb() { Word ="норвегия",ThemeId = themes },
                new WordsDb() { Word ="франция",ThemeId = themes },
                new WordsDb() { Word ="австралия",ThemeId = themes },
                new WordsDb() { Word ="сша",ThemeId = themes },
                new WordsDb() { Word ="швеция",ThemeId = themes },
                new WordsDb() { Word ="япония",ThemeId = themes },
                new WordsDb() { Word ="германия",ThemeId = themes },
                new WordsDb() { Word ="великобритания",ThemeId = themes },
                new WordsDb() { Word ="канада",ThemeId = themes },
                new WordsDb() { Word ="швейцария",ThemeId = themes }
            };

            db.Themes.Add(themes);
            db.Words.AddRange(words);
            #endregion

            base.Seed(db);
        }
    }
}
