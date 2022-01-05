using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;

namespace WebSiteAPI.Services.Interfaces
{
   public interface IResumeService
    {
        public Task<Tuple<bool, ResumeUploadDto>> UploadResumeAsync(ResumeUploadDto model, string userId);
        public Task<Tuple<bool, ResumeUploadDto>> AddResumeAsync(ResumeUploadDto model, string userId);
        public Task<List<Resume>> GetUserResumesAsync(string userId);
    }
}
