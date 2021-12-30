using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Commons.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Data.Repositories.Interfaces;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;

namespace WebSiteAPI.Services.Implementations
{
    public class ResumeService : IResumeService
    {
        private readonly UserManager<AppUser> _userMgr;
        private readonly IResumeRepo _resumeRepo;
        private readonly IOptions<CloudinarySettings> _config;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public ResumeService(IOptions<CloudinarySettings> config, UserManager<AppUser> userManager, IMapper mapper, IResumeRepo resumeRepo)
        {
            _userMgr = userManager;
            _resumeRepo = resumeRepo;
            _config = config;
            _mapper = mapper;
            var account = new Account( config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);

        }
        public async Task<Tuple<bool, ResumeUploadDto>> AddResumeAsync(ResumeUploadDto model, string userId)
        {
            var user = await _userMgr.Users.Include(x => x.Resumes).FirstOrDefaultAsync(x => x.Id == userId);
            var resume = _mapper.Map<Resume>(model);
            resume.AppUserId = userId;
            resume.Title = model.title;
           
            
            

            var result = await _resumeRepo.Add(resume);
            return new Tuple<bool, ResumeUploadDto>(result, model);
        }

        public async Task<Tuple<bool, ResumeUploadDto>> UploadResumeAsync(ResumeUploadDto model, string userId)
        {
            var uploadResult = new ImageUploadResult();

            using (var stream = model.Resume.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(model.Resume.FileName, stream),
                    //Transformation = new Transformation().Width(500).Height(600).Gravity("face").Crop("fill")
                };
                 uploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            }
            var status = uploadResult.StatusCode.ToString();
            if (status.Equals("OK"))
            {
                model.publicId = uploadResult.PublicId;
                model.url = uploadResult.Url.ToString();
                return new Tuple<bool, ResumeUploadDto>(true, model);
            }
            return new Tuple<bool, ResumeUploadDto>(false, model);
        }
    }
}
