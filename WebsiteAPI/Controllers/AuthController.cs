using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.JWTServices;
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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, UserManager<AppUser> userManager, IJwtService jwtService )
        {
            _authService = authService;
            _userManager = userManager;
            _jwtService = jwtService;
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("Invalid", "Credentials provided but the user is invalid");
                return BadRequest(Util.BuildResponse<object>(false, "Invalid credentials", ModelState, null));
            }

            // check if user's email is confirmed
            if(await _userManager.IsEmailConfirmedAsync(user))
            {
                var result = await _authService.Login(model.Email, model.Password, model.Rememberme);

                if (!result.Status)
                {
                    ModelState.AddModelError("Invalid", "Credentials provided but the user is invalid");
                    return BadRequest(Util.BuildResponse<object>(false, "Invalid credentials", ModelState, null));
                }
                return Ok(Util.BuildResponse(true, "Login is sucessful!", null, result));
            }
            ModelState.AddModelError("Invalid", "User must first confirm email before attempting to login");
            return BadRequest(Util.BuildResponse<object>(false, "Email not confirmed", ModelState, null));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfrimEmail(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("Invalid", "UserId and token is required");
                return BadRequest(Util.BuildResponse<object>(false, "UserId or token is empty!", ModelState, null));
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("NotFound", $"User with email: {email} was not found");
                return NotFound(Util.BuildResponse<object>(false, "User not found!", ModelState, null));
            }

            var res = await _userManager.ConfirmEmailAsync(user, token);
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
    }
}
