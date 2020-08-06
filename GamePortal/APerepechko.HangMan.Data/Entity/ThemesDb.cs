using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APerepechko.HangMan.Data
{
    public class ThemesDb
    {
        public int ThemeId { get; set; }
        public string Theme { get; set; }
        public ICollection<WordsDb> Words { get; set; }
    }
}
