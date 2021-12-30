﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpPost("add-Resume")]
        public async Task<IActionResult> AddResume([FromForm] ResumeUploadDto model, string userId)
        {
            //check if user logged is the one making the changes - only works for system using Auth tokens
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //if (!userId.Equals(currentUserId))
            //{
            //    ModelState.AddModelError("Denied", $"You are not allowed to upload resume for another user");
            //    var result2 = Util.BuildResponse<string>(false, "Access denied!", ModelState, "");
            //    return BadRequest(result2);
            //}

            var file = model.Resume;

            if (file.Length > 0)
            {
                var uploadStatus = await _resumeService.UploadResumeAsync(model, userId);

                if (uploadStatus.Item1)
                {
                    var res = await _resumeService.AddResumeAsync(model, userId);
                    if (!res.Item1)
                    {
                        ModelState.AddModelError("Failed", "Could not add resume to database");
                        return BadRequest(Util.BuildResponse<ImageUploadResult>(false, "Failed to add to database", ModelState, null));
                    }

                    return Ok(Util.BuildResponse<object>(true, "Uploaded successfully", null, new { res.Item2.publicId, res.Item2.url }));
                }

                ModelState.AddModelError("Failed", "File could not be uploaded to cloudinary");
                return BadRequest(Util.BuildResponse<ImageUploadResult>(false, "Failed to upload", ModelState, null));

            }

            ModelState.AddModelError("Invalid", "File size must not be empty");
            return BadRequest(Util.BuildResponse<ImageUploadResult>(false, "File is empty", ModelState, null));

        }

    }
}
