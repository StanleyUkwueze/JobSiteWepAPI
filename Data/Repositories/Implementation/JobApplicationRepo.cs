using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Data.Repositories.Interfaces;

namespace WebSiteAPI.Data.Repositories.Implementation
{
    public class JobApplicationRepo : IJobApplicationRepo
    {
        private readonly AppDbContext _context;

        public JobApplicationRepo(AppDbContext appDbContext)
        {
            _context = appDbContext;
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
        public async Task<List<AppUser>> JobApplications(Guid JobId)
        {

            var res = await _context.JobApplications.Where(x => x.JobId == JobId)
                .Include(x => x.AppUser).Select(x => x.AppUser).ToListAsync();
            return res;
        }

        public async Task<int> RowCount()
        {
            return await _context.JobApplications.CountAsync();
           
        }
        public async Task<bool> SaveChanges()
        {
          return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Job>> UserApplications(string UserId)
        {
            var res = await _context.JobApplications.Where(x => x.AppUserId == UserId).Include(x => x.Job).Select(x => x.Job).ToListAsync();
            return res;
        }
    }
}
