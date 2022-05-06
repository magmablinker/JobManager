using JobManager.Core.Data.Model;
using JobManager.Core.Database;
using JobManager.Repository.Base;
using JobManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobManager.Repository
{
    public class JobOfferCategoryRepository : GenericRepository<DbJobOfferCategory>, IJobOfferCategoryRepository
    {
        public JobOfferCategoryRepository(JobManagerContext context) : base(context)
        {

        }

        public async Task InsertCategoriesForJobOffer(DbJobOffer jobOffer, List<DbCategory> categories)
        {
            foreach (var category in categories)
                _context.Add(new DbJobOfferCategory
                {
                    CategoryId = category.Id,
                    JobOfferId = jobOffer.Id
                });

            await _context.SaveChangesAsync();
        }
    }
}
