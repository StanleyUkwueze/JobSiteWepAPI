using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
   public class ResumeUploadDto
    {
        [Required]
        public IFormFile Resume { get; set; }

        public string title { get; set; }
        public string url { get; set; }
        public string publicId { get; set; }
    }
}
