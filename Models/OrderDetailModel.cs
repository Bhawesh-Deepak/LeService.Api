using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Models
{
    public class OrderDetailModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string ServiceName { get; set; }
        public string VendorName { get; set; }
        public string ServiceStatus { get; set; }
        public decimal PriceAmount { get; set; }
    }
}
