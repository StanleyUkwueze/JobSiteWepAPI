using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        [HttpPost("Add-Industry")]
        public async Task<IActionResult> AddIndustry(IndustryDto model)
        {
            var result = await _industryService.Addindustry(model);
            return Ok(result);
        }

        [HttpPut("Update-Industry-By-Id/Id")]
        public async Task<IActionResult>UpdateIndustryById(Guid Id, IndustryDto model)
        {
            var res = await _industryService.UpdateIndustryById(Id, model);
            return Ok(res);
        }

        [HttpDelete("Delete-Industry-By-Id/Id")]
        public async Task<IActionResult> DeleteIndustryById(Guid Id)
        {
            var res = await _industryService.DeleteIndustryById(Id);
            return Ok(res);
            
        }
    }
}
