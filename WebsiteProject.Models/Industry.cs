using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteProject.Models
{
  public class Industry : BaseEntity
    {
        public string Name { get; set; }

        public List<Job> Jobs { get; set; }

        public Industry()
        {
            Jobs = new List<Job>();
            Id = Guid.NewGuid();
        }

    }
}
