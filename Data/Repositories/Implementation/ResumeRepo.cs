using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Data.Repositories.Interfaces;

namespace WebSiteAPI.Data.Repositories.Implementation
{
    public class ResumeRepo : IResumeRepo
    {
        private readonly AppDbContext _context;

        public ResumeRepo(AppDbContext context)
        {
            _context = context;
        }
        public  Task<bool> Add<T>(T entity)
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

        public async Task<Resume> GetResumeByPublicId(string PublicId)
        {
            return await _context.Resumes.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.PublicId == PublicId);
        }

        public async Task<List<Resume>> GetResumes()
        {
            return await _context.Resumes.ToListAsync();
        }

        public async Task<List<Resume>> GetResumesByUserId(string UserId)
        {
            return await _context.Resumes.Where(x => x.AppUserId == UserId).ToListAsync();
        }

        public async Task<int> RowCount()
        {
            return await _context.Resumes.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
