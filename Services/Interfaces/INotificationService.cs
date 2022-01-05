using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models;

namespace WebSiteAPI.Services.Interfaces
{
   public interface INotificationService
    {
        Task SendEmailAsync(Message message);
    }
}
