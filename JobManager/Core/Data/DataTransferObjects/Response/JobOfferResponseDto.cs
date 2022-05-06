using System.Collections.Generic;

namespace JobManager.Core.Data.DataTransferObjects.Response
{
    public class JobOfferResponseDto : BaseResponseDto
    {
        public JobOfferDto JobOffer { get; set; }
        public List<JobOfferDto> JobOffers { get; set; }

        public JobOfferResponseDto()
        {
            JobOffer = new JobOfferDto();
            JobOffers = new List<JobOfferDto>();
        }
    }
}
