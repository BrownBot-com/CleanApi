using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class CreatePriceListRequest
    {
        public DateTime Date { get; set; }

        public string BrandCode { get; set; }

        public CreateItemPriceRequest[] Prices { get; set; }
    }
}
