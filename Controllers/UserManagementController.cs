using LeService.Api.DataLayer.DBModel;
using LeService.Api.Helpers;
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
    public class UserManagementController : ControllerBase
    {
        private readonly IRepository<AppointmentModel> _IAppointmentRepository;
        private readonly IRepository<ServiceDetail> _IServiceDetailRepository;
        private readonly IRepository<VendorBasicInformationModel> _IVendorBasicRepository;
        private readonly IRepository<VendorServiceDetails> _IVendorServiceRepository;
        private readonly IRepository<LoginModel> _IAuthenticateRepository;
        public UserManagementController(IRepository<AppointmentModel> appointmentRepo,
            IRepository<ServiceDetail> serviceDetailRepo,
            IRepository<VendorBasicInformationModel> vendorBasicInfoRepository,
            IRepository<VendorServiceDetails> vendorServiceRepo, IRepository<LoginModel> authenticateRepository)
        {
            _IAppointmentRepository = appointmentRepo;
            _IServiceDetailRepository = serviceDetailRepo;
            _IVendorBasicRepository = vendorBasicInfoRepository;
            _IVendorServiceRepository = vendorServiceRepo;
            _IAuthenticateRepository = authenticateRepository;

        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login(UserAuthModel model)
        {
            var customerModel = await _IAuthenticateRepository.GetSingle(x => x.Phone.Trim().ToLower() == model.Phone.Trim().ToLower()
                && x.Password.Trim().ToLower() == model.Password.Trim().ToLower());

            if (customerModel != null)
            {
                return Ok(new { isSuccess=true,data=customerModel });
            }
            return Ok(new { isSuccess = false });


        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateUser(LoginModel model)
        {
            model.IsActive = true;
            model.IsDeleted = false;
            var userModels = await _IAuthenticateRepository.GetList(x => !x.IsDeleted && x.IsActive);
            if (userModels.Any(x => x.Phone.Trim() == model.Phone.Trim()))
            {
                return Ok(false);
            }
            else
            {
                var customerModel = await _IAuthenticateRepository.AddEntity(model);
                return Ok(customerModel);
            }

        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetCustomerOrderDetail(string phone)
        {
            var appointModels = (await _IAppointmentRepository.GetList(x => x.IsActive && !x.IsDeleted && x.Phone.Trim().ToLower() == phone.Trim().ToLower())).OrderByDescending(x => x.Id);
            var serviceModels = await _IServiceDetailRepository.GetList(x => x.IsActive && !x.IsDeleted);
            var vendorDetail = await _IVendorBasicRepository.GetList(x => x.IsActive && !x.IsDeleted);

            var response = (from APM in appointModels
                            join SM in serviceModels
                            on APM.ServiceId equals SM.Id
                            select new OrderModel
                            {
                                CustomerName = APM.CustomerName,
                                Address = APM.Address,
                                Phone = APM.Phone,
                                Service = SM.ServiceName,
                                ZipCode = APM.ZipCode,
                                ServiceStatus = APM.ServiceStatus,
                                ServiceId = SM.Id,
                                OrderId = APM.Id,
                                VendorName = vendorDetail.FirstOrDefault(x => x.Id == APM.VendorId)?.VendorName

                            }).ToList();

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetStartCode(int orderId)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var randonNumber = new string(Enumerable.Repeat(chars, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var appointmentModel = await _IAppointmentRepository.GetSingle(x => x.Id == orderId);
            if (string.IsNullOrEmpty(appointmentModel.StartCode))
            {

                appointmentModel.StartCode = randonNumber;
                appointmentModel.ServiceStatus = ServiceStatus.Started.ToString();

                await _IAppointmentRepository.Update(appointmentModel);

                return Ok(randonNumber);
            }
            return Ok(appointmentModel.StartCode);

        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CancelService(int orderId, string reason)
        {

            var appointmentModel = await _IAppointmentRepository.GetSingle(x => x.Id == orderId);
            appointmentModel.ServiceStatus = ServiceStatus.Cancel.ToString();
            appointmentModel.Reason = reason;

            var response = await _IAppointmentRepository.Update(appointmentModel);
            return Ok(response);


        }
    }
}
