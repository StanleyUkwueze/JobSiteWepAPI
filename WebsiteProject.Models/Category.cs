using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteProject.Models
{
   public class Category : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Job name should lie between 5 and 15")]
        public string JobName { get; set; }
        [Required]
        public List<Job> Jobs { get; set; }
        public Category()
        {
            Jobs = new List<Job>();
            Id = Guid.NewGuid();
        }
    }
}
