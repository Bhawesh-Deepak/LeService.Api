using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Models
{
    public class CompleteModel
    {
        public int OrderId { get; set; }
        public string SeviceFeedBack { get; set; }
        public string VendorBehaveFeedBack { get; set; }
        public string ApplicationFeedBack { get; set; }
        public string ShareTheApp { get; set; }
    }
}
