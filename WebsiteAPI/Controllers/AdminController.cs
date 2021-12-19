using AutoMapper;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WepSiteAPI.Commons;

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;
        private readonly IMapper _mapper;

        public AdminController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            _userMgr = userManager;
            _roleMgr = roleManager;
            _mapper = mapper;
        }

        [HttpPost("Add-User")]
        public async Task<IActionResult> AddUser(AddUserDto model)
        {
            //var user = new AppUser
            //{
            // FirstName = model.FirstName,
            // LastName =  model.LastName,
            // Email = model.Email,
            // UserName = model.Email
            //};

            //var res = await  _userMgr.CreateAsync(user);
            //return Ok(res);

            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.Phonenumber
            };

            var result = await _userMgr.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                    return BadRequest(Util.BuildResponse<string>(false, "Failed to add user", ModelState, null));

                };
            }

            var res = await _userMgr.AddToRoleAsync(user, "Applicant");

            if (!res.Succeeded)
            {
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                    return BadRequest(Util.BuildResponse<string>(false, "Failed to add user role", ModelState, null));
                };
            }

            var details = new RegisterSuccessDto
            {
                UserId = Guid.Parse(user.Id.ToString()),
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email

            };
            return Ok(Util.BuildResponse<RegisterSuccessDto>(true, "New user added successfully!", null, details));
        }
    }
}
