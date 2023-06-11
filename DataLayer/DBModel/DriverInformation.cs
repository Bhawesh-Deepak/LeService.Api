using System.ComponentModel.DataAnnotations.Schema;

namespace LeService.Api.Models
{
    [Table("DriverDetail")]
    public class DriverInformation
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string AadharCardNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string Address { get; set; }
        public string FeedBack { get; set; }
        public int Rating { get; set; }
        public int VehicleType { get; set; }
        public string VehicleNumber { get; set; }
    }
}
