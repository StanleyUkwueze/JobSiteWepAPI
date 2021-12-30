using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Location : BaseEntity
    {
        public string Name { get; set; }
        public List<Job> Jobs { get; set; }
        public Location()
        {
            Jobs = new List<Job>();
            Id = Guid.NewGuid();
        }
    }
}
