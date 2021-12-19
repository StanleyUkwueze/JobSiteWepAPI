using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Models.Dtos;
using WebSiteAPI.Services.Interfaces;

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationservice _locationService;

        public LocationController(ILocationservice locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("Add-Job-Location")]

        public async Task<IActionResult> AddLocation(LocationDto location)
        {
            var result = await _locationService.AddJobLocation(location);
            return Ok(result);
        }
    }
}
