using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Api.Services.Models
{
    public class AbnResult
    {
        public string Abn { get; set; }
        public string AbnStatus { get; set; }
        public string Acn { get; set; }
        public string AddressDate { get; set; }
        public string AddressPostcode { get; set; }
        public string AddressState { get; set; }
        public List<string> BusinessName { get; set; }
        public string EntityName { get; set; }
        public string EntityTypeCode { get; set; }
        public string EntityTypeName { get; set; }
        public string Gst { get; set; }
        public string Message { get; set; }
    }
}
