using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models
{
   public class JobApplication : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Job Job { get; set; }
        public Guid JobId { get; set; }
    }
}
