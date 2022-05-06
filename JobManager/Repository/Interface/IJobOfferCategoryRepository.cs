using JobManager.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Repository.Interface
{
    public interface IJobOfferCategoryRepository : IRepository<DbJobOfferCategory>
    {
        Task InsertCategoriesForJobOffer(DbJobOffer jobOffer, List<DbCategory> categories);
    }
}
