using APerepechko.HangMan.Data;
using APerepechko.HangMan.Logic.Model;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using JetBrains.Annotations;
using NullGuard;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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


        /// здесь получаем статистику по юзеру по id
        //public async Task<Result<Maybe<UserDto>>> GetUserByIdAsync(string id)
        //{
        //    _logger.Information("Get User By Id");
        //    try
        //    {
        //        var user = await _context.UserStatistics.AsNoTracking()
        //            .CountAsync(x => x.UserId == id);

        //        if (user == 0)
        //        {
        //            return Result.Failure<Maybe<UserDto>>("id пользователя не найден");
        //        }

        //        Maybe<UserDto> result = _mapper.Map<UserDto>(user);

        //        return Result.Success(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error("Connection to db is failed", ex);
        //        return Result.Failure<Maybe<UserDto>>(ex.Message);
        //    }
        //}

        public async Task<Result<Maybe<WordDto>>> SelectWordsFromThemeAsync(int themeId)
        {
            _logger.Information("Select Words From Theme");
            try
            {
                var getIdTheme = await _context.Themes.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ThemeId == themeId);
                var allWords = await _context.Words
                    .AsNoTracking()
                    .Where(x => x.ThemeId.ThemeId == getIdTheme.ThemeId)
                    .Select(x => new WordDto()
                    {
                        WordId = x.WordId.ToString(),
                        Word = x.Word,
                        Theme = x.ThemeId.Theme,
                        RemainingLetters = x.Word,
                        SendChar = string.Empty,
                        IsWin = false,
                        HasChar = false
                    }).ToArrayAsync();

                var rndNumb = new Random().Next(0, allWords.Count());
                Maybe<WordDto> result = allWords[rndNumb];

                return Result.Success(result);
            }
            catch (SqlException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<Maybe<WordDto>>(ex.Message);
            }
        }


        ////тестирование
        //public async Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync()
        //{
        //    try
        //    {
        //        _logger.Information("Get all Users");
        //        return await _context.User.AsNoTracking().ProjectToArrayAsync<UserDto>(_mapper.ConfigurationProvider);
        //    }
        //    catch (SqlException ex)
        //    {
        //        _logger.Error("Connection to db is failed", ex);
        //        return Result.Failure<IEnumerable<UserDto>>(ex.Message);
        //    }
        //}


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

        public Task<Result<UserStatisticsDto>> GetStatisticsByUser(int id, UserStatisticsDto model)
        {
            throw new NotImplementedException();
        }


        public ThemeDto AddTheme(ThemeDto themeDto)
        {
            _logger.Information("AddTheme");

            try
            {
                var theme = _mapper.Map<ThemesDb>(themeDto);
                _context.Themes.Add(theme);
                _context.Users.Include(x => x.Claims);
                _context.Users.Include(x => x.Logins);
                _context.Users.Include(x => x.Roles);


                _context.SaveChanges();

            }
            catch (UpdateException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                // return Result.Failure<WordDto>(ex.Message);
            }

            return themeDto;
        }

        public Result<WordDto> IsLetterExistWord(WordDto model)
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
                var maxId = await _context.Words.AsNoTracking().MaxAsync(x => x.WordId);
                var minId = await _context.Words.AsNoTracking().MinAsync(x => x.WordId);
                int rdNumb = new Random().Next(minId, maxId);
                var result = _context.Words.AsNoTracking().Where(x => x.WordId == rdNumb)
                    .Select(x => new WordDto()
                    {
                        WordId = x.WordId.ToString(),
                        Word = x.Word,
                        Theme = x.ThemeId.Theme,
                        RemainingLetters = x.Word,
                        SendChar = string.Empty,
                        IsWin = false,
                        HasChar = false
                    }).FirstOrDefault();

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
