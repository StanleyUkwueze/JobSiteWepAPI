using Data;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Data.Repositories.Implementation
{
    public class LocationRepo : ILocationRepo
    {
        private readonly AppDbContext _context;

        public LocationRepo(AppDbContext context)
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
         return await _context.Locations.CountAsync();
          
        }

        public async Task<bool> SaveChanges()
        {
          return await  _context.SaveChangesAsync() > 0;
        }
    }
}
