using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool Featured { get; set; }
        public bool Service { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
