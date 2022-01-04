using System;
using System.Collections.Generic;
using System.Text;
using WebSiteProject.Data;
using System.Threading.Tasks;
using Models;

namespace Data.Repositories.Interfaces
{
    public interface ICategoryRepo : ICRUDRepo
    {
        Task<bool> SaveChanges();
        Task<int> RowCount();
        public Task<Category> GetCategoryByName(string CatName);
        Task<Category> FindCategoryById(Guid Id);
    }
}
