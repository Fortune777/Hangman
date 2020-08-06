using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Model.Logic;
using CSharpFunctionalExtensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace APerepechko.HangMan.Logic.Services
{
    public interface IHangmanService : IDisposable
    {
        Result<IEnumerable<ThemeDto>> GetAllThemes();
        Result<WordDto> GenerateRandomWord();
        Result<WordDto> IsLetterExistWord(WordDto model);
        Result<Maybe<WordDto>> SelectWordsFromTheme(int themeId);

        Result<IEnumerable<UserDto>> GetAllUsers();

        Result<UserStatisticsDto> UpdateStatistics(UserStatisticsDto model);

        Result<Maybe<UserDto>> GetUserById(int id);



        //void Delete();
        // ThemeDto AddTheme(ThemeDto themeDto);
    }
}