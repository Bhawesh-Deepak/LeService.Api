using LeService.Api.DataLayer.DBModel;
using LeService.Api.Helpers;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTransportApiController : ControllerBase
    {
        private readonly IRepository<TransportAppintmentModel> _IAppointmentRepository;

        public BookTransportApiController(IRepository<TransportAppintmentModel> transportRepository)
        {
            _IAppointmentRepository = transportRepository;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateAppointment(TransportAppintmentModel model)
        {
            model.IsActive = true;
            model.IsDeleted = false;

            var response = await _IAppointmentRepository.AddEntity(model);

            var appointmentMessage = @$"New Transport Appointment has been create for Customer Name : 
            {model.CustomerName} phone number : {model.CustomerPhone} pickUpLocation {model.PickUpLocation}
            DropLocation {model.DropLocation} Vehicle Type {model.VehicleType} AC-NoAC {model.ACType} PickUpDateTime {model.AppointmentDate}";
            TelegramMessageHelper.SendMessage(appointmentMessage);

            return Ok("success");
        }
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> GetBookingDetails(string phoneNumber)
        {
            var response = await _IAppointmentRepository.GetList(x=>x.IsActive && x.CustomerPhone.Trim()==phoneNumber.Trim());
            return Ok(response);
        }
    }
}
