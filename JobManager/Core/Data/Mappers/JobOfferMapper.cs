using JobManager.Core.Data.DataTransferObjects;
using JobManager.Core.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace JobManager.Core.Data.Mappers
{
    public static class JobOfferMapper
    {
        public static JobOfferDto ToDto(DbJobOffer dbJobOffer)
        {
            return new JobOfferDto
            {
                Categories = CategoryMapper.ToDto(dbJobOffer.JobOfferCategories.Select(jc => jc.Category).ToList()),
                CreatedOn = dbJobOffer.CreatedOn,
                Description = dbJobOffer.Description,
                Employer = UserMapper.ToDto(dbJobOffer.Employer),
                Id = dbJobOffer.Id,
                Name = dbJobOffer.Name,
                ModifiedOn = dbJobOffer.ModifiedOn,
                PayPerHour = dbJobOffer.PayPerHour
            };
        }

        public static List<JobOfferDto> ToDto(List<DbJobOffer> dbJobOffers)
        {
            return dbJobOffers.Select(ToDto).ToList();
        }
    }
}
