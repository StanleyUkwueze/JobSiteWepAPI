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
        public string IndustryId { get; set; }
        public string CategoryId { get; set; }
        public string LocationId { get; set; }
        public string JobNatureId { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string Category { get; set; }
    }
}
