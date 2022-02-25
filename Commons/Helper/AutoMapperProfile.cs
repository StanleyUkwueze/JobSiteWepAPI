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
            CreateMap<AppUser, AddJobDto>();
            CreateMap<Job, JobDto>();
            CreateMap<Job, JobToReturnDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<AppUser, RegisterSuccessDto>();
            CreateMap<ResumeUploadDto, Resume>();
            CreateMap<JobToEditDto, Job>();
            CreateMap<AppUser, UserToReturnDto>();
        }

        
    }
}
