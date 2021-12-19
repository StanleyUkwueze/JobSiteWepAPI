using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Industry : BaseEntity
    {
        public string IndustryName { get; set; }

        public List<Job> Jobs { get; set; }

        public Industry()
        {
            Jobs = new List<Job>();
            Id = Guid.NewGuid();
        }
    }
}
