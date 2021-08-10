using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Brands
{
    public class CreateBrandRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DynamicsCode { get; set; }
    }
}
