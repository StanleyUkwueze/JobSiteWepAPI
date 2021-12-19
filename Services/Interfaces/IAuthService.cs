using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;

namespace WebSiteAPI.Services.Interfaces
{
   public interface IAuthService
    {
        Task<LoginCredDto> Login(string email, string password, bool rememberMe);
    }
}
