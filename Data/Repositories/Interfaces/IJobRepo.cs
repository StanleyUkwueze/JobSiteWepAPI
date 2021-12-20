﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteProject.Data;

namespace Data.Repositories.Interfaces
{
   public interface IJobRepo : ICRUDRepo
    {
        Task<bool> SaveChanges();
        Task<int> RowCount();
        public Task<Job> FindJobById(Guid Id);
        public Task<List<Job>> GetAllJobs();
        public Task<List<Job>> GetJobByIndustryId(Guid Id);
        public Task<List<Job>> GetJobByIndustryName(string IndName);
        public Task<List<Job>> GetJobByCategoryName(string CatName);
        public Task<List<Job>> GetJobBySalary(decimal salary);
        public Task<List<Job>> GetJobByLocation(string location);
        
    }
}
