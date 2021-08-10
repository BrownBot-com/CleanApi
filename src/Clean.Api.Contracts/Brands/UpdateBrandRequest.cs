using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Brands
{
    public class UpdateBrandRequest
    {
        public string Name { get; set; }
        public string DynamicsCode { get; set; }
    }
}
