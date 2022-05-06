using JobManager.Core.Data.DataTransferObjects;
using JobManager.Core.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace JobManager.Core.Data.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(DbCategory dbCategory)
        {
            return new CategoryDto
            {
                Id = dbCategory.Id,
                Name = dbCategory.Name
            };
        }

        public static List<CategoryDto> ToDto(List<DbCategory> dbCategories)
        {
            return dbCategories.Select(ToDto).ToList();
        }
    }
}
