using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
using FluentValidation;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace APerepechko.HangMan.Logic.Validators
{
    class UserStatisticsDtoValidators : AbstractValidator<UserStatisticsDto>
    {
        private readonly HangmanContext _context;
        public UserStatisticsDtoValidators(HangmanContext context)
        {
            _context = context;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.LossCount).NotNull().WithMessage("field LossCount is invalid");
                RuleFor(x => x.Score).NotNull().WithMessage("field Score is invalid");
                RuleFor(x => x.StatisticsId).NotNull().WithMessage("field StatisticsId is invalid");
                RuleFor(x => x.WinCount).NotNull().WithMessage("field WinCount is invalid");
                RuleFor(x => x.IsWin).NotNull().WithMessage("field WinCount is invalid");
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.LossCount).NotNull().WithMessage("field LossCount is invalid");
                RuleFor(x => x.Score).NotNull().WithMessage("field Score is invalid");
                RuleFor(x => x.StatisticsId).NotNull().WithMessage("field StatisticsId is invalid");
                RuleFor(x => x.WinCount).NotNull().WithMessage("field WinCount is invalid");
                RuleFor(x => x.IsWin).NotNull().WithMessage("field WinCount is invalid");
            });

        }
    }
}
