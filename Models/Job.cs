using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Job : BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
        public string Company { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public Guid IndustryId { get; set; }
        public Industry Industry { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public JobNature JobNature { get; set; }
        public int JobNatureId { get; set; }

    }
}
