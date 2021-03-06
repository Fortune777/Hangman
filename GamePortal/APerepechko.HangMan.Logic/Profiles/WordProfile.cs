﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APerepechko.HangMan.Logic.Model;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;

namespace APerepechko.HangMan.Data.Profiles
{
    class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<WordsDb, WordDto>().ReverseMap();
        }
    }

    class ThemeProfile : Profile
    {
        public ThemeProfile()
        {
            CreateMap<ThemesDb, ThemeDto>().ReverseMap();
        }
    }

    class UserStatisticsProfile : Profile
    {
        public UserStatisticsProfile()
        {
            CreateMap<UserStatisticsDb, UserStatisticsDto>().ReverseMap();
        }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserDto>();
        }
    }

    //public class UserDbDto : Profile
    //{
    //    public UserDbDto()
    //    {
    //        CreateMap<UserDto, UserDb>()
    //            .ForMember(dest =>dest.UserId, opt =>opt.MapFrom(src=>src.Id))
    //            .ForMember(dest =>dest.UserName, opt =>opt.MapFrom(src=>src.UserName))
    //            .ReverseMap();
    //    }
    //}

}
