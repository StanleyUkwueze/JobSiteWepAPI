using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteProject.Data;

namespace Data.Repositories.Interfaces
{
   public interface ILocationRepo : ICRUDRepo
    {
        Task<bool> SaveChanges();
        Task<int> RowCount();
    }
}
