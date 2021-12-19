using Data.Repositories.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;

namespace WebSiteAPI.Services.Implementations
{
   public class IndustryService : IIndustryService
    {
        private readonly IIndustryRepo _industryRepo;

        public IndustryService(IIndustryRepo industryRepo)
        {
            _industryRepo = industryRepo;
        }
        public async Task<ResponseDto<Industry>> Addindustry(IndustryDto model)
        {
            var industryToAdd = new Industry()
            {
                IndustryName = model.IndustryName
            };
            var res = await _industryRepo.Add(industryToAdd);
            var response = new ResponseDto<Industry>();
            if (res)
            {
                 response = new ResponseDto<Industry>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Message = "Industry Added Successfully!",
                    Data = industryToAdd
                };
            }
            else
            {
                response = new ResponseDto<Industry>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Message = "Industry Failed to be added!",
                    Data = null
                };
            }

            return response;
        }
    }
}
