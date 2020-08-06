using APerepechko.HangMan.Logic.Validators;
using FluentValidation.Attributes;
using NullGuard;

namespace APerepechko.HangMan.Logic.Model
{
    [NullGuard(ValidationFlags.None)] // проверяем только аргументы методов на null в классе]
    [Validator(typeof(ThemeDtoValidators))]
    public class ThemeDto
    {
        public int ThemeId { get; set; }
        public string Theme { get; set; }
    }
}
