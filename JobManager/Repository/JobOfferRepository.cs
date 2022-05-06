using JobManager.Core.Data.Model;
using JobManager.Core.Database;
using JobManager.Repository.Base;
using JobManager.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Repository
{
    public class JobOfferRepository : GenericRepository<DbJobOffer>, IJobOfferRepository
    {
        public JobOfferRepository(JobManagerContext context) : base(context)
        {

        }

        public async Task<List<DbJobOffer>> SelectAllWithUserAndCategory()
        {
            return await _context.JobOffers
                .Include(jobOffer => jobOffer.JobOfferCategories)
                    .ThenInclude(jobOfferCategories => jobOfferCategories.Category)
                .Include(jobOffer => jobOffer.Employer)
                .ToListAsync();
        }

        public async Task<List<DbJobOffer>> SelectByCategoryId(Guid categoryId) 
        {
            return await _context.JobOffers
                .Where(jobOffer => jobOffer.JobOfferCategories.Any(category => category.Category.Id == category.Category.Id))
                .Include(jobOffer => jobOffer.JobOfferCategories)
                .Include(jobOffer => jobOffer.Employer)
                .ToListAsync();
        }
    }
}
