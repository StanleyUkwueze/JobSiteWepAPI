using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
  public  class Category : BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public List<Job> Jobs { get; set; }
        public Category()
        {
            Jobs = new List<Job>();
            Id = Guid.NewGuid();
        }
    }
}
