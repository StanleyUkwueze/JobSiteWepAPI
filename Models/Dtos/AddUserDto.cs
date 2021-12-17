using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
   public class AddUserDto
    {
        [Required]
       
        public string FirstName { get; set; }

        [Required]
       
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
