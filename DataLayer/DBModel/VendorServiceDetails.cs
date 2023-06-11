using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("VendorServiceDetails")]
    public class VendorServiceDetails
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int ServiceId { get; set; }
        public decimal PriceAmount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
