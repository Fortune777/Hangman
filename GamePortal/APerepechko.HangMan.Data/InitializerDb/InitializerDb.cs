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
                new WordsDb() { Word ="Бег",ThemeId = themes },
                new WordsDb() { Word ="Борьба",ThemeId = themes },
                new WordsDb() { Word ="Бокс",ThemeId = themes },
                new WordsDb() { Word ="Гимнастика",ThemeId = themes },
                new WordsDb() { Word ="Гонка",ThemeId = themes },
                new WordsDb() { Word ="Гребля",ThemeId = themes }
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
                new WordsDb() { Word ="Альпы",ThemeId = themes },
                new WordsDb() { Word ="Аннапурна",ThemeId = themes },
                new WordsDb() { Word ="Нангапарбат",ThemeId = themes },
                new WordsDb() { Word ="Манаслу",ThemeId = themes },
                new WordsDb() { Word ="Дхаулагири",ThemeId = themes },
                new WordsDb() { Word ="Чо Ойю",ThemeId = themes },
                new WordsDb() { Word ="Макалу",ThemeId = themes },
                new WordsDb() { Word ="Лхоцзе",ThemeId = themes },
                new WordsDb() { Word ="Канченджанга",ThemeId = themes },
                new WordsDb() { Word ="Чогори",ThemeId = themes }
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
                new WordsDb() { Word ="Бизон",ThemeId = themes },
                new WordsDb() { Word ="Дельфин",ThemeId = themes },
                new WordsDb() { Word ="Орёл",ThemeId = themes },
                new WordsDb() { Word ="Пони",ThemeId = themes },
                new WordsDb() { Word ="Омар",ThemeId = themes },
                new WordsDb() { Word ="Корова",ThemeId = themes },
                new WordsDb() { Word ="Обезьяна",ThemeId = themes },
                new WordsDb() { Word ="Утка",ThemeId = themes },
                new WordsDb() { Word ="Индейка",ThemeId = themes },
                new WordsDb() { Word ="Лев",ThemeId = themes }
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
                new WordsDb() { Word ="Норвегия",ThemeId = themes },
                new WordsDb() { Word ="Франция",ThemeId = themes },
                new WordsDb() { Word ="Австралия",ThemeId = themes },
                new WordsDb() { Word ="США",ThemeId = themes },
                new WordsDb() { Word ="Швеция",ThemeId = themes },
                new WordsDb() { Word ="Япония",ThemeId = themes },
                new WordsDb() { Word ="Германия",ThemeId = themes },
                new WordsDb() { Word ="Великобритания",ThemeId = themes },
                new WordsDb() { Word ="Канада",ThemeId = themes },
                new WordsDb() { Word ="Швейцария",ThemeId = themes }
            };

            db.Themes.Add(themes);
            db.Words.AddRange(words);
            #endregion

            base.Seed(db);
        }
    }
}
