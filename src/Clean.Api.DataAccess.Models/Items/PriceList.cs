using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clean.Api.DataAccess.Models.Items
{
    public class PriceList
    {
        [Key]
        [Column("PriceListId")]
        public int Id { get; set; }

        [Column("PriceListDate")]
        public DateTime Date { get; set; }

        [Column("BrandCode")]
        public string BrandCode { get; set; }
        public Brand Brand { get; set; }

        public List<ItemPrice> Prices { get; set; } = new List<ItemPrice>();
    }
}
