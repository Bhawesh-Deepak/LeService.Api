using LeService.Api.Helpers;
using LeService.Api.Models;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LeService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IRepository<DriverInformation> _IDriverInfoRepository;

        public DriverController(IRepository<DriverInformation> iDriverInfoRepository)
        {
            _IDriverInfoRepository = iDriverInfoRepository;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateDriver(DriverInformation model)
        {
            model.IsActive = true;
            model.IsDeleted = false;
            var response = await _IDriverInfoRepository.AddEntity(model);
            return Ok(new ResponseHelper()
            {
                Message = "Driver Information created successfull !",
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DriverInfoList()
        {
            var response = await _IDriverInfoRepository.GetList(x => x.IsActive && !x.IsDeleted);
            return Ok(new ResponseEntitiesHelper<List<dynamic>>()
            {
                Message = "Driver Information Fetch successfully !",
                IsSuccess = true,
                Data = response.Select(x => new
                {
                    x.Id,
                    x.DriverName,
                    x.DriverPhone,
                    x.AadharCardNumber,
                    x.LicenseNumber,
                    x.Address,
                    x.FeedBack,
                    x.Rating
                }).ToList<dynamic>(),
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetDriverInfo([Required]int id)
        {
            var response = await _IDriverInfoRepository.GetSingle(x => x.Id == id);
            return Ok(new ResponseEntitiesHelper<dynamic>()
            {
                Message = "Driver Information Fetch successfully !",
                IsSuccess = true,
                Data = new
                {
                    response.Id,
                    response.DriverName,
                    response.DriverPhone,
                    response.AadharCardNumber,
                    response.LicenseNumber,
                    response.Address,
                    response.FeedBack,
                    response.Rating,
                },
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateDriverInfo(DriverInformation model)
        {
            model.IsDeleted = false;
            model.IsActive = true;

            var response = await _IDriverInfoRepository.Update(model);

            if (response)
            {
                return Ok(new ResponseHelper()
                {
                    IsSuccess = true,
                    Message = "Driver Information updated successfully !",
                    StatusCode = HttpStatusCode.OK
                });
            }
            return BadRequest(new ResponseHelper()
            {
                IsSuccess = false,
                Message = "Something wents wrong, Please contact Admin !",
                StatusCode = HttpStatusCode.BadRequest
            });
        }

        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteDriverInfo([Required]int id)
        {
            var deleteModel = await _IDriverInfoRepository.GetSingle(x => x.Id == id);

            deleteModel.IsDeleted = true;
            deleteModel.IsActive = false;

            var deleteResponse = await _IDriverInfoRepository.Update(deleteModel);

            if (deleteResponse)
            {
                return Ok(new ResponseHelper()
                {
                    IsSuccess = true,
                    Message = "Driver Information deleted successfully !",
                    StatusCode = HttpStatusCode.OK
                });
            }
            return BadRequest(new ResponseHelper()
            {
                IsSuccess = false,
                Message = "Something wents wrong, Please contact Admin !",
                StatusCode = HttpStatusCode.BadRequest
            });
        }


    }
}
