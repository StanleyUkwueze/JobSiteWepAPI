using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteProject.Models
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
