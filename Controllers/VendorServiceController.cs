using LeService.Api.DataLayer.DBModel;
using LeService.Api.Models;
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
    public class VendorServiceController : ControllerBase
    {
        private readonly IRepository<VendorBasicInformationModel> _IVendorBasicRepository;
        private readonly IRepository<VendorServiceDetails> _IVendorServiceRepository;

        public VendorServiceController(IRepository<VendorBasicInformationModel> basicRepo, IRepository<VendorServiceDetails> serviceRepo)
        {
            _IVendorBasicRepository = basicRepo;
            _IVendorServiceRepository = serviceRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddUpdate(VendorVm model)
        {
            if (model.BasicModel.Id == 0)
            {
                await CreateVendor(model);
            }
            else
            {
                await UpdateVendor(model);
            }
            return Ok(true);

        }

        private async Task<bool> CreateVendor(VendorVm model)
        {
            model.BasicModel.IsActive = true;
            model.BasicModel.IsDeleted = false;
            var response = await _IVendorBasicRepository.AddEntity(model.BasicModel);
            var repoId = (await _IVendorBasicRepository.GetList(x => x.IsActive && !x.IsDeleted)).Max(x => x.Id);

            model.ServiceModels.ForEach(data =>
            {
                data.VendorId = repoId;
                data.IsActive = true;
                data.IsDeleted = false;
            });

            var serviceResponse = await _IVendorServiceRepository.AddEnttities(model.ServiceModels.ToArray());
            return serviceResponse;
        }

        private async Task<bool> UpdateVendor(VendorVm model)
        {
            model.BasicModel.IsActive = true;
            model.BasicModel.IsDeleted = false;
            var vendorBasicModel = await _IVendorBasicRepository.Update(model.BasicModel);

            var vendorServiceModels = await _IVendorServiceRepository.GetList(x => x.VendorId == model.BasicModel.Id);
            vendorServiceModels.ToList().ForEach(data =>
            {
                data.IsActive = false;
                data.IsDeleted = true;
            });

            var vendorRespose = await _IVendorServiceRepository.Update(vendorServiceModels.ToArray());
            var createResponse = await _IVendorServiceRepository.AddEnttities(model.ServiceModels.ToArray());
            return (true);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var basicDetails = await _IVendorBasicRepository.GetList(x => x.IsActive && !x.IsDeleted);
            return Ok(basicDetails);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetServiceDetails(int vendorId)
        {
            var basicDetails = await _IVendorServiceRepository.GetList(x => x.VendorId == vendorId);
            return Ok(basicDetails);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var basicModel = await _IVendorBasicRepository.GetSingle(x => x.Id == id);
            var serviceModels = await _IVendorServiceRepository.GetList(x => x.VendorId == id);
            var response = new VendorVm()
            {
                BasicModel = basicModel,
                ServiceModels = serviceModels.ToList()
            };
            return Ok(response);
        }


     

        //[HttpGet]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var deleteModel = await _IAppointmentRepository.GetSingle(x => x.Id == id);
        //    deleteModel.IsDeleted = true;
        //    deleteModel.IsActive = true;
        //    var response = await _IAppointmentRepository.Update(deleteModel);
        //    return Ok(response);
        //}
    }
}
