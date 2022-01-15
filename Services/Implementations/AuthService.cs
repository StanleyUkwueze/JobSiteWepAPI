using Microsoft.AspNetCore.Identity;
using Models;
using NETCore.MailKit.Core;
using Services.JWTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;

namespace WebSiteAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _signMgr;
        private readonly IJwtService _jwtService;
       

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService)
        {
            _userMgr = userManager;
            _signMgr = signInManager;
            _jwtService = jwtService;
        
        }
        public async Task<LoginCredDto> Login(string email, string password, bool rememberMe)
        {
            var user = await _userMgr.FindByEmailAsync(email);
            if(user == null)
            {
                return new LoginCredDto { Status = false };
            }
            var res = await _signMgr.PasswordSignInAsync(user, password, rememberMe, false);

            if (res == null)
            {
                return new LoginCredDto { Status = false };
            }
            //get jwt token

            var userRoles = await _userMgr.GetRolesAsync(user);
            var token =  _jwtService.GenerateJWTToken(user, userRoles.ToList());

            return new LoginCredDto { Status = true, Id = user.Id, token = token };
        }

       
    }
}
