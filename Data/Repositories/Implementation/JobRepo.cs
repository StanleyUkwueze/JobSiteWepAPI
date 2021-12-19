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
            _context.Remove(entity);
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

        public async Task<List<Job>> GetJobByIndustryId(Industry industry)
        {
            var res = await _context.Jobs.Where(x => x.IndustryId == industry.Id).ToListAsync();
            return res;

        }

        public async Task<List<Job>> GetJobByLocation(LocationDto location)
        {
            var result = await _context.Jobs.Where(x => x.LocationId == location.NewId).ToListAsync();
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
