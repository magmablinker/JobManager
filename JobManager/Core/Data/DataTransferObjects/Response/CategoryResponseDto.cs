using System.Collections.Generic;

namespace JobManager.Core.Data.DataTransferObjects.Response
{
    public class CategoryResponseDto : BaseResponseDto
    {
        public CategoryDto Category { get; set; }
        public List<CategoryDto> Categories { get; set; }

        public CategoryResponseDto()
        {
            Category = new CategoryDto();
            Categories = new List<CategoryDto>();
        }
    }
}
