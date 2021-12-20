using AutoMapper;
using Data.Repositories.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;

namespace WebSiteAPI.Services.Implementations
{
   public class JobService : IJobService
    {
        private readonly IJobRepo _jobRepo;
        private readonly IMapper _mapper;

        public JobService(IJobRepo jobRepo, IMapper mapper )
        {
            _jobRepo = jobRepo;
            _mapper = mapper;
        }
        public async Task<ResponseDto<Job>> AddJob(JobDto model)
        {


            var job = new Job()
            {
                JobNatureId = model.JobNatureId,
                LocationId = model.LocationId,
                Description = model.Description,
                MinimumSalary = model.MinimumSalary,
                MaximumSalary = model.MaximumSalary,
                Title = model.Title,
                IndustryId = model.IndustryId,
                CategoryId = model.CategoryId,
                Company = model.Company,
                IndustryName = model.IndustryName,
                CategoryName = model.CategoryName,
                LocationName = model.LocationName,
                JobNatureName = model.JobNatureName
               
            };

            var res = await _jobRepo.Add(job);
            var response = new ResponseDto<Job>();
            if (res)
            {
                response = new ResponseDto<Job>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Message = "Job added successfully!",
                    Data = job
                };
            }
            else
            {
                response = new ResponseDto<Job>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Message = "Job failed to add!",
                    Data = null
                };
            }

            return response;
        }

        public async Task<bool> DeleteJobById(Guid Id)
        {
            var jobTodelete = await _jobRepo.FindJobById(Id);
            if (jobTodelete == null) return false;
            return await _jobRepo.Delete(jobTodelete);
        }

        public async Task<Job> FindJobById(Guid Id)
        {
         return await _jobRepo.FindJobById(Id);
       
        }

        public  async Task<List<Job>> GetAllJobs()
        {
            return await _jobRepo.GetAllJobs();
        }

        public async Task<List<Job>> GetJobByCategoryName(string CatName)
        {
            return await  _jobRepo.GetJobByCategoryName(CatName);
        }

        public async Task<List<Job>> GetJobByIndustryId(Guid Id)
        {
            return await _jobRepo.GetJobByIndustryId(Id);
        }

        public async Task<List<Job>> GetJobByIndustryName(string IndName)
        {
            return await _jobRepo.GetJobByIndustryName(IndName);
        }

        public async Task<List<Job>> GetJobByLocation(string location)
        {
            return await _jobRepo.GetJobByLocation(location);
        }

        public async Task<List<Job>> GetJobBySalary(decimal salary)
        {
            return await _jobRepo.GetJobBySalary(salary);
        }
    }
}
