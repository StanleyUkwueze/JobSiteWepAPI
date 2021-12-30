using System;
using System.Collections.Generic;
using System.Text;

namespace Models
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
