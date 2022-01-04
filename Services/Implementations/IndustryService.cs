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

        public async Task<ResponseDto<Industry>> DeleteIndustryById(Guid Id)
        {
            var response = new ResponseDto<Industry>();
            var indToDelete = await _industryRepo.FindIndustryById(Id);
            var res = await _industryRepo.Delete(indToDelete);
            if (res)
            {
                response = new ResponseDto<Industry>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Message = $"{indToDelete.IndustryName} industry with Id: {indToDelete.Id} was deleted successfully ",
                    Data = null
                };
                return response;
            }
            else
            {
                response = new ResponseDto<Industry>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Message = "Industry deleted successfully",
                    Data = indToDelete
                };
                return response;
            }
        }

        public async Task<ResponseDto<Industry>> UpdateIndustryById(Guid Id, IndustryDto model)
        {
            var response = new ResponseDto<Industry>();
            var indToEdit = await _industryRepo.FindIndustryById(Id);
            if (indToEdit != null)
            {
                indToEdit.IndustryName = model.IndustryName;

            }
            var res = await _industryRepo.Edit(indToEdit);
            if (res)
            {
                response = new ResponseDto<Industry>
                {
                    IsSuccessful = true,
                    Message = "Inustry Updated Successfully",
                    Errors = { },
                    Data = indToEdit
                };
                return response;
            }
            else
            {
                response = new ResponseDto<Industry>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Message = "Industry failed to add",
                    Data = indToEdit
                };

                return response;
            }   
        }
    }
}
