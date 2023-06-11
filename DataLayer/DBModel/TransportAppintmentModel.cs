using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("BookTransport")]
    public class TransportAppintmentModel
    {
        public int Id { get; set; }
        public string PickUpLocation { get; set; }
        public string DropLocation { get; set; }

        public string VehicleType { get; set; }
        public int Seater { get; set; }

        public string ACType { get; set; }
        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }
  

        public DateTime AppointmentDate { get; set; }
        public int DriverId { get; set; }

        public string VehicleNumber { get; set; }
        public int Distance { get; set; }

        public decimal Price { get; set; }
        public decimal FuelPrice { get; set; }

        public decimal DriverPrice { get; set; }
        public decimal VendoPrice { get; set; }

        public string FeedBack { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
