using Commons.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Models;
using Services.JWTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models;
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
        private readonly INotificationService _notify;

        public AuthController(IAuthService authService, UserManager<AppUser> userManager, 
            IJwtService jwtService, INotificationService notificationService )
        {
            _authService = authService;
            _userManager = userManager;
            _jwtService = jwtService;
            _notify = notificationService;
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

        [HttpPost("ForgotPasswored")]
        public async Task<IActionResult> ForgotPassord(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("Invalid", "No matching user found");
                return BadRequest(Util.BuildResponse<object>(false, "No matching user found!", ModelState, null));
            }
            var Token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var ResetPasswordLink = Url.Action("ResetPassword", "Auth",
                new { email = user.Email, token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(Token)) }, Request.Scheme);

            string ResetPasswordMessage = $"<p>Hello! {user.FirstName},<p>" +
               $"<p>Please, click the following link to reset your password: " +
               $"<a href = '{ResetPasswordLink}'> Reset Password<a> <p>";

            var message = new Message(new List<EmailConfiguration> { new EmailConfiguration { DisplayName = user.FirstName, From = "no-reoply@gmail.com", Address = user.Email } }, "Email confirmation", ResetPasswordMessage);
            await _notify.SendEmailAsync(message);

            return Ok(Util.BuildResponse<Object>(true, "Successfull", ModelState, new { url = ResetPasswordLink}));
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassworedDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token)), model.Password);
                if (!result.Succeeded)
                {
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError("Failed", err.Description);
                    }
                    return Ok(Util.BuildResponse<object>(false, "Failed to reset password for the user", ModelState, null));
                }
               
            }
            return Ok(Util.BuildResponse<object>(true, "Successfully reset password for the user", ModelState, ""));

        }
        [HttpGet("Gen-JWT")]
        public  IActionResult GenerateToken()
        {
            var user = new AppUser
            {
               LastName = "stanley",
               FirstName = "Jekwu"
              
            };
            var roles = new List<string> { "Admin", "Applicant" };
            var token = _jwtService.GenerateJWTToken(user, roles);
            return Ok(token);
        }
    }
}
