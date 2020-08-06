using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Data
{
    public class WordsDb
    {
        public int WordId { get; set; }
        public string Word { get; set; }
        public virtual ThemesDb ThemeId { get; set; }

    }
}
