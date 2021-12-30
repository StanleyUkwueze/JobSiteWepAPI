using AutoMapper;
using Commons.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;
using WepSiteAPI.Commons;

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet("Find-Job-by-Id/{Id}")]
        public async Task<IActionResult> FindJobById(Guid Id)
        {
            var res = await _jobService.FindJobById(Id);
            return Ok(res);
        }

        [HttpGet("Get-All-Jobs")]
        public async Task<IActionResult> GetAllJobs(int pageNumber, int pageSize)
        {
            var listOfJobsToReturn = new List<JobToReturnDto>();
            var jobs = await _jobService.GetAllJobs();

            if(jobs != null)
            {
                var pagedList = PagedList<Job>.Paginate(jobs, pageNumber, pageSize);
                var result = pagedList.Data;

                foreach(var job in result)
                {
                    listOfJobsToReturn.Add(_mapper.Map<JobToReturnDto>(job));
                }
                var res = new PaginatedDto<JobToReturnDto>
                {
                 MetaData = pagedList.MetaData,
                 Data = listOfJobsToReturn
                };

                return Ok(Util.BuildResponse(true, "list of jobs", ModelState, res));
            }
            else
            {
                ModelState.AddModelError("NotFound", "No job record found");
                var response = Util.BuildResponse<string>(false, "No job record found", ModelState, null);
                return NotFound(response);
            }
         
        }

        [HttpGet("Get-Job-By-Industry-Name/Indname")]
        public async Task<IActionResult> GetJobByIndustryName(string IndName)
        {
            var job = await _jobService.GetJobByIndustryName(IndName);
            return Ok(job);
        }

        [HttpGet("Get-Job-By-Category/{CatName}")]
        public async Task<IActionResult> GetJobByCategoryName(string CatName)
        {
            var job = await _jobService.GetJobByCategoryName(CatName);
            return Ok(job);
        }

        [HttpGet("Get-Job-By-Nature/{jobnature}")]
        public async Task<IActionResult> GetJobByNature(string jobnature)
        {
            var job = await _jobService.GetJobByJobNatureName(jobnature);
            return Ok(job);
        }
        [HttpGet("Get-Job-By-salary")]
        public async Task<IActionResult> GetJobBySalary(decimal salary)
        {
           
            var job = await _jobService.GetJobBySalary(salary); 
            return Ok(job);
        }

        [HttpGet("Get-Job-By-Location/{LocatioName}")]
        public async Task<IActionResult> GetJobByLocation(string location)
        {
            var job = await _jobService.GetJobByLocationName(location);
            return Ok(job);
        }
       
        [HttpPut("Update-Job/Id")]
        public async Task<IActionResult> UpdateJobById(Guid Id, JobToEditDto job)
        {
            var res = await _jobService.UpdateJobById(Id, job);
            return Ok(res);
        }

    }
}
