using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteProject.Data;

namespace WebSiteAPI.Data.Repositories.Interfaces
{
   public interface IJobApplicationRepo : ICRUDRepo 
    {
        Task<bool> SaveChanges();
        Task<int> RowCount();
        Task<List<AppUser>> JobApplications(Guid JobId);
        Task<List<Job>> UserApplications(string UserId);
    }
}
