using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models
{
  public class PageMeta
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}
