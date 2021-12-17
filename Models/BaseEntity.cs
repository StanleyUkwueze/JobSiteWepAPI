using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class BaseEntity
    {

        public Guid Id { get; set; }
        public string DateCreated { get; set; } = DateTime.Now.ToString();
        public string DateUpdated { get; set; } = DateTime.Now.ToString();
    }
}
