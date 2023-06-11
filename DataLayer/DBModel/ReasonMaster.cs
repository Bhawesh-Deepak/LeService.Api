using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.DataLayer.DBModel
{
    [Table("ReasonMaster")]
    public class ReasonMaster
    {

        public int Id { get; set; }
        public string Reason { get; set; }
        public string ReasonType { get; set; }
    }
}
