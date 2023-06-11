using LeService.Api.DataLayer.DBModel;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IRepository<ReasonMaster> _IReasonMasterRepository;
        private readonly IRepository<AppointmentModel> _IAppointmentRepository;
        public MasterController(IRepository<ReasonMaster> reasonRepo)
        {
            _IReasonMasterRepository = reasonRepo;
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetCancelReason()
        {
            var reason = await _IReasonMasterRepository.GetList(x => x.ReasonType == "Cancel");
            return Ok(reason);
        }

       
    }
}
