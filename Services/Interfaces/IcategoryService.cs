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
        Task<ResponseDto<Category>> AddCategory(CategoryDto model);
        Task<ResponseDto<Category>> UpdateCategoryById(Guid Id, CategoryDto CatToUpdate);
        Task<ResponseDto<Category>> DeleteCategoryById(Guid Id);
    }
}
