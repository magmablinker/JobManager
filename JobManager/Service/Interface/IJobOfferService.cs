using JobManager.Core.Data.DataTransferObjects.Request.JobOffer;
using JobManager.Core.Data.DataTransferObjects.Response;
using System.Threading.Tasks;

namespace JobManager.Service.Interface
{
    public interface IJobOfferService
    {
        Task<JobOfferResponseDto> CreateJobOffer(JobOfferRequestDto jobOfferRequestDto);
        Task<JobOfferResponseDto> GetJobOffers();
        Task<JobOfferResponseDto> GetJobOffersByCategory(string categoryName);
    }
}
