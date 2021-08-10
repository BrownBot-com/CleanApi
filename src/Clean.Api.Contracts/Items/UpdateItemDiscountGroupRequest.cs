using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class UpdateItemDiscountGroupRequest
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Additional { get; set; }
        public string SupplierRef { get; set; }
    }
}
