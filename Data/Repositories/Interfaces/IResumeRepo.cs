using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteProject.Data;

namespace WebSiteAPI.Data.Repositories.Interfaces
{
   public interface IResumeRepo : ICRUDRepo
    {
        Task<bool> SaveChanges();
        Task<int> RowCount();
        Task<List<Resume>> GetResumes();
        Task<Resume> GetResumeByPublicId(string PublicId);
        Task<List<Resume>> GetResumesByUserId(string UserId);

    }
}
