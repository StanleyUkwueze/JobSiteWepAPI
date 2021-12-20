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

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("Add-Job")]
        
        public async Task<IActionResult> AddJob(JobDto model)
        {
          var res =  await _jobService.AddJob(model);
            return Ok(res);
           
        }

        [HttpGet("Find-Job-by-Id/{Id}")]
        public async Task<IActionResult> FindJobById(Guid Id)
        {
            var res = await _jobService.FindJobById(Id);
            return Ok(res);
        }

        [HttpGet("Get-All-Jobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var availableJobs = await _jobService.GetAllJobs();
            return Ok(availableJobs);
        }

        [HttpGet("Get-Jobs-By-Industry-Id/{Id}")]
        public async Task<IActionResult> GetJobsByIndustryId(Guid Id)
        {
            var jobs = await _jobService.GetJobByIndustryId(Id);
            return Ok(jobs);
        }

        [HttpGet("Get-Job-By-Industry-Name/{Ind-name}")]
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

        [HttpGet("Get-Job-By-salary")]
        public async Task<IActionResult> GetJobBySalary(decimal salary)
        {
            var job = await _jobService.GetJobBySalary(salary);
            return Ok(job);
        }

        [HttpGet("Get-Job-By-Location/{LocatioName}")]
        public async Task<IActionResult> GetJobByLocation(string location)
        {
            var job = await _jobService.GetJobByLocation(location);
            return Ok(job);
        }
        [HttpDelete("Delete-Job-By-Id/{Id}")]
        public async Task<IActionResult> DeleteJobById(Guid Id)
        {
            return Ok(await _jobService.DeleteJobById(Id));
        }

    }
}
