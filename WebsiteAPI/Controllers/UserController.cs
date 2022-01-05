using AutoMapper;
using Commons.Helper;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Models;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;
using WepSiteAPI.Commons;

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;
        private readonly IMapper _mapper;
        private readonly INotificationService _notification;

        public UserController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, INotificationService notification)
        {
            _context = context;
            _userMgr = userManager;
            _roleMgr = roleManager;
            _mapper = mapper;
            _notification = notification;
        }

        [HttpPost("Add-User")]
        public async Task<IActionResult> AddUser(AddUserDto model)
        {
           
            // if user already exist return early
            var existingEmailUser = await _userMgr.FindByEmailAsync(model.Email);
            if (existingEmailUser != null)
            {
                ModelState.AddModelError("Invalid", $"User with email: {model.Email} already exists");
                return BadRequest(Util.BuildResponse<object>(false, "User already exists!", ModelState, null));
            }

            // map data from model to user
            //var user = _mapper.Map<AppUser>(model);

            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.Phonenumber,
                UserName = model.Email
            };
            var response = await _userMgr.CreateAsync(user, model.Password);

            if (!response.Succeeded)
            {
                foreach (var err in response.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Util.BuildResponse<string>(false, "Failed to add user!", ModelState, null));
            }

            var res = await _userMgr.AddToRoleAsync(user, "Applicant");

            if (!res.Succeeded)
            {
                foreach (var err in response.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Util.BuildResponse<string>(false, "Failed to add user role!", ModelState, null));
            }

            var token = await _userMgr.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfrimEmail", "User", new { Email = user.Email, Token = token }, Request.Scheme);
            //work remains

            string confirmEmailMessage = $"<p>Hello! {user.FirstName},<p>" + 
                $"<p>Before you can login, you need to verify your email using the link: " +
                $"<a href = '{url}'> Verify email <a> <p>";

            var message = new Message(new List<EmailConfiguration> { new EmailConfiguration { DisplayName = user.FirstName, From = "no-reoply@gmail.com", Address = user.Email }}, "Email confirmation", confirmEmailMessage);
            await _notification.SendEmailAsync(message);
            // map data to dto
            //var details = _mapper.Map<RegisterSuccessDto>(user);
            var details = new RegisterSuccessDto
            {
                UserId = Guid.Parse(user.Id),
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email
            };   
            return Ok(Util.BuildResponse(true, "New user added!", null, new { details, ConfimationLink = url }));


        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfrimEmail(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("Invalid", "UserId and token is required");
                return BadRequest(Util.BuildResponse<object>(false, "UserId or token is empty!", ModelState, null));
            }

            var user = await _userMgr.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("NotFound", $"User with email: {email} was not found");
                return NotFound(Util.BuildResponse<object>(false, "User not found!", ModelState, null));
            }

            var res = await _userMgr.ConfirmEmailAsync(user, token);
            if (!res.Succeeded)
            {
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(Util.BuildResponse<object>(false, "Failed to confirm email", ModelState, null));
            }

            return Ok(Util.BuildResponse<object>(true, "Email confirmation suceeded!", null, null));
        }

        [HttpGet("Get-User/email")]
        public async Task<IActionResult> GetUser(string email)
        {
            // map data from db to dto to reshape it and remove null fields
            var UserToReturn = new UserToReturnDto();
            //var user = await _userService.GetUser(email);
            var user = await _userMgr.FindByEmailAsync(email);
           
            if (user != null)
            {
                UserToReturn = new UserToReturnDto
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Email = user.Email
                };

                var res = Util.BuildResponse(true, "User details", null, UserToReturn);
                return Ok(res);
            }
            else
            {
                ModelState.AddModelError("Notfound", $"There was no record found for user with email {user.Email}");
                return NotFound(Util.BuildResponse<List<UserToReturnDto>>(false, "No result found!", ModelState, null));
            }

        }

        [HttpGet("Get-Users")]
        public IActionResult GetUsers(int pageNumber, int pageSize)
        {
            var listOfUsers = new List<UserToReturnDto>();
            var users =   _userMgr.Users.ToList();
            if(users != null)
            {
                var pagedList = PagedList<AppUser>.Paginate(users, pageNumber, pageSize);
                var result = pagedList.Data;
                foreach(var user in result)
                {
                    listOfUsers.Add(_mapper.Map<UserToReturnDto>(user));
                }
                var res = new PaginatedDto<UserToReturnDto>
                {
                    MetaData = pagedList.MetaData,
                    Data = listOfUsers
                };
                return Ok(Util.BuildResponse(true, "list of users", ModelState, res));
            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for users found!");
                var res = Util.BuildResponse<List<UserToReturnDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }

        [HttpDelete("delete-user/email")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userMgr.FindByEmailAsync(email);
            if(user != null)
            {
             var res =  await _userMgr.DeleteAsync(user);
                return Ok(res);
            }
            return null;
        }

    }
}
