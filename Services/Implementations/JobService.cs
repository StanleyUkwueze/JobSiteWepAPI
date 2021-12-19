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

        public Task<Job> FindJobById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetAllJobs()
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJobByIndustryId(Industry industry)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJobByLocation(LocationDto location)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJobBySalary(decimal salary)
        {
            throw new NotImplementedException();
        }
    }
}
