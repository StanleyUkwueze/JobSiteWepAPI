using Data;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using System.Linq;
using System.Data.Common;

namespace WebSiteAPI.Data.Repositories.Implementation
{
    public class JobRepo : IJobRepo
    {
        private readonly AppDbContext _context;

        public JobRepo(AppDbContext context)
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
            _context.Update(entity);
            return SaveChanges();
        }

        public async Task<Job> FindJobById(Guid Id)
        {
            var res = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == Id);
            return res;

        }

        public async Task<List<Job>> GetAllJobs()
        {
            var result = await _context.Jobs.ToListAsync();
            return result;
        }

        public async Task<List<Job>> GetJobByCategoryName(string CatName)
        {
            return await _context.Jobs.Where(x => x.CategoryName == CatName).ToListAsync();
        }

        public async Task<List<Job>> GetJobByIndustryId( Guid Id)
        {
            var res = await _context.Jobs.Where(x => x.IndustryId == Id).ToListAsync();
            return res;

        }

        public async Task<List<Job>> GetJobByIndustryName(string IndName)
        {
            return await _context.Jobs.Where(x => x.IndustryName == IndName).ToListAsync();
        }

        public async Task<List<Job>> GetJobByLocation(string location)
        {
            var result = await _context.Jobs.Where(x => x.LocationName == location).ToListAsync();
            return result;
        }

        public async Task<List<Job>> GetJobBySalary(decimal salary)
        {
            return await _context.Jobs.Where(x => x.MinimumSalary >= salary).ToListAsync();
        }

        public async Task<int> RowCount()
        {
            return await _context.Jobs.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
         return await _context.SaveChangesAsync() > 0;
        }
    }
}
