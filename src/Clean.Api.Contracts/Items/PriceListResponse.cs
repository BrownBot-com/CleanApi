using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class PriceListResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string BrandCode { get; set; }

        public ItemPriceResponse[] Prices { get; set; }
    }
}
