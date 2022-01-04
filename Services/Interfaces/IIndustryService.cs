using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;

namespace WebSiteAPI.Services.Interfaces
{
   public interface IIndustryService
    {
        Task<ResponseDto<Industry>> Addindustry(IndustryDto model);
        Task<ResponseDto<Industry>> UpdateIndustryById(Guid Id, IndustryDto model);
        Task<ResponseDto<Industry>> DeleteIndustryById(Guid Id);
    }
}
