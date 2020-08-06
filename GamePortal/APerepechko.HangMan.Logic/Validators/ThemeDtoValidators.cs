using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
using FluentValidation;
using System.Linq;

namespace APerepechko.HangMan.Logic.Validators
{
    class ThemeDtoValidators : AbstractValidator<ThemeDto>
    {
        private readonly HangmanContext _context;
        public ThemeDtoValidators(HangmanContext context)
        {
            _context = context;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.Theme).NotNull().MinimumLength(3).WithMessage("Field Word is invalid");
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.Theme).Must(CheckDuplicate).MinimumLength(1).WithMessage("Theme is exist Database");
            });

        }

        private bool CheckDuplicate(string name)
        {
            return !_context.Words.AsNoTracking().Any(x => x.Word == name);
        }
    }
}
