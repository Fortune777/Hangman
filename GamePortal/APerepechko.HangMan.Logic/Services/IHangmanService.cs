using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Model.Logic;
using CSharpFunctionalExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Logic.Services
{
    public interface IHangmanService : IDisposable
    {
        Task<Result<IEnumerable<ThemeDto>>> GetAllThemesAsync();
        Task<Result<WordDto>> GenerateRandomWordAsync();
        Result<WordDto> IsLetterExistWord(WordDto model);
        Task<Result<Maybe<WordDto>>> SelectWordsFromThemeAsync(int themeId);

        Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync();

        Result<UserStatisticsDto> UpdateStatistics(int id, UserStatisticsDto model);

        Task<Result<Maybe<UserDto>>> GetUserByIdAsync(int id);
        



        //void Delete();
        // ThemeDto AddTheme(ThemeDto themeDto);
    }
}