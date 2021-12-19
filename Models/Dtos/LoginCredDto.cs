using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
   public class LoginCredDto
    {
        public string Id { get; set; }
        public string token { get; set; }
        public bool Status { get; set; }
    }
}
