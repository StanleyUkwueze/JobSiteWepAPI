using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
   public class UserApplicationDto
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
