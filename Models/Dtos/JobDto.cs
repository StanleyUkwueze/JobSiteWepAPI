using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
   public class JobDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
        public Guid IndustryId { get; set; }
        public Guid CategoryId { get; set; }
        public int LocationId { get; set; }
        public int JobNatureId { get; set; }
        public string Company { get; set; }
        //public string Industry { get; set; }
        //public string Category { get; set; }
    }
}
