using System.ComponentModel.DataAnnotations.Schema;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("VehicleMaster")]
    public class VehicleMaster
    {
        public int Id { get; set; }
        public string VeahicleName { get; set; }
        public string AC_NonAC { get; set; }
        public string FuelType { get; set; }
        public string VehicleType { get; set; }
        public int Seater { get; set; }
        public bool IsActive { get; set; }= true;
        public bool IsDeleted { get; set; } = false;
    }
}
