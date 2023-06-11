using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace LeService.Api.DataLayer.DBModel
{
    [Table("AppointmentDetail")]
    public class AppointmentModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public int ServiceId { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public bool IsContacted { get; set; }
        public string ServiceStatus { get; set; }
        public string Reason { get; set; }
        public int VendorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PriceAmount { get; set; }
        public decimal DiscountAmout { get; set; }
        public string CustomerFeedBack { get; set; }
        public string VendorFeedBack { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string StartCode { get; set; }
        public string SeviceFeedBack { get; set; }
        public string VendorBehaveFeedBack { get; set; }
        public string ApplicationFeedBack { get; set; }
        public string ShareTheApp { get; set; }
        public decimal FinalPayment { get; set; }
    }
}
