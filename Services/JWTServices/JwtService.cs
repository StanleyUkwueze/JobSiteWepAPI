using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.JWTServices
{
   public class JwtService :IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration configuration)
        {
            _config = configuration; 
        }
        public string GenerateJWTToken(AppUser user, List<string> userRoles)
        {
            //Adding user claims
            var Claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };
            foreach(var role in userRoles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Set up system security
            var SymmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(SymmetricSecurity, SecurityAlgorithms.HmacSha256)
            };
            //Create token
            var SecurityTokenHandler = new JwtSecurityTokenHandler();

            var token =  SecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return  SecurityTokenHandler.WriteToken(token);
        }
    }
}
