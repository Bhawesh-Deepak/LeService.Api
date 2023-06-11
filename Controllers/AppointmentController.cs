using LeService.Api.DataLayer.DBModel;
using LeService.Api.Helpers;
using LeService.Api.Models;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LeService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IRepository<AppointmentModel> _IAppointmentRepository;
        private readonly IRepository<ServiceDetail> _IServiceDetailRepository;
        private readonly IRepository<VendorBasicInformationModel> _IVendorBasicRepository;
        private readonly IRepository<VendorServiceDetails> _IVendorServiceRepository;

        public AppointmentController(IRepository<AppointmentModel> appointmentRepo,
            IRepository<ServiceDetail> serviceRepo, IRepository<VendorBasicInformationModel> basicInfoRepo,
            IRepository<VendorServiceDetails> vendorServiceRepo)
        {
            _IAppointmentRepository = appointmentRepo;
            _IServiceDetailRepository = serviceRepo;
            _IVendorBasicRepository = basicInfoRepo;
            _IVendorServiceRepository = vendorServiceRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddUpdate(AppointmentModel model)
        {
            model.FinalPayment = Convert.ToDecimal("0.0");
            if (model.Id == 0)
            {
                model.AppointmentDate = DateTime.Now;
                model.ServiceStatus = "Request";

                model.IsActive = true;
                model.IsDeleted = false;
                var response = await _IAppointmentRepository.AddEntity(model);

                var appointmentMessage = @$"New Appointment has been create for Customer Name : {model.CustomerName} phone number : {model.Phone} ZipCode {model.ZipCode} address {model.Address}";
                TelegramMessageHelper.SendMessage(appointmentMessage);

                //await SendWhatsAppMessage();
                return Ok(response);
            }
            else
            {
                model.IsActive = true;
                model.IsDeleted = false;
                var response = await _IAppointmentRepository.Update(model);
                return Ok(response);
            }

        }



        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var response = (await _IAppointmentRepository.GetList(x => x.IsActive && !x.IsDeleted)).OrderByDescending(x => x.Id);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetVendorService()
        {
            var appointModels = (await _IAppointmentRepository.GetList(x => x.IsActive && !x.IsDeleted && x.ServiceStatus != "Complete")).OrderByDescending(x => x.Id);
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
        public async Task<IActionResult> GetSngle(int id)
        {
            var response = await _IAppointmentRepository.GetSingle(x => x.Id == id);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteModel = await _IAppointmentRepository.GetSingle(x => x.Id == id);
            deleteModel.IsDeleted = true;
            deleteModel.IsActive = true;
            var response = await _IAppointmentRepository.Update(deleteModel);
            return Ok(response);
        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetAllVendorForService(int serviceId)
        {
            var vendorModels = await _IVendorBasicRepository.GetList(x => x.IsActive && !x.IsDeleted);
            var vendorServiceDetails = await _IVendorServiceRepository.GetList(x => x.IsActive && !x.IsDeleted);

            var response = (from VM in vendorModels
                            join VSD in vendorServiceDetails
                            on VM.Id equals VSD.VendorId
                            where VSD.ServiceId == serviceId
                            select new VendorBasicInformationModel
                            {
                                Id = VM.Id,
                                VendorName = VM.VendorName,
                                Phone = VM.Phone,
                                IsVerified = VM.IsVerified,
                                ZipCode = VM.ZipCode
                            }).ToList();

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateJobCard(int orderId, int vendorId)
        {
            var basicModel = await _IAppointmentRepository.GetSingle(x => x.Id == orderId);
            basicModel.VendorId = vendorId;
            basicModel.ServiceStatus = ServiceStatus.Progress.ToString();
            var response = await _IAppointmentRepository.Update(basicModel);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CancelService(int orderId, string reason)
        {
            var updateModel = await _IAppointmentRepository.GetSingle(x => x.Id == orderId);
            updateModel.ServiceStatus = ServiceStatus.Cancel.ToString();
            updateModel.Reason = reason;

            var response = await _IAppointmentRepository.Update(updateModel);

            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CompleteService(CompleteModel model)
        {
            var updateModel = await _IAppointmentRepository.GetSingle(x => x.Id == model.OrderId);
            updateModel.SeviceFeedBack = model.SeviceFeedBack;
            updateModel.VendorBehaveFeedBack = model.VendorBehaveFeedBack;
            updateModel.ApplicationFeedBack = model.ApplicationFeedBack;
            updateModel.ShareTheApp = model.ShareTheApp;

            updateModel.ServiceStatus = ServiceStatus.Completed.ToString();

            var response = await _IAppointmentRepository.Update(updateModel);

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> SendMessage(string message)
        {
            TelegramMessageHelper.SendMessage(message);

            return await Task.Run(() => Ok(true));
        }

        private async Task SendWhatsAppMessage()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.ultramsg.com/instance11520/messages/chat"))
                {
                    var contentList = new List<string>();
                    contentList.Add("token=216tq4ry5y9cbhek");
                    contentList.Add("to=+919315775084");
                    contentList.Add("body=Dear Customer Your Order for Service has been created successfully ! Our service Team will contact You soon !");
                    contentList.Add("priority=10");
                    contentList.Add("referenceId=");
                    request.Content = new StringContent(string.Join("&", contentList));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                }
            }

        }
    }
}
