﻿using APerepechko.HangMan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APerepechko.HangMan.Logic.Model;
using System.Data.Entity;
using System.Runtime.InteropServices;
using APerepechko.HangMan.Model.Logic;
using AutoMapper;
using FluentValidation;
using CSharpFunctionalExtensions;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using JetBrains.Annotations;
using NullGuard;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Serilog;
using System.Data.Entity.Core;
using Ninject.Modules;

namespace APerepechko.HangMan.Logic.Services
{
    [NullGuard(ValidationFlags.Arguments)] // проверяем только аргументы методов на null в классе
    public class HangmanService : IHangmanService
    {
        private readonly HangmanContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public HangmanService([NotNull] HangmanContext context, [NotNull] IMapper mapper, [NotNull] ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }


        //тестирование
        public async Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                _logger.Information("Get all Users");
                return await _context.User.AsNoTracking().ProjectToArrayAsync<UserDto>(_mapper.ConfigurationProvider);
            }
            catch (SqlException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<IEnumerable<UserDto>>(ex.Message);
            }
        }


        public Result<UserStatisticsDto> UpdateStatistics(int id, [NotNull] UserStatisticsDto model)
        {
            _logger.Information("UpdateStatistics requested by anonymous");
            try
            {
                var dbModel = _mapper.Map<UserStatisticsDb>(model);
               _context.UserStatistics.Attach(dbModel);
                var entry = _context.Entry(dbModel);
                // global state
                entry.State = EntityState.Modified;

                //entry.Property(x => x.Name).IsModified = true; 
                //entry.Property(x => x.Price).IsModified = true;
                _context.SaveChanges(); //UPDATE
                return Result.Success(model);
            }
            catch (DbUpdateException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<UserStatisticsDto>(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<ThemeDto>>> GetAllThemesAsync()
        {
            _logger.Information("Get all Themes requested by anonymous");
            try
            {
                var models = await _context.Themes.AsNoTracking().ToArrayAsync();
                return Result.Success(_mapper.Map<IEnumerable<ThemeDto>>(models));
            }
            catch (SqlException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<IEnumerable<ThemeDto>>(ex.Message);
            }
        }

        public async Task<Result<Maybe<UserDto>>> GetUserByIdAsync(int id)
        {

           //неправильно работающий метод
           //, причем тут userstatics в методе userdto?

            _logger.Information("Get User By Id");
            try
            {
                Maybe<UserDto> updateUserStatistics = await _context.UserStatistics
                    .Where(x => x.StatisticsId == id)
                    .ProjectToSingleOrDefaultAsync<UserDto>(_mapper.ConfigurationProvider);

                _context.SaveChanges();
                return Result.Success(updateUserStatistics);
            }
            catch (DbUpdateException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<Maybe<UserDto>>(ex.Message);
            }
        }

        public async Task<Result<Maybe<WordDto>>> SelectWordsFromThemeAsync(int themeId)
        {
            _logger.Information("Select Words From Theme");
            try
            {
                var getIdTheme = await _context.Themes.AsNoTracking().FirstOrDefaultAsync(x => x.ThemeId == themeId);
                var allWords = await _context.Words
                    .AsNoTracking()
                    .Where(x => x.ThemeId.ThemeId == getIdTheme.ThemeId)
                    .Select(x => new WordDto() {
                        WordId = x.WordId.ToString(),
                        Word = x.Word,
                        Theme = x.ThemeId.Theme,
                        RemainingLetters = x.Word,
                        SendChar = string.Empty,
                        IsWin = false,
                        HasChar = false
                    }).ToArrayAsync();

                var rndNumb = new Random().Next(0, allWords.Count());
                Maybe<WordDto> result =allWords[rndNumb];

                return Result.Success(result);
            }
            catch (SqlException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<Maybe<WordDto>>(ex.Message);
            }
        }


        public ThemeDto AddTheme(ThemeDto themeDto)
        {
            _logger.Information("AddTheme");

            try
            {
                var theme = _mapper.Map<ThemesDb>(themeDto);
                _context.Themes.Add(theme);
                _context.SaveChanges();

            }
            catch (UpdateException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                // return Result.Failure<WordDto>(ex.Message);
            }

            return themeDto;
        }

        public  Result<WordDto> IsLetterExistWord(WordDto model)
        {
            _logger.Information("IsLetterExistWord");
            model.IsWin = false;
            model.HasChar = false;

            if (model.RemainingLetters.Contains(model.SendChar))
            {
                model.RemainingLetters = model.RemainingLetters.Replace(model.SendChar, string.Empty);
                model.HasChar = true;
            }

            if (model.RemainingLetters.Length == 0)
            {
                model.IsWin = true;
            }

            model.SendChar = null;

            return Result.Success(model);
        }

        public async Task<Result<WordDto>> GenerateRandomWordAsync()
        {
            //случайное слово 
            _logger.Information("GenerateRandomWord");
            try
            {
                var maxIdWord = await _context.Words.AsNoTracking().MaxAsync(x => x.WordId);
                int rdNumb = new Random().Next(1, maxIdWord);
                var result = await _context.Words.AsNoTracking().Where(x => x.WordId == maxIdWord)
                    .Select(x => new WordDto()
                    {
                        WordId = x.WordId.ToString(),
                        Word = x.Word,
                        Theme = x.ThemeId.Theme,
                        RemainingLetters = x.Word,
                        SendChar = string.Empty,
                        IsWin = false,
                        HasChar = false
                    }).FirstOrDefaultAsync();

                return Result.Success(result);
            }
            catch (SqlException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<WordDto>(ex.Message);
            }
        }


        public void Delete()
        {
            //ищем по id
            var model = _context.Words.Find(1);
            _context.Words.Remove(model);
            _context.SaveChanges();
        }

        #region Pattern Disposible
        private bool disposedValue;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                _context.Dispose();
                GC.SuppressFinalize(this);
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~HangmanService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        #endregion

    }
}
