using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Add-Category")]
        public async Task<IActionResult> AddCategory(CategoryDto category)
        {
            var res = await _categoryService.AddCategory(category);
            return Ok(res);
        }

        [HttpPut("Update-Category-By-id/id")]
        public async Task<IActionResult> UpdateCaregoryById(Guid id, CategoryDto cat)
        {
            var res = await _categoryService.UpdateCategoryById(id, cat);
            return Ok(res);
        }

        [HttpDelete("Delete-Category-by-Id/id")]
        public async Task<IActionResult> DeleteCategoryById(Guid id)
        {
            var res = await _categoryService.DeleteCategoryById(id);
            return Ok(res);
        }
    }
}
