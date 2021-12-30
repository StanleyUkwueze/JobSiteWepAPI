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

        public async Task<List<Job>> GetJobByCategoryId(Guid Id)
        {
            var something = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);//.Select(x => x.Jobs).ToListAsync();
            var anothersomething = _context.Jobs.Where(x => x.CategoryId == Id);
            //something.Jobs;
            return await _context.Jobs.Where(x => x.CategoryId == Id).ToListAsync();
        }

       
        public async Task<List<Job>> GetJobByIndustryId( Guid Id)
        {
            var result = await _context.Industries.FirstOrDefaultAsync(x => x.Id == Id);
            var result2 = _context.Jobs.Where(x => x.IndustryId == Id);
            var res = await _context.Jobs.Where(x => x.IndustryId == Id).ToListAsync();
            return res;

        }

        public  async Task<List<Job>> GetJobByJobNatureId(Guid Id)
        {
            return await _context.Jobs.Where(x => x.LocationId == Id).ToListAsync();
        }

        public async Task<List<Job>> GetJobByLocationId(Guid Id)
        {
            return await _context.Jobs.Where(x => x.LocationId == Id).ToListAsync();
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
