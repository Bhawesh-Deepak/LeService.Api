using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("ServiceDetail")]
    public class ServiceDetail
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ServiceName { get; set; }
        public string ShortDescription { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool FeaturedService { get; set; }

        public bool PopularService { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
