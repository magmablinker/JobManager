using JobManager.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobManager.Repository.Interface
{
    public interface ICategoryRepository : IRepository<DbCategory>
    {
        Task<List<DbCategory>> SelectByIds(List<Guid> categoryIds);
        Task<DbCategory> SelectByName(string categoryName);
    }
}
