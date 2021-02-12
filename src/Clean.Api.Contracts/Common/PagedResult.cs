using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Common
{
    public class PagedResult<T>
    {
        public T[] Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
