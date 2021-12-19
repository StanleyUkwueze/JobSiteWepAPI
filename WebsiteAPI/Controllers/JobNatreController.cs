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
    public class JobNatureController : ControllerBase
    {
        private readonly IJobNatureService _jobNatureService;

        public JobNatureController(IJobNatureService jobNatureService)
        {
            _jobNatureService = jobNatureService;
        }

        [HttpPost("Add-Job-nature")]

        public async Task<IActionResult> AddJobNature(JobNatureDto model)
        {
            var result = await _jobNatureService.AddJobNature(model);
            return Ok(result);
        }
    }
}
