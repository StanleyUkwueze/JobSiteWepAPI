using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebsiteProject.Models
{ 
       public class JobNature : BaseEntity
       { 
        public string Name { get; set; }
        public List<Job> Jobs { get; set; }

        public JobNature()
        {
            Jobs = new List<Job>();
            Id = Guid.NewGuid();
        }
       }  
}
