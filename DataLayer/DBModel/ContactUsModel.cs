using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("ContactUs")]
    public class ContactUsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string PlaceId { get; set; }

        public string ZipCode { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Comment { get; set; }
        public string Area { get; set; }
    }
}
