﻿using Models;
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
        public Guid LocationId { get; set; }
        public Guid JobNatureId { get; set; }
        public string Company { get; set; }
        public int JobValidDays { get; set; }
        //public string IndustryName { get; set; }
        //public string CategoryName { get; set; }
        //public string LocationName { get; set; }
        //public string JobNatureName { get; set; }
    }
}
