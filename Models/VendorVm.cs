using LeService.Api.DataLayer.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Models
{
    public class VendorVm
    {
        public VendorBasicInformationModel BasicModel { get; set; }
        public List<VendorServiceDetails> ServiceModels { get; set; }
    }
}
