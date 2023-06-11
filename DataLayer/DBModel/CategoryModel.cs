using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("Category")]
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
