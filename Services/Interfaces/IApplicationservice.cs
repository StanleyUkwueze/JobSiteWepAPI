using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;

namespace WebSiteAPI.Services.Interfaces
{
    public interface IApplicationservice
    {
        Task<ApplicationResponseDto> ApplyForJob(string userId, Guid JobId);
        Task<List<UserApplicationDto>> JobApplications( Guid JobId);
        Task<List<JobApplicationDto>> UserApplications(string userId);
    }
}
