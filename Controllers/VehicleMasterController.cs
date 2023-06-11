using LeService.Api.DataLayer.DBModel;
using LeService.Api.Helpers;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LeService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleMasterController : ControllerBase
    {
        private readonly IRepository<VehicleMaster> _IVehicleMasterRepository;

        public VehicleMasterController(IRepository<VehicleMaster> iVehicleMasterRepository)
        {
            _IVehicleMasterRepository = iVehicleMasterRepository;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateVehicleMaster(VehicleMaster model)
        {
            model.IsDeleted = false;
            model.IsActive = true;

            var response = await _IVehicleMasterRepository.Update(model);

            if (response)
            {
                return Ok(new ResponseHelper()
                {
                    IsSuccess = true,
                    Message = "Vehicle Master created successfully !",
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

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetVehicleTypes()
        {
            var response = await _IVehicleMasterRepository.GetList(x => !x.IsDeleted && x.IsActive);

            return Ok(new ResponseEntitiesHelper<dynamic>()
            {
                Message = "Vehicle Master Information Fetch successfully !",
                IsSuccess = true,
                Data = response.Select(x => new
                {
                    x.Id,
                    x.Seater,
                    x.AC_NonAC,
                    x.VeahicleName,
                    x.FuelType,
                    x.VehicleType,
                }).ToList(),
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateVehicleMaster(VehicleMaster model)
        {
            model.IsActive = true;
            model.IsDeleted = false;

            var response = await _IVehicleMasterRepository.Update(model);

            if (response)
            {
                return Ok(new ResponseHelper()
                {
                    IsSuccess = true,
                    Message = "Vehicle Master Updatesd successfully !",
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
        public async Task<IActionResult> DeleteVehicleMaster(int id)
        {
            var deleteModel = await _IVehicleMasterRepository.GetSingle(x => x.Id == id);

            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;

            var response = await _IVehicleMasterRepository.Update(deleteModel);

            if (response)
            {
                return Ok(new ResponseHelper()
                {
                    IsSuccess = true,
                    Message = "Vehicle Master Updatesd successfully !",
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

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetVehicleSingle(int id)
        {
            var model = await _IVehicleMasterRepository.GetSingle(x => x.Id == id);

            return Ok(new ResponseEntitiesHelper<dynamic>()
            {
                Message = "Vehicle Master Information Fetch successfully !",
                IsSuccess = true,
                Data = new
                {
                    model.Id,
                    model.Seater,
                    model.AC_NonAC,
                    model.VeahicleName,
                    model.FuelType,
                    model.VehicleType,
                },
                StatusCode = HttpStatusCode.OK
            });

        }
    }
}
