using JobManager.Controller.Base;
using JobManager.Core.Data.DataTransferObjects.Request.JobOffer;
using JobManager.Core.Data.DataTransferObjects.Response;
using JobManager.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobManager.Controller
{
    [Route("api/job/offer")]
    public class JobOfferController : BaseController
    {
        private readonly IJobOfferService _jobOfferService;

        public JobOfferController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<JobOfferResponseDto>> Create(JobOfferRequestDto jobOfferRequestDto)
        {
            var response = await _jobOfferService.CreateJobOffer(jobOfferRequestDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        public async Task<ActionResult<JobOfferResponseDto>> GetAll()
        {
            var response = await _jobOfferService.GetJobOffers();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<JobOfferResponseDto>> GetByCategoryName(string categoryName)
        {
            var response = await _jobOfferService.GetJobOffersByCategory(categoryName);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
