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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthService authService, UserManager<AppUser> userManager )
        {
            _authService = authService;
            _userManager = userManager;
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
    }
}
