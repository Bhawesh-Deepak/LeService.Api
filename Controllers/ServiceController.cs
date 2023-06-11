using LeService.Api.DataLayer.DBModel;
using LeService.Api.Helpers;
using LeService.Api.Models;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.AspNetCore.Hosting;
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
    public class ServiceController : ControllerBase
    {
        private readonly IRepository<CategoryModel> _ICategoryRepository;
        private readonly IRepository<ServiceDetail> _IServiceRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ServiceController(IRepository<CategoryModel> repository, IHostingEnvironment hostingEnvironment, IRepository<ServiceDetail> serviceRepo)
        {
            _ICategoryRepository = repository;
            _hostingEnvironment = hostingEnvironment;
            _IServiceRepository = serviceRepo;
        }
        [HttpPost]

        public async Task<IActionResult> AddUpdate([FromForm] ServiceDetail model)
        {
            if (model.Id == 0)
            {
                if (model.Image != null)
                {
                    model.ImagePath = await new BlobUpload().UploadImage(model.Image, _hostingEnvironment);
                }
                model.IsActive = true;
                model.IsDeleted = false;
                var response = await _IServiceRepository.AddEntity(model);
                return Ok(response);
            }
            else
            {
                if (model.Image != null)
                {
                    model.ImagePath = await new BlobUpload().UploadImage(model.Image, _hostingEnvironment);
                }
                else {
                    model.ImagePath = (await _IServiceRepository.GetSingle(x => x.Id == model.Id)).ImagePath;
                }
                model.IsActive = true;
                model.IsDeleted = false;
                var response = await _IServiceRepository.Update(model);
                return Ok(response);
            }

        }



        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var serviceModels = await _IServiceRepository.GetList(x => x.IsActive && !x.IsDeleted);
            var categoryModels = await _ICategoryRepository.GetList(x => x.IsActive && !x.IsDeleted);
            List<ServiceModel> models = GetServiceDetail(serviceModels, categoryModels);

            return Ok(models);
        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetServiceByCategoryId(int catId)
        {
            if (catId > 0) {
                return Ok(await _IServiceRepository.GetList(x => x.IsActive && !x.IsDeleted && x.CategoryId == catId));
            }
            return Ok(await _IServiceRepository.GetList(x => x.IsActive && !x.IsDeleted));
        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetSngle(int id)
        {
            var response = await _IServiceRepository.GetSingle(x => x.Id == id);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteModel = await _IServiceRepository.GetSingle(x => x.Id == id);
            deleteModel.IsDeleted = true;
            deleteModel.IsActive = true;
            var response = await _IServiceRepository.Update(deleteModel);
            return Ok(response);
        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetFeatureService()
        {
            var response = await _IServiceRepository.GetList(x => x.FeaturedService);
            return Ok(response);
        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetPopularService()
        {
            var response = await _IServiceRepository.GetList(x => x.PopularService);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetselectedService (string serviceName)
        {
            var finalName = serviceName.Split('(')[0];
            var response = await _IServiceRepository.GetList(x => x.ServiceName.Trim().ToLower()== finalName.ToLower().Trim());
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> ServiceAutoComplete()
        {
            var response = await _IServiceRepository.GetList(x => x.IsActive && !x.IsDeleted);
            var categories = await _ICategoryRepository.GetList(x => x.IsActive && !x.IsDeleted);

            var models = new List<ServiceSearch>();
            response.ToList().ForEach(data =>
            {
                var model = new ServiceSearch()
                {
                    Id= data.Id,
                    Name= data.ServiceName

                };
                model.Name =model.Name+"  (" + categories.FirstOrDefault(x => x.Id == data.CategoryId).CategoryName+")";
                models.Add(model);
            });
            return Ok(models);
        }

        private static List<ServiceModel> GetServiceDetail(IList<ServiceDetail> serviceModels, IList<CategoryModel> categoryModels)
        {
            return (from SM in serviceModels
                    join CM in categoryModels
                    on SM.CategoryId equals CM.Id
                    select new ServiceModel
                    {
                        Id = SM.Id,
                        ServiceName = SM.ServiceName,
                        Category = CM.CategoryName,
                        Description = SM.ShortDescription,
                        ImagePath = SM.ImagePath,
                        Featured=SM.FeaturedService,
                        Service=SM.PopularService,
                        Price= SM.Price,
                        DiscountPrice= SM.DiscountPrice
                    }).OrderBy(x=>x.Category).ToList();
        }


    }
}
