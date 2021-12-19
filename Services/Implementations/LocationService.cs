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
    public class LocationService : ILocationservice
    {

        private readonly ILocationRepo _locationRepo;

        public LocationService( ILocationRepo locationrepo)
        {
            _locationRepo = locationrepo;
        }
        public async Task<ResponseDto<Location>> AddJobLocation(LocationDto model)
        {
            var locationToAdd = new Location()
            {
                Name = model.Name,
                NewId = model.NewId
            };
            var res = await _locationRepo.Add(locationToAdd);
            var response = new ResponseDto<Location>();
            if (res)
            {
                response = new ResponseDto<Location>
                {
                    IsSuccessful = true,
                    Errors = { },
                    Message = "Location added successfully!",
                    Data = locationToAdd
                };
            }
            else
            {
                response = new ResponseDto<Location>
                {
                    IsSuccessful = false,
                    Errors = { },
                    Message = "oops! Location could not be added",
                    Data = null
                };
            }
            return response;
        }
    }
}
