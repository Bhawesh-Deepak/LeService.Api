using LeService.Api.DataLayer.DBModel;
using LeService.Api.Helpers;
using LeService.Api.ServiceLayer.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LeService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<CategoryModel> _ICategoryRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CategoryController(IRepository<CategoryModel> repository, IHostingEnvironment hostingEnvironment)
        {
            _ICategoryRepository = repository;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]

        public async Task<IActionResult> AddUpdate([FromForm] CategoryModel model)
        {
            if (model.Id == 0)
            {
                if (model.Image != null)
                {
                    model.ImagePath = await new BlobUpload().UploadImage(model.Image, _hostingEnvironment);
                }
                model.IsActive = true;
                model.IsDeleted = false;
                var response = await _ICategoryRepository.AddEntity(model);
                return Ok(response);
            }
            else
            {
                if (model.Image != null)
                {
                    model.ImagePath = await new BlobUpload().UploadImage(model.Image, _hostingEnvironment);
                }
                model.IsActive = true;
                model.IsDeleted = false;
                var response = await _ICategoryRepository.Update(model);
                return Ok(response);
            }

        }



        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ICategoryRepository.GetList(x => x.IsActive && !x.IsDeleted);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetSngle(int id)
        {
            var response = await _ICategoryRepository.GetSingle(x => x.Id == id);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteModel = await _ICategoryRepository.GetSingle(x => x.Id == id);
            deleteModel.IsDeleted = true;
            deleteModel.IsActive = true;
            var response = await _ICategoryRepository.Update(deleteModel);
            return Ok(response);
        }
    }
}
