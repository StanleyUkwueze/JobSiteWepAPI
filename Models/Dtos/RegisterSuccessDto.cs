﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
  public class RegisterSuccessDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
