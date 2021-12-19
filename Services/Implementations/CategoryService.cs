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
    }
}
