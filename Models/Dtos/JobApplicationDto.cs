using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
   public class JobApplicationDto
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Location {get; set; }
        public string JobNature { get; set; }
        public string Company { get; set; }
    }
}
