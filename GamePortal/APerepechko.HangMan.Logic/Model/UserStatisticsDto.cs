using APerepechko.HangMan.Logic.Validators;
using FluentValidation.Attributes;
 

namespace APerepechko.HangMan.Logic.Model
{
    [Validator(typeof(UserStatisticsDtoValidators))]
    public class UserStatisticsDto
    {
        public int StatisticsId { get; set; }
        public int WinCount { get; set; }
        public int LossCount { get; set; }
        public int Score { get; set; }
        public bool IsWin { get; set; }
    }
}
