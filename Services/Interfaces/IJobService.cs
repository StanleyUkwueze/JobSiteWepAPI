using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;

namespace WebSiteAPI.Services.Interfaces
{
   public interface IJobService
    {
        public Task<ResponseDto<Job>> AddJob(JobDto model);
        public Task<Job> FindJobById(Guid Id);
        public Task<List<Job>> GetAllJobs();
        public Task<bool> DeleteJobById(Guid Id);
        public Task<Job> UpdateJobById(Guid Id, JobToEditDto job);
        public Task<List<JobToReturnDto>> GetJobByIndustryName(string IndName);
        public Task<List<JobToReturnDto>> GetJobByCategoryName(string CatName);
        public Task<List<JobToReturnDto>> GetJobBySalary(decimal salary);
        public Task<List<JobToReturnDto>> GetJobByJobNatureName(string JobNature);
        public Task<List<JobToReturnDto>> GetJobByLocationName(string locName);
    }
}
