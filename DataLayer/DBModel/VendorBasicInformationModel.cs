using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("VendorBasicInformation")]
    public class VendorBasicInformationModel
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int WorkingArea { get; set; }
        public string WorkingDays { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Locality { get; set; }
    }
}
