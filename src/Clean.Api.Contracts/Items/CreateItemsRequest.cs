using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Clean.Api.Contracts.Items
{
    public class CreateItemsRequest
    {
        CreateItemRequest[] Items { get; set; } = new CreateItemRequest[0];
    }

    public class CreateItemRequest
    {
        [Required]
        public string FullCode { get; set; }

        [Required]
        public string FullDescription { get; set; }

        public string SupplierCode { get; set; }

        public string FullType { get; set; }

    }
}
