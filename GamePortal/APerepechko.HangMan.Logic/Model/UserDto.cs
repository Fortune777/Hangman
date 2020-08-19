using APerepechko.HangMan.Logic.Validators;
using FluentValidation.Attributes;
using NullGuard;

namespace APerepechko.HangMan.Logic.Model
{
    [NullGuard(ValidationFlags.None)] // проверяем только аргументы методов на null в классе]
    [Validator(typeof(UserDtoValidators))]
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
