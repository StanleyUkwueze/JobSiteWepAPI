using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;

namespace WebSiteAPI.Services.Interfaces
{
   public interface ICategoryService
    {
        public Task<ResponseDto<Category>> AddCategory(CategoryDto model);
    }
}
