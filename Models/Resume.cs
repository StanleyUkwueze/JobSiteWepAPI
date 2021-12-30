using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Resume
    {
        public string Title { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Url { get; set; }
        public string PublicId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
