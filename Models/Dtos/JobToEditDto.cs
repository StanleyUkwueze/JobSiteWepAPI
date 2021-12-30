using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
   public class JobToEditDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
        public string Company { get; set; }
    }
}
