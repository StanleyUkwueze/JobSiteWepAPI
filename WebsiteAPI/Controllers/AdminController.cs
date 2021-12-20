using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSiteAPI.Services.Interfaces;

namespace WebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IJobService _adminService;

        public AdminController(IJobService adminService)
        {
            _adminService = adminService;
        }
    }
}
