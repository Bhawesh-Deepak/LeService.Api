using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Models
{
    public class OrderModel
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Service { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string ServiceStatus { get; set; }
        public int ServiceId { get; set; }
        public string VendorName { get; set; }
        public int OrderId { get; set; }
    }
}
