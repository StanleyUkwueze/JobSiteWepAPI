using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSiteAPI.Models;
using WebSiteAPI.Models.Dtos;

namespace Commons.Helper
{
   public static class PagedList<T>
    {
        public static PageMeta CreatePageMetaData(int page, int pageSize, int total)
        {
            var total_pages = total % pageSize == 0 ? total / pageSize : total / pageSize + 1;

            return new PageMeta
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                TotalPages = total_pages
            };
        }

        public static PaginatedDto<T> Paginate(List<T> source, int pageNumber, int PageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;

            var paginatedList = source.Skip((pageNumber - 1) * PageSize).Take(PageSize);

            var pageMeta = CreatePageMetaData(pageNumber, PageSize, source.Count);

            return new PaginatedDto<T>
            {
                MetaData = pageMeta,
                Data = paginatedList
            };

        }
    }
}
