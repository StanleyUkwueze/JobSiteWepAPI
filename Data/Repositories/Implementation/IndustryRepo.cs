using Data;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace WebSiteAPI.Data.Repositories.Implementation
{
    public class IndustryRepo : IIndustryRepo
    {
        private readonly AppDbContext _context;

        public IndustryRepo(AppDbContext context)
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

        public async Task<int> RowCount()
        {
            return await _context.Industries.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Industry> GetIndustryByName(string IndName)
        {
            var cat = await _context.Industries.FirstOrDefaultAsync(x => x.IndustryName == IndName);

            return cat;
        }

        public async Task<Industry> FindIndustryById(Guid Id)
        {
            var res = await _context.Industries.FirstOrDefaultAsync(x => x.Id == Id);
            return res;
        }
    }
}
