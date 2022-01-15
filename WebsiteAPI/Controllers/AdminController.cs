using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    //[Authorize(Roles = "Admin")]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly ILocationservice _locationservice;
        private readonly object _industryService;
        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _signMgr;

        public AdminController(IJobService jobService, ILocationservice locationservice, 
            IIndustryService industryService, UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            _jobService = jobService;
            _locationservice = locationservice;
            _industryService = industryService;
            _userMgr = userManager;
            _signMgr = signInManager;
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

        [HttpPut("Activate-User")]
        public async Task<IActionResult> ActivateUser(string UserEmail)
        {
            var user = await _userMgr.FindByEmailAsync(UserEmail);
            //var res = _signMgr.SignInAsync(user, false);
            if(user == null)
            {
                ModelState.AddModelError("Invalid", "No user with the provided email");
                Util.BuildResponse<string>(false, "Invalid user email", ModelState, null);
            }
            if (user.IsActive != true) user.IsActive = true;
           await _userMgr.UpdateAsync(user);
                  

            return Ok(Util.BuildResponse<object>(true, "User Successfully activated", ModelState, null));

        }

        [HttpPut("Deactivate-User")]
         public async Task<IActionResult> DeactivateUser(string email)
        {
            var user = await _userMgr.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("Invalid", "No user with the provided email");
                Util.BuildResponse<string>(false, "Invalid user email", ModelState, null);
            }
            if (user.IsActive != false) user.IsActive = false;

            return Ok(Util.BuildResponse<object>(true, "User Successfully Deactivated", ModelState, null));

        }

  
    }
}
