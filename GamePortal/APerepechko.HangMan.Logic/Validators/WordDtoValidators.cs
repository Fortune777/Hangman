using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
using FluentValidation;
using System.Linq;

namespace APerepechko.HangMan.Logic.Validators
{
    class WordDtoValidators : AbstractValidator<WordDto>
    {
        private readonly HangmanContext _context;
        public WordDtoValidators(HangmanContext context)
        {
            _context = context;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.WordId).NotNull().WithMessage("Field WordId is invalid");
                RuleFor(x => x.Word).NotNull().MinimumLength(3).WithMessage("Field Word is invalid");
                RuleFor(x => x.SendChar).NotNull().MaximumLength(1).WithMessage("Field SendChar is invalid");
                RuleFor(x => x.Theme).NotNull().MinimumLength(3).WithMessage("Field Theme is invalid");
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.Word).Must(CheckDuplicate).MaximumLength(1).WithMessage("Word is exist Database");
            });

        }

        private bool CheckDuplicate(string name)
        {
            return !_context.Words.AsNoTracking().Any(x => x.Word == name);
        }
    }
}
