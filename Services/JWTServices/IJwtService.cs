using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.JWTServices
{
   public interface IJwtService
    {
        public string GenerateJWTToken(AppUser user, List<string> userRoles);
    }
}
