using FluentValidation.Results;
using JobManager.Core.Data.DataTransferObjects.Request.JobOffer;
using JobManager.Core.Data.DataTransferObjects.Response;
using JobManager.Core.Data.Mappers;
using JobManager.Core.Data.Model;
using JobManager.Core.Enum;
using JobManager.Repository.Interface;
using JobManager.Service.Interface;
using JobManager.Validation.JobOffer;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JobManager.Service
{
    public class JobOfferService : IJobOfferService
    {
        private readonly IRequestDataService _requestDataService;
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly ILanguageService _languageService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IJobOfferCategoryRepository _jobOfferCategoryRepository;

        public JobOfferService(IRequestDataService requestDataService, 
            IJobOfferRepository jobOfferRepository, 
            ILanguageService languageService,
            ICategoryRepository categoryRepository,
            IJobOfferCategoryRepository jobOfferCategoryRepository)
        {
            _requestDataService = requestDataService;
            _jobOfferRepository = jobOfferRepository;
            _languageService = languageService;
            _categoryRepository = categoryRepository;
            _jobOfferCategoryRepository = jobOfferCategoryRepository;
        }

        public async Task<JobOfferResponseDto> GetJobOffers()
        {
            var response = new JobOfferResponseDto();

            var dbJobOffers = await _jobOfferRepository.SelectAllWithUserAndCategory();

            if (dbJobOffers is null) dbJobOffers = new List<DbJobOffer>();

            response.JobOffer = null;
            response.JobOffers = JobOfferMapper.ToDto(dbJobOffers);

            return response;
        }

        public async Task<JobOfferResponseDto> GetJobOffersByCategory(string categoryName)
        {
            var response = new JobOfferResponseDto();

            var dbCategory = await _categoryRepository.SelectByName(categoryName);

            if(dbCategory is null)
            {
                response.AddError(_languageService.Get("categoryWithNameDoesntExist", categoryName));
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            var dbJobOffers = await _jobOfferRepository.SelectByCategoryId(dbCategory.Id);

            response.JobOffer = null;
            response.JobOffers = JobOfferMapper.ToDto(dbJobOffers);

            return response;
        }

        public async Task<JobOfferResponseDto> CreateJobOffer(JobOfferRequestDto jobOfferRequestDto)
        {
            var response = new JobOfferResponseDto();
            var user = await _requestDataService.GetCurrentUser();

            if(user.UserType != UserType.Employer)
            {
                response.AddError(_languageService.Get("onlyEmployersCanCreateJobOffers"));
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var dbCategories = await _categoryRepository.SelectByIds(jobOfferRequestDto.CategoryIds);

            if(dbCategories.Count != jobOfferRequestDto.CategoryIds.Count)
            {
                var invalidCategories = jobOfferRequestDto.CategoryIds
                    .Where(categoryId => !dbCategories.Any(category => category.Id == categoryId));

                response.AddError(_languageService.Get("theCategoriesWithIdsHaveNotBeenFound", string.Join(",", invalidCategories)));
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            JobOfferRequestDtoValidator validator = new JobOfferRequestDtoValidator();
            ValidationResult validationResult = await validator.ValidateAsync(jobOfferRequestDto);

            if(!validationResult.IsValid)
            {
                response.AddErrors(validationResult.Errors.Select(error => error.ErrorMessage));
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            // TODO: Notify clients via websockets when new job offers 
            // in subscribed categories are available
            DbJobOffer newJobOffer = new DbJobOffer
            {
               Description = jobOfferRequestDto.Description,
               EmployerId = user.Id,
               Name = jobOfferRequestDto.Name,
               PayPerHour = jobOfferRequestDto.PayPerHour
            };

            await _jobOfferRepository.Insert(newJobOffer);

            await _jobOfferRepository.SaveChangesAsync();

            await _jobOfferCategoryRepository.InsertCategoriesForJobOffer(newJobOffer, dbCategories);

            response.JobOffer = JobOfferMapper.ToDto(newJobOffer);

            return response;
        }

    }
}
