using JobManager.Controller.Base;
using JobManager.Core.Data.DataTransferObjects.Request.Category;
using JobManager.Core.Data.DataTransferObjects.Response;
using JobManager.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobManager.Controller
{
    [Route("api/category")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryResponseDto>> Create(CategoryRequestDto categoryRequestDto)
        {
            var response = await _categoryService.CreateCategory(categoryRequestDto);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
