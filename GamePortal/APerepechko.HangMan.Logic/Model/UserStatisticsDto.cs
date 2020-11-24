using APerepechko.HangMan.Logic.Validators;
using FluentValidation.Attributes;
using NullGuard;


namespace APerepechko.HangMan.Logic.Model
{
    [NullGuard(ValidationFlags.None)] // проверяем только аргументы методов на null в классе]
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
