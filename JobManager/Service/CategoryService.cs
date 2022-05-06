using FluentValidation.Results;
using JobManager.Core.Data.DataTransferObjects.Request.Category;
using JobManager.Core.Data.DataTransferObjects.Response;
using JobManager.Core.Data.Mappers;
using JobManager.Core.Data.Model;
using JobManager.Core.Enum;
using JobManager.Repository.Interface;
using JobManager.Service.Interface;
using JobManager.Validation.Category;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JobManager.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRequestDataService _requestDataService;
        private readonly ILanguageService _languageService;

        public CategoryService(ICategoryRepository categoryRepository, IRequestDataService requestDataService, ILanguageService languageService)
        {
            _categoryRepository = categoryRepository;
            _requestDataService = requestDataService;
            _languageService = languageService;
        }

        public async Task<CategoryResponseDto> CreateCategory(CategoryRequestDto categoryRequestDto)
        {
            var response = new CategoryResponseDto();

            var currentUser = await _requestDataService.GetCurrentUser();

            if(currentUser.UserType != UserType.Admin && currentUser.UserType != UserType.Moderator)
            {
                response.AddError(_languageService.Get("onlyAdministratorsAndModeratorsCanCreateCategory"));
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var dbCategory = await _categoryRepository.SelectByName(categoryRequestDto.CategoryName);

            if(dbCategory != null)
            {
                response.AddError(_languageService.Get("categoryWithNameExistsAlready", dbCategory.Name));
                response.StatusCode = HttpStatusCode.Conflict;
            }

            CategoryRequestDtoValidator validator = new CategoryRequestDtoValidator();
            ValidationResult validationResult = await validator.ValidateAsync(categoryRequestDto);

            if(!validationResult.IsValid)
            {
                response.AddErrors(validationResult.Errors.Select(error => error.ErrorMessage).ToList());
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var newCategory = new DbCategory
            {
                Name = categoryRequestDto.CategoryName
            };

            await _categoryRepository.Insert(newCategory);
            await _categoryRepository.SaveChangesAsync();

            response.Category = CategoryMapper.ToDto(newCategory);

            return response;
        }

    }
}
