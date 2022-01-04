using Data;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Data.Repositories.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;

        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> Add<T>(T entity)
        {
            _context.Add(entity);
            return SaveChanges();
        }

        public Task<bool> Delete<T>(T entity)
        {
            _context.Remove(entity);
            return SaveChanges();
        }

        public Task<bool> Edit<T>(T entity)
        {
            _context.Update(entity);
            return SaveChanges();
        }

        public async Task<Category> FindCategoryById(Guid Id)
        {
            var res = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            return res;
        }

        public async Task<Category> GetCategoryByName(string CatName)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == CatName);
        }

        public async Task<int> RowCount()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
