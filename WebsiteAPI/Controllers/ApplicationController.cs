using Commons.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
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
        private readonly UserManager<AppUser> _userMgr;

        public ApplicationController(IApplicationservice applicationService, IResumeService resumeService, UserManager<AppUser> userManager)
        {
            _applicationService = applicationService;
            _resumeService = resumeService;
            _userMgr = userManager;
        }

        [HttpPost("Apply-for-Job")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> ApplyForJob(string userId, Guid JobId)
        {
            ClaimsPrincipal currentUser = this.User;
            var curentuserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!userId.Equals(curentuserId))
            {
                ModelState.AddModelError("Denied", $"You are not allowed to apply for job for another user");
                var result2 = Util.BuildResponse<string>(false, "Access denied!", ModelState, "");
                return BadRequest(result2);
            }

            var res = await _applicationService.ApplyForJob(userId, JobId);
            if (res == null)
            {
                ModelState.AddModelError("Failed", $"You do not have your resume uploaded");
                var result2 = Util.BuildResponse<string>(false, "Action Failed", ModelState, "");
                return BadRequest(result2);
            }
            return Ok(Util.BuildResponse<object>(true, "Applied for job successfully", ModelState, res));
        }

        [HttpGet("Jobs-applied-For")]
        [Authorize]
        public async Task<IActionResult> UserApplications(string userId, int page, int pageSize)
        {

            var user = await _userMgr.FindByIdAsync(userId);
            var userRole = await _userMgr.IsInRoleAsync(user, "Admin");
            if (!userRole)
            {
                ClaimsPrincipal currentuser = this.User;
                var currentuserId = currentuser.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (!userId.Equals(currentuserId))
                {
                    ModelState.AddModelError("Denied", $"You are not allowed to apply for job for another user");
                    var result2 = Util.BuildResponse<string>(false, "Access denied!", ModelState, "");
                    return BadRequest(result2);
                }
            }
            var jobs = await _applicationService.UserApplications(userId);
            if(jobs.Count < 1)
            {
                ModelState.AddModelError("Empty", $"User with the Id: {userId} has not applied for any job");
                var result2 = Util.BuildResponse<string>(false, "Empty application record!", ModelState, "");
                return BadRequest(result2);
            }
            var pagedList = PagedList<JobApplicationDto>.Paginate(jobs, page, pageSize);
            var response = new PaginatedDto<JobApplicationDto>
            {
                MetaData = pagedList.MetaData,
                Data = pagedList.Data
            };
            return Ok(Util.BuildResponse<Object>(true, "Jobsapplied gotten successfully", ModelState, response));
        }

        [HttpGet("User-Applications")]
        [Authorize]
        public async Task<IActionResult> JobApplications(Guid jobId, int page, int pageSize)
        {
            var applicants = await _applicationService.JobApplications(jobId);
            if(applicants.Count < 1)
            {
                var pagedList = PagedList<UserApplicationDto>.Paginate(applicants, page, pageSize);
                var response = new PaginatedDto<UserApplicationDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = pagedList.Data
                };
                ModelState.AddModelError("Empty", "None has applied for this job");
                var result2 = Util.BuildResponse<string>(false, "Empty application record!", ModelState, "");
                return BadRequest(result2);
            }
            return Ok(Util.BuildResponse<Object>(true, "Applicants gotten successfully", ModelState, applicants));
        }

    }
}
 