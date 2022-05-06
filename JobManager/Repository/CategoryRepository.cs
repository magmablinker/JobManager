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
    public class CategoryRepository : GenericRepository<DbCategory>, ICategoryRepository
    {
        public CategoryRepository(JobManagerContext context) : base(context)
        {

        }

        public async Task<List<DbCategory>> SelectByIds(List<Guid> categoryIds)
        {
            // TODO: Better solution for this
            var categories = await _context.Categories.Select(category => category).ToListAsync();

            return categories.Where(category => categoryIds.Contains(category.Id)).ToList();
        }

        public async Task<DbCategory> SelectByName(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(category => category.Name == categoryName);
        }
    }
}
