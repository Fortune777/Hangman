using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
using FluentValidation;
using System.Linq;

namespace APerepechko.HangMan.Logic.Validators
{
    class UserDtoValidators : AbstractValidator<UserDto>
    {
        public UserDtoValidators()
        {
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.Id).NotNull().WithMessage("Field Theme is invalid");
                RuleFor(x => x.UserName).NotNull().MinimumLength(3).WithMessage("Field ThemeId is invalid");
            });
        }
    }
}
