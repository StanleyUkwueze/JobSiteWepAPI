using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using WebSiteAPI.Models.Dtos;

namespace WepSiteAPI.Commons.Helper
{
   public class AutoMapperProfile : Profile
    {
   

        public AutoMapperProfile()
        {
            CreateMap<AppUser, AddUserDto>();
            CreateMap<JobDto, Job>();
            CreateMap<CategoryDto, Category>();
        }

        
    }
}
