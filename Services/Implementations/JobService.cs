using AutoMapper;
using Data.Repositories.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        private readonly IIndustryRepo _industryRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILocationRepo _locationRepo;
        private readonly IJobNature _jobNature;

        public JobService(IJobRepo jobRepo, IMapper mapper, ILocationRepo locationRepo, 
            IIndustryRepo industryRepo, ICategoryRepo categoryRepo, IJobNature jobNature )
        {
            _jobRepo = jobRepo;
            _mapper = mapper;
            _industryRepo = industryRepo;
            _categoryRepo = categoryRepo;
            _locationRepo = locationRepo;
            _jobNature = jobNature;
        }
        public async Task<ResponseDto<Job>> AddJob(JobDto model)
        {
        // return await  _mapper.Map<Job>(model);

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
                JobValidDays = model.JobValidDays

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

        public async Task<List<JobToReturnDto>> GetJobByCategoryName(string CatName)
        {
            var count = await _jobRepo.RowCount();
            if(count > 0) { 
            var res = await _categoryRepo.GetCategoryByName(CatName);
            var jobs = await _jobRepo.GetJobByCategoryId(res.Id);

            var listofJobs = new List<JobToReturnDto>();  
            foreach (var job in jobs)
            {
                    var result = _mapper.Map<JobToReturnDto>(job);
                    listofJobs.Add(result);   
            }
            return listofJobs;
            }
            return null;
        }
        public async Task<List<JobToReturnDto>> GetJobByIndustryName(string IndName)
        {
            var count = await _industryRepo.RowCount();
            var listOfJobs = new List<JobToReturnDto>();
            if (count > 0)
            {
                try
                {
                    var res = await _industryRepo.GetIndustryByName(IndName);
                    if(res != null) {
                        var jobs = await _jobRepo.GetJobByIndustryId(res.Id);                      
                        foreach (var job in jobs)
                        {
                            var result = _mapper.Map<JobToReturnDto>(job); 
                            listOfJobs.Add(result);
                        }
                    }     
                }catch(DbException dbex)
                {
                    throw new Exception(dbex.Message);
                }
                return listOfJobs;
            }
            return null;
        }

        public async Task<List<JobToReturnDto>> GetJobBySalary(decimal salary)
        {
            var count = await _jobRepo.RowCount();
            var listOfJobs = new List<JobToReturnDto>();
            if (count > 0)
            {
                try
                {
                    var res = await _jobRepo.GetJobBySalary(salary);
                    if (res != null)
                    {
                        foreach (var job in res)
                        {
                            var result = _mapper.Map<JobToReturnDto>(job);
                            listOfJobs.Add(result);
                        }
                    }
                }
                catch (DbException dbex)
                {
                    throw new Exception(dbex.Message);
                }

                return listOfJobs;
            }
            return null;
        }

        public async Task<List<JobToReturnDto>> GetJobByLocationName(string locName)
        {
            var count = await _locationRepo.RowCount();
            var listOfJobs = new List<JobToReturnDto>();
            if(count > 0)
            {
                try
                {
                    var res = await _locationRepo.GetLocationByName(locName);
                  if(res != null)
                    {
                        var jobs = await _jobRepo.GetJobByLocationId(res.Id);
                        var jobToReturn = new JobToReturnDto();
                        foreach(var job in jobs)
                        {
                            var result = _mapper.Map<JobToReturnDto>(job);
                            listOfJobs.Add(result);
                        }
                    }
                }
                catch(DbException dbex)
                {
                    throw new Exception(dbex.Message);
                }

                return listOfJobs;
            }
            return null;
           
      
           
        }

        public async Task<List<JobToReturnDto>> GetJobByJobNatureName(string JobNature)
        {
            var count = await _jobNature.RowCount();
            var listOfJobs = new List<JobToReturnDto>();
            if(count > 0)
            {
                try
                {
                    var res = await _jobNature.GetJobNatureName(JobNature);
                    if(res != null)
                    {
                        var jobs = await _jobRepo.GetJobByJobNatureId(res.Id);
                        foreach(var job in jobs)
                        {
                            var result = _mapper.Map<JobToReturnDto>(job);

                            listOfJobs.Add(result);
                        }
                    }
                }catch(DbException dbexc)
                {
                    throw new Exception(dbexc.Message);
                }

                return listOfJobs;
            }
            return null;
           
            
        }

        public async Task<Job> UpdateJobById(Guid Id, JobToEditDto job)
        {

           var jobToEdit = await _jobRepo.FindJobById(Id);
            
            if (jobToEdit!= null)
            {
                //jobToEdit = _mapper.Map<Job>(job);      
                jobToEdit.Title = job.Title;
                jobToEdit.Description = job.Description;
                jobToEdit.MaximumSalary = job.MaximumSalary;
                jobToEdit.MinimumSalary = job.MinimumSalary;
                jobToEdit.Company = job.Company;

                var res = await _jobRepo.Edit(jobToEdit);
                if (res)
                    return jobToEdit;
            }
            return null;

        }
    }
}
