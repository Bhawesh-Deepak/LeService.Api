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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IRepository<ContactUsModel> _IContactUsRepository;
        public ContactUsController(IRepository<ContactUsModel> contactRepo)
        {
            _IContactUsRepository = contactRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateContact(ContactUsModel model)
        {
            var response = await _IContactUsRepository.AddEntity(model);
            if (response) {

                EmailHelper.SendEmail("New Contact Us Recieved ! Please check the admin panel for more..",
                    "New Contact Us Received", "BhaweshDeepak@gmail.com");

                EmailHelper.SendEmail("New Contact Us Recieved ! Please check the admin panel for more..",
                   "New Contact Us Received", "BhaweshDeepak@gmail.com");

                EmailHelper.SendEmail("New Contact Us Recieved ! Please check the admin panel for more..",
                   "New Contact Us Received", "yogeshdeepak17@gmail.com");
            }
            return Ok(response);
        }
    }
}
