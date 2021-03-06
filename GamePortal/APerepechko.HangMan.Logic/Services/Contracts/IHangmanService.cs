﻿using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
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
        Result<UserStatisticsDto> UpdateStatistics(int id, UserStatisticsDto model);
        Task<Result<UserStatisticsDto>> GetStatisticsByUser(int id, UserStatisticsDto model);

        // Task<Result<Maybe<UserDto>>> GetUserByIdAsync(string id);


        // Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync();

        //void Delete();
        // ThemeDto AddTheme(ThemeDto themeDto);
    }
}