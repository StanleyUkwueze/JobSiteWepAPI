using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Models
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
        public string ModifiedAt { get; set; } = DateTime.Now.ToString();

        public List<Resume> Resumes { get; set; }

        public AppUser()
        {
            Resumes = new List<Resume>();
        }
    }
}

