using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteProject.Data;

namespace Data.Repositories.Interfaces
{
   public interface IIndustryRepo : ICRUDRepo
    {
        Task<bool> SaveChanges();
        Task<int> RowCount();
        public Task<Industry> GetIndustryByName(string IndName);
    }
}
