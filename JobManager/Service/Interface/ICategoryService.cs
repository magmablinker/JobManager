using JobManager.Core.Data.DataTransferObjects.Request.Category;
using JobManager.Core.Data.DataTransferObjects.Response;
using System.Threading.Tasks;

namespace JobManager.Service.Interface
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategory(CategoryRequestDto categoryRequestDto);
    }
}
