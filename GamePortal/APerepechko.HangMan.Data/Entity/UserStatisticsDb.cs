using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Data
{
    public class UserStatisticsDb
    {
        public string StatisticsId { get; set; }
        public int WinCount { get; set; }
        public int LossCount { get; set; }
        public int Score { get; set; }
    }
}
