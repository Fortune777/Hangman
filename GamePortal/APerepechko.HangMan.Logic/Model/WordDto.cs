﻿using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Validators;
using FluentValidation.Attributes;
using NullGuard;

namespace APerepechko.HangMan.Logic.Model
{
    [NullGuard(ValidationFlags.None)] // проверяем только аргументы методов на null в классе]
    [Validator(typeof(WordDtoValidators))]
    public class WordDto
    {
        public string WordId { get; set; }
        public string Word { get; set; }
        public string Theme { get; set; }
        public string RemainingLetters { get; set; } = string.Empty;
        public string SendChar { get; set; } = string.Empty;
        public bool IsWin { get; set; } = false;
        public bool HasChar { get; set; } = false;
    }
}
