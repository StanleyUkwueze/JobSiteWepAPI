using Data.Repositories.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;

namespace WebSiteAPI.Services.Interfaces
{
    public class JobNatureservice : IJobNatureService
    {
        private readonly IJobNature _jobNatureRepo;

        public JobNatureservice(IJobNature jobNatureRepo)
        {
            _jobNatureRepo = jobNatureRepo;
        }
        public async Task<ResponseDto<JobNature>> AddJobNature(JobNatureDto model)
        {
            var jobNatureToAdd = new JobNature()
            {
                Name = model.Name,
                NewId = model.NewId
            };
            var res = await _jobNatureRepo.Add(jobNatureToAdd);
            var response = new ResponseDto<JobNature>();
            if (res)
            {
                response = new ResponseDto<JobNature>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Message = "New Job nature added successfully!",
                    Data = jobNatureToAdd
                };
            }
            else
            {
                response = new ResponseDto<JobNature>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Message = "oops! Failed to add new job natre",
                    Data = null
                };
            }
            return response;
           
        }
    }
}
