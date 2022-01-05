using AutoMapper;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Data.Repositories.Interfaces;
using WebSiteAPI.Models;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;

namespace WebSiteAPI.Services.Implementations
{
    public class ApplicationService : IApplicationservice
    {
        private readonly IJobApplicationRepo _jobApplicationRepo;
        private readonly IJobRepo _jobRepository;
        private readonly IResumeRepo _resumeUpload;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public ApplicationService(IJobApplicationRepo jobApplicationRepo, IJobRepo jobRepository, IResumeRepo resumeUpload, 
            IMapper mapper, UserManager<AppUser> userManager)
        {
            _jobApplicationRepo = jobApplicationRepo;
            _jobRepository = jobRepository;
            _resumeUpload = resumeUpload;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<ApplicationResponseDto> ApplyForJob(string userId, Guid jobId)
        {

            var checkJob = await _jobRepository.FindJobById(jobId);
            if (checkJob == null)
            {
                return new ApplicationResponseDto { Success = false, Report = "Invalid Job Id" };
            }
            if(DateTime.Parse(checkJob.Deadline) < DateTime.Now)
            {
                return new ApplicationResponseDto
                {
                    Success = false, Report = "Job Advert is already closed"
                };
            }
            //if (DateTime.Parse(checkJob.Deadline) < DateTime.Now)
            //{
            //    return new ApplicationResponseDto { Success = false, Report = "Job Advert is already closed" };
            //}
            var checkCv = _resumeUpload.GetResumesByUserId(userId);
            if (checkCv == null)
            {
                return new ApplicationResponseDto { Success = false, Report = "You need to upload your Cv to your dashboard" };
            }
            try
            {
                var applplication = new JobApplication { AppUserId = userId, JobId = jobId };
                var res = await _jobApplicationRepo.Add(applplication);
                if (res)
                    return new ApplicationResponseDto { Success = true, Report = "Application successfully submitted" };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<List<UserApplicationDto>> JobApplications(Guid jobId)
        {
            var checkJob = await _jobRepository.FindJobById(jobId);
            var listofApplicants = new List<UserApplicationDto>();
            if (checkJob != null)
            {
                try
                {
                    var users = await _jobApplicationRepo.JobApplications(jobId);
                    if (users != null)
                    {
                        foreach (var user in users)
                        {
                            listofApplicants.Add(_mapper.Map<UserApplicationDto>(user));
                        }
                        return listofApplicants;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<List<JobApplicationDto>> UserApplications(string userId)
        {
            var checkUser = await _userManager.FindByIdAsync(userId);
            var listofJobs = new List<JobApplicationDto>();
            if (checkUser != null)
            {
                try
                {
                    var jobs = await _jobApplicationRepo.UserApplications(userId);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listofJobs.Add(_mapper.Map<JobApplicationDto>(job));
                        }
                        return listofJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }
    }
}
