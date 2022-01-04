using AutoMapper;
using Data.Repositories.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;
using WepSiteAPI.Commons;


namespace WebSiteAPI.Services.Implementations
{
   public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper )
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<ResponseDto<Category>> AddCategory(CategoryDto model)
        {
            var category = new Category()
            {
                CategoryName = model.CategoryName
            };
          
          var result = await _categoryRepo.Add(category);
            var responseDto = new ResponseDto<Category>();
            if (result)
            {

                 responseDto = new ResponseDto<Category>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Data = category,
                    Message = "category added successfully!"
                };

            }
            else
            {
                responseDto = new ResponseDto<Category>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Data = null,
                    Message = "Failed to add category!"
                };
            }

            return responseDto;
        }

        public async Task<ResponseDto<Category>> DeleteCategoryById(Guid Id)
        {
            var response = new ResponseDto<Category>();
            var catToDelete = await _categoryRepo.FindCategoryById(Id);
            var res = await _categoryRepo.Delete(catToDelete);
            if (res)
            {
                response = new ResponseDto<Category>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Message = "Category deleted successfully",
                    Data = null
                };
                return response;
            }
            else
            {
                response = new ResponseDto<Category>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Message = "Category Failed to delete",
                    Data = catToDelete
                };
                return response;
            }
        }

        public async Task<ResponseDto<Category>> UpdateCategoryById(Guid Id, CategoryDto CatToUpdate)
        {
            var response = new ResponseDto<Category>();
            var catToEdit = await _categoryRepo.FindCategoryById(Id);
            if (catToEdit != null) catToEdit.CategoryName = CatToUpdate.CategoryName;
            var res = await _categoryRepo.Edit(catToEdit);
            if (res)
            {
                response = new ResponseDto<Category>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Message = "Category Updated successfully",
                    Data = catToEdit
                };
                return response;
            }
            else
            {
                response = new ResponseDto<Category>
                {
                    IsSuccessful = false,
                    Errors = {},
                    Message = "Category Failed to update",
                    Data = null
                };
              return  response;
            }
        }
    }
}
