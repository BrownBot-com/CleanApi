﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Common
{
    public class PagedResult<T>
    {
        public T[] Data { get; set; }
        public uint Page { get; set; }
        public uint PageSize { get; set; }
        public uint TotalPages { get; set; }
    }
}
