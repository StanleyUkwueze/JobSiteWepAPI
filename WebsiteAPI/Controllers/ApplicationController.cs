using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebSiteAPI.Services.Interfaces;
using WepSiteAPI.Commons;

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationservice _applicationService;
        private readonly IResumeService _resumeService;

        public ApplicationController(IApplicationservice applicationService, IResumeService resumeService)
        {
            _applicationService = applicationService;
            _resumeService = resumeService;
        }

        [HttpPost("Apply-for-Job")]
        public async Task<IActionResult> ApplyForJob(string userId, string JobId)
        {
            ClaimsPrincipal currentUser = this.User;
            var curentuserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!userId.Equals(curentuserId))
            {
                ModelState.AddModelError("Denied", $"You are not allowed to apply for job for another user");
                var result2 = Util.BuildResponse<string>(false, "Access denied!", ModelState, "");
                return BadRequest(result2);
            }

            var res = await _resumeService.GetUserResumesAsync(userId);
            if (res == null)
            {
                ModelState.AddModelError("Failed", $"You do not have your resume uploaded");
                var result2 = Util.BuildResponse<string>(false, "Action Failed", ModelState, "");
                return BadRequest(result2);
            }
            return Ok(Util.BuildResponse<object>(true, "Applied for job successfully", ModelState, res));
        }

        [HttpGet("Jobs-applied-For")]
        public async Task<IActionResult> UserApplications(string userId)
        {
            var jobs = await _applicationService.UserApplications(userId);
            if(jobs.Count < 1)
            {
                ModelState.AddModelError("Empty", $"User with the Id: {userId} has not applied for any job");
                var result2 = Util.BuildResponse<string>(false, "Empty application record!", ModelState, "");
                return BadRequest(result2);
            }
            return Ok(Util.BuildResponse<Object>(true, "Jobsapplied gotten successfully", ModelState, jobs));
        }

        [HttpGet("User-Applications")]
        public async Task<IActionResult> JobApplications(Guid jobId)
        {
            var applicants = await _applicationService.JobApplications(jobId);
            if(applicants.Count < 1)
            {
                ModelState.AddModelError("Empty", $"None has applied for this job");
                var result2 = Util.BuildResponse<string>(false, "Empty application record!", ModelState, "");
                return BadRequest(result2);
            }
            return Ok(Util.BuildResponse<Object>(true, "Applicants gotten successfully", ModelState, applicants));
        }

    }
}
 