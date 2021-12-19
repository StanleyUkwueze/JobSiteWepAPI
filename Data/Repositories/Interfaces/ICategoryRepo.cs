using System;
using System.Collections.Generic;
using System.Text;
using WebSiteProject.Data;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICategoryRepo : ICRUDRepo
    {
        Task<bool> SaveChanges();
        Task<int> RowCount();
    }
}
