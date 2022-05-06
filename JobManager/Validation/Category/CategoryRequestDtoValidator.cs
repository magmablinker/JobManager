using FluentValidation;
using JobManager.Core.Data.DataTransferObjects.Request.Category;

namespace JobManager.Validation.Category
{
    public class CategoryRequestDtoValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryRequestDtoValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty();
        }
    }
}
