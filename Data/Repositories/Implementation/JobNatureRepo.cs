using Data;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Data.Repositories.Implementation
{
    public class JobNatureRepo : IJobNature
    {
        private readonly AppDbContext _context;

        public JobNatureRepo(AppDbContext context)
        {
            _context = context;
        }
        public Task<bool> Add<T>(T entity)
        {
            try
            {
                _context.Add(entity);

            }catch(DbException dbex)
            {
                throw new Exception(dbex.Message);
            }
            return SaveChanges();
        }

        public Task<bool> Delete<T>(T entity)
        {
            try
            {
                _context.Remove(entity);
            }catch(DbException dbex)
            {
                throw new Exception(dbex.Message);
            }
            return SaveChanges();
        }

        public Task<bool> Edit<T>(T entity)
        {
            try
            {
                _context.Update(entity);
            }catch(DbException dbexc)
            {
                throw new Exception(dbexc.Message);
            }
            return SaveChanges();
        }

        public async Task<JobNature> GetJobNatureName(string JobNatureName)
        {
            return await _context.JobNatures.FirstOrDefaultAsync(x => x.Name == JobNatureName);
        }

        public async Task<int> RowCount()
        {

            return await _context.JobNatures.CountAsync();
        }
        
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
