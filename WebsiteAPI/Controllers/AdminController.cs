using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly ILocationservice _locationservice;
        private readonly object _industryService;

        public AdminController(IJobService jobService, ILocationservice locationservice, IIndustryService industryService)
        {
            _jobService = jobService;
            _locationservice = locationservice;
            _industryService = industryService;
        }

        [HttpPost("Add-Job")]

        public async Task<IActionResult> AddJob(JobDto model)
        {
            var res = await _jobService.AddJob(model);
            return Ok(res);

        }

        [HttpDelete("Delete-Job-By-Id/{Id}")]
        public async Task<IActionResult> DeleteJobById(Guid Id)
        {
            return Ok(await _jobService.DeleteJobById(Id));
        }

        [HttpPut("Update-Job/Id")]
        public async Task<IActionResult> UpdateJobById(Guid Id, JobToEditDto job)
        {
            var res = await _jobService.UpdateJobById(Id, job);
            return Ok(res);
        }

        [HttpPost("Add-Job-Location")]

        public async Task<IActionResult> AddLocation(LocationDto location)
        {
            var result = await _locationservice.AddJobLocation(location);
            return Ok(result);
        }

       
    }
}
