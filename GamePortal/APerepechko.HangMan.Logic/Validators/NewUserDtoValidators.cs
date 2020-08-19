using APerepechko.HangMan.Logic.Model;
using FluentValidation;

namespace APerepechko.HangMan.Logic.Validators
{
    class NewUserDtoValidators : AbstractValidator<NewUserDto>
    {
        public NewUserDtoValidators()
        {
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.UserName).NotNull().MinimumLength(3).WithMessage("Field UserName is invalid");
                RuleFor(x => x.Password).NotNull().MinimumLength(3).WithMessage("Field Password is invalid");
                RuleFor(x => x.Email).NotNull().MinimumLength(3).WithMessage("Field Email is invalid");
            });
        }
    }
         
}
