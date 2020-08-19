using APerepechko.HangMan.Logic.Validators;
using FluentValidation.Attributes;
using NullGuard;


namespace APerepechko.HangMan.Logic.Model
{
    [NullGuard(ValidationFlags.None)] // проверяем только аргументы методов на null в классе]
    [Validator(typeof(NewUserDtoValidators))]
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
