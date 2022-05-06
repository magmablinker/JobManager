using JobManager.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.Repository.Interface
{
    public interface IJobOfferRepository : IRepository<DbJobOffer>
    {
        Task<List<DbJobOffer>> SelectAllWithUserAndCategory();
        Task<List<DbJobOffer>> SelectByCategoryId(Guid categoryId);
    }
}
