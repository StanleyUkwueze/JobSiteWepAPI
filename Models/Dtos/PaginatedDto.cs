using System;
using System.Collections.Generic;
using System.Text;

namespace WebSiteAPI.Models.Dtos
{
  public class PaginatedDto<T>
    {
        public PageMeta MetaData { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginatedDto()
        {
            Data = new List<T>();
        }
    }
}
